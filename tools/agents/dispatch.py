#!/usr/bin/env python3
"""
Agent dispatch helper — يوزع المهام على الوكلاء ويفحص تطابق الكتالوج مع اللوحة.
Usage: python3 tools/agents/dispatch.py --gate 3 --agent "Net Sage"
"""
import argparse, pathlib, sys, re

ROOT = pathlib.Path(__file__).resolve().parents[2]
BOARD = ROOT / "docs/governance/EXECUTION_BOARD.md"
CHARTER = ROOT / "docs/governance/AGENT_CHARTER.md"
PLAN = ROOT / "docs/roadmap/WORK_PLAN_V2_DETAILED.md"

AGENTS = ["Governor","Architect","Domain Guardian","Net Sage","Sentinel","Unity Forger","Balancer","Playtest Oracle","Verifier","DevOps Herald"]

def check_catalog():
    gov = (ROOT / "docs/governance/DOCUMENT_GOVERNANCE.md").read_text(encoding="utf-8")
    missing = []
    for p in [BOARD, CHARTER, PLAN]:
        rel = str(p.relative_to(ROOT / "docs"))
        if rel not in gov:
            missing.append(rel)
    return missing

def tasks_for(gate: int):
    text = BOARD.read_text(encoding="utf-8")
    section = f"GATE-{gate}"
    tasks = [l for l in text.splitlines() if re.match(r"\|\s*\d+\.\d+", l) and section in text.split(l)[0][-2000:]]
    # fallback: simple scan
    return [l for l in text.splitlines() if re.match(r"\|\s*"+str(gate)+r"\.\d+", l)]

if __name__ == "__main__":
    ap = argparse.ArgumentParser()
    ap.add_argument("--gate", type=int, required=True, help="Gate number 0-7")
    ap.add_argument("--agent", type=str, help="Agent name")
    ap.add_argument("--check", action="store_true", help="Check catalog vs board")
    args = ap.parse_args()

    if args.check:
        miss = check_catalog()
        if miss:
            print(f"NOT IN CATALOG: {miss}")
            sys.exit(1)
        print("catalog OK")
        sys.exit(0)

    if args.agent and args.agent not in AGENTS:
        print(f"unknown agent {args.agent}. choose from {AGENTS}")
        sys.exit(1)

    t = tasks_for(args.gate)
    print(f"Gate {args.gate}: {len(t)} tasks")
    for line in t:
        if not args.agent or args.agent.lower() in line.lower():
            print(line)
    if args.agent:
        print(f"\nAssigned to {args.agent} — open PR per task <400 lines + evidence")
