# Command Envelope, Time Values and Sequence Admission — CI Verification

**Date:** 2026-11-01  
**Source commit:** `ce995ff0dfab7bacd012ca9a472cf9040cd6fa84`  
**Core CI run:** `41327308752` — SUCCESS  
**Branch:** `feature/core-lifecycle-resolution`

## Verification results

- Core dependency-boundary scan: PASS.
- .NET 8 restore: PASS.
- Core Release build: PASS.
- NUnit test project Release build: PASS.
- NUnit: **103 passed, 0 failed, 0 skipped**.
- TRX artifact upload: PASS.

Artifact:
- ID: `13091746610`
- Name: `core-test-results`
- SHA-256: `7b4e306d63d34a52e65c0e71888cf39915dcb25db5b1da4320862c67f1f9593f`

## Verified source additions

- `CommandSequenceId`: non-zero ulong session-scoped sequence identity.
- `ServerTick`: explicit server-assigned tick; zero is valid.
- `StateRevision`: zero initial revision and overflow-safe TryAdvance.
- `CommandEnvelope<TCommand>`: server-side envelope containing resolved SessionId, sequence, receive tick and command intent; default structs are detectable as invalid.
- `CommandSequenceTracker`: per-session strict monotonic sequence acceptance, configurable positive maximum jump, duplicate/stale/future-jump rejection, session reset and independent session windows.
- `CoreComposition`: factory for sequence trackers with explicit maximum jump.

## Policy status — ADR-008 PROPOSED

The code and deterministic behavior are CI-verified, but ADR-008 remains PROPOSED pending owner review. In particular, the production MaximumSequenceJump, reconnect/session continuity, queue capacity and backpressure remain unspecified. The implementation consumes an accepted sequence before later gameplay-rule validation; this policy is documented and awaits review.

## Remaining Core scope

The full command admission/validation pipeline and bounded queue are not implemented. Session lifecycle, match/player/world aggregate ownership, movement simulation, Debuff rules and atomic transfer, snapshots and match outcome resolution also remain unimplemented.

This run verifies standalone Core only. It does not verify Unity, FishNet, Android, network authentication, Host/Dedicated Server or production deployment.
