# Decision Traceability Matrix — Initial Pass

**Snapshot:** 2026-11-09  
**Status:** `INITIAL — PARTIAL; ORIGINAL CONVERSATION MESSAGE LINKS INCOMPLETE`  
**Companion:** [SOURCE_INVENTORY.md](SOURCE_INVENTORY.md), [PROVENANCE_REGISTER.md](PROVENANCE_REGISTER.md)

## 1. Rules

- This matrix records only decisions or dispositions surfaced in the currently available sources.
- A report that says a decision was approved is not automatically the original approval message.
- A repository ADR status is authoritative only for the ADR version and branch actually inspected.
- `APPROVED` is a design/governance state, not proof of implementation or verification.
- A decision may be approved for documentation while the gameplay or technical rule remains PROPOSED/TBD.
- Where the original user message or source thread is unavailable, the matrix records the claim as report-derived and leaves the original trace open.

## 2. Initial decision matrix

| ID | Subject | Current recorded disposition | Source reference | Scope / interpretation | Trace status |
|---|---|---|---|---|---|
| DEC-001 | Server authority | `APPROVED AS PRINCIPLE`; implementation not established by the decision alone | Main `docs/governance/DECISIONS_LOG.md` | Server is intended to own authoritative match decisions; this is not an implementation claim. | `DOCUMENTED — ORIGINAL USER MESSAGE LINK PENDING` |
| DEC-002 | Core executable boundary | `APPROVED` in the development-branch decision log | `feature/core-lifecycle-resolution` `docs/governance/DECISIONS_LOG.md`; ADR-005 | Core purity boundary is a documented architectural decision; current main does not contain the same ADR set. | `BRANCH-SCOPED SOURCE VERIFIED; ORIGINAL APPROVAL MESSAGE PENDING` |
| DEC-003 | Player lifecycle / Vitality | `APPROVED` in ADR-007 and PR #6 description | Development-branch ADR-007 / PR #6 | Alive / Spirit / Eliminated; zero Vitality moves Alive to Spirit; Eliminated terminal. Do not treat PR description as proof of all integrations. | `DOCUMENTED; APPROVAL TRACE NEEDS ORIGINAL MESSAGE` |
| DEC-004 | Victim scope | `PROPOSED` | Development-branch ADR-014; blob `0ec406999c3880e36838618b6939de0bdcb9bb6a` | Victim categories remain unresolved; retain VictimId; NPC support is not approved. | `ADR CONTENT VERIFIED ON FEATURE BRANCH; DOMAIN DECISION OPEN` |
| DEC-005 | Command identity / idempotency | `PROPOSED` | Development-branch ADR-015; blob `616089292b34b16113672038d32d4d98fc0d90d1` | Independent CommandId scoped to trusted stable player identity; sequence remains session-scoped; in-memory record survives reconnect only while server runs; restart protection not promised; reservation atomicity implementation TBD. | `ADR CONTENT VERIFIED ON FEATURE BRANCH; NOT APPROVED` |
| DEC-006 | C1 architecture baseline | `PREFERRED BASELINE — PROPOSED` according to recovered project context | Prior project summary / Report 02–03 references | Do not promote to approved architecture without original source and explicit owner decision. | `SOURCE TRACE PENDING` |
| DEC-007 | B3 idempotency registry | `INDEPENDENT DESIGN ELEMENT — NOT IMPLEMENTED` according to recovered context | Prior project summary / Report 02–03 references | Keep separate from C1 selection; no implementation claim. | `SOURCE TRACE PENDING` |
| DEC-008 | A1 × C exploration | `SELECTED FOR EXPLORATION — CONDITIONAL` according to recovered context | Prior project summary | Exploration selection is not final architecture approval. | `SOURCE TRACE PENDING` |
| DEC-009 | S-C1 rollback after publication | `ROLLBACK NOT PERMITTED AFTER PUBLICATION` according to recovered context | Prior project summary | Treat as reported constraint pending original message/ADR mapping. | `SOURCE TRACE PENDING` |
| DEC-010 | S-C1B soft invalidation | `REPORTED AS A DESIGN ALTERNATIVE`; exact full rule text unavailable | Prior project summary | Do not expand or operationalize the incomplete source phrase. | `SOURCE PARTIAL` |
| DEC-011 | Stage 0 acceptance with reservation | `ACCEPTED WITH DOCUMENTED CONSTRAINT` in recovered Stage 0 materials | `NEGATIVE_ATTESTATION_STAGE0.md` and provenance note in Library | Raw-byte/upload-audit limitations remain binding for the exact Library records. | `DOCUMENTED; ORIGINAL APPROVAL MESSAGE TRACE PENDING` |
| DEC-012 | Stage 1 documentation plan | Explicitly approved by the current user message `نعم اعتمد` | Current project conversation | Authorizes staged documentation-only work on a feature branch, commits, and PR for review; does not approve stage closure, ADR changes, code, tests, or merge. | `USER APPROVAL PRESENT — SCOPE LIMITED` |

## 3. Branch scope warning

ADR-005, ADR-007, ADR-014, ADR-015 and the expanded decision log were read from `feature/core-lifecycle-resolution`, whose observed HEAD is `2820285021b1a3d7ae9acd953b3bb65677c33512`. They are not asserted to exist on `main` at the same snapshot. This Stage 1 documentation branch is based on `main` and does not copy or modify those ADRs.

## 4. Required next traceability work

For every row above, the later decision-trace pass must capture, where available:

1. Original conversation/thread ID.
2. Exact user message or turn reference.
3. Decision wording as approved, including conditions and exclusions.
4. ADR/document path and blob/commit reference.
5. Later amendment, supersession, or withdrawal.
6. Implementation references, if any.
7. Tests and evidence supporting verification, if any.

No row in this initial matrix closes the decision traceability gap by itself.
