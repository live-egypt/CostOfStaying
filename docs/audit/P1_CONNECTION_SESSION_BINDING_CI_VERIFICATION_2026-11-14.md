# P1.1 — Connection-to-Session Binding CI Verification

**Date:** 2026-11-14  
**Status:** VERIFIED — standalone pure-C# Core scope only  
**Implementation branch:** `fix/p1-connection-session-binding-2026-11-14`  
**Pull request:** [PR #26](https://github.com/live-egypt/CostOfStaying/pull/26) — targets `feature/core-lifecycle-resolution`; not merged  
**Tested source commit:** `95a7bc93a7fd4772f45e7dc428f7574906945061`  
**Current branch head at report time:** `4ae4088716eb740ec736c3bea0f75727e2d6bc77`  
**Post-test difference:** seven documentation-only commits updating ADR-016 wording, the audit report, project status, Core implementation status, technical master, requirements traceability and documentation index. No source, test or workflow files changed after the tested commit.

## GitHub Actions evidence

- Workflow run: [43599420409 — Core CI](https://github.com/live-egypt/CostOfStaying/actions/runs/43599420409)
- Job: [Build and test standalone Core](https://github.com/live-egypt/CostOfStaying/actions/runs/43599420409/job/132460773242)
- Core dependency-boundary scan: PASS.
- CoreComposition construction-root scan: PASS.
- .NET 10 Release restore: PASS.
- Core Release build: PASS — 0 warnings, 0 errors.
- NUnit test-project Release build: PASS — 0 warnings, 0 errors.
- NUnit: **210 passed, 0 failed, 0 skipped**.
- Test-results artifact upload: PASS.

## Implemented scope covered by this verification

- Added a typed, non-zero `TransportConnectionId`.
- Added `IConnectionSessionRegistry` and bounded `ConnectionSessionRegistry` with bidirectional connection/session lookup.
- Binding accepts only an existing Pending session and rejects duplicate connection IDs, duplicate session bindings, invalid identifiers, missing sessions and capacity exhaustion without partial mapping mutation.
- Detach transitions Pending sessions to Closed and Authenticated sessions to Disconnected before removing the mapping.
- Commands queued before detach are rejected by the existing dequeue-time authenticated-session revalidation.
- Added a cleanup guard which prevents removal while a connection remains bound.
- CoreComposition rejects a cleanup composition if its ConnectionSessionRegistry is backed by a different SessionRegistry instance.
- Added tests for identifier validity, binding, capacity, duplicate mapping, detach lifecycle, same-ActorId/different-session behavior, stale queued command rejection, and cleanup ownership.

## Policy boundaries and limitations

This PR does **not** implement or verify:
- FishNet or another real transport adapter.
- Allocation/non-reuse of the transport connection IDs by a production adapter. The adapter is required to issue opaque server-side IDs and not reuse them during one server-process lifetime.
- Credential/token validation or external account identity.
- Whether a single ActorId may own multiple distinct sessions.
- Reconnect grace periods, expiry timers, restoration or persistence.
- Gameplay authorization, match-phase checks or payload validation.
- Unity Editor/PlayMode, Android, Linux Dedicated Server, network adversarial tests, performance or production security.

ADR-016 remains **PROPOSED**. Passing Core CI is evidence for the pure-C# contract and tests, not approval of unresolved policy or proof of transport integration.

## CI repair note

The first PR #26 run (43598979070) exposed a test dependency on `InFlightCount` from the separate, unmerged PR #25. That assertion was removed so PR #26 remains independently buildable against its stated base branch. This did not remove the disconnect behavior assertion: the test still proves that a queued command is rejected after the bound session transitions to Disconnected. The successful final run is 43599420409 on the tested source commit listed above.
