# Deployment Architecture — v2.1
**الحالة: PROPOSED | لا بيئة منشأة ولا CI منفذ**

البيئات التالية مستهدفة. وجود اسمها هنا لا يعني أنها تعمل:
- Local: تطوير محلي عند وجود مشروع.
- CI: بناء واختبار آلي مستقبلي بلا أسرار إنتاج. قوالب `.github/workflows` ليست هذا الـ CI.
- Staging: خادم Linux تجريبي وعميل Android تجريبي بعد وجود build.
- Production: غير معتمد قبل بوابات الإصدار. Backend الإنتاج ينتظر ADR-003.

## وحدات النشر المستهدفة
1. Android Client موقّع بمفتاح خارج Git.
2. Linux Dedicated Server مستقل عن العميل.
3. Backend فقط بعد اعتماد مزود.

كل artifact مستقبلي يحمل commit SHA وإصدار Unity وFishNet وbuild number والهدف وhash.

النشر المستهدف: Staging ثم smoke ثم مقارنة hash ثم توسيع تدريجي مع نسخة سابقة معروفة. الرجوع يجب أن يُجرّب قبل الاعتماد عليه. لا أسرار داخل العميل أو Git.
