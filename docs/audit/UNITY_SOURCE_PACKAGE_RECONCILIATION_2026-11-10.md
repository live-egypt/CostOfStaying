# Unity Source Package Reconciliation — 2026-11-10

**Revision:** 2026-11-10  
**Decision Status:** — (source/package reconciliation; no code or architecture decision made)  
**Evidence Maturity:** REVIEWED — GitHub branch, PR metadata, binary blob identity, ZIP central directory, README and checksum manifest inspected  
**Repository:** live-egypt/CostOfStaying  
**Source branch:** review/unity-source-package-2026-11-01  
**Source PR:** [PR #5](https://github.com/live-egypt/CostOfStaying/pull/5) — OPEN / DRAFT / NOT MERGED

> This review establishes that an archive is committed on a review branch and describes its archive-level contents. It does not establish that Unity imports or compiles the package, that FishNet APIs match an installed version, or that a playable game is implemented.

## 1. Repository object identity

| Field | Retrieved value |
|---|---|
| Repository path | incoming/unity-source-package/CostOfStaying_Unity_Source_Package.zip |
| Branch | review/unity-source-package-2026-11-01 |
| Git blob SHA | 319a584046d3ece7ce74242233015738758ec210 |
| Decoded archive size | 56,217 bytes |
| ZIP entries | 94 |
| ZIP directory entries | 7 |
| C# files | 81 |
| Assembly Definition files | 4 |
| Markdown files | 2 |
| Top-level roots | Assets/, README.md, docs/ |

The ZIP was retrieved through the GitHub file connector in Base64 form. The line breaks were removed before decoding. The computed Git blob SHA-1 for the exact decoded bytes matched GitHub's recorded blob SHA 319a584046d3ece7ce74242233015738758ec210. This confirms the decoded bytes correspond to the repository blob.

## 2. Archive structure

The archive contains source/test areas under:

- Assets/CostOfStaying/Core/
- Assets/CostOfStaying/Net/
- Assets/CostOfStaying/Tests/EditMode/
- Assets/CostOfStaying/Tests/Net/

The entry inventory contains 81 .cs files, 4 .asmdef files, 2 .md files, and 7 directory entries. The file list includes source names such as CorePipeline.cs, MovementService.cs, BetrayalService.cs, SessionLifecycleService.cs, FishNetServerAdapter.cs, FishNetClientAdapter.cs, CoreServerCommandIngress.cs, and ConnectionSessionMap.cs, as well as EditMode and Net test fixtures.

This is positive evidence that a Unity-oriented source package is present in this review branch. It corrects the earlier report that the corrected ZIP was not present in the branch.

## 3. Checksum conflict

The branch file docs/audit/UNITY_SOURCE_PACKAGE_SHA256.txt declares:

- SHA-256: 351190c6789b0a146be0978606c831f48b951509e7f353bb3b469374eb9b15d9
- Archive size: 55 KB
- Note: corrected archive is attached in the ChatGPT conversation and not committed to this repository branch.

For the exact 56,217 decoded bytes retrieved from the Git blob, SHA-256 was independently computed as:

5f92947c8b23d1ee15d7c744fb62ef0cf7af361bfdda277fcdaeff61dab51ac4

The computed SHA-256 does not match the checksum file. The Git blob identity did match, so the mismatch is not explained by a different file being fetched. Preserve both values; do not silently overwrite the manifest. The source manifest needs a correction/lineage decision identifying which archive version the declared hash was intended to authenticate.

## 4. PR description versus committed file list

PR #5's description says the corrected ZIP is attached to the ChatGPT conversation and is not included in the branch. However:

- list_pr_changed_filenames for PR #5 includes incoming/unity-source-package/CostOfStaying_Unity_Source_Package.zip.
- GitHub fetch_file returns the ZIP at that exact path on the PR #5 head branch.
- The Git blob SHA and decoded archive structure were independently checked.

Therefore the PR body and branch file/manifest disagree. This review records the branch file as present; it does not infer who uploaded it or when the discrepancy arose. PR #5 remains open Draft and unmerged.

## 5. Package completeness boundary

The archive has a Unity Assets tree and assembly definitions, but the inspected ZIP inventory does not contain:

- Packages/manifest.json
- Packages/packages-lock.json
- ProjectSettings/ProjectVersion.txt
- ProjectSettings/EditorBuildSettings.asset
- Unity scene files (.unity)
- Unity asset metadata files (.meta)

Accordingly, the ZIP is an Assets/source package, not a complete self-contained Unity project. A clean Unity project must still supply the editor version, resolved package set, project settings, scenes/build configuration, and any external dependencies. This absence is based on the full 94-entry ZIP central-directory inventory, not on a name-only assumption.

## 6. Static review claims retained from PR #5 report

The companion docs/audit/UNITY_SOURCE_PACKAGE_REVIEW.md states:

- 81 C# files inventoried.
- Static Core scan found no UnityEngine or FishNet references in the Core area.
- Several source corrections were prepared in the reviewed package.
- Unity compilation: NOT RUN.
- NUnit EditMode/PlayMode tests: NOT RUN.
- FishNet 4.7.4R compatibility: NOT VERIFIED against an installed package.
- Dedicated Server/Host integration: NOT RUN.
- Gameplay conformance: NOT VERIFIED.

These are report claims. The current archive presence does not independently validate all static review conclusions or source corrections. The exact package needs extraction, full source review, import and compilation against a real, pinned Unity/FishNet environment.

## 7. Required verification before implementation claims

1. Reconcile and correct the archive SHA-256 manifest with a source/version record.
2. Extract the archive in a controlled working copy and inventory all source and assembly definitions.
3. Create or identify the exact Unity editor project and pin its package manifest/lock and ProjectVersion.
4. Verify FishNet package version from the resolved package and compile against its actual APIs.
5. Run Unity EditMode and PlayMode tests and retain XML/log artifacts.
6. Run Host and Linux Dedicated Server integration tests, including authentication, disconnect, snapshots, tick execution and packet impairment.
7. Reconcile the archive's movement/direct-damage Betrayal behavior with the current proposed Suffering Transfer Move/atomicity specification before treating gameplay conformance as met.

## 8. Status

| Claim | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| ZIP exists on PR #5 review branch | — | VERIFIED — branch path and blob identity fetched | NOT ON main |
| ZIP contains 81 C# files and 4 asmdefs | — | REVIEWED — ZIP central directory | Static archive inventory only |
| Declared SHA-256 is correct for current ZIP | — | REVIEWED — MISMATCH | OPEN |
| PR #5 body accurately describes current branch file list | — | REVIEWED — CONTRADICTION | OPEN |
| ZIP is a complete Unity project | — | REVIEWED — NOT COMPLETE | Asset/source package only |
| Unity compilation / NUnit / FishNet compatibility | — | NOT ESTABLISHED | NOT RUN / NOT VERIFIED |
| Playable integration / Dedicated Server | — | NOT ESTABLISHED | NOT RUN |
| Production readiness | — | NOT ESTABLISHED | NOT CLAIMED |

No ZIP was modified, no source files were extracted into the repository, and no PR #5 change was made by this reconciliation.
