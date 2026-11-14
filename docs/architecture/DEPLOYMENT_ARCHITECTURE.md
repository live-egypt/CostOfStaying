# Deployment Architecture — v2.1
**الحالة: PROPOSED | CI النواة منفذ (ADR-005) | لا Unity/Android CI بعد**

البيئات التالية مستهدفة. وجود اسمها هنا لا يعني أنها تعمل:
- **Local:** تطوير محلي — ✅ النواة تبنى بـ `dotnet` عبر `global.json` (SDK 8.0.400).
- **CI:** بناء واختبار آلي — ✅ `ci.yml` نشط على `push/PR` (تدقيق + `dotnet test` + محاكاة + فحص أسرار).
- **Staging:** خادم Linux تجريبي وعميل Android تجريبي بعد وجود build — PROPOSED.
- **Production:** غير معتمد قبل بوابات الإصدار. Backend الإنتاج ينتظر ADR-003.

## وحدات النشر المستهدفة
1. Android Client موقّع بمفتاح خارج Git.
2. Linux Dedicated Server مستقل عن العميل.
3. Backend فقط بعد اعتماد مزود.

كل artifact مستقبلي يحمل commit SHA وإصدار Unity وFishNet وbuild number والهدف وhash. الحالي: `ci.yml` يرفع `TestResults/` و`playtest-logs/` كـ artifacts.

النشر المستهدف: Staging ثم smoke ثم مقارنة hash ثم توسيع تدريجي مع نسخة سابقة معروفة. الرجوع يجب أن يُجرّب قبل الاعتماد عليه. لا أسرار داخل العميل أو Git (`SECRETS_MANAGEMENT.md`).

## المراجع الموحدة
- المصادر: `governance/SOURCES_AND_REFERENCES.md`
- الهيكل: `REPOSITORY_STRUCTURE.md` + `.github/workflows/README.md`
