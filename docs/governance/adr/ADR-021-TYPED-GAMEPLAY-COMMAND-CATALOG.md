# ADR-021 — Typed Gameplay Command Catalog and Wire Intent Boundary

**Status:** PROPOSED — command families and trust boundaries catalogued; no concrete DTO schema, wire format or field limits approved  
**Date:** 2026-11-14  
**Depends on:** ADR-002, ADR-008, ADR-015, ADR-017, ADR-018, ADR-019, ADR-020  
**Related implementation:** `CommandEnvelope<TCommand>`, `AuthenticatedCommand<TCommand>`, `MatchCommandPayloadValidationService<TCommand>`

## Context

The current Core accepts a generic `CommandEnvelope<TCommand>`, validates an explicitly supplied typed payload, verifies the authenticated session and match membership, and evaluates an application-supplied authorization policy. It still has no concrete gameplay command DTOs, command-kind catalog, versioned wire schema, ingress router or gameplay application handlers.

The product documents describe Movement, Combat, Suffering Transfer, Collective Surrender and Spirit/Echo intents. Their status is PROPOSED or PROPOSED FOR PLAYTEST. They do not yet provide complete serialization schemas or approved field/range limits. Therefore, this ADR catalogues candidate command families and trust boundaries but does not turn draft rules into approved wire contracts.

## Proposed trust boundary

1. The transport adapter enforces raw frame-size, serializer/decompression, nesting/depth, collection-count and string-length quotas before or during deserialization.
2. The transport adapter deserializes a versioned wire intent into a typed candidate DTO. Exact serializer, wire encoding, schema versioning, compatibility and numeric limits are not selected here.
3. The adapter maps the untrusted wire intent to a typed Core/application command. It must not copy client claims of ActorId, SessionId, MatchId, ServerTick, StateRevision, Vitality, damage, ammo, transfer result, vote result, cooldown, score or match outcome into authoritative fields.
4. Trusted ingress resolves the transport connection to its Pending/Authenticated session, and derives ActorId from the server-owned session. MatchId is selected by server routing and checked against roster membership.
5. The command pipeline applies the generic typed-payload validator, authenticated session/match membership admission and explicit match authorization policy before handing the command to a typed application handler.
6. The application handler performs current-state checks and any atomic gameplay transaction. Only the server may mutate authoritative state and publish outcomes.
7. `CommandSequenceId` remains a per-session transport ordering/replay mechanism. A logical `CommandId` under ADR-015 is a separate proposed idempotency key; neither is interchangeable with the other.
8. Server receive tick, ActorId, SessionId and MatchId are derived by trusted server code and belong in the server-side context/envelope, not in client-authoritative DTO fields.
9. Concrete DTOs should be immutable and bounded. The generic payload validation service revalidates after dequeue, but immutability remains the preferred contract.
10. Payload validation checks static shape and declared static ranges only. Dynamic phase, lifecycle, ownership, distance, cooldown, target eligibility, capacity and other current-state conditions remain authorization/handler checks.
11. No successful payload validation or command admission alone is permission to mutate gameplay state.

## Candidate command catalog

All entries below are candidate intent categories only. The word “candidate” is deliberate: no field list or wire schema in this table is approved.

| Candidate command family | Client may express | Must remain server-authoritative | Source / status |
|---|---|---|---|
| Movement intent | Direction/input intent and any minimal client input flags later approved | Actor identity, authoritative position/velocity, movement speed, collision, movement result, server tick and movement validation | Technical Master movement candidate; PROPOSED |
| Attack intent | Attack action intent and candidate aim/target reference, subject to later schema review | Weapon ownership, ammo, fire rate/cooldown, hit result, hitbox/line-of-sight decision, damage, Vitality and effects | `COMBAT_SYSTEM.md`; PROPOSED |
| Suffering Transfer intent | Candidate target reference and any approved request/idempotency reference | Source ActorId, match membership, source/target lifecycle, Debuff selection/ownership, distance, cooldown, capacity, atomic Move, Betrayal update and result | `SUFFERING_TRANSFER_SYSTEM.md`, RULE-02; PROPOSED FOR PLAYTEST |
| Collective Surrender vote intent | Candidate Yes/No choice only, if the vote rule is approved | Voter identity, eligibility, vote window, deadline, quorum/unanimity, disconnect/timeout semantics and match-ending result | `ENDGAME_SYSTEM.md`, RULE-03; PROPOSED FOR PLAYTEST |
| Spirit/Echo intent | Candidate fragment interaction or public-hint request, if those actions are approved | Spirit identity/state, fragment existence and collection, one-time scoring, cooldown, hint audience/content and all resulting state | `SPIRIT_SYSTEM.md`, RULE-04; PROPOSED FOR PLAYTEST |
| Resurrect / Eliminate | No client-authored authoritative transition or result | Explicit server-side operation, authorization, cost/conditions/cooldown for Resurrect, elimination authority, lifecycle transition and audit | ADR-007 APPROVED lifecycle primitive; operation policy TBD |

## Prohibited client-authoritative claims

A client DTO must not represent any of the following as an authoritative fact:
- ActorId/PlayerId, SessionId, authenticated status or account identity;
- MatchId membership or server-side recipient/ownership;
- server receive time, ServerTick or StateRevision;
- authoritative position, velocity, speed, health/Vitality, damage, ammo, weapon ownership or hit result;
- Debuff ownership/state, transfer success, Betrayal score or cooldown completion;
- vote tally, voter eligibility, vote success or match outcome;
- Spirit score, Echo collection success, cooldown completion or environmental effect result;
- Resurrect/Eliminate success or any terminal lifecycle state.

An intent may contain a candidate reference or requested action; the server resolves and validates it against current authoritative state.

## DTO design requirements before implementation

Each concrete DTO must have a reviewed schema record containing:
1. Stable command name and immutable command type.
2. Intended actor and match context source (must be derived server-side, not accepted from client).
3. Exact client-controlled fields and the semantic meaning of each field.
4. Prohibited/derived fields and explicit rejection behavior for attempts to supply them.
5. Wire schema version and compatibility/migration policy.
6. Primitive types, units, coordinate space and numeric bounds.
7. Maximum frame/payload size, nesting/depth, collection counts and string limits.
8. Required, optional and mutually exclusive fields.
9. Static payload validator rules with boundary-value tests.
10. Dynamic authorization/precondition rules and the application handler responsible for them.
11. Ordering, `CommandSequenceId`, logical `CommandId`/idempotency relationship and retry behavior.
12. No-partial-mutation invariants and result/error projection.
13. Positive, negative, fuzz/property and replay tests, with CI evidence.

No field limit, unit, enum value or schema version may be invented solely to make a DTO compile. For each command, use the approved system specification and an owner-reviewed decision.

## Implementation order

1. Approve the command family inventory and scope for MVP.
2. Select/verify the actual transport serialization format and raw frame quotas in the adapter; do not add transport dependencies to Core.
3. Approve each DTO's fields, types, units, static limits and versioning policy.
4. Implement one immutable DTO at a time with its concrete `ICommandPayloadValidator<TCommand>`.
5. Add a typed server ingress/router that constructs `CommandEnvelope<TCommand>` from trusted connection/session resolution and server tick.
6. Route through payload validation → authenticated/match membership → explicit authorization policy → typed application handler.
7. Implement handlers separately, with no-partial-mutation and atomicity tests before changing player/match state.
8. Keep unapproved rule values and permissions out of default Core policies; no allow-all application policy is shipped as production configuration.
9. Add raw transport parser/serializer abuse tests only when the actual transport and serializer versions are selected.

## Explicit non-decisions

This ADR does not select:
- wire encoding, serializer, schema version or package;
- exact DTO class names, property names, enum values or numeric limits;
- movement axes/speed/units, attack target encoding or hit validation;
- Transfer RequestId/CommandId mapping, Debuff selection details or distance/cooldown constants;
- vote quorum/window semantics, Spirit/Echo behavior or Resurrection costs;
- command rate limits, payload/frame size budgets, per-player command budgets or rate-limit actions;
- gameplay phase/lifecycle permissions or any concrete authorization policy;
- persistence or durable idempotency.

These decisions require owner review, the actual transport prototype and approved gameplay rules.

## Verification boundary

ADR-020's generic payload-validation gate is implemented and CI-verified in PR #31, but no concrete command DTO or wire schema is implemented by that gate. Passing its tests does not verify network parsing, raw frame limits, gameplay authorization or handler behavior.

ADR-021 remains **PROPOSED** until the command catalog and DTO schemas are reviewed. ADR-004 remains **PROPOSED FOR PLAYTEST**; no draft gameplay rule is promoted by this catalogue.
