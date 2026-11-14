# تتبع المتطلبات — v2.1
**الحالة: PARTIALLY VERIFIED | Core baseline and separate P1 review branches have CI evidence; ADR-008 through ADR-017 remain proposed pending policy/owner review**

الغرض هو تتبع العلاقة من المتطلب إلى الضابط والاختبار والدليل. Historical Core CI run 41333575156 passed 181 tests. PR #25 separately passed 191 tests for in-flight cleanup; PR #26 separately passed 210 tests for P1.1 connection/session mapping (run 43599420409, tested source commit 95a7bc93a7fd4772f45e7dc428f7574906945061). These are separate, unmerged review branches, not a single combined build. The evidence verifies standalone pure-C# Core only, not Unity/FishNet or production transport integration.

| Requirement ID | المتطلب | Control ID | Test ID المقترح | الدليل المطلوب |
|---|---|---|---|---|
| GAME-001 | انتقال Vitality عند الصفر إلى Spirit وفق ADR-007 | GAME-CTRL-001 | VitalityAccountTests: Lose_ReachingZeroAutomaticallyTransitionsToSpirit | Core CI run 41333575156; 181 NUnit passed |
| COL-001 | مؤقت Collapse وإعادة الجدولة | COL-CTRL-001 | UNIT-COL-001 | تقرير Unit Test |
| TRF-001 | نقل Debuff ذرياً دون Copy | TRF-CTRL-001 | INT-TRF-001 | تقرير Integration |
| BET-001 | احتساب Betrayal بعد نجاح النقل فقط | BET-CTRL-001 | UNIT-BET-001 | تقرير Unit Test |
| SP-001 | الروح لا تغير حالة الأحياء مباشرة | SP-CTRL-001 | INT-SP-001 | تقرير Integration |
| END-001 | التصويت والنتيجة يقررهما الخادم | END-CTRL-001 | NET-END-001 | سجل مباراة واختبار شبكة |
| NET-001 | العميل يرسل Intent فقط | TRUST-CTRL-001 | SEC-NET-001 | Security regression report |
| CORE-ID-001 | معرفات اللاعب والهدف غير صفرية عند دخول Domain Core | CORE-CTRL-001 | IdentifierTests / VitalityAccountTests | CI run 41333575156; 181 NUnit passed |
| BET-002 | يمنع self-target وdefault BetrayalEvent قبل mutation | BET-CTRL-002 | BetrayalLedgerTests | CI run 41333575156; 181 NUnit passed |
| BET-003 | ترتيب ledger يعتمد EventSequenceId لا ServerTick | BET-CTRL-003 | BetrayalLedgerTests | CI run 41333575156; 181 NUnit passed |
| CORE-TIME-001 | ServerTick zero is valid; StateRevision advances without overflow | CORE-TIME-CTRL-001 | TimeValueTests | Core CI run 41333575156; 181 NUnit passed |
| CORE-CMD-AUTH-001 | Only registered Authenticated sessions with a valid ActorId may enter the bounded command queue; rejected sessions do not consume sequence | CORE-CMD-AUTH-CTRL-001 | CommandAdmissionServiceTests | Core CI run 41333575156; 181 NUnit passed; gameplay payload validation remains downstream |
| CORE-CMD-001 | CommandEnvelope uses server-resolved SessionId and server receive tick | CORE-CMD-CTRL-001 | CommandEnvelopeTests | Core CI run 41333575156; 181 NUnit passed |
| CORE-SEQ-001 | Per-session command sequence is monotonic and bounded by configured maximum jump | CORE-SEQ-CTRL-001 | CommandSequenceTrackerTests | Core CI run 41333575156; 181 NUnit passed |
| CORE-SESSION-001 | Session identity is server-issued; authentication association occurs once; disconnect/close are explicit and closed is terminal | CORE-SESSION-CTRL-001 | SessionLifecycleTests | Core CI run 41333575156; 181 NUnit passed; ADR-010 PROPOSED |
| CORE-ROSTER-001 | Match membership is bounded per MatchId; duplicate ActorId rejected; removal does not mutate player lifecycle; snapshots preserve registration order | CORE-ROSTER-CTRL-001 | MatchPlayerRegistryTests | Core CI run 41333575156; 181 NUnit passed; ADR-009 PROPOSED |
| CORE-SESSION-CLEANUP-001 | Closed-session cleanup drains queued commands, resets replay state, then removes the session; direct registry removal/replay reset are internal | CORE-SESSION-CLEANUP-CTRL-001 | SessionCleanupServiceTests | Core CI run 41333575156; 181 NUnit passed; ADR-013 PROPOSED |
| CORE-SESSION-REGISTRY-001 | Session registry is explicitly bounded; accepts only Pending sessions; duplicate SessionId rejected; removal requires Closed; snapshots preserve insertion order | CORE-SESSION-REGISTRY-CTRL-001 | SessionRegistryTests | Core CI run 41333575156; 181 NUnit passed; ADR-011 PROPOSED; actor multi-session policy remains TBD |
| CORE-CONN-001 | Transport connection identity is a non-zero typed server-side identifier, distinct from ActorId and SessionId | CORE-CONN-CTRL-001 | IdentifierTests: TransportConnectionId_* | Core CI run 43599420409; 210 NUnit passed on PR #26; adapter allocation/non-reuse remains a transport responsibility |
| CORE-CONN-002 | A registered Pending session binds to one live connection; one connection cannot bind to multiple sessions; duplicate/invalid/unknown/capacity failures do not partially mutate mappings | CORE-CONN-CTRL-002 | ConnectionSessionRegistryTests: TryBind_* | Core CI run 43599420409; 210 NUnit passed on PR #26; ADR-016 PROPOSED |
| CORE-CONN-003 | Detaching a connection transitions Pending to Closed or Authenticated to Disconnected before clearing the mapping; queued commands are rejected by dequeue-time authentication revalidation | CORE-CONN-CTRL-003 | ConnectionSessionRegistryTests: TryDetach_* / Detach_MakesPreviouslyQueuedCommandFailDequeueAuthenticationRevalidation | Core CI run 43599420409; 210 NUnit passed on PR #26; no real transport adapter tested |
| CORE-SESSION-CLEANUP-002 | Cleanup is blocked while the session remains connection-bound, and Composition rejects a connection registry backed by a different SessionRegistry instance | CORE-SESSION-CLEANUP-CTRL-002 | ConnectionAwareSessionCleanupServiceTests; CoreCompositionTests: CreateSessionCleanupService_RejectsConnectionRegistryFromDifferentSessionRegistry | Core CI run 43599420409; 210 NUnit passed on PR #26 |
| CORE-CMD-DEQUEUE-001 | A dequeued command is handed to gameplay only if its registered session is still Authenticated with a valid ActorId; stale commands are discarded | CORE-CMD-DEQUEUE-CTRL-001 | CommandAdmissionServiceTests: TryTakeNext_DiscardsCommandWhenSessionDisconnectsAfterAdmission / ...ClosesAfterAdmission | Core CI run 41333575156; 181 NUnit passed; transport authorization remains external |
| CORE-QUEUE-001 | Command admission uses bounded FIFO; full queue rejects without consuming sequence; session replay state resets only after queued commands drain | CORE-QUEUE-CTRL-001 | CommandAdmissionQueueTests | Core CI run 41333575156; 181 NUnit passed |
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

اختبارات القبول المضافة تغطي الانتقالات والرفض والحالات النهائية. حالة هذه الاختبارات: VERIFIED في Core CI run 41333575156 على commit 8a193190fb5f2e10c05773fb0789171011bfd2eb.


## ADR-008 — Command envelope and sequence admission

ADR-008 is PROPOSED. The implementation and tests exist, but the policy is not yet owner-approved. Open policy items include production MaximumSequenceJump, maximum tracked sessions, queue capacity, reconnect/session continuity, rate limiting and command timeout. Queue-full behavior and safe reset semantics passed Core CI run 41333575156 (181 NUnit tests). The current implementation consumes an accepted sequence before later gameplay-rule validation; this is explicitly recorded in ADR-008 and awaits review.


## ADR-009 — Match-scoped player registry

ADR-009 is PROPOSED. The bounded registry, deterministic insertion order, immutable player-ID snapshot and membership-only removal passed Core CI run 41333575156 with 181 NUnit tests. Production match capacity, spectator/Spirit slot treatment, reconnect grace period and post-match retention remain owner/application policy.

## ADR-010 — Server session lifecycle

ADR-010 is PROPOSED. The pure Core session lifecycle primitive and tests cover Pending, Authenticated, Disconnected and Closed states, immutable ActorId binding and terminal closure. Core CI run 41333575156 passed 181 NUnit tests. This does not verify credential validation, connection-to-session mapping, global session limits, reconnect tokens or timeout policy.


## ADR-011 — Bounded server session registry

ADR-011 is PROPOSED. The registry is keyed only by SessionId, has explicit capacity, registers Pending sessions only and does not expose removal publicly. Actor multi-session/reconnect policy remains open. Core CI run 41333575156 passed 181 NUnit tests.

## ADR-012 — Authenticated command admission

ADR-012 is PROPOSED. The gate requires a valid envelope, registered Authenticated session and valid ActorId before queue admission. `TryTakeNext` revalidates that binding immediately before handler handoff and returns a server-derived `AuthenticatedCommand<TCommand>`; commands queued before disconnect/close are discarded. Core CI run 41333575156 passed 181 NUnit tests. Gameplay payload validation remains downstream.

## ADR-013 — Safe session cleanup

ADR-013 is PROPOSED. Only SessionCleanupService can coordinate queue drain, internal replay-state reset and internal closed-session removal. The caller must also wait for dequeued/in-flight handlers to finish. Core CI run 41333575156 passed 181 NUnit tests.


## ADR-016 — Transport connection to session binding

ADR-016 is **PROPOSED**. The transport-neutral mapping contract and NUnit tests are implemented in PR #26, but the PR remains unmerged. Core CI run [43599420409](https://github.com/live-egypt/CostOfStaying/actions/runs/43599420409) passed 210 tests on tested source commit `95a7bc93a7fd4772f45e7dc428f7574906945061`; boundary and composition scans passed, and both Release builds reported 0 warnings and 0 errors. Subsequent commits after the tested source SHA updated documentation only; no source, test or workflow files changed after verification. Detailed evidence: [P1.1 verification report](../audit/P1_CONNECTION_SESSION_BINDING_CI_VERIFICATION_2026-11-14.md).

The contract covers typed `TransportConnectionId`, bounded bidirectional mapping, Pending-only binding, one live connection per session, detach transitions, stale-command rejection, and cleanup blocking while bound. It does not implement FishNet/transport integration, actual server connection-ID allocation or non-reuse enforcement by the adapter, credentials/account identity, one-session-per-ActorId policy, reconnect grace/expiry or persistence. The actor-level multi-session and reconnect choices remain unresolved and must not be inferred from this mapping.


## ADR-017 — Credential validation and ActorId binding boundary

ADR-017 is **PROPOSED**; no credential validator, identity provider, account store or ActorId resolver is implemented. The intended trust boundary is outside deterministic Core: resolve the server-issued connection to a Pending session, validate credentials through a selected trusted identity provider, resolve the authenticated principal to a server-owned in-match ActorId, then call `TryAuthenticate`. Account identity and ActorId must not be assumed to be the same identifier.

| Requirement ID | Requirement | Control | Test/evidence | Status |
|---|---|---|---|---|
| CORE-AUTH-001 | A session can become Authenticated only after trusted server-side validation of credentials and principal | CORE-AUTH-CTRL-001 | Provider/application integration tests required after provider selection | PROPOSED — no implementation |
| CORE-AUTH-002 | ActorId is assigned/resolved by the server and cannot be selected by an untrusted client or confused with a persistent account ID | CORE-AUTH-CTRL-002 | Principal-to-Actor resolver tests required | PROPOSED — actor assignment model unresolved |
| CORE-AUTH-003 | Invalid, expired, revoked, malformed or unverifiable credentials do not authenticate a session or partially mutate match state | CORE-AUTH-CTRL-003 | Failure-injection and no-partial-mutation tests required | PROPOSED — credential semantics/provider unresolved |
| CORE-AUTH-004 | Credentials and tokens are excluded from Core state and diagnostic output | CORE-AUTH-CTRL-004 | Redaction tests and security review required | PROPOSED — logging boundary not implemented |

Owner decisions still required: identity provider and guest flow; credential/token format and validation; account-to-ActorId mapping and lifetime; provider outage behavior; retry/rate-limit/lockout; expiration/revocation/refresh; privacy retention; reconnect and concurrent-session policy. ADR-010 and ADR-011 are PROPOSED and do not by themselves settle these items.
