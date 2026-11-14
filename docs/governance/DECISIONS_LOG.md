# سجل القرارات — v2.1
**آخر مراجعة: 2026-11-14**

هذا الفهرس يعكس ملفات ADR الفعلية. الحالة في هذا الجدول لا تتجاوز حالة ADR المرتبط.

| ADR | الموضوع | الحالة | النطاق |
|---|---|---|---|
| ADR-001 | اختيار التكدس التقني: Unity/URP/C#/FishNet/Linux | PROPOSED | خيار تقني يحتاج prototype |
| ADR-002 | سلطة الخادم ومصدر حقيقة المباراة | APPROVED كمبدأ | التنفيذ NOT IMPLEMENTED |
| ADR-003 | Backend واستضافة الخوادم | PROPOSED | يؤجل الحسم حتى prototype |
| ADR-004 | خط أساس قواعد اللعب الخمس | PROPOSED FOR PLAYTEST | يتطلب 10 جلسات موثقة على الأقل |
| ADR-005 | حدود Core التنفيذي وشريحة التنفيذ الأولى | APPROVED | اعتماد مالك المشروع بتاريخ 2026-11-01 |
| ADR-006 | ثوابت معرفات Core وسجل Betrayal | PROPOSED | يتطلب اعتماد المالك قبل الدمج |
| ADR-007 | دورة حياة اللاعب وVitality | APPROVED | تأكيد القرار السابق: Alive / Spirit / Eliminated؛ الصفر ينقل إلى Spirit؛ Resurrect وEliminate عمليتان صريحتان وخادميتان؛ Eliminated نهائية |
| ADR-008 | Server Command Envelope وSequence Admission | PROPOSED | عقد خادمي للهوية/التوقيت/التسلسل ومنع replay؛ قيم الحدود وسياسة reconnect ما زالت TBD |
| ADR-009 | Match-Scoped Player Registry | PROPOSED | سجل عضوية bounded لكل MatchId؛ سعة اللاعبين وسياسة spectator/reconnect ما زالت TBD |
| ADR-010 | Server Session Lifecycle | PROPOSED | Pending/Authenticated/Disconnected/Closed؛ reconnect/timeout/multi-session policy ما زالت TBD |
| ADR-011 | Bounded Server Session Registry | PROPOSED | سعة صريحة؛ تسجيل Pending فقط؛ الإزالة بعد Closed وبعد تفريغ أوامر الجلسة؛ سياسة تعدد الجلسات لكل لاعب تبقى TBD |
| ADR-012 | Authenticated Command Admission Gate | PROPOSED | قبول الأمر من Session مسجلة وAuthenticated ومرتبطة بـ ActorId صالح؛ إعادة التحقق عند dequeue |
| ADR-013 | Safe Session Cleanup and Replay-State Release | PROPOSED | خدمة واحدة تنظّم إغلاق الجلسة وتفريغ الطابور وإعادة ضبط replay ثم إزالة التسجيل |
| ADR-014 | Victim Scope | PROPOSED | تأجيل حسم فئات الضحايا؛ الإبقاء على VictimId دون تغيير؛ دعم NPCs غير معتمد؛ فئات الضحايا TBD. |
| ADR-015 | Command Identity and Idempotency | PROPOSED | CommandId مستقل مرتبط بهوية اللاعب الموثوقة؛ سجل In-Memory مستقل عن Session Cleanup ويستمر عبر reconnect فقط؛ InProgress/النتيجة السابقة؛ بصمة الأمر وذرية الحجز مطلوبتان؛ التخزين الدائم ومدة الاحتفاظ TBD؛ المخاطر متوسطة. |
| ADR-016 | Transport Connection to Session Binding | PROPOSED | ربط one-to-one مؤقت بين TransportConnectionId خادمي وSessionId مسجلة؛ فصل/تحديث دورة حياة الجلسة عند الانقطاع وحماية cleanup؛ لا يقرر عدد الجلسات لكل ActorId ولا ينفذ FishNet أو credential validation. |
| ADR-017 | Credential Validation and Actor Binding Boundary | PROPOSED | فصل التحقق من بيانات اعتماد الحساب خارج Core عن تعيين ActorId خادمي وربطه بجلسة Pending؛ لا يختار مزود الهوية أو نوع الاعتماد ولا يقرر guest/multi-session/reconnect policy. |
| ADR-018 | Match Roster Command Admission | PROPOSED | لا تقبل المباراة أو تسلّم أوامر إلى المعالج إلا إذا كان ActorId المحلول من جلسة Authenticated عضواً في سجل لاعبي تلك المباراة؛ يعاد فحص العضوية عند dequeue؛ لا يحدد صلاحيات الحالات/الأطوار أو تعدد العضوية بين المباريات. |
| ADR-019 | Match Command Authorization Policy Boundary | PROPOSED | بوابة تفويض عامة تتلقى سياسة تطبيق خادمية صريحة بلا سماح افتراضي؛ تفحص MatchPhase وحالة اللاعب قبل queue admission وقبل handoff؛ لا تعتمد أي مصفوفة صلاحيات لعب محددة ولا ترقي قواعد playtest إلى موافقة. |

## قرارات نطاق ومبادئ
- MVP لأربعة لاعبين وخريطة واحدة: نطاق المنتج المستهدف في ميثاق المشروع، ويعاد تقييمه عبر قرار موثق إذا تغير.
- فصل Domain Core عن Unity/FishNet: مبدأ معماري مستهدف، وليس بنية منفذة بعد.
- لا تحسين بلا قياس: مبدأ هندسي.
- لا يوجد اعتماد نهائي لمزود Backend أو استضافة إنتاج أو ميزانيات أداء.
- لا تعتمد أي نسخة كود من سجل محادثات Phase 1 قبل مطابقتها مع ADRs ومواصفات المجال.
- عند تعارض v2.1 مع ADR-007، تكون دورة Alive / Spirit / Eliminated هي المرجع؛ يجب تحديث مواصفة v2.1 ولا يجوز إعادة إدخال حالة Dead كمنافس.

## قواعد الحوكمة
- لا تغير حالة ADR دون تحديث ملفه وسجل القرارات.
- لا تستخدم APPROVED لوصف كود لم ينفذ.
- لا تستخدم IMPLEMENTED أو VERIFIED دون commit واختبار ودليل.
- عند التعارض، راجع ADR المعني ومواصفة المجال؛ لا تستنتج قراراً من رقم ADR أو من نص قديم.
