# ADR-007 — Player Lifecycle and Vitality Zero-Transition Conflict

**Status:** DECISION REQUIRED — proposed options; no replacement decision approved  
**Date:** 2026-11-01  
**Related:** ADR-004, ADR-005, docs/02_GAME_RULES_SPECIFICATION.md, docs/architecture/PHASE1_TECHNICAL_MASTER.md

## Context

The project history contains an explicit owner-approved Phase 1 lifecycle decision dated 2026-10-31 12:28 UTC:

- Player-state matrix: Alive / Spirit / Eliminated.
- Vitality reaching zero automatically transitions the player to Spirit.
- Resurrect and Eliminate are explicit operations.
- Numeric IDs use ulong.
- The server owns event ordering; Core does not promise thread safety.
- NUnit tests are required.

The current repository v2.1 gameplay proposal and the Phase 1 implementation branch describe a different lifecycle:

- Vitality reaching zero transitions Alive → Dead.
- MVP has no Dead → Alive revival operation.
- Spirit is a separate post-death role/projection, not a player vitality/lifecycle state.

These contracts cannot both be the canonical player lifecycle. The current VitalityAccount and its NUnit tests implement the second model. ADR-005 approves the pure-Core boundary and first-slice approach, but the project history does not establish that the earlier explicit lifecycle approval was deliberately revoked.

## Decision required

The owner must choose which lifecycle contract is canonical. Until then, the current VitalityAccount behavior is code-verified but gameplay-provisional.

### Option A — Preserve the earlier owner-approved matrix

- Player lifecycle states are Alive, Spirit and Eliminated.
- Reaching zero Vitality automatically transitions Alive → Spirit.
- Resurrect and Eliminate are explicit server-authoritative operations.
- The contract must define who may resurrect, under what conditions, whether Vitality is restored, costs/cooldowns, and how Spirit can be eliminated.
- Vitality amount/state and player participation state must be modelled separately where necessary; do not overload one enum without a transition table.

### Option B — Adopt the current v2.1 proposal

- Vitality reaching zero transitions Alive → Dead.
- Dead is terminal for the MVP; no resurrection operation exists.
- Spirit is a separate post-death role/projection and does not participate as an Alive/Spirit/Eliminated state in the Vitality account.
- Any previous Resurrect/Eliminate operations are explicitly withdrawn from the MVP scope.

## Required acceptance criteria

Before changing the Core lifecycle API:

1. Record the selected option as APPROVED in this ADR.
2. Publish one canonical player lifecycle transition matrix.
3. Define the relationship between VitalityState, ParticipationState, SpiritRole, MatchMembership and ConnectionState.
4. Define the exact zero-Vitality transition and whether the transition is automatic.
5. Define Resurrect and Eliminate command permissions, preconditions, effects, idempotency and failure behavior if Option A is selected.
6. Update docs/02_GAME_RULES_SPECIFICATION.md, the system documents, ADR-004 and the technical master consistently.
7. Update IVitalityAccount and related Core types only after the contract is approved.
8. Add tests for every transition and rejected transition, including no partial mutation.
9. Re-run standalone Core CI and retain the new commit/test evidence.

## Current implementation and verification

- VitalityAccount currently implements Option B.
- Its build and 38 NUnit tests passed in GitHub Actions run 41255553487 for commit e86880a318df49164aa61fe5e43c7a47f9c44560.
- That PASS is limited to the implementation currently in source. It does not approve Option B or supersede the earlier owner-approved Option A.
- No lifecycle migration or source rewrite is authorized by this proposed ADR.

## Recommendation for governance

Until the owner explicitly approves a replacement, preserve the earlier explicit owner-approved matrix as the controlling gameplay decision. Treat the current Option B implementation as provisional and do not merge it as the final lifecycle implementation.
