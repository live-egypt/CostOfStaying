# ADR-007 — Canonical Player Lifecycle and Vitality Semantics

**Status:** APPROVED — earlier owner-approved Phase 1 decision reaffirmed 2026-11-01  
**Decision recorded:** 2026-11-01  
**Supersedes:** the conflicting player-lifecycle statements in gameplay proposal v2.1 only  
**Related:** ADR-004, ADR-005, ADR-006, `docs/02_GAME_RULES_SPECIFICATION.md`

## Context

The project history contains an explicit owner-approved Phase 1 decision dated 2026-10-31 12:28 UTC:

- Player lifecycle states are `Alive`, `Spirit`, and `Eliminated`.
- Vitality reaching zero automatically transitions `Alive → Spirit`.
- `Resurrect` and `Eliminate` are explicit server-authoritative operations.
- Numeric identifiers use `ulong`.
- The server owns event ordering; Core does not promise thread safety.
- NUnit tests are required.

The later v2.1 proposal and the initial executable `VitalityAccount` used `Alive → Dead`, with no revival. That implementation contradicted the earlier owner-approved lifecycle and did not revoke it. The Core-boundary approval in ADR-005 did not authorize changing gameplay lifecycle semantics.

## Decision

**Option A is canonical.** The earlier owner-approved `Alive / Spirit / Eliminated` lifecycle is reaffirmed. The v2.1 `Alive → Dead` lifecycle is not the canonical player lifecycle. v2.1 remains a proposed gameplay baseline for all rules that do not contradict this ADR.

### Canonical transition matrix

| Current state | Operation | Preconditions | Next state | Vitality result |
|---|---|---|---|---|
| Alive | Lose(amount > 0), result > 0 | Valid account | Alive | Decreases, clamped at zero |
| Alive | Lose(amount > 0), result = 0 | Valid account | Spirit | Exactly zero |
| Alive | Lose(0) | Any | Alive | Unchanged |
| Alive | Restore(amount > 0) | Alive only | Alive | Increases, capped at Maximum |
| Spirit | Lose / Restore | Any | Spirit | Unchanged; zero |
| Spirit | Resurrect(amount > 0) | Explicit server-authorized operation | Alive | Set to min(amount, Maximum) |
| Alive or Spirit | Eliminate | Explicit server-authorized operation | Eliminated | Set to zero |
| Eliminated | Any lifecycle/vitality mutation | Any | Eliminated | Zero; terminal |

### Domain/API semantics

1. `PlayerLifecycleState` is the single lifecycle enum: `Alive`, `Spirit`, `Eliminated`. Do not retain a competing `Dead` state.
2. Vitality is bounded by `0..Maximum`; Maximum must be positive. The initial value is Maximum.
3. Reaching zero through `Lose` automatically changes `Alive` to `Spirit`.
4. `Restore` is not resurrection. It affects only an Alive player.
5. Resurrection is explicit. It succeeds only from Spirit and requires a positive requested vitality amount; the resulting amount is capped at Maximum. It is not triggered by Restore.
6. Elimination is explicit, valid from Alive or Spirit, sets vitality to zero and is terminal.
7. Repeating a transition that is no longer valid returns false from the corresponding `Try...` operation; it does not mutate state.
8. The Core primitive validates domain state and values, not user permissions. Only the server application layer may invoke Resurrect or Eliminate after checking authorization, match phase, gameplay costs/cooldowns and other policy.
9. No resurrection cost, cooldown, actor permission matrix, Spirit abilities, or balance constants are invented here. Those remain application/gameplay policy inputs until approved by a separate rule decision.
10. Core remains single-writer/server-serialized; thread safety and persistence are not promised.

### Separation of concerns

- `PlayerLifecycleState` describes participation lifecycle.
- Vitality is a numeric resource owned by the account.
- Connection/authentication state is separate and must not be inferred from lifecycle.
- Match membership and spectator/presentation projections are separate.
- Spirit abilities and scoring are outside this primitive.

## Consequences

- The initial `Alive → Dead` implementation is replaced by the approved `Alive → Spirit → Alive/Eliminated` lifecycle.
- A player reaching zero remains a Spirit, not a Dead player.
- Spirit cannot be revived by ordinary Restore; only the explicit Resurrect operation can do so.
- Eliminated is terminal.
- v2.1 RULE-01 and RULE-04 must be reconciled with this ADR before they are described as an internally consistent final gameplay specification.
- All lifecycle tests must cover every matrix row, rejected operation and no-partial-mutation case.

## Verification gate

The lifecycle implementation is considered verified only when the Core Release build and all NUnit tests pass on the commit containing this ADR and implementation. This does not verify Unity/FishNet integration or gameplay balance.
