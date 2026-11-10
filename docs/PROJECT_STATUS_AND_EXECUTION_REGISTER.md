# Project Status and Execution Register

**Revision:** 2026-11-10 — WO-DOC-01 execution update  
**Decision Status:** PROPOSED — operational control document  
**Evidence Maturity:** REVIEWED — bounded repository and supplied-source snapshot  
**Default branch:** main  
**Observed main SHA:** 3dd9cfc27aa48b09b3cde802e266a8221c39f0d3

> This register records repository and project status by branch and evidence scope. It does not override an approved ADR or convert a proposal into implementation.

## 1. Repository delivery baseline

| Workstream | Location | State | Delivery meaning |
|---|---|---|---|
| v2.1 design documentation | main | PRESENT — proposed baseline | Documents exist; gameplay remains PROPOSED FOR PLAYTEST. |
| Governance/source recovery package | docs/project-reconciliation-baseline-2026-11-10 / PR #11 | OPEN / DRAFT / REVIEW ONLY | 36 documentation paths in the live PR changed-file list at the current checkpoint; based on main; not merged. |
| Standalone pure-C# Core | feature/core-lifecycle-resolution, PR #6 | IMPLEMENTED ON FEATURE BRANCH; CI TESTED | Not part of main. PR #6 remains open Draft. |
| Core hotfix | PR #7 | MERGED INTO feature/core-lifecycle-resolution | Does not represent a main merge. |
| Stage 1 source package | PR #8 | OPEN / DRAFT / NOT MERGED | Based on main; overlaps PR #9. |
| 85-TBD mapping package | PR #9 | OPEN / DRAFT / NOT MERGED | Based on Core feature branch; overlaps PR #8. |
| Superseded package | PR #10 | CLOSED WITHOUT MERGE | Historical-only; not an active delivery candidate. |
| Unity project | No complete Unity project tree on main | NOT PRESENT IN REVIEWED MAIN SCOPE | The PR #5 ZIP is only an Assets/source package; no Packages manifest/lock, ProjectSettings, scenes or .meta inventory. |
| Unity Assets/source package | review/unity-source-package-2026-11-01 / PR #5 | PRESENT ON REVIEW BRANCH — 81 C# / 4 asmdef | Not on main; archive SHA-256 manifest mismatch; Unity compilation, tests and FishNet compatibility NOT VERIFIED. |
| FishNet runtime adapters | Not established in reviewed repository evidence | NOT IMPLEMENTED | API/version verification required. |
| Android release client | No app build evidence recovered | NOT VERIFIED | Requires Unity build and device validation. |
| Linux Dedicated Server | No integrated server build evidence recovered | NOT VERIFIED | Requires Unity headless/server build and runtime tests. |

## 2. Decision snapshot

| Decision record | Main state | Branch-specific state | Boundary |
|---|---|---|---|
| ADR-001 Tech Stack | PROPOSED | Same proposal carried in branch | Unity/FishNet versions and hosting require prototype verification. |
| ADR-002 Server Authority | APPROVED as a principle | Same | Implementation is not established by the principle alone. |
| ADR-003 Backend/Hosting | PROPOSED | Same | No production provider selected. |
| ADR-004 Gameplay Baseline | PROPOSED FOR PLAYTEST | Same baseline maintained with lifecycle reconciliation | Requires at least 10 documented playtest sessions per ADR. |
| ADR-005 Core Boundary | Not on main | APPROVED in PR #6 branch | Branch-scoped until reviewed/merged. |
| ADR-006 Core identifiers/ledger | Not on main | PROPOSED in PR #6 branch | Not an owner-approved gameplay rule. |
| ADR-007 Player Lifecycle | Not on main | Marked APPROVED in PR #6 branch | Original approval trace still requires direct source verification; conflicts with main v2.1 lifecycle wording. |
| ADR-008…015 | Not on main | Branch-specific ADRs; status varies, many PROPOSED | Do not assume approval from implementation or test evidence. |

## 3. Core implementation and CI evidence

PR #6 contains standalone Core and NUnit source on feature/core-lifecycle-resolution at 2820285021b1a3d7ae9acd953b3bb65677c33512. That branch was 441 commits ahead and 0 behind the observed main SHA at comparison time. PR #6 is open, Draft, and unmerged.

PR #11 is the current documentation consolidation candidate: base main at 3dd9cfc27aa48b09b3cde802e266a8221c39f0d3; head docs/project-reconciliation-baseline-2026-11-10. It remains Draft and unmerged. Its current metadata must be rechecked before any review/merge action; mergeability is not owner approval.

Historical Core CI results are recorded in the CI Evidence Register. The latest recovered PR #6 head run reports 187/187 NUnit tests passing on the exact Core commit. This verifies only the standalone Core workflow scope. It does not verify Unity import/build, FishNet runtime, Android, Linux Dedicated Server, multiplayer networking, game balance, or production security.

## 4. Stage track and Phase track

Keep these independent.

### Stage track — governance/source/evidence lifecycle

| Stage | Current recorded position | Notes |
|---|---|---|
| Stage 0 | Historical acceptance with source/raw-byte limitations | Stage 0 Negative Attestation remains binding for specified Library items. |
| Stage 1 | PARTIAL | Source inventory/provenance records exist; full conversation enumeration and original audits remain incomplete. |
| Stage 2 | PARTIAL / CLOSURE GATE BLOCKED in recovered records | No closure declaration is issued by this register. |
| Stages 3–5 | NOT ESTABLISHED | No source-backed closure status asserted here. |

### Phase track — product/engineering implementation

| Phase | Current recorded position | Notes |
|---|---|---|
| Phase 1 | PARTIAL — standalone Core slice on a feature branch | Unity/FishNet integration and remaining Core/application services are incomplete. |
| Phase 2 | TBD / NOT ESTABLISHED | Requires an approved scope and entry gate. |
| Phase 3 | TBD / NOT ESTABLISHED | Requires an approved scope and entry gate. |

A completed CI run is not a Stage closure event. A Stage closure is not a runtime implementation result.

## 5. Requirements and test boundaries

- The 85-item TBD register remains a source/decision uncertainty register; no item is resolved merely because it has a row or conversation-event mapping.
- The 109-record Consistency Matrix reports 24 pending-source records and pending primary-group completeness.
- Existing REQUIREMENTS_TRACEABILITY.md contains initial gameplay/Core requirement examples; it does not explicitly map TBD-01…85 in the main snapshot reviewed by PR #9.
- Every Core logic function must have NUnit coverage or an explicit documented exception.
- Net/Unity adapters require integration tests once an actual Unity project and pinned FishNet package are available.
- Do not introduce unverified FishNet APIs or set a final FishNet version from an assumption.

## 6. Open implementation boundaries

The branch-scoped Core contains tested primitives for identifiers/results, vitality/lifecycle, betrayal ledger, match lifecycle/player registry, session lifecycle/registry, command envelope/sequence/queue/admission, time/revision, cleanup, and snapshot construction as described in PR #6 branch records.

The PR #5 Unity ZIP contains Core/Net and EditMode/Net test source, but is only a branch-scoped Assets/source package. Its static source review identifies contract gaps and no Unity compilation/test execution. See the Unity Source Package Reconciliation audit.

The following are not established as complete production systems:
- Credential validation and authentication integration.
- Transport connection-to-session mapping and authorization policy.
- Reconnect continuity/timeouts and restart-safe idempotency.
- Full match/player/world aggregate and deterministic simulation pipeline.
- Movement simulation and authoritative command handlers.
- Debuff model and atomic Suffering Transfer application.
- Snapshot transport/replication and complete match outcome resolution.
- Unity project, asmdefs, package manifest/lock, scenes and build targets.
- FishNet ServerRpc/ObserversRpc adapters and serialization compatibility.
- Android device performance, compatibility and release.
- Linux Dedicated Server build, hosting, operations and recovery.
- End-to-end security and abuse-resistance verification.

## 7. Work sequence

1. Finish source inventory, provenance, and chat/source identity boundaries.
2. Reconcile decision traces and open source/design conflicts.
3. Validate all 85 TBD rows and 109 consistency records without flattening their dimensions.
4. Complete Architecture ↔ Network ↔ Security responsibility mapping.
5. Complete CI/artifact and requirement-to-test evidence reconciliation.
6. Complete document authority/path mapping; do not freeze numbering before owner review.
7. Keep Risk and Technical Debt registers deferred until ID/source reconciliation is complete.
8. Define reproducible Unity/FishNet integration, Android, Linux, security, performance, and playtest validation gates.
9. Issue final assessment only when all declared gates have evidence and owner acceptance.

## 8. WO-DOC-01 documentation governance execution

The current project conversation context now contains 20 attachment objects. The prior source snapshot recorded 15; five additional IDs and mounted-byte SHA-256 values are documented in `docs/audit/ATTACHMENT_SNAPSHOT_DELTA_2026-11-10.md`. This does not re-enumerate Project Library or prove full conversation coverage.

The project owner approved WO-DOC-01 for sequential execution. The work-order tracker is `docs/governance/WORK_ORDER_REGISTER.md` and is the single index for action state. This approval authorizes documentation work; it does not approve the proposed governance policy wholesale, alter ADRs, authorize code changes, merge PRs, or close project stages.

| Work package | Scope | Current state |
|---|---|---|
| A — Governance Baseline | Authority, status axes, document/change control | INITIAL BASELINE PRESENT ON PR #11 — OWNER REVIEW PENDING |
| B — Source & Provenance | Attachments, Library, conversations, exports, repository sources | PARTIAL — 20 ATTACHMENT IDS ENUMERATED; 5-ID DELTA HASHED; LIBRARY/CHAT SOURCES OPEN |
| C — Decision & Action Tracking | Owner events, ADR traceability, conflicts, work orders | PARTIAL — WO-DOC-01 AUTHORIZATION MAPPED; A1-HOLD/WO-A3 SOURCE RECOVERY BLOCKED |
| D — Evidence & Traceability | Requirements, code, tests, CI, artifacts, commits | PARTIAL — HISTORICAL EVIDENCE ONLY WHERE RECORDED |
| E — Session Continuity | Session handoff and next action | PRESENT — MAINTAIN EACH SESSION |

PR #11 remains OPEN / DRAFT / NOT MERGED. No changes to main or code/test/ADR files are authorized by this documentation work order.

## 9. Current delivery status

| Item | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| Project design baseline | PROPOSED FOR PLAYTEST | DOCUMENTED | Current main |
| Server-authority principle | APPROVED | DOCUMENTED | Implementation evidence partial and branch-scoped |
| Standalone Core in PR #6 | — | IMPLEMENTED / TESTED | Feature branch only |
| Core CI 187/187 | — | TESTED | Exact run/commit in evidence register |
| Unity/FishNet integration | PROPOSED | — | NOT IMPLEMENTED / NOT VERIFIED |
| Android/Linux release | PROPOSED | — | NOT VERIFIED |
| Stage 1 closure | — | — | NOT DECLARED |
| Stage 2 closure | — | — | BLOCKED in recovered status |
| Risk Register | PROPOSED | — | DEFERRED |
| Technical Debt Register | PROPOSED | — | DEFERRED |
| Final readiness assessment | — | — | NOT ISSUED |
| Documentation consolidation delivery | PROPOSED | DOCUMENTED / REVIEWED | PR #11 — OPEN / DRAFT / NOT MERGED |
