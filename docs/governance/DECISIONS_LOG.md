# سجل القرارات — v2.1
**آخر مراجعة: 2026-11-01**

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
