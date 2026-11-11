# Work Order Register — Cost of Staying

**Revision:** 2026-11-11 — DOC-B-02 metadata crosswalk  
**Work Order:** WO-DOC-01 — Project Documentation Governance  
**Decision Status:** APPROVED — execution authorized by project owner  
**Evidence Maturity:** DOCUMENTED — initial execution baseline  
**Working branch:** `docs/project-reconciliation-baseline-2026-11-10`  
**Pull Request:** [#11 — Documentation and evidence baseline](https://github.com/live-egypt/CostOfStaying/pull/11)  
**Merge status:** OPEN / DRAFT / NOT MERGED

> This register is the single index for work-order scope, sequencing, execution state, and next actions. Subject-specific facts remain in their canonical registers; this file must not duplicate their detailed contents.

## 1. Authorization and boundaries

The project owner approved WO-DOC-01 and authorized sequential execution of all five work packages.

Authorized:
- Documentation-only repository changes on a dedicated feature branch.
- Inspection and reconciliation of existing documentation, sources, decisions, actions, and evidence.
- Updates to canonical registers and their indexes.
- Commits and Draft PR maintenance for review.

Not authorized by this work order:
- Direct changes to `main`.
- Changes to Core, Net, Unity, Tests, or ADR decision content.
- Merging any PR.
- Declaring Stage 1/2 closed.
- Promoting a proposal to APPROVED unless the source contains an explicit owner decision.
- Creating Risk or Technical Debt registers before source/identifier reconciliation is complete.

## 2. Canonical authority map

| Record type | Canonical location | Rule |
|---|---|---|
| Governance policy | `docs/governance/PROJECT_GOVERNANCE_BASELINE.md` | Policy and authority map; currently PROPOSED pending owner review. |
| Source inventory | `docs/governance/SOURCE_INVENTORY.md` + `docs/audit/ATTACHMENT_SNAPSHOT_DELTA_2026-11-10.md` | Inventory and dated delta snapshots; distinguish attachments, Library entries, exports, repository copies, and reports. |
| Provenance | `docs/governance/PROVENANCE_REGISTER.md` | Source lineage and limitations; never replace originals with summaries. |
| Decisions | ADRs + `docs/governance/DECISIONS_LOG.md` | ADR is authoritative for its decision scope; log is an index. |
| Decision traceability | `docs/governance/DECISION_TRACEABILITY_REGISTER.md` | Link owner event to decision/ADR and preserve missing-source states. |
| Conflicts | `docs/governance/CONFLICT_REGISTER.md` | Preserve competing claims until source-backed resolution. |
| Requirements uncertainty | `docs/governance/TBD_REGISTER.md` | Registration is not resolution. |
| Requirement/source mapping | `docs/governance/SOURCE_TO_REQUIREMENT_MATRIX.md` | Mapping aid; does not independently approve requirements. |
| Requirement/evidence | `docs/governance/REQUIREMENT_EVIDENCE_MATRIX.md` | Evidence scope and maturity. |
| CI evidence | `docs/audit/CI_EVIDENCE_REGISTER.md` | Exact run/commit/job/artifact scope. |
| Document control | `docs/governance/DOCUMENT_CONTROL_REGISTER.md` | Document owner, authority, and change rules. |
| Work orders/actions | This file | Single work-order and action index; detailed domain facts stay in their canonical registers. |
| Session continuity | `docs/SESSION_HANDOFF.md` | Resume pointer, not a competing source of truth. |
| Project delivery status | `docs/PROJECT_STATUS_AND_EXECUTION_REGISTER.md` | Branch-aware implementation and stage/phase status. |

## 3. Execution sequence and current state

Decision status and evidence maturity are independent. Work state is a separate field.

| Sequence | Work package | Scope | Current state | Exit gate |
|---|---|---|---|---|
| A | Governance Baseline | Authority map, status axes, change control, document ownership | IMPLEMENTED ON PR #11 BRANCH — REVIEW PENDING | Owner review of governance policy; no silent canonicalization. |
| B | Source & Provenance | Reconcile attachment, Library, conversation, export, and repository source surfaces | PARTIAL — DOC-B-01 AND DOC-B-02 BOUNDED INVENTORY PASSES DONE; DOC-B-03 OPEN | Original chat identity, completeness, and provenance gaps remain explicit. |
| C | Decision & Action Tracking | Link owner decisions, ADRs, conflicts, work orders, and actions | PARTIAL | Every recovered decision has source event or explicit SOURCE PENDING; no invented approvals. |
| D | Evidence & Traceability | Link requirement → implementation → test → CI/run/artifact/commit | PARTIAL | Claims are scoped to exact branch/commit/test and unsupported claims remain NOT VERIFIED. |
| E | Session Continuity | Maintain handoff, current branch/PR/commit, blockers, and next action | IMPLEMENTED ON PR #11 BRANCH — UPDATE EACH SESSION | Handoff matches live repository state and identifies one next action. |
| F | Package Structure Reconciliation | Map every document to one functional package; identify overlaps and authority | DRAFT 0.1 CREATED — OWNER REVIEW REQUIRED | Owner accepts functional map; no path migration until separately approved. |

Work packages proceed in order. A blocked item is recorded and carried forward; it is not bypassed by changing its status.

## 4. Active action tracker

| Action ID | Work package | Action | State | Dependency / exit condition |
|---|---|---|---|---|
| DOC-A-01 | A | Preserve independent Decision Status and Evidence Maturity axes | DONE — branch documentation | Governance baseline exists; owner review remains separate. |
| DOC-A-02 | A | Establish one canonical authority per record type | DONE — initial mapping | Validate against all current documentation paths. |
| DOC-B-01 | B | Reconcile surfaced attachment objects against prior dated snapshots and record each newly surfaced object/hash | DONE — 6-OBJECT DELTA OVER THE 15-OBJECT BASELINE; 21 CURRENT OBJECTS RECORDED | See ATTACHMENT_SNAPSHOT_DELTA_2026-11-10.md and ATTACHMENT_SNAPSHOT_DELTA_2026-11-10-02.md; hashes cover mounted bytes only. |
| DOC-B-02 | B | Reconcile 40 Library file records + one folder with current attachment and repository inventories | DONE — 2026-11-11 METADATA CROSSWALK | 45 current Library file records + 1 folder; all 40 historical IDs retained; 5 additions; current conversation attachment ID matched to Library; full pre-write repository tree enumerated (87 blobs); 5 basename matches. No raw-byte/content equality is claimed. |
| DOC-B-03 | B | Preserve ChatGPT/DeepSeek origin-attribution conflict and missing original chat threads | OPEN | Requires original conversation/export identity evidence. |
| DOC-C-01 | C | Map WO-DOC-01 owner approval to this work-order record | DONE — current conversation approval | Approval applies to documentation work order, not to all proposed governance policy content. |
| DOC-C-02 | C | Recover A1-HOLD-01…06 and both WO-A3 amendments from exact owner source | BLOCKED — original source not recovered | Do not reconstruct from summaries. |
| DOC-C-03 | C | Reconcile CONFLICT-01 and CONFLICT-02 without inferring owners | OPEN | Exact requirement source and owner decision required. |
| DOC-D-01 | D | Maintain exact CI run/commit/job/artifact links | PARTIAL | Existing CI evidence register; no fresh test execution claimed. |
| DOC-D-02 | D | Link all 85 TBD and 109 consistency records to primary sources/evidence | PARTIAL | Item-level source validation and pending-source reconciliation required. |
| DOC-E-01 | E | Keep SESSION_HANDOFF aligned with live PR head and next action | IN PROGRESS | Update after each material commit/session. |
| DOC-E-02 | E | Record commit, changed paths, validation, and PR state in closeout | REQUIRED EACH DELIVERY | Exact GitHub evidence required. |
| DOC-F-01 | F | Create a functional package map and identify overlapping document responsibilities | DONE — DRAFT 0.1 ON PR #11 | `docs/audit/PACKAGE_STRUCTURE_RECONCILIATION.md`; proposal only, owner review pending. |
| DOC-F-02 | F | Obtain owner approval before renaming/moving/merging documents or fixing final numbering | BLOCKED — OWNER REVIEW REQUIRED | No migration is authorized by WO-DOC-01 alone. |

## 5. Additional bounded reconciliation checkpoint — 2026-11-10

- DOC-B-01 is complete only for the bounded attachment-ID delta: five additional objects beyond the prior 15-object snapshot are mapped and their mounted-byte hashes are recorded.
- Canonical SOURCE_INVENTORY and PROVENANCE_REGISTER now point to the dated delta addendum.
- CONFLICT_REGISTER SRC-001 now distinguishes 20 attachment objects from 13 local materialized paths and preserves earlier counts as dated snapshots.
- DECISION_TRACEABILITY_REGISTER records the direct owner authorization of WO-DOC-01 without inventing a message turn ID.
- No Project Library re-enumeration, full conversation enumeration, or original DeepSeek export recovery is claimed.
- A1-HOLD-01…06 and both WO-A3 amendments remain source-blocked.

## 6. Execution and closeout protocol

For each action:
1. Read the canonical record and source evidence.
2. State the bounded scope and expected changes.
3. Preserve original claims and unresolved alternatives.
4. Make documentation-only edits on the dedicated branch.
5. Validate changed links, IDs, counts, and cross-references.
6. Commit with a scoped message.
7. Update this register, project status, and session handoff as needed.
8. Keep the PR Draft until owner review; never merge automatically.

Every closeout must record:
- Branch and exact head SHA.
- Commit SHA and message.
- Changed paths.
- Validation performed and results.
- Work-order/action IDs advanced.
- Remaining blockers and exact next action.

## 7. Delivery log

| Date | Commit | Scope | Validation | Result |
|---|---|---|---|---|
| 2026-11-10 | `c7723063f0be5ea9613d0673c1135762a7b88a08` | WO-DOC-01 tracker, README index, status, and handoff baseline | Live GitHub branch/PR inspection; file fetch verification | Committed on documentation branch; PR #11 remains Draft. |
| 2026-11-10 | `7fd3d8e96279c70a83ed26e0bdc525494a74959d` | Five additional attachment IDs, mounted-byte SHA-256 values, and three new runtime paths | Recomputed SHA-256/byte/line counts from mounted files; fetched committed addendum; confirmed README link and PR changed-file count | DOC-B-01 completed for this bounded 5-ID delta; Library and conversation completeness remain open. |
| 2026-11-10 | `d9405d347f56ac14401bf7ab19892893fd939e70` | Recorded WO-DOC-01 owner authorization in Decision Traceability Register; corrected Stage 1 source-snapshot wording | Fetched committed register and checked approval scope/source limitation | DOC-C-01 mapped; broader decision recovery remains PARTIAL/BLOCKED. |

## 8. Current closeout

- WO-DOC-01: APPROVED — execution authorized.
- Work package A: initial baseline present on PR #11 branch; governance policy itself remains PROPOSED pending owner review.
- Work package B: PARTIAL. DOC-B-01 remains complete for the bounded 21-object attachment snapshot; DOC-B-02 is DONE for the 2026-11-11 metadata crosswalk (45 Library files + 1 folder; 40/40 prior IDs retained; 5 additions; one active conversation attachment ID matched to Library; complete pre-write repository tree of 87 blobs; five basename matches). DOC-B-03 remains OPEN for original chat/thread identity and completeness.
- Work packages C and D: partial; source and evidence gaps remain.
- Work package E: handoff exists and must be kept current.
- PR #11: OPEN / DRAFT / NOT MERGED.
- main: unchanged by this work order.
- Stage 1 / Stage 2: no closure declared.

**Next action:** DOC-B-03 — reconcile the accessible `محادثة 2.html` record and owner-attested identity relation while preserving the unresolved ChatGPT/DeepSeek origin, original-thread, and completeness gaps. Do not infer missing conversation parts or owner decisions.


## 9. Additional attachment reconciliation checkpoint — 2026-11-10

- One further distinct attachment object was surfaced after the prior 20-object checkpoint: `file_0000000022c481f4b8d45ee51c9bf603` (`قسم · قسم · مراجعة وجرد الملفات.txt`).
- Mounted bytes: 5314 bytes, 152 lines, SHA-256 `23ebb91ab1e85b562e41aaca7884ccaf98f306aab0d20ce03340ef40794e817d`.
- Bounded current context count: 21 attachment objects; mounted runtime: 14 unique text paths / 110,718 bytes.
- Added [ATTACHMENT_SNAPSHOT_DELTA_2026-11-10-02.md](../audit/ATTACHMENT_SNAPSHOT_DELTA_2026-11-10-02.md) and [SESSION_MATERIALIZATION_MANIFEST_2026-11-10-02.md](../audit/SESSION_MATERIALIZATION_MANIFEST_2026-11-10-02.md).
- DOC-B-01 remains complete only for the bounded surfaced attachment-ID/hash reconciliation. DOC-B-02 and DOC-B-03 remain open/blocked because direct Library enumeration and original chat provenance are unavailable.


## 10. DOC-D-01 live evidence re-fetch — 2026-11-10

- Re-fetched the exact commit `2820285021b1a3d7ae9acd953b3bb65677c33512`, associated Core CI run `42383513646`, job `128435932500`, decoded job log, and artifact metadata.
- Confirmed the existing log result: 187 passed, 0 failed, 0 skipped; job completed successfully; artifact `13578582284` was reported not expired with the recorded digest.
- This revalidates the metadata/log association for one existing historical row; it is not a fresh test execution and does not expand verification scope.
- DOC-D-01 remains **PARTIAL** because the five historical rows and associated artifacts have not all been freshly re-fetched in this run, and artifact contents were not downloaded/inspected.
- DOC-D-02 remains **PARTIAL**; no TBD or Consistency Matrix item is resolved by this CI check.

**Delivery log:** 2026-11-10 — live re-fetch of Core CI 187/187 evidence for exact commit/run/job/artifact; recorded in `docs/audit/CI_EVIDENCE_REGISTER.md`. Validation: exact commit association, run/job success, NUnit summary in decoded log, artifact ID/digest/expiry metadata. No tests were executed by this documentation session.


## 11. DOC-B-02 live Library and repository-tree crosswalk — 2026-11-11

- Re-fetched repository, branch, and PR #11 before writing: default branch `main`; PR #11 OPEN / DRAFT / NOT MERGED; base SHA `3dd9cfc27aa48b09b3cde802e266a8221c39f0d3`; branch `docs/project-reconciliation-baseline-2026-11-10`; pre-write head `8e7bc1ec0a0306655781755eb281ebb22d1b0ae6`; 39 changed paths, all under `docs/`.
- **DOC-B-02 content commit:** `35197b7acfc0ae31dfa958abdccf8d3f16b9abb4` — `docs(provenance): reconcile live Library metadata snapshot`; post-write PR head and branch ref were re-fetched and matched this SHA.
- Fresh recursive Library listing at `2026-11-11T08:55:12.632764Z`: 45 file records with 45 unique IDs and one folder under `/لعبة`; no warnings or continuation cursor.
- Stable-ID comparison: all 40 records in the 2026-11-09 inventory remain present (40/40); five additional Library records are listed in `SOURCE_INVENTORY.md` Section 4.1; no prior record was absent.
- Active conversation Files surface at `2026-11-11T08:55:13.004803Z`: one `محادثة 2.html` attachment, ID `file_000000007f6482109c42c450bf73d0b4`, identical to the Library record ID. The 21 historical attachment IDs from the 2026-11-10 snapshots are distinct from the 45 Library IDs.
- Complete recursive repository tree at the pre-write head: tree SHA `28aabb1420b5e794566d0001249243b5a3032fca`, 99 entries / 87 blobs, not truncated. All 45 Library basenames were compared against all 87 repository blob paths; five exact basename matches and 40 non-matches are recorded in `SOURCE_INVENTORY.md` Section 4.1.
- PR #11 changed-file list: 39 paths, all under `docs/`. Basename matches are not content/byte equality.
- Four additional text records were read as extracted text and classified as derivative/draft material. `STAGE_1_WORKING_OUTPUTS.zip` was not opened. No Library raw-byte hashes were calculated. Stage 0 Negative Attestation remains binding for its three exact records only.
- **Disposition:** DOC-B-02 DONE for the bounded metadata crosswalk. Library raw-byte identity, content equivalence, and original source lineage remain unverified. DOC-B-03 remains OPEN.

**Next single action:** DOC-B-03 — reconcile the accessible HTML export metadata with the owner-attested identity relationship without treating the export as complete or resolving the origin conflict.
