# تتبع المتطلبات — v2.1
**الحالة: PARTIALLY VERIFIED | Lifecycle, results, identifiers, match/session lifecycle primitives, composition, command/time primitives, bounded FIFO queue and match player registry verified; ADR-008/ADR-009/ADR-010 policies remain proposed**

الغرض هو تتبع العلاقة من المتطلب إلى الضابط والاختبار والدليل. The latest Core CI run 41331236864 passed the dependency-boundary scan, CoreComposition construction-root scan, Release build and 148 NUnit tests. This verifies standalone Core only and does not verify Unity/FishNet.

| Requirement ID | المتطلب | Control ID | Test ID المقترح | الدليل المطلوب |
|---|---|---|---|---|
| GAME-001 | انتقال Vitality عند الصفر إلى Spirit وفق ADR-007 | GAME-CTRL-001 | VitalityAccountTests: Lose_ReachingZeroAutomaticallyTransitionsToSpirit | Core CI run 41331236864; 148 NUnit passed |
| COL-001 | مؤقت Collapse وإعادة الجدولة | COL-CTRL-001 | UNIT-COL-001 | تقرير Unit Test |
| TRF-001 | نقل Debuff ذرياً دون Copy | TRF-CTRL-001 | INT-TRF-001 | تقرير Integration |
| BET-001 | احتساب Betrayal بعد نجاح النقل فقط | BET-CTRL-001 | UNIT-BET-001 | تقرير Unit Test |
| SP-001 | الروح لا تغير حالة الأحياء مباشرة | SP-CTRL-001 | INT-SP-001 | تقرير Integration |
| END-001 | التصويت والنتيجة يقررهما الخادم | END-CTRL-001 | NET-END-001 | سجل مباراة واختبار شبكة |
| NET-001 | العميل يرسل Intent فقط | TRUST-CTRL-001 | SEC-NET-001 | Security regression report |
| CORE-ID-001 | معرفات اللاعب والهدف غير صفرية عند دخول Domain Core | CORE-CTRL-001 | IdentifierTests / VitalityAccountTests | CI run 41331236864; 148 NUnit passed |
| BET-002 | يمنع self-target وdefault BetrayalEvent قبل mutation | BET-CTRL-002 | BetrayalLedgerTests | CI run 41331236864; 148 NUnit passed |
| BET-003 | ترتيب ledger يعتمد EventSequenceId لا ServerTick | BET-CTRL-003 | BetrayalLedgerTests | CI run 41331236864; 148 NUnit passed |
| CORE-TIME-001 | ServerTick zero is valid; StateRevision advances without overflow | CORE-TIME-CTRL-001 | TimeValueTests | Core CI run 41331236864; 148 NUnit passed |
| CORE-CMD-001 | CommandEnvelope uses server-resolved SessionId and server receive tick | CORE-CMD-CTRL-001 | CommandEnvelopeTests | Core CI run 41331236864; 148 NUnit passed |
| CORE-SEQ-001 | Per-session command sequence is monotonic and bounded by configured maximum jump | CORE-SEQ-CTRL-001 | CommandSequenceTrackerTests | Core CI run 41331236864; 148 NUnit passed |
| CORE-SESSION-001 | Session identity is server-issued; authentication association occurs once; disconnect/close are explicit and closed is terminal | CORE-SESSION-CTRL-001 | SessionLifecycleTests | Core CI run 41331236864; 148 NUnit passed; ADR-010 PROPOSED |
| CORE-ROSTER-001 | Match membership is bounded per MatchId; duplicate ActorId rejected; removal does not mutate player lifecycle; snapshots preserve registration order | CORE-ROSTER-CTRL-001 | MatchPlayerRegistryTests | Core CI run 41331236864; 148 NUnit passed; ADR-009 PROPOSED |
| CORE-SESSION-001 | One server-issued SessionId follows Pending → Authenticated → Disconnected → Closed; ActorId binding is immutable; Closed is terminal | CORE-SESSION-CTRL-001 | SessionLifecycleTests | Core CI run 41331236864; 148 NUnit passed; ADR-010 PROPOSED; credentials/reconnect are outside scope |
| CORE-QUEUE-001 | Command admission uses bounded FIFO; full queue rejects without consuming sequence; session replay state resets only after queued commands drain | CORE-QUEUE-CTRL-001 | CommandAdmissionQueueTests | Core CI run 41331236864; 148 NUnit passed |
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

اختبارات القبول المضافة تغطي الانتقالات والرفض والحالات النهائية. حالة هذه الاختبارات: VERIFIED في Core CI run 41331236864 على commit be67d8a3399f99c2c0e05a37eec8ffa0cb2abb13.


## ADR-008 — Command envelope and sequence admission

ADR-008 is PROPOSED. The implementation and tests exist, but the policy is not yet owner-approved. Open policy items include production MaximumSequenceJump, maximum tracked sessions, queue capacity, reconnect/session continuity, rate limiting and command timeout. Queue-full behavior and safe reset semantics passed Core CI run 41331236864 (148 NUnit tests). The current implementation consumes an accepted sequence before later gameplay-rule validation; this is explicitly recorded in ADR-008 and awaits review.


## ADR-009 — Match-scoped player registry

ADR-009 is PROPOSED. The bounded registry, deterministic insertion order, immutable player-ID snapshot and membership-only removal passed Core CI run 41331236864 with 148 NUnit tests. Production match capacity, spectator/Spirit slot treatment, reconnect grace period and post-match retention remain owner/application policy.

## ADR-010 — Server session lifecycle

ADR-010 is PROPOSED. The pure Core session lifecycle primitive and tests cover Pending, Authenticated, Disconnected and Closed states, immutable ActorId binding and terminal closure. Core CI run 41331236864 passed 148 NUnit tests. This does not verify credential validation, connection-to-session mapping, global session limits, reconnect tokens or timeout policy.
