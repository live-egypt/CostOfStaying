> **Repository copy provenance (2026-11-09):** Imported from the extracted text of Project Library record `DECISION_AND_CONFLICT_LOG.md` (file ID `file_00000000c83c8210a0932c4a7073271b`). The Library source record was not modified. This copy preserves the extracted text; raw-byte equality with the Library payload is not asserted.  
> **Status boundary:** Content is source-recorded. It does not imply that requirements are resolved, decisions are approved, or implementation is verified.

# Decision and Conflict Log — Stage 1

> **Status:** RECORDED — OPEN. Local review artifact. No source conflict is silently resolved.
> **Source:** `محادثة 2.html`; register/Binding Clarifications reconstructed in `FULL_DOCUMENTS_REVIEW.md`.
> **Dates:** Individual message dates are `NOT PRESENT IN EXPORT`.

## CONFLICT-01 — TBD-37 Owner discrepancy

| Field | Recorded value | Location |
|---|---|---|
| TBD | TBD-37 — Record Preservation | Main register and Binding Clarifications |
| Requirement | Retain the Compensation Record while Compensation is in progress | Register snapshot |
| Source status | BLOCKING | Main register |
| Dependencies | D8, ADR-013, ADB-01 | Main register |
| Owner value A | `Core Owner / Application Owner` | Main TBD-21…58 table |
| Owner value B | `Core Owner` | Binding Clarifications table |
| Classification | OPEN — SOURCE VERIFICATION REQUIRED | Stage 1 |
| Resolution | NOT DETERMINED | Stage 1 |

**Treatment:** Preserve both source values. TBD-37 owner is recorded as `CONFLICT` in the Stage 1 working register. Neither value is selected. The existence of ADR-013 or the D8 reference is not sufficient authority to override one recorded owner field.

**Required closure evidence:** Locate the original approved source statement or an explicit user correction that identifies the authoritative owner field. Record the exact source turn and date if present.

## CONFLICT-02 — TBD-45 Multiple Owners

| Field | Recorded value | Location |
|---|---|---|
| TBD | TBD-45 — Failure Safety | Main register and Binding Clarifications |
| Requirement | Do not delete an InProgress record during failure processing | Register snapshot |
| Source status | BLOCKING | Main register |
| Dependencies | D8, ADR-013 | Main register |
| Owner value | `Core Owner / Application Owner` | Both recorded locations |
| Classification | OPEN — MULTIPLE OWNERS | Stage 1 |
| Primary owner | TBD | Not specified |
| Responsibility split | TBD | Not specified |

**Treatment:** Keep the two-owner value exactly as recorded. Do not convert it to one owner. `Multiple Owners` is not itself proof of an inconsistency between source locations; the unresolved issue is the missing primary owner/responsibility split.

**Required closure evidence:** Explicit owner assignment or responsibility matrix approved by the user.

## Binding relationship between TBD-37 and TBD-45

The two items are distinct requirements and are cross-referenced (`TBD-37 ↔ TBD-45`). Evidence for one does not establish the other.

## Decision log

| Decision ID | Decision | Source event | Status |
|---|---|---|---|
| ST1-DEC-01 | Preserve TBD-37 owner discrepancy; do not choose an owner | User approval in current conversation | APPROVED — BINDING FOR STAGE 1 |
| ST1-DEC-02 | Preserve TBD-45 multiple-owner field; primary owner and split remain TBD | User approval in current conversation | APPROVED — BINDING FOR STAGE 1 |
| ST1-DEC-03 | Keep TBD-59…85 registered unresolved and source verification pending | User turns 376, 380, 384; current approval | APPROVED — BINDING FOR STAGE 1 |
| ST1-DEC-04 | Individual source-message dates absent from export are recorded as `NOT PRESENT IN EXPORT` | Current user instruction | APPROVED — BINDING |

## Open actions

1. Resolve CONFLICT-01 only after authoritative source verification.
2. Resolve TBD-45 ownership split only after explicit assignment evidence.
3. Match TBD-59…85 individually to original requirement statements; retain `SOURCE VERIFICATION PENDING` until completed.