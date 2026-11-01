# Core CI — Consolidated Standalone Core Baseline

**Date:** 2026-11-01  
**Source commit:** `5648aaf232dd247a0af52317f8ddd363a0441f0c`  
**Workflow run:** `41330975856` — SUCCESS  
**Branch:** `feature/core-lifecycle-resolution`

## Verification

- Core dependency boundary: PASS — Core contains no UnityEngine/FishNet references.
- Core composition root: PASS — aggregate/service construction is guarded by Core CI.
- .NET 10 SDK restore: PASS.
- Core Release build: PASS — target framework .NET Standard 2.1.
- NUnit test project Release build: PASS — target framework .NET 10.
- NUnit: **134 passed, 0 failed, 0 skipped**.
- TRX artifact: `13092889041`.
- Artifact SHA-256: `2b13f11e173414f7b232a89ef1ec0389d1d237686cc9137f5efa9662a8d1ee67`.

## Verified Core inventory

- Result / Result<T> and typed Core error codes.
- Typed ulong ActorId, MatchId, SessionId, VictimId, EventSequenceId and CommandSequenceId.
- VitalityAccount lifecycle under ADR-007: Alive / Spirit / Eliminated.
- BetrayalLedger replay, ordering and score invariants.
- MatchLifecycle state machine.
- MatchPlayerRegistry: bounded match-scoped membership, duplicate rejection, immutable ordered ID snapshots and membership-only removal.
- CoreComposition factories for the Core aggregates and command services.
- ServerTick and overflow-safe StateRevision.
- Server-side CommandEnvelope.
- Bounded per-session CommandSequenceTracker.
- Bounded FIFO CommandAdmissionQueue with replay-aware admission and safe session-state reset after queue drain.

## Verification boundary

This verifies standalone Core only. It does not verify Unity Editor import/compile, FishNet API compatibility, Android builds, transport authentication, Host/Dedicated Server behavior, persistence, performance or production security.

ADR-008 and ADR-009 remain PROPOSED because production limits and reconnect/spectator policy still require owner review. The implementation is test-verified; those policy choices are not silently frozen.
