# ADR-010 — Server Session Lifecycle

**Status:** PROPOSED — deterministic Core primitive and NUnit coverage exist; session/reconnect policy requires owner review  
**Date:** 2026-11-01  
**Related:** ADR-005, ADR-008, `ISessionLifecycle`, `SessionLifecycle`

## Context

Transport connection state, authenticated actor identity and player gameplay lifecycle are distinct concerns. The server must own the session identity and the association between an authenticated session and an ActorId. Session state must not be conflated with Alive/Spirit/Eliminated player state.

## Proposed contract

1. Every session is created with a server-issued, non-zero SessionId.
2. A new session starts Pending and has no ActorId association.
3. Only trusted server authentication code may call TryAuthenticate after credentials/identity have been validated outside Core.
4. Pending → Authenticated associates exactly one non-zero ActorId.
5. A session can be authenticated only once. A second association attempt fails without replacing the original ActorId.
6. Authenticated → Disconnected is explicit. Pending cannot be disconnected.
7. A Disconnected session cannot be re-authenticated or reactivated. Reconnect must create a new SessionId and pass through trusted authentication again.
8. TryClose moves Pending, Authenticated or Disconnected to terminal Closed. Repeated close and all later state transitions fail.
9. The ActorId association is retained after Disconnected/Closed for server-side audit/correlation; retention duration and log redaction are application policy.
10. Core stores no credentials, transport objects, timers, tokens or client-controlled identity.
11. SessionLifecycle is deterministic, in-memory, single-writer/server-serialized and not thread-safe.
12. This primitive does not enforce global SessionId uniqueness, one-session-per-actor policy, session capacity, expiration timeouts, reconnect grace periods or persistence.

## Implementation

- `ISessionLifecycle` and `SessionLifecycle`.
- `SessionState`: Pending, Authenticated, Disconnected, Closed.
- `CoreComposition.CreateSessionLifecycle(sessionId)`.
- NUnit coverage for constructor validation, authentication, invalid/repeated association, disconnect, close, terminal behavior and instance isolation.

## Open policy items

- Whether multiple active sessions may bind to the same ActorId.
- Whether disconnected sessions remain registered during a reconnect grace period.
- Session expiration and timeout source.
- Global session capacity and cleanup policy.
- Persistence/recovery and audit-retention period.

## Verification boundary

CI verifies the deterministic session state machine only. It does not verify credential validation, identity provider integration, connection-to-session binding, transport security, replay handling across process restarts or production reconnect behavior.
