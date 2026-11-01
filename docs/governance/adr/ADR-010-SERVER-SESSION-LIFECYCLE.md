# ADR-010 — Server Session Lifecycle Primitive

**Status:** PROPOSED — implementation and tests exist; owner review required  
**Date:** 2026-11-01  
**Related:** ADR-002, ADR-005, ADR-008, `ISessionLifecycle`, `SessionLifecycle`

## Context

Core needs a deterministic representation of one server-issued session after the transport/application layer has created it. Authentication credentials, connection handles, expiry, reconnect tokens and authorization are not domain responsibilities. The player lifecycle (ADR-007) is independent of session connectivity.

## Proposed state model

`Pending → Authenticated → Disconnected → Closed`

A Pending session may also be Closed before authentication. An Authenticated session may be Closed directly. Disconnected sessions may be Closed. Closed is terminal.

- A session is constructed with one valid non-zero SessionId and starts Pending.
- `TryAuthenticate(ActorId)` associates one valid non-zero actor with a Pending session exactly once.
- `TryDisconnect()` succeeds only from Authenticated.
- `TryClose()` succeeds from Pending, Authenticated or Disconnected.
- Actor association is retained after disconnect/close for server-side audit and cleanup correlation.
- A disconnected or closed SessionId cannot be re-authenticated or rebound to another actor.
- Reconnect policy, token continuity, grace period, expiry, concurrent-session policy and authentication mechanisms remain application/backend decisions. A reconnect must not be inferred by this primitive.

## Implementation boundary

- `ISessionLifecycle` and `SessionLifecycle` are pure C#.
- No network connection, credentials, wall clock, timers, persistence or Unity/FishNet references.
- Single-writer/server-serialized; no thread-safety promise.
- CoreComposition is the construction root.

## Verification

NUnit tests cover constructor validation, initial state, actor binding, invalid/duplicate authentication, disconnect guards, association retention, terminal close and independent sessions. Passing tests verify only this primitive, not authentication security or reconnect behavior.
