# Bounded Session Registry — Core CI Verification

**Date:** 2026-11-01  
**Repository:** `live-egypt/CostOfStaying`  
**Branch:** `feature/core-lifecycle-resolution`  
**Source commit:** `1353b84ff1951f9c1fa8f2a3c3316bd3c89a6ac8`  
**Workflow:** Core CI  
**Run:** `41331861227` — SUCCESS  
**TRX artifact:** `13093327104`  
**Artifact SHA-256:** `e3c698e55c592137bea7df3bf8ee31ae2bafa5ecaacbef6e135313cb090598bb`

## Verification results

- Core dependency-boundary scan (no UnityEngine/FishNet references): PASS.
- CoreComposition construction-root scan, including SessionLifecycle and SessionRegistry: PASS.
- .NET 10 SDK restore: PASS.
- Core Release build targeting .NET Standard 2.1: PASS.
- NUnit project Release build targeting .NET 10: PASS.
- NUnit: **161 passed, 0 failed, 0 skipped**.
- Session registry factory is covered through CoreCompositionTests.

## Verified SessionRegistry contract

- Explicit positive capacity; no implicit production default.
- Registry keys are valid, server-issued SessionId values.
- Only Pending sessions can be registered.
- Duplicate SessionId returns Conflict without replacing the original.
- Full capacity returns CapacityExceeded without mutation.
- Invalid and unknown IDs are distinguished.
- Removal is rejected unless the session has reached Closed.
- Removing a Closed session releases capacity.
- ID snapshots preserve insertion order and are immutable copies.
- Registry instances are independent.
- The registry does not enforce ActorId uniqueness or multi-session policy.

## Implementation

- `ISessionRegistry` and `SessionRegistry`.
- `CoreComposition.CreateSessionRegistry(capacity)`.
- Bounded dictionary plus insertion-order list.
- `SessionRegistryTests` cover capacity, validation, duplicate registration, pending-only registration, lookup, closed-only removal, capacity release, immutable snapshots and instance isolation.

## Decision boundary

ADR-011 remains **PROPOSED**. Production capacity and per-actor multi-session policy remain owner/application decisions. The registry does not implement connection-to-session mapping, credentials, authorization, reconnect tokens, timeout/expiry, persistence or recovery.

This run verifies standalone pure-C# Core only. It does not verify Unity, FishNet, network authentication, Host/Dedicated Server behavior or production security.
