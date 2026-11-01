# تتبع المتطلبات — v2.1
**الحالة: PARTIALLY VERIFIED | ADR-007 approved; lifecycle implementation CI verification pending on this branch**

الغرض هو تتبع العلاقة من المتطلب إلى الضابط والاختبار والدليل. اجتاز Core CI السابق 38 اختباراً، لكنه سبق تعديل دورة الحياة. ADR-007 حُسم لصالح Alive / Spirit / Eliminated؛ يلزم تشغيل CI على commit التعديل قبل إعلان التحقق. لا يثبت Core CI تكامل Unity/FishNet.

| Requirement ID | المتطلب | Control ID | Test ID المقترح | الدليل المطلوب |
|---|---|---|---|---|
| GAME-001 | انتقال Vitality عند الصفر إلى Spirit وفق ADR-007 | GAME-CTRL-001 | VitalityAccountTests: Lose_ReachingZeroAutomaticallyTransitionsToSpirit | تشغيل Core CI على commit lifecycle الجديد؛ لا يُعد run السابق دليلاً لهذا التعديل |
| COL-001 | مؤقت Collapse وإعادة الجدولة | COL-CTRL-001 | UNIT-COL-001 | تقرير Unit Test |
| TRF-001 | نقل Debuff ذرياً دون Copy | TRF-CTRL-001 | INT-TRF-001 | تقرير Integration |
| BET-001 | احتساب Betrayal بعد نجاح النقل فقط | BET-CTRL-001 | UNIT-BET-001 | تقرير Unit Test |
| SP-001 | الروح لا تغير حالة الأحياء مباشرة | SP-CTRL-001 | INT-SP-001 | تقرير Integration |
| END-001 | التصويت والنتيجة يقررهما الخادم | END-CTRL-001 | NET-END-001 | سجل مباراة واختبار شبكة |
| NET-001 | العميل يرسل Intent فقط | TRUST-CTRL-001 | SEC-NET-001 | Security regression report |
| CORE-ID-001 | معرفات اللاعب والهدف غير صفرية عند دخول Domain Core | CORE-CTRL-001 | IdentifierTests / VitalityAccountTests | CI run 41255553487؛ 38 NUnit passed; latest workflow run 41255769742 passed |
| BET-002 | يمنع self-target وdefault BetrayalEvent قبل mutation | BET-CTRL-002 | BetrayalLedgerTests | CI run 41255553487؛ latest workflow run 41255769742 passed |
| BET-003 | ترتيب ledger يعتمد EventSequenceId لا ServerTick | BET-CTRL-003 | BetrayalLedgerTests | CI run 41255553487؛ latest workflow run 41255769742 passed |
| PERF-001 | أداء Android ضمن ميزانية معتمدة | PERF-CTRL-001 | PERF-ANDROID-001 | تقرير جهاز فعلي |

## قواعد التتبع
- لكل متطلب مالك وأولوية ومعيار قبول قبل التنفيذ.
- لكل اختبار خطوات ونتيجة متوقعة ونتيجة فعلية وبيئة وcommit SHA.
- الدليل يشير إلى artifact أو تقرير محفوظ يمكن مراجعته.
- الحالة VERIFIED تتطلب اجتيازاً فعلياً؛ لا تمنح بناءً على اكتمال الوثيقة.
- حدّث الجدول مع كل تغيير في المواصفات أو الاختبارات.


## تحديث ADR-007 — 2026-11-01

تم اعتماد دورة حياة اللاعب وفق القرار السابق الموثق:
- Alive + Vitality > 0 → Alive.
- Alive + Vitality reaches 0 → Spirit automatically.
- Spirit → Alive فقط عبر Resurrect صريح.
- Alive أو Spirit → Eliminated فقط عبر Eliminate صريح.
- Eliminated حالة نهائية.
- Restore لا يحيي Spirit ولا يعيد لاعباً Eliminated.

اختبارات القبول المضافة تغطي الانتقالات والرفض والحالات النهائية. حالة هذه الاختبارات: IMPLEMENTED؛ تصبح VERIFIED فقط بعد نجاح CI على commit الذي يحتوي التغيير.
