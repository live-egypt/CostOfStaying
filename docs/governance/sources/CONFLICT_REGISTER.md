# Conflict Register — Source & Governance Recovery

**Snapshot:** 2026-11-09  
**Status:** `OPEN REGISTER — NO CONFLICT CLOSED BY ASSUMPTION`  
**Companion:** [SOURCE_INVENTORY.md](SOURCE_INVENTORY.md), [PROVENANCE_REGISTER.md](PROVENANCE_REGISTER.md)

## 1. Handling rules

- Preserve every source value and its origin.
- Do not silently choose the newest, largest, or most detailed value.
- Distinguish a true contradiction from a different snapshot, branch, version, or scope.
- `NOT VERIFIED` does not mean `NOT IMPLEMENTED`.
- A conflict closes only when its resolution rule, source evidence, and owner acceptance are recorded.
- Keep original IDs. If two subjects share an ID, preserve both source occurrences and create a mapping proposal; do not overwrite either identity.

## 2. Open conflicts and reconciliation items

| ID | Subject | Source values / observations | Initial classification | Status | Required resolution evidence |
|---|---|---|---|---|---|
| SRC-001 | Project attachment counts | Earlier reports mention 5, 9, and 11 files; current `files.list(surface=conversation)` returns 15 distinct file records. | Likely different inventory snapshots / scopes, not yet a proven contradiction. | `OPEN` | Preserve timestamp, surface, exact IDs, and query result for each count; map the historical snapshots. |
| SRC-002 | Same-basename continuation reports | Four `2 استكمال المحادثة.txt` records have distinct file IDs, sizes, contents, and SHA-256 values. | Duplicate-name collision; content identity is distinct. | `RESOLVED — IDENTITY ONLY` | Do not merge; retain each file as separate source. |
| SRC-003 | ChatGPT HTML export mapping | `محادثة 2.html` is reported as a 449-message ChatGPT Shared Chat export; exact mapping to Project chat `2` is not established. | Source identity / export mapping gap. | `OPEN` | Full thread/export metadata or user confirmation tied to exact export identity. |
| SRC-004 | DeepSeek-origin conversations | User-attested project chats `1` and `2`; no full original transcript or authenticated DeepSeek export recovered. | User-attested identity with missing content. | `OPEN — SOURCE PENDING` | Full transcripts/thread IDs and origin evidence. |
| SRC-005 | Network Audit original | Network topics and reconciliation drafts exist; original complete `NETWORK_AUDIT.md` not present in current recovered material. | Derived material without complete original. | `OPEN — SOURCE PENDING` | Original document or complete source export. |
| SRC-006 | Security Audit original | `5.txt` is explicitly an initial reconciliation; full original `SECURITY_AUDIT.md` not recovered. | Partial source. | `OPEN — SOURCE PARTIAL` | Original complete audit and version history. |
| SRC-007 | Stage 0 Library provenance | A Library record named `STAGE_2_CLOSURE_DECLARATION.md` contains text saying it was not written to Library; related local-only records are also present in Library metadata. | Provenance contradiction; actor and causal path unknown. | `OPEN — CAUSAL ORIGIN UNRESOLVED` | Authorized raw bytes for exact records and an authenticated upload/audit event, if available. |
| GOV-001 | TBD-37 owner | Main TBD table reportedly lists `Core Owner / Application Owner`; Binding Clarifications reportedly lists `Core Owner`. | Conflicting owner values within the recovered register. | `OPEN — CONFLICT-01` | Original requirement source and owner decision. |
| GOV-002 | TBD-45 responsibility | Both cited locations reportedly list `Core Owner / Application Owner`; primary owner and split remain unresolved. | Responsibility allocation incomplete. | `OPEN — CONFLICT-02` | Explicit owner/decision record defining primary and supporting responsibilities. |
| GOV-003 | RISK-013 identity collision | Same ID used for Suffering Transfer Strategic Dominance and Suffering Transfer Abuse in different source material. | Identifier collision; potentially distinct risk identities. | `OPEN` | Original risk entries, source IDs, descriptions, and mapping approved by owner. |
| EVID-001 | Historical Core CI counts | Sources report 148/148, 177/177, 181/181, and 187/187 on different historical records. Examples: 148 on run `41331400978` / commit `94c5789b0e82493290ca85133fc692e83ffd9884`; 177 on run `41332755494` / commit `26a77dcf1394d35d3db48ea0f5b7cde4c118998f`; 187 on run `42383513646` / commit `2820285021b1a3d7ae9acd953b3bb65677c33512`. A 181 count is reported without a complete run/commit pair in the current source set. | Version drift across different commits/runs, not one result. | `OPEN — EVIDENCE MAPPING` | Run IDs, commit SHAs, workflow files, artifacts/TRX, and scope for every count; do not choose a maximum. |
| GOV-004 | Stage vs Phase numbering | Stage 0–5 and Phase 1–3 both appear in project records. | Separate lifecycle frameworks; risk of accidental conflation. | `OPEN — CONTROL RULE ESTABLISHED` | Maintain separate definitions and gate maps. |
| DOC-001 | Package structure / numbering | Prior proposals include 15-document, 21-document, hybrid A–G, and other groupings. | Package architecture not approved. | `OPEN — PROPOSED ONLY` | Complete document mapping and explicit user approval before freezing paths/numbering. |
| DOC-002 | Library and local-only status | Stage 1 outputs are described as local-only in earlier reports, while corresponding records now appear in Project Library metadata. | Provenance/control mismatch; do not infer uploader. | `OPEN` | Exact source bytes and an authenticated operation/audit trail. |
| REPO-001 | “No code in project” vs branch code | `main` and `feature/core-lifecycle-resolution` differ; PR #6 contains Core and tests while main snapshot is an earlier base. | Scope/branch confusion, not necessarily contradiction. | `OPEN — SCOPE CONTROL` | Always state branch, commit SHA, PR, and merge state with implementation claims. |

## 3. Non-conflicts that must remain distinct

- `TBD-37` and `TBD-45` are different requirements; do not collapse them.
- `TBD-49` is reported as a prerequisite for `TBD-34`.
- `TBD-53` is reported to require a 54-subtask mapping; the individual subtask labels are not available here.
- `TBD-55` and `TBD-58` are reported as mutually dependent.
- Reservation, Compensation, InFlight, and External Effects requirements remain a distinct technical/operations stream until their original source/domain mapping establishes otherwise.
- Sequence replay protection, command-result idempotency, reconnect protection, and restart protection are four separate guarantees.
- Connection identity, authenticated player identity, and session identity are not interchangeable.
- `Immutable After Capture` is not the same guarantee as `Atomic Snapshot Capture`.

## 4. Current closure rule

No item above is closed merely because it has been documented in this register. A closure entry must identify:

1. The exact conflict ID.
2. The selected source or resolution.
3. Why competing source values are retained or superseded.
4. Owner approval where the conflict is a product/design decision.
5. A reproducible evidence reference where the claim is technical.
