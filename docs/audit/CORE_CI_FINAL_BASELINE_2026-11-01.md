# Core CI — Consolidated Standalone Core Baseline

**Date:** 2026-11-01  
**Source commit:** `be67d8a3399f99c2c0e05a37eec8ffa0cb2abb13`  
**Workflow run:** `41331236864` — SUCCESS  
**Branch:** `feature/core-lifecycle-resolution`

## Verification

- Core dependency boundary: PASS — Core contains no UnityEngine/FishNet references.
- Core composition root: PASS — aggregate/service construction is guarded by Core CI.
- .NET 10 SDK restore: PASS.
- Core Release build: PASS — target framework .NET Standard 2.1.
- NUnit test project Release build: PASS — target framework .NET 10.
- NUnit: **148 passed, 0 failed, 0 skipped**.
- TRX artifact: `13093536721`.
- Artifact SHA-256: `2f9e1fae28c3bf7526b33a9e11942439885d95b345df0c60797c8c93f90e59c1`.

## Verified Core inventory

- Result / Result<T> and typed Core error codes.
- Typed ulong ActorId, MatchId, SessionId, VictimId, EventSequenceId and CommandSequenceId.
- VitalityAccount lifecycle under ADR-007: Alive / Spirit / Eliminated.
- BetrayalLedger replay, ordering and score invariants.
- MatchLifecycle state machine.
- MatchPlayerRegistry: bounded match-scoped membership, duplicate rejection, immutable ordered ID snapshots and membership-only removal.\n- SessionLifecycle: Pending → Authenticated → Disconnected → Closed; immutable actor binding and terminal close; no credentials or reconnect policy.
- CoreComposition factories for session lifecycle, match/player aggregates and command services.
- ServerTick and overflow-safe StateRevision.
- Server-side CommandEnvelope.
- Bounded per-session CommandSequenceTracker.
- Bounded FIFO CommandAdmissionQueue with replay-aware admission and safe session-state reset after queue drain.

## Verification boundary

This verifies standalone Core only. It does not verify Unity Editor import/compile, FishNet API compatibility, Android builds, transport authentication, Host/Dedicated Server behavior, persistence, performance or production security.

ADR-008 and ADR-009 remain PROPOSED because production limits and reconnect/spectator policy still require owner review. The implementation is test-verified; those policy choices are not silently frozen.
