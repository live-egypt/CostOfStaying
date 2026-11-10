# CI Evidence Register — Cost of Staying

**Revision:** 2026-11-10  
**Decision Status:** — (evidence register; it does not approve project decisions)  
**Evidence Maturity:** TESTED — four historical standalone Core CI runs inspected  
**Repository:** live-egypt/CostOfStaying  
**Evidence scope:** Core CI workflow jobs and retained GitHub Actions artifacts.

## 1. Evidence rules

- Each test result belongs to its exact workflow run and commit SHA.
- Test totals from different commits are separate snapshots, not a cumulative count.
- A successful workflow verifies only the checks it actually executed.
- CI success does not establish Unity Editor compilation, FishNet integration, Android builds, Linux Dedicated Server operation, multiplayer behavior, gameplay balance, or production security.
- TESTED is used here. Do not promote the project-wide or integration status to VERIFIED based only on these runs.
- Artifact digests below are GitHub-reported archive digests; they are not a substitute for inspecting the artifact contents.

## 2. Recovered Core CI snapshots

| Tests passed | Failed | Skipped | Run ID | Tested commit | Job ID | Artifact ID | Artifact SHA-256 |
|---:|---:|---:|---|---|---|---|---|
| 148 | 0 | 0 | 41331400978 | 94c5789b0e82493290ca85133fc692e83ffd9884 | 124922115632 | 13092964341 | b4067bbd6c6b0492a245dea4c8970acca23d7196d398018f5266da42709b4295 |
| 177 | 0 | 0 | 41332759086 | 26a77dcf1394d35d3db48ea0f5b7cde4c118998f | 124926147257 | 13093918489 | 9e3b7b23f47fae5b4dce89c688a8a46e298cce228461c9f069b8e10c54abd82b |
| 181 | 0 | 0 | 41333575156 | 8a193190fb5f2e10c05773fb0789171011bfd2eb | 124928582613 | 13094535795 | d561476795814d2b2f9a57b151539dfbcbb64f1e27b3e896605e64a6db7033d2 |
| 181 | 0 | 0 | 41332989910 | c33faf84aa39856d46181ccc1c9decbfba2777d2 | 124926820979 | 13093814339 | badb2929b1fdd4f4bbacc74405499df5c1902ca2181789f2903369db8dd5f7c1 |
| 187 | 0 | 0 | 42383513646 | 2820285021b1a3d7ae9acd953b3bb65677c33512 | 128435932500 | 13578582284 | f57902a95f22498a4007aad7e135f3144fb8df994895e93a191bf26d8a04bdad |

All five Core CI workflow runs reported completed/success. The retrieved job logs contained explicit NUnit summaries:
- 148 passed, 0 failed, 0 skipped, total 148.
- 177 passed, 0 failed, 0 skipped, total 177.
- 181 passed, 0 failed, 0 skipped, total 181 (run 41333575156).
- 181 passed, 0 failed, 0 skipped, total 181 (run 41332989910).
- 187 passed, 0 failed, 0 skipped, total 187.

The jobs also completed Core dependency-boundary and CoreComposition enforcement, restore, Release build, test-project build, NUnit tests, and test-result upload steps.

## 3. 177-run identifier correction

A historical report associated the 177-test result with run ID 41332755494. The GitHub workflow association for commit 26a77dcf1394d35d3db48ea0f5b7cde4c118998f returned run 41332759086. The job log for 41332759086 explicitly reports 177 passed.

**Treatment:** 41332759086 is the run ID supported by the recovered commit association and log. The historical value 41332755494 remains recorded as an unresolved documentation discrepancy; it is not silently overwritten.

## 4. Artifact retention snapshot

At the retrieval time, GitHub reported all five artifacts as not expired. Their expiry timestamps were:
- Artifact 13092964341: 2027-01-30T17:25:36Z.
- Artifact 13093918489: 2027-01-30T17:41:29Z.
- Artifact 13094535795: 2027-01-30T17:51:12Z.
- Artifact 13093814339: 2027-01-30T17:44:18Z.
- Artifact 13578582284: 2027-02-05T14:50:58Z.

These are the returned repository metadata values, not an assertion that the artifacts will remain accessible indefinitely.

## 5. Additional 181-test snapshot cited by ADR-012 and ADR-013

The Core CI run 41332989910 cited by branch ADR-012 and ADR-013 was retrieved independently. It checked out commit c33faf84aa39856d46181ccc1c9decbfba2777d2, completed job 124926820979 successfully, and its log reports 181 passed, 0 failed, 0 skipped. Artifact 13093814339 was returned as not expired, with digest sha256:badb2929b1fdd4f4bbacc74405499df5c1902ca2181789f2903369db8dd5f7c1.

This is a separate 181/181 snapshot from run 41333575156 on commit 8a193190... . Equal test totals on different commits/runs are not a conflict and must not be deduplicated.

## 6. Security workflow snapshot

For the 187-test commit, associated workflow runs also reported:
- SecurityCodeScan run 42383513631 — completed/success.
- CodeQL Advanced run 42383513638 — completed/success.

The present evidence register does not claim that these scanners prove the absence of vulnerabilities. Their exact jobs, configurations, findings, exclusions, and artifacts require separate tool-specific review.

## 7. Scope limitations

These runs cover the standalone .NET Core project and its NUnit suite on their respective commits. They do not verify:
- Unity project import, Editor compilation, EditMode or PlayMode tests.
- FishNet package compatibility, serialization, RPC behavior, or runtime networking.
- Host mode or Linux Dedicated Server behavior.
- Authentication, transport connection ownership, session mapping, reconnect or restart behavior in production.
- Atomic gameplay transfer under concurrency in a live multiplayer session.
- Android builds, physical-device compatibility, performance, or release readiness.
- Gameplay balance or acceptance of ADR-004's proposed playtest baseline.
- Full production security or operational resilience.

## 8. Current classification

| Claim | Decision Status | Evidence Maturity | Boundary |
|---|---|---|---|
| The five named Core CI runs passed their declared steps | — | TESTED | Exact run/commit only |
| The NUnit totals listed above (five separate run/commit snapshots) | — | TESTED | Historical per-commit snapshots |
| Standalone Core is fully production-ready | — | NOT ESTABLISHED | Not supported by these runs |
| Unity/FishNet integration is verified | — | NOT ESTABLISHED | No integration run evidenced |
| Project is production-ready | — | NOT ESTABLISHED | No project-wide readiness evidence |
