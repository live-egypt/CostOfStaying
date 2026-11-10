# Decision Traceability Register — Cost of Staying

> **Governance status axes:** Decision Status and Evidence Maturity are independent. This document's existence does not approve its claims or prove implementation. See [Project Governance Baseline](PROJECT_GOVERNANCE_BASELINE.md).

**Revision date:** 2026-11-10 — WO-DOC-01 owner authorization and attachment-snapshot reconciliation  
**Record State:** INITIAL RECONCILIATION SNAPSHOT — DOCUMENTATION ONLY; CONTENT AND SOURCE COMPLETENESS REMAIN PARTIAL
**Scope:** Owner decisions, ADR status, architectural directions, and unresolved decision conflicts visible in the current repository and supplied source records.

## 1. Rules

- Record a decision only at the status supported by its source.
- Distinguish approval to create or document an ADR from approval of the gameplay/architecture rule described by that ADR.
- An ADR with status PROPOSED remains proposed even if implementation or tests exist.
- A repository implementation does not silently supersede an owner-approved decision.
- Where source turn/date is absent or not directly rechecked, record the limitation.
- Do not collapse Stage 0–5 into Phase 1–3.

## 2. Decision status snapshot

| Decision / record | Current recorded status | Evidence basis | Boundary / open item |
|---|---|---|---|
| WO-DOC-01 — Project Documentation Governance | APPROVED — EXECUTION AUTHORIZED | Current Project conversation owner message on 2026-11-10: “نعم اعتماد والبدء بتنفيذ جميع المراحل بالترتيب”, following the explicit WO-DOC-01 approval request | Authorizes sequential documentation work packages A–E only. Does not approve the proposed governance policy wholesale, alter ADRs/gameplay, authorize code changes, merge PRs, or close stages. |
| ADR-007 — Player Lifecycle | APPROVED | Repository ADR and Project Status Register | Canonical lifecycle: Alive → Spirit at zero Vitality; explicit Resurrect; Eliminated terminal. Does not approve every gameplay rule. |
| ADR-008 — Command Envelope / Sequence Admission | PROPOSED | Repository ADR and Requirements Traceability | Implementation/tests are documented, but production policy limits and owner approval remain open. |
| ADR-009 — Match-Scoped Player Registry | PROPOSED | Repository ADR and Requirements Traceability | Capacity, spectator/Spirit treatment, reconnect grace and retention remain policy items. |
| ADR-010 — Server Session Lifecycle | PROPOSED | Repository ADR and Requirements Traceability | Credential validation, connection mapping, reconnect tokens/timeouts and production policy remain external/TBD. |
| ADR-011 — Bounded Session Registry | PROPOSED | Repository ADR and Requirements Traceability | Actor multi-session/reconnect policy remains TBD. |
| ADR-012 — Authenticated Command Admission | PROPOSED | Repository ADR and Requirements Traceability | Core admission primitive does not prove real credential authentication or transport binding. |
| ADR-013 — Safe Session Cleanup | PROPOSED | Repository ADR and Requirements Traceability | In-flight handler coordination and transport enforcement remain integration obligations. |
| ADR-014 — Victim Scope | PROPOSED | Repository ADR and prior owner approval record summarized in project context | Document creation/administrative closure does not finalize gameplay victim scope; VictimId unchanged; NPC victims not adopted. |
| ADR-015 — Command Identity and Idempotency | PROPOSED | Existing repository ADR-015 | CommandId is proposed as stable-player scoped; in-memory lifetime only; atomic reservation implementation details TBD. No Core/Tests change authorized by ADR. |
| C1 (B1+B3 preferred baseline) | PROPOSED / DESIGN DIRECTION | Historical architecture discussion summary | Not an approved ADR or verified implementation. |
| B3 Idempotency Registry | NOT IMPLEMENTED (reported) | Historical architecture review record | Separate registry direction; no implementation claim. |
| D8 InFlight | DEFERRED (reported) | Historical requirements/architecture records | Keep distinct from Gameplay requirements; exact scope remains to be source-matched. |

## 3. Stage and Phase status — separate control tracks

### Stage track

| Stage | Recorded status | Boundary |
|---|---|---|
| Stage 0 | ACCEPTED WITH DOCUMENTED CONSTRAINT | Stage 0 Negative Attestation remains binding for affected Library records. |
| Stage 1 | PARTIAL — SOURCE RECOVERY | Twenty attachment objects are currently surfaced in project conversation context (the prior 15-record snapshot plus five additional IDs recorded in the 2026-11-10 attachment delta); the historical Library listing reports 40 file records + one folder. No fresh direct Library enumeration or complete Project chat enumeration is available. |
| Stage 2 | PARTIAL — CLOSURE GATE BLOCKED | Stage 2 declaration/acceptance records exist in Library; existence does not establish gate closure. |
| Stage 3 | BLOCKED (recorded) | Do not begin by label alone. |
| Stage 4–5 | NOT STARTED (recorded) | Requires preceding gates and evidence. |

### Phase track

| Phase | Recorded status | Boundary |
|---|---|---|
| Phase 1 | PARTIAL — BRANCH (recorded) | Core branch work is not equivalent to default-branch delivery. |
| Phase 2 | IN PROGRESS (recorded) | No closure inferred from documentation work. |
| Phase 3 | BLOCKED (recorded) | Requires explicit entry criteria. |

These status rows are source-recorded snapshots. They are not a fresh re-evaluation of every Stage/Phase acceptance criterion.

## 4. Source event references requiring direct revalidation

| Topic | Reported source event | Current classification | Missing evidence |
|---|---|---|---|
| TBD Register registration | ChatGPT export User turns 364–384 (reported) | RECORDED — SOURCE MATCH PARTIAL | Direct source excerpt and item-by-item matching |
| Final Decision Matrix | User turn 390 (reported) | REPORTED APPROVAL EVENT | Direct re-read of original HTML and exact decision text |
| CI Run #406 approval | User turn 442 (reported) | REPORTED APPROVAL EVENT | Exact run/commit/evidence mapping |
| ADR-014 scope | Prior explicit user approval summarized in project context | PROPOSED ADR; administrative creation approved | Original approval message mapping and gameplay rule disposition |
| ADR-015 | Existing repository ADR | PROPOSED | Owner decision to approve/reject and implementation authorization |
| WO-DOC-01 | Current active Project conversation — owner message immediately following the WO-DOC-01 approval request, 2026-11-10 | APPROVED — DOCUMENTATION EXECUTION AUTHORIZATION | Exact text is present in current conversation context; no separate exported thread ID/message ID was available in this inspection |

Do not use an export-level timestamp as a substitute for missing per-message dates.

## 5. Unresolved owner conflicts

| Conflict | Requirement | Recorded source values | Current status | Closure requirement |
|---|---|---|---|---|
| CONFLICT-01 | TBD-37 — Record Preservation | Main register: Core Owner / Application Owner; Binding Clarifications: Core Owner | OPEN — SOURCE VERIFICATION REQUIRED | Original approved source or explicit owner correction |
| CONFLICT-02 | TBD-45 — Failure Safety | Core Owner / Application Owner; primary owner and responsibility split not specified | OPEN — RESPONSIBILITY SPLIT TBD | Explicit owner assignment or approved responsibility matrix |

TBD-37 and TBD-45 are distinct requirements. Do not merge them or infer one requirement's owner from the other.

## 6. Requirement relationships that must be preserved

The supplied source records report:
- TBD-37 and TBD-45 are distinct requirements.
- TBD-49 is a prerequisite for TBD-34.
- TBD-53 requires a mapping of 54 subordinate linking operations; details are not fully recovered.
- TBD-55 and TBD-58 have a mutual dependency.
- Requirements involving Reservation, Compensation, InFlight, and External Effects must remain distinct from Gameplay requirements.

These are recorded relationships awaiting item-level source verification; they are not a substitute for the full original TBD Register.

## 7. Additional owner authorization events — 2026-11-10

| Event ID | Owner instruction | Decision scope | Status | Source/evidence boundary |
|---|---|---|---|---|
| OWNER-EVENT-001 / WO-DOC-01 | User explicitly approved WO-DOC-01 and authorized sequential execution of all five documentation-governance work packages. | Documentation governance execution: repository inspection, documentation-only branch changes, commits and Draft PR maintenance, subject to the approved boundaries. | APPROVED — EXECUTION AUTHORIZED | Direct owner message in the current project conversation on 2026-11-10. Exact platform message/thread ID is not exposed in the mounted source; retain this conversation reference and do not fabricate a turn ID. |
| OWNER-EVENT-002 / DOC-B-01 | User said “تابع الاستكمال” after the WO-DOC-01 approval. | Continue the authorized documentation work and bounded source reconciliation. | CONTINUATION AUTHORIZED | Direct current conversation message; this is not separate approval for code, ADR, stage closure, or PR merge. |

**Scope limitation:** WO-DOC-01 authorization does not approve the proposed Governance Baseline wholesale, change any gameplay/architecture decision, approve the two WO-A3 amendments, resolve A1-HOLD-01…06, authorize Core/Net/Unity/Tests changes, or permit merging PR #11.

## 8. Decision change protocol

Any future change must record:
1. Decision ID and prior status.
2. Exact owner instruction or source event.
3. Date/time only when present in source.
4. Affected ADRs, requirements, contracts, code, tests, and documents.
5. Whether the action changes documentation, design approval, implementation, or verification.
6. Evidence and reviewer outcome.

No decision in this register is upgraded to APPROVED merely because it is listed here.
