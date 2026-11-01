# تتبع المتطلبات — v2.1
**الحالة: PARTIALLY VERIFIED | Lifecycle, results, identifiers, match lifecycle, composition, command/time primitives and bounded FIFO queue verified; ADR-008 policy remains proposed**

الغرض هو تتبع العلاقة من المتطلب إلى الضابط والاختبار والدليل. The latest Core CI run 41327543734 passed the dependency-boundary scan, CoreComposition construction-root scan, Release build and 103 NUnit tests. This verifies standalone Core only and does not verify Unity/FishNet.

| Requirement ID | المتطلب | Control ID | Test ID المقترح | الدليل المطلوب |
|---|---|---|---|---|
| GAME-001 | انتقال Vitality عند الصفر إلى Spirit وفق ADR-007 | GAME-CTRL-001 | VitalityAccountTests: Lose_ReachingZeroAutomaticallyTransitionsToSpirit | Core CI run 41329893768; 119 NUnit passed |
| COL-001 | مؤقت Collapse وإعادة الجدولة | COL-CTRL-001 | UNIT-COL-001 | تقرير Unit Test |
| TRF-001 | نقل Debuff ذرياً دون Copy | TRF-CTRL-001 | INT-TRF-001 | تقرير Integration |
| BET-001 | احتساب Betrayal بعد نجاح النقل فقط | BET-CTRL-001 | UNIT-BET-001 | تقرير Unit Test |
| SP-001 | الروح لا تغير حالة الأحياء مباشرة | SP-CTRL-001 | INT-SP-001 | تقرير Integration |
| END-001 | التصويت والنتيجة يقررهما الخادم | END-CTRL-001 | NET-END-001 | سجل مباراة واختبار شبكة |
| NET-001 | العميل يرسل Intent فقط | TRUST-CTRL-001 | SEC-NET-001 | Security regression report |
| CORE-ID-001 | معرفات اللاعب والهدف غير صفرية عند دخول Domain Core | CORE-CTRL-001 | IdentifierTests / VitalityAccountTests | CI run 41327543734; 103 NUnit passed |
| BET-002 | يمنع self-target وdefault BetrayalEvent قبل mutation | BET-CTRL-002 | BetrayalLedgerTests | CI run 41326831611; 77 NUnit passed |
| BET-003 | ترتيب ledger يعتمد EventSequenceId لا ServerTick | BET-CTRL-003 | BetrayalLedgerTests | CI run 41327543734; 103 NUnit passed |
| CORE-TIME-001 | ServerTick zero is valid; StateRevision advances without overflow | CORE-TIME-CTRL-001 | TimeValueTests | Core CI run 41327543734; 103 NUnit passed |
| CORE-CMD-001 | CommandEnvelope uses server-resolved SessionId and server receive tick | CORE-CMD-CTRL-001 | CommandEnvelopeTests | Core CI run 41327543734; 103 NUnit passed |
| CORE-SEQ-001 | Per-session command sequence is monotonic and bounded by configured maximum jump | CORE-SEQ-CTRL-001 | CommandSequenceTrackerTests | Core CI run for command/time increment pending |
| CORE-QUEUE-001 | Command admission uses bounded FIFO; full queue rejects without consuming sequence; session replay state resets only after queued commands drain | CORE-QUEUE-CTRL-001 | CommandAdmissionQueueTests | Core CI run 41329893768; 119 NUnit passed |
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


## ADR-008 — Command envelope and sequence admission

ADR-008 is PROPOSED. The implementation and tests exist, but the policy is not yet owner-approved. Open policy items include production MaximumSequenceJump, maximum tracked sessions, queue capacity, reconnect/session continuity, rate limiting and command timeout. Queue-full behavior and safe reset semantics passed Core CI run 41329893768. The current implementation consumes an accepted sequence before later gameplay-rule validation; this is explicitly recorded in ADR-008 and awaits review.
