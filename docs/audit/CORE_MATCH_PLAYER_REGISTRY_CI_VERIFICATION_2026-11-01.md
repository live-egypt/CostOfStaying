# Core CI — Match-Scoped Player Registry

**Date:** 2026-11-01  
**Source commit:** `08b68e68a6cf393c6177b6b54ecd53d6ffff5496`  
**Workflow run:** `41330344616` — SUCCESS  
**Branch:** `feature/core-lifecycle-resolution`

## Verification

- Core dependency boundary: PASS — no UnityEngine/FishNet references in Core.
- CoreComposition construction-root guard: PASS — registry construction is centralized.
- .NET 8 restore: PASS.
- Core Release build: PASS.
- NUnit test project Release build: PASS.
- NUnit: **132 passed, 0 failed, 0 skipped**.
- TRX artifact: `13093370056`.
- Artifact SHA-256: `19eeb3b4395035c5b8cae9051a10b69bddd3d5c15db6a4fe248340e46e3f88b7`.

## Implemented registry contract

- `IMatchPlayerRegistry` and `MatchPlayerRegistry` are bound to a non-zero MatchId.
- Positive player capacity is explicit; Core chooses no default match size.
- ActorId must be valid and unique within the registry.
- Duplicate registration returns Conflict without replacing the existing account.
- Full capacity returns CapacityExceeded without mutation.
- Lookup distinguishes InvalidArgument from NotFound.
- Removal changes membership only; vitality/lifecycle state is unchanged.
- Removal releases one capacity slot.
- Player ID snapshots preserve registration order and are immutable copies.
- `CoreComposition.CreateMatchPlayerRegistry(matchId, capacity)` is the construction path guarded by CI.

## Policy boundary

ADR-009 remains PROPOSED. Production capacity, spectator/Spirit slot treatment, reconnect grace-period behavior, match-end retention and persistence remain owner/application policy. This run verifies deterministic registry mechanics only.

## Remaining Core scope

Session lifecycle, complete player/match/world aggregates, command payload validation/application handlers, movement simulation, Debuff eligibility and atomic transfer, snapshots, voting and match outcome resolution remain unimplemented.

Unity, FishNet, Android, authenticated transport mapping, Host/Dedicated Server, persistence and production security are not verified by this workflow.
