# Core CI — Consolidated Standalone Core Baseline

**Date:** 2026-11-01  
**Source commit:** `21682e9af7285013ac17524097fffbebc69bf293`  
**Workflow run:** `41332141494` — SUCCESS  
**Branch:** `feature/core-lifecycle-resolution`

## Verification

- Core dependency boundary: PASS — Core contains no UnityEngine/FishNet references.
- Core composition root: PASS — aggregate/service construction is guarded by Core CI.
- .NET 10 SDK restore: PASS.
- Core Release build: PASS — target framework .NET Standard 2.1.
- NUnit test project Release build: PASS — target framework .NET 10.
- NUnit: **173 passed, 0 failed, 0 skipped**.
- TRX artifact: `13093412354`.
- Artifact SHA-256: `9fef72d79dce1f86e96558896d6cb25946079a7cca3d5286b45f465d706e31eb`.

## Verified Core inventory

- Result / Result<T> and typed Core error codes.
- Typed ulong ActorId, MatchId, SessionId, VictimId, EventSequenceId and CommandSequenceId.
- VitalityAccount lifecycle under ADR-007: Alive / Spirit / Eliminated.
- BetrayalLedger replay, ordering and score invariants.
- MatchLifecycle state machine.
- MatchPlayerRegistry: bounded match-scoped membership, duplicate rejection, immutable ordered ID snapshots and membership-only removal.\n- SessionLifecycle: Pending → Authenticated → Disconnected → Closed; immutable actor binding and terminal close; no credentials or reconnect policy.\n- SessionRegistry: explicit capacity; Pending-only registration; Closed-only removal; immutable ordered snapshots; no actor multi-session policy.
- CoreComposition factories for session lifecycle, match/player aggregates and command services.
- ServerTick and overflow-safe StateRevision.
- Server-side CommandEnvelope.
- Bounded per-session CommandSequenceTracker.
- Bounded FIFO CommandAdmissionQueue with replay-aware admission and safe session-state reset after queue drain.\n- CommandAdmissionService: requires a registered Authenticated session with valid ActorId before enqueuing; tests verify unauthorized sessions do not consume sequences.

## Verification boundary

This verifies standalone Core only. It does not verify Unity Editor import/compile, FishNet API compatibility, Android builds, transport authentication, Host/Dedicated Server behavior, persistence, performance or production security.

ADR-008 and ADR-009 remain PROPOSED because production limits and reconnect/spectator policy still require owner review. The implementation is test-verified; those policy choices are not silently frozen.
