# CI Workflow and Security Automation Review — 2026-11-10

**Revision:** 2026-11-10  
**Decision Status:** PROPOSED — review findings; no workflow changed  
**Evidence Maturity:** REVIEWED — workflow YAML from feature/core-lifecycle-resolution and related CI run metadata  
**Reviewed ref:** feature/core-lifecycle-resolution at 2820285021b1a3d7ae9acd953b3bb65677c33512.

## 1. Workflows inspected

| Workflow | Trigger scope | Main job | Permissions visible | Evidence boundary |
|---|---|---|---|---|
| core-ci.yml | Push to main, feature/hotfix-cs0246, feature/core-lifecycle-resolution when Core/tests/workflows paths change; PR targeting main or feature/core-lifecycle-resolution with same paths; manual; weekly schedule | Core dependency scan, composition-root enforcement, .NET 10 restore/build/NUnit, TRX artifact | contents: read | Standalone Core only; documentation-only changes do not trigger it under current path filters. |
| codeql.yml | Push/PR targeting main on C# / tests / workflow paths; weekly schedule; manual | CodeQL C# autobuild and GitHub Actions analysis | contents: read; job also actions/packages read and security-events write | Completion is not proof of zero findings. |
| securitycodescan.yml | Push/PR targeting main on C# / tests / selected workflow paths; manual | Windows .NET build with SecurityCodeScan analyzer; preserves SARIF and uploads artifact | contents: read | Workflow retains SARIF as artifact; inspected YAML does not itself upload SARIF to GitHub Code Scanning. |
| contrast-scan.yml | Manual dispatch with explicit artifact_path | Optional external Contrast scan of supplied artifact | contents: read; security-events: write | Not a required CI gate; requires three repository secrets and an artifact file. |
| dependabot.yml | Weekly NuGet and GitHub Actions updates | Dependency update PRs | Managed by Dependabot | NuGet entry points at repository root while project files are nested; actual discovery/update coverage needs evidence. |

## 2. Positive controls present

- Core CI has explicit read-only repository permissions.
- Core dependency-boundary check rejects UnityEngine/FishNet references from Core.
- Composition enforcement checks designated construction, queue, cleanup and snapshot entry points.
- Core/test builds use Release configuration; Core treats warnings as errors.
- Test artifact upload runs with always() semantics.
- CodeQL has explicit security-events write permission scoped to the analysis job.
- SecurityCodeScan action is pinned to a full commit SHA.
- Contrast scan requires a manually supplied artifact and secrets.
- Checked-in workflow source does not hardcode credentials.

## 3. Boundaries and open control questions

| ID | Observation | Classification | Required follow-up |
|---|---|---|---|
| CI-REV-01 | Core CI path filters omit docs-only changes; PR #11 is documentation-only and should not be expected to run Core tests. | Intentional scope boundary | Record documentation validation separately; do not claim Core CI ran for PR #11 without a run. |
| CI-REV-02 | Core CI targets main and the Core feature branch; the docs branch is not a Core CI target unless relevant paths/base filters match. | Trigger boundary | Keep Core source changes out of this docs PR. |
| CI-REV-03 | SecurityCodeScan preserves SARIF as an artifact but inspected YAML does not use upload-sarif. | Reporting integration gap | Decide whether artifact retention is sufficient or GitHub Code Scanning ingestion is required. |
| CI-REV-04 | CodeQL and SecurityCodeScan trigger on main-targeted PRs, but findings/configuration must be reviewed per run. | Evidence boundary | Record run IDs, commit SHAs, SARIF findings, suppressions and artifact retention before security closure. |
| CI-REV-05 | Contrast is optional/manual and depends on external secrets and a supplied artifact. | Optional tool boundary | Do not count it as a required security gate until approved and configured. |
| CI-REV-06 | Dependabot NuGet configuration uses repository root while inspected Core/test csproj files are nested. | Configuration verification question | Confirm Dependabot creates updates for NUnit/Test SDK and future nested dependencies. |
| CI-REV-07 | Workflow action references mix major tags and full commit pins. | Supply-chain review item | Apply a consistent action-pinning policy if approved; no workflow code changed here. |
| CI-REV-08 | Static-analysis success is not proof of zero vulnerabilities or complete threat coverage. | Evidence interpretation rule | Inspect SARIF findings, rules, analyzed paths and exclusions. |

## 4. Relationship to recovered runs

The historical Core CI runs are listed in CI_EVIDENCE_REGISTER.md: 41331400978, 41332759086, 41333575156, 42383513646, and the separate ADR-referenced run 41332989910.

SecurityCodeScan and CodeQL successful run metadata were recovered for commit 2820285021b1a3d7ae9acd953b3bb65677c33512, but this review does not claim a finding-free result because native scanner output and complete run configuration were not audited here.

PR #11 is documentation-only and has no fresh Core CI execution evidenced for its head.

## 5. Required future release gates

1. Ensure all production code paths are included in Core, Unity, Net and security workflow triggers.
2. Add Unity project validation, EditMode/PlayMode integration tests, and Android/Linux build jobs after those projects exist.
3. Decide whether SecurityCodeScan SARIF must be uploaded to GitHub Code Scanning or retained only as an artifact.
4. Define required versus optional scanners and review findings at the exact commit.
5. Verify Dependabot updates actual nested package references.
6. Define action pinning, branch protection, required checks, artifact retention and approval rules.
7. Add a documented failure/retry and evidence-retention procedure.

## 6. Status

| Area | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| Core CI workflow definition | PROPOSED / configured on feature branch | REVIEWED | Core-only automation |
| Core historical test runs | — | TESTED | Commit/run scoped |
| CodeQL workflow | PROPOSED / configured | REVIEWED | Findings require review |
| SecurityCodeScan workflow | PROPOSED / configured | REVIEWED | SARIF artifact path present |
| Contrast workflow | PROPOSED / optional | REVIEWED | Not an enforced gate |
| Dependabot configuration | PROPOSED / configured | REVIEWED | Nested project coverage not verified |
| Unity/FishNet/Android/Linux CI | PROPOSED | — | NOT IMPLEMENTED / NOT VERIFIED |
| PR #11 CI | — | — | No fresh test run evidenced; docs-only scope |

## 7. Non-claims

No workflow YAML, permission, branch protection, Dependabot configuration, or security finding was changed by this review. No scanner is claimed to have found zero vulnerabilities. No current action major tag is claimed to be the newest available release.
