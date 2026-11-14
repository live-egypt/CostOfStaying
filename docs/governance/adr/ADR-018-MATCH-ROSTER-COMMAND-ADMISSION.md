# ADR-018 — Match Roster Command Admission

**Status:** PROPOSED — Core membership gate and NUnit coverage implemented on PR #28 review branch; owner review and gameplay authorization policy remain pending  
**Date:** 2026-11-14  
**Related:** ADR-002, ADR-009, ADR-012, ADR-016, `IMatchCommandAdmissionService<TCommand>`, `MatchCommandAdmissionService<TCommand>`

## Context

The authenticated command admission gate establishes that a server-resolved session is currently Authenticated and bound to a valid ActorId. A separate match-scoped player registry establishes membership of ActorIds in one MatchId.

Those two valid facts must be joined before a command can enter a particular match's queue. Otherwise, an authenticated session whose ActorId is not a member of the selected match could be handed to that match's gameplay handler if application routing omits the membership check.

The command queue and CoreComposition are match-scoped by design. The match-specific admission gate can therefore use one fixed roster and require membership without embedding MatchId in client-controlled command payloads.

## Proposed contract

1. The application creates one `IMatchCommandAdmissionService<TCommand>` per match scope, passing the same server-side `ISessionRegistry`, that match's `IMatchPlayerRegistry`, and that match's bounded command queue through `CoreComposition`.
2. The service exposes its trusted `MatchId`; the match scope is selected by server-side routing. A client-supplied MatchId is never accepted as proof of membership or permission.
3. At admission, the service validates the envelope, resolves the registered SessionId, requires the session to be Authenticated with a valid server-bound ActorId, and requires that ActorId to exist in this match's player registry.
4. A session that is absent, Pending, Disconnected, Closed or lacks a valid ActorId is rejected before sequence/replay admission.
5. An authenticated ActorId that is not a member of this match is rejected with Unauthorized before queue capacity or sequence/replay state is consumed.
6. If membership is valid, the existing authenticated-session admission service applies its existing bounded queue and sequence/replay rules.
7. Immediately after dequeue and before returning an `AuthenticatedCommand<TCommand>` to the application handler, the service rechecks that the actor remains a member of this match. A removed actor's queued command is discarded with Unauthorized; its accepted sequence remains consumed.
8. Membership is checked against the ActorId resolved from the server-owned session. The client does not supply an authoritative ActorId.
9. The registry/account returned for an ActorId must be non-null and its PlayerId must equal that ActorId; malformed membership data fails closed with InvalidState.
10. All calls remain single-writer/server-serialized. The service is not thread-safe and does not create an atomic transaction spanning arbitrary application handlers.
11. Player membership removal is independent of player lifecycle. This gate does not automatically remove Spirit or Eliminated players from the roster.
12. Distinct authenticated sessions for the same ActorId are not rejected by this gate. It does not impose one-session-per-actor policy.

## Explicit non-goals

This decision does not:
- authenticate credentials or prove a transport connection owns a SessionId;
- decide which identity provider or account-to-ActorId mapping is used;
- authorize command types, player lifecycle states, spectator behavior or match phases;
- define whether an ActorId may be a member of multiple matches simultaneously;
- define whether Spirit or Eliminated players may issue specific commands;
- validate payload schema, size, movement ranges, cooldowns, target eligibility or other gameplay rules;
- execute or mutate gameplay state;
- provide cross-thread safety, concurrent writers or transactional roster locking;
- implement FishNet or other transport routing.

Those checks remain the responsibility of later server application command handlers and separately reviewed policy decisions.

## Rejection and mutation invariants

- Invalid envelope: InvalidArgument; no queue or sequence mutation.
- Unknown session: propagate NotFound; no queue or sequence mutation.
- Non-authenticated session or invalid ActorId: Unauthorized; no queue or sequence mutation.
- Actor not in this match: Unauthorized; no queue or sequence mutation, and the same sequence may be retried after the server establishes valid membership.
- Queue full or replay violation: preserve existing admission-service behavior.
- Actor removed after admission but before dequeue handoff: dequeue and reject; do not hand the command to a gameplay handler.
- Actor membership restored after a rejection: a subsequent valid command may be admitted according to the existing session sequence and queue rules.
- A successful dequeue returns the server-resolved ActorId and the command only while that actor remains in the match roster.

## Implementation and tests

- `IMatchCommandAdmissionService<TCommand>` exposes MatchId, TryAdmit and TryTakeNext.
- `MatchCommandAdmissionService<TCommand>` wraps authenticated admission with roster checks at admission and dequeue.
- `CoreComposition.CreateMatchCommandAdmissionService(...)` creates the wrapper and inner admission service from the same SessionRegistry and queue.
- NUnit coverage includes unknown sessions, Pending sessions, non-member actors, membership retry without sequence consumption, cross-match isolation, successful membership admission, dequeue-time membership revalidation, multiple sessions for one ActorId, and composition construction-root enforcement.

## Verification boundary

Core CI on PR #28 must verify the pure-C# contract and tests. A passing test suite does not approve match-phase rules, Spirit/Eliminated permissions, cross-match concurrency policy or real transport routing. ADR-018 remains PROPOSED until owner review and gameplay authorization rules are separately approved.
