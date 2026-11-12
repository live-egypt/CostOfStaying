# Document Control — Source & Governance Recovery

**Snapshot:** 2026-11-09  
**Status:** `PROPOSED CONTROL DOCUMENT — INITIAL STAGE 1 BASELINE`

## 1. Purpose

This document defines how source, decision, conflict, requirement, audit, and evidence documents are controlled during the Cost of Staying recovery effort. It does not approve gameplay rules, architecture alternatives, ADRs, or a final package numbering scheme.

## 2. Current Stage 1 canonical documents

| Information type | Stage 1 reference | Authority status |
|---|---|---|
| Exposed source files and metadata | [SOURCE_INVENTORY.md](SOURCE_INVENTORY.md) | Initial inventory; partial by design. |
| Origin, lineage, and evidence limits | [PROVENANCE_REGISTER.md](PROVENANCE_REGISTER.md) | Initial provenance register; partial. |
| Decision claims and their source traces | [DECISION_TRACEABILITY_MATRIX.md](DECISION_TRACEABILITY_MATRIX.md) | Initial matrix; not a final decision log. |
| Open source/governance conflicts | [CONFLICT_REGISTER.md](CONFLICT_REGISTER.md) | Open register; no automatic conflict closure. |
| Stage 1 review and gate | [STAGE1_SOURCE_PROVENANCE_REVIEW.md](STAGE1_SOURCE_PROVENANCE_REVIEW.md) | Stage 1 review output; user acceptance required before Stage 2 recovery. |

These are the canonical documents **for this Stage 1 source-recovery package only**. They do not supersede the existing Library records or establish the final project-wide document architecture.

## 3. Document status vocabulary

| Status | Meaning |
|---|---|
| `PROPOSED` | Suggested content or rule not approved. |
| `PROPOSED FOR PLAYTEST` | Gameplay baseline awaiting playtest evidence and owner decision. |
| `APPROVED` | Explicitly accepted design/governance decision; not proof of implementation. |
| `RECORDED` | Appears in a source or register; source may remain unverified. |
| `REVIEWED` | Content has been read and assessed within a declared scope. |
| `IMPLEMENTED` | Present in an identifiable executable/code artifact. |
| `TESTED` | A named test or procedure was executed with a recorded result. |
| `VERIFIED` | A specific claim passed a reproducible verification procedure with retained evidence. |
| `OPERATIONALLY PROVEN` | Sustained real-environment behavior is evidenced over a declared operational window. |
| `PARTIAL` | Some required evidence or content exists, but the scope is incomplete. |
| `SOURCE PENDING` | Required source content is unavailable. |
| `TBD` | Not yet resolved. |
| `DEFERRED` | Intentionally postponed; not closed. |

Statuses are claim-specific. Do not promote a whole system to VERIFIED because one test passed.

## 4. Evidence record minimum fields

Every later evidence entry should record:

- Evidence ID.
- Claim/requirement/decision ID supported.
- Source and exact location.
- Repository, branch, and commit SHA where applicable.
- Workflow/run ID and execution date.
- Environment, SDK/tool versions, and configuration.
- Test/procedure and exact scope.
- Result, artifact path/ID, and digest where available.
- Limitations and excluded claims.
- Status and reviewer/owner.

A historical CI result must remain attached to its original commit and run. It is not evidence of a different HEAD.

## 5. Source and document ownership rules

- Original conversations and user messages are the primary source for user decisions when available.
- ADR files record architectural decisions; their status must match their content and decision history.
- Decision logs index ADRs; they do not independently approve decisions.
- Requirements registers describe requirements; registration does not resolve a requirement.
- Audit documents report findings and gaps; they do not implement fixes.
- Evidence registers support bounded claims; they do not replace source artifacts.
- Risk and technical-debt registers must not be created as authoritative registers until their source identities and classifications are reconciled and the user approves their role.
- Each project topic should eventually have one authoritative definition; other documents should reference it rather than redefine it.

## 6. Stage and Phase separation

`Stage 0–5` and `Phase 1–3` are separate project control structures. They must not be merged or used interchangeably. Each needs its own purpose, entry criteria, exit criteria, evidence, and approval gate.

## 7. Change control for this recovery branch

- Branch: `docs/stage1-source-provenance-recovery`.
- Base: observed `main` SHA `3dd9cfc27aa48b09b3cde802e266a8221c39f0d3`.
- Scope: Markdown documentation under `docs/governance/sources/` plus the documentation index.
- Excluded: `main` direct changes, Core, Net, Unity, Tests, ADR files, gameplay implementation, test execution, and merge.
- PR is for review only; no automatic merge.
- Existing PR #6 remains untouched and is not part of this documentation branch.

## 8. Change log

| Version | Date | Change |
|---|---|---|
| 0.1 | 2026-11-09 | Initial Stage 1 source and document-control baseline created from the currently exposed source surfaces. |
