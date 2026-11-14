# Monitoring and Logging — v2.1
**الحالة: PROPOSED | لا نظام مراقبة منفذ ولا عتبات تنبيه | CI يرفع artifacts كبداية**

مقاييس مستهدفة بعد وجود خادم: عدد الجلسات، إكمال المباراة، disconnect rate، أسباب رفض الأوامر، وأخطاء الخادم.

مقاييس الأداء: CPU/RAM وtick duration وbandwidth وcrash rate. لا عتبات قبل baseline. استخدم MatchId عشوائياً. لا تسجل tokens أو أسراراً أو بيانات شخصية غير لازمة.

**الحالي:** CI يرفع `TestResults/` و`playtest-logs/` (جلسات محاكاة) كـ artifacts — بداية قابلة للمراجعة لكنها ليست مراقبة تشغيلية. انظر `DEPLOYMENT_ARCHITECTURE.md`.

## المراجع الموحدة
- `governance/SOURCES_AND_REFERENCES.md`
- `SECURITY_ARCHITECTURE.md` (حماية السجلات)
