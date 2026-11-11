# Package Structure Reconciliation — Draft 0.1

**Project:** Cost of Staying (ثمن البقاء)  
**Revision:** 2026-11-10  
**Decision Status:** PROPOSED — OWNER REVIEW REQUIRED  
**Evidence Maturity:** REVIEWED — bounded documentation-tree and inventory inspection  
**Work Order:** WO-DOC-01 / Work package C-to-D transition  
**Branch:** `docs/project-reconciliation-baseline-2026-11-10`  
**Scope:** Functional mapping of the current documentation package; no path migration or ADR change.

> This document is a reconciliation proposal, not an approval of final numbering, package names, or document ownership changes. Existing source paths remain intact until explicit owner review.

## 1. Objective and constraints

The repository currently has an established documentation tree organized around product/design, systems, architecture, security, quality, operations, roadmap, governance, and audit evidence. The objective is to assign each document one primary responsibility and identify cross-links, overlaps, and missing sources without creating parallel sources of truth.

Constraints:
- Preserve all existing paths and history.
- Do not rename, move, delete, or merge documents in this draft.
- Do not convert a proposal into an approved decision.
- Keep Stage 0–5 distinct from Phase 1–3.
- Keep Risk Register and Technical Debt Register deferred until source and identifier reconciliation is complete.
- Treat the 41-file main inventory as a bounded historical snapshot, not a complete repository tree. The current branch adds further governance and audit artifacts.

## 2. Proposed functional package map

| Package | Primary responsibility | Canonical existing paths | Supporting paths / boundary |
|---|---|---|---|
| P00 — Product Charter & Design | Product premise, target, GDD, player loop, glossary | `docs/00_PROJECT_CHARTER.md`, `docs/01_GAME_DESIGN_DOCUMENT.md`, `docs/03_PLAYER_EXPERIENCE_AND_CORE_LOOP.md`, `docs/GLOSSARY.md` | `docs/roadmap/MVP_SCOPE.md`; no implementation claims. |
| P01 — Gameplay Rules & Systems | Proposed rules and individual gameplay system contracts | `docs/02_GAME_RULES_SPECIFICATION.md`, `docs/systems/*.md` | `docs/quality/PLAYTEST_PLAN.md`, `docs/roadmap/ACCEPTANCE_CRITERIA.md`; rules remain proposed for playtest where stated. |
| P02 — Architecture & Platform | Technical layers, data, hosting, network, deployment architecture | `docs/architecture/*.md` | ADRs govern approved decision scope; architecture documents describe intended design, not proof of implementation. |
| P03 — Security & Privacy | Trust boundaries, threat model, security architecture, privacy, secrets, incident response | `docs/security/*.md` | `docs/quality/SECURITY_TEST_PLAN.md` defines proposed verification; network audit retains transport-specific responsibility. |
| P04 — Quality & Validation | Test strategy, compatibility, performance, playtest, security test protocols | `docs/quality/*.md` | Test results belong in Evidence/CI registers; a plan is not an executed test. |
| P05 — Operations & Delivery | Build/release, monitoring, backup/recovery, delivery plan and acceptance | `docs/operations/*.md`, `docs/roadmap/DELIVERY_PLAN.md`, `docs/roadmap/ACCEPTANCE_CRITERIA.md` | Runtime/production readiness requires operational evidence. |
| P06 — Governance & Sources | Authority, source lineage, decisions, requirements, conflicts, document control, work orders | `docs/governance/*.md`, `docs/governance/adr/*.md` | ADRs remain authoritative for their decision scope; registers index and trace them. |
| P07 — Audits & Evidence | Cross-domain reviews, source recovery, CI, repository, package and reconciliation reports | `docs/audit/*.md` | Audits state their scope, branch, commit, evidence maturity and limitations. |
| P08 — Project Control & Continuity | Current status and resumable session state | `docs/PROJECT_STATUS_AND_EXECUTION_REGISTER.md`, `docs/SESSION_HANDOFF.md` | These are navigation/control records, not alternative decision or evidence sources. |

The package labels P00–P08 are provisional grouping labels for this proposal only. They are not final document numbers and must not be used to rename existing files.

## 3. Single-authority rules by information type

| Information type | Primary authority | Other documents should |
|---|---|---|
| Product/gameplay design intent | GDD and relevant system specification | Link; do not restate changed rules independently. |
| Approved architecture decision | Relevant ADR | Link to ADR and record implementation/evidence separately. |
| Decision/event trace | `DECISION_TRACEABILITY_REGISTER.md` | Reference the source event; do not invent source IDs. |
| Source identity and access boundary | `SOURCE_INVENTORY.md` + `PROVENANCE_REGISTER.md` | Link to the source record. |
| Requirement uncertainty | `TBD_REGISTER.md` | Preserve status; do not treat registration as resolution. |
| Requirement/source relation | `SOURCE_TO_REQUIREMENT_MATRIX.md` | Keep mapping distinct from acceptance. |
| Requirement/evidence relation | `REQUIREMENT_EVIDENCE_MATRIX.md` | Link exact code/test/run/artifact and scope. |
| Cross-document conflicts | `CONFLICT_REGISTER.md` | Link to conflicting canonical sources; do not resolve by summary. |
| CI execution evidence | `CI_EVIDENCE_REGISTER.md` | Link exact workflow/run/job/commit/artifact. |
| Work-order/action state | `WORK_ORDER_REGISTER.md` | Do not create competing action trackers. |
| Live project delivery state | `PROJECT_STATUS_AND_EXECUTION_REGISTER.md` | Summarize only; link detailed records. |
| Session resume state | `SESSION_HANDOFF.md` | Point to canonical records; do not become a second master register. |

## 4. Observed overlaps and treatment

| Overlap | Treatment in Draft 0.1 | No-change boundary |
|---|---|---|
| `REQUIREMENTS_TRACEABILITY.md` vs `SOURCE_TO_REQUIREMENT_MATRIX.md` | Retain the former for concise product/engineering examples and the latter for the 85-item source mapping. Clarify scope in headers after review. | No deletion or merge until row-level comparison is complete. |
| `SOURCE_TRACEABILITY_REGISTER.md` vs `SOURCE_INVENTORY.md` / `PROVENANCE_REGISTER.md` | Treat source traceability as a relationship/index view; inventory owns source identity, provenance owns lineage/limitations. | Do not duplicate full source descriptions. |
| `TBD_CROSS_REFERENCE_MAP.md` vs `CONSISTENCY_MATRIX.md` | Cross-reference map links IDs/groups; Consistency Matrix remains the 109-record consistency dataset. | Do not collapse their data without row-level proof. |
| `DECISIONS_LOG.md` vs `DECISION_TRACEABILITY_REGISTER.md` | Decisions Log indexes ADRs/decision records; Traceability Register maps owner events and provenance. | Do not infer approval from an ADR label without the source event where required. |
| `NETWORK_ARCHITECTURE.md` vs Network Audit reconciliation | Architecture defines intended system design; audit records findings, gaps, and evidence needs. | Original Network Audit remains source-pending. |
| Security architecture/threat model vs Security Audit reconciliation | Security docs define intended controls/threats; audit evaluates evidence and gaps. | Original Security Audit remains source-pending. |
| Audit reports vs Evidence Register | Reports summarize bounded reviews; Evidence Register binds claims to exact evidence. | Report text alone is not executable proof. |
| Project status vs Session Handoff | Status is durable project delivery snapshot; handoff is short-lived resume pointer. | Avoid copying detailed registers into both. |

## 5. Source and inventory coverage

The current branch tree exposes the paths listed in its documentation tree, while `DOCUMENTATION_REVIEW_MATRIX.md` records a bounded 41-Markdown-file main snapshot. Additional branch documents are listed in the repository README and audit indexes. Neither fact establishes a complete inventory of all repository assets, binaries, hidden files, all branches, Project Library objects, or source conversations.

Before final numbering or migration:
1. Generate a full recursive repository tree manifest with exact commit SHA.
2. Classify Markdown, code, test, configuration, binary, archive, generated, and external-source files.
3. Compare every documentation path to exactly one primary package.
4. Validate every internal Markdown link at the same commit.
5. Review duplicate/overlap candidates row-by-row.
6. Obtain owner approval for any path move, rename, merge, or canonical ownership change.

## 6. Deferred decisions and blockers

| ID | Item | State | Why it remains open |
|---|---|---|---|
| PKG-01 | Final package numbering and names | PENDING OWNER REVIEW | This draft proposes functional grouping only. |
| PKG-02 | Document moves/renames/merges | NOT AUTHORIZED | Requires explicit owner approval and migration plan. |
| PKG-03 | Full repository asset inventory | PARTIAL | Current inventory scope is bounded. |
| PKG-04 | Network Audit canonical source | SOURCE PENDING | Complete original audit not recovered. |
| PKG-05 | Security Audit canonical source | SOURCE PENDING | Complete original audit not recovered. |
| PKG-06 | A1-HOLD-01…06 and WO-A3 mandatory amendments | SOURCE BLOCKED | Exact owner source not recovered. |
| PKG-07 | Central Risk and Technical Debt registers | DEFERRED | Source and identifier reconciliation gate not passed. |
| PKG-08 | Stage 1/2 closure | NOT DECLARED | Required source/evidence/owner gates remain open. |

## 7. Proposed acceptance checklist

This draft can be considered ready for owner review when:
- Every current documentation path is assigned one primary package.
- Each subject has one authoritative record type.
- Cross-links do not create circular authority or duplicate truth.
- Overlap candidates have a documented keep/merge/retire recommendation.
- Missing source material and incomplete inventories remain explicit.
- Proposed numbering is clearly distinguished from approved repository paths.
- The owner explicitly accepts the package map before any migration.

## 8. Current status

**Package map:** PROPOSED — DRAFT 0.1  
**Canonical path migration:** NOT AUTHORIZED / NOT PERFORMED  
**Document deletion:** NOT PERFORMED  
**ADR changes:** NOT PERFORMED  
**Risk Register / Technical Debt Register:** DEFERRED  
**Stage 1 / Stage 2 closure:** NOT DECLARED

**Next action:** continue the source-backed requirement and evidence reconciliation using the existing 85-item and 109-record canonical datasets; retain unresolved source gaps and do not migrate paths before owner review.
