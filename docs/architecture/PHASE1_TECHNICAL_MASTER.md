# Phase 1 — Technical Master Specification
# Cost of Staying (ثمن البقاء)

**Document status:** APPROVED ARCHITECTURAL BASELINE; IMPLEMENTATION PARTIAL  
**Revision date:** 2026-11-01  
**ADR:** ADR-005 — APPROVED  
**Gameplay baseline:** v2.1 — PROPOSED FOR PLAYTEST  
**Unity target:** 6000.3 LTS — assumption, not verified by a Unity project  
**FishNet target:** 4.7.4R — provisional assumption only; not pinned or verified  
**Core target:** .NET Standard 2.1  
**Standalone tests:** .NET 8 / NUnit 3.14

> This document replaces the 85k-line conversation transcript as the maintained technical index. The transcript is historical design evidence only, not a source tree; do not bulk-copy it.

---

## 1. Executive status

The repository currently has a standalone pure-C# Core project and an NUnit test project committed on the feature branch. It does not yet contain a Unity project, FishNet package installation, Unity assembly definitions, or network adapters. The standalone .NET Core build and NUnit tests have passed in GitHub Actions; this evidence does not cover Unity or FishNet.

| Area | Status | Meaning |
|---|---|---|
| ADR-005 Core boundary | APPROVED | Architecture and first-slice scope accepted |
| Pure Core project | VERIFIED | .NET 8 Release build passed in GitHub Actions runs 41255553487 and 41255769742 |
| Vitality and player lifecycle primitive | IMPLEMENTED — verification pending for this branch commit | ADR-007 now selects Alive / Spirit / Eliminated; CI rerun required |
| Betrayal ledger primitive | VERIFIED | Included in successful Core build and NUnit run |
| Core unit tests | VERIFIED | 38 passed; 0 failed; 0 skipped in run 41255553487; latest workflow run 41255769742 also passed and retained a TRX artifact |
| Movement / Debuff transfer | NOT IMPLEMENTED | Contracts and tests required first |
| Match/session lifecycle | NOT IMPLEMENTED | Authoritative identity/lifecycle contracts pending |
| Command admission / queue / pipeline | NOT IMPLEMENTED | Ordering and bounds require decisions |
| Snapshots | NOT IMPLEMENTED | Projection/revision semantics pending |
| FishNet / Unity adapters | NOT IMPLEMENTED | Requires actual Unity project and pinned package |
| Unity project settings / manifests | NOT CREATED | No project metadata exists in repository |
| Gameplay rules v2.1 | PROPOSED FOR PLAYTEST | Not final balance rules |

IMPLEMENTED means source has been committed. VERIFIED requires reproducible compilation and tests with retained evidence. NOT IMPLEMENTED and NOT VERIFIED are distinct states.

## 2. Authority and source-of-truth hierarchy

Resolve conflicts in this order:
1. Approved ADRs and explicit owner decisions.
2. Current gameplay specification (docs/02_GAME_RULES_SPECIFICATION.md).
3. Security boundary (docs/security/TRUST_BOUNDARIES.md).
4. Technical architecture and test strategy.
5. This technical master and implementation notes.
6. Historical transcript and old code examples (reference only).

Existing ADR identifiers are occupied:
- ADR-001: technology stack.
- ADR-002: server authority.
- ADR-003: backend and hosting.
- ADR-004: gameplay rules baseline.
- ADR-005: executable Core boundary.
- ADR-006: Core identifier and Betrayal ledger invariants (PROPOSED; pending owner approval).
- ADR-007: canonical player lifecycle (APPROVED: Alive / Spirit / Eliminated).

Do not create duplicate ADR-001 through ADR-007 using the historical transcript's suggested titles. New decisions use the next available ADR number and must be added to docs/governance/DECISIONS_LOG.md.

## 3. Non-negotiable architecture

### 3.1 Server authority
- The server is the only authority allowed to mutate match state.
- A client sends intent, never an asserted result.
- Never accept client-supplied PlayerId, vitality, betrayal score, match phase, authoritative position, server tick, or state revision.
- The server derives sender identity from its authenticated connection-to-session mapping.
- Core state mutation is invoked only through server-owned application boundaries.

### 3.2 Separation of concerns
- Core is ordinary C# and has no Unity or FishNet dependency.
- Core must not reference UnityEngine, FishNet, MonoBehaviour, NetworkConnection, Time.deltaTime, or Unity logging.
- Net contains transport DTOs and FishNet integration only.
- Unity contains scene lifecycle, input, presentation and view adapters.
- Tests separate pure Core tests from real Unity/FishNet integration tests.

### 3.3 Determinism
- Core receives explicit server tick/time values from its caller.
- No wall-clock reads, random authoritative IDs, Unity time, or network APIs inside deterministic domain functions.
- Ordering is explicit and stable; never rely on dictionary iteration order.
- Sequence numbers are server-owned and monotonic within a documented scope.
- Tie-break rules must be documented and tested before implementation.

### 3.4 Atomicity
Any rejected command must leave authoritative state unchanged. A successful transfer must commit Debuff ownership, source/target state, cooldown and Betrayal event/score as one server-side operation. Partial success is forbidden.

### 3.5 Test-driven development
Every public state transition, rule decision, command admission path and deterministic ordering function requires tests for success, invalid input, boundaries, replay, wrong phase, identity mismatch, and unchanged state on rejection.

## 4. Canonical domain decisions

### 4.1 Canonical player lifecycle — ADR-007 APPROVED

The controlling decision is the earlier owner-approved Phase 1 matrix, reaffirmed in ADR-007:
- Player lifecycle states are `Alive`, `Spirit`, and `Eliminated`.
- Vitality reaching zero automatically transitions `Alive → Spirit`.
- `Resurrect` and `Eliminate` are explicit server-authoritative operations.
- `Eliminated` is terminal.
- The v2.1 `Alive → Dead` lifecycle is superseded and must not be implemented as a competing state model.

The current Core implementation now exposes `PlayerLifecycleState` and explicit `TryResurrect` / `TryEliminate` operations. A successful Core build/test on the new branch is still required before calling it VERIFIED.

### 4.2 Vitality semantics — canonical domain contract

- Default Maximum is 100; custom positive maxima are supported.
- Initial state is Alive with Current = Maximum.
- `Lose` affects Alive only, clamps at zero, and automatically enters Spirit at zero.
- `Restore` affects Alive only and is capped at Maximum; it never resurrects.
- `TryResurrect` succeeds only from Spirit and requires a positive requested vitality amount, capped at Maximum.
- `TryEliminate` succeeds from Alive or Spirit, sets vitality to zero, and enters terminal Eliminated.
- Invalid/repeated Try operations return false without mutation.
- Core validates domain invariants only. Authorization, match phase, resurrection cost/cooldown and other policy checks belong to the server application layer and must be performed before invoking these methods.
- Spirit abilities, score, and connection/match membership are separate systems.
- Decay rate and Collapse increments remain configuration/TBD until playtest evidence.

### 4.3 Betrayal / Debuff transfer
Proposed v2.1 preconditions:
1. Source and target are Alive.
2. Both belong to the same match.
3. Target differs from source.
4. Distance is at most 2 metres.
5. Source cooldown is ready.
6. Target has capacity.
7. Source has an eligible transferable Debuff.

On success, move (do not copy) the latest eligible Debuff, preserve remaining duration and linked decay modifier, remove it from source, apply it to target, apply configured cooldown, and record exactly one Betrayal point/event for source.

On rejection, no player state, Debuff, cooldown, score or event ledger mutation.

Cooldown duration, capacity, Debuff duration, decay modifier and eligible-selection tie-break remain TBD unless explicitly defined in the current gameplay specification. Do not invent constants.

### 4.4 Match, player and connection lifecycle separation

Match phases in v2.1:
Created → Waiting → Starting → Running → VoteOpen → Running or Ending → Completed.

Player participation lifecycle is controlled by ADR-007:
Alive → Spirit → (Alive through explicit Resurrect, or Eliminated through explicit Eliminate).
Eliminated is terminal.

Do not collapse these independent concepts into one enum:
- PlayerLifecycleState: Alive / Spirit / Eliminated.
- Vitality: bounded numeric resource.
- ConnectionState: transport/authentication connectivity.
- MatchMembership: membership in a match.
- Spectator/presentation projection: view and interaction permissions.

Disconnect/reconnect policy, Spirit command permissions, resurrection costs/cooldowns and match-phase preconditions remain separate decisions. Do not infer them from connection state or embed them in VitalityAccount.

### 4.5 Movement
The server computes authoritative movement. Client messages are inputs/intents only. Final speed, acceleration, input normalization, collision, tick rate and anti-cheat thresholds are not established by the current baseline. Keep them configurable/TBD until a prototype and measurement exist.

## 5. Canonical repository layout

### 5.1 Current standalone source layout

~~~text
src/
└── CostOfStaying.Core/
    ├── CostOfStaying.Core.csproj
    ├── Contracts/
    │   ├── IVitalityAccount.cs
    │   └── IBetrayalLedger.cs
    └── Domain/
        ├── Identifiers.cs
        ├── VitalityAccount.cs
        └── BetrayalLedger.cs

tests/
└── CostOfStaying.Core.Tests/
    ├── CostOfStaying.Core.Tests.csproj
    ├── VitalityAccountTests.cs
    └── BetrayalLedgerTests.cs
~~~

### 5.2 Intended Unity layout (target only; not yet created)

~~~text
Assets/CostOfStaying/
├── Core/
│   ├── Abstractions/
│   ├── Commands/
│   ├── Domain/{Match,Players,Sessions,Vitality}/
│   ├── Rules/
│   ├── Simulation/
│   ├── Snapshots/
│   ├── Time/
│   ├── Results/
│   └── CoreComposition.cs
├── Net/{Broadcasts,Adapters,Sessions,Mapping}/
├── Unity/{Bootstrap,Input,Presentation}/
└── Tests/{EditMode,PlayMode}/
~~~

The standalone .NET source is the current executable boundary. Moving it into Assets is a later integration task after Unity project creation. Do not maintain two divergent Core copies.

## 6. Technical file register

Status definitions:
- IMPLEMENTED: source file committed.
- PROPOSED: planned file/contract; no source committed.
- NOT IMPLEMENTED: explicitly deferred.
- NOT VERIFIED: source/config exists without reproducible verification.
- TBD: behavior is not specified enough to implement safely.

### 6.1 Core contracts, identifiers and state

| File | Responsibility | Required test | Status |
|---|---|---|---|
| Contracts/IVitalityAccount.cs | Pure vitality contract | VitalityAccountTests | IMPLEMENTED |
| Contracts/IBetrayalLedger.cs | Betrayal ledger contract | BetrayalLedgerTests | IMPLEMENTED |
| Domain/Identifiers.cs | ActorId, VictimId, EventSequenceId | IdentifierTests | IMPLEMENTED |
| Domain/VitalityAccount.cs | Bounded vitality; terminal Alive → Dead | VitalityAccountTests | IMPLEMENTED |
| Domain/BetrayalLedger.cs | Monotonic event sequence, replay rejection, score | BetrayalLedgerTests | IMPLEMENTED |
| CoreComposition.cs | Single Core composition root | CoreCompositionTests | PROPOSED |
| Result.cs / ResultT.cs | Consistent success/failure contracts | ResultTests | PROPOSED |
| PlayerId.cs / SessionId.cs | Canonical player/session IDs | IdentifierTests | PROPOSED; reconcile ActorId naming |
| ServerTick.cs / StateRevision.cs | Server tick and revision values | TimeAndRevisionTests | PROPOSED |
| Position2.cs / MovementInput.cs | Engine-independent movement values | MovementValueTests | PROPOSED |
| PlayerState.cs / MatchState.cs / WorldState.cs | Authoritative domain state | Corresponding state tests | PROPOSED |

Naming rule: do not create PlayerId as a second identity type if ActorId is retained. Select one canonical name in a reviewed contract change before adding duplicates.

### 6.2 Rules

| File | Responsibility | Required test | Status |
|---|---|---|---|
| MatchRules.cs | Match capacity and phase invariants | MatchRulesTests | PROPOSED |
| MovementRules.cs | Movement constraints and configured parameters | MovementRulesTests | PROPOSED; tuning TBD |
| BetrayalRules.cs | Transfer eligibility, 2m range, cooldown/capacity | BetrayalRulesTests | PROPOSED |
| VitalityRules.cs | Vitality bounds and owner-approved lifecycle transition | VitalityRulesTests | PROPOSED; lifecycle blocked by ADR-007 |
| AdmissionRules.cs | Command admission conditions | AdmissionRulesTests | PROPOSED |
| PhaseRules.cs | Command-to-phase permissions | PhaseRulesTests | PROPOSED |

### 6.3 Commands and admission

| File | Responsibility | Required test | Status |
|---|---|---|---|
| CommandEnvelope.cs | Server-derived sender/session, sequence and receive tick | CommandEnvelopeTests | PROPOSED |
| CommandKind.cs | Supported command kinds | CommandKindTests | PROPOSED |
| MovementCommand.cs | Client movement intent only | MovementCommandTests | PROPOSED |
| BetrayalCommand.cs | Client transfer intent only | BetrayalCommandTests | PROPOSED |
| CommandOutcome.cs | Accepted/rejected outcome projection | CommandOutcomeTests | PROPOSED |
| CommandSequenceTracker.cs | Duplicate and replay protection | CommandSequenceTrackerTests | PROPOSED |
| CommandAdmission.cs | Initial validation before queue | CommandAdmissionTests | PROPOSED |
| MovementCommandIngress.cs | Validate/enqueue movement intent | MovementIngressTests | PROPOSED |
| BetrayalCommandIngress.cs | Validate/enqueue transfer intent | BetrayalIngressTests | PROPOSED |
| CommandQueue.cs | Bounded deterministic queue | CommandQueueTests | PROPOSED; capacity/order TBD |

Client payloads must not choose authoritative sender identity, SessionId, server tick or state revision.

### 6.4 Sessions and match lifecycle

| File | Responsibility | Required test | Status |
|---|---|---|---|
| PlayerSession.cs | Authenticated player/session association | PlayerSessionTests | PROPOSED |
| SessionLifecycle.cs | Pending/auth/disconnect transitions | SessionLifecycleTests | PROPOSED |
| MatchLifecycle.cs | Lobby/running/ending transitions | MatchLifecycleTests | PROPOSED |
| MatchPhase.cs | Match phase enumeration | MatchPhaseTests | PROPOSED |
| AdmissionResult.cs | Admission decision value | AdmissionResultTests | PROPOSED |
| ReconnectPolicy.cs | Reconnect eligibility and identity continuity | ReconnectPolicyTests | TBD; retention/token model unspecified |

### 6.5 Simulation

| File | Responsibility | Required test | Status |
|---|---|---|---|
| MovementSystem.cs | Server-side movement simulation | MovementSystemTests | PROPOSED |
| BetrayalSystem.cs | Validate and atomically apply Debuff transfer | BetrayalSystemTests | PROPOSED |
| VitalitySystem.cs | Server-side decay and death transition | VitalitySystemTests | PROPOSED; rates TBD |
| CooldownSystem.cs | Tick-based cooldown evaluation | CooldownSystemTests | PROPOSED |
| MatchSystem.cs | Match end conditions/outcomes | MatchSystemTests | PROPOSED |
| CommandProcessor.cs | Execute one admitted command | CommandProcessorTests | PROPOSED |
| CorePipeline.cs | Deterministic tick stages | CorePipelineTests | PROPOSED |
| TickExecutor.cs | Single owner of authoritative tick | TickExecutorTests | PROPOSED |
| DeterministicOrdering.cs | Stable ordering/tie-break | DeterministicOrderingTests | PROPOSED; ADR required |

### 6.6 Snapshots

| File | Responsibility | Required test | Status |
|---|---|---|---|
| CoreSnapshot.cs | Immutable world projection | CoreSnapshotTests | PROPOSED |
| PlayerSnapshot.cs | Immutable player projection | PlayerSnapshotTests | PROPOSED |
| MatchSnapshot.cs | Immutable match projection | MatchSnapshotTests | PROPOSED |
| SnapshotFactory.cs | Build snapshot from authoritative state | SnapshotFactoryTests | PROPOSED |
| StateRevisionGenerator.cs | Monotonic revision | StateRevisionGeneratorTests | PROPOSED |
| SnapshotValidation.cs | Validate snapshot invariants | SnapshotValidationTests | PROPOSED |

Full/delta strategy, frequency and retention are undecided. Do not optimize replication before measurement.

### 6.7 Net / FishNet

| File | Responsibility | Status |
|---|---|---|
| MovementInputBroadcast.cs | Movement intent DTO | PROPOSED |
| BetrayalRequestBroadcast.cs | Transfer intent DTO | PROPOSED |
| CoreSnapshotBroadcast.cs | Snapshot DTO | PROPOSED |
| CoreRejectBroadcast.cs | Rejection DTO | PROPOSED |
| CommandOutcomeBroadcast.cs | Outcome DTO | PROPOSED |
| FishNetServerAdapter.cs | Server transport boundary | NOT IMPLEMENTED |
| FishNetClientAdapter.cs | Client send/receive boundary | NOT IMPLEMENTED |
| CoreServerCommandIngress.cs | Authenticated network intent to Core | PROPOSED |
| CoreServerSessionBridge.cs | FishNet connection lifecycle to Core sessions | PROPOSED |
| ConnectionSessionMap.cs | Server-owned connection key to SessionId | PROPOSED |
| PendingConnectionRegistry.cs | Unauthenticated connection tracking | PROPOSED |
| FishNetServerRejector.cs | Safe rejection to requester | PROPOSED |
| CoreSnapshotMapper.cs | Core snapshot to network DTO | PROPOSED |
| FishNetCommandOutcomePublisher.cs | Publish outcome to authorized recipient | PROPOSED |
| ServerCoreRuntime.cs | Drive Core from server tick lifecycle | NOT IMPLEMENTED |

Write adapter signatures against the FishNet package actually installed. 4.7.4R is not verified and must not be treated as a pinned dependency.

### 6.8 Unity

| File | Responsibility | Status |
|---|---|---|
| ServerBootstrap.cs | Compose server-owned Core services | PROPOSED |
| ClientBootstrap.cs | Compose client adapter/projection | PROPOSED |
| ServerCoreRuntimeBehaviour.cs | Bind server lifecycle to tick executor | PROPOSED |
| ClientProjectionBehaviour.cs | Apply snapshots to presentation | PROPOSED |
| UnityInputAdapter.cs | Convert input to movement intent | PROPOSED |
| PlayerView.cs | Player visual representation | PROPOSED |
| MatchView.cs | Match presentation | PROPOSED |
| ConnectionView.cs | Connection presentation | PROPOSED |

Unity behaviours are thin adapters; authoritative rules do not belong in MonoBehaviours.

### 6.9 Pure Core tests

| Test file | Coverage | Status |
|---|---|---|
| ResultTests.cs | Result and Result<T> | PROPOSED |
| ValueObjectTests.cs | IDs, ticks, positions, revisions | PROPOSED |
| RulesTests.cs | Rule boundaries | PROPOSED |
| CommandTests.cs | Commands and sequence behavior | PROPOSED |
| QueueTests.cs | Capacity, order, duplicates | PROPOSED |
| SessionLifecycleTests.cs | Pending/auth/disconnect/reconnect | PROPOSED |
| MovementSystemTests.cs | Movement validation/simulation | PROPOSED |
| BetrayalSystemTests.cs | Transfer gates and atomic rejection | PROPOSED |
| VitalitySystemTests.cs | Decay, clamp, terminal death | PROPOSED |
| CorePipelineTests.cs | Deterministic tick | PROPOSED |
| SnapshotTests.cs | Immutability and revision | PROPOSED |
| SecurityInvariantTests.cs | Authority, replay, identity spoofing | PROPOSED |
| IdentifierTests.cs | Numeric value semantics, validity and type separation for current IDs | VERIFIED in CI |
| VitalityAccountTests.cs | Current Vitality primitive | VERIFIED in CI |
| BetrayalLedgerTests.cs | Current ledger primitive | VERIFIED in CI |

### 6.10 Unity/FishNet integration tests

| Test file | Coverage | Status |
|---|---|---|
| ServerClientConnectionTests.cs | Server/client connection | NOT IMPLEMENTED |
| AuthenticationFlowTests.cs | Pending to authenticated | NOT IMPLEMENTED |
| MovementBroadcastTests.cs | Client intent to server | NOT IMPLEMENTED |
| BetrayalBroadcastTests.cs | Transfer intent to server | NOT IMPLEMENTED |
| SnapshotBroadcastTests.cs | Server to client snapshot | NOT IMPLEMENTED |
| DisconnectCleanupTests.cs | Session cleanup | NOT IMPLEMENTED |
| ReconnectTests.cs | Reconnect identity continuity | NOT IMPLEMENTED |
| DuplicateRegistrationTests.cs | Duplicate registration rejection | NOT IMPLEMENTED |
| DedicatedServerTests.cs | Headless dedicated server | NOT IMPLEMENTED |
| HostModeTests.cs | Host mode | NOT IMPLEMENTED |
| PacketReorderingTests.cs | Reordered packet behavior | NOT IMPLEMENTED |
| PacketLossTests.cs | Loss behavior by channel | NOT IMPLEMENTED |

### 6.11 Assembly definitions and project configuration

| File | Purpose | Status |
|---|---|---|
| CostOfStaying.Core.asmdef | Engine-independent Core assembly | NOT CREATED |
| CostOfStaying.Net.asmdef | FishNet adapter assembly | NOT CREATED |
| CostOfStaying.Unity.asmdef | Unity behaviours assembly | NOT CREATED |
| CostOfStaying.Core.Tests.asmdef | Core EditMode tests | NOT CREATED |
| CostOfStaying.Net.Tests.asmdef | Net PlayMode tests | NOT CREATED |
| Packages/manifest.json | Unity package dependencies | NOT CREATED |
| Packages/packages-lock.json | Resolved package versions | NOT CREATED |
| ProjectSettings/ProjectVersion.txt | Exact Unity editor version | NOT CREATED |
| ProjectSettings/EditorBuildSettings.asset | Build/test scenes | NOT CREATED |
| .gitignore | Unity generated-file exclusions | Existing policy requires review |
| .gitattributes | Text normalization / optional LFS | PROPOSED |
| README.md | Setup and test instructions | Existing README; update after project exists |
| CHANGELOG.md | Versioned change history | PROPOSED |

Do not create placeholder manifests or locks that imply an installed Unity/FishNet version. Pin versions only after installation and successful editor import.

## 7. Required future ADRs

The historical transcript suggested new ADRs numbered 001–006, which conflict with the existing register. ADR-006 is assigned to Core identifier and Betrayal ledger invariants; ADR-007 is assigned to the player lifecycle/Vitality conflict. The following topics are candidates for ADR-008 onward; create each only when the decision is ready for review.

| Candidate topic | Decision required | Gate |
|---|---|---|
| Command ordering | Ordering key, tie-break, late command policy | Before pipeline |
| Fixed tick | Tick rate, catch-up cap, overload policy | Before TickExecutor |
| Session authentication | Credential/token model, binding, expiry, reconnect | Before network auth |
| Snapshot strategy | Full/delta, frequency, revision semantics | Before replication |
| Queue bounds | Capacity, overflow, per-session limits | Before command queue |
| Movement model | Units, speed, acceleration, collision/input limits | Before movement implementation |

Core/Net separation and server authority are already addressed by ADR-005 and ADR-002. Do not duplicate them.

## 8. Security invariants

These must become automated regression tests before network release:
1. Client cannot choose authoritative ActorId/PlayerId.
2. Client cannot choose SessionId or claim authenticated status.
3. Client cannot submit authoritative position, Vitality, Betrayal score, server tick or revision.
4. Duplicate command/event cannot execute twice.
5. Old/out-of-order sequence cannot roll back accepted state.
6. Rejected transfer leaves source, target, cooldown, score and event history unchanged.
7. Successful transfer moves one eligible Debuff exactly once.
8. Client cannot execute commands outside the allowed match phase.
9. Disconnected/unauthenticated connection cannot mutate match state.
10. Tick execution has one owner and cannot run concurrently or reentrantly.
11. Snapshots expose only approved client-visible state.
12. Malformed, oversized and out-of-range payloads are rejected before Core admission.

## 9. Implementation sequence

1. Foundation (current): identifiers, Vitality contract/primitive, Betrayal ledger contract/primitive and tests.
2. Domain state: Result types, player/match/session state and transition tables.
3. Rules: Debuff model, transfer eligibility, atomic transfer and tests.
4. Command boundary: intent DTOs, sender identity derivation, admission and replay protection.
5. Deterministic simulation: ordering ADR, tick ADR, queue and pipeline.
6. Snapshots: immutable projection and revision generation.
7. Unity project: create project, pin editor/packages, add asmdefs and test assemblies.
8. FishNet integration: only after package/API verification; thin server/client adapters.
9. Integration/security tests: host, dedicated server, disconnect/reconnect, malformed payloads, replay and packet behavior.
10. Playtest: at least 10 documented sessions before gameplay rules become approved.

Each slice requires a PR, tests, review and updated status register. Do not merge automatically.

## 10. Build and verification gates

### Standalone Core

With the .NET 8 SDK installed:

~~~bash
dotnet restore tests/CostOfStaying.Core.Tests/CostOfStaying.Core.Tests.csproj
dotnet build src/CostOfStaying.Core/CostOfStaying.Core.csproj --configuration Release
dotnet test tests/CostOfStaying.Core.Tests/CostOfStaying.Core.Tests.csproj --configuration Release --logger "trx;LogFileName=core-tests.trx"
~~~

### Unity

After a real Unity project and exact package lock exist, run EditMode and PlayMode tests in batchmode and retain XML results. Read Unity version from ProjectSettings/ProjectVersion.txt and FishNet version from resolved package metadata. Assumed versions are not verification evidence.

### Current Core verification evidence
The standalone Core build and NUnit suite passed in GitHub Actions. See docs/audit/PHASE1_CORE_CI_VERIFICATION.md for the exact commit, workflow run, job, test totals and retained TRX artifact. This verifies only the standalone .NET Core slice; it does not verify Unity, FishNet, networking, gameplay balance or production security.

### Evidence required for VERIFIED
- exact commit SHA;
- exact Unity/.NET/FishNet versions;
- clean build log;
- test result file;
- test count and pass/fail totals;
- reproducible command or CI workflow;
- no unexplained skipped tests.

## 11. Supersession and cleanup

- The uploaded 85k-line Markdown is a historical design transcript, not executable source.
- Do not delete the user's archive automatically.
- Do not import repeated declarations, competing implementations, malformed snippets or obsolete ADR numbering.
- This document is the maintained index for Phase 1 technical files.
- When a file becomes implemented, update this register and add its test in the same PR.
- When a design changes, update the canonical spec/ADR and tests together.

## 12. Exact current implementation status

**Lifecycle warning:** the CI-tested VitalityAccount implements the current repository v2.1 proposal. The earlier owner-approved Alive / Spirit / Eliminated matrix with automatic Spirit and explicit Resurrect / Eliminate remains in conflict; see ADR-007. Do not treat the Vitality API as final until the owner decision is reconciled.

| Item | Status |
|---|---|
| ADR-005 approval | APPROVED |
| Core .NET project | VERIFIED — CI Release build passed |
| Numeric identifiers and identifier value-semantics tests | VERIFIED — included in 38 passing tests |
| Vitality contract and primitive | VERIFIED — CI build and tests passed |
| Vitality terminal state aligned with v2.1 | VERIFIED against current v2.1 draft only; canonical gameplay lifecycle pending ADR-007 |
| Betrayal ledger contract and primitive | VERIFIED — CI build and tests passed |
| Identifier, Vitality and ledger NUnit tests | VERIFIED — 38 passed, 0 failed, 0 skipped |
| Unity project/package lock | NOT CREATED |
| FishNet version | ASSUMPTION ONLY |
| Network/session adapters | NOT IMPLEMENTED |
| Full Phase 1 inventory | NOT IMPLEMENTED |
| Gameplay baseline | PROPOSED FOR PLAYTEST |

**This document does not claim compilation, runtime integration, security verification, or completion of all Phase 1 files.**
