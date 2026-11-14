#!/usr/bin/env python3
"""
Parity check: Python simulate.py must match C# Match.cs formulas.
This is run in CI alongside dotnet tests to catch drift.
We check a few deterministic scenarios by re-implementing the same
logic that the C# tests cover, but here we assert the Python mirror
produces the same numbers.
"""
import pathlib, sys, json
sys.path.insert(0, str(pathlib.Path(__file__).parent))
from simulate import PRESETS, run_match

def test_vitality_decay():
    cfg = dict(PRESETS["default"])
    cfg["playerCount"] = 1
    cfg["baseDecayPerSecond"] = 0.1
    cfg["collapseIntervalSeconds"] = 1000
    cfg["collapseIntervalThreeAliveSeconds"] = 1000
    m = run_match(cfg, seed=0, random_transfers=False, random_votes=False)
    # Tick 10s manually via run_match is not granular; instead use direct sim
    from simulate import MatchSim
    ms = MatchSim(cfg, seed=0)
    dt = 1.0 / cfg["tickHz"]
    for _ in range(int(10 * cfg["tickHz"])):
        ms.tick(dt)
    assert abs(ms.players["P0"].vitality - 99.0) < 1e-9, f"got {ms.players['P0'].vitality}"

def test_transfer_move():
    cfg = dict(PRESETS["default"])
    cfg["playerCount"] = 2
    cfg["collapseIntervalSeconds"] = 1000
    cfg["collapseIntervalThreeAliveSeconds"] = 1000
    from simulate import MatchSim
    ms = MatchSim(cfg, seed=0)
    ms.players["P0"].debuffs.append({"id": "test", "bonus": 0.05, "remaining": 30, "transferable": True})
    r = ms.try_transfer("P0", "P1", 1.5, "req-1")
    assert r == "Success"
    assert len(ms.players["P0"].debuffs) == 0
    assert len(ms.players["P1"].debuffs) == 1
    assert ms.players["P0"].betrayal == 1

if __name__ == "__main__":
    test_vitality_decay()
    test_transfer_move()
    print("parity ok")
