# Project Governance Baseline — Cost of Staying

**Revision:** 2026-11-10  
**Decision Status:** PROPOSED — awaiting owner acceptance of the governance baseline  
**Evidence Maturity:** DOCUMENTED — initial reconciliation from repository snapshots and supplied project records  
**Scope:** Repository documentation, decisions, requirements, sources, conflicts, and verification evidence.

> This document proposes a control model. Its presence does not approve gameplay rules, ADRs, architecture choices, or project-stage closure.

## 1. Purpose

Create one traceable path from source material to decisions, requirements, implementation, tests, and retained evidence. The objective is not to multiply registers. It is to assign a single purpose and authority to each record, preserve unresolved source material, and prevent an implementation or passing test from silently changing a design decision.

## 2. Independent status axes

Every material claim must be classified on two independent axes.

### 2.1 Decision Status

| Value | Meaning |
|---|---|
| PROPOSED | Suggested decision; not approved. |
| APPROVED | Explicitly accepted design/governance decision within its declared scope. |
| REJECTED | Explicitly rejected decision. |
| SUPERSEDED | Replaced by a later identified decision; historical record retained. |

### 2.2 Evidence Maturity

| Value | Meaning |
|---|---|
| DOCUMENTED | Recorded in an identifiable source or repository document. |
| REVIEWED | Read and assessed within a declared scope. |
| IMPLEMENTED | Present in an identifiable executable/code artifact. |
| TESTED | A named test/procedure was executed and its result retained. |
| VERIFIED | A specific claim passed a reproducible verification procedure with retained evidence and reviewer confirmation. |
| OPERATIONALLY PROVEN | Sustained behavior is evidenced in the declared real operating environment and time window. |

### 2.3 Rules

- Never collapse these axes into one status.
- APPROVED does not imply IMPLEMENTED.
- IMPLEMENTED does not imply APPROVED.
- TESTED does not automatically imply VERIFIED.
- A passing Core test does not verify Unity, FishNet, Android, Dedicated Server, production security, or gameplay balance.
- TBD, OPEN, PARTIAL, PENDING, and DEFERRED are workflow/source states or qualifiers, not substitutes for either axis. Record them separately.
- A repository document may be DOCUMENTED while its underlying source claim remains SOURCE PENDING.

## 3. Authority map — proposed

| Information type | Primary authority | Supporting records | Rule |
|---|---|---|---|
| Project vision and scope | docs/00_PROJECT_CHARTER.md | GDD, MVP scope, acceptance criteria | Do not redefine product scope in audit reports. |
| Gameplay rules | docs/02_GAME_RULES_SPECIFICATION.md plus applicable approved ADRs | System specifications, playtest evidence | PROPOSED FOR PLAYTEST is not final approval. |
| Architectural decisions | Individual ADR files | DECISIONS_LOG.md, traceability register | The ADR carries the decision; the log indexes it. |
| Decision index | docs/governance/DECISIONS_LOG.md | Decision Traceability Register | Index only; it does not independently approve an ADR. |
| Existing project requirements traceability | docs/governance/REQUIREMENTS_TRACEABILITY.md | Core tests and system specs | Keep gameplay/Core requirements distinct from the 85-item uncertainty register. |
| 85-item uncertainty register | docs/governance/TBD_REGISTER.md | Source-to-Requirement Matrix, Source Traceability Register | Registration is not resolution or source verification. |
| 109-record consistency set | docs/governance/CONSISTENCY_MATRIX.md | TBD cross-references and source evidence | Preserve its independent dimensions and pending-source entries. |
| Source inventory | docs/governance/SOURCE_INVENTORY.md | Current-session materialization manifest | Inventory states what was visible in a dated snapshot. |
| Source lineage and limitations | docs/governance/PROVENANCE_REGISTER.md | Source Inventory, source artifacts | A derived report never replaces the original source. |
| Decision-to-source mapping | docs/governance/DECISION_TRACEABILITY_REGISTER.md | ADRs, source event references | Missing original approval remains open. |
| Conflicts | docs/governance/CONFLICT_REGISTER.md | Relevant source documents | No conflict closes by inference or recency alone. |
| Requirement/evidence linkage | docs/governance/REQUIREMENT_EVIDENCE_MATRIX.md | Requirement and test reports | Bound every claim to its evidence scope. |
| Document authority and change control | docs/governance/DOCUMENT_CONTROL_REGISTER.md | This baseline, docs index | One authoritative definition per subject. |
| Project execution status | docs/PROJECT_STATUS_AND_EXECUTION_REGISTER.md | PRs, commits, CI evidence | Every implementation claim names branch and commit. |
| Test/evidence history | docs/audit/CI_EVIDENCE_REGISTER.md | GitHub Actions runs/artifacts | A historical result remains attached to its run and commit. |

## 4. Source hierarchy and decision precedence

Use the following order for a claim, while preserving branch and date scope:

1. Exact owner-authored approval/rejection event, when available.
2. Approved ADR or explicitly approved product decision, for its stated scope.
3. Current canonical specification that does not contradict an approved ADR.
4. Requirements and governance registers, for tracking rather than independent approval.
5. Executable implementation and reproducible test evidence, for what the code does.
6. Audit reports, summaries, conversation extracts, and historical drafts, as secondary evidence with provenance labels.

This hierarchy is not permission to discard a lower-level source. Conflicting values remain recorded until a resolution is evidenced and accepted.

## 5. Branch and delivery controls

- main is the delivery baseline. A feature branch is not part of main until a reviewed PR is merged.
- Every repository status statement must include branch/ref and commit SHA when that scope matters.
- Do not describe files from PR #6 or PR #9 as present on main merely because they exist on their head branches.
- Do not merge overlapping PR packages as-is.
- Use a dedicated feature branch for this reconciliation; keep main unchanged.
- Preserve historical branches, PRs, commits, and superseded documents. Deletion requires a separate evidence-backed migration decision.
- Each PR must state scope, base SHA, included/excluded paths, validation performed, and unresolved issues.

## 6. Stage and Phase separation

Stage 0–5 and Phase 1–3 are separate control structures. They are not synonyms and must not be combined into one progress percentage.

- Stage track: source recovery, governance, reconciliation, evidence, and stage-closure gates.
- Phase track: technical/product implementation slices and their engineering gates.

Each track maintains its own entry criteria, exit criteria, evidence, owner approval, and state. A Phase 1 Core CI pass does not close a Stage 1 source-recovery gate. A Stage closure declaration does not prove a Unity build.

## 7. Change control

1. Identify the exact source, affected canonical document, and downstream references.
2. Classify the change as correction, clarification, new proposal, approval, rejection, or supersession.
3. For a major architecture change, prepare/revise an ADR before implementation.
4. Preserve the previous value and source trace.
5. Update the canonical specification, affected system documents, traceability, tests, and evidence map in one reviewable delivery slice where applicable.
6. Do not elevate Decision Status without an explicit owner decision.
7. Do not elevate Evidence Maturity without reproducible evidence and reviewer confirmation.
8. Do not remove old documents until references, provenance, and replacement coverage are verified.

## 8. Deferred registers

The central Risk Register and Technical Debt Register remain DEFERRED until source identifiers, ownership, categories, and evidence are reconciled. In particular, the two different uses of RISK-013 must not be merged or silently renumbered. This baseline does not create either register.

## 9. Current limitations

- Complete Project conversation enumeration and direct thread IDs for chats labelled 1 and 2 are not available in the current source surface.
- The origin attribution of chats 1 and 2 remains disputed in the records.
- The original complete Network Audit is not recovered.
- The original complete Security Audit is not recovered.
- The محادثة 2.html export is a candidate; its exact mapping to Project chat 2 is not proven.
- Project Library items remain within the Stage 0 metadata/text limitation where raw bytes were not made available.
- PR #8 and PR #9 are overlapping, unmerged documentation packages on different bases.
- PR #6 contains a separate Core implementation branch and must not be conflated with main.

## 10. Status

| Item | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| This governance baseline | PROPOSED | DOCUMENTED | Awaiting owner acceptance |
| Existing ADR index | — | DOCUMENTED | Present on main |
| 85-item register | — | DOCUMENTED | Source reconciliation partial |
| 109-record consistency matrix | — | DOCUMENTED | Pending-source records reported |
| Central Risk Register | PROPOSED | — | DEFERRED |
| Technical Debt Register | PROPOSED | — | DEFERRED |
| Complete source/chat inventory | — | PARTIAL | OPEN |
| Final readiness assessment | — | — | NOT ISSUED |
