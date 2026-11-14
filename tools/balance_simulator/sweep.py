#!/usr/bin/env python3
"""
Parametric sweep over decay rates / intervals. Outputs CSV for analysis.
This is a DESIGN TOOL — not VERIFIED performance data.
"""
import argparse
import csv
import json
import pathlib
import itertools
import time

from simulate import PRESETS, run_match

def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--preset", default="default")
    ap.add_argument("--runs", type=int, default=20, help="runs per config")
    ap.add_argument("--output", default=None)
    args = ap.parse_args()

    base = PRESETS[args.preset]
    # Sweep axes (proposed test values, not approved)
    decays = [0.06, 0.10, 0.14]
    intervals = [15.0, 20.0, 25.0]
    cooldowns = [3.0, 5.0, 8.0]

    rows = []
    for decay, interval, cd in itertools.product(decays, intervals, cooldowns):
        cfg = dict(base)
        cfg["baseDecayPerSecond"] = decay
        cfg["collapseIntervalSeconds"] = interval
        cfg["transferCooldownSeconds"] = cd
        for seed in range(args.runs):
            m = run_match(cfg, seed=seed)
            rows.append({
                "decay": decay,
                "interval": interval,
                "cooldown": cd,
                "seed": seed,
                "duration": round(m.now, 1),
                "outcome": m.outcome,
                "winner": m.winner or "",
                "transfers": sum(1 for _, k, _ in m.events if k == "transfer"),
                "collapses": sum(1 for _, k, _ in m.events if k == "collapse"),
            })

    out = pathlib.Path(args.output) if args.output else pathlib.Path(f"sweep-{time.strftime('%Y%m%d-%H%M%S')}.csv")
    out.parent.mkdir(parents=True, exist_ok=True)
    with open(out, "w", newline="", encoding="utf-8") as f:
        w = csv.DictWriter(f, fieldnames=list(rows[0].keys()))
        w.writeheader()
        w.writerows(rows)

    # Print distribution
    from collections import Counter
    cnt = Counter(r["outcome"] for r in rows)
    print(f"Wrote {len(rows)} rows to {out}")
    print("Outcome distribution:", dict(cnt))
    print("Note: PROPOSED TEST VALUES only — not approved balance.")

if __name__ == "__main__":
    main()
