#!/usr/bin/env python3
"""
Run N simulated playtest sessions and emit logs required by DELIVERY_PLAN
and ADR-004 gate (≥10 documented sessions before APPROVED).
This is a SIMULATED harness — not a substitute for human playtest on a
real build, but provides the required data shape and CI check.
"""
import argparse
import csv
import json
import pathlib
import time
import sys

ROOT = pathlib.Path(__file__).resolve().parents[2]
sys.path.insert(0, str(ROOT / "tools" / "balance_simulator"))

from simulate import PRESETS, run_match

def main():
    ap = argparse.ArgumentParser(description="CostOfStaying playtest harness")
    ap.add_argument("--sessions", type=int, default=10, help="number of sessions (≥10 for gate)")
    ap.add_argument("--preset", default="default", choices=list(PRESETS.keys()))
    ap.add_argument("--seed", type=int, default=1, help="base seed (session i uses seed+i)")
    ap.add_argument("--output", default="docs/quality/playtest-logs", help="output directory")
    args = ap.parse_args()

    cfg = PRESETS[args.preset]
    out_dir = pathlib.Path(args.output)
    # If relative, resolve against repo root
    if not out_dir.is_absolute():
        out_dir = ROOT / out_dir
    out_dir.mkdir(parents=True, exist_ok=True)

    timestamp = time.strftime("%Y%m%d-%H%M%S")
    summaries = []

    for i in range(args.sessions):
        seed = args.seed + i
        m = run_match(cfg, seed=seed)
        duration = m.now
        death_times = []
        # Approximate death times from events
        for t, kind, detail in m.events:
            if kind == "died":
                death_times.append(round(t, 1))

        session = {
            "session_id": f"sim-{timestamp}-{i+1:02d}",
            "preset": args.preset,
            "seed": seed,
            "started_at": timestamp,
            "duration_seconds": round(duration, 1),
            "outcome": m.outcome,
            "winner": m.winner,
            "death_times": death_times,
            "transfers": sum(1 for _, k, _ in m.events if k == "transfer"),
            "collapses": sum(1 for _, k, _ in m.events if k == "collapse"),
            "voted": m.vote_has_opened,
            "players": [
                {"id": pid, "state": p.state, "vitality": round(p.vitality, 2), "betrayal": p.betrayal, "debuffs": len(p.debuffs)}
                for pid, p in m.players.items()
            ],
            "events": [{"t": round(t,1), "kind": k, "detail": str(d)} for t, k, d in m.events],
            "config": cfg,
            "simulator_version": "0.1",
            "note": "SIMULATED — not human playtest. Required human sessions must be logged separately."
        }

        fname = out_dir / f"session-{timestamp}-{i+1:02d}.json"
        with open(fname, "w", encoding="utf-8") as f:
            json.dump(session, f, ensure_ascii=False, indent=2)

        summaries.append({
            "session": session["session_id"],
            "seed": seed,
            "duration": session["duration_seconds"],
            "outcome": session["outcome"],
            "winner": session["winner"] or "",
            "transfers": session["transfers"],
            "collapses": session["collapses"],
            "deaths": len(death_times),
            "voted": session["voted"],
        })
        print(f"Session {i+1}/{args.sessions}: {session['outcome']} in {duration:.1f}s (seed {seed})")

    # Summary CSV
    csv_path = out_dir / f"summary-{timestamp}.csv"
    with open(csv_path, "w", newline="", encoding="utf-8") as f:
        if summaries:
            w = csv.DictWriter(f, fieldnames=list(summaries[0].keys()))
            w.writeheader()
            w.writerows(summaries)

    # Report md
    from collections import Counter
    cnt = Counter(s["outcome"] for s in summaries)
    avg_dur = sum(s["duration"] for s in summaries) / len(summaries) if summaries else 0
    avg_transfers = sum(s["transfers"] for s in summaries) / len(summaries) if summaries else 0

    report = f"""# Playtest Report — {timestamp}
**Preset:** {args.preset} | **Sessions:** {args.sessions} | **Seed base:** {args.seed}

> SIMULATED sessions — human playtest still required for ADR-004. These logs
> provide data shape and distribution preview, not VERIFIED results.

## Summary
- Average duration: {avg_dur:.1f}s
- Average transfers per match: {avg_transfers:.1f}

## Outcome distribution (RULE-05 — 5 outcomes)
"""
    for outcome in ["LastSurvivor", "CollectiveSurrender", "TimeLimit", "Draw", "MutualCollapse"]:
        report += f"- {outcome}: {cnt.get(outcome, 0)}\n"

    report += f"""
## Sessions
| Session | Duration | Outcome | Transfers | Collapses |
|---|---:|---|---:|---:|
"""
    for s in summaries:
        report += f"| {s['session']} | {s['duration']:.1f}s | {s['outcome']} | {s['transfers']} | {s['collapses']} |\n"

    report += """
## Gate checklist (ADR-004 / DELIVERY_PLAN)
- [ ] ≥10 documented sessions: """ + ("✅" if args.sessions >= 10 else "❌") + f" ({args.sessions})\n"
    report += """- [ ] Duration, death times, transfer counts, 5-outcome distribution logged: ✅ (this report)
- [ ] Quit rate & comprehension notes: requires human sessions (add manually)
- [ ] Human notes reviewed before APPROVED: ⏳

## Files
"""
    report += f"- CSV: `{csv_path.name}`\n"
    report += f"- Sessions: `session-{timestamp}-*.json`\n"

    report_path = out_dir / f"report-{timestamp}.md"
    with open(report_path, "w", encoding="utf-8") as f:
        f.write(report)

    print(f"\nWrote {args.sessions} sessions to {out_dir}")
    print(f"CSV: {csv_path}")
    print(f"Report: {report_path}")
    print(f"Distribution: {dict(cnt)}")

if __name__ == "__main__":
    main()
