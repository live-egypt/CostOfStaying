# ADR-016 — Transport Connection to Session Binding

**Status:** PROPOSED — Core contract and NUnit coverage are implemented on a review branch; owner approval and transport-adapter integration are pending  
**Date:** 2026-11-14  
**Related:** ADR-008, ADR-010, ADR-011, ADR-012, ADR-013, ADR-015, `TransportConnectionId`, `IConnectionSessionRegistry`, `ConnectionSessionRegistry`

## Context

The authenticated command admission gate accepts a server-resolved `SessionId`, but the Core previously had no explicit contract describing how a transport connection is mapped to that session. A server must not accept a client-supplied `SessionId` as proof that the connection owns the session.

Transport-specific connection objects must remain outside Core. The boundary needs a bounded, testable mapping that uses an opaque identifier assigned by the trusted transport adapter.

## Proposed contract

1. The trusted transport adapter allocates a non-zero `TransportConnectionId` for each accepted connection. The ID is server-issued, opaque to the client, and must not be reused during one server-process lifetime.
2. The adapter creates and registers a Pending `SessionId`, then binds the connection to that Pending session before credential authentication so the trusted ingress can resolve the session while validating credentials.
3. While live, the mapping is one-to-one: one transport connection maps to one session, and one session maps to one transport connection.
4. Binding rejects invalid identifiers, unknown sessions, non-Pending sessions, duplicate connection IDs, duplicate session IDs, and capacity exhaustion without partial mutation.
5. Resolution returns only Pending or Authenticated sessions. Disconnected or Closed sessions are not usable connection bindings.
6. Detaching a Pending connection closes its session. Detaching an Authenticated connection transitions its session to Disconnected. Already Disconnected or Closed sessions may be detached without another lifecycle transition.
7. The lifecycle transition occurs before the mapping is removed, so queued commands fail the existing dequeue-time authentication revalidation after a disconnect.
8. Session cleanup must not remove a session while a connection mapping still references it. The Core composition root therefore wraps session cleanup with a connection-binding guard.
9. The connection registry is bounded by an explicit positive capacity and is single-writer/server-serialized; it is not thread-safe.
10. Core stores only `TransportConnectionId` and `SessionId`; no FishNet type, socket, peer address, transport connection object, token or credential is stored in this registry.

## Explicit non-goals

- This decision does not authenticate credentials or establish account identity.
- This decision does not authorize gameplay commands or match membership.
- This decision does not enforce one session per `ActorId`. Multiple distinct sessions may map to the same ActorId until a separate multi-session policy is approved.
- This decision does not define reconnect grace periods, session expiry, credential refresh, persistence, or restoration after server restart.
- This decision does not provide the FishNet adapter or prove network integration.
- The adapter must provide a fresh, non-reused connection ID; this Core registry does not allocate transport IDs.

## Implementation and tests

- `TransportConnectionId` is a typed, non-zero server-side identifier.
- `IConnectionSessionRegistry` and `ConnectionSessionRegistry` provide bounded bidirectional mapping.
- `TryDetach` transitions the session before clearing the binding.
- `ConnectionAwareSessionCleanupService` blocks session removal while a binding exists.
- CoreComposition exposes factories for the mapping and guarded cleanup.
- NUnit coverage includes invalid IDs, unknown and non-Pending sessions, duplicate binding, capacity, both lookup directions, detach transitions, multiple sessions for one ActorId, rejection of queued commands after disconnect, and cleanup blocking while bound.

## Verification boundary

A successful Core CI run verifies these deterministic pure-C# contracts only. It does not verify connection-ID allocation in FishNet, credential validation, connection hijacking resistance at the transport layer, reconnect policy, or production security. The ADR remains PROPOSED until owner review and the actual transport adapter integration are completed.
