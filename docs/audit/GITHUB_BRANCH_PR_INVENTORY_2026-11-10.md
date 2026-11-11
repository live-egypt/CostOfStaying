# GitHub Branch and Pull Request Inventory — 2026-11-10

**Revision:** 2026-11-10  
**Repository:** live-egypt/CostOfStaying  
**Default branch:** main  
**Observed main SHA:** 3dd9cfc27aa48b09b3cde802e266a8221c39f0d3  
**Decision Status:** — (repository inventory; not merge authorization)  
**Evidence Maturity:** REVIEWED — GitHub repository metadata, paginated branch search, PR metadata and selected ref comparisons

> This is a dated snapshot of branches and PRs visible through the GitHub connector. Ahead/behind values are comparisons against the named base ref at the time retrieved; they are not cumulative project progress and may change as branches move.

## 1. Repository metadata

| Field | Value |
|---|---|
| Repository | live-egypt/CostOfStaying |
| Visibility | Public |
| Default branch | main |
| Archived | No |
| Current session permission snapshot | Pull / Push / Maintain / Admin available |
| Observed main commit | 3dd9cfc27aa48b09b3cde802e266a8221c39f0d3 |

## 2. Branch inventory

The paginated branch search returned 11 branch names across two pages. The current reconciliation branch was created afterward, giving 12 known branches in this snapshot.

| Branch | Comparison/base | Ahead | Behind | Changed files in compare | Main scope / interpretation |
|---|---|---:|---:|---:|---|
| main | Default branch | — | — | — | Baseline |
| audit/p0-ci-and-architecture-decisions | main | 435 | 0 | 94 | Older Core/CI/ADR feature line; overlaps the later consolidated Core branch |
| audit/phase1-uploaded-source-review | main | 3 | 7 | 3 | Diverged source-audit branch; not a clean fast-forward to main |
| docs/source-provenance-reconciliation-2026-11-09 | feature/core-lifecycle-resolution | 48 | 0 | 14 | PR #9 branch; source/TBD governance package based on Core branch |
| docs/stage1-source-provenance-recovery | main | 13 | 0 | 7 | PR #8 branch; source/provenance package based on main |
| docs/stage1-source-reconciliation | main | 444 | 0 | 95 | PR #10 historical branch; includes the Core bundle plus a superseded source-inventory checkpoint |
| feature/core-lifecycle-resolution | main | 441 | 0 | 94 | PR #6 branch; consolidated standalone Core, NUnit tests, ADRs and CI; not merged |
| feature/hotfix-cs0246 | main | 438 | 0 | 94 | Hotfix branch whose PR #7 target was the Core feature branch; branch is not itself main delivery |
| feature/phase1-core-foundation | main | 17 | 7 | 13 | Earlier divergent Core foundation |
| feature/phase1-technical-consolidation | main | 48 | 7 | 25 | Earlier divergent Phase 1 Core/technical consolidation |
| review/unity-source-package-2026-11-01 | main | 4 | 0 | 4 | PR #5 branch; includes a Unity Assets/source ZIP and its audit/manifest files |
| docs/project-reconciliation-baseline-2026-11-10 | main | Live PR #11 metadata | Live PR #11 metadata | Live PR #11 metadata | Current documentation consolidation branch; do not use this dated file for live head counts |

The comparison for docs/stage1-source-reconciliation is against main, and its 95-file diff inherits the large Core implementation/CI change set in addition to the superseded audit file. PR #10 is closed without merge and explicitly superseded. Its branch is preserved for history only.

## 3. Pull request inventory

| PR | Title | State | Base | Head branch | Changed files | Merge result / interpretation |
|---:|---|---|---|---|---:|---|
| 1 | docs: replace legacy documentation and define v2.1 gameplay baseline | CLOSED | main | docs/v2.0 | 49 | MERGED — supplied v2.1 documentation baseline to main |
| 2 | Audit uploaded Phase 1 source before implementation | CLOSED / Draft | main | audit/phase1-uploaded-source-review | 3 | NOT MERGED — superseded by later Phase 1 consolidation |
| 3 | Implement Phase 1 pure C# Core foundations | CLOSED / Draft | audit/phase1-uploaded-source-review | feature/phase1-core-foundation | 12 | NOT MERGED — superseded by later consolidation |
| 4 | Consolidate Phase 1 technical files and correct Core rules | CLOSED / Draft | main | feature/phase1-technical-consolidation | 25 | NOT MERGED — superseded by PR #6 |
| 5 | docs(audit): review supplied Unity source package | OPEN / Draft | main | review/unity-source-package-2026-11-01 | 4 | NOT MERGED — branch contains a ZIP despite the PR body/checksum manifest claiming it is not committed |
| 6 | Resolve player lifecycle and align Core vitality contract | OPEN / Draft | main | feature/core-lifecycle-resolution | 94 | NOT MERGED — standalone Core and tests are feature-branch only |
| 7 | fix(tests): use CoreComposition factory in roster snapshot test | CLOSED | feature/core-lifecycle-resolution | feature/hotfix-cs0246 | 2 | MERGED into the Core feature branch, not main |
| 8 | docs: establish Stage 1 source and provenance baseline | OPEN / Draft | main | docs/stage1-source-provenance-recovery | 7 | NOT MERGED — overlaps PR #9 and current PR #11 |
| 9 | docs: reconcile project sources and 85 TBD requirements | OPEN / Draft | feature/core-lifecycle-resolution | docs/source-provenance-reconciliation-2026-11-09 | 14 | NOT MERGED — inherits Core branch base and overlaps PR #8 |
| 10 | docs(audit): superseded Stage 1 source inventory reconciliation | CLOSED / Draft | feature/core-lifecycle-resolution | docs/stage1-source-reconciliation | 2 | NOT MERGED — SUPERSEDED; historical only |
| 11 | docs: consolidate project governance and evidence baseline | OPEN / Draft | main | docs/project-reconciliation-baseline-2026-11-10 | Live PR #11 metadata | Current review candidate; docs-only; not merged |

## 4. Main delivery boundaries

- PR #1 is the merged v2.1 documentation baseline.
- PR #7 contributes its test correction only to the Core feature branch through PR #7's target; it did not merge to main.
- PR #6 is not in main, even though it has a passing standalone Core CI history.
- PR #5's Unity ZIP is present on its review branch and is not in main.
- PR #8 and PR #9 remain overlapping unmerged source/governance packages. Do not merge both as-is.
- PR #10 is closed without merge and explicitly superseded.
- PR #11 is the current consolidation candidate based on main; it does not silently absorb or merge Core code, PR #5, PR #6, PR #8 or PR #9.

## 5. Branch comparison cautions

1. Ahead/behind counts belong to a particular base/head pair at a particular time. A branch can contain another branch's history and appear hundreds of commits ahead without containing hundreds of independent deliverables.
2. The 94-file comparisons for audit/p0-ci-and-architecture-decisions and feature/hotfix-cs0246 largely reflect the same shared Core change set.
3. PR #9 is 48 commits ahead of its feature/core-lifecycle-resolution base, not 48 commits ahead of main. Its main ancestry includes the 441-commit Core branch.
4. PR #10's 444-commit comparison to main does not make it a preferred source package; it is closed and explicitly superseded.
5. Branch search is not a repository file-tree inventory. The connector did not expose a complete recursive tree-read operation in this session.
6. This inventory does not close or delete branches. Branch cleanup requires a separate explicit decision and historical-retention review.

## 6. Status

| Area | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| PR metadata #1–#11 | — | REVIEWED | Snapshot recovered |
| Branch names | — | REVIEWED | 12 known names, including current reconciliation branch |
| Main file-tree completeness | — | PARTIAL | Full recursive tree not exposed |
| PR #5 Unity ZIP | — | REVIEWED | Present on review branch; checksum and PR body conflicts open |
| PR #6 Core implementation | — | TESTED | Feature branch only; not main delivery |
| PR #8/#9 documentation overlap | PROPOSED | REVIEWED | OPEN — integration candidate selection |
| PR #11 documentation consolidation | PROPOSED | REVIEWED | OPEN / Draft / not merged |
| Branch deletion/cleanup | PROPOSED | — | NOT AUTHORIZED / NOT PERFORMED |

