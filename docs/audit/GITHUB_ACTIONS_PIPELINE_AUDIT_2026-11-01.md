# GitHub Actions Pipeline Audit and Remediation

**Audit date:** 2026-11-01  
**Repository:** `live-egypt/CostOfStaying`  
**Review branch:** `feature/core-lifecycle-resolution`  
**PR:** #6 — Draft, not merged  
**Scope:** Core CI, static analysis and optional third-party scanners. This is an operational workflow audit, not a source-code security certification.

## 1. Observed execution evidence

### Core CI baseline

- Run `41329400470` — SUCCESS.
- Source commit: `a0cc96c02ce163691456bc2a02dbbc9c57cd1d12`.
- Core dependency boundary and composition-root guards: PASS.
- .NET 10 Release restore/build: PASS.
- NUnit: **119 passed, 0 failed, 0 skipped**.
- TRX artifact: `13092811803`.

A later Core CI push run `41329723500` also completed successfully on merge-synced branch commit `e6478390b52f70c7fa29a6736a10078cd76a6e21`; its detailed NUnit result is retained in that run. Core CI verifies only standalone Core.

### Scanner runs and root causes

| Workflow | Run | Result | Root cause / interpretation |
|---|---:|---|---|
| MobSF legacy action | 41327877519 | FAIL | Legacy action dependency requested unavailable `semgrep==0.50.1`; scan never began |
| MobSF replacement | 41329727360 | PASS | Pinned `mobsfscan==1.1.0` installed and ran; repository still has no Android/iOS application source, so this is scanner-execution evidence, not mobile-app security certification |
| SecurityCodeScan inherited | 41327877574 | FAIL | Root-level `dotnet restore` failed with MSB1003 because no root solution/project exists |
| SecurityCodeScan replacement attempt | 41329697483 | FAIL | Explicit Core test project restore and analyzer build succeeded, but the legacy SARIF converter required .NET Core 3.1, which is absent/EOL on the runner; no final SARIF upload |
| CodeQL inherited | 41329615128 | FAIL | Inherited workflow had an empty language matrix and created no analysis jobs |
| CodeQL corrected | 41329697491 | PASS | Explicit C# and GitHub Actions analysis jobs both completed successfully and uploaded results |
| Contrast inherited | 41327877553 | FAIL | Template artifact path was `mypath/target/myartifact.jar`; required organization secret was absent |
| Debricked inherited | 41329618006 | FAIL | Scan initialization returned Unauthorized because no valid Debricked access token was supplied |
| NowSecure inherited | 41329616589 | FAIL | Template workflow targeted a Gradle build and APK that do not exist in this repository; it also contained an unresolved group-ID placeholder |
| Frogbot inherited | Not triggered in this branch run | NOT VERIFIED | Template workflow grants write permissions and runs on main pushes; credentials and account configuration are not verified |

A failed scanner is not a clean scan. A workflow setup failure is not itself proof that the source is vulnerable or vulnerability-free.

## 2. Branch-level workflow corrections

The review branch now contains these corrections:

### Core CI
- Enforces the Core dependency boundary (no UnityEngine/FishNet references).
- Enforces CoreComposition as the construction root; the bounded queue may own its private sequence tracker.
- Uses the active feature branch trigger, .NET 10, Release builds, NUnit and TRX artifact retention.
- Latest source CI passed 119 tests; the merge-synced source-only follow-up also passed.

### CodeQL
- Explicitly analyzes `csharp` and `actions` rather than an empty matrix.
- Uses CodeQL Action v4.
- Corrected run `41329697491` passed both language jobs.

### SecurityCodeScan
- Targets `tests/CostOfStaying.Core.Tests/CostOfStaying.Core.Tests.csproj` explicitly; this restores/builds the referenced Core project too.
- The analyzer build passed in run `41329697483`.
- The old SARIF conversion action was removed because it depends on the retired .NET Core 3.1 runtime.
- The replacement combines SARIF 2.1.0 reports using Python's standard JSON library and uploads through CodeQL Action v4.
- **The new SARIF-combination/upload revision still requires a successful CI run.** Do not mark SecurityCodeScan verified until it passes and findings are reviewed.

### MobSF
- Replaced the obsolete action dependency chain with pinned `mobsfscan==1.1.0` on Python 3.13.
- Automatic triggers are limited to actual Android/iOS source changes; editing the workflow alone does not trigger a mobile scan.
- Run `41329727360` proves the scanner installed and executed. There is currently no mobile application source in the repository, so it does not certify an Android/iOS app.

### Optional account-backed scanners
- Contrast is manual-only and validates a real artifact path plus the required API key, organization ID and auth header.
- NowSecure is manual-only and requires an existing APK/IPA, group ID and `NS_TOKEN`.
- Debricked is manual-only and validates `DEBRICKED_TOKEN`.
- Frogbot is manual-only, validates JFrog credentials and retains write permissions only for an explicitly requested dependency-fix run.

These changes prevent unconfigured external account integrations from creating automatic failing checks or making unauthorized automated changes. Manual execution still requires valid account credentials and supported artifacts.

## 3. Verification and security policy

- A successful scanner job means the scanner ran and uploaded its report; it does not mean zero vulnerabilities.
- SARIF findings must be triaged and dispositioned; upload is not security sign-off.
- MobSF must be evaluated against real mobile source or a built APK/IPA before mobile security is certified.
- Contrast and NowSecure require real artifacts and provisioned account credentials.
- Debricked and Frogbot require valid service tokens.
- CodeQL and SecurityCodeScan results must be reviewed for actual alerts, not just workflow conclusion.
- Core CI does not verify Unity, FishNet, Android, transport authentication, Host/Dedicated Server, persistence or production deployment.

## 4. Remediation state

| Item | Current branch state | Verification |
|---|---|---|
| Core purity/composition + 119 NUnit | Implemented | PASS — run 41329400470 |
| Merge branch with current main | Synchronized; no behind commits | Compare status: ahead only |
| CodeQL C# + Actions | Corrected | PASS — run 41329697491 |
| MobSF scanner runtime | Corrected and source-path scoped | PASS — run 41329727360; no app source to certify |
| SecurityCodeScan project targeting | Corrected | Analyzer build PASS; SARIF converter replacement pending verification |
| Contrast placeholder | Manual-only and validated | Not run; credentials/artifact required |
| NowSecure template | Manual-only and validated | Not run; credentials/artifact required |
| Debricked template | Manual-only | Not run; token required |
| Frogbot automation | Manual-only | Not run; JFrog account required |
| Unity/FishNet security verification | Not implemented | Not verified |

## 5. Delivery boundary

All workflow corrections are on the Draft PR branch. They have not been merged to `main`. PR #6 remains unmerged. Do not claim the default branch is repaired until the PR is reviewed and merged.

**Status:** Core CI is green for the verified source increment. CodeQL and MobSF replacement runs have passed within their stated scope. SecurityCodeScan's analyzer build passes, but its revised SARIF upload still needs verification. Account-backed scanners remain optional/manual until valid secrets and artifacts exist.
