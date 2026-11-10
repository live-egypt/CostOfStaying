# TBD Cross-Reference Map — Cost of Staying

> **Governance status axes:** Decision Status and Evidence Maturity are independent. This document's existence does not approve its claims or prove implementation. See [Project Governance Baseline](PROJECT_GOVERNANCE_BASELINE.md).

**Revision date:** 2026-11-09  
**Record State:** INITIAL RECONCILIATION SNAPSHOT — DOCUMENTATION ONLY; CONTENT AND SOURCE COMPLETENESS REMAIN PARTIAL
**Primary register:** `docs/governance/TBD_REGISTER.md`  
**Detailed row mapping:** `docs/governance/SOURCE_TO_REQUIREMENT_MATRIX.md`

## 1. Purpose

This map records only non-trivial relationships that must not be flattened into generic dependency strings. The per-item dependency fields remain in the Source-to-Requirement Matrix and the primary TBD Register.

## 2. Binding relationships

| From | Relation | To | Source event | Interpretation | Current status |
|---|---|---|---|---|---|
| TBD-37 | Distinct requirement / cross-reference | TBD-45 | User turn 368 | Record Preservation is not Failure Safety. Each needs independent acceptance/evidence. | APPROVED — BINDING; owner discrepancy for TBD-37 OPEN |
| TBD-49 | Prerequisite | TBD-34 | User turn 368 | Level 2 Resolution Contract precedes its Recovery-specific application. | APPROVED — BINDING |
| TBD-53 | Parent process | TBD-53-01…TBD-53-54 | User turn 368 | 54 source-to-requirement mappings; individual mapping details were not supplied. | APPROVED — BINDING; OPEN |
| TBD-55 | Mutual design dependency | TBD-58 | User turn 368 | Contract and trusted external-effect evidence source may be designed in parallel; neither is a one-way prerequisite. | APPROVED — BINDING |
| TBD-58 | Mutual design dependency | TBD-55 | User turn 368 | Same mutual relation, shown in reverse for explicitness. | APPROVED — BINDING |
| D05-C | Referral | D03-T08 / related external effect idempotency | D05 source records | Referral only; not a standalone D05 workstream. | RECORDED |
| D05-D | Referral | D8 / InFlight | D05 source records | Referral only; not a standalone D05 workstream. | RECORDED |
| D05-E | Referral | D03-T14 / Level 2 Resolution | D05 source records | Referral only; not a standalone D05 workstream. | RECORDED |

## 3. Source event sequence

| Event | Source-supported change | Resulting register state |
|---|---|---|
| User turn 364 | Accepted the 58-item TBD Register; preserved TBD-01…20 as PENDING SOURCE and TBD-21…58 as recorded/registered pending source verification | D04-Task-07 closure remained administrative only |
| User turn 368 | Approved the four relationship clarifications and closed D04-Task-07 as CLOSED — REGISTERED / UNRESOLVED ITEMS RETAINED | Relationships binding; unresolved TBDs retained |
| User turn 376 | Accepted TBD-59…70 as REGISTERED — UNRESOLVED | Total expanded to 70 |
| User turn 380 | Accepted TBD-71…77 as REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | Total expanded to 77 |
| User turn 384 | Accepted TBD-78…85 as REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | Total expanded to 85; D07 Charter approved as charter, D07 workstream not complete |

Per-message calendar dates are NOT PRESENT IN EXPORT.

## 4. Non-equivalence rules

- TBD-37 ≠ TBD-45.
- TBD-34 ≠ TBD-49.
- TBD-53 is a process with 54 sub-tasks, not a single requirement.
- TBD-55 ≠ TBD-58; their relationship is mutual, not hierarchical.
- D04 administrative closure ≠ requirement resolution.
- D07 Charter approval ≠ D07 Workstream completion.
- Register inclusion ≠ source verification ≠ implementation ≠ test verification.

## 5. Open relationship issues

- CONFLICT-01: TBD-37 owner value differs between main register and Binding Clarifications. Do not select an owner.
- CONFLICT-02: TBD-45 lists Core Owner / Application Owner; primary owner and responsibility split remain unspecified.
- TBD-53: all 54 sub-task source mappings remain unavailable in the current register.
- TBD-59…85: original requirement-level mapping remains pending; per-item owner/phase/dependencies are not specified.
