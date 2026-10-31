# Phase 1 Core — Initial Contracts and Primitives

**Status:** Source committed — NOT VERIFIED  
**ADR:** ADR-005 (APPROVED 2026-11-01)  
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
- Standalone NUnit tests targeting .NET 8.

## Vitality semantics

The v2.1 gameplay specification defines zero Vitality as Dead and prohibits Dead → Alive revival in the MVP.

| Current state | Lose | Restore |
|---|---|---|
| Alive | Apply; reaching zero transitions to Dead | Apply, capped at Maximum |
| Dead | No-op | No-op; cannot revive |

Spirit is a separate post-death role/projection. It is not a Vitality state. The earlier Alive / Spirit / Eliminated Vitality draft is superseded by this implementation.

## Betrayal ledger semantics

- EventSequenceId is numeric ulong and must strictly increase for new events.
- Multiple events may share ServerTick; tick is informational, not the ordering key.
- Exact replay returns false and does not increment score.
- Reusing a sequence ID with different event data throws InvalidOperationException.
- An unseen sequence ID lower than or equal to the latest accepted sequence throws InvalidOperationException.
- The actor's score increases once per accepted event.
- The server application layer must call this ledger only after a successful authoritative transfer. This ledger does not validate gameplay eligibility or perform Debuff transfer.
- Calls are single-writer/server-serialized. No locking, persistence, clock, or network dependency is included.

## Deliberate exclusions

No Unity project, Unity assemblies, FishNet adapters, connection identity mapping, command transport, server tick scheduler, Debuff model, atomic transfer system, persistence, or backend has been added. No tuning values beyond default Vitality maximum 100 have been invented.

## Verification status

- Source files and NUnit test cases: **IMPLEMENTED** (committed to feature branch).
- Compilation: **NOT VERIFIED** — the available environment reported dotnet: command not found.
- NUnit execution: **NOT VERIFIED** — no reproducible test run is available.
- Unity/FishNet integration: **NOT IMPLEMENTED**.
- Gameplay rules: remain **PROPOSED FOR PLAYTEST**.

Do not mark this slice VERIFIED until a .NET SDK environment runs the build and test project successfully and retains the logs.
