# PDR-009 — TBD Register Review

**Workstream:** A5 — Document Control  
**Status:** CANNOT RESOLVE — CANONICAL REGISTER NOT LOCATED  
**Document class:** Governance review / evidence assessment  
**Repository:** `live-egypt/CostOfStaying`  
**Base examined:** `main` at review time  
**Decision effect:** This review does not create, approve, renumber, close, or delete any TBD item.

## 1. Purpose and decision question

Determine whether the repository contains an identifiable, authoritative TBD register and whether its entries can be reconciled against the A4 governance baseline without inventing identifiers or silently changing their meaning.

## 2. Search scope and reproducible findings

The following repository paths and search terms were examined through the GitHub repository connector:

| Check | Result | Evidence / limitation |
|---|---|---|
| `docs/governance/REQUIREMENTS_TRACEABILITY.md` | FOUND | Blob SHA `204d02d19e2bd7fa23a749ec818cf26f7096b514`; status is PROPOSED and its rows are explicitly preliminary examples. |
| `docs/governance/DECISIONS_LOG.md` | FOUND | Blob SHA `8cb7e4f4d2d30436bdeb05eebb61cf55b9e4c59b`; records ADR-001 through ADR-004, but no TBD register. |
| `docs/README.md` | FOUND | Blob SHA `1a69115d9a984d2b8abbda9b9c268a0639ea6d61`; governance index links the decision log and requirements traceability only. |
| `docs/governance/TBD_REGISTER.md` | NOT FOUND | Contents API returned 404 on the examined default branch. |
| `docs/governance/A4/TBD_REGISTER.md` | NOT FOUND | Contents API returned 404 on the examined default branch. |
| `docs/governance/A4/REQUIREMENTS_TRACEABILITY_MATRIX.md` | NOT FOUND | Contents API returned 404 on the examined default branch. |
| Repository code/document search for `TBD-01`, `TBD-45`, `TBD Register`, and `CONFLICT-02` | NO MATCH | Search index returned no matching repository files. This is not proof that no historical or unindexed source exists. |
| Broad repository search for `TBD` | MATCHES | Returned general design/architecture documents with unresolved design values, not a structured item register. |

### 2.1 What the available documents establish

- The current requirements traceability document is marked PROPOSED; it describes its rows as preliminary examples and requires owner, priority, acceptance criteria, test procedure, environment, commit SHA, and retained evidence for full traceability.
- The decisions log does not identify a TBD register or enumerate TBD IDs.
- The document index does not link a TBD register.
- Individual system documents contain unresolved design values, but these are prose-level TBD mentions and cannot safely be converted into numbered governance items without an authoritative mapping.

### 2.2 Conversation-baseline references — not repository-verified

The A4 governance context available to this review identifies `TBD-37` and `TBD-45` as open references, with TBD-45 associated with unresolved responsibility allocation under CONFLICT-02. Those references are preserved here as reported governance context only. Their original records, exact wording, ownership, and complete register membership were not located in the examined repository files. They must not be treated as a complete or independently verified register.

## 3. Evidence assessment

| Required element | Finding | Status |
|---|---|---|
| Canonical register path and file | No canonical register located on examined `main` | MISSING |
| Complete item inventory | Not available | MISSING |
| Stable TBD identifiers and exact wording | Only isolated mentions / contextual references found | INSUFFICIENT |
| Per-item owner and priority | Not established by a register | MISSING |
| Origin / source reference per item | Not established systematically | MISSING |
| Dependency and conflict mapping | Not available as a complete register | MISSING |
| Status history and closure authority | Not established | MISSING |
| Evidence supporting closure of individual items | Not established | MISSING |

## 4. Determination

**PDR-009 = CANNOT RESOLVE — CANONICAL REGISTER AND COMPLETE ITEM EVIDENCE NOT LOCATED.**

This is an evidence limitation, not a finding that the TBD items do not exist, were never recorded, or are invalid. No item is closed, deleted, superseded, renumbered, or reclassified by this review.

The A4.6 source record supplied in the project files explicitly reports that TBD-01…TBD-85 were historically recorded. This establishes a source-derived historical count/range claim, not that all 85 entries are currently present in the repository, complete, correctly numbered, source-verified, or approved. The original register and row-level source mapping remain unrecovered in the examined repository evidence. Preserve the historical 85-item claim as RECORDED — NOT INDEPENDENTLY RECONCILED.

## 5. Required recovery and reconciliation

1. Recover the original TBD register from the authoritative A4 source package or historical conversation/source artifact.
2. Preserve the recovered source unchanged and record its provenance, version/date, and source location.
3. Build a row-level reconciliation table containing: TBD ID, exact original wording, source document and section, originating decision/PDR/conflict, owner, priority, dependencies, current status, closure evidence, and disposition.
4. Compare every recovered row with the current repository documents and A4 decisions. Record exact matches, partial matches, conflicts, missing entries, and duplicates without silently resolving them.
5. Confirm special references such as TBD-37 and TBD-45 against their original records; retain their current open status until evidence supports a decision.
6. Obtain explicit governance disposition for each item. A missing register must not be repaired by fabricating item descriptions or identifiers.

## 6. Closure criteria

PDR-009 may be reconsidered only when all of the following are satisfied:

- The authoritative original register (or formally accepted reconstruction with provenance) is available.
- The full inventory and exact item wording are preserved.
- Every item is mapped to its source and relevant governance decision.
- Conflicts, duplicates, gaps, and status discrepancies are recorded and dispositioned.
- No item is marked closed without the required decision and evidence.
- The reconciled register and its approval record are committed and independently reviewable.

## 7. Non-effects and boundaries

- No changes to A4 closure decisions.
- No resolution of CONFLICT-01 or CONFLICT-02.
- No change to TBD-37 or TBD-45 status.
- No approval of requirements, ADRs, implementation, or Code Gate readiness.
- No changes to gameplay, Core, Unity, FishNet, CI, or runtime code.
- No merge to `main` is performed by this review.

## 8. Status summary

| Item | Status |
|---|---|
| Repository search and evidence review | IMPLEMENTED — review recorded in this document |
| Canonical TBD register located | NOT VERIFIED / NOT LOCATED |
| Complete TBD inventory | TBD |
| PDR-009 final resolution | CANNOT RESOLVE |
| A5.5 closure | NOT ACHIEVED |
