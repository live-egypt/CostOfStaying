# Core Foundations — CI Verification

**Date:** 2026-11-01  
**Branch:** `feature/core-lifecycle-resolution`  
**Source commit:** `64630225b2680fadcc412878b6ecf7ab76e23c11`  
**Core CI run:** `41326282217` — SUCCESS  
**Job:** Build and test standalone Core — SUCCESS

## Results

- Restore: PASS.
- Core Release build: PASS.
- Test project Release build: PASS.
- NUnit: **55 passed, 0 failed, 0 skipped**.
- TRX artifact upload: PASS.

Latest retained artifact:
- ID: `13091282585`
- Name: `core-test-results`
- SHA-256: `dd8f338a9915be5aa61c26730b2f28d3d7f13f2f4bdc03cf16f83760de7a4912`

## Source scope included in this run

- ADR-007 lifecycle implementation: Alive / Spirit / Eliminated.
- Automatic Alive → Spirit transition at zero Vitality.
- Explicit TryResurrect and TryEliminate operations.
- MatchId and SessionId typed ulong identifiers.
- Existing ActorId remains the single canonical player-actor identity; no duplicate PlayerId type was introduced.
- Result and Result<T> contracts with CoreErrorCode, stable success/failure semantics and safe default failure behavior.
- NUnit coverage for results, identifiers, lifecycle and Betrayal ledger.

## Verification boundary

This run verifies only standalone .NET Core and NUnit for the exact source commit above. It does not verify Unity/FishNet integration, Android, server networking, authentication, gameplay balance, persistence or production security.

The subsequent branch commits after this source commit are documentation-only. Any further source change requires a new CI run.
