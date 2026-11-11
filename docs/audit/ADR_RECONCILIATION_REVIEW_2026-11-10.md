# ADR and Core Contract Reconciliation Review — 2026-11-10

**Revision:** 2026-11-10  
**Decision Status:** PROPOSED — review findings; no ADR decision changed  
**Evidence Maturity:** REVIEWED — ADR text, selected Core source files, NUnit tests, GitHub Actions job logs and artifact metadata  
**Repository:** live-egypt/CostOfStaying  
**Branch scope:** main at 3dd9cfc27aa48b09b3cde802e266a8221c39f0d3 and feature/core-lifecycle-resolution at 2820285021b1a3d7ae9acd953b3bb65677c33512.

## 1. Review boundaries

This review separates:
- ADR status recorded on a specific branch.
- Whether the implementation described by the ADR exists in the inspected Core source.
- Whether tests exercise the implementation.
- Whether the original owner approval event is directly available.
- Whether an ADR has unresolved operational/application policy.

A branch ADR marked APPROVED is not silently copied to main. A passing CI job does not approve a PROPOSED ADR. A code implementation does not settle an unapproved policy.

## 2. Decision/ADR inventory across refs

| ADR | Main status | Core feature-branch status | Review interpretation |
|---|---|---|---|
| ADR-001 — Tech Stack | PROPOSED | Same proposal | No exact Unity/FishNet package lock or verified runtime prototype established. FishNet 4.7.4R remains unverified. |
| ADR-002 — Server Authority | APPROVED as a principle | Same principle | Decision approval is separate from transport/application enforcement. End-to-end authority is not verified. |
| ADR-003 — Backend/Hosting | PROPOSED | Same proposal | No production backend/hosting provider selected. |
| ADR-004 — Gameplay Baseline | PROPOSED FOR PLAYTEST | Same gameplay baseline, with lifecycle dependency on ADR-007 | Requires the documented playtest gate; no final gameplay approval inferred. |
| ADR-005 — Core Boundary | Not present in inspected main ADR set | APPROVED in branch ADR; approval date asserted as 2026-11-01 | Pure Core / thin adapters approach recorded on feature branch. Original approval event remains a separate traceability task. |
| ADR-006 — Identifier and Ledger Invariants | Not present in inspected main ADR set | PROPOSED | Ledger implementation and tests exist; proposal status remains PROPOSED. |
| ADR-007 — Player Lifecycle | Not present in inspected main ADR set | APPROVED in branch ADR | Branch canonical lifecycle is Alive / Spirit / Eliminated. Exact original owner approval message is not present in the inspected source trace, so retain branch ADR's stated status but keep the approval-event mapping open. |
| ADR-008 — Command Envelope / Sequence Admission | Not present in inspected main ADR set | PROPOSED | Sequence tracker and bounded queue implementation/tests exist; production limits and reconnect policy remain open. |
| ADR-009 — Match-Scoped Player Registry | Not present in inspected main ADR set | PROPOSED | Bounded registry implementation/tests exist; capacity, spectator/Spirit slot and reconnect retention remain policy items. |
| ADR-010 — Server Session Lifecycle | Not present in inspected main ADR set | PROPOSED | Deterministic session primitive/tests exist; credentials, connection mapping, timeout and reconnect production policy remain outside Core. |
| ADR-011 — Bounded Session Registry | Not present in inspected main ADR set | PROPOSED | Bounded in-memory registry/tests exist; production capacity and lifecycle policy remain open. |
| ADR-012 — Authenticated Command Admission | Not present in inspected main ADR set | PROPOSED | Core admission gate/tests exist; it is not credential authentication or proof of connection ownership. |
| ADR-013 — Safe Session Cleanup | Not present in inspected main ADR set | PROPOSED | Cleanup order and tests exist; in-flight handler completion remains the caller's responsibility. |
| ADR-014 — Victim Scope | Not present in inspected main ADR set | PROPOSED | Victim categories remain TBD; retaining VictimId does not approve NPC victims or player-only scope. |
| ADR-015 — Command Identity / Idempotency | Not present in inspected main ADR set | PROPOSED | No CommandId/idempotency result store was found in the inspected Core identifiers/services. ADR-015 explicitly says no code change; its proposed guarantee is not implemented by the existing sequence tracker. |

## 3. Original approval trace — branch-scoped decisions

### 3.1 ADR-005

The branch ADR states that project-owner approval was received on 2026-11-01. The available decision-traceability register describes Core boundary approval as branch-recorded and leaves source-event linking as a separate traceability task. This review did not recover a direct, exact user-authored approval message from the mounted source files for independent event-level confirmation.

**Treatment:** Keep the ADR's recorded status APPROVED on the feature branch. Record the original approval-event cross-reference as SOURCE TRACE PENDING until the exact message/turn can be matched. Do not infer that this approval changes gameplay rules.

### 3.2 ADR-007

The feature-branch ADR states that an earlier owner-approved Phase 1 lifecycle matrix dated 2026-10-31 12:28 UTC was reaffirmed on 2026-11-01:
- Alive, Spirit, Eliminated.
- Vitality zero automatically changes Alive → Spirit.
- Resurrect and Eliminate are explicit server-authoritative operations.
- Eliminated is terminal.

The current mounted text-file set and inspected decision-traceability record do not contain the exact original user approval event with its full message/thread reference. PR #6 description and ADR-007 repeat the approval claim, but those are repository/derived records, not the original approval message.

**Treatment:** Record APPROVED as the status asserted by ADR-007 on the feature branch, with original source event mapping still open. Keep the conflict with main v2.1 Alive → Dead wording open. Do not update main rules or lifecycle code on this basis alone.

## 4. Core source-to-ADR implementation review

| Contract area | Inspected implementation | Tests/evidence | Implementation assessment | Policy/evidence boundary |
|---|---|---|---|---|
| ADR-006 identifier invariants | Identifiers.cs, BetrayalLedger.cs | IdentifierTests.cs, BetrayalLedgerTests.cs | Implemented on feature branch | ADR-006 remains PROPOSED; its design is implemented/tested without implying owner approval. |
| ADR-007 vitality lifecycle | VitalityAccount.cs | VitalityAccountTests.cs | Implements Alive → Spirit, explicit Resurrect and terminal Eliminated | Matches branch ADR-007; conflicts with main v2.1. |
| ADR-008 sequence admission | CommandEnvelope.cs, CommandSequenceTracker.cs, CommandAdmissionQueue.cs | CommandEnvelopeTests.cs, CommandSequenceTrackerTests.cs, CommandAdmissionQueueTests.cs | Implemented on feature branch | Sequence is session-scoped and in-memory; production sequence jump, tracked-session and queue capacities are configuration. |
| ADR-009 match registry | MatchPlayerRegistry.cs | MatchPlayerRegistryTests.cs | Implemented on feature branch | Match capacity and Spirit/spectator membership remain policy. |
| ADR-010 session lifecycle | SessionLifecycle.cs | SessionLifecycleTests.cs | Implemented on feature branch | No credentials, transport binding, timeout, reconnect token, persistence or multi-session policy inside this primitive. |
| ADR-011 session registry | SessionRegistry.cs | SessionRegistryTests.cs | Implemented on feature branch | Bounded in-memory registry only; production capacity/expiry remains unresolved. |
| ADR-012 authenticated admission | CommandAdmissionService.cs, AuthenticatedCommand.cs | CommandAdmissionServiceTests.cs | Implemented on feature branch | Validates registered Authenticated session and ActorId binding, revalidates at dequeue. Does not authenticate credentials or prove connection-to-session ownership; gameplay payload validation is downstream. |
| ADR-013 safe cleanup | SessionCleanupService.cs, CommandAdmissionQueue.ResetSession, SessionRegistry.RemoveClosedSession | SessionCleanupServiceTests.cs, CommandAdmissionQueueTests.cs | Implemented on feature branch | Queue drain and replay reset precede closed-session removal. Queue does not track a dequeued/in-flight handler; caller must wait for processing completion before cleanup. |
| ADR-015 stable command identity | No CommandId type in inspected Identifiers.cs; no IdempotencyStore/command-result registry in inspected Core services | No ADR-015 implementation/test file in the PR #6 changed-file list | NOT IMPLEMENTED in inspected Core scope | Per-session sequence replay protection is not stable command identity, result replay, reconnect-safe idempotency, or restart-safe idempotency. |

### 4.1 Important boundaries confirmed by source

- CommandAdmissionService.TryAdmit validates envelope shape, resolves SessionId in the Core session registry, requires Authenticated state and a valid ActorId, then delegates to the bounded queue.
- TryTakeNext removes the next queued envelope, re-resolves the session, and returns an AuthenticatedCommand with ActorId from the current session. If the session is no longer authenticated, the dequeued command is discarded and the sequence remains consumed.
- CommandAdmissionQueue.TryEnqueue checks queue capacity before consuming a sequence. Once accepted, the sequence is consumed even if downstream gameplay validation later rejects the command.
- SessionCleanupService requires a Closed session, relies on the queue to reject reset while commands remain queued, resets sequence tracking, then removes the closed session from the registry.
- SessionCleanupService cannot prove that a command already dequeued is no longer executing. Its API documentation leaves completion synchronization to the caller. This is an explicit integration obligation, not a verified Core feature.
- SessionLifecycle and SessionRegistry are in-memory, single-writer/server-serialized primitives. They do not establish credentials, transport ownership, global identity uniqueness, durable persistence or production reconnect behavior.
- The inspected Identifiers.cs defines ActorId, MatchId, SessionId, VictimId, EventSequenceId and CommandSequenceId; it does not define CommandId.
- ADR-006's BetrayalEvent constructor rejects the event when ActorId.Value equals VictimId.Value. This is appropriate only if the permitted victim identities share the same numeric namespace as actors. ADR-014 leaves victim categories TBD, so the namespace assumption must remain explicit and unresolved until victim scope is decided.

## 5. ADR-012 / ADR-013 CI reference verification

Both branch ADR-012 and ADR-013 cite Core CI run 41332989910, commit c33faf84aa39856d46181ccc1c9decbfba2777d2.

This citation was independently checked against GitHub:
- Workflow: Core CI, completed/success.
- Job: 124926820979 — Build and test standalone Core.
- Job log: 181 passed, 0 failed, 0 skipped.
- Artifact: 13093814339 (core-test-results).
- GitHub-reported artifact digest: sha256:badb2929b1fdd4f4bbacc74405499df5c1902ca2181789f2903369db8dd5f7c1.
- At retrieval time, GitHub reported the artifact was not expired.

This is a second distinct 181/181 CI snapshot. It does not conflict with run 41333575156 on commit 8a193190...; the runs belong to different commits. Both are standalone Core evidence, not transport authentication or end-to-end server integration.

## 6. ADR-015 internal status ambiguity

ADR-015 has overall status PROPOSED, but its alternatives table labels Option B (CommandId independent plus SessionId and sequence) as "معتمد". This wording could be read as an approved decision even though the ADR header and multiple sections say the ADR remains PROPOSED and that code/tests are unchanged.

**Treatment:** Do not update the ADR without a decision-authorized edit. Record a documentation clarification candidate: if "معتمد" means the preferred proposal, use an unambiguous phrase such as "الخيار المقترح / المفضل ضمن هذا ADR"; if it means owner-approved, the ADR status and original approval event must be reconciled. No status promotion is made here.

## 7. Open reconciliation actions

| ID | Action | Priority | State |
|---|---|---:|---|
| ADR-REV-01 | Recover exact original owner approval event supporting ADR-005 | HIGH | SOURCE TRACE PENDING |
| ADR-REV-02 | Recover exact original owner approval event supporting ADR-007 lifecycle | BLOCKING | SOURCE TRACE PENDING |
| ADR-REV-03 | Resolve main Alive → Dead vs branch Alive → Spirit lifecycle conflict before any main rule/code change | BLOCKING | OPEN — OWNER DECISION REQUIRED |
| ADR-REV-04 | Confirm wording/status meaning in ADR-015 alternatives table | HIGH | OPEN — ADR DOCUMENT CLARIFICATION |
| ADR-REV-05 | Specify and test in-flight handler completion before session cleanup is invoked | HIGH | APPLICATION INTEGRATION REQUIREMENT |
| ADR-REV-06 | Define production sequence jump, queue capacity and tracked-session limits | MEDIUM | PROPOSED / TBD |
| ADR-REV-07 | Define credential authentication and trusted connection-to-session binding | BLOCKING FOR NETWORK INTEGRATION | NOT IMPLEMENTED IN CORE |
| ADR-REV-08 | Decide reconnect/multi-session/expiry/restart policy | HIGH | TBD |
| ADR-REV-09 | Design CommandId/idempotency only after ADR-015 is approved; include atomic reservation and result persistence boundary | HIGH | PROPOSED — NOT IMPLEMENTED |
| ADR-REV-10 | Keep Debuff/atomic transfer application outside current implemented Core claims until code/tests are present | HIGH | NOT IMPLEMENTED |
| ADR-REV-11 | Resolve whether ActorId and VictimId share the same numeric identity namespace before relying on numeric equality as self-target rejection | HIGH | OPEN — ADR-014 DOMAIN SCOPE TBD |

## 8. Status

| Area | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| ADR-005 Core boundary | APPROVED on feature branch | DOCUMENTED / REVIEWED | Original approval event mapping pending |
| ADR-006 invariants | PROPOSED | IMPLEMENTED / TESTED on feature branch | Approval pending |
| ADR-007 lifecycle | APPROVED on feature branch | IMPLEMENTED / TESTED on feature branch | Original approval event mapping and main conflict pending |
| ADR-008 sequence admission | PROPOSED | IMPLEMENTED / TESTED on feature branch | Production policy pending |
| ADR-009 match registry | PROPOSED | IMPLEMENTED / TESTED on feature branch | Membership policy pending |
| ADR-010 session lifecycle | PROPOSED | IMPLEMENTED / TESTED on feature branch | Authentication/reconnect integration pending |
| ADR-011 session registry | PROPOSED | IMPLEMENTED / TESTED on feature branch | Production lifecycle policy pending |
| ADR-012 authenticated admission | PROPOSED | IMPLEMENTED / TESTED on feature branch | Transport authentication not established |
| ADR-013 cleanup | PROPOSED | IMPLEMENTED / TESTED on feature branch | In-flight completion synchronization external |
| ADR-014 victim scope | PROPOSED | DOCUMENTED | Domain scope TBD |
| ADR-015 command identity/idempotency | PROPOSED | DOCUMENTED | No implementation in inspected Core scope |
| Unity/FishNet integration | PROPOSED | — | NOT IMPLEMENTED / NOT VERIFIED |
| End-to-end server authority | APPROVED as principle (ADR-002) | DOCUMENTED | NOT VERIFIED |

## 9. Explicit non-claims

This review does not:
- Promote any PROPOSED ADR to APPROVED.
- Treat branch-only ADRs as present on main.
- Resolve the main/branch lifecycle conflict.
- Claim credential authentication, transport connection ownership, gameplay authorization, CommandId idempotency, reconnect persistence or restart protection are implemented.
- Claim Unity/FishNet, Android, Linux Dedicated Server or production operation is verified.
- Authorize changes to ADRs or Core code.
