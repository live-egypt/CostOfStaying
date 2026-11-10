# Core Test Coverage Inventory — 2026-11-10

**Revision:** 2026-11-10  
**Decision Status:** — (test inventory; it does not approve gameplay or architecture policy)  
**Evidence Maturity:** REVIEWED — exact test fixture files fetched from feature/core-lifecycle-resolution at 2820285021b1a3d7ae9acd953b3bb65677c33512  
**Scope:** Standalone C# Core and NUnit fixtures only. No test source was changed.

> The counts below are a static source inventory. The historical CI results are separately recorded in CI_EVIDENCE_REGISTER.md. No fresh local test execution was performed during this review.

## 1. Project configuration

| Project | Target framework | Language | Dependencies / policy |
|---|---|---|---|
| src/CostOfStaying.Core/CostOfStaying.Core.csproj | netstandard2.1 | C# 10.0 | Nullable enabled, implicit usings disabled, TreatWarningsAsErrors true |
| tests/CostOfStaying.Core.Tests/CostOfStaying.Core.Tests.csproj | net10.0 | C# 10.0 | NUnit 3.14.0, NUnit3TestAdapter 4.6.0, Microsoft.NET.Test.Sdk 17.12.0 |

The test assembly has InternalsVisibleTo("CostOfStaying.Core.Tests") so tests can exercise internal constructors and internal server-side services. This does not make those constructors public API.

## 2. Fixture inventory

Counts below count each [Test] and each [TestCase] declaration as one NUnit test case. A method with multiple TestCase attributes contributes multiple cases.

| # | Test fixture | Source file | Test methods | NUnit cases |
|---:|---|---|---:|---:|
| 1 | BetrayalLedgerTests | tests/CostOfStaying.Core.Tests/BetrayalLedgerTests.cs | 15 | 15 |
| 2 | CommandAdmissionQueueTests | tests/CostOfStaying.Core.Tests/CommandAdmissionQueueTests.cs | 11 | 11 |
| 3 | CommandAdmissionServiceTests | tests/CostOfStaying.Core.Tests/CommandAdmissionServiceTests.cs | 13 | 15 |
| 4 | CommandEnvelopeTests | tests/CostOfStaying.Core.Tests/CommandEnvelopeTests.cs | 5 | 5 |
| 5 | CommandSequenceTrackerTests | tests/CostOfStaying.Core.Tests/CommandSequenceTrackerTests.cs | 17 | 17 |
| 6 | CoreCompositionTests | tests/CostOfStaying.Core.Tests/CoreCompositionTests.cs | 11 | 11 |
| 7 | IdentifierTests | tests/CostOfStaying.Core.Tests/IdentifierTests.cs | 12 | 13 |
| 8 | MatchLifecycleTests | tests/CostOfStaying.Core.Tests/MatchLifecycleTests.cs | 5 | 18 |
| 9 | MatchPlayerRegistryTests | tests/CostOfStaying.Core.Tests/MatchPlayerRegistryTests.cs | 12 | 12 |
| 10 | MatchRosterSnapshotBuilderTests | tests/CostOfStaying.Core.Tests/MatchRosterSnapshotBuilderTests.cs | 6 | 6 |
| 11 | ResultTests | tests/CostOfStaying.Core.Tests/ResultTests.cs | 12 | 12 |
| 12 | SessionCleanupServiceTests | tests/CostOfStaying.Core.Tests/SessionCleanupServiceTests.cs | 6 | 6 |
| 13 | SessionLifecycleTests | tests/CostOfStaying.Core.Tests/SessionLifecycleTests.cs | 13 | 13 |
| 14 | SessionRegistryTests | tests/CostOfStaying.Core.Tests/SessionRegistryTests.cs | 9 | 9 |
| 15 | TimeValueTests | tests/CostOfStaying.Core.Tests/TimeValueTests.cs | 5 | 5 |
| 16 | VitalityAccountTests | tests/CostOfStaying.Core.Tests/VitalityAccountTests.cs | 19 | 19 |
| | **Total** | **16 fixture files** | **171 methods** | **187 cases** |

The source-inventoried 187 NUnit cases match the 187 passed result in Core CI run 42383513646 on the same feature-branch commit 2820285021b1a3d7ae9acd953b3bb65677c33512. This is a consistency check between the fetched fixture source and historical CI metadata, not a new test execution.

## 3. Contract-to-test map

| Core contract / domain | Fixture(s) | What the tests cover | Boundary not established by these fixtures |
|---|---|---|---|
| Result / Result<T> | ResultTests | Success/failure, default sentinel, error-code validation, null rejection, value-type support | No domain/application behavior beyond result semantics |
| Typed identifiers | IdentifierTests | Non-zero validity, equality, boundary values, type separation | Does not resolve whether ActorId and VictimId share the same domain namespace |
| ServerTick / StateRevision | TimeValueTests | Initial values, comparisons, monotonic increment, overflow rejection | No central tick executor or state-revision owner |
| Vitality / player lifecycle | VitalityAccountTests | Alive → Spirit at zero, restore bounds, explicit Resurrect, terminal Eliminated | No gameplay authorization, costs, cooldowns, permission matrix or balance |
| Betrayal ledger | BetrayalLedgerTests | Score increments, exact replay, conflicting sequence, order, default struct rejection, overflow boundary | No Debuff movement, eligibility, match persistence or bounded retention |
| Match phase machine | MatchLifecycleTests | Allowed and rejected transitions, invalid enums, immutable rejection | No player-count, timer, vote, result or match orchestration preconditions |
| Match membership | MatchPlayerRegistryTests | Capacity, duplicate rejection, lookup/removal, order, immutable ID snapshot | No spectator/Spirit slot policy, player-count rules or match aggregate |
| Session lifecycle | SessionLifecycleTests | Pending/authenticated/disconnected/closed transitions, actor binding, terminal state | No credential authentication, transport connection state, reconnect token or timeout |
| Session registry | SessionRegistryTests | Pending-only registration, capacity, duplicates, ordered immutable IDs | No per-actor multi-session policy, persistence or global identity uniqueness |
| Command envelope | CommandEnvelopeTests | Required session/sequence/command validity and server metadata preservation | Does not authenticate the transport source or validate gameplay payload |
| Sequence tracker | CommandSequenceTrackerTests | Strictly increasing session sequence, bounded jump, tracked-session capacity, reset | No stable CommandId, result replay, reconnect-safe or restart-safe idempotency |
| Command queue | CommandAdmissionQueueTests | FIFO, capacity, replay reset restrictions and session capacity | No in-flight execution tracking or concurrency/thread-safety |
| Command admission | CommandAdmissionServiceTests | Authenticated session binding, server-resolved ActorId, dequeue-time revalidation, stale session discard | No credentials, connection ownership, operation authorization, phase/payload validation |
| Session cleanup | SessionCleanupServiceTests | Closed-only cleanup, queued-command drain, replay reset, registry removal | No in-flight handler completion barrier; caller coordination remains required |
| Roster snapshot | MatchRosterSnapshotBuilderTests | Roster order, lifecycle/vitality validation, immutable copy, match metadata | Not a full world snapshot; no atomic capture coordinator or replication |
| Composition root | CoreCompositionTests | Construction is centralized; created scopes/services use supplied dependencies | One-composition-per-match is documented, not enforced by API |

## 4. Coverage gaps visible from the inspected Core source scope

The following gameplay/application capabilities do not have corresponding implementations and fixtures in the inspected Core slice. This is a source-boundary finding, not proof that no work exists elsewhere in every branch.

| Capability | Inspected Core state | Required future tests before claiming implementation |
|---|---|---|
| Debuff model and atomic Suffering Transfer | Not present in inspected Core slice | Eligibility, source/target capacity, full Move semantics, rejection with zero partial mutation, one-time score/event commit |
| Movement simulation | Not present | Bounds, deterministic update, invalid input, server-owned movement, speed/teleport abuse |
| Authoritative tick pipeline / TickExecutor | Not present | Single owner, non-reentrancy, deterministic ordering, overload/catch-up policy |
| Match end/result resolver and vote rules | Not present | Last Survivor, unanimous surrender, timeout, mutual collapse, ties and disconnect edge cases |
| Stable CommandId / idempotency result store | Not present | Duplicate command result replay, atomic reservation, conflict on same ID/different payload, approved retention boundary |
| Connection-to-session mapping and credential authentication | Outside Core | Spoofed connection/session, revoked session, credential failure and identity-binding integration |
| In-flight command completion / cleanup coordination | Outside current cleanup service | Close → command dequeued/in flight → cleanup waits → registry removal |
| Full snapshot capture / network replication | Outside current roster snapshot | Atomic capture, revision advancement, client projection privacy, serialization and reconnect snapshot |
| Unity/FishNet runtime adapters | Not present in inspected Core project | EditMode/PlayMode, ServerRpc/ObserversRpc ingress/egress, serialization, Host and Dedicated Server |
| Android / Linux release | Not part of these projects | Real build artifacts, install/launch, physical-device profiling and headless server runtime tests |

## 5. CI interpretation

The Core CI workflow:
- Enforces no UnityEngine/FishNet references in Core source.
- Enforces selected construction-root and service-access boundaries.
- Uses .NET 10 SDK for restore/build/test.
- Builds Core in Release and the NUnit test project.
- Runs NUnit and uploads TRX output.

The CI workflow is path-filtered to Core, tests and workflow paths. A documentation-only PR does not imply that this workflow ran. The historical run/commit/artifact data is maintained in CI_EVIDENCE_REGISTER.md.

## 6. QA result and limitations

| Check | Result | Meaning |
|---|---|---|
| Test fixture files | 16 | All 16 files in the PR #6 changed test-file inventory fetched |
| Test method declarations | 171 | Static source count |
| NUnit case declarations | 187 | [Test] plus [TestCase] declarations in those files |
| Historical CI match | 187 passed at run 42383513646 | Run/commit-scoped; no fresh execution |
| Statement/branch coverage percentage | NOT AVAILABLE | No coverage artifact inspected |
| Unity/FishNet integration coverage | NOT PRESENT IN THIS CORE SUITE | Separate test assemblies/runtime needed |
| Production readiness | NOT ESTABLISHED | Core tests cannot certify the complete game |

No source code or tests were changed by this review.
