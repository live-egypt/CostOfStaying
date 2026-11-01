# Project Status and Execution Register

**Revision:** 2026-11-01  
**Status:** ACTIVE CONTROL DOCUMENT  
**Applies to:** repository-wide documentation, Core, Unity and networking work  
**Related:** [Technical Master](architecture/PHASE1_TECHNICAL_MASTER.md), [ADR-007](governance/adr/ADR-007-PLAYER-LIFECYCLE-VITALITY-CONFLICT.md), [Requirements Traceability](governance/REQUIREMENTS_TRACEABILITY.md)

> This register coordinates work and verification. It does not override an approved ADR or owner decision, and it does not convert a proposal into an implementation.

## 1. Repository delivery state

The default branch is `main`. The implementation and consolidation work is isolated in review branches and must not be treated as delivered to the default branch until its pull request is reviewed and merged.

| Workstream | Current location | State | Delivery interpretation |
|---|---|---|---|
| Reorganized v2.1 design documentation | PR #1 / historical branch | Superseded by consolidated work where noted | Do not reintroduce legacy files |
| Uploaded transcript audit | PR #6 branch | Included in consolidated review | Audit evidence, not executable source |
| Standalone pure-C# Core and NUnit tests | PR #6 branch | CI-verified within stated scope | Not yet merged to `main`; full Core remains partial |
| Unity source-package static review | PR #5 branch | Audit checkpoint | Does not claim the ZIP/binary package was uploaded |
| Unity project and FishNet integration | Not present in reviewed tree | NOT IMPLEMENTED | Requires actual project/package files and exact-version verification |

PR #6 is the consolidated Core/documentation review and supersedes PR #4, which is closed to avoid duplicate delivery. PR #5 is a separate source-package audit checkpoint. PR #2, PR #3 and PR #4 are superseded; do not continue parallel implementation on them.

## 2. Decision authority and conflict control

Use this precedence when documents or code disagree:

1. Explicit owner-approved decisions and approved ADRs.
2. Current approved gameplay rules and system specifications.
3. Security and trust-boundary requirements.
4. Technical Master and requirements traceability.
5. Implementation and test evidence.
6. Historical conversation transcript and superseded drafts.

An implementation or passing test cannot silently revoke an owner-approved gameplay decision.

### Resolved decision: player lifecycle (ADR-007 APPROVED)

ADR-007 has been resolved in favor of the earlier owner-approved model and reaffirmed on 2026-11-01:

- Canonical states: `Alive`, `Spirit`, `Eliminated`.
- Vitality reaching zero automatically transitions `Alive → Spirit`.
- `Resurrect` and `Eliminate` are explicit server-authoritative operations.
- `Eliminated` is terminal.
- Ordinary Restore never resurrects; only explicit Resurrect can transition Spirit → Alive.
- The v2.1 `Alive → Dead` lifecycle text is superseded; maintained player-lifecycle specifications follow ADR-007.

The Core source and NUnit tests have been updated on `feature/core-lifecycle-resolution`. Core CI run 41330344616 verified Core source commit 08b68e68a6cf393c6177b6b54ecd53d6ffff5496: dependency-boundary and CoreComposition construction-root scans, Release build and 132/132 NUnit passed. This includes lifecycle, Result, typed IDs, MatchLifecycle, CoreComposition, time/revision values, command envelope, bounded sequence tracker and bounded FIFO command queue and match-scoped player registry. The Core API does not implement authorization, costs, cooldowns or match-phase policy; those remain server application responsibilities.

## 3. Verified evidence boundary

The latest GitHub Actions Core CI run 41330344616 passed on source commit 08b68e68a6cf393c6177b6b54ecd53d6ffff5496: dependency-boundary and CoreComposition construction-root scans, Release restore/build succeeded; NUnit reported 132 passed, 0 failed, 0 skipped. TRX artifact 13093370056 is retained.

This verifies only the source and tests exercised by that workflow. It does **not** verify:

- Unity Editor import or compilation.
- Unity EditMode or PlayMode tests.
- FishNet 4.7.4R API compatibility or serialization.
- Host mode or Dedicated Server behavior.
- Authentication, connection/session mapping or production security.
- Android builds, device compatibility, performance or release readiness.

Never widen a verification claim beyond its actual command, commit, environment and retained evidence.

## 4. Canonical work sequence

| Order | Work package | Entry condition | Exit evidence |
|---:|---|---|---|
| 0 | Preserve and index historical material | Source inventory available | Transcript audit, no blind bulk-copy |
| 1 | Resolve lifecycle and gameplay ADR conflicts | Owner decision | Approved ADR, canonical state/transition table |
| 2 | Freeze Core vocabulary and contracts | ADRs reconciled | One definition per type, namespace and ownership register |
| 3 | Implement Core composition, results, identity, time and state | Contracts approved | Pure-C# build and focused NUnit tests |
| 4 | Implement command admission and deterministic simulation | State/ordering contracts approved | Replay, ordering, rejection and no-partial-mutation tests |
| 5 | Implement atomic transfer and snapshots if in approved scope | Transaction/persistence decisions approved | Failure-injection and deterministic round-trip tests |
| 6 | Create/pin Unity project and assemblies | Exact Unity version available | Editor import/compile and EditMode evidence |
| 7 | Integrate FishNet adapters | Exact installed FishNet source/version available | Compile, serialization, Host and Dedicated Server tests |
| 8 | Validate Android, security and performance | Integrated build available | Device matrix, threat tests, measured budgets and release evidence |

Do not skip a gate to make the project appear more complete. Keep deferred systems explicitly marked NOT IMPLEMENTED or TBD.

## 5. Repository hygiene rules

- Maintain one canonical implementation for each type and one source of truth for each decision.
- Do not create `PlayerId` beside `ActorId`, or parallel `Result` / lifecycle / composition abstractions, without a reviewed naming decision.
- Keep Core free of UnityEngine and FishNet dependencies.
- Keep server-authoritative identity, ticks, revisions and state mutation outside client-controlled payloads.
- Do not add speculative package versions, API signatures, gameplay constants or security claims.
- Do not delete a document until its unique decisions and requirements have been migrated, links updated, and references checked.
- Preserve Git history; remove obsolete files through reviewed commits rather than history rewriting.
- Every implementation change must include tests and update the requirement traceability.
- Work on a dedicated branch and PR. Do not push directly to `main` or merge without explicit authorization.

## 6. Immediate next actions

1. Continue the Core roadmap with session lifecycle and authoritative match/player state ownership; do not mark the full Core complete.
2. Add command payload validation/application handlers on top of the verified bounded queue.
3. Implement movement and Debuff/atomic transfer only after their contracts and gameplay parameters are approved.
4. Keep ADR-008 PROPOSED until production sequence/session/queue limits and reconnect policy are approved.
5. Review PR #6 as the consolidated technical baseline; it remains unmerged pending review.
6. Keep PR #5 as static source audit only; do not represent the Unity source ZIP as uploaded.
7. Keep Unity/FishNet integration gated on actual project metadata and exact package verification.

## 7. Status vocabulary

- `PROPOSED`: design suggested, not approved.
- `DECISION REQUIRED`: incompatible choices remain unresolved.
- `NOT IMPLEMENTED`: no corresponding source implementation exists.
- `IMPLEMENTED`: source exists in a named branch/commit.
- `VERIFIED`: a named reproducible check passed for that exact scope.
- `PARTIAL`: some subcomponents exist, but the complete requirement is not met.

A status must always be accompanied by its scope and evidence.

## 8. Automated build and dependency maintenance

The Core workflow `.github/workflows/core-ci.yml` is configured to run for relevant Core/test/workflow changes on pushes to `main` and `feature/core-lifecycle-resolution`, and pull requests targeting `main`. It also supports manual dispatch and a weekly scheduled verification on the default branch (the schedule will not run from an unmerged feature branch).

The workflow performs:
1. .NET 10 SDK setup with NuGet cache.
2. Restore of the NUnit test project and its Core dependency.
3. Release build of the pure-C# Core.
4. Release build of the test project.
5. NUnit execution with a TRX result file.
6. Artifact upload even when tests fail, when a result file exists.

Concurrency cancels obsolete in-progress runs for the same workflow/ref. Job timeout is 15 minutes. Workflow permissions are read-only for repository contents.

Dependabot is configured in `.github/dependabot.yml` for weekly NuGet and GitHub Actions update pull requests. Dependency updates are proposals: they must pass CI and receive review before merge. Automatic dependency PR creation is enabled; automatic merging is not.

**Current Core verification state (2026-11-01):** Core CI run `41330344616` completed successfully against source commit `08b68e68a6cf393c6177b6b54ecd53d6ffff5496`: dependency-boundary and CoreComposition construction-root scans, Release restore/build/test succeeded; NUnit reported 132 passed, 0 failed, 0 skipped; TRX artifact `13093370056` was uploaded. This verifies standalone Core only, not Unity/FishNet. The inherited security workflows contained template/configuration defects. The branch now contains scoped replacements for CodeQL, MobSF, SecurityCodeScan, Contrast, NowSecure, Debricked and Frogbot. CodeQL and MobSF replacement runs have passed; SecurityCodeScan's latest SARIF upload revision is running; account-backed services remain manual-only and unverified until credentials/artifacts exist.


## 9. Latest CI and security-scanner audit

The latest Core CI run `41330344616` passed on source commit `08b68e68a6cf393c6177b6b54ecd53d6ffff5496`: dependency-boundary and CoreComposition construction-root scans, Release restore/build succeeded; NUnit reported 132 passed, 0 failed, 0 skipped. Artifact `13093370056` was retained. This verifies standalone Core only, not Unity/FishNet.

The existing MobSF, SecurityCodeScan and Contrast Scan workflows failed before producing completed security-analysis results:
- MobSF: pinned `semgrep==0.50.1` dependency could not be installed.
- SecurityCodeScan: root-level `dotnet restore` failed because there is no root solution/project.
- Contrast Scan: required organization ID was absent and the configured artifact path remains a placeholder.

These failures are not clean security scans and must not be reported as zero findings. The detailed run IDs, log-derived causes and remediation gates are recorded in [GitHub Actions Pipeline Audit](audit/GITHUB_ACTIONS_PIPELINE_AUDIT_2026-11-01.md). Branch PR #6 now contains replacement workflow definitions: MobSF uses pinned mobsfscan 1.1.0 and mobile-source path filters; SecurityCodeScan targets the explicit NUnit project; Contrast is manual-only and validates a real artifact path and secrets. These replacements are not yet verified and do not change `main` until PR merge. Do not make them required checks until they complete successfully against the intended artifacts.
