# تتبع المتطلبات — v2.1
**الحالة: PARTIALLY VERIFIED | Core CI passed; gameplay lifecycle decision remains blocked by ADR-007**

الغرض هو تتبع العلاقة من المتطلب إلى الضابط والاختبار والدليل. اجتاز Core build و38 اختبار NUnit في run 41255553487، كما نجح workflow لاحق 41255769742. هذا يثبت سلوك الكود الحالي فقط؛ لا يحسم تعارض دورة حياة اللاعب ولا يثبت تكامل Unity/FishNet.

| Requirement ID | المتطلب | Control ID | Test ID المقترح | الدليل المطلوب |
|---|---|---|---|---|
| GAME-001 | انتقال Vitality عند الصفر — متطلب متعارض | GAME-CTRL-001 | UNIT-VIT-001 (السلوك الحالي فقط) | CI يثبت الكود الحالي؛ اعتماد السلوك النهائي محظور حتى حسم ADR-007 |
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
