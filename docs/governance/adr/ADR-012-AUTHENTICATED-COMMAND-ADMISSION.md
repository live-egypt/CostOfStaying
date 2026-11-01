# ADR-012 — Authenticated Command Admission Gate

**Status:** PROPOSED — Core implementation and tests exist; owner review required  
**Date:** 2026-11-01  
**Related:** ADR-002, ADR-005, ADR-008, ADR-010, ADR-011, `ICommandAdmissionService<TCommand>`, `CommandAdmissionService<TCommand>`

## Context

The bounded FIFO and per-session sequence tracker prevent duplicate/stale sequence admission, but they do not independently establish that the referenced session is currently authenticated. A trusted server ingress needs a small Core gate between session resolution and queue admission.

## Proposed decision

1. A command envelope must pass its constructor/default-value validity check.
2. Its SessionId must resolve to a registered session.
3. The session must be in `Authenticated` state.
4. The session must carry a valid, non-zero ActorId binding.
5. Only after those checks may the command enter the bounded FIFO and consume its sequence.
6. Unknown sessions return NotFound; malformed envelopes return InvalidArgument; non-authenticated or unbound sessions return Unauthorized.
7. Queue capacity and sequence/replay rules remain enforced by `CommandAdmissionQueue<TCommand>`.
8. Session rejection must not consume a sequence or mutate the queue.
9. The service is pure C#, in-memory and single-writer/server-serialized.
10. `TryTakeNext` revalidates the session at dequeue time and returns an `AuthenticatedCommand<TCommand>` carrying the ActorId re-resolved from the current session. Commands queued before disconnect/close are discarded with Unauthorized rather than handed to gameplay handlers.

## Explicit non-goals

This gate does not:
- authenticate credentials or validate tokens;
- prove that a network connection owns the SessionId;
- compare actor identity with command-specific actor fields (client commands must not contain authoritative actor identity);
- enforce match phase, gameplay permissions, rate limits, payload size or command-specific bounds;
- execute gameplay commands or mutate match state.

Those checks belong to trusted transport/session resolution and typed application command handlers. The server adapter must construct the envelope only after resolving its connection to a registered session.

## Implementation and tests

- `ICommandAdmissionService<TCommand>`
- `CommandAdmissionService<TCommand>`
- `CoreComposition.CreateCommandAdmissionService(...)`
- `AuthenticatedCommand<TCommand>` is created internally and carries server-resolved ActorId, SessionId, sequence, receive tick and command.
- `TryTakeNext` discards stale commands if the session disconnected/closed after admission.
- NUnit coverage for null dependencies, default envelopes, unknown sessions, Pending/Disconnected/Closed sessions, missing ActorId binding, successful admission, replay rejection, full-queue retry, actor-binding validation, dequeue-time disconnect/close revalidation and no sequence consumption before authentication.

## Verification boundary

Core CI run `41332989910` passed the Release build and **181 NUnit tests (0 failed, 0 skipped)** on source commit `c33faf84aa39856d46181ccc1c9decbfba2777d2`. This verifies the pure Core gate only; it does not certify transport authentication or production authorization policy.
