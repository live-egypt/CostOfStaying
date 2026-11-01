# GitHub Actions Pipeline Audit

**Audit date:** 2026-11-01  
**Commit under test:** `308c8541ad75befb4ade2d0de6ef1f4dcbd3d8cc`  
**Pull request:** [#4 — Technical Consolidation](https://github.com/live-egypt/CostOfStaying/pull/4)  
**Scope:** workflow execution evidence for the PR merge commit; this is an operational pipeline audit, not a source-code security certification.

## 1. Execution summary

| Workflow | Run ID | Result | Evidence |
|---|---:|---|---|
| Core CI | 41312765519 | PASS | Restore, Core Release build, test-project Release build, NUnit and artifact upload all succeeded |
| MobSF | 41312765557 | FAIL — scanner setup | MobSF action dependency installation failed before scanning |
| SecurityCodeScan | 41312765504 | FAIL — project discovery | `dotnet restore` ran at repository root where no solution/project file exists |
| Contrast Scan | 41312765456 | FAIL — configuration | Required organization ID was absent; configured artifact path is still a template placeholder |

Core CI ran against the PR head commit listed above. Its NUnit result was **38 passed, 0 failed, 0 skipped**. The TRX artifact `core-test-results` was uploaded successfully (artifact ID `13086610319`, SHA-256 `ad27e2309320fd9c2430bf09eca0b9728968c6f14a807f959da8f4ddcf9d9383`).

## 2. Failure analysis

### MobSF

The pinned `MobSF/mobsfscan` action failed while installing its Python requirements. Its dependency set requests `semgrep==0.50.1`, which pip could not resolve from the package index used by the runner. The scan step exited before producing a SARIF report; therefore this run gives no mobile-security findings.

**Required action:** select and pin a currently supported scanner release/dependency set, then verify it on the actual Android/mobile source tree. Do not suppress the dependency error or mark the scan as passed.

### SecurityCodeScan

The workflow invokes `dotnet restore` and `dotnet build` from the repository root. The repository has no root-level solution or project file, so restore fails with MSB1003 before compilation or analysis.

**Required action:** point restore and build explicitly to `tests/CostOfStaying.Core.Tests/CostOfStaying.Core.Tests.csproj` (or introduce a reviewed solution file if one becomes the canonical entry point). Re-run the scanner and inspect its SARIF output. This is a workflow path defect, not evidence of a code vulnerability.

### Contrast Scan

The action reports: `Organization ID is required but not present`. The workflow also still specifies `mypath/target/myartifact.jar`, a template path rather than a real build artifact. The project currently has no Java artifact build stage, and the required Contrast account secrets are not available to this run.

**Required action:** do not enable this scan as a required check until a supported real artifact, valid account configuration and secret provisioning are in place. If the project remains .NET-only, configure the action for the actual .NET artifact after the build pipeline exists, or remove/disable this unused template workflow through a separately reviewed change.

## 3. Security interpretation

- A failed scanner is **not** a clean scan.
- These failures are configuration/toolchain failures; they do not establish that the source is vulnerable or vulnerability-free.
- No SARIF security result was uploaded by the failed scanner steps.
- The green Core CI result verifies only the standalone Core build and NUnit tests; it is not a security audit and does not verify Unity, FishNet, Android, authentication or production deployment.
- Existing third-party scanning workflows are inherited from the default branch and are separate from the newly added Core CI workflow.

## 4. Recommended remediation order

1. Fix SecurityCodeScan project/solution targeting and rerun it.
2. Replace or upgrade the obsolete MobSF scanner dependency chain with a supported, pinned release; validate on actual mobile source.
3. Decide whether Contrast is applicable. If yes, build the real artifact and configure secrets outside source control; if not, remove the placeholder workflow in a reviewed change.
4. Reassess required status checks only after each scanner produces a valid completed result.
5. Keep the lifecycle conflict in ADR-007 as a separate gameplay decision gate.

**Status:** Core CI verified for the exact commit and scope above. The three additional scanner workflows remain unverified due to the failures documented here.
