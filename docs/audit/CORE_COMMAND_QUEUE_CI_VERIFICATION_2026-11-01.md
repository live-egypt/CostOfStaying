# Core CI — Bounded Command Queue and Session Replay Capacity

**Date:** 2026-11-01  
**Source commit:** `a0cc96c02ce163691456bc2a02dbbc9c57cd1d12`  
**Workflow run:** `41329400470` — SUCCESS  
**Branch:** `feature/core-lifecycle-resolution`

## Verified checks

- Core dependency boundary: PASS — no UnityEngine/FishNet references in Core source/project files.
- Core composition root: PASS — aggregate and queue construction is centralized in CoreComposition; the queue owns its private sequence tracker.
- .NET 10 restore: PASS.
- Core Release build: PASS.
- NUnit test project Release build: PASS.
- NUnit: **119 passed, 0 failed, 0 skipped**.
- TRX artifact upload: PASS.

Artifact:
- ID: `13092811803`
- Name: `core-test-results`
- SHA-256: `91eeceebd7a30581bc2958d798daa12c8a9be625dbf841c87043dcf9d5311fdf`

## Source increment verified

1. `CommandSequenceTracker` now requires explicit positive `MaximumSequenceJump` and `MaximumTrackedSessions`.
2. A new session beyond the configured tracker capacity receives `CapacityExceeded`; existing sessions may continue.
3. `CommandAdmissionQueue<TCommand>` is a bounded FIFO with explicit positive capacity.
4. A full queue rejects before sequence consumption, allowing retry after dequeue.
5. Duplicate, stale and excessive-jump commands are rejected without queue mutation.
6. Session replay state can be reset only after the session's queued commands drain; reset of an unknown or invalid session returns a typed failure.
7. Queue construction and Core aggregate construction remain guarded by the CI composition-root check.

## Policy boundary

ADR-008 remains PROPOSED pending owner review. Production values for maximum sequence jump, tracked sessions and queue capacity, reconnect continuity, rate limiting and command timeout remain configuration/policy decisions. This CI run verifies code behavior, not approval of those production values.

## Remaining Core scope

Session lifecycle, player/match/world aggregate ownership, command payload validation/application handlers, movement simulation, Debuff eligibility and atomic transfer, snapshots and match outcome resolution remain unimplemented.

Unity, FishNet, Android, authenticated transport mapping, Host/Dedicated Server, persistence and production security are not verified by this workflow.
