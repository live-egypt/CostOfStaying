> **Repository copy provenance (2026-11-09):** Imported from the extracted text of Project Library record `SOURCE_TRACEABILITY_REGISTER.md` (file ID `file_00000000b7cc82108d6e616fd70b238e`). The Library source record was not modified. This copy preserves the extracted text; raw-byte equality with the Library payload is not asserted.  
> **Status boundary:** Content is source-recorded. It does not imply that requirements are resolved, decisions are approved, or implementation is verified.

# Stage 1 — Source Traceability Register

> **Scope:** TBD-37, TBD-45, TBD-59…85. Local review artifact only.
> **Evidence basis:** `/mnt/data/محادثة 2.html` and the exact reconstructed register/charter text embedded in `FULL_DOCUMENTS_REVIEW.md`.
> **Date policy:** Individual source-message calendar dates are `NOT PRESENT IN EXPORT`; export-level timestamp is not substituted for message dates.

## 1. Interpretation rules

- `REGISTERED` / `RECORDED` is not `SOURCE VERIFIED` and is not `RESOLVED`.
- A source turn records an approval or registration event; it does not necessarily provide every field for each item.
- `TBD` means the field is not specified in the approved source record.
- `SOURCE NOT FOUND` is used only when the searched export does not contain a supporting source; `SOURCE VERIFICATION PENDING` means a record exists but has not been matched to its original requirement.
- No source date is inferred.

## 2. TBD-37 and TBD-45 — owner traceability

| Field | TBD-37 | TBD-45 |
|---|---|---|
| Topic | Record Preservation — retain the Compensation Record while Compensation is in progress | Failure Safety — do not delete an InProgress record during failure processing |
| Source status | BLOCKING | BLOCKING |
| Registry status | RECORDED — SOURCE VALIDATION PENDING | RECORDED — SOURCE VALIDATION PENDING |
| Dependencies | D8, ADR-013, ADB-01 | D8, ADR-013 |
| Owner evidence — main register | Core Owner / Application Owner | Core Owner / Application Owner |
| Owner evidence — Binding Clarifications | Core Owner | Core Owner / Application Owner |
| Current owner field | CONFLICT — CONFLICT-01 | MULTIPLE OWNERS — CONFLICT-02 |
| Resolution | NOT DETERMINED | TBD — primary owner / responsibility split not specified |
| Source event | User turn 368 | User turn 368 |
| Calendar date | NOT PRESENT IN EXPORT | NOT PRESENT IN EXPORT |

**Interpretation:** The requirements are distinct and cross-reference each other. Evidence for one does not establish the other. No owner is selected in this stage.

## 3. TBD-59…85 — item-level traceability

| ID | Topic | Origin charter | Charter state recorded | Register state | Owner | Target phase | Dependencies | Source event | Source verification |
|---|---|---|---|---|---|---|---|---|---|
| TBD-59 | Transport Auth Contract | D05-A | REQUIRED in D05-A charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 376 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-60 | Connection Ownership Contract | D05-A | REQUIRED in D05-A charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 376 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-61 | Rate Limiting Policy | D05-A | REQUIRED in D05-A charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 376 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-62 | Payload Validation Policy | D05-A | REQUIRED in D05-A charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 376 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-63 | Trust Boundary Data Flow | D05-A | REQUIRED in D05-A charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 376 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-64 | RequestId vs CommandId equality / translation | D05-A | REQUIRED in D05-A charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 376 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-65 | Correlation ID Contract | D05-B | REQUIRED in D05-B charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 376 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-66 | Structured Log Schema | D05-B | REQUIRED in D05-B charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 376 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-67 | Tracing Span Boundaries | D05-B | REQUIRED in D05-B charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 376 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-68 | Secret/PII Redaction Rules | D05-B | REQUIRED in D05-B charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 376 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-69 | Log Retention Policy | D05-B | REQUIRED in D05-B charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 376 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-70 | Monitoring Metrics Definitions | D05-B | REQUIRED in D05-B charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 376 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-71 | Performance Baseline Contract | D06 | REQUIRED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 380 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-72 | Performance Test Protocol | D06 | REQUIRED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 380 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-73 | Device Matrix | D06 | REQUIRED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 380 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-74 | Playtest Protocol | D06 | REQUIRED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 380 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-75 | Unity/Android/FishNet Test Plan | D06 | BLOCKED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 380 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-76 | Security Test Suite | D06 | REQUIRED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 380 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-77 | Golden Baseline Protocol | D06 | REQUIRED in D06 charter | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 380 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-78 | Backup & Recovery Plan | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 384 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-79 | Build & Release Pipeline | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 384 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-80 | Monitoring Plan | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 384 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-81 | Acceptance Criteria Framework | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 384 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-82 | Roadmap Governance | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 384 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-83 | MVP Gates | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 384 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-84 | README Roadmap Link | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 384 | PENDING — SOURCE VERIFICATION PENDING |
| TBD-85 | Vote Cycle Contract | D07 | PROPOSED in D07 charter before registration approval | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | TBD — not specified in approval record | TBD — not specified in approval record | TBD — not specified in approval record | User turn 384 | PENDING — SOURCE VERIFICATION PENDING |

### 3.1 Registration event mapping

| Range | Source event | What the source supports | What remains unknown |
|---|---|---|---|
| TBD-59…64 | User turn 376 | Six named D05-A items; registered unresolved; D05-A association | Per-item owner, target phase, detailed dependencies; original requirement-level source match |
| TBD-65…70 | User turn 376 | Six named D05-B items; registered unresolved; D05-B association | Per-item owner, target phase, detailed dependencies; original requirement-level source match |
| TBD-71…77 | User turn 380 | Seven named D06 items; registered unresolved; D06 association | Per-item owner, target phase, detailed dependencies; original requirement-level source match |
| TBD-78…85 | User turn 384 | Eight named D07 items; registered unresolved; D07 association | Per-item owner, target phase, detailed dependencies; original requirement-level source match |

**Special recorded state:** TBD-75 has `BLOCKED in D06 charter` as its charter state, while its register state remains `REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING`. These are separate fields and must not be collapsed.

## 4. Source verification status

- TBD-37: source record exists; owner discrepancy remains open. `SOURCE VERIFICATION PENDING`.
- TBD-45: source record exists; multiple-owner responsibility remains unresolved. `SOURCE VERIFICATION PENDING`.
- TBD-59…85: registration/charter records exist; original requirement-level source matching remains pending.
- No item in this scope is marked `RESOLVED` or `VERIFIED`.
- No per-message calendar dates were present in the export.

## 5. Search limitations and missing evidence

- The export identifies approval turns and contains the charter/register content summarized above.
- The export does not consistently map each new TBD to a unique original requirement statement or provide complete owner/phase/dependency fields.
- For any item where the original requirement text cannot be located or uniquely linked, final disposition remains `SOURCE NOT FOUND` or `SOURCE VERIFICATION PENDING`; no semantic reconstruction is substituted.

## 6. Reproducibility

- Source export: `محادثة 2.html`.
- Review snapshot: `FULL_DOCUMENTS_REVIEW.md`, section `docs/governance/TBD_REGISTER.md` and embedded D05-A/D05-B/D06/D07 charters.
- Extraction method: exact register rows for TBD-59…85; approval event mapping from User turns 376, 380, and 384.
- The source traceability artifact was prepared as a local Stage 1 record. A text-extracted repository copy was added on 2026-11-09; the original Project Library record remains unchanged, and raw-byte equality is not asserted.