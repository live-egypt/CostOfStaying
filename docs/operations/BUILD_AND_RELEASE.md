# Build and Release — v2.1
**الحالة: PROPOSED | لا يوجد CI لعبة منفذ**

كل build مستقبلي يحمل version وcommit SHA وbuild number وإصدار Unity وقفل الحزم والهدف وhash للـ artifact.

المسار المستهدف بعد وجود كود: مراجعة وADR → اختبارات → فحص أسرار واعتماديات → بناء Android وLinux → smoke على staging → التحقق من التوقيع والـ hash → نشر متدرج → مراقبة ورجوع.

مفاتيح التوقيع خارج المستودع. افصل debug عن release. لا تضع server credentials داخل العميل.

## قوالب GitHub Actions
الملفات في `.github/workflows` قوالب بائعين لأدوات لا يملكها المشروع بعد. تشغّل يدوياً فقط عبر `workflow_dispatch`. لا تعمل على الدفع إلى main، ولا تعد بوابة جودة، ولا تثبت فحصاً ناجحاً. لا تُفعّل على الدفع قبل وجود artifact حقيقي وأسرار مُعدّة وقرار يذكر الأداة.
