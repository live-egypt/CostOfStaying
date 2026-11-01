# Core CI — Consolidated Standalone Core Baseline

**Date:** 2026-11-01  
**Source commit:** `1353b84ff1951f9c1fa8f2a3c3316bd3c89a6ac8`  
**Workflow run:** `41331861227` — SUCCESS  
**Branch:** `feature/core-lifecycle-resolution`

## Verification

- Core dependency boundary: PASS — Core contains no UnityEngine/FishNet references.
- Core composition root: PASS — aggregate/service construction is guarded by Core CI.
- .NET 10 SDK restore: PASS.
- Core Release build: PASS — target framework .NET Standard 2.1.
- NUnit test project Release build: PASS — target framework .NET 10.
- NUnit: **161 passed, 0 failed, 0 skipped**.
- TRX artifact: `13093327104`.
- Artifact SHA-256: `e3c698e55c592137bea7df3bf8ee31ae2bafa5ecaacbef6e135313cb090598bb`.

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
- Bounded FIFO CommandAdmissionQueue with replay-aware admission and safe session-state reset after queue drain.

## Verification boundary

This verifies standalone Core only. It does not verify Unity Editor import/compile, FishNet API compatibility, Android builds, transport authentication, Host/Dedicated Server behavior, persistence, performance or production security.

ADR-008 and ADR-009 remain PROPOSED because production limits and reconnect/spectator policy still require owner review. The implementation is test-verified; those policy choices are not silently frozen.
