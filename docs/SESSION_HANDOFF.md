# Session Handoff — Cost of Staying

**Project:** Cost of Staying (ثمن البقاء)  
**Updated:** 2026-11-10  
**Work Order:** WO-DOC-01 — Project Documentation Governance  
**Decision Status:** APPROVED TO PROCEED WITH DOCUMENTATION WORK  
**Evidence Maturity:** REVIEWED — bounded live GitHub inspection  
**Repository:** `live-egypt/CostOfStaying`  
**Default branch:** `main`  
**Working branch:** `docs/project-reconciliation-baseline-2026-11-10`  
**Pull Request:** [#11 — Documentation and evidence baseline](https://github.com/live-egypt/CostOfStaying/pull/11)  
**PR state at update:** OPEN / DRAFT / NOT MERGED  
**Last substantive commit before package-map draft:** `c11e60fe5313a60d6b2bf6637acfdacf710b4d6a`

> This handoff is a navigation aid, not a competing source of truth. Canonical decisions, source lineage, requirements, conflicts, and delivery status remain in their designated registers and ADRs.

## 1. Current authorized scope

The owner authorized WO-DOC-01 to proceed through sequential documentation-governance work. The current delivery is documentation-only.

Allowed within this work order:
- Inspect and reconcile repository documentation and source records.
- Improve documentation on the dedicated documentation branch.
- Maintain provenance, decision, conflict, action, and evidence traceability.
- Update this handoff when the working state changes.

Excluded:
- Direct changes to `main`.
- Changes to Core, Net, Unity, Tests, or ADR decision content.
- Merging PRs or closing project stages.
- Promoting proposals to approvals or evidence claims without source-backed owner decisions.
- Creating Risk or Technical Debt registers before the documented source/identifier reconciliation gate.

## 2. Canonical records — consult before editing

| Subject | Canonical record |
|---|---|
| Governance rules and authority map | `docs/governance/PROJECT_GOVERNANCE_BASELINE.md` |
| Source inventory | `docs/governance/SOURCE_INVENTORY.md` |
| Provenance and source limitations | `docs/governance/PROVENANCE_REGISTER.md` |
| Decision traceability | `docs/governance/DECISION_TRACEABILITY_REGISTER.md` |
| Conflicts | `docs/governance/CONFLICT_REGISTER.md` |
| 85-item uncertainty register | `docs/governance/TBD_REGISTER.md` |
| 109-record consistency matrix | `docs/governance/CONSISTENCY_MATRIX.md` |
| Requirement-to-source mapping | `docs/governance/SOURCE_TO_REQUIREMENT_MATRIX.md` |
| Requirement/evidence linkage | `docs/governance/REQUIREMENT_EVIDENCE_MATRIX.md` |
| Document authority and change control | `docs/governance/DOCUMENT_CONTROL_REGISTER.md` |
| Project delivery and work status | `docs/PROJECT_STATUS_AND_EXECUTION_REGISTER.md` |
| CI evidence | `docs/audit/CI_EVIDENCE_REGISTER.md` |
| Reconciliation progress | `docs/audit/RECONCILIATION_PROGRESS.md` |

Do not create a parallel register for a subject already governed by one of these records. Update the canonical record and its index instead.

## 3. Verified repository checkpoint

The live repository metadata was read during this session:
- Repository exists and is writable by the connected GitHub identity.
- Default branch is `main`.
- PR #11 is open, Draft, mergeable according to current metadata, and not merged.
- PR #11 targets `main` from `docs/project-reconciliation-baseline-2026-11-10`.
- PR #11 contains 36 changed documentation paths at the attachment-delta checkpoint; the current branch head may be newer.
- PR #11's base SHA is `3dd9cfc27aa48b09b3cde802e266a8221c39f0d3`.
- PR #11's observed head SHA at the attachment-delta checkpoint is `7fd3d8e96279c70a83ed26e0bdc525494a74959d`.

These are a time-bounded observations. Re-fetch PR metadata and branch heads before every subsequent write or review. A mergeable flag is not owner approval.

## 4. Current work state

The owner explicitly approved WO-DOC-01 for sequential execution. The authoritative work-order/action tracker is [WORK_ORDER_REGISTER.md](governance/WORK_ORDER_REGISTER.md). The governance baseline remains PROPOSED pending owner review; work-order authorization does not approve every policy statement in that baseline.

| Workstream | State | Next action |
|---|---|---|
| Governance baseline | PRESENT ON PR #11 — PROPOSED | Owner review; do not self-approve |
| WO-DOC-01 execution | APPROVED — AUTHORIZED | Follow the sequential work packages in WORK_ORDER_REGISTER.md |
| Work package A | INITIAL BASELINE PRESENT — REVIEW PENDING | Preserve current authority map and status axes |
| Work package B | PARTIAL — 5-ID DELTA HASHED | Library/chat-source gaps remain blocked; see attachment snapshot delta |
| Work package C | PARTIAL — WO-DOC-01 AUTHORIZATION MAPPED | Recover remaining owner decision events; A1-HOLD-01…06 remain source-blocked |
| Work package D | PARTIAL | Keep evidence scoped to exact run/commit/artifact |
| Work package E | PRESENT — MAINTAIN | Update this handoff after material changes |
| Work package F — Package Structure | DRAFT 0.1 CREATED — OWNER REVIEW REQUIRED | Review functional package map; no path migration authorized |
| Source inventory / provenance | PARTIAL — 20 ATTACHMENT OBJECTS; 13 LOCAL PATHS | Library and complete conversation enumeration remain unavailable |
| Decision traceability | PARTIAL | Recover exact owner approval events; preserve missing-source states |
| TBD register (85) | STRUCTURALLY MAPPED; SOURCE VALIDATION PARTIAL | Validate item-by-item; do not mark resolved from registration alone |
| Consistency matrix (109) | STRUCTURALLY MAPPED; 24 pending-source records reported | Reconcile source and dimension details |
| Network Audit | ORIGINAL SOURCE NOT RECOVERED | Keep reconciliation explicitly initial/partial |
| Security Audit | ORIGINAL SOURCE PARTIAL | Keep reconciliation explicitly initial/partial |
| CI evidence | HISTORICAL RUN-SCOPED RECORDS | Preserve exact run/commit/job/artifact associations |
| Risk Register | DEFERRED | Wait for source and identifier reconciliation |
| Technical Debt Register | DEFERRED | Wait for source and identifier reconciliation |
| Stage 1 / Stage 2 closure | NOT DECLARED | Closure requires explicit gates and evidence |

## 5. Mandatory unresolved items

- Complete Project conversation enumeration and direct thread IDs for chats labelled 1 and 2 are unavailable in the current source surface.
- ChatGPT/DeepSeek origin attribution remains an open provenance conflict.
- Exact mapping of `محادثة 2.html` to Project chat 2 remains unverified.
- Original complete Network Audit is not recovered.
- Original complete Security Audit is not recovered.
- A1-HOLD-01…06 original texts and the exact two mandatory WO-A3 amendments must not be reconstructed by inference.
- CONFLICT-01 and CONFLICT-02 remain open.
- TBD-53's 54 sub-task mappings remain unavailable.
- Do not merge overlapping PR #8 and PR #9 as-is; PR #11 is the current consolidation candidate and remains Draft.

## 6. Execution sequence

1. Re-fetch live repository, PR, branch, and relevant document state.
2. Identify the canonical document for the next work item; inspect it and its linked sources.
3. Produce a bounded plan and preserve the approved scope.
4. Make documentation-only changes on the existing dedicated branch unless a new branch is explicitly required.
5. Validate changed Markdown links, identifiers, table counts, and cross-references within the changed scope.
6. Update the canonical register, `docs/README.md`, and this handoff as applicable.
7. Commit with a specific message and update/open a Draft PR; never merge automatically.
8. Record exact commit SHA, PR URL, changed paths, validation performed, and remaining limitations.
9. Stop at an owner-approval gate whenever a decision, source attribution, ADR, gameplay rule, or scope change is required.

## 7. Status vocabulary

Keep Decision Status and Evidence Maturity independent:
- Decision Status: PROPOSED, APPROVED, REJECTED, SUPERSEDED.
- Evidence Maturity: DOCUMENTED, REVIEWED, IMPLEMENTED, TESTED, VERIFIED, OPERATIONALLY PROVEN.
- Work/source qualifiers such as OPEN, PARTIAL, PENDING, BLOCKED, DEFERRED, and TBD remain separate.

Never infer implementation from documentation, approval from a test, or verification from a historical report without reproducible evidence.

## 8. Session closeout template

At the end of each substantial work session, update:
- Current branch and exact head SHA.
- PR state and URL.
- Files changed.
- Commit(s) created.
- Validation performed and results.
- Decisions made by the owner in this session.
- Open blockers and missing source evidence.
- Exact next action.

**Current next action:** continue source-backed requirement/evidence mapping across TBD-01…85 and the 109-record Consistency Matrix. Preserve per-item source gaps; no path migration, Risk/Debt register creation, PR merge, or Stage 1/2 closure.
