# Safe Session Cleanup — Core CI Verification

**Date:** 2026-11-01  
**Repository:** `live-egypt/CostOfStaying`  
**Branch:** `feature/core-lifecycle-resolution`  
**Source commit:** `26a77dcf1394d35d3db48ea0f5b7cde4c118998f`  
**Workflow:** Core CI  
**Run:** `41332755494` — SUCCESS  
**TRX artifact:** `13093413196`  
**Artifact SHA-256:** `7320747a7a78878879a651882467800b2c5514fac93cb0b480e996aa1545b39f`

## Verification results

- Core dependency-boundary scan: PASS.
- CoreComposition construction-root scan: PASS.
- .NET 10 restore/build: PASS.
- Core Release build targeting .NET Standard 2.1: PASS.
- NUnit: **177 passed, 0 failed, 0 skipped**.

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

## Decision boundary

ADR-013 remains **PROPOSED**. The service verifies deterministic in-memory ordering, not production disconnect detection, authentication, reconnect, persistence or distributed cleanup.
