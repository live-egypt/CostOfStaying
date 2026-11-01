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
9. Reset clears one session's sequence state when that authenticated session is permanently removed. Reconnect policy must decide whether a new SessionId is issued.
10. The tracker is single-writer/server-serialized and is not thread-safe. It is in-memory and is not a durable anti-replay store.
11. `StateRevision` zero represents initial state. Advancing increments by one; advancing at ulong.MaxValue fails without wrapping.
12. No default MaximumSequenceJump is selected. The host must supply a positive configured value.

## Implementation scope

The current Core branch includes:
- ServerTick and StateRevision value objects.
- CommandSequenceId.
- CommandEnvelope<TCommand>.
- CommandSequenceTracker.
- CoreComposition factory for a tracker with explicit MaximumSequenceJump.
- NUnit tests for invalid inputs, duplicate/stale sequences, excessive jumps, per-session isolation, reset, trusted metadata and overflow.

## Open decisions

- Production value for MaximumSequenceJump.
- Whether sequence state survives reconnects (requires stable authenticated session identity or persistence).
- Queue capacity, backpressure, rate limiting and command timeout policy.
- Whether a command may be admitted before all payload validation is complete.

These remain configuration/application policy; no production constants are embedded in Core.

## Verification boundary

CI can verify deterministic code behavior and unit tests. It cannot approve the proposed gameplay/network admission policy or prove real transport authentication.
