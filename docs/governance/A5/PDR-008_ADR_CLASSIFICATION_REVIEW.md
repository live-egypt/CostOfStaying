# PDR-008 — ADR-C-002 / ADR-C-005 Classification Review

**Project:** Cost of Staying (ثمن البقاء)  
**Workstream:** A5 — Document Control  
**Document ID:** PDR-008  
**Document Version:** 0.1.0  
**Status:** EVIDENCE REVIEW — CANNOT RESOLVE  
**Prepared:** 2026-11-11  
**Repository:** live-egypt/CostOfStaying  
**Branch:** docs/a5-pdr-008-adr-classification-review  
**PDR Closure:** NOT PERFORMED  
**ADR Status Change:** NONE

---

## 1. Purpose

Review the unresolved classification and relationship claims concerning ADR-C-002 and ADR-C-005. The objective is to establish what can and cannot be supported by the recovered records, identify the missing primary evidence, and prevent classification labels from being treated as architectural authority without proof.

This review does not reconstruct either ADR, merge them, assign a classification system, or promote their status.

## 2. A4 baseline

The A4 PDR recommendation record identifies:

- **PDR:** PDR-008 — ADR-C-002/005 Classification.
- **Status:** OPEN.
- **Dependencies:** ADR-REV-001; ARCH-01; CONFLICT-01/02; SOURCE-ORIGIN-01.
- **Known:** There are references to ADR-C-002 and ADR-C-005, and a general gap in verifying ADR classification and approval status.
- **Missing:** The two original texts; definition of the classification system; relationship between ADR-C-002 and ADR-C-005; and owner approval for the classification.
- **A4 recommendation:** CANNOT RESOLVE — classification, merging, or relationship cannot be accepted without recovering the texts and governing decision.
- **Deferral:** ADR Review / A5.
- **Impact:** Unresolved classifications must not be used as binding design authority; the impact of each ADR must be examined before the phase that depends on it.

## 3. Repository inspection

The repository default branch is `main`. Searches were performed for:

- `ADR-C-002`
- `ADR-C-005`
- `PDR-008`
- `ADR classification`

No matching repository file was returned by these searches.

The current `docs/governance/DECISIONS_LOG.md` lists ADR-001 through ADR-004 only. It states that the register reflects actual ADR files and that ADR state must not be changed without updating the ADR file and decision log.

This finding is bounded to the repository content and search results available for this review. It does not prove the referenced ADRs never existed in another branch, conversation, Project Library, export, or unindexed source.

## 4. Evidence matrix

| Evidence ID | Required evidence | Current finding | Status |
|---|---|---|---|
| P008-E01 | Original ADR-C-002 complete text | Not recovered in the repository search or available A4 baseline. | MISSING |
| P008-E02 | Original ADR-C-005 complete text | Not recovered in the repository search or available A4 baseline. | MISSING |
| P008-E03 | Canonical classification taxonomy and definitions | No authoritative taxonomy or governing definition recovered. | MISSING |
| P008-E04 | Explicit relationship between ADR-C-002 and ADR-C-005 | No source establishes whether they are independent, overlapping, conflicting, or one supersedes the other. | MISSING |
| P008-E05 | Owner approval for classification and relationship | No primary owner approval event or source locator recovered. | MISSING |
| P008-E06 | ADR-REV-001 provenance | Original approval/reconfirmation events and source-to-ADR trace remain pending. | OPEN |
| P008-E07 | ARCH-01 status reconciliation | Current canonical status and original approval trace for in-scope architecture decisions remain incomplete. | PARTIAL |
| P008-E08 | CONFLICT-01 / CONFLICT-02 | Both remain OPEN; no conflict disposition is inferred here. | OPEN |
| P008-E09 | SOURCE-ORIGIN-01 | Remains unresolved; SO-GAP-04 is recorded as decisive for its resolution under O-1. | OPEN |

## 5. Classification rules for this review

Until the original taxonomy and owner decision are recovered:

1. A label appearing in a filename, heading, summary, or conversation is not sufficient to establish a canonical ADR classification.
2. Similar titles or subject matter do not prove that two ADRs are duplicates.
3. A later date does not prove supersession.
4. An ADR's existence does not prove approval.
5. An approval entry without its required source trace must retain its recorded evidence limitation.
6. A classification decision must identify the taxonomy version, the classified artifact/version, the decision authority, and the source event.
7. If two ADRs overlap, preserve both records and document the overlap until an explicit authority decision defines their relationship.

These are evidence-handling controls for this review, not a newly approved project-wide ADR taxonomy.

## 6. Findings and disposition

**Finding:** PDR-008 remains **CANNOT RESOLVE — PRIMARY ADR TEXTS AND CLASSIFICATION AUTHORITY NOT RECOVERED**.

The current record cannot establish:

- what ADR-C-002 says;
- what ADR-C-005 says;
- what classification labels they use;
- whether the labels belong to one common taxonomy;
- whether either ADR supersedes, amends, duplicates, or conflicts with the other;
- whether the owner approved either classification.

This does not establish that the ADRs are invalid or that no approval ever occurred. It establishes that the required evidence is not available in the reviewed source set.

## 7. Recovery actions

| Action ID | Required action | Responsible role | Completion evidence |
|---|---|---|---|
| P008-R01 | Recover the original ADR-C-002 file/export and identify its exact version and source. | Project Owner provides source; Governance Executor records and hashes it. | Original artifact, locator, SHA-256, completeness check. |
| P008-R02 | Recover the original ADR-C-005 file/export and identify its exact version and source. | Project Owner provides source; Governance Executor records and hashes it. | Original artifact, locator, SHA-256, completeness check. |
| P008-R03 | Recover the classification taxonomy and its approval event. | Project Owner identifies governing decision; Governance Executor traces it. | Taxonomy text/version and approving decision source. |
| P008-R04 | Build a side-by-side comparison of the two ADRs, including scope, status, dates, dependencies, and conflicting clauses. | Governance Executor; independent Reviewer. | Clause-level comparison tied to source hashes. |
| P008-R05 | Record the explicit relationship decision: independent, overlapping, amended, superseded, duplicate, or conflict. | Authorized decision authority. | Explicit decision record with rationale and scope. |
| P008-R06 | Reconcile relevant ADR-REV-001, ARCH-01, CONFLICT-01/02, and SOURCE-ORIGIN-01 dependencies. | Governance Executor; Project Owner for authority decisions. | Traceable evidence matrix and separate disposition records. |

## 8. PDR-008 closure criteria

PDR-008 may be reconsidered for closure only when:

- [ ] Both original ADR texts and exact versions are recovered.
- [ ] The canonical classification taxonomy and version are recovered.
- [ ] The source and owner approval for the taxonomy are established.
- [ ] A clause-level comparison of ADR-C-002 and ADR-C-005 is complete.
- [ ] Their relationship is explicitly decided by the appropriate authority.
- [ ] ADR-REV-001 and ARCH-01 dependencies are reconciled or explicitly deferred with impact.
- [ ] CONFLICT-01/02 and SOURCE-ORIGIN-01 impacts are explicitly dispositioned without implied resolution.
- [ ] The closure decision references exact document hashes and commit SHA.

Until these criteria are met, PDR-008 remains OPEN.

## 9. Explicit non-actions

This review does not:

- invent or recreate either ADR's text;
- infer a classification taxonomy;
- merge, delete, rename, or supersede either ADR;
- change the status of any ADR;
- resolve ADR-REV-001, ARCH-01, CONFLICT-01/02, or SOURCE-ORIGIN-01;
- authorize code, gameplay, or Code Gate activity;
- close PDR-008.

## 10. Status

| Item | Status |
|---|---|
| A4 baseline for PDR-008 | RECOVERED FROM PROJECT RECORD |
| Repository search | IMPLEMENTED — NO MATCH FOUND IN SEARCH RESULTS |
| ADR-C-002 original text | TBD / MISSING |
| ADR-C-005 original text | TBD / MISSING |
| Classification taxonomy | TBD / MISSING |
| Relationship / owner approval | TBD / MISSING |
| ADR-REV-001 | OPEN |
| ARCH-01 | PARTIAL |
| CONFLICT-01 / CONFLICT-02 | OPEN |
| SOURCE-ORIGIN-01 | OPEN |
| PDR-008 disposition | CANNOT RESOLVE — EVIDENCE INSUFFICIENT |
| PDR-008 closure | NOT PERFORMED |
