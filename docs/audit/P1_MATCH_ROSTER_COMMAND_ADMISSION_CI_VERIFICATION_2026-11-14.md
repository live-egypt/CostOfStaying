# P1.3 — Match Roster Command Admission CI Verification

**Date:** 2026-11-14  
**Status:** VERIFIED — standalone pure-C# Core scope only; PR remains unmerged  
**Pull request:** [PR #28](https://github.com/live-egypt/CostOfStaying/pull/28)  
**Stacked base:** `docs/p1-authentication-boundary-proposal-2026-11-14` (PR #27 / ADR-017)  
**Tested source SHA:** `dd853f02ccea94fe12d46f13d586ee56dbd13fb7`  
**Workflow run:** [43601883483 — Core CI](https://github.com/live-egypt/CostOfStaying/actions/runs/43601883483)  
**Job:** [Build and test standalone Core](https://github.com/live-egypt/CostOfStaying/actions/runs/43601883483/job/132468189573)  
**Test-results artifact:** `core-test-results`, artifact ID `14116607266` (not expired at verification time).

## GitHub Actions results

- Core dependency-boundary scan: PASS.
- CoreComposition construction-root scan: PASS.
- .NET 10 setup and restore: PASS.
- Core Release build: PASS — 0 warnings, 0 errors.
- Test project Release build: PASS — 0 warnings, 0 errors.
- NUnit: **222 passed, 0 failed, 0 skipped**.
- Test-results artifact upload: PASS.

## Implemented contract

- `IMatchCommandAdmissionService<TCommand>` defines a match-scoped admission boundary.
- `MatchCommandAdmissionService<TCommand>` wraps the authenticated-session admission service for one match roster.
- The match scope is server-selected; the client does not establish membership by supplying MatchId or ActorId.
- Before queue admission, Core requires a registered Authenticated session with a valid server-resolved ActorId and verifies that the ActorId exists in this exact match's `IMatchPlayerRegistry`.
- A non-member is rejected before queue capacity or sequence/replay state is consumed.
- The same sequence can be retried after the server establishes membership.
- After dequeue and before handing an `AuthenticatedCommand<TCommand>` to the gameplay handler, membership is checked again.
- A player removed from the roster while a command is queued cannot receive that command through this gate.
- Session disconnection after admission remains rejected by the wrapped authenticated admission gate.
- Multiple sessions bound to the same ActorId remain possible; the gate does not impose the unresolved multi-session policy.
- CoreComposition constructs the inner authenticated service and match-specific wrapper using the same SessionRegistry and queue.
- Core CI's construction-root guard now includes `MatchCommandAdmissionService`.

## Test coverage

The 222-test suite includes cases for:
- invalid/default envelopes and unknown sessions;
- Pending sessions and non-member ActorIds;
- no sequence consumption on pre-admission rejection, followed by retry using the same sequence after membership/authentication is established;
- membership in another MatchId not granting access to this match;
- successful admission and server-resolved ActorId handoff;
- removal from the roster after admission but before dequeue handoff;
- disconnect after admission;
- multiple sessions for one ActorId remaining allowed pending policy approval;
- composition through CoreComposition.

## CI repair history

Two earlier attempts exposed test-only defects before the final successful run:
- The first attempt did not import the service namespace and discarded the match-registry output in one test; the test project did not compile.
- The next attempt had a duplicate `TryAuthenticate` call in the Pending-session retry test. The lifecycle correctly rejected the second authentication transition.
Both defects were corrected. The final run 43601883483 on `dd853f02ccea94fe12d46f13d586ee56dbd13fb7` passed all 222 tests.

## Explicit verification limits

This is verification of a deterministic pure-C# Core membership gate only. It does not implement or verify:
- credential validation, account identity provider or server-side account-to-ActorId assignment;
- real transport/FishNet routing or production connection-ID allocation;
- one-session-per-ActorId or cross-match concurrent membership policy;
- MatchPhase permissions, Alive/Spirit/Eliminated command permissions, spectator rules or gameplay command authorization;
- payload schema/size/range validation, movement, cooldowns, target eligibility or gameplay mutation;
- Unity Editor/PlayMode, Android, Linux Dedicated Server, network behavior, load/performance or production security.

ADR-018 remains **PROPOSED** pending owner review. A passing membership gate must be followed by command-specific application authorization; membership is not permission to execute every command.
