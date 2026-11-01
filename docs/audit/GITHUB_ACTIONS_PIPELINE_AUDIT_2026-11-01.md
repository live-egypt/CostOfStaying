# GitHub Actions Pipeline Audit and Remediation

**Audit date:** 2026-11-01  
**Repository:** `live-egypt/CostOfStaying`  
**PR:** #6 — `feature/core-lifecycle-resolution`  
**Scope:** Core CI and inherited static-analysis workflows. This is an operational workflow audit, not a source-code security certification.

## 1. Latest observed run before workflow remediation

PR head at the time of the run: `0734b194bb7434a20873601c4f3d6033ac5d5f49`.

| Workflow | Run ID | Result | Actual failure / evidence |
|---|---:|---|---|
| Core CI | 41327877562 | PASS | Core dependency boundary, composition-root check, Release build and test job passed |
| MobSF | 41327877519 | FAIL | Legacy action attempted to install `semgrep==0.50.1`, which pip could not resolve |
| Contrast Scan | 41327877553 | FAIL | Placeholder artifact `mypath/target/myartifact.jar`; required Contrast Organization ID secret absent |
| SecurityCodeScan | 41327877574 | FAIL | `dotnet restore` executed at repository root; no solution/project file exists there (MSB1003) |

The green Core CI result verifies only the standalone Core slice. Failed scanners are not clean scans and provide no evidence that the source is vulnerability-free.

## 2. Root-cause analysis

### MobSF

The inherited `MobSF/mobsfscan` action was pinned to a legacy action revision whose requirements attempted to install `semgrep==0.50.1`. That dependency could not be resolved by the runner's package index, so scanning never began and no SARIF report was produced.

**Branch remediation:** replace the legacy action with a pinned `mobsfscan==1.1.0` installation on Python 3.13, invoke the documented CLI, and restrict automatic triggers to mobile source changes. This avoids executing a mobile scanner for documentation/Core-only commits. The new workflow still requires a real mobile source tree and a successful run before mobile security can be marked verified.

### SecurityCodeScan

The inherited workflow ran `dotnet restore` at the repository root. There is no root-level solution or project, so MSBuild returned MSB1003 before the analyzer build.

**Branch remediation:** target `tests/CostOfStaying.Core.Tests/CostOfStaying.Core.Tests.csproj` explicitly for restore and Release build. The test project references the Core project, so the analyzer build includes the current Core C# source. SARIF upload is conditional on a generated report. A successful scanner run and review of findings are still required.

### Contrast Scan

The inherited workflow was an unconfigured template: it used a non-existent Java JAR path and required secrets that were not provisioned. The action failed because Organization ID was absent.

**Branch remediation:** make this a manual-only workflow requiring an explicit artifact path and validate all three required secrets before invoking the action. It is not an automatic required check. It can only be used after a real supported artifact has been built and Contrast credentials have been configured in repository secrets.

## 3. Core CI workflow hardening

The Core workflow includes:
- A source scan that rejects UnityEngine/FishNet references in Core.
- A composition-root scan that rejects direct production construction of Core aggregates/services outside `CoreComposition.cs`.
- .NET 8 restore, Release builds and NUnit.
- TRX artifact retention even when tests fail.
- Read-only repository permissions and a bounded job timeout.

The branch trigger was corrected to the active branch name, and the composition-root check was extended to include the bounded command-admission queue. The exact latest source increment still needs a new CI result.

## 4. Verification policy

- A failed scanner is not a clean scan.
- A scanner setup/configuration failure is not itself proof of a source vulnerability.
- A successful Core CI run does not verify Unity, FishNet, Android, authentication, host/dedicated-server behavior or production security.
- MobSF findings must only be interpreted against actual Android/iOS source.
- Contrast must not be reported as passed unless a real artifact is scanned with valid credentials.
- Static-analysis findings must be triaged; merely uploading SARIF is not a security sign-off.

## 5. Remediation state

| Item | Branch state | Verification state |
|---|---|---|
| Core dependency and composition boundaries | Implemented in Core CI | Previous run passed; latest source increment pending |
| SecurityCodeScan project path | Corrected to explicit test project | New workflow run pending |
| MobSF scanner dependency | Replaced with pinned 1.1.0 package and Python 3.13 | New workflow run pending; mobile source not yet present |
| Contrast placeholder | Converted to manual-only artifact/secret validation | Not run; requires real artifact and secrets |
| Unity/FishNet security verification | Not implemented | Not verified |

## 6. Source references

- MobSF scanner release 1.1.0 and CLI documentation: official MobSF/mobsfscan project and PyPI release.
- SecurityCodeScan workflow changes are on the feature branch and have not been merged to `main`.
- Contrast workflow is intentionally manual-only until an actual artifact and account credentials are available.

**Status:** root causes are identified and branch-level workflow corrections have been prepared. Do not mark any of the three scanner workflows green until their revised versions complete successfully.
