# ADR-020 — Typed Command Payload Validation Boundary

**Status:** PROPOSED — generic pure-Core validation gate and tests implemented on PR #31 review branch; concrete command DTOs and domain rules remain unimplemented  
**Date:** 2026-11-14  
**Related:** ADR-002, ADR-008, ADR-012, ADR-018, ADR-019, `CommandEnvelope<TCommand>`, `ICommandPayloadValidator<TCommand>`

## Context

Core currently provides a generic server-side `CommandEnvelope<TCommand>`, sequence/replay tracking, authenticated-session admission, match-roster membership validation and a generic authorization policy gate. It does not yet define concrete immutable gameplay command DTOs such as movement, attack, Suffering Transfer, vote or Spirit intents.

The game-rule and system documents propose these command concepts, but important constraints and balance values remain PROPOSED or PROPOSED FOR PLAYTEST. A generic queue must not silently become a permissive parser or embed incomplete gameplay rules.

Payload validation is distinct from:
- raw transport frame limits and serializer/decompression quotas;
- authentication and session/match membership;
- authorization by match phase and player lifecycle;
- command-specific application preconditions and atomic gameplay mutations.

## Proposed boundary

1. The trusted transport adapter enforces maximum wire-frame size, nesting/depth, collection/string limits, serializer quotas and safe deserialization before constructing a typed `TCommand`.
2. The adapter maps client intent into a typed command payload. It must not accept client-authoritative ActorId, SessionId, MatchId, server tick, state revision, vitality, damage result, transfer outcome, vote result or other server-owned state.
3. The application supplies an explicit `ICommandPayloadValidator<TCommand>`; there is no permissive/default validator.
4. The validator is bounded, synchronous, deterministic from the payload, side-effect-free and free of I/O. It validates only payload structure, allowed primitive ranges, enum validity, required fields and internal shape invariants.
5. Dynamic checks that depend on SessionId, ActorId, MatchId, MatchPhase, PlayerLifecycleState, current server tick, ownership, cooldown, distance, capacity or target state belong to the relevant authorization policy or application handler, not the generic payload validator.
6. The match-scoped payload validation service evaluates the payload validator before calling the authenticated/match/authorization admission gate. A rejected payload does not enter the queue and does not consume its sequence.
7. A successful payload validation is followed by the existing match authorization gate, which independently resolves authenticated session, membership and policy.
8. Immediately after dequeue and before returning the command to the application caller, payload validation is repeated. This detects changes to mutable reference payloads while queued. Command DTOs should nevertheless be immutable.
9. If post-dequeue validation fails, the command is discarded and not returned to the gameplay handler. The sequence remains consumed because the command was previously admitted.
10. A default/uninitialized validator Result or an exception fails closed. Validator exceptions return Unavailable; they do not enqueue or consume sequence before admission.
11. Payload validation must not mutate match/player/queue state, reserve resources, consume cooldowns, add Betrayal, append event history or perform gameplay effects.
12. A payload validator is a construction dependency. The service cannot be composed without one.
13. Core remains single-writer/server-serialized. It does not provide cross-thread mutation protection.

## Concrete command taxonomy — not yet implemented

The current documents describe candidate intents, not verified DTO contracts:

| Candidate intent | Design source | Current status | Remaining DTO/validation work |
|---|---|---|---|
| Movement intent | Phase 1 technical master / movement candidate | PROPOSED | Vector representation, units, bounds, tick/sequence mapping, movement authority and anti-teleport limits |
| Attack intent | COMBAT_SYSTEM.md | PROPOSED | Weapon enum/catalog version, target reference, aim data, allowed fields, frame size and rate constraints |
| Suffering Transfer intent | ADR-004 / SUFFERING_TRANSFER_SYSTEM.md RULE-02 | PROPOSED FOR PLAYTEST | Target identity format, debuff selector, request identity, maximum serialized size; range/cooldown/capacity remain dynamic server checks |
| Collective Surrender vote | ADR-004 / ENDGAME_SYSTEM.md RULE-03 | PROPOSED FOR PLAYTEST | Vote choice enum and request identity; quorum/timing/eligible voters remain application policy |
| Spirit hint/Echo intent | ADR-004 / SPIRIT_SYSTEM.md RULE-04 | PROPOSED FOR PLAYTEST | Intent DTOs and bounded fragment/hint selectors; exact limits and cooldown remain TBD |
| Resurrect / Eliminate | ADR-007 | Explicit server-authoritative operations | Must not be accepted as client-authored lifecycle results; any client request is only intent and needs separate authorization |

No DTO, field name, numeric range, serialization schema or wire version is approved by this table.

## Rejection and mutation invariants

- Default/invalid envelope: InvalidArgument; validator is not invoked.
- Invalid payload: propagate a failure; queue capacity and sequence remain unchanged.
- Validator exception: Unavailable; queue capacity and sequence remain unchanged.
- Uninitialized/default Result: InvalidState; queue capacity and sequence remain unchanged.
- Valid payload but unauthenticated session/non-member/policy denied: existing gates reject; no sequence is consumed before queue admission.
- Payload mutated while queued and now invalid: discard after dequeue; do not return to handler; sequence remains consumed.
- Successful validation alone never grants authorization or permits gameplay mutation.
- No rejected command may mutate match/player state.

## Implementation and tests

- `ICommandPayloadValidator<TCommand>` defines the application-supplied validator contract.
- `IMatchCommandPayloadValidationService<TCommand>` and `MatchCommandPayloadValidationService<TCommand>` validate before queue admission and after dequeue.
- `CoreComposition.CreateMatchCommandPayloadValidationService(...)` requires explicit authorization and validator dependencies.
- NUnit coverage includes invalid envelope without validator call, rejected payload without sequence consumption, same-sequence retry after validation succeeds, uninitialized result, validator exception, mutable payload invalidation while queued and successful validated handoff.

## Verification boundary

A successful Core CI run verifies this generic payload-validation wrapper and tests only. It does not verify any actual Movement/Attack/Transfer/Vote/Spirit DTO, wire serializer, transport frame limits, gameplay rules or application handler. ADR-020 remains PROPOSED until the command catalog, DTO schemas, size/range limits and application handler contracts are reviewed and tied to approved rules.
