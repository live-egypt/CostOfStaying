# حل التعارضات — v2.1
**الحالة: PROPOSED | التاريخ: 2026-11-14**

سجل تحريري. لا يرفع حالة أي ADR ولا يعتمد أرقام توازن.

| # | التعارض | الحسم |
|---|---|---|
| 1 | خطة التسليم أخّرت Playtest | الترتيب هو بوابات الميثاق |
| 2 | المسرد استخدم REFERENCE و«معتمد» | الحالة PROPOSED |
| 3 | البيانات قالت إن النتيجة غير معرّفة | RULE-05 يعرّفها؛ الحفظ ينتظر ADR-003 |
| 4 | PlayFab قُرئ كاختيار | ADR-003 هو القرار |
| 5 | جدول فترات Collapse | 15 ثانية لثلاثة أحياء فقط هي المسماة |
| 6 | عتبات Betrayal | ليست قواعد ولا اختبارات قبول |
| 7 | أرقام Combat | ليست جزءاً من RULE-01..05 |
| 8 | 30FPS وRedmi Note 8 كأهداف v1 | أرقام موروثة غير معتمدة |
| 9 | رؤوس v2.0 | الملفات المراجَعة أصبحت v2.1 دون رفع الحالة |
| 10 | workflows على الدفع إلى main | يدوية فقط وليست CI |
| 11 | النتائج قُرئت 3 أو 4 وسقط Draw | RULE-05 يميز خمساً: Last Survivor وCollective Surrender وTime Limit وDraw وMutual Collapse |
| 12 | Energy ظهر كقرار خادمي معتمد | Energy ما زال TBD. الخادم يملكه فقط إن اعتُمد لاحقاً |
| 13 | Snapshot الشبكة قُرئ كحفظ دائم | لقطة ذاكرة للانضمام فقط. الحفظ الدائم ينتظر ADR-003 |
| 14 | بيئة CI ونموذج التهديد كأن الخط موجود | CI مستهدف وغير منفذ. القوالب اليدوية ليست خط بناء |
| 15 | PERF-001 افترض ميزانية معتمدة | لا ميزانية حتى يوجد baseline وقرار |
| 16 | Spectating قُرئ كبديل فوري لـ Spirit | الموت ينقل إلى Spirit أولاً. موعد Spectating غير محدد |
| 17 | workflows قُرئت كلها يدوية | `ci.yml` نشط على push/PR (ADR-005/021)؛ بقية القوالب تبقى يدوية فقط |
| 18 | Domain Core قُرئ كمنفذ وموثق تلقائياً | النواة في `src/CostOfStaying.Domain` بحالة PROPOSED بانتظار CI أخضر؛ لا VERIFIED |
| 19 | مصادر متفرقة بلا مرجع موحد | `SOURCES_AND_REFERENCES.md` هو مصدر الحقيقة للاستشهادات والإصدارات |
| 20 | هيكل المستودع بلا وثيقة مرجعية | `REPOSITORY_STRUCTURE.md` يوثق البنية وقواعد الإضافة |
| 21 | رقم ADR-005 مستخدم مرتين (Core vs Domain) | ADR-005 الشرعي هو `ADR-005-CORE-EXECUTABLE-BOUNDARY.md` (APPROVED)؛ `ADR-005-CI-AND-DOMAIN-CORE` مُستبدل بـ ADR-021 |
| 22 | خطان تنفيذيان Core وDomain لم يُوفّق بينهما | النواة الموحدة هي Core (ADR-023) — Domain يُنقل إليها عبر ADR-021 |
| 23 | ADR-007 يسمح Resurrect وRULE-01 تمنع العودة | المصفوفة Alive/Spirit/Eliminated معتمدة، Resurrect/Eliminate معطلان في MVP (ADR-023) — "لا عودة" قاعدة نطاق MVP لا دورة حياة |
| 24 | ADR-021 مكرر (Core candidate vs Domain) | ADR-021 الحالى هو نقل Domain (هذا الفرع) — مرشح Core السابق يُرقم ADR-024 في التوحيد |
| 25 | LangVersion 10 لا يعمل على Unity 6 | Core وDomain على LangVersion 9.0 — CI يحرس (ADR-023) |

لم تُرفع حالة ADR-001 أو ADR-002 أو ADR-003 أو ADR-004. ADR-005 الأصلى APPROVED (Core). ADR-021..023 جديدة PROPOSED بانتظار توقيعك.
