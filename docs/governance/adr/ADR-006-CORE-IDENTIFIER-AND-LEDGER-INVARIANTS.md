# ADR-006 — Core Identifier and Betrayal Ledger Invariants

**Status:** PROPOSED — requires project-owner approval before merge  
**Date:** 2026-11-01  
**Related:** ADR-002, ADR-005, docs/02_GAME_RULES_SPECIFICATION.md

## Context

The first executable Core slice uses strongly typed numeric identifiers (ulong). Underlying value types can be default-constructed, and their public constructors permit zero. Without explicit domain-boundary validation, a default identifier or a self-targeted betrayal could enter the ledger even though neither represents a valid player action.

The ledger also needs a precise distinction between event ordering and server simulation time. The Phase 1 contract uses a server-owned event sequence as its ordering key; ServerTick is event metadata and may be shared by multiple accepted events.

## Proposed decision

1. ActorId, VictimId, and EventSequenceId use ulong.
2. Numeric zero is reserved as an invalid/default sentinel. A value type may represent zero, but a domain object requiring a valid identity must reject it.
3. A valid BetrayalEvent requires a non-zero sequence ID, non-zero actor ID, non-zero victim ID, and different actor/victim numeric values.
4. The ledger rejects a default/invalid event before any mutation.
5. New events require a strictly increasing EventSequenceId. Gaps are allowed; sequence values need not be contiguous.
6. An exact replay of a previously accepted event returns false, even if newer events have since been accepted.
7. Reusing an accepted sequence ID with different payload throws InvalidOperationException.
8. ServerTick is metadata, not the ledger ordering key. Equal or lower tick values do not reorder accepted events; the authoritative command pipeline remains responsible for producing valid server ticks.
9. The ledger is in-memory and single-writer. Thread safety, persistence, and gameplay eligibility are outside this primitive.
10. The ledger increments the actor's Betrayal score exactly once per accepted event. It does not validate whether a transfer was legal and must only be called after the authoritative transfer transaction commits.

## Consequences

- Default structs cannot silently become valid player identities or ledger events.
- The ledger has deterministic replay and sequence semantics.
- Server tick validation remains at the command/simulation boundary instead of being duplicated in the ledger.
- The server application service remains responsible for ensuring that a ledger append and the corresponding transfer are one logical transaction.

## Required tests

- zero/default identity is represented but marked invalid;
- zero sequence, actor, or victim is rejected;
- self-betrayal is rejected;
- default BetrayalEvent is rejected by the ledger;
- exact replay before and after newer events does not increment score;
- conflicting replay does not mutate score;
- non-increasing new sequence is rejected;
- equal and decreasing ticks with increasing sequence preserve acceptance order;
- maximum sequence value is accepted without overflow;
- opposite-direction events are distinct.

## Verification

This ADR is a proposed contract. Source and tests may be committed against it, but the decision remains PROPOSED until explicitly approved. Build and NUnit execution are separate verification gates; this ADR does not claim either has passed.
