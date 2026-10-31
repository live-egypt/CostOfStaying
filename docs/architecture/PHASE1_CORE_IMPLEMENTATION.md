# Phase 1 Core — Initial Contracts and Primitives

**Status:** Source committed — NOT VERIFIED  
**ADR:** ADR-005 (APPROVED 2026-11-01)  
**Gameplay baseline:** v2.1 / PROPOSED FOR PLAYTEST  
**FishNet assumption:** 4.7.4R appears only as an unverified historical assumption. No FishNet adapter or package reference is included in this slice.

## Scope delivered

This commit series establishes an engine-independent .NET Core project and the first pure-domain contracts and primitives:

- Strongly typed `ulong` wrappers: `ActorId`, `VictimId`, `EventSequenceId`.
- `IVitalityAccount` and `IBetrayalLedger` contracts, defined before their concrete implementations.
- `VitalityAccount`: bounded vitality, Alive / Spirit / Eliminated states, and the Phase 1 transition matrix.
- `BetrayalLedger`: server-authored event records, monotonically increasing event sequence, exact replay rejection, and actor score accumulation.
- Standalone NUnit tests targeting .NET 8; Core targets .NET Standard 2.1.

## Vitality state matrix

| Current state | Lose | Restore | Resurrect | Eliminate |
|---|---|---|---|---|
| Alive | Apply; reaching zero transitions to Spirit | Apply, capped at Maximum | Throw `InvalidOperationException` | Throw `InvalidOperationException` |
| Spirit | No-op | No-op | Apply; returns to Alive | Apply; transitions to terminal Eliminated |
| Eliminated | No-op | No-op | Throw `InvalidOperationException` | Throw `InvalidOperationException` |

**MVP policy boundary:** The domain primitive exposes the Spirit-to-Alive transition because it is part of the previously accepted Phase 1 primitive matrix. The v2.1 gameplay baseline still prohibits revival in the MVP. The authoritative application layer must not call `Resurrect` under that gameplay policy. This is not a change to ADR-004.

## Betrayal event rules

- `EventSequenceId` is a numeric `ulong` and must strictly increase for newly recorded events.
- Multiple events may share the same `ServerTick`; tick is informational and is not the ordering key.
- An exact replay of a recorded event returns `false` and does not increment the score.
- Reusing a sequence ID with different event data throws `InvalidOperationException`.
- A previously unseen sequence ID lower than or equal to the latest accepted sequence throws `InvalidOperationException`.
- The actor's score increases exactly once per accepted event.
- Calls are single-writer/server-serialized by contract. This primitive deliberately has no locking, persistence, clock access, or network dependency.

## Deliberate exclusions

No Unity project, Unity assemblies, FishNet adapters, connection identity mapping, command transport, server tick scheduler, persistence, or backend has been added. No balance values beyond the documented default maximum vitality of 100 have been invented.

## Verification status

- Source files and NUnit test cases: **IMPLEMENTED** (committed to the feature branch).
- Compilation: **NOT VERIFIED** — the available execution environment previously reported `dotnet: command not found`.
- NUnit execution: **NOT VERIFIED** — no reproducible test run is available.
- Unity/FishNet integration: **NOT IMPLEMENTED**.
- Gameplay rules: remain **PROPOSED FOR PLAYTEST**.

Do not mark this slice VERIFIED until a .NET SDK environment runs the test project successfully and the build/test output is retained as evidence.
