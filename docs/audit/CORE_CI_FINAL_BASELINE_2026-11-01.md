# Core CI — Consolidated Standalone Core Baseline

**Date:** 2026-11-01  
**Source commit:** `26a77dcf1394d35d3db48ea0f5b7cde4c118998f`  
**Workflow run:** `41332755494` — SUCCESS  
**Branch:** `feature/core-lifecycle-resolution`

## Verification

- Core dependency boundary: PASS — Core contains no UnityEngine/FishNet references.
- Core composition root: PASS — aggregate/service construction is guarded by Core CI.
- .NET 10 SDK restore: PASS.
- Core Release build: PASS — target framework .NET Standard 2.1.
- NUnit test project Release build: PASS — target framework .NET 10.
- NUnit: **177 passed, 0 failed, 0 skipped**.
- TRX artifact: `13093413196`.
- Artifact SHA-256: `7320747a7a78878879a651882467800b2c5514fac93cb0b480e996aa1545b39f`.

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
- Bounded FIFO CommandAdmissionQueue with replay-aware admission and safe session-state reset after queue drain.\n- CommandAdmissionService: requires a registered Authenticated session with valid ActorId before enqueuing; tests verify unauthorized sessions do not consume sequences.\n- SessionCleanupService: requires Closed, drains the queue, internally resets replay state and removes the session; direct registry removal and replay reset are internal-only.

## Verification boundary

This verifies standalone Core only. It does not verify Unity Editor import/compile, FishNet API compatibility, Android builds, transport authentication, Host/Dedicated Server behavior, persistence, performance or production security.

ADR-008 and ADR-009 remain PROPOSED because production limits and reconnect/spectator policy still require owner review. The implementation is test-verified; those policy choices are not silently frozen.
