# النواة المجردة — Domain Core — v0.1
**الحالة: PROPOSED | التنفيذ في `src/CostOfStaying.Domain` على فرع `arena/4bfc0882-costofstaying` | لا VERIFIED قبل CI أخضر على GitHub**

هذه الوثيقة تصف التنفيذ المقترح لمنطق اللعبة المجرد عن Unity/FishNet تنفيذاً لمبدأ فصل النواة في `TECHNICAL_ARCHITECTURE.md` وADR-001/ADR-002.

## الهدف
توفير منطق خادمي خالص يطبق RULE-01..05 كما في `02_GAME_RULES_SPECIFICATION.md` دون اعتماد على `MonoBehaviour` أو `NetworkBehaviour` أو ساعة العميل، حتى يمكن اختباره وقياس توازنه قبل بناء Unity.

## ما هو منفذ (PROPOSED — ليس APPROVED ولا VERIFIED)
- `src/CostOfStaying.Domain/` — مشروع `netstandard2.1` (محمول إلى Unity 6 لاحقاً):
  - `MatchConfig.cs` — جميع القيم القابلة للضبط في مكان واحد؛ القيم المسماة في RULE (Vitality 100، تصويت 20ث، 15ث لثلاثة أحياء، 20د حد) مفصولة عن قيم الاختبار الأولية (معدل التآكل، التبريد، إلخ) المشار إليها كـ PROPOSED TEST VALUE.
  - `Player.cs`, `Debuff.cs`, `Enums.cs` — حالات اللاعب/المباراة والنتائج الخمس المميزة.
  - `Clock.cs` — `IMatchClock` مع `ManualClock` للاختبارات و`MonotonicClock` للخادم (لا يعتمد على wall-clock).
  - `Match.cs` — تجميع خادمي واحد:
    - تآكل Vitality وانتهاء Debuff وتحويل Alive→Spirit عند الصفر (RULE-01).
    - Collapse يزيد التآكل ويطبق Debuff ويحترم `MaxActiveDebuffs`، وإعادة جدولة تحافظ النسبة عند تغير عدد الأحياء (COLLAPSE_SYSTEM).
    - نقل المعاناة Move ذري مع idempotency بـ `RequestId`، يزيد Betrayal نقطة فقط عند النجاح (RULE-02).
    - تصويت Collective Surrender عند 3 أحياء، 20ث، إجماع 3/3، الامتناع/الانقطاع/المهلة = No، مرة واحدة في MVP (RULE-03).
    - Spirit يجمع Echo ويحقق WitnessScore وتلميح مع cooldown (RULE-04، Energy TBD).
    - النتائج الخمس: Last Survivor / Collective Surrender / Time Limit / Draw / Mutual Collapse (RULE-05).
  - `Intent.cs` — DTOs لنوايا العميل (التزام ADR-002: العميل يرسل Intent فقط).

- `tests/CostOfStaying.Domain.Tests/` — مشروع `net8.0` بـ NUnit:
  - `VitalityTests`, `TransferTests`, `CollapseTests`, `VoteAndEndgameTests`, `SpiritTests`, `SecurityTests`
  - تغطي: التآكل، الموت، Collapse لا يقتل فوراً، النقل الذري وحدوده، التصويت والإجماع، النتائج الخمس، Spirit المحايد، وحواجز سلطة الخادم (رفض المسافة المزعومة، منع replay، Betrayal فقط عند النجاح).

- `tools/balance_simulator/` — محاكي Python يعكس نفس الصيغ لاختبار التوازن؛ `test_parity.py` يتحقق من التطابق.
- `tools/playtest/run_sessions.py` — مولد جلسات محاكاة موثقة (≥10) ينتج `session-*.json` و`summary.csv` و`report.md` مطلوبة لبوابة ADR-004.
- `tools/audit.py` — تدقيق الروابط والكتالوج والحالات.

## ما ليس منفذاً
- لا Unity project، لا FishNet replication، لا Dedicated Server قابل للتشغيل، لا Android build.
- الأرقام خارج RULE (معدل التآكل، زيادة Collapse، مدة Debuff، التبريد) تبقى PROPOSED TEST VALUE حتى Playtest بشري.
- لا VERIFIED قبل CI أخضر على GitHub Actions يحمل SHA وartifacts.

## كيف يُختبر
```bash
dotnet restore && dotnet build -c Release
dotnet test -c Release --collect:"XPlat Code Coverage"
python3 tools/balance_simulator/test_parity.py
python3 tools/playtest/run_sessions.py --sessions 10 --preset default
python3 tools/audit.py
```
CI في `.github/workflows/ci.yml` يشغل كل ذلك عند كل push/PR.

## الحوكمة
أي تغيير في RULE يحدث في `02_GAME_RULES_SPECIFICATION.md` ووثيقة النظام المقابل وADR-004 و`MatchConfig.cs` معاً. لا ترفع الحالة إلى APPROVED/IMPLEMENTED/VERIFIED دون دليل في CI.
