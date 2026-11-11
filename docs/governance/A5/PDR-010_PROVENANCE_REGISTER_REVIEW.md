# PDR-010 — Provenance Register Review

**Workstream:** A5 — Document Control  
**Status:** PARTIAL — MULTIPLE PROVENANCE REGISTERS RECOVERED ON UNMERGED REVIEW BRANCHES; SOURCE AUTHORITY / COVERAGE INCOMPLETE  
**Document class:** Governance review / evidence assessment  
**Repository:** `live-egypt/CostOfStaying`  
**Branches examined:** `main`, PR #8 branch `docs/stage1-source-provenance-recovery`, PR #9 branch `docs/source-provenance-reconciliation-2026-11-09`, and PR #11 branch `docs/project-reconciliation-baseline-2026-11-10`  
**Decision effect:** This review does not establish or infer the origin, authorship, approval, or authority of undocumented source material.

## 1. Purpose

Assess whether the repository contains a canonical provenance register capable of tracing governance and design artifacts to their origin, version, custody, and approval evidence.

## 2. Search scope and findings

The initial exact-term search was performed on the default branch only and returned no matches. That search scope was insufficient. A subsequent branch-aware inspection directly fetched provenance and source-inventory registers from PR #8, PR #9, and PR #11 branches.

| Branch / PR | Provenance artifact | Blob SHA | Source inventory artifact | Blob SHA |
|---|---|---|---|---|
| `docs/stage1-source-provenance-recovery` / PR #8 | `docs/governance/sources/PROVENANCE_REGISTER.md` | `fcaeb5d2acd0cdaeaf0d08b68a30754f4623efb5` | `docs/governance/sources/SOURCE_INVENTORY.md` | `6ae0d344a968f9f94ed481501a9f2dbd48abf599` |
| `docs/source-provenance-reconciliation-2026-11-09` / PR #9 | `docs/governance/PROVENANCE_REGISTER.md` | `d7f08fbb3ccd091881a8a29416bad92da34eb13c` | `docs/governance/SOURCE_INVENTORY.md` | `2cac4bdec6d30f5dba28b61c9e2d87b8e0981cfb` |
| `docs/project-reconciliation-baseline-2026-11-10` / PR #11 | `docs/governance/PROVENANCE_REGISTER.md` | `2c13e9d5e20627ce730eb34dff098b6cd9022c4d` | `docs/governance/SOURCE_INVENTORY.md` | `ae4400e134f5fd2385a27b8b01c19a5e751147de` |

The PR #8 register explicitly marks its state PARTIAL, with 15 current attachments hashed and Library/chat provenance limited. PR #9 labels its provenance register IMPLEMENTED as a repository document but PROVENANCE PARTIAL; its source inventory also says coverage PARTIAL. PR #11 is a later metadata-only crosswalk and keeps content/source completeness PARTIAL. These are real branch artifacts, not approved canonical authority.

The following `main`-branch governance documents were also inspected:

| Artifact | Finding |
|---|---|
| `docs/governance/DECISIONS_LOG.md` | Records ADR-001..004; does not provide a source-provenance ledger. |
| `docs/governance/REQUIREMENTS_TRACEABILITY.md` | PROPOSED requirements examples; not a provenance register. |
| `docs/README.md` | No provenance register is indexed on `main`; the PR #8/#9/#11 branches index distinct source/provenance packages. |
| A5 PDR-005 review | Exists on its separate, unmerged feature branch / PR #13; it records missing source evidence for LIF-DEC-01, but is not itself the original source or a canonical provenance register. |

The recovered branch documents establish that provenance registers exist as repository artifacts on PR #8/#9/#11. Their existence does not establish approval, canonical path, full coverage, or closure of the source-origin gaps. PR #8 and PR #9 overlap with different paths and base contexts; PR #11 is a proposed consolidation candidate. DOC-PACKAGE-01 remains OPEN.

## 3. Provenance fields required

A usable provenance register should preserve, at minimum:

| Field | Purpose |
|---|---|
| Source ID | Stable identifier; never reused. |
| Artifact title and type | Distinguishes decision, ADR, PDR, requirement, conversation export, attachment, and implementation evidence. |
| Original location | Repository path, conversation/export identifier, or external source location. |
| Originator / issuing authority | Recorded only where the source itself supports attribution. |
| Created / issued date | Original date, with timezone where relevant; unknown values remain unknown. |
| Version / revision | Original version label and immutable commit/hash where available. |
| Integrity reference | Git blob/commit SHA or file hash where available. |
| Custody / acquisition record | How and when the artifact entered the project record. |
| Authority class | Governing, approved, proposed, reference, historical, or unverified; must follow the approved classification rules. |
| Approval evidence | Decision ID, approving authority, date, and exact scope, or explicit NONE LOCATED. |
| Supersession relationship | Replaced-by / supersedes references supported by explicit evidence. |
| Related records | PDR, ADR, conflict, TBD, requirement, PR, or issue identifiers. |
| Review status | Verified, partial, missing, disputed, or not assessed, with evidence reference. |

These are proposed schema fields for future reconciliation, not a claim that the project has approved this schema.

## 4. Evidence gaps

- No provenance register is indexed or present in the examined `main` governance package; provenance registers are present on unmerged PR #8/#9/#11 branches.
- The provenance registers are explicitly partial and do not close SOURCE-ORIGIN-01.
- Canonical path and package authority remain unresolved under DOC-PACKAGE-01.
- The source origin, original wording, version, and approval evidence for LIF-DEC-01 remain unestablished in the available repository evidence reviewed for PDR-005.
- The separate PDR-005 review is an evidence assessment, not a substitute for the missing original source.
- Provenance of historical conversation exports and uploaded files has not been reconciled into a row-level ledger.
- No complete mapping from each A4/A5 decision and PDR to its originating source artifact is available here.

## 5. Determination

**PDR-010 = PARTIAL — PROVENANCE REGISTERS AND SOURCE INVENTORIES RECOVERED ON UNMERGED REVIEW BRANCHES; CANONICAL AUTHORITY AND FULL SOURCE COVERAGE NOT ESTABLISHED.**

The branch-aware review corrects the earlier default-branch-only conclusion. PR #8, PR #9, and PR #11 contain provenance/source-inventory documents. Their own status declarations and recorded gaps show partial coverage. This review therefore cannot claim that no register exists; it also cannot treat any branch copy as an approved canonical register or declare source provenance complete.

No authorship, date, approval, supersession, or authority is inferred from filenames, current repository placement, search ranking, or an unmerged review document.

## 6. Recovery and reconciliation procedure

1. Collect source artifacts from the repository history, all relevant feature branches and PRs, uploaded project files, and the authoritative conversation/export archive.
2. Preserve each original artifact unchanged; record its original filename/location, acquisition date, and integrity hash where available.
3. Assign stable Source IDs only after confirming that an ID does not already exist in the authoritative source records.
4. Record unknown or disputed metadata explicitly as UNKNOWN / NOT LOCATED; do not infer it.
5. Map each PDR, ADR, conflict, TBD, and decision to the exact source passages that support it.
6. Compare conflicting copies and revisions; preserve each version and record the relationship rather than overwriting history.
7. Reconcile provenance status with PDR-005, PDR-009, PDR-011, PDR-012, and PDR-014 without treating any unmerged PR as an approved baseline.
8. Submit the resulting register for the applicable governance review and record its approval decision separately.

## 7. Closure criteria

PDR-010 can be reconsidered when:
- The source inventory covers the required governance and project artifacts.
- Each entry has a stable identifier and a traceable original location.
- Version/integrity metadata is recorded wherever available.
- Authority and approval are supported by explicit evidence or clearly marked absent.
- Supersession and conflict relationships are supported by source records.
- Missing, disputed, and unavailable sources are explicitly enumerated.
- The register is indexed, committed, and independently reviewable.

## 8. Non-effects

- Does not resolve SOURCE-ORIGIN-01.
- Does not resolve LIF-DEC-01 supersession or close PDR-005.
- Does not resolve PDR-009; a TBD register is present on PR #9/#11 branches, but remains only partially source-validated.
- Does not change A4 closure decisions or approve any ADR.
- Does not authorize code, gameplay, Unity, networking, CI, or runtime changes.
- Does not merge any PR or alter `main`.

## 9. Status summary

| Item | Status |
|---|---|
| Repository provenance search | IMPLEMENTED — review recorded |
| Provenance register documents located | VERIFIED — PR #8 / PR #9 / PR #11 branches; unmerged |
| Complete source inventory | PARTIAL — NOT ESTABLISHED |
| Source authority / approval mapping | INSUFFICIENT EVIDENCE |
| PDR-010 final resolution | PARTIAL — NOT CLOSED |
| A5.6 closure | NOT ACHIEVED |
