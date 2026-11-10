# Requirement Evidence Matrix — TBD-01…85

> **Governance status axes:** Decision Status and Evidence Maturity are independent. This document's existence does not approve its claims or prove implementation. See [Project Governance Baseline](PROJECT_GOVERNANCE_BASELINE.md).

**Revision date:** 2026-11-09  
**Record State:** INITIAL RECONCILIATION SNAPSHOT — DOCUMENTATION ONLY; CONTENT AND SOURCE COMPLETENESS REMAIN PARTIAL
**Primary register:** `docs/governance/TBD_REGISTER.md`  
**Source mapping:** `docs/governance/SOURCE_TO_REQUIREMENT_MATRIX.md`

## 1. Evidence policy

This matrix separates four independent evidence layers:
1. Source evidence — original requirement and approval/registration source.
2. Implementation evidence — exact code path and commit.
3. Test evidence — reproducible test, environment, and result.
4. Verification evidence — reviewer/CI artifact and bounded acceptance result.

`NOT ASSESSED` means this reconciliation has not yet mapped the item to code/tests. It does **not** mean that no implementation or test exists. Do not convert `NOT ASSESSED` to `NOT IMPLEMENTED` or `NOT VERIFIED` without a repository/evidence check.

## 2. Item-level evidence status

| Range | Source register status | Source event mapping | Original requirement match | Implementation evidence | Test evidence | Verification evidence |
|---|---|---|---|---|---|---|
| TBD-01…20 | PENDING SOURCE | User turn 364 — register accepted | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-21…33 | RECORDED — SOURCE VALIDATION PENDING | User turn 364 — register accepted | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-34 | RECORDED — SOURCE VALIDATION PENDING | User turn 364; User turn 368 clarifies prerequisite relationship with TBD-49 | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-35…36 | RECORDED — SOURCE VALIDATION PENDING | User turn 364 — register accepted | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-37 | RECORDED — SOURCE VALIDATION PENDING | User turn 364; User turn 368 clarifies requirement distinction | PENDING; owner conflict open | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-38…44 | RECORDED — SOURCE VALIDATION PENDING | User turn 364 — register accepted | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-45 | RECORDED — SOURCE VALIDATION PENDING | User turn 364; User turn 368 clarifies requirement distinction | PENDING; primary owner/split TBD | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-46…52 | RECORDED — SOURCE VALIDATION PENDING | User turn 364 — register accepted | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-53 | REGISTERED — SOURCE VALIDATION PENDING | User turn 364; User turn 368 confirms process/54 sub-tasks | 54 mappings pending | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-54 | REGISTERED — SOURCE VALIDATION PENDING | User turn 364 — register accepted | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-55 | REGISTERED — SOURCE VALIDATION PENDING | User turn 364; User turn 368 confirms mutual dependency with TBD-58 | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-56…57 | REGISTERED — SOURCE VALIDATION PENDING | User turn 364 — register accepted | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-58 | REGISTERED — SOURCE VALIDATION PENDING | User turn 364; User turn 368 confirms mutual dependency with TBD-55 | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-59…70 | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | User turn 376 — D05 extension accepted | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-71…77 | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | User turn 380 — D06 extension accepted | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |
| TBD-78…85 | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | User turn 384 — D07 extension accepted | PENDING | NOT ASSESSED | NOT ASSESSED | NOT ASSESSED |

## 3. Evidence record requirements

For each individual TBD, populate the following only when the evidence exists:

| Field | Required value |
|---|---|
| Evidence ID | Stable unique identifier |
| TBD ID | One of TBD-01…85 |
| Source locator | Exact file, conversation turn, section, and quoted/anchored text |
| Requirement statement | Original wording; do not paraphrase in place of source |
| Implementation path | Repository path and symbol(s), or explicit NOT IMPLEMENTED after source inspection |
| Commit SHA | Exact commit containing the implementation |
| Test ID | NUnit/Unity/Integration test identifier |
| Test command | Reproducible command and required environment |
| Run ID / Artifact | Exact CI run and downloadable artifact |
| Result | Pass/fail/skipped with counts |
| Verification scope | What the evidence proves and what it does not prove |
| Reviewer / acceptance | Explicit acceptance event, if any |
| Status | DOCUMENTED / SOURCE VERIFIED / IMPLEMENTED / TESTED / VERIFIED / NOT IMPLEMENTED / TBD |

## 4. Repository traceability gap

A read-only scan of the base-branch `docs/governance/REQUIREMENTS_TRACEABILITY.md` (76 lines) found zero explicit references to `TBD-01`…`TBD-85`. This means the 85-item TBD register is not directly mapped in that file. A recursive scan of all repository documentation, code, and tests has not yet been performed. Do not infer mappings from similar wording.

## 5. Existing evidence that must not be overgeneralized

- Core CI run `42383513646` is reported in a Library source record as 187/187 on commit `2820285021b1a3d7ae9acd953b3bb65677c33512`; direct workflow/artifact reconciliation remains pending in this documentation pass.
- Core CI run `41333575156` is recorded in the repository status register as 181/181 on commit `8a193190fb5f2e10c05773fb0789171011bfd2eb`.
- These are distinct commit/run pairs. Neither proves Unity compilation, FishNet integration, Android build, or Linux Dedicated Server operation.
- The 109-record Consistency Matrix is a separate register from the 85 TBD requirements. Its 24 PENDING SOURCE records must not be added to or subtracted from the TBD count.

## 6. Current result

No TBD is declared IMPLEMENTED, TESTED, VERIFIED, or RESOLVED by this scaffold. The matrix is ready for item-by-item code/test/evidence mapping after the source layer is accepted.
