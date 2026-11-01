# Core Session Lifecycle — CI Verification

**Date:** 2026-11-01  
**Repository:** `live-egypt/CostOfStaying`  
**Branch:** `feature/core-lifecycle-resolution`  
**Source commit:** `be67d8a3399f99c2c0e05a37eec8ffa0cb2abb13`  
**Workflow:** Core CI  
**Run:** `41331236864` — SUCCESS  
**TRX artifact:** `13093536721`  
**Artifact SHA-256:** `2f9e1fae28c3bf7526b33a9e11942439885d95b345df0c60797c8c93f90e59c1`

## Verification results

- Core Unity/FishNet dependency-boundary scan: PASS.
- CoreComposition construction-root scan, including SessionLifecycle: PASS.
- .NET 10 SDK setup and restore: PASS.
- Core Release build targeting .NET Standard 2.1: PASS.
- NUnit test project Release build targeting .NET 10: PASS.
- NUnit: **148 passed, 0 failed, 0 skipped**.
- Test duration: 84 ms (reported by NUnit runner).

## Implemented session lifecycle

The verified Core primitive consists of:

- `ISessionLifecycle` contract.
- `SessionLifecycle` implementation.
- `SessionState`: Pending, Authenticated, Disconnected, Closed.
- One immutable, non-zero SessionId per instance.
- `TryAuthenticate(ActorId)`: valid actor binding exactly once while Pending.
- `TryDisconnect()`: only from Authenticated.
- `TryClose()`: from Pending, Authenticated or Disconnected; Closed is terminal.
- Actor binding remains available after disconnect/close for server-side cleanup correlation.
- Disconnected sessions cannot be re-authenticated or rebound.

CoreComposition creates the lifecycle through `CreateSessionLifecycle(SessionId)`. The CI construction-root guard includes SessionLifecycle.

## Test coverage

The 13 SessionLifecycle tests cover invalid session IDs, initial Pending state, actor association, invalid/duplicate authentication, disconnect guards, actor retention, no re-authentication after disconnect, close from each non-terminal state, terminal close and instance isolation. A CoreComposition test verifies the factory.

## Decision and scope boundary

ADR-010 remains **PROPOSED** pending owner review. The primitive intentionally does not implement:
- Credential/password/token validation.
- Transport connection handles or connection-to-session mapping.
- Global session registry or maximum concurrent sessions.
- Authentication provider integration or authorization.
- Reconnect tokens, grace periods, timeouts or concurrent-session policy.
- Persistence or recovery.

This successful run verifies the standalone Core implementation and its NUnit tests only. It does not verify Unity, FishNet, network authentication, Host/Dedicated Server behavior or production security.
