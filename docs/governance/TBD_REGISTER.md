> **Repository copy provenance (2026-11-09):** Imported from the extracted text of Project Library record `TBD_REGISTER.md` (file ID `file_0000000087cc821087da41b9341877d6`). The Library source record was not modified. This copy preserves the extracted text; raw-byte equality with the Library payload is not asserted.  
> **Status boundary:** Content is source-recorded. It does not imply that requirements are resolved, decisions are approved, or implementation is verified.

## `docs/governance/TBD_REGISTER.md`

# TBD Register — TBD-07

> **STATUS: PARTIAL — TBD-01…20 PENDING SOURCE; TBD-21…58 RECEIVED / SOURCE VALIDATION PENDING WITH OWNER DISCREPANCIES; TBD-59…85 REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING**
>
> **Source:** uploaded `محادثة 2.html`; initial register captured in Assistant turn 363, accepted in User turn 364; clarifications accepted in User turn 368; extensions accepted in User turns 376, 380 and 384. Per-message calendar dates are not present in the export.
> **Source branch:** NOT IDENTIFIED. This register is reconstructed from the conversation export; it is not copied from another Git branch.

## 1. Governing interpretation

- `RECORDED` / `REGISTERED` does not mean `SOURCE VERIFIED` and does not mean `RESOLVED`.
- Listing or administratively closing the register does not resolve an underlying TBD.
- `D04-Task-07` closure means `CLOSED — REGISTERED / UNRESOLVED ITEMS RETAINED`, not `CLOSED — ALL TBDs RESOLVED`.
- Preserve distinct requirements and cross-references; do not collapse them because they share a topic.
- This document registers uncertainty; it does not approve a new architecture or change an ADR.

## 2. Register: TBD-01…58

| ID | Topic | Source status (original register) | Registry status after owner decision | Dependencies | Owner | Target phase | Source verification |
|---|---|---|---|---|---|---|---|
| TBD-01 | Reserved Outcome vs Reservation State | PENDING SOURCE | PENDING SOURCE | D04-Task-01 | Design Owner | Phase 2 | Pending |
| TBD-02 | Completed Definition | OPEN | PENDING SOURCE | D04-Task-06, EVD-02 | Design Owner | Phase 2 | Pending |
| TBD-03 | Failed Policy | CONDITIONAL | PENDING SOURCE | T05, EVD-02, EVD-04 | Design Owner / Product Owner | Phase 2 | Pending |
| TBD-04 | Indeterminate Source of Truth | BLOCKING | PENDING SOURCE | B3-R09, EVD-04, EVD-05 | Design Owner | Phase 2 | Pending |
| TBD-05 | Transitions After Indeterminate | CONDITIONAL | PENDING SOURCE | B3-R09, D03-T14 | Design Owner | Phase 2 | Pending |
| TBD-06 | Reservation State vs Execution State | OPEN | PENDING SOURCE | D04-Task-01, D04-Task-02 | Design Owner | Phase 2 | Pending |
| TBD-07 | Restart Behavior | OPEN | PENDING SOURCE | ADR-015, D8 | Design Owner / Application Owner | Phase 3 | Pending |
| TBD-08 | Attempt / Lifecycle Ordering | BLOCKING | PENDING SOURCE | ADB-01, D02-B Final | Design Owner | Phase 2 | Pending |
| TBD-09 | Reservation Record Creation Point | BLOCKING | PENDING SOURCE | ADB-01, D04-Task-02 | Design Owner | Phase 2 | Pending |
| TBD-10 | Reserved Outcome → InProgress Relationship | BLOCKING | PENDING SOURCE | ADB-01, D04-Task-02 | Design Owner | Phase 2 | Pending |
| TBD-11 | Level 2 Resolution | REQUIRED | PENDING SOURCE | D03-T14, ADR-D03-02 | Design Owner | Phase 2 | Pending |
| TBD-12 | Failed Retry Policy | BLOCKING | PENDING SOURCE | T05, ADR-015 | Design Owner / Product Owner | Phase 2 | Pending |
| TBD-13 | Handoff Boundary | CONDITIONAL | PENDING SOURCE | D05, D04-Task-02 | Application/Network Owner | Phase 2 | Pending |
| TBD-14 | InFlight Tracking | BLOCKING | PENDING SOURCE | D8, ADR-013 | Core Owner / Application Owner | Phase 3 | Pending |
| TBD-15 | Cleanup Authority | CONDITIONAL | PENDING SOURCE | D8, ADR-013 | Design Owner | Phase 2 | Pending |
| TBD-16 | Restart Survival | OPEN | PENDING SOURCE | ADR-015, D8 | Application Owner | Phase 3 | Pending |
| TBD-17 | Indeterminate Resolution Authority | BLOCKING | PENDING SOURCE | B3-R09, D03-T14 | Design Owner / Product Owner | Phase 2 | Pending |
| TBD-18 | Reservation Record Publication | BLOCKING | PENDING SOURCE | ADB-01, D02-B Final | Design Owner | Phase 2 | Pending |
| TBD-19 | Result Replay Model | CONDITIONAL | PENDING SOURCE | ADR-015, EVD-06 | Core Owner | Phase 2 | Pending |
| TBD-20 | Reservation Capacity Limits | OPEN | PENDING SOURCE | Phase 3 capacity/performance planning | Product Owner / Design Owner | Phase 3 | Pending |
| TBD-21 | Reservation Record Mutability | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | D04-Task-03, ADR-015 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-22 | Cancel Semantics | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | C-N01, D04-Task-03 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-23 | Delete Exception Contract | BLOCKING | RECORDED — SOURCE VALIDATION PENDING | C-N02, D8, ADR-015 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-24 | MarkFailed Preconditions | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | C-N03, T05, EVD-04 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-25 | Compensation Record Identity | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | C-N04, ADR-015 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-26 | Duplicate Compensation Prevention | BLOCKING | RECORDED — SOURCE VALIDATION PENDING | D03-T08, ADR-015 | Core Owner | Phase 2 | Recorded; source validation pending |
| TBD-27 | Outcome Immutability | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | D04-Task-03, EVD-02 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-28 | Corrected Outcome Contract | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | D03-T14, EVD-05 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-29 | Correction Authority | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | D03-T14, B3-R09 | Product Owner / Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-30 | Reservation Side Effect Definition | OPEN | RECORDED — SOURCE VALIDATION PENDING | D03-T08, EVD-04 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-31 | Effect Idempotency | BLOCKING | RECORDED — SOURCE VALIDATION PENDING | D03-T08, ADR-015 | Core Owner / Application Owner | Phase 2 | Recorded; source validation pending |
| TBD-32 | External Reconciliation | OPEN | RECORDED — SOURCE VALIDATION PENDING | EVD-04, External Reconciliation Contract | Application/Network Owner | Phase 3 | Recorded; source validation pending |
| TBD-33 | Forward Recovery Authority | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | D03-T14, ADR-D03-02 | Product Owner / Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-34 | Recovery-Specific Level 2 Resolution | BLOCKING | RECORDED — SOURCE VALIDATION PENDING | D03-T14, ADR-D03-02 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-35 | Record vs Effect Compensation Relationship | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | D04-Task-03, EVD-03, EVD-04 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-36 | Indeterminate During Compensation | BLOCKING | RECORDED — SOURCE VALIDATION PENDING | B3-R09, D8, EVD-05 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-37 | Record Preservation — retain the Compensation Record while Compensation is in progress | BLOCKING | RECORDED — SOURCE VALIDATION PENDING | D8, ADR-013, ADB-01 | CONFLICT — see CONFLICT-01 (main register: Core Owner / Application Owner; Binding Clarification: Core Owner) | Phase 3 | Recorded; source validation pending; owner conflict unresolved |
| TBD-38 | Attempt Failure Classification | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | D04-Task-04, EVD-01 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-39 | Lifecycle Failed Evidence | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | T05, EVD-02, EVD-04 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-40 | Failure Event Recording Contract | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | D04-Task-04, EVD-05 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-41 | InProgress → Indeterminate Proof | BLOCKING | RECORDED — SOURCE VALIDATION PENDING | B3-R09, D8, EVD-05 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-42 | Compensation Failure Contract | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | D04-Task-04, EVD-03 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-43 | Compensation Indeterminate Resolution | BLOCKING | RECORDED — SOURCE VALIDATION PENDING | D03-T14, B3-R09 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-44 | External Effect State Evidence | BLOCKING | RECORDED — SOURCE VALIDATION PENDING | D03-T08, EVD-04 | Application/Network Owner | Phase 3 | Recorded; source validation pending |
| TBD-45 | Failure Safety — do not delete an InProgress record during failure processing | BLOCKING | RECORDED — SOURCE VALIDATION PENDING | D8, ADR-013 | MULTIPLE OWNERS — see CONFLICT-02; primary owner / responsibility split TBD | Phase 3 | Recorded; source validation pending; multiple-owner responsibility unresolved |
| TBD-46 | Restart State Recovery | OPEN | RECORDED — SOURCE VALIDATION PENDING | ADR-015, D8 | Application Owner | Phase 3 | Recorded; source validation pending |
| TBD-47 | Failed Retry Policy | REQUIRED | RECORDED — SOURCE VALIDATION PENDING | T05, ADR-015 | Design Owner / Product Owner | Phase 2 | Recorded; source validation pending |
| TBD-48 | Level 1 Resolution Authority | REQUIRED | RECORDED — SOURCE VALIDATION PENDING | B3-R09, ADB-06 | Design Owner / Product Owner | Phase 2 | Recorded; source validation pending |
| TBD-49 | Level 2 Resolution Contract | REQUIRED | RECORDED — SOURCE VALIDATION PENDING | D03-T14, ADR-D03-02 | Design Owner | Phase 2 | Recorded; source validation pending |
| TBD-50 | Compensation Completion Evidence | CONDITIONAL | RECORDED — SOURCE VALIDATION PENDING | D04-Task-06, EVD-03, EVD-04 | QA Owner / Design Owner | Phase 3 | Recorded; source validation pending |
| TBD-51 | Failure Classification After Handoff | OPEN | RECORDED — SOURCE VALIDATION PENDING | D05, EVD-02, EVD-04 | Application/Network Owner | Phase 2 | Recorded; source validation pending |
| TBD-52 | Safe Retry After Restart | BLOCKING | RECORDED — SOURCE VALIDATION PENDING | ADR-015, Restart Durability | Application Owner / Core Owner | Phase 3 | Recorded; source validation pending |
| TBD-53 | Source Requirement Mapping for all 54 IDs | REQUIRED | REGISTERED — SOURCE VALIDATION PENDING | D04-Task-06, original ADRs, requirements | Design Owner | Phase 2 | Pending |
| TBD-54 | Evidence Schema and Provenance Contract | REQUIRED | REGISTERED — SOURCE VALIDATION PENDING | XCC-EVI-01…03, EVD-01…06 | Design Owner / QA Owner | Phase 2 | Pending |
| TBD-55 | External Reconciliation Contract | BLOCKING | REGISTERED — SOURCE VALIDATION PENDING | D03-T08, EVD-04, B3-R09 | Application/Network Owner | Phase 2 | Pending |
| TBD-56 | Recovery Identity Contract | REQUIRED | REGISTERED — SOURCE VALIDATION PENDING | D03-T14, ADR-D03-02, ADR-015 | Design Owner | Phase 2 | Pending |
| TBD-57 | Recovery Authority Contract | REQUIRED | REGISTERED — SOURCE VALIDATION PENDING | D03-T14, B3-R09, ADR-D03-02 | Product Owner / Design Owner | Phase 2 | Pending |
| TBD-58 | Trusted External Effect Evidence Source | BLOCKING | REGISTERED — SOURCE VALIDATION PENDING | D03-T08, EVD-04, XCC-EFF-03 | Application/Network Owner | Phase 3 | Pending |

### Binding clarifications for TBD-01…58

**TBD-01…20:** all remain `PENDING SOURCE`. A semantic clarification made during discussion does not substitute for matching the record to its original source.

**TBD-21…52:** recorded; source validation pending. Preserve the table-level status separately from the register state.

**TBD-37 and TBD-45 are separate requirements:**

| Item | Meaning | Source task | Owner | Status | Reference |
|---|---|---|---|---|---|
| TBD-37 | Record Preservation: do not delete the Compensation Record while Compensation is in progress | D04-Task-03 | Core Owner | BLOCKING | D8, ADR-013 |
| TBD-45 | Failure Safety: do not delete an InProgress record during failure processing | D04-Task-04 | Core Owner / Application Owner | BLOCKING | D8, ADR-013 |

Cross-reference: `TBD-37 ↔ TBD-45`. Evidence for one does not establish the other.

**TBD-49 is a prerequisite contract for TBD-34:** `D03-T14 + ADR-D03-02 → TBD-49 → TBD-34`. ADR-D03-02 remains PROPOSED in the recorded design context.

**TBD-53 is a process, not a single unresolved requirement:** `Source Requirement Mapping` contains 54 sub-tasks (`TBD-53-01…TBD-53-54`). Each sub-task must map one criterion/constraint to its specific original requirement. The 54 individual mappings were not supplied in the source export; they are not invented here. Parent TBD-53 remains open until all 54 mappings are matched.

**TBD-55 ⇄ TBD-58 are a mutual design dependency:** do not impose a one-way prerequisite. The external reconciliation contract and trusted external-effect evidence source can be designed in parallel, but neither should be independently declared complete while the other remains unresolved where the relevant external-reconciliation path depends on both.

## 3. Register: TBD-59…85

| ID | Topic | Originating charter | Charter status in source | Register status after approval | Owner | Target phase | Source verification |
|---|---|---|---|---|---|---|---|
| TBD-59 | Transport Auth Contract | D05-A | REQUIRED in D05-A charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-60 | Connection Ownership Contract | D05-A | REQUIRED in D05-A charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-61 | Rate Limiting Policy | D05-A | REQUIRED in D05-A charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-62 | Payload Validation Policy | D05-A | REQUIRED in D05-A charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-63 | Trust Boundary Data Flow | D05-A | REQUIRED in D05-A charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-64 | RequestId vs CommandId equality / translation | D05-A | REQUIRED in D05-A charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-65 | Correlation ID Contract | D05-B | REQUIRED in D05-B charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-66 | Structured Log Schema | D05-B | REQUIRED in D05-B charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-67 | Tracing Span Boundaries | D05-B | REQUIRED in D05-B charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-68 | Secret/PII Redaction Rules | D05-B | REQUIRED in D05-B charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-69 | Log Retention Policy | D05-B | REQUIRED in D05-B charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-70 | Monitoring Metrics Definitions | D05-B | REQUIRED in D05-B charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-71 | Performance Baseline Contract | D06 | REQUIRED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-72 | Performance Test Protocol | D06 | REQUIRED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-73 | Device Matrix | D06 | REQUIRED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-74 | Playtest Protocol | D06 | REQUIRED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-75 | Unity/Android/FishNet Test Plan | D06 | BLOCKED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-76 | Security Test Suite | D06 | REQUIRED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-77 | Golden Baseline Protocol | D06 | REQUIRED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-78 | Backup & Recovery Plan | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-79 | Build & Release Pipeline | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-80 | Monitoring Plan | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-81 | Acceptance Criteria Framework | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-82 | Roadmap Governance | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-83 | MVP Gates | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-84 | README Roadmap Link | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |
| TBD-85 | Vote Cycle Contract | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | PENDING |

### Extension interpretation

- **TBD-59…64:** added to the register by User turn 376 as `REGISTERED — UNRESOLVED`; origin D05-A.
- **TBD-65…70:** added in the same approval event; origin D05-B.
- **TBD-71…77:** added by User turn 380 as `REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING`; origin D06.
- **TBD-78…85:** added by User turn 384 as `REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING`; origin D07.
- For TBD-59…85 the export supports the topic and charter association, but does not consistently provide a per-item owner, target phase, or full dependency list. Such fields are explicitly left TBD rather than inferred.

## 4. Cross-reference rules

| Relationship | Binding interpretation |
|---|---|
| TBD-37 ↔ TBD-45 | Distinct record-preservation and failure-safety requirements; mutual cross-reference only |
| TBD-49 → TBD-34 | Contract prerequisite precedes recovery-specific application |
| TBD-53-01…54 | 54 mapping sub-tasks; names/mappings not available in source |
| TBD-55 ⇄ TBD-58 | Mutual design dependency; not one-way |
| D05-C / D05-D / D05-E | Referrals, not independent D05 workstreams |

## 5. Conflict Register

| Conflict ID | Affected TBD | Recorded evidence | Classification | Resolution |
|---|---|---|---|---|
| CONFLICT-01 | TBD-37 — Record Preservation | Main TBD-21…58 table: `Core Owner / Application Owner`; Binding Clarifications table: `Core Owner`. | OPEN — SOURCE VERIFICATION REQUIRED | NOT DETERMINED. Preserve both source values; do not select an owner without source verification. |
| CONFLICT-02 | TBD-45 — Failure Safety | Owner is `Core Owner / Application Owner` in both recorded locations. | OPEN — MULTIPLE OWNERS | TBD. The source consistently lists multiple owners, but does not establish primary ownership or responsibility split. |

## 6. Completeness and unresolved source data

- 85 items are registered in this reconstruction.
- TBD-01…20 remain `PENDING SOURCE`.
- TBD-21…58 retain recorded/registered states with source verification pending.
- TBD-59…85 are registered unresolved; source verification pending.
- The full source mapping for TBD-53's 54 sub-tasks is not present.
- Some details for TBD-59…85 (owners, target phases, and detailed dependencies) are absent from the approved source messages.
- No item is marked RESOLVED solely because it appears here.

## 7. Version and approval traceability

| Version / change | Source event | Approval meaning | Calendar date |
|---|---|---|---|
| Initial 58-item register and admin-closure rules | Assistant turn 363; accepted in User turn 364 | Register accepted; 01…20 pending source; 21…52 recorded; 53…58 registered; closure administrative only | NOT PRESENT IN EXPORT |
| Relationship and dependency corrections | User turn 368 | TBD-37/45 distinct; TBD-49 prerequisite to TBD-34; TBD-53 process; TBD-55 ⇄ TBD-58 mutual | NOT PRESENT IN EXPORT |
| D05 extensions 59…70 | User turns 374/376 | 59…70 registered unresolved | NOT PRESENT IN EXPORT |
| D06 extensions 71…77 | User turn 380 | 71…77 registered unresolved; source verification pending | NOT PRESENT IN EXPORT |
| D07 extensions 78…85 | User turn 384 | 78…85 registered unresolved; source verification pending | NOT PRESENT IN EXPORT |

---

**Status honesty:** `PARTIAL`. The register is recoverable, but source matching is not complete. This is not evidence that the 85 items are resolved or that Phase 2 is closed.

---