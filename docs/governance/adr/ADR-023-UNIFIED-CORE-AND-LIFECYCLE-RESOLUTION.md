# ADR-023 — النواة الموحدة وحسم تعارض دورة الحياة
**الحالة: PROPOSED | التاريخ: 2026-11-14 | الفرع: `arena/4bfc0882-costofstaying` | يعتمد على ADR-005..021, ADR-007, RULE-01**

## السياق

المستودع وصل إلى **32 فرعاً** (gh branches) بخطين تنفيذيين لم يُوفّق بينهما:

- `main` (2c68f74→eb4100a) توثيق فقط — لا كود.
- **خط Core** (`feature/phase1-*` 16 ADR للبنية التحتية قبل Playtest) — يطبق البنية الصارمة لكنه خالف بوابة الميثاق الأولى ("Playtest أولاً").
- **خط Domain** (هذا الفرع) — يطبق RULE-01..05 كاملة + أدوات Playtest، لكنه بلا طبقة جلسات.

**تعارضان حرجان:**

1. **ADR-005 مكرر**: Core يستخدمه لـ `CORE-EXECUTABLE-BOUNDARY` (APPROVED 2026-11-01)، وDomain استخدمه لـ `CI-AND-DOMAIN-CORE` (PROPOSED). ثالث ADR بنفس الرقم كان سيُقترح لـ Betrayal.

2. **ADR-007 vs RULE-01**: ADR-007 (`PLAYER-LIFECYCLE-VITALITY-CONFLICT`) يسجل خلافاً: المالك وافق في 2026-10-31 على مصفوفة `Alive/Spirit/Eliminated` + انتقال تلقائي لصفر Vitality → Spirit + عمليتي Resurrect/Eliminate، بينما `02_GAME_RULES_SPECIFICATION.md` RULE-01 و`VitalityAccount` الحالي يطبقان `Alive→Dead` نهائي + Spirit كدور منفصل بلا Resurrect. ADR-007 يطلب اختياراً صريحاً — لم يُحسم.

الجذور: لا حجز أرقام ADR، فروع متراصة على قاعدة قديمة، بوابة Playtest مكتوبة لكن CI لا يفرضها، و`main` يصف نفسه فقط.

## القرار

### 1) النواة الوحيدة هي Core

- **الحد القابل للتنفيذ** (Executable Boundary) هو `Core` كما في ADR-005 (APPROVED). كل منطق خادمي جديد يمر عبر `Core` — `Net` و`Unity` طبقات خارجية فقط (ADR-005 §First slice).
- **Domain يُنقل إلى Core** (ADR-021): قواعد RULE-01..05 تُعاد تنظيمها داخل Core كـ `VitalityAccount`, `CollapseScheduler`, `TransferService`, `VoteService`, `SpiritService` مع الحفاظ على اختباراتها (32 حالة) وأدوات `balance_simulator`/`playtest`.
- **حجز الأرقام**: ADR-005..020 تبقى لخط Core (موافقة المالك موجودة). Domain أخذ ADR-021 (هذا التوحيد ADR-023). Betrayal أخذ ADR-022. أي ADR جديد يحجز رقماً في `DOCUMENT_GOVERNANCE.md` قبل الكتابة.

### 2) حسم تعارض دورة الحياة (ADR-007 + RULE-01)

- **المصفوفة المعتمدة**: `Alive`, `Spirit`, `Eliminated` (كما وافق المالك 2026-10-31) هي الحالات القانونية للاعب.
- **الانتقال عند صفر Vitality**: `Alive → Spirit` **تلقائي** (Vitality=0). `Spirit` ليس موتاً نهائياً — هو دور محايد كما في RULE-04.
- **Resurrect و Eliminate**: عمليتان خادميتان صريحتان **موجودتان في المصفوفة لكن معطلتان في MVP** (disabled). الميثاق §7 "لا عودة من Dead إلى Alive في MVP" يُعاد تفسيره كـ **قاعدة نطاق MVP** (Scope Rule) لا قاعدة دورة حياة (Lifecycle Rule). هذا يحل التناقض: المصفوفة تبقى كاملة، والقاعدة تمنع استخدام Resurrect في MVP فقط.
- **الانتقال**: يُحدّث `IVitalityAccount`, `PlayerState`, `02_GAME_RULES_SPECIFICATION.md`, `GLOSSARY.md`, و`TECHNICAL_ARCHITECTURE.md` بعد موافقة هذا ADR — لا يُعدّل الكود قبل الموافقة (ADR-007 §Required acceptance criteria).

### 3) إصلاح الأرقام المتكررة

- `ADR-005-CI-AND-DOMAIN-CORE.md` (هذا الفرع) → **مُستبدل** (SUPERSEDED) بـ ADR-021. يبقى الملف للتاريخ مع ترويسة `SUPERSEDED`.
- `ADR-005-CORE-EXECUTABLE-BOUNDARY.md` (Core) هو الـ ADR-005 الشرعي الوحيد.
- أي اقتراح ثالث لـ ADR-005 (Betrayal) مرفوض — أخذ ADR-022 بدلاً.

## البدائل المرفوضة

- **Domain كأساس**: يفقد صرامة Core (WarnAsError, NetArchTest) ويؤخر الأمان.
- **اعتماد RULE-01 كمصفوفة نهائية بدون Spirit/Eliminated**: يتجاهل موافقة المالك 2026-10-31 المسجلة في ADR-007.
- **تفعيل Resurrect في MVP**: يحوّل اللعبة من بقاء نفسي إلى قتال إحياء — يخالف رؤية الميثاق.

## العواقب

- **إيجابية**: نواة واحدة صارمة + قواعد قابلة للاختبار + أدوات Playtest + أرقام محجوزة + CI يفرض البوابة.
- **تكلفة**: يتطلب نقل 529 سطر `Match.cs` إلى خدمات Core + إعدة اختبارات Core (≥38) + توثيق مصفوفة انتقالات واحدة.
- **مخاطر**: إن لم يُحسم ADR-007 هنا, يبقى التعارض ويمنع دمج أي نواة.

## خطوات التنفيذ (الترتيب الملزم)

1. **التحقق** (قبل القرار): ادفع هذا الفرع, شغّل `gh pr list --state all`, جرّب `git merge-tree` للتعارضات.
2. **القرارات** (هذا ADR): سجّل الموافقة/التعديل في ADR-021/022/023, حدّث `DOCUMENT_GOVERNANCE.md` بحجز الأرقام.
3. **الدمج**: ادمج سلسلة PRs لـ Core بالترتيب (الأقدم أولاً, كل فرع مبني على سابقه).
4. **بوابة Playtest**: 10 جلسات صالحة + تحليل بتروس + حسم أسئلة Betrayal في ADR-022.
5. **نقل القواعد إلى Core** (ADR-021): كل قاعدة مع اختباراتها.
6. **Unity**: ثبّت الإصدار واستورد Core, جرّب FishNet على جهازين (ADR-001 prototype).
7. **الأمن والأداء**: تزوير/replay/spam + baseline Android.

## التحقق

- هذا ADR يحمل حالة PROPOSED حتى توقيع المالك.
- بعده: تحدّث مصفوفة دورة الحياة + `02_GAME_RULES_SPECIFICATION.md` + `ADR-007` إلى APPROVED (أحد الخيارين) + CI أخضر.
- لا حالة VERIFIED بدون commit SHA + تشغيل CI أخضر + hash artifact.

## الحالة

PROPOSED — يحتاج توقيعك الصريح. لا يرفع حالة ADR-001..004/ADR-007 تلقائياً.
