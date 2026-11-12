# PDR-013 — DIFF-XXX Review

**Workstream:** A5 — Document Control  
**Status:** PARTIAL — PR / PACKAGE DIFFERENCES IDENTIFIED; ORIGINAL DIFF-XXX SCOPE NOT RECOVERED  
**Document class:** Repository comparison / historical difference review  
**Repository:** `live-egypt/CostOfStaying`  
**Base examined:** `main` and historical PR base/head refs  
**Related records:** DOC-PACKAGE-01; ADR-REV-001; CONFLICT-01/02; SOURCE-ORIGIN-01; PDR-016

## 1. Purpose and decision question

A4 records PDR-013 as `DIFF-XXX`, related to DOC-PACKAGE-01, ADR-REV-001, CONFLICT-01/02, and SOURCE-ORIGIN-01.

The A4 source baseline states that comparisons among PR #8, PR #9, and PR #11 revealed path/content differences; PR #11 was a proposed documentation baseline candidate, while PR #8/#9 were retained as historical inputs. It requires recovery of the original DIFF-XXX definition and scope, reconciliation with PDR-016, and a check whether DIFF-XXX includes differences beyond the nine shared paths assigned to PDR-016.

The original DIFF-XXX source definition, identifier expansion, and complete acceptance criteria were not located in the repository files or source excerpts examined for this review.

## 2. Direct PR comparison evidence

The following comparisons were run against the stated base/head refs. Counts below are from the live GitHub compare results, not from PR description text.

| PR | Base | Head | Commits ahead | Changed files | Additions | Deletions |
|---|---|---|---:|---:|---:|---:|
| #8 | `main` | `docs/stage1-source-provenance-recovery` | 13 | 7 | 553 | 0 |
| #9 | `feature/core-lifecycle-resolution` | `docs/source-provenance-reconciliation-2026-11-09` | 48 | 14 | 1,547 | 2 |
| #11 | `main` | `docs/project-reconciliation-baseline-2026-11-10` | 144 | 39 | 4,657 | 12 |

### 2.1 PR #8 — source package under a nested path

Changed files:

- `docs/README.md`
- `docs/governance/sources/CONFLICT_REGISTER.md`
- `docs/governance/sources/DECISION_TRACEABILITY_MATRIX.md`
- `docs/governance/sources/DOCUMENT_CONTROL.md`
- `docs/governance/sources/PROVENANCE_REGISTER.md`
- `docs/governance/sources/SOURCE_INVENTORY.md`
- `docs/governance/sources/STAGE1_SOURCE_PROVENANCE_REVIEW.md`

The source/provenance package is under `docs/governance/sources/`. PR #8 is a documentation-only branch based on `main`.

### 2.2 PR #9 — expanded source and 85-TBD package under governance root

Changed files include:

- `docs/governance/PROVENANCE_REGISTER.md`
- `docs/governance/SOURCE_INVENTORY.md`
- `docs/governance/TBD_REGISTER.md`
- `docs/governance/SOURCE_TO_REQUIREMENT_MATRIX.md`
- `docs/governance/CONSISTENCY_MATRIX.md`
- `docs/governance/DOCUMENT_CONTROL_REGISTER.md`
- Additional decision, evidence, and cross-reference registers.

PR #9 is based on `feature/core-lifecycle-resolution`, not `main`. Its governance package uses root-level `docs/governance/` paths. The PR body records the 85-item TBD register and mapping work, but also preserves source-validation gaps and the open package overlap.

### 2.3 PR #11 — consolidation candidate

PR #11 is based on `main` and changes 39 documentation paths. It places the source/provenance/TBD/consistency/document-control records under a consolidated `docs/governance/` package and adds audit/reconciliation materials.

The PR #11 branch includes:

- `docs/audit/PACKAGE_STRUCTURE_RECONCILIATION.md` — blob SHA `1bdf14d8192c6b4f577391d30acbf3e35b8793be`.
- `docs/governance/DOCUMENT_CONTROL_REGISTER.md` — blob SHA `cee0b934bdee749a6cae3227912b905096ff2034`.
- `docs/audit/RECONCILIATION_PROGRESS.md` — blob SHA `375f407a1923679df7add5c81ca4cc3410bbb911`.

The Document Control Register explicitly describes the PR #8/#9 overlap, proposes consolidated paths, and says PR #8/#9 must not be merged as-is while the consolidation candidate is under review. It also says that PR #8/#9 should not be closed automatically. This confirms that the PR #11 paths remain a candidate, not an approved canonical package.

## 3. Confirmed difference classes

| Difference class | Observed evidence | Current conclusion |
|---|---|---|
| Path divergence | PR #8 uses `docs/governance/sources/`; PR #9 uses `docs/governance/`; PR #11 consolidates at `docs/governance/` | CONFIRMED |
| Base-context divergence | PR #8 and PR #11 target from `main`; PR #9 is based on `feature/core-lifecycle-resolution` | CONFIRMED |
| Scope divergence | PR #8 adds 7 files; PR #9 adds/updates 14; PR #11 adds/updates 39 | CONFIRMED |
| Content overlap | Source inventory, provenance, decision traceability, document control, conflict register, README indexing overlap across the packages | CONFIRMED BY DOCUMENTED PR SCOPE |
| Additional source/evidence records | PR #9 carries 85-TBD and 109-record consistency/source mapping materials; PR #11 consolidates and expands audit/evidence records | CONFIRMED AS BRANCH CONTENT |
| Canonical authority | No owner-approved final package decision is established by the PR metadata or branch presence | NOT ESTABLISHED |
| Semantic equivalence of all overlapping files | Requires row/path-level comparison, including source, SHA, base context, unique content, and omissions | NOT COMPLETED |

A tree-level path inventory is not a semantic comparison. A matching filename is not proof of matching content. PR file counts and commit counts must not be conflated with the nine shared paths assigned to PDR-016.

## 4. Relationship to PDR-016

PDR-016 remains the designated work item for the semantic comparison of the nine shared paths. Its required comparison includes source/base context, SHA, additions, deletions, modifications, unique records, and classification of differences.

PDR-013 does not replace PDR-016 and must not assume that PDR-016 covers every DIFF-XXX issue.

The remaining PDR-013 test is:

1. Recover the original DIFF-XXX definition and exact source reference.
2. Extract its full list of in-scope differences.
3. Compare that list against the nine PDR-016 paths.
4. Classify each DIFF-XXX item as: INCLUDED IN PDR-016, OUTSIDE PDR-016, DUPLICATE REFERENCE, or UNRESOLVED.
5. Add any out-of-scope differences as separate rows; do not silently expand PDR-016 or collapse distinct findings.

## 5. Evidence gaps

- Original DIFF-XXX definition / source entry: NOT LOCATED.
- Full original scope and acceptance criteria: NOT LOCATED.
- Complete row-level mapping of DIFF-XXX findings to PDR-016's nine paths: NOT COMPLETED.
- Full semantic comparison of all nine paths: delegated to PDR-016 and not completed here.
- Owner-approved canonical package/path decision: NOT ESTABLISHED; DOC-PACKAGE-01 remains OPEN.
- Provenance and source-origin gaps: remain open under PDR-005/PDR-010/PDR-014 and related conflict records.

## 6. Determination

**PDR-013 = PARTIAL — BRANCH / PACKAGE DIFFERENCES IDENTIFIED; ORIGINAL DIFF-XXX SCOPE AND FULL SEMANTIC RECONCILIATION NOT ESTABLISHED.**

This review confirms substantial and measurable differences among PR #8/#9/#11 in base context, changed paths, and scope. It does not resolve the missing original DIFF-XXX definition, does not complete PDR-016's nine-path semantic comparison, and does not select an approved canonical package.

## 7. Recovery and closure criteria

PDR-013 may be reconsidered when:

- The original DIFF-XXX source definition and exact scope are recovered, or a formally accepted reconstruction is recorded with provenance.
- Every DIFF-XXX difference is mapped to a source file/path, branch/base, and immutable blob/commit reference.
- The nine PDR-016 paths are reconciled without assuming that they exhaust DIFF-XXX.
- Out-of-scope differences are separately listed and dispositioned.
- DOC-PACKAGE-01 path/base conflicts are resolved or explicitly deferred with impact.
- No unique source record is dropped, merged, renamed, or superseded without evidence and decision.
- The final comparison matrix is committed and independently reviewable.

## 8. Non-effects

- Does not close PDR-013 or PDR-016.
- Does not resolve DOC-PACKAGE-01, ADR-REV-001, CONFLICT-01/02, or SOURCE-ORIGIN-01.
- Does not approve PR #11 as the canonical package.
- Does not authorize merging, closing, deleting, renaming, or migrating PR #8/#9/#11 content.
- Does not modify `main`, code, gameplay, Unity, FishNet, tests, CI, or runtime behavior.

## 9. Status summary

| Item | Status |
|---|---|
| PR #8/#9/#11 compare metadata | VERIFIED — LIVE COMPARE RESULTS |
| Path/base/scope divergence | VERIFIED — STRUCTURAL |
| Original DIFF-XXX definition | NOT LOCATED |
| Nine-path semantic comparison | DEFERRED TO PDR-016 |
| DOC-PACKAGE-01 | OPEN |
| PDR-013 | PARTIAL — NOT CLOSED |
| A5 PDR-013 closure | NOT ACHIEVED |
