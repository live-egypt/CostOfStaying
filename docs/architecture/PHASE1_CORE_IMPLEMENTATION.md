# Phase 1 Core — Canonical Implementation Status

**Status:** PARTIALLY VERIFIED — latest Core CI passed boundary/composition checks, Release builds and 119/119 NUnit on commit `6bf2d61f56e37ea54f93cfa42d2dc72e4b2a78b4`; the broader Core scope remains incomplete.  
**Branch:** `feature/core-lifecycle-resolution`  
**Core target:** .NET Standard 2.1  
**Standalone tests:** .NET 8 / NUnit 3.14  
**Architecture ADR:** ADR-005 (APPROVED)  
**Identifier/ledger ADR:** ADR-006 (PROPOSED; approval status must be confirmed before freezing policy)  
**Player lifecycle ADR:** ADR-007 (APPROVED: Alive / Spirit / Eliminated)  
**Command ingress ADR:** ADR-008 (PROPOSED; policy owner review pending)  
**Unity/FishNet:** NOT IMPLEMENTED / NOT VERIFIED.

## Implemented Core source

- Engine-independent .NET Standard 2.1 class library.
- Strongly typed numeric identifiers: `ActorId`, `MatchId`, `SessionId`, `VictimId`, `EventSequenceId`, `CommandSequenceId` (all backed by `ulong`; zero is the invalid/default sentinel).
- Shared `Result` / `Result<T>` and stable `CoreErrorCode` categories.
- `IVitalityAccount` / `VitalityAccount` implementing ADR-007 lifecycle and bounded vitality.
- `IBetrayalLedger` / `BetrayalLedger` enforcing non-zero IDs, no self-target, monotonic event sequence, replay handling and actor score accumulation.
- `IMatchLifecycle` / `MatchLifecycle` deterministic match-phase state machine.
- `CoreComposition` as the production construction root for the match-scoped ledger and factories for player vitality, match lifecycle, sequence tracker and command queue.
- `ServerTick` and `StateRevision` value objects; zero is the initial value and revision advance refuses overflow.
- `CommandEnvelope<TCommand>` for server-resolved session identity, sequence, receive tick and command intent. It is not a client wire DTO.
- `CommandSequenceTracker` with strictly increasing per-session sequences, explicit maximum jump, explicit maximum tracked sessions, duplicate/stale rejection and bounded in-memory state.
- `CommandAdmissionQueue<TCommand>` with explicit bounded FIFO capacity, replay-aware enqueue and safe session reset only after that session's queued commands have drained.
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

1. Session lifecycle service and connection/authentication state transitions.
2. Full match aggregate, player registry, membership and world-state ownership.
3. Player aggregate and cross-system state coordination.
4. Command payload validation, application handlers and end-to-end authoritative admission pipeline.
5. Movement command model, validation, simulation and deterministic movement rules.
6. Debuff model, transfer eligibility and atomic transfer application/rollback.
7. Immutable authoritative snapshots and projection contracts.
8. Voting, endgame and match outcome resolution.
9. Persistence, reconnect recovery and backend integration.

Unity project metadata, Unity assemblies, FishNet adapters, authenticated transport mapping, Host/Dedicated Server integration, Unity EditMode/PlayMode tests, Android builds and device performance/security verification are also incomplete.

## Verification evidence

The latest Core CI run:
- Run: `41329893768` — SUCCESS.
- Source commit: `6bf2d61f56e37ea54f93cfa42d2dc72e4b2a78b4`.
- Core dependency boundary and composition-root checks: PASS.
- .NET 8 Release restore/build: PASS.
- NUnit: 119 passed, 0 failed, 0 skipped.
- TRX artifact: `13092503957`.
- Artifact SHA-256: `bd80302f06e057c041c9ed418dbf7f64a98f86a145edfa7864eabe541af31bf5`.
- Detailed evidence: [Bounded command queue and session replay capacity CI](../audit/CORE_COMMAND_QUEUE_CI_VERIFICATION_2026-11-01.md).

This run verifies the latest Core queue/capacity increment. It does not verify the unimplemented Core systems or Unity/FishNet.

## Next implementation order

1. Session lifecycle contract and service, separated from player lifecycle.
2. Match/player aggregate ownership and authoritative state revision integration.
3. Command payload validation and application handlers around the bounded queue.
4. Movement command/rules/simulation.
5. Debuff rules and atomic transfer transaction.
6. Immutable snapshots and deterministic match outcome rules.
7. Integration boundary design for Unity/FishNet after exact package/API verification.

Every increment requires an approved contract, focused NUnit coverage, Core dependency/composition checks and a successful CI run. Do not invent gameplay constants or Unity/FishNet API signatures.
