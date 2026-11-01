# ADR-008 — Server Command Envelope and Sequence Admission

**Status:** PROPOSED — implementation and NUnit coverage exist; owner review required before treating the policy as frozen  
**Date:** 2026-11-01  
**Related:** ADR-002, ADR-005, ADR-007, Core CommandEnvelope, CommandSequenceTracker

## Context

The server must convert a network intent into a trusted Core command envelope. Client-controlled payloads must not establish the authenticated session identity or authoritative receive tick. Per-session sequence replay and excessive future jumps must be bounded before commands enter authoritative processing.

## Proposed contract

1. `SessionId` is resolved by trusted server ingress from the authenticated connection/session mapping. It is not accepted from the client as authoritative identity.
2. `ServerTick` is assigned by the server at ingress. Zero is valid as the initial tick; Core does not read a clock.
3. `CommandSequenceId` is a non-zero ulong value scoped to one authenticated session.
4. `CommandEnvelope<TCommand>` is a server-side Core envelope, not a network wire DTO. It contains the resolved SessionId, validated command sequence, server receive tick and command intent.
5. Each session has an independent last-accepted sequence.
6. New sessions start with last accepted sequence zero. A sequence is accepted only when it is strictly greater than the previous value and its numeric jump does not exceed configured `MaximumSequenceJump`.
7. Exact duplicate sequence returns `Conflict`; older sequence returns `InvalidState`; excessive forward jump returns `OutOfRange`; invalid IDs return `InvalidArgument`.
8. An accepted sequence is consumed before later gameplay-rule validation. A command rejected by gameplay rules cannot be replayed with the same sequence.
9. Reset clears one session's sequence state only after that authenticated session is permanently removed and its queued commands have drained. Reconnect policy must decide whether a new SessionId is issued.
10. The tracker is single-writer/server-serialized and is not thread-safe. It is in-memory and is not a durable anti-replay store.
11. The command admission queue is FIFO and has an explicit positive capacity. When full, it returns CapacityExceeded before consuming the command sequence; the sender may retry the same envelope after backpressure clears.
12. A session's replay state can be reset through the queue only when no queued command remains for that session; invalid reset requests fail without mutation.
13. `StateRevision` zero represents initial state. Advancing increments by one; advancing at ulong.MaxValue fails without wrapping.
14. No default MaximumSequenceJump, maximum tracked-session count or queue capacity is selected. The host must supply all three positive configured values.
15. The tracker has a hard maximum number of tracked sessions. New session admission returns CapacityExceeded at the limit; existing sessions may continue; Reset releases the slot.

## Implementation scope

The current Core branch includes:
- ServerTick and StateRevision value objects.
- CommandSequenceId.
- CommandEnvelope<TCommand>.
- CommandSequenceTracker with explicit maximum jump and tracked-session capacity.
- CommandAdmissionQueue<TCommand> with bounded FIFO capacity, replay-aware enqueue and safe session-state release after queued commands drain.
- CoreComposition factories for tracker and queue creation with explicit limits.
- NUnit tests for invalid inputs, duplicate/stale sequences, excessive jumps, per-session isolation, reset, trusted metadata and overflow.

## Open decisions

- Production values for MaximumSequenceJump, maximum tracked sessions and queue capacity (aligned with match/player limits and expected burst behavior).
- Whether sequence state survives reconnects (requires stable authenticated session identity or persistence).
- Rate limiting and command timeout policy. Queue-full backpressure is implemented as CapacityExceeded without consuming the sequence; production capacity remains a configuration decision.
- Whether a command may be admitted before all payload validation is complete.

These remain configuration/application policy; no production constants are embedded in Core.

## Verification boundary

CI can verify deterministic code behavior and unit tests. It cannot approve the proposed gameplay/network admission policy or prove real transport authentication.
