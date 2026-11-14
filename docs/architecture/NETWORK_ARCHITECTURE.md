# Network Architecture — v2.1
**الحالة: PROPOSED | المبدأ في ADR-002 (معتمد) | التنفيذ المنطقي في Domain Core (ADR-005)**

Dedicated Server يملك الحالة. العميل يرسل Intent فقط (`Intent.cs`). الخادم يحسم الصحة والضرر والذخيرة والمؤقتات والنقل والتصويت والنتيجة. Energy غير معتمدة في RULE-04؛ إن وُجدت لاحقاً يملكها الخادم أيضاً.

استخدم ساعة الخادم monotonic (`MonotonicClock`) وRequestId وrate limits وحدود حجم الرسائل. المنطق الخادمي مُختبر وحدوياً في `tests/CostOfStaying.Domain.Tests/SecurityTests.cs` (رفض مسافة مزعومة، منع replay).

Snapshot هنا يعني لقطة حالة شبكية في ذاكرة الخادم عند الانضمام أو إعادة الاتصال، مع Delta عند التغيير. ليس قاعدة بيانات دائمة. الحفظ الدائم يتبع [DATA_ARCHITECTURE.md](DATA_ARCHITECTURE.md) وADR-003. سياسة مهلة إعادة الاتصال ما زالت غير معتمدة ويجب حسمها قبل Alpha.

اختبر tick rates 10 و15 و20Hz. لا تثبّت 20Hz قبل قياس CPU وbandwidth وlatency. معدل الشبكة مستقل عن معدل الرسم وعن Unity physics timestep.

نقاط الاختبار المقترحة: RTT 100/200/300ms، loss 1/3/5%، jitter، وإعادة ترتيب الحزم. ليست مواصفات تشغيل معتمدة.

## المراجع الموحدة
- المصادر: `governance/SOURCES_AND_REFERENCES.md` SRC-ADR-002 / EXT-FISHNET
- التنفيذ المنطقي: `architecture/DOMAIN_CORE.md` + `src/CostOfStaying.Domain/Match.cs`
