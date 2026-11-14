# ADR-012 — Authenticated Command Admission Gate

**Status:** PROPOSED — Core implementation and tests exist; owner review required  
**Date:** 2026-11-14  
**Related:** ADR-002, ADR-005, ADR-008, ADR-010, ADR-011, ADR-013, `ICommandAdmissionService<TCommand>`, `CommandAdmissionService<TCommand>`

## Context

The bounded FIFO and per-session sequence tracker prevent duplicate/stale sequence admission, but they do not independently establish that the referenced session is currently authenticated. A trusted server ingress needs a small Core gate between session resolution and queue admission.

The application handoff also needs an explicit completion acknowledgement so session cleanup and queue capacity account for commands already dequeued and currently being processed.

## Proposed decision

1. A command envelope must pass its constructor/default-value validity check.
2. Its SessionId must resolve to a registered session.
3. The session must be in `Authenticated` state.
4. The session must carry a valid, non-zero ActorId binding.
5. Only after those checks may the command enter the bounded FIFO and consume its sequence.
6. Unknown sessions return NotFound; malformed envelopes return InvalidArgument; non-authenticated or unbound sessions return Unauthorized.
7. Queue capacity covers both queued commands and commands handed to application handlers but not yet completed.
8. Queue capacity and sequence/replay rules remain enforced by `CommandAdmissionQueue<TCommand>`.
9. Session rejection must not consume a sequence or mutate the queue.
10. `TryTakeNext` revalidates the session at dequeue time. Commands queued before disconnect/close are discarded with Unauthorized rather than handed to gameplay handlers.
11. A command successfully returned by `TryTakeNext` is marked in-flight. The application must call `Complete(command)` exactly once after processing ends, including all failure/exception paths.
12. Cleanup cannot release replay state while any command for the session remains queued or in-flight.

## Explicit non-goals

This gate does not:
- authenticate credentials or validate tokens;
- prove that a network connection owns the SessionId;
- compare actor identity with command-specific actor fields (client commands must not contain authoritative actor identity);
- enforce match phase, gameplay permissions, rate limits, payload size or command-specific bounds;
- execute gameplay commands or mutate match state;
- decide reconnect or per-actor multi-session policy.

Those checks belong to trusted transport/session resolution and typed application command handlers. The server adapter must construct the envelope only after resolving its connection to a registered session.

## Implementation and tests

- `ICommandAdmissionService<TCommand>` with admission, dequeue and completion operations.
- `CommandAdmissionService<TCommand>` validates the authenticated session at ingress and again at dequeue.
- `AuthenticatedCommand<TCommand>` carries the server-resolved ActorId, SessionId, sequence, receive tick and command.
- Successfully handed-off commands are tracked in-flight until explicit completion.
- NUnit coverage includes authentication, replay, queue capacity, dequeue-time session revalidation, completion lifecycle and in-flight backpressure.

## Verification boundary

The resulting Core CI run must be cited for the exact implementation commit. Core tests verify only the pure C# gate; they do not certify transport authentication, gameplay authorization, reconnect policy or production security.
