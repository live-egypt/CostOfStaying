# Phase 1 Core — Canonical Implementation Status

**Status:** PARTIALLY VERIFIED — historical baseline: 181/181 NUnit; PR #25 separately verifies 191 tests for in-flight command cleanup; PR #26 verifies 210 tests for P1.1 connection/session binding. Both PRs target `feature/core-lifecycle-resolution` and remain unmerged. Unity/FishNet and production authentication remain unverified.  
**Branch:** `feature/core-lifecycle-resolution`  
**Core target:** .NET Standard 2.1  
**Standalone tests:** .NET 10 / NUnit 3.14  
**Architecture ADR:** ADR-005 (APPROVED)  
**Identifier/ledger ADR:** ADR-006 (PROPOSED; approval status must be confirmed before freezing policy)  
**Player lifecycle ADR:** ADR-007 (APPROVED: Alive / Spirit / Eliminated)  
**Command ingress ADR:** ADR-008 (PROPOSED; policy owner review pending)
**Match membership ADR:** ADR-009 (PROPOSED; capacity/reconnect policy owner review pending)
**Session lifecycle ADR:** ADR-010 (PROPOSED; timeout/reconnect/multi-session policy owner review pending)  
**Session registry ADR:** ADR-011 (PROPOSED; capacity and multi-session policy owner review pending)  
**Authenticated admission ADR:** ADR-012 (PROPOSED; transport identity and gameplay authorization remain outside Core)  
**Session cleanup ADR:** ADR-013 (PROPOSED; in-flight handler completion remains an application responsibility)  
**Connection binding ADR:** ADR-016 (PROPOSED; P1.1 Core contract implemented and CI-verified on PR #26; production adapter and owner approval pending)  
**Unity/FishNet:** NOT IMPLEMENTED / NOT VERIFIED.

## Implemented Core source

- Engine-independent .NET Standard 2.1 class library.
- Strongly typed numeric identifiers: `ActorId`, `MatchId`, `SessionId`, `TransportConnectionId`, `VictimId`, `EventSequenceId`, `CommandSequenceId` (all backed by `ulong`; zero is the invalid/default sentinel). `TransportConnectionId` is introduced in PR #26 as a server-issued opaque transport identifier; the real adapter must allocate it and avoid reuse during a server-process lifetime.
- Shared `Result` / `Result<T>` and stable `CoreErrorCode` categories.
- `IVitalityAccount` / `VitalityAccount` implementing ADR-007 lifecycle and bounded vitality.
- `IBetrayalLedger` / `BetrayalLedger` enforcing non-zero IDs, no self-target, monotonic event sequence, replay handling and actor score accumulation.
- `IMatchLifecycle` / `MatchLifecycle` deterministic match-phase state machine.
- `ISessionLifecycle` / `SessionLifecycle` server-owned Pending → Authenticated → Disconnected → Closed state machine (ADR-010 PROPOSED).
- `ISessionRegistry` / `SessionRegistry` bounded by explicit capacity, keyed by SessionId, accepts Pending sessions; public removal is unavailable and cleanup service is required (ADR-011 PROPOSED).
- `IConnectionSessionRegistry` / `ConnectionSessionRegistry` in PR #26 provides bounded bidirectional mapping between a server-issued `TransportConnectionId` and one registered Pending/Authenticated session; a live session has at most one live connection. It deliberately does not enforce one session per ActorId.
- `ConnectionAwareSessionCleanupService` in PR #26 prevents removal while the same session registry still has a live connection binding; CoreComposition rejects mismatched registry instances.
- `IMatchPlayerRegistry` / `MatchPlayerRegistry` bounded per-match membership with deterministic insertion order, immutable ID snapshots and membership-only removal (ADR-009 PROPOSED).
- `CoreComposition` as the production construction root for the match-scoped ledger and factories for player vitality, match lifecycle, session registry, connection/session mapping, guarded cleanup, sequence tracker and command queue.
- `ServerTick` and `StateRevision` value objects; zero is the initial value and revision advance refuses overflow.
- `CommandEnvelope<TCommand>` for server-resolved session identity, sequence, receive tick and command intent. It is not a client wire DTO.
- `CommandSequenceTracker` with strictly increasing per-session sequences, explicit maximum jump, explicit maximum tracked sessions, duplicate/stale rejection and bounded in-memory state.
- `CommandAdmissionQueue<TCommand>` with explicit bounded FIFO capacity, replay-aware enqueue and internal replay reset restricted to session cleanup.
- `ICommandAdmissionService<TCommand>` / `CommandAdmissionService<TCommand>` validates envelope, registered session, Authenticated state and ActorId binding before admission; `TryTakeNext` revalidates the session and returns `AuthenticatedCommand<TCommand>` with server-resolved ActorId, discarding stale commands after disconnect/close.
- `ISessionCleanupService<TCommand>` / `SessionCleanupService<TCommand>` is the only public cleanup path; it requires Closed state, drained queue, replay-state reset, then internal registry removal.
- `ICommandAdmissionService<TCommand>` / `CommandAdmissionService<TCommand>` validates envelope and requires a registered Authenticated session with a valid ActorId before queue admission; gameplay payload rules remain downstream.
- `ISessionCleanupService<TCommand>` / `SessionCleanupService<TCommand>` drains pending queue state, resets replay state and removes a Closed session in one server-serialized cleanup path.
- NUnit coverage for the implemented primitives and invariants.

## Canonical player lifecycle — ADR-007

| Current state | Lose | Restore | Resurrect | Eliminate |
|---|---|---|---|---|
| Alive | Decrease; zero enters Spirit | Apply, capped at Maximum | Reject | Enter Eliminated; vitality becomes zero |
| Spirit | No-op | No-op | Explicit operation; positive vitality, capped at Maximum | Enter Eliminated |
| Eliminated | No-op | No-op | Reject | No-op / false |

### Lifecycle invariants

- New accounts begin Alive at Maximum; Maximum must be positive.
- Vitality reaching zero transitions Alive → Spirit.
- Restore never revives Spirit.
- Resurrection is explicit, Spirit-only and requires positive requested vitality.
- Elimination is explicit and terminal.
- Authorization, match phase, costs, cooldowns and other gameplay policy belong to the server application layer.
- Spirit abilities, connection/authentication state, match membership and spectator projection are separate systems.

## Command ingress — ADR-008 PROPOSED

- Trusted server ingress resolves the authenticated connection to `SessionId` and assigns `ServerTick`; clients do not author these values.
- `CommandSequenceId` is a non-zero `ulong` scoped to one authenticated session.
- `CommandSequenceTracker` accepts only strictly increasing sequences whose jump is within the configured positive maximum.
- The tracker has a configured positive maximum session count. New sessions beyond that bound receive `CapacityExceeded`; existing sessions can continue.
- `CommandAdmissionQueue<TCommand>` is FIFO and has an explicitly configured positive capacity.
- A full queue rejects before sequence consumption, allowing the same envelope to be retried after backpressure clears.
- Once admitted, the sequence is consumed even if later gameplay-rule validation rejects the command.
- Session replay state can be reset only after that session has been permanently removed and no queued commands remain for it.
- The tracker and queue are single-writer/server-serialized and are not thread-safe. They are in-memory, not durable anti-replay storage.
- Production limits, reconnect continuity, rate limiting and command timeout policy remain application configuration / owner decisions.

## Time and revision

- `ServerTick` is supplied by authoritative server code; Core does not read wall-clock time.
- Tick zero is valid.
- `StateRevision` zero represents initial state. `TryAdvance` increments by one and returns false at `ulong.MaxValue` without wrapping.

## Match phase state machine

Allowed transitions:
- Created → Waiting → Starting → Running
- Running → VoteOpen or Ending
- VoteOpen → Running or Ending
- Ending → Completed

Completed is terminal. Undefined enum values and all undocumented transitions fail without mutation. Player-count, timer, vote-result and match-end preconditions are application-service responsibilities.

## Betrayal ledger invariants

- ActorId, VictimId and EventSequenceId are typed ulong values; zero is invalid at domain boundaries.
- Actor and victim must differ.
- Default/invalid events are rejected before mutation.
- New accepted events require strictly increasing EventSequenceId; gaps are allowed.
- Exact replay is rejected; reusing an accepted sequence with conflicting data is an invariant violation.
- ServerTick is metadata, not the ordering key.
- Actor score increments exactly once per accepted event.
- The application layer must append only after a successful authoritative transfer. The ledger does not itself validate transfer eligibility or mutate Debuff state.
- Single-writer/server-serialized; no persistence or thread-safety guarantee.

## Explicitly incomplete Core scope

The following remain unimplemented and must not be represented as complete:

1. Credential validation, external account identity, production transport-adapter hooks, connection-ID allocation/reuse enforcement in that adapter, reconnect/timeout orchestration and persistence remain unimplemented. The pure-Core connection/session mapping contract is implemented and verified in PR #26 but is not merged. PR #25 separately tracks dequeued/in-flight commands for safe cleanup; it is also unmerged and is not part of PR #26's base branch.
2. Full match aggregate, world-state ownership and cross-aggregate coordination (bounded match membership registry is implemented).
3. Player aggregate and cross-system state coordination.
4. Gameplay payload validation, application handlers and end-to-end authoritative execution pipeline (the authenticated session admission gate is implemented).
5. Movement command model, validation, simulation and deterministic movement rules.
6. Debuff model, transfer eligibility and atomic transfer application/rollback.
7. Immutable authoritative snapshots and projection contracts.
8. Voting, endgame and match outcome resolution.
9. Persistence, reconnect recovery and backend integration.

Unity project metadata, Unity assemblies, FishNet adapters, authenticated transport mapping, Host/Dedicated Server integration, Unity EditMode/PlayMode tests, Android builds and device performance/security verification are also incomplete.

## Verification evidence

Core verification is cumulative across separate, unmerged review branches; do not treat their source trees as one integrated build:

- Consolidated baseline: run `41333575156` on `8a193190fb5f2e10c05773fb0789171011bfd2eb`; 181 passed, 0 failed, 0 skipped.
- PR #25 in-flight command cleanup: run `43596902378` on `7d82c3becf96eac338999de767770c75bc5795eb`; 191 passed, 0 failed, 0 skipped. PR #25 is unmerged.
- PR #26 P1.1 connection/session binding: run `43599420409` on tested source commit `95a7bc93a7fd4772f45e7dc428f7574906945061`; 210 passed, 0 failed, 0 skipped. Dependency-boundary and CoreComposition scans passed; Core and test Release builds had 0 warnings and 0 errors. Subsequent commits after the tested source SHA updated documentation only; no source, test or workflow files changed after verification. See [P1.1 verification report](../audit/P1_CONNECTION_SESSION_BINDING_CI_VERIFICATION_2026-11-14.md).

These checks verify separate pure-C# Core review branches only. They do not verify their combined merge result, Unity/FishNet, credential validation, actual connection-ID allocation by a transport adapter, reconnect policy or production security.

## Next implementation order

1. P1.1 pure-Core connection/session mapping is implemented and CI-verified on PR #26 (not merged). Next: trusted transport adapter hooks and credential validation; separately review PR #25 for in-flight completion before integrating both increments.
2. Match/player aggregate ownership and authoritative state revision integration.
3. Command payload validation and application handlers around the bounded queue.
4. Movement command/rules/simulation.
5. Debuff rules and atomic transfer transaction.
6. Immutable snapshots and deterministic match outcome rules.
7. Integration boundary design for Unity/FishNet after exact package/API verification.

Every increment requires an approved contract, focused NUnit coverage, Core dependency/composition checks and a successful CI run. Do not invent gameplay constants or Unity/FishNet API signatures.
