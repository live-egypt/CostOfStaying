# ADR-009 — Match-Scoped Player Registry

**Status:** PROPOSED — implementation and NUnit coverage exist; owner review required before freezing membership policy  
**Date:** 2026-11-01  
**Related:** ADR-005, ADR-006, ADR-007, `IMatchPlayerRegistry`, `MatchPlayerRegistry`

## Context

The Core needs one deterministic, bounded owner for match membership. Player vitality/lifecycle state is separate from whether an actor is currently registered in a match. A player leaving a match must not implicitly change the player's vitality or lifecycle state.

## Proposed contract

1. Each registry is bound to one non-zero MatchId.
2. Capacity is a positive explicit constructor parameter. Core selects no default maximum player count.
3. Each registered actor is identified by a non-zero ActorId and has one IVitalityAccount reference in this registry.
4. Duplicate ActorId registration returns Conflict and never replaces the existing account.
5. Registration beyond capacity returns CapacityExceeded without mutation.
6. Null accounts or invalid ActorIds return InvalidArgument.
7. Get returns NotFound for an actor that is not registered and InvalidArgument for an invalid ActorId.
8. Remove affects membership only; it does not mutate vitality, lifecycle, score or any external player state.
9. Removing a registered actor releases one capacity slot. Removing an unknown actor returns NotFound.
10. Player ID snapshots preserve registration order and are immutable copies.
11. The registry is in-memory, single-writer/server-serialized and not thread-safe.
12. Cross-match uniqueness, account ownership, reconnect policy and persistence are application/backend responsibilities.

## Implementation

- `IMatchPlayerRegistry` and `MatchPlayerRegistry`.
- `CoreComposition.CreateMatchPlayerRegistry(matchId, capacity)`.
- Bounded dictionary plus insertion-order list.
- NUnit tests for constructor validation, null/duplicate/capacity checks, lookup, removal, lifecycle isolation, capacity release, immutable snapshots and match isolation.

## Open policy items

- Production maximum players per match.
- Whether spectators/Spirits occupy a player slot or a separate roster.
- Whether a disconnected player remains registered during a reconnect grace period.
- Whether match completion retains or clears the registry.
- Persistence/recovery and cross-match account ownership.

## Verification boundary

CI verifies deterministic in-memory registry behavior. It does not approve production capacity or reconnect/spectator policy and does not verify transport authentication.
