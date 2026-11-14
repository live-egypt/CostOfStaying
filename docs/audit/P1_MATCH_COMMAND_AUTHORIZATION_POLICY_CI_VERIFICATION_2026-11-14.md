# P1.4 — Match Command Authorization Policy Boundary CI Verification

**Date:** 2026-11-14  
**Status:** VERIFIED — generic pure-C# policy gate only; no gameplay policy approved  
**Pull request:** [PR #29](https://github.com/live-egypt/CostOfStaying/pull/29)  
**Stacked base:** `fix/p1-match-membership-command-admission-2026-11-14` (PR #28 / ADR-018)  
**Tested source SHA:** `7dc7f1ceb1fd4dc9cccb6fd5a61ed036b41e17d5`  
**Workflow run:** [43602883504 — Core CI](https://github.com/live-egypt/CostOfStaying/actions/runs/43602883504)  
**Job:** [Build and test standalone Core](https://github.com/live-egypt/CostOfStaying/actions/runs/43602883504/job/132471246090)  
**Test-results artifact:** `core-test-results`, artifact ID `14117245816` (not expired at verification time).

## GitHub Actions results

- Core dependency-boundary scan: PASS.
- CoreComposition construction-root scan: PASS.
- .NET 10 setup and restore: PASS.
- Core Release build: PASS — 0 warnings, 0 errors.
- Test project Release build: PASS — 0 warnings, 0 errors.
- NUnit: **234 passed, 0 failed, 0 skipped**.
- Test-results artifact upload: PASS.

## Implemented contract

- `MatchCommandAuthorizationContext<TCommand>` is an immutable context containing server-resolved SessionId, ActorId, MatchId, MatchPhase, PlayerLifecycleState, server-received tick and the command.
- `IMatchCommandAuthorizationPolicy<TCommand>` is explicitly injected by the application; there is no allow-all or permissive default.
- A missing policy is a composition error.
- `IMatchCommandAuthorizationService<TCommand>` / `MatchCommandAuthorizationService<TCommand>` compose with the authenticated-session and match-roster admission gates.
- Before queue admission, Core re-resolves session and roster facts, builds trusted context, and evaluates the policy. Rejection leaves the queue and command sequence unchanged.
- The same sequence can be retried after the application policy allows the command.
- Before handler handoff, Core re-evaluates the policy against current phase and player lifecycle. A command queued while allowed is discarded if the current policy rejects it after a phase or lifecycle change.
- A disconnected/unauthenticated session is rejected by the inner authenticated gate before the policy is invoked.
- An uninitialized/default Result from policy fails closed as InvalidState.
- A policy exception fails closed as Unavailable; it does not enqueue the command or consume its sequence.
- CoreComposition enforces consistent MatchId across lifecycle, player roster and membership gate.
- The CI construction-root guard and CoreComposition tests include the new service.

## Test coverage

The 234-test suite includes:
- default envelope and unknown/non-member rejection before policy invocation;
- trusted context contents (session, actor, match, phase, lifecycle, received tick and command);
- policy denial without queue/sequence consumption and retry with the same sequence;
- uninitialized policy result failure;
- policy exception failure and same-sequence retry;
- successful explicit policy approval;
- phase change after admission;
- player lifecycle change after admission (Alive to Spirit);
- session disconnect after admission;
- missing policy and mismatch of match scopes through CoreComposition.

## CI repair history

The first PR #29 run exposed a missing test-policy helper in CoreComposition tests; the test project did not compile. The helper was added to the intended P1.4 branch. A subsequent successful run, 43602883504, tested source SHA `7dc7f1ceb1fd4dc9cccb6fd5a61ed036b41e17d5` and passed all 234 NUnit tests.

## Explicit verification limits

This verifies the generic fail-closed authorization gate only. It does not implement or approve:
- command-specific permissions for Alive, Spirit or Eliminated;
- the allowed-command matrix for Created, Waiting, Starting, Running, VoteOpen, Ending or Completed;
- Spirit, combat, movement, Collapse, voting or Suffering Transfer rules;
- payload schema/range validation, rate limits, target eligibility, cooldowns or gameplay transactions;
- credential validation, identity provider, transport/FishNet integration, Unity, Android or dedicated-server behavior.

ADR-019 remains **PROPOSED**. ADR-004 remains **PROPOSED FOR PLAYTEST**. A concrete application policy still requires owner-approved command taxonomy and phase/lifecycle rules, then its own positive, negative and no-partial-mutation tests.
