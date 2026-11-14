#!/usr/bin/env python3
"""
Deterministic match simulator mirroring src/CostOfStaying.Domain/Match.cs.
All formulas and policies are identical to the C# domain so results are
comparable and parity-tested (see test_parity.py).

This is a DESIGN TOOL — outputs are hypotheses, not VERIFIED performance.
"""
import argparse
import json
import math
import random
import pathlib
import time

ROOT = pathlib.Path(__file__).resolve().parents[2]
PRESETS_PATH = pathlib.Path(__file__).with_name("presets.json")

with open(PRESETS_PATH, encoding="utf-8") as f:
    PRESETS = json.load(f)

# ---------------------------------------------------------------------------
# Domain mirror (simplified but formula-identical to Match.cs)
# ---------------------------------------------------------------------------

class Player:
    def __init__(self, pid, cfg, now):
        self.id = pid
        self.state = "Alive"
        self.vitality = cfg["startingVitality"]
        self.base_decay = cfg["baseDecayPerSecond"]
        self.debuffs = []  # list of dict {id, bonus, remaining, transferable}
        self.betrayal = 0
        self.last_transfer_at = float("-inf")
        self.next_collapse_at = now + current_interval(cfg, 4)
        self.last_interval = current_interval(cfg, 4)

    def total_decay(self):
        return self.base_decay + sum(d["bonus"] for d in self.debuffs)

    def last_transferable(self):
        for d in reversed(self.debuffs):
            if d["transferable"]:
                return d
        return None

def current_interval(cfg, alive):
    return cfg["collapseIntervalThreeAliveSeconds"] if alive == 3 else cfg["collapseIntervalSeconds"]

class MatchSim:
    def __init__(self, cfg, seed=0):
        self.cfg = cfg
        self.now = 0.0
        self.players = {f"P{i}": Player(f"P{i}", cfg, self.now) for i in range(cfg["playerCount"])}
        self.started_at = 0.0
        self.vote_open = False
        self.vote_opened_at = None
        self.vote_deadline = None
        self.votes = {}
        self.vote_has_opened = False
        self.outcome = None
        self.winner = None
        self.events = []
        self.rng = random.Random(seed)
        self.processed_req = set()

    def alive_count(self):
        return sum(1 for p in self.players.values() if p.state == "Alive")

    def tick(self, dt):
        if self.outcome:
            return
        self.now += dt

        # 1) vitality drain + debuff expiry
        for p in self.players.values():
            if p.state != "Alive":
                continue
            p.vitality = max(0.0, p.vitality - p.total_decay() * dt)
            # expiry
            new_debuffs = []
            for d in p.debuffs:
                d["remaining"] -= dt
                if d["remaining"] > 0:
                    new_debuffs.append(d)
            p.debuffs = new_debuffs

        # 2) death -> spirit
        for p in self.players.values():
            if p.state == "Alive" and p.vitality <= 0:
                p.state = "Spirit"
                p.vitality = 0
                self.events.append((self.now, "died", p.id))

        # 3) collapse
        if not self.outcome:
            for p in list(self.players.values()):
                if p.state != "Alive":
                    continue
                if self.now >= p.next_collapse_at - 1e-9:
                    p.base_decay += self.cfg["collapseDecayIncreasePerSecond"]
                    if len(p.debuffs) < self.cfg["maxActiveDebuffs"]:
                        p.debuffs.append({
                            "id": f"collapse-{self.now:.1f}-{p.id}",
                            "bonus": self.cfg["debuffDecayBonusPerSecond"],
                            "remaining": self.cfg["debuffDurationSeconds"],
                            "transferable": True
                        })
                    iv = current_interval(self.cfg, self.alive_count())
                    p.next_collapse_at = self.now + iv
                    p.last_interval = iv
                    self.events.append((self.now, "collapse", p.id))

        # 4) reschedule if alive changed (ratio-preserving)
        # We do it lazily by tracking alive before; simpler: recompute when needed
        # The C# does explicit reschedule after death; we mimic by ratio
        # For simulation we track previous alive
        # (Handled via explicit method called after death — simplified: if alive changed, reschedule)
        # We store last alive in Tick wrapper outside.

        # 5) vote timeout
        if self.vote_open and self.vote_deadline is not None and self.now >= self.vote_deadline:
            self._close_vote()

        # 6) maybe open vote
        if not self.vote_open and not self.vote_has_opened and self.alive_count() == 3:
            self._open_vote()

        # 7) end conditions
        self._evaluate_end()

    def _open_vote(self):
        self.vote_open = True
        self.vote_has_opened = True
        self.vote_opened_at = self.now
        self.vote_deadline = self.now + self.cfg["voteDurationSeconds"]
        self.votes = {pid: "Abstain" for pid, p in self.players.items() if p.state == "Alive"}
        self.events.append((self.now, "vote_open", None))

    def _close_vote(self):
        unanimous = len(self.votes) > 0 and all(v == "Yes" for v in self.votes.values())
        self.events.append((self.now, "vote_close", "unanimous" if unanimous else "not unanimous"))
        if unanimous:
            self.outcome = "CollectiveSurrender"
            self.winner = None
            self.vote_open = False
            return
        self.vote_open = False
        self.votes = {}
        self.vote_opened_at = None
        self.vote_deadline = None
        # reschedule to 3-alive interval
        new_iv = current_interval(self.cfg, self.alive_count())
        for p in self.players.values():
            if p.state == "Alive":
                old_iv = p.last_interval
                remaining = p.next_collapse_at - self.now
                ratio = max(0.0, min(1.0, remaining / old_iv if old_iv else 1))
                p.next_collapse_at = self.now + ratio * new_iv
                p.last_interval = new_iv

    def _evaluate_end(self):
        if self.outcome:
            return
        alive = [p for p in self.players.values() if p.state == "Alive"]
        has_death = any(p.state in ("Spirit", "Dead") for p in self.players.values())
        if len(alive) == 0 and has_death:
            self.outcome = "MutualCollapse"
            self.winner = None
            return
        if self.now - self.started_at >= self.cfg["matchTimeLimitSeconds"] - 1e-9:
            max_v = max(p.vitality for p in alive)
            top = [p for p in alive if abs(p.vitality - max_v) < 1e-9]
            if len(top) > 1:
                self.outcome = "Draw"
                self.winner = None
            else:
                self.outcome = "TimeLimit"
                self.winner = top[0].id
            return
        if len(alive) == 1 and has_death:
            self.outcome = "LastSurvivor"
            self.winner = alive[0].id
            return

    def try_transfer(self, src_id, tgt_id, distance, req_id):
        if req_id and req_id in self.processed_req:
            return "RejectedDuplicate"
        if src_id == tgt_id:
            return "RejectedSelf"
        if src_id not in self.players or tgt_id not in self.players:
            return "RejectedNotAlive"
        src = self.players[src_id]
        tgt = self.players[tgt_id]
        if src.state != "Alive" or tgt.state != "Alive":
            return "RejectedNotAlive"
        cand = src.last_transferable()
        if not cand:
            return "RejectedNoDebuff"
        if distance > self.cfg["transferRangeMeters"] + 1e-9:
            return "RejectedOutOfRange"
        if self.now - src.last_transfer_at < self.cfg["transferCooldownSeconds"] - 1e-9:
            return "RejectedCooldown"
        if len(tgt.debuffs) >= self.cfg["maxActiveDebuffs"]:
            return "RejectedTargetFull"
        # atomic move
        src.debuffs.remove(cand)
        # preserve remaining
        tgt.debuffs.append({"id": cand["id"], "bonus": cand["bonus"], "remaining": cand["remaining"], "transferable": cand["transferable"]})
        src.betrayal += 1
        src.last_transfer_at = self.now
        if req_id:
            self.processed_req.add(req_id)
        self.events.append((self.now, "transfer", f"{src_id}->{tgt_id} {cand['id']}"))
        return "Success"

    # Reschedule helper mirroring C# after alive change
    def reschedule_after_death(self):
        new_iv = current_interval(self.cfg, self.alive_count())
        for p in self.players.values():
            if p.state != "Alive":
                continue
            old_iv = p.last_interval
            if old_iv <= 0:
                p.next_collapse_at = self.now + new_iv
                p.last_interval = new_iv
                continue
            remaining = p.next_collapse_at - self.now
            ratio = max(0.0, min(1.0, remaining / old_iv))
            p.next_collapse_at = self.now + ratio * new_iv
            p.last_interval = new_iv

def run_match(cfg, seed=0, random_transfers=True, random_votes=True):
    m = MatchSim(cfg, seed)
    dt = 1.0 / cfg["tickHz"]
    # Simulate up to time limit + vote
    max_steps = int((cfg["matchTimeLimitSeconds"] + 60) * cfg["tickHz"]) + 1000
    last_alive = m.alive_count()
    transfer_counter = 0

    for step in range(max_steps):
        # Random transfer attempts (server validates everything)
        if random_transfers and m.rng.random() < 0.08:  # ~8% per tick
            alive_ids = [pid for pid, p in m.players.items() if p.state == "Alive" and p.last_transferable()]
            if len(alive_ids) >= 2:
                src = m.rng.choice(alive_ids)
                # pick different alive target
                candidates = [pid for pid in m.players if m.players[pid].state == "Alive" and pid != src]
                if candidates:
                    tgt = m.rng.choice(candidates)
                    dist = m.rng.uniform(0.5, 3.0)
                    req = f"req-{transfer_counter}"
                    transfer_counter += 1
                    m.try_transfer(src, tgt, dist, req)

        # Random vote choices when vote open
        if random_votes and m.vote_open:
            for pid in list(m.votes.keys()):
                if m.votes[pid] == "Abstain" and m.rng.random() < 0.3:
                    # 70% Yes bias for testing unanimous path
                    m.votes[pid] = "Yes" if m.rng.random() < 0.7 else "No"

        m.tick(dt)

        # Detect alive change for reschedule (mirror C# flag)
        cur_alive = m.alive_count()
        if cur_alive != last_alive:
            m.reschedule_after_death()
            last_alive = cur_alive

        if m.outcome:
            break

    return m

def main():
    ap = argparse.ArgumentParser(description="CostOfStaying balance simulator")
    ap.add_argument("--preset", default="default", choices=list(PRESETS.keys()))
    ap.add_argument("--seed", type=int, default=42)
    ap.add_argument("--json", action="store_true", help="output machine-readable JSON")
    ap.add_argument("--no-random", action="store_true", help="disable random transfers/votes")
    args = ap.parse_args()

    cfg = PRESETS[args.preset]
    m = run_match(cfg, seed=args.seed, random_transfers=not args.no_random, random_votes=not args.no_random)

    duration = m.now
    alive = [(pid, p.vitality, p.betrayal, len(p.debuffs), p.state) for pid, p in m.players.items()]
    summary = {
        "preset": args.preset,
        "seed": args.seed,
        "duration_seconds": round(duration, 2),
        "outcome": m.outcome,
        "winner": m.winner,
        "players": [{"id": pid, "vitality": round(v, 2), "betrayal": b, "debuffs": d, "state": s} for pid, v, b, d, s in alive],
        "events": len(m.events),
        "transfers": sum(1 for _, k, _ in m.events if k == "transfer"),
        "collapses": sum(1 for _, k, _ in m.events if k == "collapse"),
    }

    if args.json:
        print(json.dumps(summary, ensure_ascii=False, indent=2))
    else:
        print(f"Preset: {args.preset}  Seed: {args.seed}")
        print(f"Duration: {duration:.1f}s  Outcome: {m.outcome}  Winner: {m.winner}")
        print("Players:")
        for p in summary["players"]:
            print(f"  {p['id']}: vitality={p['vitality']} betrayal={p['betrayal']} debuffs={p['debuffs']} state={p['state']}")
        print(f"Events: {len(m.events)} (transfers={summary['transfers']} collapses={summary['collapses']})")
        if m.events:
            print("Last 10 events:")
            for t, k, d in m.events[-10:]:
                print(f"  {t:6.1f}s  {k}  {d}")

if __name__ == "__main__":
    main()
