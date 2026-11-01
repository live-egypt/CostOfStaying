# GitHub Actions Pipeline Audit and Remediation

**Audit date:** 2026-11-01  
**Repository:** `live-egypt/CostOfStaying`  
**Review branch:** `feature/core-lifecycle-resolution`  
**PR:** #6 — Draft, not merged  
**Scope:** Core CI, static analysis and optional third-party scanners. This is an operational workflow audit, not a source-code security certification.

## 1. Observed execution evidence

### Core CI baseline

- Run `41331236864` — SUCCESS.
- Source commit: `be67d8a3399f99c2c0e05a37eec8ffa0cb2abb13`.
- Core dependency boundary and composition-root guards: PASS.
- .NET 10 SDK restore/build: PASS.
- Core target: .NET Standard 2.1; test target: .NET 10.
- SessionLifecycle and CoreComposition factory: included in this run.
- NUnit: **148 passed, 0 failed, 0 skipped**.
- TRX artifact: `13093536721`.
- Artifact SHA-256: `2b13f11e173414f7b232a89ef1ec0389d1d237686cc9137f5efa9662a8d1ee67`.

A later Core CI push run `41329723500` also completed successfully on merge-synced branch commit `e6478390b52f70c7fa29a6736a10078cd76a6e21`; its detailed NUnit result is retained in that run. Core CI verifies only standalone Core.

### Scanner runs and root causes

| Workflow | Run | Result | Root cause / interpretation |
|---|---:|---|---|
| MobSF legacy action | 41327877519 | FAIL | Legacy action dependency requested unavailable `semgrep==0.50.1`; scan never began |
| MobSF replacement | 41329727360 | PASS | Pinned `mobsfscan==1.1.0` installed and ran; repository still has no Android/iOS application source, so this is scanner-execution evidence, not mobile-app security certification |
| SecurityCodeScan inherited | 41327877574 | FAIL | Root-level `dotnet restore` failed with MSB1003 because no root solution/project exists |
| SecurityCodeScan replacement attempts | 41329697483 / 41329900099 / 41330344606 | FAIL | Project restore/build succeeded; legacy .NET 3.1 converter, strict SARIF 2.1 check and converter schema assumptions failed in sequence |
| SecurityCodeScan final replacement | 41331240071 | PASS | Analyzer build passed; two native SARIF 1.0.0 reports preserved as artifact 13093451136; both reported 0 findings |
| CodeQL inherited | 41329615128 | FAIL | Inherited workflow had an empty language matrix and created no analysis jobs |
| CodeQL corrected | 41329697491 and 41331240115 | PASS | Explicit C# and GitHub Actions analysis jobs both completed successfully and uploaded results |
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
- Latest Core CI run `41331236864` passed 148 tests on the synchronized branch.

### CodeQL
- Explicitly analyzes `csharp` and `actions` rather than an empty matrix.
- Uses CodeQL Action v4.
- Corrected run `41329697491` passed both language jobs.

### SecurityCodeScan
- Targets `tests/CostOfStaying.Core.Tests/CostOfStaying.Core.Tests.csproj` explicitly; this restores/builds the referenced Core project too.
- The analyzer build passed in run `41331240071`.
- The old SARIF conversion action depended on the retired .NET Core 3.1 runtime. A custom conversion attempt also failed because the analyzer emits native SARIF 1.0.0 and the legacy converter expected fields that were absent in empty reports.
- The final replacement preserves the analyzer's native SARIF 1.0.0 reports as a dedicated workflow artifact instead of uploading an invalid schema to GitHub Code Scanning.
- Run `41331240071` completed successfully on the session-lifecycle source commit: Core and test project analyzer builds passed; two native SARIF reports were retained; each reported 0 findings.
- Artifact: `securitycodescan-sarif`, ID `13093451136`, SHA-256 `b88f551b08d2947043cf1c2375c462b71e5d0b7f9a522d88c61c0088d4be893c`.
- This is a successful analyzer run with zero reported findings in those reports, not a GitHub Code Scanning SARIF upload. The reports remain downloadable for manual review.

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

- A successful scanner job means the scanner ran and produced/preserved its report; it does not mean zero vulnerabilities.
- SARIF findings must be triaged and dispositioned; upload is not security sign-off.
- MobSF must be evaluated against real mobile source or a built APK/IPA before mobile security is certified.
- Contrast and NowSecure require real artifacts and provisioned account credentials.
- Debricked and Frogbot require valid service tokens.
- CodeQL alerts must be triaged. SecurityCodeScan's raw SARIF 1.0.0 artifact must be retained and reviewed; it is not uploaded to GitHub Code Scanning.
- Core CI does not verify Unity, FishNet, Android, credential validation, transport authentication, global session mapping/reconnect, Host/Dedicated Server, persistence or production deployment.

## 4. Remediation state

| Item | Current branch state | Verification |
|---|---|---|
| Core purity/composition/session lifecycle + 148 NUnit | Implemented | PASS — run 41331236864 |
| Merge branch with current main | Synchronized; no behind commits | Compare status: ahead only |
| CodeQL C# + Actions | Corrected | PASS — run 41330669137 |
| MobSF scanner runtime | Corrected and source-path scoped | PASS — run 41329727360; no app source to certify |
| SecurityCodeScan project targeting | Corrected | PASS — run 41331240071; 2 native SARIF reports retained, 0 findings reported |
| Contrast placeholder | Manual-only and validated | Not run; credentials/artifact required |
| NowSecure template | Manual-only and validated | Not run; credentials/artifact required |
| Debricked template | Manual-only | Not run; token required |
| Frogbot automation | Manual-only | Not run; JFrog account required |
| Unity/FishNet security verification | Not implemented | Not verified |

## 5. Delivery boundary

All workflow corrections are on the Draft PR branch. They have not been merged to `main`. PR #6 remains unmerged. Do not claim the default branch is repaired until the PR is reviewed and merged.

**Status:** Core CI is green for the verified source increment. CodeQL and MobSF replacement runs have passed within their stated scope. SecurityCodeScan's analyzer build and native report retention pass (run 41331240071); two raw SARIF 1.0.0 reports with zero reported findings are retained as an artifact, not uploaded to GitHub Code Scanning. Account-backed scanners remain optional/manual until valid secrets and artifacts exist.


## 6. Latest verified PR #6 scanner baseline

The latest source-and-documentation head used for the current verified baseline is `c33faf84aa39856d46181ccc1c9decbfba2777d2`.

| Workflow | Run | Result | Evidence |
|---|---:|---|---|
| Core CI | 41332989910 | PASS | .NET 10 Release build; **181 NUnit passed, 0 failed, 0 skipped**; TRX artifact 13093814339 |
| CodeQL Advanced | 41332990015 | PASS | Both C# and GitHub Actions analysis jobs completed successfully; alerts still require triage |
| SecurityCodeScan | 41332989918 | PASS | Analyzer build and SARIF retention completed; artifact 13093029825 contains two native SARIF 1.0.0 reports, each reporting zero findings |

SecurityCodeScan's successful job does not imply the SARIF reports were uploaded to GitHub Code Scanning; they are retained as a workflow artifact. A zero-finding analyzer result is scoped to the rules and source analyzed, not a general security certification. CodeQL completion likewise means analysis completed; review any generated alerts in the repository's Security tab.

The current Core CI workflow no longer runs on both feature-branch push and pull-request events. It runs on pushes to `main`, pull requests targeting `main`, manual dispatch and its scheduled default-branch scan, avoiding duplicate branch-push/PR checks.
