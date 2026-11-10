# Core Implementation Review — 2026-11-10

**Revision:** 2026-11-10  
**Decision Status:** PROPOSED — review findings; no Core code changed  
**Evidence Maturity:** REVIEWED — feature-branch source, NUnit tests, project files and CI job logs  
**Reviewed ref:** feature/core-lifecycle-resolution at 2820285021b1a3d7ae9acd953b3bb65677c33512  
**Implementation boundary:** Standalone pure-C# Core only.

## 1. Review scope

Inspected:
- Result / Result<T> and error codes.
- Typed identifiers, ServerTick and StateRevision.
- VitalityAccount and PlayerLifecycleState.
- BetrayalEvent and BetrayalLedger.
- MatchLifecycle and MatchPlayerRegistry.
- SessionLifecycle and SessionRegistry.
- CommandEnvelope, CommandSequenceTracker, CommandAdmissionQueue.
- AuthenticatedCommand, CommandAdmissionService and SessionCleanupService.
- PlayerStateSnapshot, MatchRosterSnapshot and MatchRosterSnapshotBuilder.
- Corresponding NUnit fixtures and project files.

The review did not execute a local build or test run. Historical CI evidence is recorded separately and remains attached to its exact commit and run.

## 2. Boundary and project configuration

| Item | Observed source state | Interpretation |
|---|---|---|
| Core target framework | netstandard2.1 | Standalone library target; Unity compatibility is not established by this alone. |
| C# language level | 10.0 | Must be checked against the exact Unity editor/toolchain used by the eventual Unity project. |
| Nullable | Enabled | Good compile-time intent; runtime boundaries still require validation. |
| Implicit usings | Disabled | Explicit imports preserve dependency clarity. |
| Treat warnings as errors | Enabled | CI build warnings can block the Core build. |
| NUnit test target | net10.0 | Tests run in the .NET CI environment; this is not a Unity Test Framework assembly. |
| Test package versions | Microsoft.NET.Test.Sdk 17.12.0; NUnit 3.14.0; NUnit3TestAdapter 4.6.0 | Exact package references in the inspected test project. |
| Unity project / asmdefs / package lock | Not established in main; PR #6 body says Unity project is not included | Unity import and integration remain unverified. |
| FishNet package lock / adapter source | Not found in the inspected PR #6 Core source set | FishNet version/API compatibility remains unverified. |

The Core boundary scan in CI reported success for the branch commits listed in the CI Evidence Register. This is evidence for the scanned Core dependency boundary, not proof of a complete Unity assembly structure.

## 3. Implemented Core primitives and test mapping

| Responsibility | Main source | NUnit coverage | Observed state |
|---|---|---|---|
| Result/error representation | Result.cs | ResultTests.cs | IMPLEMENTED / TESTED on feature branch |
| Typed identifiers | Domain/Identifiers.cs | IdentifierTests.cs | IMPLEMENTED / TESTED |
| Server tick and revision value semantics | Domain/TimeValues.cs | TimeValueTests.cs | IMPLEMENTED / TESTED |
| Player vitality/lifecycle | Domain/VitalityAccount.cs | VitalityAccountTests.cs | IMPLEMENTED / TESTED against branch ADR-007 |
| Betrayal event ledger | Domain/BetrayalLedger.cs | BetrayalLedgerTests.cs | IMPLEMENTED / TESTED; no gameplay transfer handler |
| Match phase state machine | Domain/MatchLifecycle.cs | MatchLifecycleTests.cs | IMPLEMENTED / TESTED; phase preconditions remain application policy |
| Match membership | Domain/MatchPlayerRegistry.cs | MatchPlayerRegistryTests.cs | IMPLEMENTED / TESTED; membership only |
| Session state machine | Domain/SessionLifecycle.cs | SessionLifecycleTests.cs | IMPLEMENTED / TESTED; no credentials or transport binding |
| Bounded session registry | Domain/SessionRegistry.cs | SessionRegistryTests.cs | IMPLEMENTED / TESTED |
| Command envelope | Domain/CommandEnvelope.cs | CommandEnvelopeTests.cs | IMPLEMENTED / TESTED |
| Per-session sequence replay guard | Domain/CommandSequenceTracker.cs | CommandSequenceTrackerTests.cs | IMPLEMENTED / TESTED |
| Bounded FIFO | Domain/CommandAdmissionQueue.cs | CommandAdmissionQueueTests.cs | IMPLEMENTED / TESTED |
| Authenticated session admission gate | Services/CommandAdmissionService.cs | CommandAdmissionServiceTests.cs | IMPLEMENTED / TESTED; transport authentication remains external |
| Safe closed-session cleanup | Services/SessionCleanupService.cs | SessionCleanupServiceTests.cs | IMPLEMENTED / TESTED; caller must coordinate in-flight work |
| Roster projection | Domain/MatchRosterSnapshot.cs; Services/MatchRosterSnapshotBuilder.cs | MatchRosterSnapshotBuilderTests.cs | IMPLEMENTED / TESTED; limited roster/vitality projection only |
| Composition root | CoreComposition.cs | CoreCompositionTests.cs | IMPLEMENTED / TESTED for inspected factory scope |

This table is branch-scoped. It does not make any branch file part of main.

## 4. Source-level findings and design boundaries

### CORE-REV-01 — Session cleanup does not track in-flight handlers

SessionCleanupService requires a Closed session and refuses cleanup while commands remain queued. It resets per-session sequence state and then removes the closed session from SessionRegistry.

However, CommandAdmissionService.TryTakeNext dequeues an item and returns an AuthenticatedCommand to the caller. The queue does not track whether the caller has finished executing that command. ADR-013 and source comments explicitly assign this synchronization to the caller.

**Classification:** Documented integration precondition, not a demonstrated Core defect.  
**Required gate:** The eventual application/tick executor must prevent cleanup while a dequeued command is in flight. Add a reproducible integration test for close → dequeue/in-flight → cleanup ordering before calling the full session path verified.

### CORE-REV-02 — Authentication and transport ownership remain outside Core

The admission service verifies:
- Envelope validity.
- SessionId lookup in the Core registry.
- Authenticated session state.
- Non-zero ActorId binding.
- Revalidation at dequeue.

It does not authenticate credentials, prove that a live transport connection owns the SessionId, or authorize the specific gameplay operation. Those checks must be performed by trusted server ingress/application handlers before state mutation.

**Classification:** Explicit responsibility boundary.  
**Required gate:** Implement trusted connection-to-session mapping and credential validation in a real Net/application layer, then test spoofed identity, revoked connections and unauthorized commands.

### CORE-REV-03 — CommandSequenceId is not CommandId

The inspected Core identifiers include CommandSequenceId scoped to SessionId. The sequence tracker is in-memory, rejects stale/replayed sequences and enforces a configured maximum jump. It does not keep command results under a stable player identity.

No CommandId type or idempotency-result store was found in the inspected Core identifiers/services. ADR-015 remains PROPOSED and explicitly says no code/test change is made by that ADR.

**Classification:** Known unimplemented proposed capability.  
**Required gate:** Do not claim effect-level idempotency, reconnect-safe result replay or restart-safe protection. Approve ADR-015 and specify atomic reservation/result semantics before implementing it.

### CORE-REV-04 — BetrayalLedger event retention is not bounded

BetrayalLedger stores every accepted BetrayalEvent in an in-memory dictionary keyed by EventSequenceId so it can distinguish exact replay from conflicting reuse. The inspected ledger exposes no per-match maximum event count or explicit clear/retention method.

The composition root describes the ledger as match-scoped and says one CoreComposition should be created per match. The actual lifetime therefore depends on the application releasing the composition and its references after match completion.

**Classification:** Resource-lifecycle policy gap; operational impact depends on composition lifetime and maximum event rate.  
**Required gate:** Define the match-context lifetime and maximum event volume/retention before long-running multi-match server use. Do not add silent eviction because it would change replay semantics.

### CORE-REV-05 — Composition-root match scope is a caller convention

CoreComposition owns one BetrayalLedger instance and can create multiple MatchLifecycle and MatchPlayerRegistry instances. Its XML documentation says to create one composition per match, but the API does not bind the composition to a specific MatchId or reject creation of objects for more than one match.

**Classification:** Wiring invariant documented but not mechanically enforced.  
**Required gate:** The application bootstrap must demonstrate one composition scope per match, or a future ADR may define an enforced match-scoped aggregate. No refactor is made in this review.

### CORE-REV-06 — Snapshot is a roster projection, not a complete world snapshot

MatchRosterSnapshot captures MatchId, phase, caller-supplied ServerTick/StateRevision and a roster of ActorId/lifecycle/vitality. It explicitly excludes world transforms, Debuffs, Collapse timers, voting, connection state and transport data.

The builder reads the roster snapshot and then reads each player account sequentially. Source comments declare single-writer/server-serialized operation; no internal transaction, lock or aggregate capture barrier is provided.

**Classification:** Deliberately limited projection; atomicity depends on the authoritative caller serializing mutation and capture.  
**Required gate:** Do not call this the complete match/world snapshot. Define and test an atomic capture boundary before network replication.

### CORE-REV-07 — StateRevision is a value type, not a world revision service

StateRevision provides a safe TryAdvance operation and rejects ulong overflow. The inspected Core does not provide a central revision owner that binds every authoritative mutation to exactly one revision advance.

**Classification:** Primitive implemented; end-to-end revision ownership is not established.  
**Required gate:** Define who advances the revision and whether it changes once per command, tick or committed state transaction before snapshot replication.

### CORE-REV-08 — Betrayal ledger and atomic transfer are separate

BetrayalLedger validates event identity, sequence, replay and increments the actor score. It does not validate transfer eligibility, move a Debuff, update source/target accounts, or coordinate a transaction across player state and ledger append.

No Debuff aggregate, transfer command handler or atomic Suffering Transfer application service is present in the inspected PR #6 Core source list.

**Classification:** Ledger primitive implemented; gameplay transfer not implemented.  
**Required gate:** Before transfer implementation, define a pure Core interface and transaction contract, then test all rejection paths for zero partial mutation and all success paths for exactly one effect.

## 4.1 Test fixture inventory cross-reference

The source-level test fixture enumeration is maintained separately in [CORE_TEST_COVERAGE_INVENTORY_2026-11-10.md](CORE_TEST_COVERAGE_INVENTORY_2026-11-10.md).

That inventory fetched 16 fixture files from the Core feature branch and counted 171 test methods / 187 NUnit case declarations. The count is consistent with the historical 187/187 Core CI run 42383513646 on commit 2820285021b1a3d7ae9acd953b3bb65677c33512. No fresh tests were run for this documentation review, and no statement/branch coverage percentage was obtained.

## 5. Implemented scope vs remaining product slice

| Capability | Core branch state |
|---|---|
| Typed IDs / Result / time values | IMPLEMENTED / TESTED |
| Vitality / branch lifecycle contract | IMPLEMENTED / TESTED |
| Betrayal ledger primitive | IMPLEMENTED / TESTED |
| Match phase state machine | IMPLEMENTED / TESTED |
| Bounded match membership | IMPLEMENTED / TESTED |
| Bounded session lifecycle and registry | IMPLEMENTED / TESTED |
| Command envelope / sequence / FIFO admission | IMPLEMENTED / TESTED |
| Authenticated-session admission gate | IMPLEMENTED / TESTED; transport identity not included |
| Safe session cleanup service | IMPLEMENTED / TESTED; in-flight coordination external |
| Immutable roster snapshot | IMPLEMENTED / TESTED; not a full match/world snapshot |
| Debuff model and atomic Suffering Transfer | NOT IMPLEMENTED in inspected Core scope |
| Movement simulation | NOT IMPLEMENTED |
| Authoritative tick pipeline / tick executor | NOT IMPLEMENTED in inspected Core scope |
| CommandId/result idempotency store | NOT IMPLEMENTED |
| Full match outcome resolver / voting rules | NOT IMPLEMENTED |
| Full world snapshot / transport replication | NOT IMPLEMENTED |
| Unity project and assemblies | NOT ESTABLISHED |
| FishNet adapters and network integration tests | NOT IMPLEMENTED / NOT VERIFIED |
| Android client build and device tests | NOT VERIFIED |
| Linux Dedicated Server build/runtime tests | NOT VERIFIED |

## 6. Recommended next engineering slices

These are proposals, not execution authorizations for architectural decisions:

1. **Core contract gap mapping:** Define interfaces and requirements for Debuff eligibility, transfer transaction, movement, tick ownership, outcome resolver and state revision ownership. No numerical balance constants should be invented.
2. **Transfer vertical slice:** After a proposed interface and tests are reviewed, implement a pure Core transaction for eligible Debuff Move + Betrayal event/score, with all failure paths proving no partial mutation.
3. **Command/application integration:** Define trusted server ingress, command handlers, in-flight command completion and cleanup orchestration before transport integration.
4. **Snapshot boundary:** Decide what the MVP client is allowed to see, what constitutes an atomic capture, and which mutations advance StateRevision.
5. **Unity/FishNet integration:** Only after a real Unity project exists and the exact resolved FishNet package version/API can be compiled and verified. Keep Core pure and Net/Unity wrappers thin.
6. **Integration evidence:** Add tests for actual host/dedicated-server connections, client spoofing, replay, disconnect/reconnect, concurrent transfer, packet loss/reorder and snapshot consistency.
7. **Android/Linux gates:** Build and test real artifacts, retain logs and hashes, measure device/server performance and compare with approved requirements.

For every new Core function, define a pure contract first and add NUnit coverage for all branches. For major architecture changes, prepare an ADR before implementation.

## 7. Status

| Area | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| Existing Core primitives | — | IMPLEMENTED / TESTED on feature branch | Not merged to main |
| Core pure-C# dependency boundary | APPROVED per branch ADR-005 | TESTED on historical CI runs | Unity assembly/import still not verified |
| Core lifecycle implementation | APPROVED per branch ADR-007 | TESTED | Original approval event mapping and main conflict open |
| Session/command admission | PROPOSED per ADR-008…013 | IMPLEMENTED / TESTED | Transport/auth/in-flight integration incomplete |
| CommandId/idempotency | PROPOSED per ADR-015 | DOCUMENTED | NOT IMPLEMENTED |
| Debuff/atomic transfer | PROPOSED FOR PLAYTEST baseline | — | NOT IMPLEMENTED |
| Full simulation/tick pipeline | PROPOSED | — | NOT IMPLEMENTED |
| Unity/FishNet | PROPOSED | — | NOT IMPLEMENTED / NOT VERIFIED |
| Android/Linux runtime | PROPOSED | — | NOT VERIFIED |

## 8. Non-claims

This review does not report a fresh build/test execution, does not modify Core source, and does not certify Unity/FishNet/Android/Linux or production readiness. Historical CI results remain scoped to their exact commits and runs.
