# PDR-014 — SOURCE-ORIGIN-01 Review

**Workstream:** A5 — Document Control  
**Status:** CANNOT RESOLVE — SO-GAP-04 PRIMARY SOURCE NOT RECOVERED  
**Document class:** Source-origin evidence review  
**Repository:** `live-egypt/CostOfStaying`  
**Base examined:** `main` plus PR #8/#9/#11 provenance branches  
**Related records:** SOURCE-ORIGIN-01; SO-GAP-01…06; MSR-001…004; SRC-01…03; PDR-005; PDR-010; PDR-012; PDR-013

## 1. Purpose

Determine whether the evidence required to establish SOURCE-ORIGIN-01 has been recovered and validated, or whether an explicit owner decision has suspended recovery with the consequences recorded.

PDR-014 is not a general review of whether provenance documentation exists. It concerns the specific unresolved source-origin claim and the decisive source dependency SO-GAP-04.

## 2. Controlling A4 baseline

The A4.6 source record supplied in the project materials states:

- **PDR-014:** SOURCE-ORIGIN-01 — OPEN.
- **SO-GAP-04:** NOT RECOVERED — CONFIRMED.
- A secondary source exists, but the original decisive source has not been recovered.
- The required evidence is the original source, or an independent owner decision that suspends recovery and records the impact without claiming that the source was recovered or provenance verified.
- The recommendation is **CANNOT RESOLVE**; PDR-014 and SOURCE-ORIGIN-01 remain OPEN.

This is the controlling baseline for this review. It is not superseded by later summaries or the existence of provenance-register documents.

## 3. Branch-aware repository evidence

PR #8, PR #9, and PR #11 contain provenance/source inventory documentation:

| PR / branch | Artifact | Blob SHA | What it establishes |
|---|---|---|---|
| PR #8 / `docs/stage1-source-provenance-recovery` | `docs/governance/sources/PROVENANCE_REGISTER.md` | `fcaeb5d2acd0cdaeaf0d08b68a30754f4623efb5` | A partial provenance register; attachment hashes and Library/chat limitations are preserved. |
| PR #9 / `docs/source-provenance-reconciliation-2026-11-09` | `docs/governance/PROVENANCE_REGISTER.md` | `d7f08fbb3ccd091881a8a29416bad92da34eb13c` | A partial provenance register with unresolved source-origin attribution and source recovery gaps. |
| PR #11 / `docs/project-reconciliation-baseline-2026-11-10` | `docs/governance/PROVENANCE_REGISTER.md` | `2c13e9d5e20627ce730eb34dff098b6cd9022c4d` | Later metadata-only provenance crosswalk; source completeness remains partial. |
| PR #11 / same branch | `docs/governance/CONFLICT_REGISTER.md` | `565ba48fa830a59d6f6c27e0ef44c7f528ee5486` | Records the ChatGPT/DeepSeek origin conflict and export-to-thread mapping gap. |

These documents establish that provenance records exist on unmerged review branches. They do not establish recovery of SO-GAP-04. The PR #11 provenance register does not provide an authenticated original source for the decisive claim, and the conflict register records other source-origin conflicts without closing them.

## 4. Evidence matrix

| Required evidence | Current finding | Status |
|---|---|---|
| Original SO-GAP-04 source artifact | Not recovered in the examined materials | MISSING |
| Source identity and provenance | Not independently established | MISSING |
| Completeness / integrity validation of the original | Cannot be performed without the original | BLOCKED |
| Exact correspondence between the original and the disputed origin claim | Not established | MISSING |
| Original Project chat threads / IDs for chats 1 and 2 | Not fully available in the reviewed source set | OPEN |
| Exact mapping of `محادثة 2.html` to Project chat 2 | Not verified by direct thread ID / metadata | OPEN |
| DeepSeek native original exports | Not present in the current local attachment snapshot | SOURCE PENDING |
| Explicit owner decision permanently suspending SO-GAP-04 recovery | Not located in the reviewed A4/A5 source record | NOT ESTABLISHED |
| Impact analysis for any recovery suspension | No such independent suspension decision available | NOT ESTABLISHED |

## 5. Important distinctions

- A provenance register is a control document; it is not the primary source it describes.
- A repository copy or extracted-text transcription is not proof of raw-byte equality with the Library source.
- A user-attested source description is not independent origin verification.
- A filename, message count, hash prefix, or summary does not establish the identity of a conversation thread unless the source mapping is demonstrated.
- A source gap being recorded in multiple documents does not mean the source has been recovered.
- A decision to suspend recovery, if later issued, must be recorded as a suspension with impact; it must not be represented as successful recovery or verified provenance.

## 6. Determination

**PDR-014 = CANNOT RESOLVE — SO-GAP-04 PRIMARY SOURCE NOT RECOVERED; NO INDEPENDENT RECOVERY-SUSPENSION DECISION LOCATED.**

The available evidence confirms the existence of partial provenance documentation and several source-origin limitations. It does not provide the decisive primary source required by SO-GAP-04, nor an independent owner decision suspending recovery with a documented impact assessment.

Therefore:

- SOURCE-ORIGIN-01 remains OPEN.
- SO-GAP-04 remains OPEN — NOT RECOVERED.
- PDR-014 remains OPEN.
- No origin claim is promoted to SOURCE-VERIFIED.
- No competing platform-origin claim is selected by inference.

## 7. Recovery path and closure criteria

PDR-014 may be reconsidered only when one of these two paths is satisfied:

### Path A — Recover the original source

1. Identify the exact source artifact named by SO-GAP-04.
2. Recover the original artifact from its authoritative location.
3. Preserve the original file/export unchanged and record source ID, location, acquisition context, and integrity hash where raw bytes are available.
4. Validate completeness, identity, and provenance.
5. Map the decisive source passages to SOURCE-ORIGIN-01 and the related claims.
6. Reconcile the result with SO-GAP-01…06, MSR-001…004, SRC-01…03, PDR-010, and PDR-012.

### Path B — Explicitly suspend recovery

1. Record a separate owner decision explicitly suspending further recovery of SO-GAP-04.
2. State the scope and reason for suspension.
3. Record the affected claims, documents, requirements, and decisions.
4. Preserve SOURCE-ORIGIN-01 as UNVERIFIED / OPEN or DEFERRED as the decision explicitly directs.
5. Do not claim the original source was recovered or provenance verified.

## 8. Non-effects

- Does not close SOURCE-ORIGIN-01, SO-GAP-04, or PDR-014.
- Does not resolve the ChatGPT/DeepSeek origin conflict.
- Does not establish the identity of `محادثة 2.html` as Project chat 2.
- Does not resolve LIF-DEC-01 or ADR-REV-001.
- Does not approve PR #8/#9/#11 as canonical.
- Does not authorize migration, deletion, merging, or source replacement.
- Does not change `main`, code, gameplay, Unity, FishNet, tests, CI, or runtime behavior.

## 9. Status summary

| Item | Status |
|---|---|
| Provenance register documents on PR #8/#9/#11 | VERIFIED — BRANCH CONTENT EXISTS |
| SO-GAP-04 original source | NOT RECOVERED |
| Source identity / origin claim | NOT VERIFIED |
| Recovery suspension decision | NOT LOCATED |
| SOURCE-ORIGIN-01 | OPEN |
| PDR-014 | CANNOT RESOLVE — OPEN |
| A5 PDR-014 closure | NOT ACHIEVED |
