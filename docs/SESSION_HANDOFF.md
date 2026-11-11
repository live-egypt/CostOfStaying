# Session Handoff — Cost of Staying

**Project:** Cost of Staying (ثمن البقاء)  
**Updated:** 2026-11-11  
**Work Order:** WO-DOC-01 — Project Documentation Governance  
**Decision Status:** APPROVED TO PROCEED WITH DOCUMENTATION WORK  
**Evidence Maturity:** REVIEWED — bounded live GitHub inspection  
**Repository:** `live-egypt/CostOfStaying`  
**Default branch:** `main`  
**Working branch:** `docs/project-reconciliation-baseline-2026-11-10`  
**Pull Request:** [#11 — Documentation and evidence baseline](https://github.com/live-egypt/CostOfStaying/pull/11)  
**PR state at update:** OPEN / DRAFT / NOT MERGED  
**Last substantive DOC-B-02 content commit:** `35197b7acfc0ae31dfa958abdccf8d3f16b9abb4`

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
- PR #11 contains 39 changed paths, all under `docs/`, at the pre-write checkpoint.
- PR #11's base SHA is `3dd9cfc27aa48b09b3cde802e266a8221c39f0d3`.
- PR #11's observed pre-write head SHA on 2026-11-11 is `8e7bc1ec0a0306655781755eb281ebb22d1b0ae6`.

These are a time-bounded observations. Re-fetch PR metadata and branch heads before every subsequent write or review. A mergeable flag is not owner approval.

## 4. Current work state

The owner explicitly approved WO-DOC-01 for sequential execution. The authoritative work-order/action tracker is [WORK_ORDER_REGISTER.md](governance/WORK_ORDER_REGISTER.md). The governance baseline remains PROPOSED pending owner review; work-order authorization does not approve every policy statement in that baseline.

| Workstream | State | Next action |
|---|---|---|
| Governance baseline | PRESENT ON PR #11 — PROPOSED | Owner review; do not self-approve |
| WO-DOC-01 execution | APPROVED — AUTHORIZED | Follow the sequential work packages in WORK_ORDER_REGISTER.md |
| Work package A | INITIAL BASELINE PRESENT — REVIEW PENDING | Preserve current authority map and status axes |
| Work package B | PARTIAL — DOC-B-01 AND DOC-B-02 BOUNDED INVENTORY PASSES DONE; DOC-B-03 OPEN | Original chat/thread identity and completeness remain unresolved |
| Work package C | PARTIAL — WO-DOC-01 AUTHORIZATION MAPPED | Recover remaining owner decision events; A1-HOLD-01…06 remain source-blocked |
| Work package D | PARTIAL | Keep evidence scoped to exact run/commit/artifact |
| Work package E | PRESENT — MAINTAIN | Update this handoff after material changes |
| Work package F — Package Structure | DRAFT 0.1 CREATED — OWNER REVIEW REQUIRED | Review functional package map; no path migration authorized |
| Source inventory / provenance | PARTIAL — 21 HISTORICAL ATTACHMENT OBJECTS; 14 LOCAL PATHS; 45 LIBRARY FILES + 1 FOLDER; 87 REPOSITORY BLOBS | One active conversation attachment matches a Library ID; full Project conversation enumeration remains unavailable |
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

**Current next action:** DOC-B-03 — reconcile the accessible `محادثة 2.html` metadata and owner-attested identity relation while preserving the origin, original-thread, and completeness gaps. Do not infer missing conversation parts or owner decisions.


## 7. Latest bounded source snapshot — 2026-11-10

A further distinct attachment object is now surfaced beyond the earlier 20-object checkpoint: `file_0000000022c481f4b8d45ee51c9bf603` (`قسم · قسم · مراجعة وجرد الملفات.txt`). Its mounted-byte SHA-256 is `23ebb91ab1e85b562e41aaca7884ccaf98f306aab0d20ce03340ef40794e817d` (5314 bytes; 152 lines). The current bounded counts are 21 attachment objects and 14 mounted text paths / 110,718 bytes. See `docs/audit/ATTACHMENT_SNAPSHOT_DELTA_2026-11-10-02.md` and `docs/audit/SESSION_MATERIALIZATION_MANIFEST_2026-11-10-02.md`.

This is not a complete Project Library or conversation inventory. A1-HOLD-01…06 and both WO-A3 amendments remain source-blocked. Next action: continue DOC-B-02/DOC-B-03 recovery where original Library/chat source access exists; otherwise preserve the blockers and proceed only with source-backed item-level mapping under DOC-D-02.


## 8. Historical execution checkpoint — 2026-11-10

- Live PR #11 re-fetched: OPEN / DRAFT / NOT MERGED; base `main`; head branch `docs/project-reconciliation-baseline-2026-11-10`; head SHA at session start `8ddf8f71d460c8db148c0069eb4a4893561eed77`; 39 changed files, all under `docs/`.
- DOC-B-02 remains BLOCKED: direct Project Library listing/export is not accessible through the current source surface.
- DOC-B-03 remains OPEN/BLOCKED for closure: surfaced reports and attachment texts do not provide the complete original Project chat threads, direct thread IDs, or decisive platform-origin metadata.
- The next source-backed action selected was DOC-D-01. A fresh read-only re-fetch confirmed the existing 187/187 Core CI record: commit `2820285021b1a3d7ae9acd953b3bb65677c33512`, run `42383513646`, job `128435932500`, artifact `13578582284`. The log says 187 passed, 0 failed, 0 skipped. No test was run in this session; no artifact archive was inspected.
- DOC-D-01 remains PARTIAL pending equivalent re-fetch/inspection for the remaining historical rows and artifact scope. DOC-D-02 remains PARTIAL; no TBD or consistency record is resolved.

**Next single action:** continue DOC-D-01 by re-fetching the next distinct Core CI snapshot (run `41331400978`, commit `94c5789b0e82493290ca85133fc692e83ffd9884`) and checking its exact run/job/log/artifact association before recording any update.


## 9. Latest execution checkpoint — DOC-B-02 — 2026-11-11

- Pre-write live state: PR #11 OPEN / DRAFT / NOT MERGED; base `main` at `3dd9cfc27aa48b09b3cde802e266a8221c39f0d3`; branch `docs/project-reconciliation-baseline-2026-11-10`; pre-write head `8e7bc1ec0a0306655781755eb281ebb22d1b0ae6`; 39 changed paths, all under `docs/`.
- DOC-B-02 content commit: `35197b7acfc0ae31dfa958abdccf8d3f16b9abb4` (`docs(provenance): reconcile live Library metadata snapshot`). Post-write verification confirmed that PR #11 and the branch ref both pointed to this commit before this closeout synchronization.
- Library metadata re-fetch at `2026-11-11T08:55:12.632764Z`: 45 file records / 45 unique IDs + 1 folder; all 40 historical IDs retained; five additional records listed in `SOURCE_INVENTORY.md` Section 4.1.
- Current active conversation file surface at `2026-11-11T08:55:13.004803Z`: one `محادثة 2.html` record; its ID matches the Library record. The historical 21 attachment IDs are a separate set and do not overlap the current 45 Library IDs.
- Complete pre-write repository tree: SHA `28aabb1420b5e794566d0001249243b5a3032fca`, 99 entries / 87 blobs, not truncated. Five Library basenames match repository paths; no content/byte equality is claimed.
- PR #11 changed-file list has 39 documentation paths. DOC-B-02: DONE for the bounded metadata crosswalk. DOC-B-03: OPEN; original thread IDs, complete original chat content, and platform-origin evidence remain unresolved. A1-HOLD-01…06 and both mandatory WO-A3 amendments remain source-blocked.
- No Core, Net, Unity, Tests, ADR content, or `main` was changed. No PR merge or Stage closure occurred.

**Next single action:** DOC-B-03 — inspect the accessible HTML export metadata against the owner-attested identity relation, preserving the possibility that other conversation parts are elsewhere and not present in this export.
