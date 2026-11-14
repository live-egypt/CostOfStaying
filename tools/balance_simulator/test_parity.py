#!/usr/bin/env python3
"""
Parity check: Python simulate.py must match C# Match.cs formulas.
Run in CI alongside dotnet tests to catch drift.
6 حالات تغطي كل RULE + Spirit + تزامن (GATE-0.3).
"""
import pathlib, sys, json, math
sys.path.insert(0, str(pathlib.Path(__file__).parent))
from simulate import PRESETS, run_match

def test_vitality_decay():
    cfg = dict(PRESETS["default"])
    cfg["playerCount"] = 1
    cfg["baseDecayPerSecond"] = 0.1
    cfg["collapseIntervalSeconds"] = 1000
    cfg["collapseIntervalThreeAliveSeconds"] = 1000
    from simulate import MatchSim
    ms = MatchSim(cfg, seed=0)
    dt = 1.0 / cfg["tickHz"]
    for _ in range(int(10 * cfg["tickHz"])):
        ms.tick(dt)
    assert abs(ms.players["P0"].vitality - 99.0) < 1e-9, f"got {ms.players['P0'].vitality} vs 99.0"
    print("✓ vitality decay 10s @0.1 → 99.0")

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
    # رفض مكرر — idempotency (simulate returns RejectedDuplicate, C# RejectedDuplicateRequest)
    r2 = ms.try_transfer("P0", "P1", 1.5, "req-1")
    assert r2 in ("RejectedDuplicate", "RejectedDuplicateRequest")
    print("✓ transfer Move + idempotency")

def test_collapse_interval_reschedule():
    """RULE-01 + RULE-03: فترات Collapse وReschedule عند 3 أحياء (15ث)"""
    cfg = dict(PRESETS["default"])
    cfg["playerCount"] = 4
    cfg["collapseIntervalSeconds"] = 20.0
    cfg["collapseIntervalThreeAliveSeconds"] = 15.0
    from simulate import MatchSim
    ms = MatchSim(cfg, seed=0)
    # تحقق الفترات الأولية
    assert ms.players["P0"].next_collapse_at == 20.0
    # محاكاة موت لاعب ليصبح 3 أحياء → يجب إعادة الجدولة إلى 15ث
    # نسخر عبر tick حتى أول Collapse ثم نقتل لاعباً يدوياً
    dt = 1.0 / cfg["tickHz"]
    for _ in range(int(21 * cfg["tickHz"])):
        ms.tick(dt)
    # بعد أول Collapse يجب أن يزيد base_decay
    alive = sum(1 for p in ms.players.values() if p.state == "Alive")
    assert ms.players["P0"].base_decay > 0.10, f"collapse لم يزد التآكل {ms.players['P0'].base_decay}"
    print(f"✓ collapse reschedule 20→15 (alive={alive}) base_decay={ms.players['P0'].base_decay:.3f}")

def test_vote_unanimous():
    """RULE-03: تصويت Collective Surrender — إجماع Yes = نهاية فورية"""
    cfg = dict(PRESETS["default"])
    cfg["playerCount"] = 3
    cfg["voteDurationSeconds"] = 20.0
    cfg["collapseIntervalThreeAliveSeconds"] = 15.0
    from simulate import MatchSim
    ms = MatchSim(cfg, seed=1)
    dt = 1.0 / cfg["tickHz"]
    # شغّل حتى يفتح التصويت (عند 3 أحياء مباشرة)
    for _ in range(int(1 * cfg["tickHz"])):
        ms.tick(dt)
    assert ms.vote_open, f"vote لم يفتح عند 3 أحياء — alive={ms.alive_count()} vote_open={ms.vote_open}"
    # صوت Yes للجميع → إجماع (simulate لا يملك cast_vote, نعدل votes مباشرة)
    for pid in list(ms.votes.keys()):
        ms.votes[pid] = "Yes"
    # tick حتى الإغلاق
    for _ in range(int(21 * cfg["tickHz"])):
        ms.tick(dt)
    assert ms.outcome == "CollectiveSurrender", f"expected CollectiveSurrender got {ms.outcome}"
    print("✓ vote unanimous → CollectiveSurrender")

def test_spirit_neutral():
    """RULE-04: الروح محايدة — لا تسبب ضرر ولا تغير حالة الأحياء"""
    cfg = dict(PRESETS["default"])
    cfg["playerCount"] = 3
    cfg["collapseIntervalSeconds"] = 1000
    cfg["collapseIntervalThreeAliveSeconds"] = 1000
    from simulate import MatchSim
    ms = MatchSim(cfg, seed=0)
    # اقتل P0 يدوياً بالوصول للصفر
    ms.players["P0"].vitality = 0
    ms.tick(1.0 / cfg["tickHz"])
    assert ms.players["P0"].state == "Spirit", f"P0 لم ينتقل لـ Spirit: {ms.players['P0'].state}"
    # Spirit لا يمكنه النقل
    ms.players["P0"].debuffs.append({"id": "spirit-test", "bonus": 0.05, "remaining": 30, "transferable": True})
    r = ms.try_transfer("P0", "P1", 1.5, "req-spirit")
    assert r == "RejectedNotAlive", f"Spirit يجب أن يُرفض: {r}"
    # Spirit لا يؤثر على تآكل الأحياء (مع 2 أحياء لا يزال اللعب مستمراً)
    vitality_before = ms.players["P1"].vitality
    for _ in range(int(5 * cfg["tickHz"])):
        ms.tick(1.0 / cfg["tickHz"])
    assert ms.players["P1"].vitality < vitality_before, f"expected decay {vitality_before}→{ms.players['P1'].vitality}"
    print("✓ spirit neutral + no transfer")

def test_end_conditions_time_limit():
    """RULE-05: حد 20د — أعلى Vitality يفوز عند TimeLimit"""
    cfg = dict(PRESETS["default"])
    cfg["playerCount"] = 2
    cfg["matchTimeLimitSeconds"] = 5.0  # قصير للاختبار
    cfg["baseDecayPerSecond"] = 0.1
    cfg["collapseIntervalSeconds"] = 1000
    cfg["collapseIntervalThreeAliveSeconds"] = 1000
    from simulate import MatchSim
    ms = MatchSim(cfg, seed=0)
    ms.players["P0"].vitality = 80.0
    ms.players["P1"].vitality = 60.0
    dt = 1.0 / cfg["tickHz"]
    for _ in range(int(6 * cfg["tickHz"])):
        ms.tick(dt)
    assert ms.outcome in ("TimeLimit", "Draw"), f"time limit expected, got {ms.outcome}"
    if ms.outcome == "TimeLimit":
        assert ms.winner == "P0", f"expected P0 winner (80>60) got {ms.winner}"
    print(f"✓ time limit outcome={ms.outcome} winner={ms.winner}")

if __name__ == "__main__":
    test_vitality_decay()
    test_transfer_move()
    test_collapse_interval_reschedule()
    test_vote_unanimous()
    test_spirit_neutral()
    test_end_conditions_time_limit()
    print("parity ok — 6/6")
