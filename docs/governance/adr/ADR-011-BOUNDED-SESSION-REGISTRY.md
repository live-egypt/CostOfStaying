# ADR-011 — Bounded Server Session Registry

**Status:** PROPOSED — implementation and tests exist; owner review required  
**Date:** 2026-11-01  
**Related:** ADR-005, ADR-008, ADR-010, `ISessionRegistry`, `SessionRegistry`

## Context

The Core has a deterministic lifecycle primitive for one SessionId. A server process also needs bounded in-memory ownership of active/pending session lifecycle instances. This registry must not invent credential, transport, reconnect or per-actor concurrent-session policy.

## Proposed contract

1. The registry is constructed with an explicit positive capacity; no default production limit is selected.
2. Entries are keyed only by valid, non-zero server-issued SessionId.
3. Only Pending sessions may be registered; authentication must happen after registration through the trusted server application boundary.
4. Duplicate SessionId registration returns Conflict without replacing the existing session.
5. Capacity exhaustion returns CapacityExceeded without mutation.
6. Lookup rejects invalid IDs and returns NotFound for absent IDs.
7. The public `ISessionRegistry` contract does not expose removal. The internal removal operation is reachable only through `SessionCleanupService`, which requires Closed state, drains queued commands and resets replay state before removal.
8. Successful cleanup releases one registry capacity slot.
9. Snapshots preserve registration order and are immutable copies.
10. The registry does not enforce uniqueness of ActorId or decide multi-session policy; that remains an application/backend decision.
11. The registry does not own connection-to-session mapping, credentials, authorization, reconnect tokens, timeout/expiry, persistence or recovery.
12. All operations are single-writer/server-serialized; no thread-safety guarantee is made.

## Implementation

- `ISessionRegistry` and `SessionRegistry`.
- `CoreComposition.CreateSessionRegistry(capacity)`.
- Bounded dictionary plus deterministic insertion-order list.
- NUnit tests for capacity, validation, duplicate handling, state guards, closed-only removal, snapshots and isolation.

## Verification boundary

A passing Core CI run verifies only the in-memory registry behavior. It does not approve the production capacity or multi-session/reconnect policy and does not verify transport authentication.
