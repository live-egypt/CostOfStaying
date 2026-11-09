> **Repository copy provenance (2026-11-09):** Imported from the extracted text of Project Library record `CONSISTENCY_MATRIX.md` (file ID `file_0000000062c48210958b0129524e29f3`). The Library source record was not modified. This copy preserves the extracted text; raw-byte equality with the Library payload is not asserted.  
> **Status boundary:** Content is source-recorded. It does not imply that requirements are resolved, decisions are approved, or implementation is verified.

# Consistency Matrix — Revision 2 RECONCILED

> **STATUS: RECONCILED — 109 RECORDS / 30 GROUPS / 24 PENDING SOURCE; PRIMARY GROUP COMPLETENESS PENDING**
>
> **Source:** full 109-row table in User turn 352; corrected aggregate totals, pending-source IDs, cross-reference rules, and withdrawal of Revision 2 — AUDITED reaffirmed in User turn 356. Revision 2 — AUDITED is withdrawn and must not be used.
> **Calendar approval date:** NOT PRESENT IN EXPORT.

## 1. Scope and interpretation

- This document contains the full 109 individual records, not only aggregate totals.
- `Original Classification`, `Current Status`, `Relationship`, `Priority`, and `Primary Group` are separate dimensions.
- `PENDING SOURCE` is a status value; it does not mean that the other fields are all unknown.
- `Primary Group` completeness is still PENDING. `SYS-04` has no resolved primary group.
- `SEC-07` and `ROOT-02` reference `CA-04` only; they are not members of that primary group.
- `RMP-03-F03` remains STANDALONE; `CA-10` is only a cross-reference.

## 2. Corrected aggregate totals

### 2.1 Original Classification

| Classification | A | B | C | Total |
|---|---:|---:|---:|---:|
| PROVEN | 24 | 9 | 16 | 49 |
| CONDITIONAL | 4 | 5 | 2 | 11 |
| PROPOSED | 0 | 18 | 20 | 38 |
| TBD | 1 | 6 | 3 | 10 |
| UNKNOWN | 1 | 0 | 0 | 1 |
| **Total** | **30** | **38** | **41** | **109** |

### 2.2 Current Status

| Status | A | B | C | Total |
|---|---:|---:|---:|---:|
| OPEN | 19 | 20 | 35 | 74 |
| RESOLVED | 7 | 0 | 0 | 7 |
| DEFERRED | 1 | 0 | 0 | 1 |
| STALE | 3 | 0 | 0 | 3 |
| PENDING SOURCE | 0 | 18 | 6 | 24 |
| **Total** | **30** | **38** | **41** | **109** |

### 2.3 Relationship

| Relationship | A | B | C | Total |
|---|---:|---:|---:|---:|
| CONSOLIDATED | 27 | 29 | 40 | 96 |
| STANDALONE | 3 | 9 | 1 | 13 |
| **Total** | **30** | **38** | **41** | **109** |

### 2.4 Priority

| Priority | A | B | C | Total |
|---|---:|---:|---:|---:|
| BLOCKING | 2 | 3 | 4 | 9 |
| HIGH | 16 | 21 | 24 | 61 |
| MEDIUM | 7 | 13 | 12 | 32 |
| LOW | 5 | 1 | 1 | 7 |
| **Total** | **30** | **38** | **41** | **109** |

## 3. All 109 individual records

| # | ID | Original classification | Current status | Relationship | Priority | Primary group / cross-reference |
|---:|---|---|---|---|---|---|
| 1 | ARCH-01 | PROVEN | OPEN | CONSOLIDATED | MEDIUM | CA-07 |
| 2 | ARCH-02 | CONDITIONAL | OPEN | CONSOLIDATED | HIGH | CA-01 |
| 3 | ARCH-03 | PROVEN | OPEN | CONSOLIDATED | HIGH | CA-06 |
| 4 | ARCH-04 | CONDITIONAL | OPEN | CONSOLIDATED | HIGH | CA-02 |
| 5 | ARCH-05 | CONDITIONAL | OPEN | CONSOLIDATED | HIGH | CA-04 |
| 6 | ARCH-06 | PROVEN | OPEN | CONSOLIDATED | HIGH | CA-06 |
| 7 | ARCH-07 | TBD | OPEN | STANDALONE | LOW | — |
| 8 | ARCH-08 | UNKNOWN | OPEN | CONSOLIDATED | HIGH | CA-11 |
| 9 | ARCH-09 | PROVEN | OPEN | CONSOLIDATED | HIGH | CA-06 |
| 10 | ARCH-10 | PROVEN | OPEN | CONSOLIDATED | MEDIUM | CA-05 |
| 11 | GOV-01 | PROVEN | RESOLVED | STANDALONE | LOW | — |
| 12 | GOV-02 | PROVEN | RESOLVED | CONSOLIDATED | HIGH | CA-09 |
| 13 | GOV-03 | PROVEN | OPEN | CONSOLIDATED | MEDIUM | CA-05 |
| 14 | GOV-04 | CONDITIONAL | OPEN | CONSOLIDATED | HIGH | CA-01 |
| 15 | GOV-05 | PROVEN | DEFERRED | CONSOLIDATED | HIGH | CA-07 |
| 16 | GOV-06 | PROVEN | OPEN | CONSOLIDATED | HIGH | CA-02 |
| 17 | GOV-07 | PROVEN | OPEN | CONSOLIDATED | HIGH | CA-04 |
| 18 | GOV-08 | PROVEN | OPEN | CONSOLIDATED | BLOCKING | CA-04 |
| 19 | GOV-09 | PROVEN | OPEN | CONSOLIDATED | MEDIUM | CA-08 |
| 20 | GOV-10 | PROVEN | OPEN | CONSOLIDATED | HIGH | CA-05 |
| 21 | AUD-01 | PROVEN | RESOLVED | STANDALONE | LOW | — |
| 22 | AUD-02 | PROVEN | RESOLVED | CONSOLIDATED | HIGH | CA-06 |
| 23 | AUD-03 | PROVEN | OPEN | CONSOLIDATED | BLOCKING | CB-06 |
| 24 | AUD-04 | PROVEN | OPEN | CONSOLIDATED | HIGH | CA-03 |
| 25 | AUD-05 | PROVEN | RESOLVED | CONSOLIDATED | LOW | CA-02 |
| 26 | AUD-06 | PROVEN | RESOLVED | CONSOLIDATED | LOW | CA-02 |
| 27 | AUD-07 | PROVEN | RESOLVED | CONSOLIDATED | HIGH | CA-02 |
| 28 | AUD-08 | PROVEN | STALE | CONSOLIDATED | MEDIUM | CA-02 |
| 29 | AUD-09 | PROVEN | STALE | CONSOLIDATED | MEDIUM | CA-02 |
| 30 | AUD-10 | PROVEN | STALE | CONSOLIDATED | MEDIUM | CA-02 |
| 31 | SYS-01 | PROPOSED | OPEN | STANDALONE | MEDIUM | — |
| 32 | SYS-02 | PROPOSED | OPEN | STANDALONE | MEDIUM | — |
| 33 | SYS-03 | PROPOSED | OPEN | STANDALONE | HIGH | — |
| 34 | SYS-04 | PROPOSED | PENDING SOURCE | CONSOLIDATED | HIGH | PENDING — PRIMARY GROUP NOT ESTABLISHED |
| 35 | SYS-05 | PROPOSED | OPEN | STANDALONE | MEDIUM | — |
| 36 | SYS-06-F01 | PROPOSED | OPEN | CONSOLIDATED | BLOCKING | CB-02 |
| 37 | SYS-06-F02 | CONDITIONAL | OPEN | CONSOLIDATED | HIGH | CA-04 |
| 38 | SYS-06-F03 | PROPOSED | OPEN | CONSOLIDATED | HIGH | CA-04 |
| 39 | SYS-09 | CONDITIONAL | OPEN | CONSOLIDATED | HIGH | CB-02 |
| 40 | SYS-10 | PROVEN | OPEN | STANDALONE | LOW | — |
| 41 | SEC-01 | PROPOSED | PENDING SOURCE | STANDALONE | MEDIUM | — |
| 42 | SEC-02-F01 | PROPOSED | PENDING SOURCE | CONSOLIDATED | MEDIUM | CB-12 |
| 43 | SEC-02-F02 | PROVEN | PENDING SOURCE | CONSOLIDATED | HIGH | CB-12 |
| 44 | SEC-03-F01 | PROPOSED | PENDING SOURCE | CONSOLIDATED | MEDIUM | CB-11 |
| 45 | SEC-03-F02 | TBD | PENDING SOURCE | CONSOLIDATED | HIGH | CB-11 |
| 46 | SEC-04-F01 | PROVEN | PENDING SOURCE | CONSOLIDATED | HIGH | CB-03 |
| 47 | SEC-04-F02 | PROPOSED | PENDING SOURCE | CONSOLIDATED | MEDIUM | CB-11 |
| 48 | SEC-05-F01 | PROPOSED | PENDING SOURCE | STANDALONE | MEDIUM | CB-07 |
| 49 | SEC-05-F02 | CONDITIONAL | PENDING SOURCE | CONSOLIDATED | HIGH | CA-04 |
| 50 | SEC-06-F01 | PROPOSED | PENDING SOURCE | CONSOLIDATED | MEDIUM | CB-08 |
| 51 | SEC-06-F02 | CONDITIONAL | PENDING SOURCE | CONSOLIDATED | HIGH | CB-08 |
| 52 | SEC-07 | PROPOSED | PENDING SOURCE | CONSOLIDATED | HIGH | — (CROSS-REFERENCE: CA-04; NOT PRIMARY MEMBERSHIP) |
| 53 | SEC-08 | TBD | PENDING SOURCE | CONSOLIDATED | HIGH | CB-03 |
| 54 | SEC-09 | TBD | PENDING SOURCE | CONSOLIDATED | HIGH | CB-03 |
| 55 | SEC-10 | PROVEN | OPEN | CONSOLIDATED | BLOCKING | CB-06 |
| 56 | QLT-01 | PROPOSED | PENDING SOURCE | STANDALONE | MEDIUM | — |
| 57 | QLT-02-F01 | PROVEN | OPEN | CONSOLIDATED | HIGH | CB-04 |
| 58 | QLT-03-F01 | PROPOSED | OPEN | CONSOLIDATED | HIGH | CB-04 |
| 59 | QLT-04-F01 | PROVEN | OPEN | CONSOLIDATED | MEDIUM | CB-09 |
| 60 | QLT-04-F02 | PROPOSED | OPEN | CONSOLIDATED | MEDIUM | CB-09 |
| 61 | QLT-04-F03 | TBD | OPEN | CONSOLIDATED | MEDIUM | CB-09 |
| 62 | QLT-05-F01 | PROPOSED | OPEN | STANDALONE | HIGH | CB-10 |
| 63 | QLT-05-F02 | CONDITIONAL | OPEN | CONSOLIDATED | HIGH | CA-04 |
| 64 | QLT-06-F01 | PROVEN | OPEN | CONSOLIDATED | HIGH | CA-06 |
| 65 | QLT-07 | TBD | PENDING SOURCE | CONSOLIDATED | HIGH | CB-05 |
| 66 | QLT-08 | TBD | PENDING SOURCE | CONSOLIDATED | HIGH | CB-05 |
| 67 | QLT-09 | PROVEN | OPEN | CONSOLIDATED | HIGH | CB-04 |
| 68 | QLT-10 | PROVEN | OPEN | CONSOLIDATED | BLOCKING | CB-06 |
| 69 | OPS-01 | PROPOSED | OPEN | CONSOLIDATED | MEDIUM | CC-01 |
| 70 | OPS-02 | TBD | OPEN | CONSOLIDATED | HIGH | CC-01 |
| 71 | OPS-03 | PROPOSED | OPEN | CONSOLIDATED | HIGH | CC-01 |
| 72 | OPS-04 | PROVEN | OPEN | CONSOLIDATED | HIGH | CC-01 |
| 73 | OPS-05 | PROVEN | PENDING SOURCE | CONSOLIDATED | HIGH | CC-02 |
| 74 | OPS-06 | TBD | PENDING SOURCE | CONSOLIDATED | HIGH | CC-02 |
| 75 | OPS-07 | PROVEN | OPEN | CONSOLIDATED | HIGH | CC-02 |
| 76 | OPS-08 | PROPOSED | PENDING SOURCE | CONSOLIDATED | HIGH | CC-02 |
| 77 | OPS-09 | PROPOSED | PENDING SOURCE | CONSOLIDATED | HIGH | CC-02 |
| 78 | OPS-10 | PROPOSED | OPEN | CONSOLIDATED | MEDIUM | CC-03 |
| 79 | OPS-11 | CONDITIONAL | OPEN | CONSOLIDATED | HIGH | CC-03 |
| 80 | OPS-12 | PROPOSED | OPEN | CONSOLIDATED | MEDIUM | CC-03 |
| 81 | OPS-13 | PROPOSED | OPEN | CONSOLIDATED | MEDIUM | CC-03 |
| 82 | OPS-14 | PROVEN | OPEN | CONSOLIDATED | HIGH | CC-03 |
| 83 | RMP-01-F01 | PROVEN | OPEN | CONSOLIDATED | HIGH | CA-09 |
| 84 | RMP-01-F02 | PROVEN | OPEN | CONSOLIDATED | BLOCKING | CB-02 |
| 85 | RMP-01-F03 | PROPOSED | OPEN | CONSOLIDATED | MEDIUM | CC-04 |
| 86 | RMP-01-F04 | PROPOSED | OPEN | CONSOLIDATED | HIGH | CC-04 |
| 87 | RMP-01-F05 | PROPOSED | OPEN | CONSOLIDATED | HIGH | CB-04 |
| 88 | RMP-01-F06 | PROPOSED | OPEN | CONSOLIDATED | MEDIUM | CC-04 |
| 89 | RMP-01-F07 | PROVEN | OPEN | CONSOLIDATED | HIGH | CA-02 |
| 90 | RMP-02-F01 | PROVEN | OPEN | CONSOLIDATED | HIGH | CC-05 |
| 91 | RMP-02-F02 | CONDITIONAL | OPEN | CONSOLIDATED | HIGH | CC-05 |
| 92 | RMP-02-F03 | PROPOSED | OPEN | CONSOLIDATED | HIGH | CC-04 |
| 93 | RMP-02-F04 | PROVEN | OPEN | CONSOLIDATED | BLOCKING | CB-06 |
| 94 | RMP-02-F05 | PROPOSED | OPEN | CONSOLIDATED | MEDIUM | CB-04 |
| 95 | RMP-02-F06 | PROPOSED | OPEN | CONSOLIDATED | HIGH | CC-05 |
| 96 | RMP-03-F01 | PROPOSED | OPEN | CONSOLIDATED | MEDIUM | CC-06 |
| 97 | RMP-03-F02 | PROVEN | OPEN | CONSOLIDATED | BLOCKING | CB-02 |
| 98 | RMP-03-F03 | TBD | OPEN | STANDALONE | MEDIUM | — (STANDALONE; CROSS-REFERENCE: CA-10) |
| 99 | RMP-03-F04 | PROPOSED | OPEN | CONSOLIDATED | HIGH | CC-06 |
| 100 | RMP-03-F05 | PROPOSED | OPEN | CONSOLIDATED | HIGH | CC-06 |
| 101 | RMP-03-F06 | PROPOSED | OPEN | CONSOLIDATED | MEDIUM | CC-06 |
| 102 | ROOT-01 | PROVEN | OPEN | CONSOLIDATED | HIGH | CA-09 |
| 103 | ROOT-02 | PROVEN | OPEN | CONSOLIDATED | HIGH | — (CROSS-REFERENCE: CA-04; NOT PRIMARY MEMBERSHIP) |
| 104 | ROOT-03 | PROVEN | PENDING SOURCE | CONSOLIDATED | HIGH | CA-02 |
| 105 | ROOT-04 | PROVEN | PENDING SOURCE | CONSOLIDATED | LOW | CA-02 |
| 106 | ROOT-05 | PROVEN | OPEN | CONSOLIDATED | MEDIUM | CC-07 |
| 107 | ROOT-06 | PROPOSED | OPEN | CONSOLIDATED | MEDIUM | CC-08 |
| 108 | ROOT-07 | PROVEN | OPEN | CONSOLIDATED | BLOCKING | CB-02 |
| 109 | ROOT-08 | PROPOSED | OPEN | CONSOLIDATED | HIGH | CC-05 |

## 4. Explicit PENDING SOURCE record IDs

**Group B — 18 records:** SYS-04, SEC-01, SEC-02-F01, SEC-02-F02, SEC-03-F01, SEC-03-F02, SEC-04-F01, SEC-04-F02, SEC-05-F01, SEC-05-F02, SEC-06-F01, SEC-06-F02, SEC-07, SEC-08, SEC-09, QLT-01, QLT-07, QLT-08.

**Group C — 6 records:** OPS-05, OPS-06, OPS-08, OPS-09, ROOT-03, ROOT-04.

## 5. Group catalogue and primary-membership limitation

Recorded group identifier catalogue (30 identifiers):

- CA: `CA-01`, `CA-02`, `CA-03`, `CA-04`, `CA-05`, `CA-06`, `CA-07`, `CA-08`, `CA-09`, `CA-10`, `CA-11`
- CB: `CB-02`, `CB-03`, `CB-04`, `CB-05`, `CB-06`, `CB-07`, `CB-08`, `CB-09`, `CB-10`, `CB-11`, `CB-12`
- CC: `CC-01`, `CC-02`, `CC-03`, `CC-04`, `CC-05`, `CC-06`, `CC-07`, `CC-08`

**Primary Group PENDING:** the source record did not establish SYS-04's primary group; the literal `CB-??` shown in the earlier row was not a valid group assignment and is replaced by `PENDING`.

**Cross-reference only:** `SEC-07 → CA-04` and `ROOT-02 → CA-04`; they are not primary membership. `RMP-03-F03` is standalone with `CA-10` as a cross-reference only.

## 6. Verified arithmetic checks

| Dimension | Recount from the 109 row entries | Corrected approved total | Result |
|---|---|---|---|
| Original classification | 49 / 11 / 38 / 10 / 1 | 49 / 11 / 38 / 10 / 1 | MATCH |
| Status | 74 / 7 / 1 / 3 / 24 | 74 / 7 / 1 / 3 / 24 | MATCH |
| Relationship | 96 / 13 | 96 / 13 | MATCH |
| Priority | 9 / 61 / 32 / 7 | 9 / 61 / 32 / 7 | MATCH |
| Group size | A=30, B=38, C=41 | A=30, B=38, C=41 | MATCH |
| Pending source split | B=18, C=6 | B=18, C=6 | MATCH |

## 7. ADR cross-reference and reconciliation status

| ADR | Relevant records / design area | Result | Notes |
|---|---|---|---|
| ADR-001 | SYS/QLT project stack assumptions | GAP | Several design records remain pending source; stack ADR itself is PROPOSED. |
| ADR-002 | SEC and trust-boundary findings | MATCH WITH NOTES | Server authority principle is approved; individual security-source matches remain pending. |
| ADR-003 | OPS hosting / operations | GAP | Hosting provider and production environment are not selected. |
| ADR-004 | Gameplay-related findings and playtest criteria | MATCH WITH NOTES | PROPOSED FOR PLAYTEST; does not resolve all gameplay assumptions. |
| ADR-005 | SYS / Core boundary findings | MATCH WITH NOTES | Core boundary decision approved; not every finding is thereby resolved. |
| ADR-006 | Identifier / ledger findings | MATCH WITH NOTES | ADR-006 is PROPOSED; record statuses remain independent. |
| ADR-007 | Player lifecycle findings | MATCH | `Alive / Spirit / Eliminated` is canonical; no competing `Dead` lifecycle may be inferred. |
| ADR-008 | Command envelope / sequence records | MATCH WITH NOTES | ADR-008 is PROPOSED; sequence and command identity are separate. |
| ADR-009 | Match-scoped registry records | GAP | Capacity / spectator / reconnect scope remains unresolved. |
| ADR-010 | Session lifecycle records | MATCH WITH NOTES | Reconnect, timeout, and multi-session policies remain TBD. |
| ADR-011 | Bounded session registry records | MATCH WITH NOTES | Does not resolve the separate B3 registry decision. |
| ADR-012 | Authenticated admission records | MATCH WITH NOTES | Admission gate is not the same as credentials or payload/rate policies. |
| ADR-013 | Cleanup / replay release records | MATCH WITH NOTES | Cleanup order does not establish in-flight handler completion. |
| ADR-014 | Victim scope | GAP | Victim categories remain TBD; no NPC or player-only rule is established. |
| ADR-015 | Idempotency / command identity records | MATCH WITH NOTES | ADR-015 remains PROPOSED; restart durability is not promised. |

## 8. Version traceability and withdrawal

| Version | Event | State |
|---|---|---|
| Revision 2 — original table | User turn 352 | Full row table provided; aggregate summary had arithmetic errors. |
| Revision 2 — RECONCILED | User turn 356 | Corrected aggregate totals approved; source verification limitations retained. |
| Revision 2 — AUDITED | User turn 356 | NOT APPROVED / withdrawn. Do not use as the current version. |

**Status honesty:** `RECONCILED` means the row set and aggregate arithmetic are aligned to the approved totals. It does not mean the 24 PENDING SOURCE records or Primary Group assignments are source-verified.