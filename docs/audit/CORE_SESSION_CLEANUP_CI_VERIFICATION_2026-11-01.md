# Safe Session Cleanup — Core CI Verification

**Date:** 2026-11-01  
**Repository:** `live-egypt/CostOfStaying`  
**Branch:** `feature/core-lifecycle-resolution`  
**Source commit:** `c33faf84aa39856d46181ccc1c9decbfba2777d2`  
**Workflow:** Core CI  
**Run:** `41332989910` — SUCCESS  
**TRX artifact:** `13093814339`  
**Artifact SHA-256:** `badb2929b1fdd4f4bbacc74405499df5c1902ca2181789f2903369db8dd5f7c1`

## Verification results

- Core dependency-boundary scan: PASS.
- CoreComposition construction-root scan: PASS.
- .NET 10 restore/build: PASS.
- Core Release build targeting .NET Standard 2.1: PASS.
- NUnit: **181 passed, 0 failed, 0 skipped**.

## Safe cleanup path

The public registry contract no longer exposes removal, and the queue no longer exposes replay-state reset publicly:

- `ISessionRegistry` exposes registration, lookup and immutable snapshots; it has no Remove method.
- `SessionRegistry.RemoveClosedSession` is internal.
- `CommandAdmissionQueue.ResetSession` is internal.
- `SessionCleanupService<TCommand>` is the single Core service that can coordinate both operations.
- `CoreComposition.CreateSessionCleanupService(...)` is the construction path.
- `InternalsVisibleTo` grants the NUnit test assembly access to internal members for direct unit testing; this is test visibility, not a runtime security boundary.

## Cleanup invariants

1. Session must already be Closed.
2. Invalid/unknown SessionId is rejected.
3. Pending queued commands block cleanup without mutation.
4. Replay sequence state is reset only after the queue has drained.
5. A session with no prior admitted command can still be removed.
6. Registry removal occurs only after replay reset succeeds or is already absent.
7. Caller must wait until any dequeued/in-flight command handler has completed before invoking cleanup; the current queue does not track in-flight handlers.

## Dequeue-time command authority

`CommandAdmissionService.TryTakeNext` re-resolves the registered session immediately before handing a command to the application handler. It returns `AuthenticatedCommand<TCommand>` with the server-resolved ActorId only when the session remains Authenticated and its actor binding is valid. Commands admitted before disconnect/close are discarded as Unauthorized. NUnit coverage verifies empty queue, successful ActorId resolution and discard after disconnect/close.

## Decision boundary

ADR-013 and ADR-012 remain **PROPOSED**. The service verifies deterministic in-memory ordering, not production disconnect detection, authentication, reconnect, persistence or distributed cleanup.
