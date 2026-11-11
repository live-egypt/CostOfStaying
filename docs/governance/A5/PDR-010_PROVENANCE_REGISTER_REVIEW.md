# PDR-010 — Provenance Register Review

**Workstream:** A5 — Document Control  
**Status:** CANNOT RESOLVE — CANONICAL PROVENANCE REGISTER NOT LOCATED  
**Document class:** Governance review / evidence assessment  
**Repository:** `live-egypt/CostOfStaying`  
**Base examined:** `main` at review time  
**Decision effect:** This review does not establish or infer the origin, authorship, approval, or authority of undocumented source material.

## 1. Purpose

Assess whether the repository contains a canonical provenance register capable of tracing governance and design artifacts to their origin, version, custody, and approval evidence.

## 2. Search scope and findings

Repository search on the default branch was performed for the exact and related terms: `SOURCE-ORIGIN-01`, `provenance register`, `PROVENANCE`, `source inventory`, and `LIF-DEC-01`. No matching repository files were returned for these queries.

The following governance documents were also inspected:

| Artifact | Finding |
|---|---|
| `docs/governance/DECISIONS_LOG.md` | Records ADR-001..004; does not provide a source-provenance ledger. |
| `docs/governance/REQUIREMENTS_TRACEABILITY.md` | PROPOSED requirements examples; not a provenance register. |
| `docs/README.md` | No provenance register is indexed. |
| A5 PDR-005 review | Exists on its separate, unmerged feature branch / PR #13; it records missing source evidence for LIF-DEC-01, but is not itself the original source or a canonical provenance register. |

A repository search returning no result is limited by the repository index and examined branch. It does not prove that source material never existed, is not present in another branch, or is unavailable in the conversation/file archive.

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

- No canonical provenance register was located on the examined default branch.
- No repository artifact matching SOURCE-ORIGIN-01 was found by exact search.
- No complete source inventory was located in the examined governance index.
- The source origin, original wording, version, and approval evidence for LIF-DEC-01 remain unestablished in the available repository evidence reviewed for PDR-005.
- The separate PDR-005 review is an evidence assessment, not a substitute for the missing original source.
- Provenance of historical conversation exports and uploaded files has not been reconciled into a row-level ledger.
- No complete mapping from each A4/A5 decision and PDR to its originating source artifact is available here.

## 5. Determination

**PDR-010 = CANNOT RESOLVE — CANONICAL PROVENANCE REGISTER AND COMPLETE SOURCE INVENTORY NOT LOCATED.**

This is not a determination that the source artifacts are invalid or never existed. It means the evidence required to establish their provenance and authority has not been assembled into a verifiable register.

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
- Does not resolve PDR-009 or create a TBD inventory.
- Does not change A4 closure decisions or approve any ADR.
- Does not authorize code, gameplay, Unity, networking, CI, or runtime changes.
- Does not merge any PR or alter `main`.

## 9. Status summary

| Item | Status |
|---|---|
| Repository provenance search | IMPLEMENTED — review recorded |
| Canonical provenance register located | NOT LOCATED |
| Complete source inventory | TBD |
| Source authority / approval mapping | INSUFFICIENT EVIDENCE |
| PDR-010 final resolution | CANNOT RESOLVE |
| A5.6 closure | NOT ACHIEVED |
