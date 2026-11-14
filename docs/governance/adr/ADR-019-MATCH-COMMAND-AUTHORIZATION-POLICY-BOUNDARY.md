# ADR-019 — Match Command Authorization Policy Boundary

**Status:** PROPOSED — generic fail-closed Core policy gate and tests implemented on PR #29 review branch; no command-specific gameplay policy is approved here  
**Date:** 2026-11-14  
**Related:** ADR-002, ADR-004, ADR-007, ADR-012, ADR-018, `IMatchCommandAuthorizationService<TCommand>`

## Context

The Core now has three separate admission facts:
- a registered session is Authenticated and bound to a server-resolved ActorId;
- the ActorId belongs to the selected match roster;
- a command envelope has valid shape and sequence/replay admission.

Those facts do not establish that a particular command is permitted in the current match phase or player lifecycle state. `MatchLifecycle` exposes `MatchPhase`, and `VitalityAccount` exposes `Alive`, `Spirit` and `Eliminated`, but the application still owns command-specific rules.

ADR-004 and the game-rule/system documents are PROPOSED FOR PLAYTEST or PROPOSED, not final approvals. They contain candidate rules such as Spirit capability limits, combat preconditions, the three-player surrender vote and Suffering Transfer eligibility, but they do not authorize treating those candidates as production policy. Hard-coding any single interpretation into a generic Core gate would silently promote playtest proposals.

## Proposed boundary

1. A match command authorization service is composed with the same server-owned session registry, match lifecycle, match player registry and match-scoped admission gate.
2. The application must explicitly provide an `IMatchCommandAuthorizationPolicy<TCommand>`; Core supplies no allow-all/default policy.
3. A missing policy is a construction error. A policy result that is uninitialized/default fails closed as InvalidState.
4. Core builds an immutable `MatchCommandAuthorizationContext<TCommand>` from server-resolved facts: SessionId, ActorId, MatchId, current MatchPhase, current PlayerLifecycleState, server-received tick and the command payload.
5. Client-supplied ActorId, SessionId, MatchId, phase or lifecycle state are never used as authoritative context values.
6. The policy is evaluated before queue admission, after session/match-membership resolution and before sequence/replay capacity is consumed.
7. If the policy rejects before admission, the command is not enqueued and its sequence is not consumed. The caller may retry the same sequence after the server-side condition changes, subject to the regular queue/replay rules.
8. Immediately after dequeue and before the command is handed to a gameplay handler, Core resolves current session, actor, match phase and player lifecycle facts again and evaluates the policy again.
9. If the policy rejects at handoff because phase or player state changed while queued, the command is discarded and never returned to the gameplay handler. Its already-admitted sequence remains consumed.
10. The policy must be deterministic with respect to its supplied context, side-effect-free, synchronous and free of I/O. It must not mutate player, match, queue, cooldown, score, event history or other gameplay state.
11. Each match scope must use one consistent MatchId across the match lifecycle, player roster and admission service.
12. Command payload schema/range validation, rate limits, target eligibility and the actual gameplay transaction remain in later typed application handlers unless separately added to a dedicated validation layer.
13. The Core gate remains single-writer/server-serialized and is not thread-safe.

## What this ADR does not approve

This ADR does not approve or implement:
- which commands Alive, Spirit or Eliminated players may issue;
- which commands are allowed in Created, Waiting, Starting, Running, VoteOpen, Ending or Completed;
- whether Spirit may send environmental hints, gather Echo Fragments or perform any other proposal-specific action;
- combat, hit validation, movement, cooldown or weapon rules;
- Collapse timing, Vitality decay rates or debuff effects;
- Suffering Transfer eligibility/cost/cooldown/atomic application;
- voting trigger, quorum, timeout or outcome;
- spectator, reconnect, cross-match membership or multi-session policy;
- any game-rule candidate from ADR-004 until owner review and playtest approval.

Those rules require explicit game-design approval and corresponding policy tests before an application policy is considered production-ready.

## Rejection and mutation invariants

- Invalid envelope: InvalidArgument; policy is not invoked.
- Unknown/non-authenticated session or non-member actor: reject before policy; queue and sequence are unchanged.
- Policy denial before admission: return the policy failure; queue and sequence are unchanged.
- Policy returns a default/uninitialized Result: reject with InvalidState.
- Policy succeeds: the existing match membership gate then performs its own trusted admission checks and sequence/queue admission.
- Session disconnect, membership removal, phase change or lifecycle change before dequeue handoff: do not return the command to the gameplay handler.
- Policy code is side-effect-free; rejected decisions do not mutate match/player state.
- A dequeue-time rejection does not refund the accepted sequence or put the command back into the queue.

## Implementation and tests

- `MatchCommandAuthorizationContext<TCommand>` carries trusted actor/session/match/phase/lifecycle/received-tick facts.
- `IMatchCommandAuthorizationPolicy<TCommand>` is application-supplied; no default policy exists.
- `IMatchCommandAuthorizationService<TCommand>` and `MatchCommandAuthorizationService<TCommand>` evaluate the injected policy before queue admission and before handler handoff.
- `CoreComposition.CreateMatchCommandAuthorizationService(...)` constructs the session/membership inner gates and requires the explicit policy.
- NUnit coverage includes default envelope rejection, unknown/non-member session rejection before policy invocation, fail-closed uninitialized policy results, no sequence consumption on policy denial, trusted context contents, explicit success, match-scope consistency, and phase/lifecycle/session revalidation before handoff.

## Verification boundary

A passing CI run verifies the generic deterministic policy gate and its tests only. It does not verify any particular gameplay rule. ADR-019 remains PROPOSED until owner approval of command taxonomy and phase/lifecycle permissions, followed by application-policy tests and playtest where required.
