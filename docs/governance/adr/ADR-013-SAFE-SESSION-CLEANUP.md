# ADR-013 — Safe Session Cleanup and Replay-State Release

**Status:** PROPOSED — implementation and NUnit coverage exist; owner review required  
**Date:** 2026-11-01  
**Related:** ADR-008, ADR-010, ADR-011, `ISessionCleanupService<TCommand>`, `SessionCleanupService<TCommand>`

## Context

SessionRegistry removal and command replay-state release are separate operations. Removing a closed session while its commands remain queued would leave accepted commands without their session identity. Clearing replay state before the queue drains would release sequence capacity too early.

## Proposed contract

1. The session must already be Closed.
2. Invalid or unknown SessionId values are rejected.
3. The queue must contain no pending commands for the SessionId; otherwise cleanup returns InvalidState and leaves registry/replay state unchanged.
4. Once drained, cleanup resets the queue's per-session sequence state.
5. If the session never admitted a command, NotFound from replay-state reset is treated as already-clean.
6. Only after replay-state reset is complete does cleanup invoke the internal SessionRegistry removal operation. The public ISessionRegistry contract does not expose removal, and CommandAdmissionQueue replay reset is internal-only.
7. The caller must invoke cleanup only after any dequeued/in-flight command for the session has finished processing. The queue does not track in-flight handlers.
8. Cleanup is single-writer/server-serialized and not thread-safe.
9. It does not implement disconnect detection, authentication, persistence, reconnect or timeout policy.

## Implementation

- `ISessionCleanupService<TCommand>` / `SessionCleanupService<TCommand>`.
- `CoreComposition.CreateSessionCleanupService(sessions, queue)`.
- The public registry contract does not expose removal; queue replay reset and registry removal are internal-only and coordinated by this service.
- Public registry contract has no removal method; replay reset and closed-session removal are internal to Core and coordinated only by the cleanup service.
- NUnit coverage for invalid/unknown IDs, Closed-state requirement, pending-queue rejection, successful cleanup, replay-capacity release and sessions with no admitted commands.

## Verification boundary

Core CI run `41332989910` passed **181 NUnit tests (0 failed, 0 skipped)** on source commit `c33faf84aa39856d46181ccc1c9decbfba2777d2`. It verifies this in-memory cleanup order only. The server application remains responsible for ensuring dequeued commands have finished before invoking cleanup.
