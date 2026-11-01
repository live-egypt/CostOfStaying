# Core CI — Session Lifecycle and Consolidated Core Baseline

**Date:** 2026-11-01  
**Source commit:** `94c5789b0e82493290ca85133fc692e83ffd9884`  
**Workflow run:** `41331400978` — SUCCESS  
**Branch:** `feature/core-lifecycle-resolution`

## Verification

- Core dependency boundary: PASS — no UnityEngine/FishNet references in Core.
- Core composition root: PASS — session lifecycle, player registry, aggregates and command services are constructed through CoreComposition (the bounded queue owns its private tracker).
- .NET 10 SDK restore: PASS.
- Core Release build: PASS — target .NET Standard 2.1.
- NUnit test project Release build: PASS — target .NET 10.
- NUnit: **148 passed, 0 failed, 0 skipped**.
- TRX artifact: `13092964341`.
- Artifact SHA-256: `b4067bbd6c6b0492a245dea4c8970acca23d7196d398018f5266da42709b4295`.

## Verified Core inventory

- Result / Result<T> and typed Core error codes.
- Typed ulong ActorId, MatchId, SessionId, VictimId, EventSequenceId and CommandSequenceId.
- VitalityAccount lifecycle under ADR-007: Alive / Spirit / Eliminated.
- BetrayalLedger replay, ordering and score invariants.
- MatchLifecycle state machine.
- SessionLifecycle: Pending → Authenticated → Disconnected/Closed; disconnected sessions cannot be reauthenticated.
- MatchPlayerRegistry: bounded match-scoped membership, duplicate rejection, immutable ordered ID snapshots and membership-only removal.
- CoreComposition factories for session lifecycle, player registry, match lifecycle, vitality accounts, tracker and queue.
- ServerTick and overflow-safe StateRevision.
- Server-side CommandEnvelope.
- Bounded per-session CommandSequenceTracker.
- Bounded FIFO CommandAdmissionQueue with replay-aware admission and safe session-state reset after queue drain.

## Policy and integration boundary

ADR-008, ADR-009 and ADR-010 remain PROPOSED because production sequence/session/queue limits, reconnect/multi-session policy and spectator/match capacity policy still require owner review.

This run verifies standalone Core only. It does not verify credential validation, identity-provider integration, connection-to-session mapping, Unity Editor import/compile, FishNet API compatibility, Android builds, Host/Dedicated Server behavior, persistence, performance or production security.
