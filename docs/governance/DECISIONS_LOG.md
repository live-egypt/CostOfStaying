# سجل القرارات — v2.1
**الحالة: PROPOSED | آخر مراجعة: 2026-11-14 | فهرس لا يرفع حالات**

هذا الفهرس يعكس ملفات ADR الفعلية. الحالة هنا لا تتجاوز حالة الملف. التنظيم في [DOCUMENT_GOVERNANCE.md](DOCUMENT_GOVERNANCE.md). حسم التعارضات التحريرية في [CONFLICT_RESOLUTIONS.md](CONFLICT_RESOLUTIONS.md).

| ADR | الموضوع | الحالة | النطاق |
|---|---|---|---|
| ADR-001 | اختيار التكدس التقني: Unity/URP/C#/FishNet/Linux | PROPOSED | يحتاج prototype قبل الاعتماد |
| ADR-002 | سلطة الخادم ومصدر حقيقة المباراة | APPROVED كمبدأ | التنفيذ منطقي في Domain (NOT IMPLEMENTED شبكياً) |
| ADR-003 | Backend واستضافة الخوادم | PROPOSED | لا مزود مختار |
| ADR-004 | خط أساس قواعد اللعب الخمس | PROPOSED FOR PLAYTEST | 10 جلسات قبل الاعتماد، لا بعده |
| ADR-005 | النواة القابلة للتنفيذ — الحد التنفيذي الوحيد (Core) | APPROVED — موافقة 2026-11-01 (Core) | `src/CostOfStaying.Core` — لا Unity/لا FishNet |
| ADR-006 | معرفات Core وثوابت سجل Betrayal | PROPOSED — Core | ulong IDs, sequence invariants |
| ADR-007 | دورة حياة اللاعب — تضارب Vitality | DECISION REQUIRED (PROPOSED) — Core | Alive/Spirit/Eliminated + Resurrect/Eliminate |
| ADR-008 | مغلف أوامر الخادم وقبول التسلسل | PROPOSED — Core | CommandEnvelope + Sequence |
| ADR-009 | سجل لاعبي النطاق المطابقي | PROPOSED — Core | Match-scoped registry |
| ADR-010 | دورة حياة جلسة الخادم | PROPOSED — Core | Server Session Lifecycle |
| ADR-011 | سجل جلسات محدود | PROPOSED — Core | Bounded Session Registry |
| ADR-012 | قبول أوامر موثقة | PROPOSED — Core | Authenticated Command Admission |
| ADR-013 | تنظيف جلسات آمن | PROPOSED — Core | Safe Session Cleanup |
| ADR-014 | نطاق الضحية | PROPOSED — Core | Victim Scope |
| ADR-015 | هوية الأوامر وidempotency | PROPOSED — Core | Command Identity |
| ADR-016 | ربط اتصال النقل بالجلسة | PROPOSED — Core | Transport Binding |
| ADR-017 | تحقق الاعتماد وربط Actor | PROPOSED — Core | Credential Binding |
| ADR-018 | قبول أوامر عضوية المباراة | PROPOSED — Core | Match Roster Admission |
| ADR-019 | سياسة تخويل أوامر المباراة | PROPOSED — Core | Authorization Policy |
| ADR-020 | التحقق من حمولة الأوامر المكتوبة | PROPOSED — Core | Typed Payload Validation |
| ADR-021 | نقل قواعد Domain إلى النواة الموحدة | PROPOSED — Domain→Core (يحل محل ADR-005 المكرر) | RULE-01..05 + tools |
| ADR-022 | عتبات Betrayal — تكلفة وليست فوزاً | PROPOSED | تكلفة فقط, عتبات 3/5/7 فرضيات |
| ADR-023 | النواة الموحدة وحسم دورة الحياة | PROPOSED — قرار التوحيد | Core=canonical, Resurrect معطل MVP |

> **ملاحظة الحجز:** ADR-005 المكرر في هذا الفرع (`ADR-005-CI-AND-DOMAIN-CORE.md`) **SUPERSEDED بـ ADR-021** — يبقى للتاريخ فقط. ADR-005 الشرعي هو `ADR-005-CORE-EXECUTABLE-BOUNDARY.md` (Core). الفهرس هنا يسرد Core كـ 005..020 للإشارة, لكن ملفاتها الفعلية على فروع `feature/phase1-*` ولم تُدمج في `main` بعد — انظر `AUDIT_REPORT_2026-11-14.md` §2.

## قرارات نطاق ومبادئ
- MVP لأربعة لاعبين وخريطة واحدة يبقى نطاق الميثاق حتى قرار موثق.
- النواة الموحدة هي Core (ADR-023) — Domain يُنقل إليها (ADR-021) — فصل Core عن Unity/FishNet منفذ كـ APPROVED (Core) وPROPOSED (Domain).
- Resurrect/Eliminate موجودان في مصفوفة دورة الحياة لكن معطلان في MVP (ADR-023) — "لا عودة" قاعدة نطاق MVP.
- لا تحسين بلا قياس.
- لا اعتماد لمزود Backend أو ميزانية أداء أو حد أدنى للأجهزة.
- مراجعة مخاطر التوازن مدخل اختبار لا قرار.
- ترتيب التسليم هو بوابات الميثاق، لا القائمة التي كانت تؤجل Playtest.

## قواعد الحوكمة
- لا تغيير حالة ADR دون تحديث ملفه وهذا السجل.
- لا تستخدم APPROVED لوصف كود لم ينفذ.
- لا تستخدم IMPLEMENTED أو VERIFIED دون commit واختبار ودليل.
- عند التعارض: ADR، ثم مواصفة القواعد، ثم وثيقة النظام.
- لا تضف وثيقة دون إدراجها في الكتالوج والفهرس.
- **حجز الأرقام قبل الكتابة**: ADR-005..020 محجوزة لـ Core — لا تُستخدم مكررة (CONFLICT #21).
