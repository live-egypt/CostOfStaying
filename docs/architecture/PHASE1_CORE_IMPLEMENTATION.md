# Phase 1 Core — Initial Contracts and Primitives

**Status:** Source committed — NOT VERIFIED  
**Architecture ADR:** ADR-005 (APPROVED 2026-11-01)  
**Identifier/ledger ADR:** ADR-006 (PROPOSED — pending owner approval)  
**Technical register:** docs/architecture/PHASE1_TECHNICAL_MASTER.md  
**Gameplay baseline:** v2.1 / PROPOSED FOR PLAYTEST  
**Unity assumption:** 6000.3 LTS — not verified by a Unity project  
**FishNet assumption:** 4.7.4R — not pinned or verified; no FishNet adapter is included.

## Scope delivered

- Engine-independent .NET Standard 2.1 Core project.
- Strongly typed numeric wrappers: ActorId, VictimId and EventSequenceId (ulong).
- IVitalityAccount and IBetrayalLedger pure contracts.
- VitalityAccount with bounded vitality and terminal Alive → Dead behavior.
- BetrayalLedger with monotonic event sequence, exact replay rejection and actor score accumulation.
- Standalone NUnit tests targeting .NET 8, including identifier value-semantics coverage.

## Vitality semantics

The v2.1 gameplay specification defines zero Vitality as Dead and prohibits Dead → Alive revival in the MVP.

| Current state | Lose | Restore |
|---|---|---|
| Alive | Apply; reaching zero transitions to Dead | Apply, capped at Maximum |
| Dead | No-op | No-op; cannot revive |

Spirit is a separate post-death role/projection. It is not a Vitality state. The earlier Alive / Spirit / Eliminated Vitality draft is superseded by this implementation.

The account rejects an invalid/default ActorId(0) player identity at construction. The numeric wrapper can still represent zero so default(ActorId) remains detectable; IsValid is the explicit validity check.

## Betrayal ledger semantics

- ActorId, VictimId and EventSequenceId are numeric ulong wrappers. Zero is reserved and invalid for domain events.
- A valid BetrayalEvent requires non-zero sequence, actor and victim IDs; actor and victim must differ.
- The ledger validates BetrayalEvent.IsValid because default(BetrayalEvent) bypasses its constructor.
- New events require a strictly increasing EventSequenceId; gaps are allowed and sequence values need not be contiguous.
- Exact replay returns false and does not increment score, including replay after newer accepted events.
- Reusing a sequence ID with different event data throws InvalidOperationException.
- ServerTick is informational and is not the ledger ordering key. Equal or decreasing tick values do not reorder events; the authoritative pipeline remains responsible for valid tick generation.
- The actor's score increases once per accepted event.
- The server application layer must call this ledger only after a successful authoritative transfer. This ledger does not validate gameplay eligibility or perform Debuff transfer.
- Calls are single-writer/server-serialized. No locking, persistence, clock, or network dependency is included.

## Deliberate exclusions

No Unity project, Unity assemblies, FishNet adapters, connection identity mapping, command transport, server tick scheduler, Debuff model, atomic transfer system, persistence, or backend has been added. IdentifierTests covers numeric preservation, validity, equality/inequality, zero sentinel behavior, maximum values and separation between identifier types. Zero is representable but invalid for runtime identities. No tuning values beyond default Vitality maximum 100 have been invented.

## Verification status

- Source files and NUnit test cases: **IMPLEMENTED** (committed to feature branch).
- Static review of new guards and test cases: **REVIEWED, NOT EXECUTED**.
- Compilation: **NOT VERIFIED** — the available environment reports dotnet: command not found.
- NUnit execution: **NOT VERIFIED** — no reproducible test run is available.
- Unity/FishNet integration: **NOT IMPLEMENTED**.
- Gameplay rules: remain **PROPOSED FOR PLAYTEST**.

Do not mark this slice VERIFIED until a .NET SDK environment runs the build and test project successfully and retains the logs.
