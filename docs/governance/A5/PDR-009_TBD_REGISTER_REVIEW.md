# PDR-009 — TBD Register Review

**Workstream:** A5 — Document Control  
**Status:** PARTIAL — REGISTER RECOVERED ON UNMERGED REVIEW BRANCHES; AUTHORITY / SOURCE VALIDATION INCOMPLETE  
**Document class:** Governance review / evidence assessment  
**Repository:** `live-egypt/CostOfStaying`  
**Branches examined:** `main`, PR #9 branch `docs/source-provenance-reconciliation-2026-11-09`, and PR #11 branch `docs/project-reconciliation-baseline-2026-11-10`  
**Decision effect:** This review does not create, approve, renumber, close, or delete any TBD item.

## 1. Purpose and decision question

Determine whether the repository contains an identifiable, authoritative TBD register and whether its entries can be reconciled against the A4 governance baseline without inventing identifiers or silently changing their meaning.

## 2. Search scope and reproducible findings

The following repository paths and search terms were examined through the GitHub repository connector:

| Check | Result | Evidence / limitation |
|---|---|---|
| `docs/governance/REQUIREMENTS_TRACEABILITY.md` | FOUND | Blob SHA `204d02d19e2bd7fa23a749ec818cf26f7096b514`; status is PROPOSED and its rows are explicitly preliminary examples. |
| `docs/governance/DECISIONS_LOG.md` | FOUND | Blob SHA `8cb7e4f4d2d30436bdeb05eebb61cf55b9e4c59b`; records ADR-001 through ADR-004, but no TBD register. |
| `docs/README.md` | FOUND | Blob SHA `1a69115d9a984d2b8abbda9b9c268a0639ea6d61`; governance index links the decision log and requirements traceability only. |
| `docs/governance/TBD_REGISTER.md` on `main` | NOT FOUND | Contents API returned 404 on `main`; branch-aware search subsequently located copies on PR #9 and PR #11 branches. |
| `docs/governance/A4/TBD_REGISTER.md` | NOT FOUND | Contents API returned 404 on the examined default branch. |
| `docs/governance/A4/REQUIREMENTS_TRACEABILITY_MATRIX.md` | NOT FOUND | Contents API returned 404 on the examined default branch. |
| Default-branch code/document search for `TBD-01`, `TBD-45`, `TBD Register`, and `CONFLICT-02` | NO MATCH ON `main` | Search was initially scoped too narrowly; it did not cover historical PR branches. |
| Broad repository search for `TBD` | MATCHES | Returned general design/architecture documents with unresolved design values, not a structured item register. |

### 2.1 Branch-aware evidence recovered

The initial `main`-only search was insufficient. Direct fetches subsequently located these historical review artifacts:

| Branch / PR | Artifact | Blob SHA | Result |
|---|---|---|---|
| `docs/source-provenance-reconciliation-2026-11-09` / PR #9 | `docs/governance/TBD_REGISTER.md` | `57f0e4e7727835cb01e8570253dacb8b34d14a78` | Source-recorded transcription of Project Library record `TBD_REGISTER.md`, file ID `file_0000000087cc821087da41b9341877d6`. |
| Same branch / PR #9 | `docs/governance/SOURCE_TO_REQUIREMENT_MATRIX.md` | `6eaab3c87141f35fcf8e0f8ec8e29faa441e9648` | 85 unique TBD rows. |
| `docs/project-reconciliation-baseline-2026-11-10` / PR #11 | `docs/governance/TBD_REGISTER.md` | `64071ca898b8cdb652c03aec61a53f000721cff3` | Consolidated proposed baseline copy. |
| Same branch / PR #11 | `docs/governance/SOURCE_TO_REQUIREMENT_MATRIX.md` | `fef95a066bd29dda19f0b3eb8e9616d254a41724` | 85 unique TBD rows. |

A structural check of the two register files found 85 unique identifiers, TBD-01…TBD-85. The register contains 87 table rows because TBD-37 and TBD-45 each appear again in conflict/clarification records. The two source-to-requirement matrices each contain 85 unique item rows with no ID gaps.

The recovered register records these maturity ranges: TBD-01…20 PENDING SOURCE; TBD-21…58 RECEIVED / SOURCE VALIDATION PENDING WITH OWNER DISCREPANCIES; TBD-59…85 REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING. TBD-53 is a parent process with 54 subtask IDs (`TBD-53-01…TBD-53-54`), but the individual mappings are not supplied in the source export.

PR #9 is unmerged and based on `feature/core-lifecycle-resolution`; PR #11 is an unmerged proposed consolidation candidate. Neither branch is `main` or an approved canonical register. PR #8 also carries a separate overlapping source/provenance package under `docs/governance/sources/`; DOC-PACKAGE-01 remains OPEN.

### 2.2 Source-derived baseline — authority still unresolved

The A4.6 record's historical 85-item claim is corroborated by the recovered PR #9 / PR #11 register and matrix copies. However, the register copy states that raw-byte equality with the Project Library payload is not asserted, and the item maturity split explicitly leaves source verification incomplete. Existence and structural completeness do not mean approval, requirement resolution, or implementation verification.

## 3. Evidence assessment

| Required element | Finding | Status |
|---|---|---|
| Canonical register path and file | Register located on PR #9 / PR #11 branches; absent from examined `main` | PARTIAL — AUTHORITY UNRESOLVED |
| Item inventory | 85 unique IDs and 85 matrix rows recovered on PR branches | PARTIAL — SOURCE VALIDATION INCOMPLETE |
| Stable TBD identifiers and exact wording | 85 unique IDs present in register and matrix copies | RECORDED — SOURCE VALIDATION PARTIAL |
| Per-item owner and priority | Not established by a register | MISSING |
| Origin / source reference per item | Not established systematically | MISSING |
| Dependency and conflict mapping | Not available as a complete register | MISSING |
| Status history and closure authority | Not established | MISSING |
| Evidence supporting closure of individual items | Not established | MISSING |

## 4. Determination

**PDR-009 = PARTIAL — 85-ITEM REGISTER AND SOURCE-TO-REQUIREMENT MATRIX RECOVERED ON UNMERGED REVIEW BRANCHES; CANONICAL AUTHORITY AND FULL SOURCE VERIFICATION NOT ESTABLISHED.**

The branch-aware inspection corrects the initial `main`-only finding. The 85-item register and matching 85-row matrix exist on PR #9 and PR #11 branches. Their existence establishes a recoverable documented register and structural item inventory, but not canonical approval, raw-byte equality with the Project Library record, complete per-item provenance, or resolution of the underlying TBDs. The source's maturity split remains binding: TBD-01…20 PENDING SOURCE; TBD-21…58 RECEIVED / SOURCE VALIDATION PENDING WITH OWNER DISCREPANCIES; TBD-59…85 REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING. No item is closed, deleted, superseded, renumbered, or reclassified by this review.

## 5. Required recovery and reconciliation

1. Reconcile PR #9 and PR #11 register copies against the original Project Library record and the applicable source exports; preserve all versions.
2. Confirm the 85 unique IDs and 54 TBD-53 subtask mappings without treating duplicate conflict rows as extra requirements.
3. Resolve DOC-PACKAGE-01 path/base overlap and identify the approved canonical register location.
4. Build a row-level reconciliation table containing: TBD ID, exact original wording, source document and section, originating decision/PDR/conflict, owner, priority, dependencies, current status, closure evidence, and disposition.
4. Compare every recovered row with the current repository documents and A4 decisions. Record exact matches, partial matches, conflicts, missing entries, and duplicates without silently resolving them.
5. Confirm special references such as TBD-37 and TBD-45 against their original records; retain their current open status until evidence supports a decision.
6. Obtain explicit governance disposition for each item. A missing register must not be repaired by fabricating item descriptions or identifiers.

## 6. Closure criteria

PDR-009 may be reconsidered only when all of the following are satisfied:

- The authoritative original register (or formally accepted reconstruction with provenance) is available.
- The full inventory and exact item wording are preserved.
- Every item is mapped to its source and relevant governance decision.
- Conflicts, duplicates, gaps, and status discrepancies are recorded and dispositioned.
- No item is marked closed without the required decision and evidence.
- The reconciled register and its approval record are committed and independently reviewable.

## 7. Non-effects and boundaries

- No changes to A4 closure decisions.
- No resolution of CONFLICT-01 or CONFLICT-02.
- No change to TBD-37 or TBD-45 status.
- No approval of requirements, ADRs, implementation, or Code Gate readiness.
- No changes to gameplay, Core, Unity, FishNet, CI, or runtime code.
- No merge to `main` is performed by this review.

## 8. Status summary

| Item | Status |
|---|---|
| Repository search and evidence review | IMPLEMENTED — review recorded in this document |
| TBD register copy located on PR #9 / PR #11 | VERIFIED — BRANCH CONTENT ONLY |
| 85 unique item IDs / matrix rows | VERIFIED — STRUCTURAL CHECK; AUTHORITY / CONTENT VALIDATION PARTIAL |
| PDR-009 final resolution | PARTIAL — NOT CLOSED |
| A5.5 closure | NOT ACHIEVED |
