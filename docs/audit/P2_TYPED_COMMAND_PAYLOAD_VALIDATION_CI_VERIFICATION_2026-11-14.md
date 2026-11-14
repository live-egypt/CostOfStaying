# P2.1 — Typed Command Payload Validation Gate CI Verification

**Date:** 2026-11-14  
**Status:** VERIFIED — generic pure-C# validation boundary only; no concrete gameplay DTO catalog approved  
**Pull request:** [PR #31](https://github.com/live-egypt/CostOfStaying/pull/31)  
**Stacked base:** `fix/p1-command-authorization-gate-2026-11-14` (PR #29 / ADR-019)  
**Tested source SHA:** `e1941c6fdf6283827a0fd39f664ced7863e94a94`  
**Workflow run:** [43603511612 — Core CI](https://github.com/live-egypt/CostOfStaying/actions/runs/43603511612)  
**Job:** [Build and test standalone Core](https://github.com/live-egypt/CostOfStaying/actions/runs/43603511612/job/132473102136)  
**Test-results artifact:** `core-test-results`, artifact ID `14117630467` (not expired at verification time).

## GitHub Actions results

- Core dependency-boundary scan: PASS.
- CoreComposition construction-root scan: PASS.
- .NET 10 setup and restore: PASS.
- Core Release build: PASS — 0 warnings, 0 errors.
- Test project Release build: PASS — 0 warnings, 0 errors.
- NUnit: **242 passed, 0 failed, 0 skipped**.
- Test-results artifact upload: PASS.

## Implemented contract

- `ICommandPayloadValidator<TCommand>` is an explicit application-supplied validator; no permissive/default validator exists.
- `IMatchCommandPayloadValidationService<TCommand>` and `MatchCommandPayloadValidationService<TCommand>` wrap the already-authorized match command gate.
- Before queue admission, the typed payload is validated; rejection leaves queue capacity and command sequence unchanged.
- The same sequence may be retried after the payload becomes valid.
- After dequeue and authorization revalidation, the typed payload is validated again before the command is returned to an application handler.
- Revalidation detects mutation of a reference payload while it is waiting in the queue; DTOs should still be immutable.
- Default/uninitialized validator results fail closed as InvalidState.
- Validator exceptions fail closed as Unavailable.
- `CoreComposition.CreateMatchCommandPayloadValidationService(...)` requires both an authorization service and an explicit validator.
- The CI construction-root guard and CoreComposition tests include the new service.

## Test coverage

- Default envelope rejection without validator invocation.
- Payload rejection before admission without queue/sequence consumption.
- Retry using the same sequence after payload validation succeeds.
- Uninitialized validator result failure.
- Validator exception failure and same-sequence retry.
- Payload mutation after admission and rejection after dequeue.
- Successful authorization, validation and authenticated handoff.
- Explicit validator and CoreComposition wiring.

## Verification limits

This gate validates an already-deserialized typed command. It does not protect the transport parser from oversized wire frames or unsafe deserialization; raw frame-size, nesting/depth, collection/string and decompression quotas must be enforced by the transport adapter before constructing `TCommand`.

No concrete Movement, Attack, Suffering Transfer, Vote or Spirit DTO, serialization schema, versioned wire contract, numeric range table, gameplay handler, or state mutation is implemented by this PR. The game-rule documents remain PROPOSED/PROPOSED FOR PLAYTEST. ADR-020 remains **PROPOSED**.

The generic validator is structural/static only. Dynamic session/match membership and phase/lifecycle permissions are handled by earlier gates. Distance, cooldown, capacity, target eligibility, ownership and atomic gameplay effects remain application-handler responsibilities.

See [PR #31](https://github.com/live-egypt/CostOfStaying/pull/31) for the review diff and [Core CI run 43603511612](https://github.com/live-egypt/CostOfStaying/actions/runs/43603511612) for the executable verification evidence.
