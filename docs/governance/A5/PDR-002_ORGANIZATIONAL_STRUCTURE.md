# PDR-002 — Organizational Structure

**Project:** Cost of Staying (ثمن البقاء)  
**Workstream:** A5 — Document Control  
**Document ID:** PDR-002  
**Document Version:** 0.1.0  
**Status:** PROPOSED — IMPLEMENTED IN FEATURE BRANCH; NOT APPROVED  
**Prepared:** 2026-11-11  
**Repository:** live-egypt/CostOfStaying  
**Branch:** docs/a5-pdr-002-organizational-structure  
**Decision State:** OPEN  
**Code / Gameplay Authority:** NONE

---

## 1. Purpose

Define a controlled organizational role model for project governance and document control. This document separates roles and decision rights without assigning real people, resolving technical ownership conflicts, or converting proposed responsibilities into approved authority.

This is a governance proposal, not evidence that the project has staffed these roles or adopted a formal organization.

## 2. Source basis and limitations

The source decision register describes PDR-002 as **Organizational Structure** and records:

- Current status: OPEN.
- Related gaps: DOC-PACKAGE-01, PHASE-GOV-001, PKG-01, DOC-02, DOC-03.
- Available evidence: references to a need to organize the project structure and a proposed functional grouping of documents; PR #11 is only a candidate documentation baseline.
- Missing evidence: an approved organizational structure, role boundaries, approval authority, and relationship to requirement and decision ownership.
- A4 recommendation: DEFER TO A5, retaining a proposed structure while leaving CONFLICT-01 / CONFLICT-02 ownership unresolved pending evidence and decision.

The exact historical source for every role assignment is not present in the repository snapshot inspected for this change. Accordingly, this document does not claim historical approval for any role model.

## 3. Proposed role model

The following roles are governance roles. A single person may temporarily perform more than one role only when the decision record explicitly identifies the capacity in which that person acted. No person is assigned by this document.

| Role ID | Role | Proposed responsibility | Authority boundary |
|---|---|---|---|
| GOV-ROLE-01 | Project Owner | Maintains the project mandate, supplies owner-controlled source material, and makes product-level ownership decisions. | Does not gain technical approval authority merely by supplying a source; approval must be recorded separately where required. |
| GOV-ROLE-02 | Director / Decision Authority | Issues explicit governance decisions, accepts or rejects deliverables, opens/closes stages, and resolves escalated decision items. | A decision is effective only within its stated scope and recorded evidence. Does not make unrecorded changes to technical implementation. |
| GOV-ROLE-03 | Governance Executor | Performs inventories, comparisons, drafting, traceability, and repository documentation changes on an authorized feature branch. | May recommend and implement approved process/document changes; cannot self-approve a deliverable or mark evidence VERIFIED without reproducible evidence. |
| GOV-ROLE-04 | Domain Owner | Maintains a defined technical or product domain and proposes domain decisions/requirements. | Domain ownership does not imply authority over another domain or over project-wide governance. Exact domain assignments remain TBD. |
| GOV-ROLE-05 | Reviewer | Independently checks a deliverable against its acceptance criteria and records findings. | Review is not approval unless the decision record explicitly grants that authority. |
| GOV-ROLE-06 | Repository Maintainer | Maintains repository access, branch protections, repository hygiene, and merge mechanics. | Repository permissions do not confer content authority or the right to approve project decisions. |

## 4. Decision and approval rules

1. **Proposal:** The Governance Executor or relevant Domain Owner may prepare a proposal.
2. **Review:** A Reviewer checks completeness, consistency, evidence references, and acceptance criteria.
3. **Decision:** The Director / Decision Authority records ACCEPT, ACCEPT WITH CONDITIONS, REJECT, or DEFER, with scope and rationale.
4. **Publication:** The Governance Executor publishes the decision record and associated approved document version on a feature branch and opens a pull request.
5. **Merge:** A repository maintainer merges only after the required project decision and repository checks are satisfied. A merge is a publication event, not a substitute for the decision itself.
6. **Verification:** VERIFIED is reserved for claims supported by reproducible checks and retained evidence. Document presence or PR approval alone is insufficient.
7. **Separation of duties:** The author of a governance deliverable must not mark their own work as independently reviewed. Where staffing makes separation impossible, record the exception explicitly.

## 5. Document and decision ownership

| Artifact / decision class | Responsible preparation | Review | Approval / decision | Current assignment |
|---|---|---|---|---|
| Project charter and product scope | Governance Executor / relevant Domain Owner | Reviewer | Director / Decision Authority | Role model proposed; named assignments TBD |
| Governance policy and stage decisions | Governance Executor | Reviewer | Director / Decision Authority | Role model proposed; named assignments TBD |
| ADRs | Relevant Domain Owner / Governance Executor | Technical Reviewer | Explicit decision authority recorded in the ADR | Per-ADR authority mapping TBD |
| Requirements and acceptance criteria | Relevant Domain Owner | Reviewer | Explicit requirement owner / decision authority | Per-requirement mapping TBD |
| Evidence and provenance records | Governance Executor | Independent Reviewer where available | Evidence acceptance by the authority defined in the applicable gate | Named evidence approver TBD |
| Repository branch / merge operations | Repository Maintainer | Required code/document reviewers | Merge under repository rules after decision prerequisites | Current permissions do not establish content authority |

## 6. Separation from technical ownership

This PDR governs project-level organizational roles only. It does **not** allocate runtime or code ownership between Core Owner, Application Owner, Net, or Unity layers.

In particular:

- CONFLICT-01 remains OPEN; no owner is assigned to TBD-37 by this document.
- CONFLICT-02 remains OPEN as an ownership-allocation gap; no responsibility split is inferred for TBD-45.
- TBD-45 remains OPEN — RESPONSIBILITY ALLOCATION UNRESOLVED.
- PDR-006 remains OPEN.
- The six responsibility dimensions identified in the A4 findings require a separate evidence-backed allocation.
- PHASE-GOV-001 remains OPEN — UNRESOLVED / NOT ESTABLISHED. This document does not define Phase 2.

## 7. Options and proposed resolution

**Option A — Proposed Functional Governance Structure**  
Use the role model in Sections 3–5 as a working governance structure, subject to explicit assignments and the unresolved boundaries listed in Section 6.

**Option B — Official Binding Organizational Structure**  
Adopt the role model as a formally binding organizational structure with assigned authorities and responsibilities.

**Proposed resolution:** Select Option A for documentation and workflow design only. Do not select Option B until role assignments, approval authority, domain boundaries, and evidence dependencies have been reconciled and explicitly approved.

## 8. Acceptance criteria for PDR-002 closure

PDR-002 may be considered for closure only when all of the following are evidenced:

- [ ] The canonical project/document package structure is reconciled with DOC-PACKAGE-01 and PKG-01.
- [ ] Every governance role has a defined scope and non-overlapping decision boundary.
- [ ] Named role assignments, or an explicit role-combination policy, are recorded by the decision authority.
- [ ] Approval authority is mapped for project scope, ADRs, requirements, evidence, and stage transitions.
- [ ] The relationship between organizational roles and requirement/decision ownership is documented.
- [ ] CONFLICT-01 and CONFLICT-02 are either resolved through their own evidence-backed decisions or explicitly excluded from this PDR's closure scope without implying resolution.
- [ ] PHASE-GOV-001, DOC-02, and DOC-03 dependencies are reconciled or explicitly deferred with impact.
- [ ] The Director / Decision Authority records a closure decision referencing the exact document version and commit SHA.

Until these criteria are satisfied, PDR-002 remains OPEN.

## 9. Related records and dependencies

- PDR-001 — Repository of Record.
- PDR-003 — Privacy.
- PDR-004 — File Unification.
- PDR-005 — LIF-DEC-01 Supersession.
- PDR-006 — Eliminate Authority.
- PDR-009 — TBD_REGISTER.
- PDR-010 — PROVENANCE_REGISTER.
- PDR-012 — Source Inventory Crosswalk.
- PDR-016 — Semantic Comparison of Nine Shared Paths.
- DOC-PACKAGE-01, PKG-01, DOC-02, DOC-03.
- CONFLICT-01, CONFLICT-02, TBD-37, TBD-45.
- PHASE-GOV-001.

## 10. Explicit non-actions

This document does not:

- approve or ratify the organizational structure;
- assign named people to roles;
- resolve CONFLICT-01, CONFLICT-02, PDR-006, TBD-37, or TBD-45;
- establish Phase 2 or change PHASE-GOV-001;
- authorize code writing, gameplay changes, or a Code Gate;
- authorize merging documentation packages or deleting historical paths;
- establish PR #11 as an approved documentation baseline.

## 11. Status

| Item | Status |
|---|---|
| PDR-002 role model | PROPOSED |
| Draft committed to feature branch | IMPLEMENTED |
| Independent review | TBD |
| Director approval | TBD |
| PDR-002 closure | OPEN |
| Repository merge | NOT PERFORMED |
| Code / gameplay authorization | NOT GRANTED |
