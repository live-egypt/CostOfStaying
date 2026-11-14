# ADR-021 — نقل قواعد اللعب Domain إلى النواة الموحدة
**الحالة: PROPOSED | التاريخ: 2026-11-14 | الفرع: `arena/4bfc0882-costofstaying` | يحل محل ADR-005 (Domain) المكرر**

## السياق

المستودع يحتوي على خطين متوازيين لم يُوفّق بينهما (انظر `AUDIT_REPORT_2026-11-14.md` §2):

- **خط Core** (`feature/phase1-*`, `fix/p1-*`, `fix/p2-*`): بنية أوامر وجلسات صارمة — ADR-005 إلى ADR-021 (Core Executable Boundary, Identifier/Ledger, Lifecycle Vitality Conflict, Command Envelope, Session, Authorization إلخ). يملك `src/CostOfStaying.Core` (netstandard2.1) واختبارات NUnit بجودة عالية (تحذيرات=أخطاء، حدود بنائية)، لكنه بلا قواعد RULE-01..05.

- **خط Domain** (هذا الفرع `arena/4bfc0882-costofstaying`): قواعد اللعب RULE-01..05 مكتملة تقريباً في `src/CostOfStaying.Domain` (895 سطر) + أدوات `balance_simulator` و`playtest` التي تحقق بوابة Playtest، لكنه بلا طبقة جلسات/أوامر.

الرقم **ADR-005 مستخدم مرتين**: Core يستخدمه لـ `CORE-EXECUTABLE-BOUNDARY` (APPROVED 2026-11-01)، وهذا الخط استخدمه لـ `CI-AND-DOMAIN-CORE` (PROPOSED 2026-11-14). هذا التعارض يجب حله دون حذف تاريخ.

## القرار المقترح

1. **النواة الموحدة هي Core**: `Core` من الخط `feature/phase1-*` هو الحد القابل للتنفيذ الوحيد (Single Canonical Executable Boundary). يحافظ على صرامته: `LangVersion 9.0` (Unity 6)، تحذيرات البناء أخطاء، حدود بنائية بـ NetArchTest بدل `grep`.

2. **نقل قواعد Domain كشريحة ثانية**: تُدمج قواعد `src/CostOfStaying.Domain` التالية في Core كخدمات خالصة بلا Unity/FishNet، مع الحفاظ على نفس الضمانات:
   - `RULE-01` Vitality 100→0 + Collapse (زيادة تآكل + Debuff + إعادة جدولة نسبة) → `Match.Tick()` + `ApplyCollapse()` + `RescheduleCollapses()`
   - `RULE-02` Suffering Transfer Move ذري كامل + 6 شروط + idempotency `RequestId` → `Match.TryTransfer()`
   - `RULE-03` تصويت Collective Surrender عند 3 أحياء (20ث, إجماع 3/3) → `Match.OpenVote()/CloseVote()`
   - `RULE-04` Spirit محايد + Echo + Hint → `Match.CollectEcho()/TrySendHint()`
   - `RULE-05` النتائج الخمس (Draw منفصلة) → `Match.EvaluateEndConditions()`
   - الأدوات `tools/balance_simulator` و`tools/playtest` تبقى كما هي لتحقيق بوابة Playtest.

3. **حجز الأرقام**: ADR-005 إلى ADR-020 تبقى لخط Core (موافقة المالك 2026-11-01 + سلسلة P1/P2). هذا القرار يأخذ **ADR-021**. الأدوات المحاكية لا تأخذ رقم ADR.

4. **إحلال**: الملف `ADR-005-CI-AND-DOMAIN-CORE.md` يبقى في التاريخ كمرجع، لكنه **مُستبدل** (superseded) بهذا ADR-021. لا تحذف الملف — أضف ترويسة `SUPERSEDED by ADR-021`.

## المبررات

- Core أصرم تقنياً: يعالج الهوية/الجلسة/التخويل/الطلب قبل القواعد — وهو الترتيب الصحيح (لا قواعد بلا جلسة موثوقة).
- Domain يملك الأدوات التي تحقق بوابة Playtest (§8 الميثاق: "Playtest أولاً") — لا تُعاد كتابتها.
- توحيد النواة ينهي التشعب الذي سببه عدم حجز الأرقام والفروع المتراصة.

## البدائل المرفوضة

- **الإبقاء على خطين**: يضاعف العمل ويثبت رقم ADR-005 المكرر.
- **Domain كأساس + نقل Core إليه**: يفقد صرامة Core (تحذيرات=أخطاء, اختبارات الحدود) ويؤخر البنية التحتية الأمنية.

## العواقب

- Core يصبح المصدر الوحيد للمنطق الخادمي؛ Domain ينضم إليه لا العكس.
- `Match.cs` الحالي (529 سطر) يُعاد تنظيمه إلى خدمات Core: `VitalityAccount`, `CollapseScheduler`, `TransferService`, `VoteService`, `SpiritService` مع الحفاظ على نفس الاختبارات (32 حالة).
- `CONFLICT_RESOLUTIONS.md` تُحدّث (بند 21).

## التحقق

1. `dotnet build -warnaserror` يمر (LangVersion 9.0).
2. `dotnet test` كل اختبارات Domain + Core تمر (≥32 + ≥38 Core).
3. `test_parity.py` يمر على 6 حالات (تآكل, Collapse, نقل, تصويت, Spirit, تزامن).
4. `tools/audit.py` 0 Issues.
5. `SOURCES_AND_REFERENCES.md` يحدّث EXT-*/SRC-*.

## الحالة

PROPOSED — يحتاج موافقة المالك قبل أي نقل كود. لا يرفع حالة ADR-001..005/ADR-004 تلقائياً.
