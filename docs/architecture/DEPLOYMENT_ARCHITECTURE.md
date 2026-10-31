# Deployment Architecture — v2.0
**الحالة: PROPOSED**

## البيئات
- Local: تطوير واختبارات محلية.
- CI: بناء واختبار آلي دون أسرار إنتاج.
- Staging: خادم Linux تجريبي وعميل Android تجريبي.
- Production: غير معتمد قبل اجتياز بوابات الإصدار.

## وحدات النشر
1. Android Client: ملف APK/AAB موقع بمفتاح إصدار محفوظ خارج Git.
2. Linux Dedicated Server: بناء مستقل عن العميل، مع إعدادات بيئية خارج الحزمة.
3. Backend: يضاف بعد اعتماد مزود وخدمات محددة.

## إصدار الخادم
كل artifact يحمل commit SHA، إصدار Unity، إصدار FishNet، build number، target architecture، config schema version وSHA-256 hash.

## التدرج والرجوع
انشر أولاً إلى Staging؛ نفذ smoke test؛ قارن hash؛ راقب crash rate وserver tick؛ ثم وسع النشر تدريجياً. احتفظ بآخر نسخة سليمة معروفة. يجب أن تكون rollback procedure مجربة وليست مجرد تعليمات.

## الأسرار
لا تضع credentials أو signing keys داخل العميل أو مستودع Git. استخدم أقل صلاحية وافصل البيئات. لا تعتمد build إنتاجياً إذا تسرب سر أو تعذر التحقق من مصدر artifact.
