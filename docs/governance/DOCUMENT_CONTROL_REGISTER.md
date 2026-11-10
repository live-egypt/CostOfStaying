# Document Control Register — Cost of Staying

> **Governance status axes:** Decision Status and Evidence Maturity are independent. This document's existence does not approve its claims or prove implementation. See [Project Governance Baseline](PROJECT_GOVERNANCE_BASELINE.md).

**Revision date:** 2026-11-10  
**Record State:** INITIAL RECONCILIATION SNAPSHOT — DOCUMENTATION ONLY; CONTENT AND SOURCE COMPLETENESS REMAIN PARTIAL
**Scope:** Initial source-of-truth mapping for the repository and Project Library documents inspected in this reconciliation. This is not yet a complete recursive repository document inventory.

## 1. Control principles

1. Each information type must have one authoritative home.
2. Supporting audits may analyze and cross-reference an item but must not create competing definitions.
3. Do not delete, rename, merge, or renumber documents before provenance and content mapping.
4. Project Library records and repository files are separate objects until an explicit transfer/commit occurs.
5. A document's existence does not prove its claims, implementation, or verification.
6. Keep Stage 0–5 and Phase 1–3 separate.
7. Preserve original IDs and historical references. Resolve collisions through a cross-reference map, not silent renumbering.

## 2. Initial authoritative-document map

| Information type | Current authoritative location | Supporting sources | Current control status |
|---|---|---|---|
| Project overview and navigation | `docs/README.md` | Repository docs | PRESENT |
| Overall execution and evidence boundary | `docs/PROJECT_STATUS_AND_EXECUTION_REGISTER.md` | CI reports, ADRs, audit reports | PRESENT — requires reconciliation against new source records |
| Requirements traceability | `docs/governance/TBD_REGISTER.md` (85-item uncertainty register), `SOURCE_TO_REQUIREMENT_MATRIX.md`, and `REQUIREMENTS_TRACEABILITY.md` (gameplay/Core traceability) | `SOURCE_TRACEABILITY_REGISTER.md`, `TBD_CROSS_REFERENCE_MAP.md`, `CONSISTENCY_MATRIX.md` | PARTIAL — original source matching remains incomplete; keep the 85 TBDs separate from the 109-record consistency matrix |
| Architecture decisions | Individual `docs/governance/adr/ADR-*.md` files | `docs/governance/DECISIONS_LOG.md` | PRESENT — status remains ADR-specific |
| Decision/owner source events | `docs/governance/DECISION_TRACEABILITY_REGISTER.md` | Original ChatGPT export candidate, ADR records, supplied reports | INITIAL — source event revalidation open |
| Source index | `docs/governance/SOURCE_INVENTORY.md` | Project attachments and Library listing | DOCUMENTED — coverage partial |
| Provenance | `docs/governance/PROVENANCE_REGISTER.md` | Stage 0 Negative Attestation and raw attachment hashes | DOCUMENTED — Library provenance partial |
| Conflict records | `docs/governance/CONFLICT_REGISTER.md` | Original owner-conflict source values and branch-scoped decision records | OPEN — no owner conflict resolved |
| Network architecture | `docs/architecture/NETWORK_ARCHITECTURE.md` | Network Audit source materials | DOCUMENT PRESENT — audit reconciliation open |
| Security architecture and controls | `docs/security/*` | Security reconciliation material and security test plan | DOCUMENTS PRESENT — design/implementation/verification not implied |
| Requirement evidence mapping | `docs/governance/REQUIREMENT_EVIDENCE_MATRIX.md` | Source-to-Requirement Matrix, exact code/test/CI artifacts when mapped | INITIAL SCAFFOLD — implementation/test evidence not assessed item-by-item |
| Core CI evidence | `docs/audit/CI_EVIDENCE_REGISTER.md` and `docs/PROJECT_STATUS_AND_EXECUTION_REGISTER.md` | GitHub Actions runs/artifacts | RUN-SCOPED; no Unity/FishNet/Android/Linux inference |
| Cross-document design review | `docs/audit/DESIGN_CONSISTENCY_REVIEW_2026-11-10.md` | GDD, rules, system specifications, ADRs, architecture, security and quality docs | REVIEWED — findings remain proposals/open until owner decision |
| Requirements register structural QA | `docs/audit/REQUIREMENT_REGISTERS_QA_2026-11-10.md` | TBD Register, Source-to-Requirement Matrix, Consistency Matrix | REVIEWED — structural counts only; source resolution not implied |
| ADR/Core implementation reconciliation | `docs/audit/ADR_RECONCILIATION_REVIEW_2026-11-10.md` | Main ADR-001…004, feature-branch ADR-005…015, Core source/tests/CI | REVIEWED — branch-specific; no ADR status changed |
| Core implementation review | `docs/audit/CORE_IMPLEMENTATION_REVIEW_2026-11-10.md` | Core source, NUnit tests, project files and CI logs | REVIEWED — source-level; no fresh tests run |
| Stage evidence | Project Library Stage 0–2 records | `STAGE_1.md`, `STAGE_2.md`, review and closure records | LIBRARY-ONLY unless separately committed |
| Central risk register | Not created in repository | Gameplay, Psychology, Architecture, Engineering, Network, Security materials | DEFERRED pending source/ID reconciliation |
| Technical debt register | Not created in repository | Engineering and repository audits | DEFERRED pending classification |
| Final readiness assessment | Not issued | All audits and validation evidence | NOT ISSUED |

## 3. Repository documents inspected in this pass

- `docs/README.md`
- `docs/PROJECT_STATUS_AND_EXECUTION_REGISTER.md`
- `docs/audit/DESIGN_CONSISTENCY_REVIEW_2026-11-10.md`
- `docs/audit/CI_EVIDENCE_REGISTER.md`
- `docs/audit/REQUIREMENT_REGISTERS_QA_2026-11-10.md`
- `docs/audit/ADR_RECONCILIATION_REVIEW_2026-11-10.md`
- `docs/audit/CORE_IMPLEMENTATION_REVIEW_2026-11-10.md`
- `docs/governance/REQUIREMENTS_TRACEABILITY.md`
- `docs/governance/adr/ADR-001` through `ADR-004` on main.
- `docs/architecture/NETWORK_ARCHITECTURE.md` — reviewed in the main snapshot.
- Security documents listed in `docs/README.md`: `SECURITY_ARCHITECTURE.md`, `TRUST_BOUNDARIES.md`, `THREAT_MODEL.md`, `SECRETS_MANAGEMENT.md`, `DATA_PRIVACY.md`, `INCIDENT_RESPONSE.md`.
- `docs/quality/SECURITY_TEST_PLAN.md`.

The list is the inspected subset, not a full repository tree inventory.

## 4. Project Library-only documents requiring mapping

The following source records now have repository text copies in this branch: `SOURCE_INVENTORY.md`, `PROVENANCE_REGISTER.md`, `TBD_REGISTER.md`, `SOURCE_TRACEABILITY_REGISTER.md`, `CONSISTENCY_MATRIX.md`, `SOURCE_TO_REQUIREMENT_MATRIX.md`, `TBD_CROSS_REFERENCE_MAP.md`, and `REQUIREMENT_EVIDENCE_MATRIX.md`. The consolidated repository conflict record is `CONFLICT_REGISTER.md`; no separate `DECISION_AND_CONFLICT_LOG.md` is created to avoid a competing conflict source. Library originals remain unchanged; raw-byte equality is not asserted.

Other current Library records still requiring mapping include:
- `FULL_DOCUMENTS_REVIEW.md`, `REVIEW_INDEX.md`, `FINAL_REVIEW_REPORT.md`
- Remaining Stage 0/1/2 evidence records and archives
- `FULL_DOCUMENTS_REVIEW.md`, `REVIEW_INDEX.md`, `FINAL_REVIEW_REPORT.md`
- `STAGE_1.md`, `STAGE_2.md`, `STAGE_2_REVIEW.md`, `STAGE_2_CLOSURE_DECLARATION.md`
- `PROGRESS_LOG.md`, `PROGRESS_LOG(1).md`, `PROVENANCE_RECONCILIATION_NOTE_STAGE0.md`, `NEGATIVE_ATTESTATION_STAGE0.md`
- Diff files, checksum manifests, and review/CI archives.

Library-only status is not equivalent to repository synchronization. This branch receives the source/reconciliation registers listed above; other Library-only files remain unmigrated unless explicitly listed in a later commit.

## 5. Naming and numbering control

- No final package numbering is established by this register.
- The proposed hybrid package structure remains a working proposal until reviewed.
- `RISK-013` appears in supplied materials for more than one description. Preserve source IDs and create a collision map before assigning canonical identities.
- Threat IDs, requirement IDs, risk IDs, technical-debt IDs, test IDs, and evidence IDs must remain distinct namespaces.
- `CONFLICT-01` and `CONFLICT-02` remain open.
- Do not create a second authoritative risk register before source reconciliation is complete.

## 6. Change control

For every document change, record:
- Path and document owner (or TBD).
- Source/authority and affected IDs.
- Prior and new revision.
- Reason for change.
- Related PR/commit.
- Review status.
- Whether the change affects design approval or merely records evidence.

A repository document update does not alter the Project Library source record. A Library update does not update the repository. Keep the two surfaces distinct.

## 7. Open control tasks

| ID | Task | Status |
|---|---|---|
| DOC-CTRL-01 | Complete recursive repository docs inventory | OPEN |
| DOC-CTRL-02 | Map all Library documents to repository documents | OPEN |
| DOC-CTRL-03 | Reconcile Stage 0/1/2 evidence and path/version conflicts | OPEN |
| DOC-CTRL-04 | Approve final hybrid package structure | PENDING OWNER REVIEW |
| DOC-CTRL-05 | Complete source-to-requirement mapping for TBD-01…85 | OPEN |
| DOC-CTRL-06 | Reconcile Network/Security/Architecture source responsibilities | OPEN |
| DOC-CTRL-07 | Maintain claim-scoped evidence register and complete artifact-to-claim mapping | INITIAL REGISTER CREATED — ITEM-LEVEL MAPPING OPEN |
| DOC-CTRL-08 | Resolve DOC-PACKAGE-01 overlap between PR #8 and PR #9 | OPEN — OWNER REVIEW REQUIRED |


## 8. Parallel Stage 1 documentation PR overlap

A separate pre-existing documentation PR was discovered during final review:

| PR | Base | Head branch | Scope | Current state |
|---|---|---|---|---|
| [#8](https://github.com/live-egypt/CostOfStaying/pull/8) | `main` | `docs/stage1-source-provenance-recovery` | Seven Stage 1 source/provenance documents under `docs/governance/sources/` plus README index | OPEN — not merged |
| [#9](https://github.com/live-egypt/CostOfStaying/pull/9) | `feature/core-lifecycle-resolution` | `docs/source-provenance-reconciliation-2026-11-09` | Fourteen documentation files, including source/provenance registers and the 85-item TBD mapping package | OPEN — DRAFT — not merged |

The two PRs overlap in source inventory, provenance, decision traceability, document control, and README indexing, but use different paths and base branches. PR #9 also contains the migrated 85-item TBD/traceability/consistency source records and initial evidence matrices.

**DOC-PACKAGE-01 — OPEN:** PR #8 and PR #9 remain overlapping review candidates and neither is merged. The current branch is a separately authorized consolidation candidate based on main; it places the source/governance records at one root path set, retains the 85-item and 109-record source material, and uses one CONFLICT_REGISTER.md. This does not make the new paths canonical until PR review and owner acceptance. Do not merge PR #8 or PR #9 as-is. This is a document-package conflict, not CONFLICT-01 or CONFLICT-02.


## 9. PR path reconciliation — consolidation candidate

The following mapping prevents the PR #8 and PR #9 packages from becoming competing sources. These are proposed consolidated paths in this branch; they do not become canonical until PR review and owner acceptance.

| PR #8 source path | PR #9 source path | Consolidated candidate path in this branch | Treatment |
|---|---|---|---|
| docs/governance/sources/SOURCE_INVENTORY.md | docs/governance/SOURCE_INVENTORY.md | docs/governance/SOURCE_INVENTORY.md | One inventory document; PR #9 copy retained as the richer reconciliation base. |
| docs/governance/sources/PROVENANCE_REGISTER.md | docs/governance/PROVENANCE_REGISTER.md | docs/governance/PROVENANCE_REGISTER.md | One provenance document; raw Library limitations preserved. |
| docs/governance/sources/DECISION_TRACEABILITY_MATRIX.md | docs/governance/DECISION_TRACEABILITY_REGISTER.md | docs/governance/DECISION_TRACEABILITY_REGISTER.md | Register retained; matrix naming is not duplicated. |
| docs/governance/sources/CONFLICT_REGISTER.md | docs/governance/DECISION_AND_CONFLICT_LOG.md | docs/governance/CONFLICT_REGISTER.md | Conflicts consolidated here; no second decision/conflict log created. |
| docs/governance/sources/DOCUMENT_CONTROL.md | docs/governance/DOCUMENT_CONTROL_REGISTER.md | docs/governance/DOCUMENT_CONTROL_REGISTER.md | One document-control register with path-mapping record. |
| docs/audit/STAGE1_SOURCE_PROVENANCE_REVIEW.md | — | docs/audit/STAGE1_SOURCE_PROVENANCE_REVIEW.md | New bounded review synthesis; not claimed byte-identical to a Library or PR source file. |
| — | docs/governance/TBD_REGISTER.md | docs/governance/TBD_REGISTER.md | Preserve the 85-item register and its unresolved fields. |
| — | docs/governance/SOURCE_TRACEABILITY_REGISTER.md | docs/governance/SOURCE_TRACEABILITY_REGISTER.md | Preserve source-event trace and missing-source limits. |
| — | docs/governance/SOURCE_TO_REQUIREMENT_MATRIX.md | docs/governance/SOURCE_TO_REQUIREMENT_MATRIX.md | Preserve 85 item mappings; source match remains partial. |
| — | docs/governance/CONSISTENCY_MATRIX.md | docs/governance/CONSISTENCY_MATRIX.md | Preserve 109 rows and 24 reported pending-source records. |
| — | docs/governance/TBD_CROSS_REFERENCE_MAP.md | docs/governance/TBD_CROSS_REFERENCE_MAP.md | Preserve non-equivalent cross-reference relationships. |
| — | docs/governance/REQUIREMENT_EVIDENCE_MATRIX.md | docs/governance/REQUIREMENT_EVIDENCE_MATRIX.md | Evidence scaffold; NOT ASSESSED is not a negative implementation claim. |
| — | docs/audit/RECONCILIATION_PROGRESS.md | docs/audit/RECONCILIATION_PROGRESS.md | Rewritten as the 2026-11-10 checkpoint for this branch. |
| — | docs/PROJECT_STATUS_AND_EXECUTION_REGISTER.md | docs/PROJECT_STATUS_AND_EXECUTION_REGISTER.md | Synthesized against main and current branch/PR evidence; does not inherit Core code. |

PR #8 and PR #9 remain open and untouched. Their branches are preserved for review/history. Do not merge either package as-is while this path consolidation candidate is under review.

## 10. Updated control tasks

| ID | Task | Status |
|---|---|---|
| DOC-CTRL-09 | Maintain one candidate path per information type across PR #8, PR #9 and this branch | INITIAL PATH MAP RECORDED — OWNER REVIEW REQUIRED |
| DOC-CTRL-10 | Confirm canonical path set and integration decision | PENDING OWNER REVIEW |
