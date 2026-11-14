# ADR-005 — تفعيل CI وتنفيذ Domain Core
**الحالة: PROPOSED | التاريخ: 2026-11-14 | الفرع: `arena/4bfc0882-costofstaying`**

## السياق
حتى 2026-11-11 كان المستودع حزمة توثيق فقط بلا كود ولا CI منفذ؛ ملفات `.github/workflows` قوالب موردين متوقفة على `workflow_dispatch`. مبادئ العمل السبعة تمنع ادعاء IMPLEMENTED/VERIFIED بلا اختبار، وتتطلب سلطة الخادم وعدم ادعاء الأداء بلا قياس. خارطة التسليم تطلب بوابة 3: بناء Domain Core منفصل عن Unity واختباره.

## القرار المقترح
1. **Domain Core مجرد:** إنشاء `src/CostOfStaying.Domain` (`netstandard2.1`) يطبق RULE-01..05 كما في `02_GAME_RULES_SPECIFICATION.md` دون Unity/FishNet، مع `IMatchClock` وساعة monotonic.
2. **اختبارات:** `tests/CostOfStaying.Domain.Tests` (NUnit) تغطي Vitality/Collapse/Transfer/Vote/Endgame/Spirit/Security.
3. **أدوات:** `tools/balance_simulator` و`tools/playtest` لمحاكاة التوازن وتوليد ≥10 جلسات موثقة مطلوبة لبوابة ADR-004، مع `test_parity.py` لضمان تطابق Python مع C#.
4. **CI حقيقي:** `.github/workflows/ci.yml` يعمل عند `push`/`pull_request` ويشغل: `tools/audit.py` → `dotnet restore/build/test` → parity → smoke simulators → فحص أسرار heuristic → رفع artifacts. يبقى ملفات القوالب القديمة على `workflow_dispatch` فقط.
5. **التوثيق:** إضافة `docs/architecture/DOMAIN_CORE.md` وتحديث الكتالوج والفهرس وسجل القرارات في نفس الالتزام.

## البدائل
- إبقاء القوالب معطلة بلا CI: يمنع التحقق المبكر ويؤخر كشف التعارض.
- تفعيل CI على قوالب الموردين مباشرة: يتطلب أسراراً خارج Git ودليل أداة مسمى — غير متوفر (انظر `SECRETS_MANAGEMENT.md`).

## المبررات
فصل النواة يسمح باختبار منطق اللعب وحواجز الأمان قبل بناء Unity؛ CI يطبق principle "لا VERIFIED بلا دليل" ويوفر SHA وartifact hash لكل بند في `ACCEPTANCE_CRITERIA.md`.

## المخاطر
- تنفيذ أولي قد يحمل افتراضات توازن؛ جميع القيم خارج RULE تبقى PROPOSED TEST VALUE.
- لا Dedicated Server فعلي بعد؛ Network replication مؤجلة إلى بوابة 4.

## التحقق
- CI أخضر على GitHub Actions (`ci.yml`) مع artifacts: `TestResults/`, `sim.json`, `playtest-logs/`.
- `dotnet test` يمر، `tools/audit.py` بلا Issues، جلسات Playtest ≥10 موثقة.

## الحالة
PROPOSED — يحتاج مراجعة وCI أخضر قبل APPROVED. لا يرفع حالة ADR-001/002/004 تلقائياً.
