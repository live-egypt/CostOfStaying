# ADR-013 — Safe Session Cleanup and Replay-State Release

**Status:** PROPOSED — implementation and NUnit coverage exist; owner review required  
**Date:** 2026-11-14  
**Related:** ADR-008, ADR-010, ADR-011, `ISessionCleanupService<TCommand>`, `SessionCleanupService<TCommand>`, `ICommandAdmissionService<TCommand>`

## Context

SessionRegistry removal and command replay-state release are separate operations. Removing a closed session while its commands remain queued would leave accepted commands without their session identity. Clearing replay state before the queue drains would release sequence capacity too early.

The previous implementation checked only queued commands. A command already dequeued and handed to an application handler was invisible to cleanup, allowing the registry entry and replay state to be released before processing completed. Relying only on caller discipline did not enforce the intended safety condition.

## Proposed contract

1. The session must already be Closed.
2. Invalid or unknown SessionId values are rejected.
3. Cleanup returns InvalidState while any command for the SessionId remains queued or is being processed by an application handler.
4. `CommandAdmissionService.TryTakeNext` revalidates the session at dequeue time. A stale command is discarded and is not marked in-flight.
5. A command successfully returned by `TryTakeNext` is marked in-flight before it is handed to the caller.
6. The application must call `ICommandAdmissionService.Complete` exactly once after handling finishes, including rejection, cancellation or exception paths (normally from a finally block).
7. Duplicate or unknown completion attempts are rejected and do not alter counters.
8. Queue capacity bounds total outstanding work: queued plus in-flight commands. Dequeue alone does not free capacity; successful completion does.
9. After the queue contains no pending commands and no in-flight work for the session, cleanup resets its per-session sequence/replay state.
10. If the session never admitted a command, NotFound from replay-state reset is treated as already-clean.
11. Only after replay-state reset is complete does cleanup invoke the internal SessionRegistry removal operation.
12. Cleanup and admission are single-writer/server-serialized; no thread-safety guarantee is made.
13. This contract does not implement disconnect detection, authentication, persistence, reconnect or timeout policy.

## Implementation

- `CommandAdmissionQueue<TCommand>` tracks per-session in-flight command sequence IDs.
- Queue capacity is applied to `PendingCount + InFlightCount`.
- `ICommandAdmissionService<TCommand>.Complete` and `CommandAdmissionService<TCommand>.Complete` release exactly one in-flight command.
- `ResetSession` refuses to reset replay state while commands are pending or in-flight.
- `SessionCleanupService<TCommand>` therefore cannot remove a closed session until all application-handled commands complete.
- NUnit coverage verifies cleanup rejection while in-flight, successful cleanup after completion, stale dequeue discard, duplicate completion rejection and total-outstanding backpressure.

## Application integration requirement

The future gameplay command dispatcher must guarantee completion on every exit path:

```csharp
Result<AuthenticatedCommand<GameplayCommand>> taken = admission.TryTakeNext();
if (taken.IsSuccess)
{
    try
    {
        handler.Handle(taken.Value);
    }
    finally
    {
        admission.Complete(taken.Value);
    }
}
```

The example is an integration contract, not an implemented dispatcher. A production handler may need to report processing results separately; completion only releases admission resources and does not indicate that the gameplay command succeeded.

## Verification boundary

This change must be verified by the Core CI run for the resulting commit. It verifies pure-C# in-memory admission and cleanup only; it does not verify transport handling, asynchronous worker coordination, persistence, reconnect policy or production security.
