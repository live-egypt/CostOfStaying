# Network Architecture — v2.1
**الحالة: PROPOSED | المبدأ في ADR-002 (معتمد) | التنفيذ المنطقي في Domain Core (ADR-005)**

Dedicated Server يملك الحالة. العميل يرسل Intent فقط (`Intent.cs`). الخادم يحسم الصحة والضرر والذخيرة والمؤقتات والنقل والتصويت والنتيجة. Energy غير معتمدة في RULE-04؛ إن وُجدت لاحقاً يملكها الخادم أيضاً.

استخدم ساعة الخادم monotonic (`MonotonicClock`) وRequestId وrate limits وحدود حجم الرسائل. المنطق الخادمي مُختبر وحدوياً في `tests/CostOfStaying.Domain.Tests/SecurityTests.cs` (رفض مسافة مزعومة، منع replay).

Snapshot هنا يعني لقطة حالة شبكية في ذاكرة الخادم عند الانضمام أو إعادة الاتصال، مع Delta عند التغيير. ليس قاعدة بيانات دائمة. الحفظ الدائم يتبع [DATA_ARCHITECTURE.md](DATA_ARCHITECTURE.md) وADR-003. سياسة مهلة إعادة الاتصال ما زالت غير معتمدة ويجب حسمها قبل Alpha.

اختبر tick rates 10 و15 و20Hz. لا تثبّت 20Hz قبل قياس CPU وbandwidth وlatency. معدل الشبكة مستقل عن معدل الرسم وعن Unity physics timestep.

نقاط الاختبار المقترحة: RTT 100/200/300ms، loss 1/3/5%، jitter، وإعادة ترتيب الحزم. ليست مواصفات تشغيل معتمدة.

## GATE-3 — الحدود الموثوقة (Trust Boundaries)

### طبقات القبول (من الخارج إلى الداخل)

1. **Transport** (FishNet/NGO): يتحقق اتصال + حجم رسالة (<4KB) — يرفض قبل فك التسلسل.
2. **Session** (ADR-016): يربط `TransportConnection → ServerSession` — لا أمر بدون جلسة مصادق عليها (ADR-017).
3. **Match Roster** (ADR-018): لا أمر إلا إذا اللاعب في `Match.Players` و`Alive`.
4. **Authorization** (ADR-019): سياسة تخويل fail-closed — لا تصويت للميت، لا تحويل للـ Spirit.
5. **Payload** (ADR-020): `IntentValidator` — `SentAt ±5ث`, `RequestId` شكل, `distance` خادمي, idempotency.

كل طبقة ترفض افتراضياً (deny-by-default) وتسجل `DomainEvent` رفض. الفشل لا يغير الحالة (ذري).

### Snapshot وإعادة الاتصال

`Snapshot` = حالة `Match` في ذاكرة الخادم عند الانضمام/إعادة اتصال + Delta منذ آخر snapshot. لا كتابة دائمة. سياسة إعادة الاتصال: مهلة TBD (مقترح 60ث) ثم إبطال الجلسة — تحسم قبل Alpha (GATE-3.6).

### Tick

شبكة Tick 10Hz افتراضياً (قابل 15/20Hz بعد قياس). شبكة Tick مستقلة عن Render (60Hz) وPhysics. كل Tick: `drain Vitality → check death→Spirit → collapse → vote → end`.



## المراجع الموحدة
- المصادر: `governance/SOURCES_AND_REFERENCES.md` SRC-ADR-002 / EXT-FISHNET
- التنفيذ المنطقي: `architecture/DOMAIN_CORE.md` + `src/CostOfStaying.Domain/Match.cs`
