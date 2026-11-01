# Phase 1 Core — Implemented Contracts and Primitives

**Status:** PARTIALLY VERIFIED — ADR-007 lifecycle source passed Core CI (45/45 NUnit) on commit `48ee4d47d5f63b89b3210b59e8a289dd4ee8ba05`; remaining Core scope is not implemented.  
**Architecture ADR:** ADR-005 (APPROVED)  
**Lifecycle ADR:** ADR-007 (APPROVED: Alive / Spirit / Eliminated)  
**Identifier/ledger ADR:** ADR-006 (PROPOSED — do not treat as owner-approved)  
**Core target:** .NET Standard 2.1  
**Standalone tests:** .NET 8 / NUnit 3.14  
**Unity/FishNet:** NOT IMPLEMENTED / NOT VERIFIED.

## Implemented source

- Engine-independent .NET Standard 2.1 Core project.
- Strongly typed numeric wrappers: `ActorId`, `VictimId`, `EventSequenceId` (ulong).
- `IVitalityAccount` and `IBetrayalLedger` contracts.
- `VitalityAccount` with bounded vitality and canonical `Alive / Spirit / Eliminated` lifecycle.
- `BetrayalLedger` with monotonic event sequence, exact replay handling and actor score accumulation.
- NUnit tests for identifier value semantics, lifecycle transitions and ledger invariants.
- GitHub Actions Core CI workflow.

## Canonical lifecycle (ADR-007)

| State | Lose | Restore | Resurrect | Eliminate |
|---|---|---|---|---|
| Alive | Decrease; zero enters Spirit | Apply, capped at Maximum | Reject | Enter Eliminated; vitality becomes zero |
| Spirit | No-op | No-op | Explicit operation; positive requested vitality, capped at Maximum | Enter Eliminated |
| Eliminated | No-op | No-op | Reject | No-op / false |

### Contract details

- New accounts begin Alive at Maximum; Maximum must be positive.
- Vitality reaching zero automatically transitions Alive → Spirit.
- Restore never revives Spirit.
- TryResurrect succeeds only from Spirit and with a positive requested amount.
- TryEliminate succeeds from Alive or Spirit and is terminal.
- Invalid or repeated Try operations return false without partial mutation.
- Core does not authenticate or authorize the caller. The server application layer must validate permissions, match phase, costs, cooldowns and other policy before calling explicit lifecycle operations.
- Spirit abilities, connection/authentication state, match membership and spectator projection are separate concerns.
- No resurrection cost/cooldown or other unapproved gameplay constant is embedded in Core.

## Betrayal ledger semantics

- ActorId, VictimId and EventSequenceId are numeric ulong wrappers.
- Zero is representable as an invalid/default sentinel; domain boundaries reject invalid identities.
- BetrayalEvent requires non-zero sequence, actor and victim IDs; actor and victim must differ.
- The ledger validates default structs before mutation.
- New events require strictly increasing EventSequenceId; gaps are allowed.
- Exact replay returns false, including after newer accepted events.
- Reusing a sequence ID with different data throws InvalidOperationException.
- ServerTick is metadata, not the ledger ordering key.
- Actor score increments exactly once per accepted event.
- The application layer must append only after a successful authoritative transfer. The ledger does not validate eligibility or perform Debuff transfer.
- Single-writer/server-serialized; no thread safety, persistence or network dependency is promised.

## Deliberate exclusions / remaining Core scope

The following are not yet implemented and must not be represented as complete:
- Result / Result<T> error contracts.
- SessionId / MatchId and session/match lifecycle.
- CoreComposition as the single service-composition root.
- Player aggregate and match/world state ownership.
- ServerTick / StateRevision.
- CommandEnvelope, command validation/admission, sequence tracking and bounded queues.
- Movement command/rules/simulation.
- Debuff model, eligibility and atomic transfer application.
- Snapshot/projection contracts.
- Match endgame voting and outcome resolution.
- Persistence and backend integration.

Unity project metadata, Unity assemblies, FishNet adapters, authenticated connection mapping, Host/Dedicated Server integration, and Unity EditMode/PlayMode tests are also not implemented.

## Verification evidence

- **Tested source commit:** `48ee4d47d5f63b89b3210b59e8a289dd4ee8ba05`
- **Core CI run:** `41325988534` — SUCCESS
- **Release restore/build:** PASS
- **NUnit:** 45 passed, 0 failed, 0 skipped
- **TRX artifact:** `core-test-results`, ID `13091132468`
- **Artifact SHA-256:** `f45d6d749463f2401906659e8c9f534977a392e2df3b0107be270186529ba783`
- **Detailed evidence:** [ADR-007 lifecycle CI verification](../audit/ADR007_LIFECYCLE_CI_VERIFICATION_2026-11-01.md)

The lifecycle implementation is VERIFIED for the standalone Core scope at that exact source commit. Subsequent commits to this branch have changed documentation only. Unity/FishNet, Android, performance and production security remain outside this standalone Core CI scope.

## Next implementation order

1. Common Result / Result<T> and stable error codes.
2. MatchId / SessionId and explicit session/match lifecycle contracts.
3. CoreComposition as the sole service creation root.
4. Player/match state ownership and monotonic tick/revision values.
5. CommandEnvelope, admission, replay tracking and bounded processing.
6. Movement simulation.
7. Debuff rules and atomic transfer transaction.
8. Immutable snapshots and deterministic match outcome rules.

Each item requires an approved contract, focused NUnit coverage, Core purity checks, and CI evidence. Do not invent gameplay values or network API signatures.
