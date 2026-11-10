# Repository Documentation Inventory and Review Matrix

**Snapshot date:** 2026-11-10  
**Base branch:** main  
**Base SHA:** 3dd9cfc27aa48b09b3cde802e266a8221c39f0d3  
**Decision Status:** — (inventory artifact; not a design approval)  
**Evidence Maturity:** REVIEWED — 41 indexed Markdown files fetched by exact repository path from the declared base snapshot  
**Scope:** The 41 Markdown paths in the main documentation index/tree snapshot reviewed for this reconciliation.

> The SHA values identify the file blobs read from the inspected main-based documentation snapshot. This is not a recursive inventory of every repository path outside the indexed documentation set. It does not cover binary files, non-Markdown assets, hidden files, or all branches.

## 1. Inventory

| # | Repository path | Domain | Baseline blob SHA | Review note |
|---:|---|---|---|---|
| 1 | docs/README.md | Index | 1a69115d9a984d2b8abbda9b9c268a0639ea6d61 | All indexed relative paths checked in the new feature branch. |
| 2 | docs/00_PROJECT_CHARTER.md | Product | 6b6e306f1401ee5d037cd7371f126e209cb7af91 | Read; proposed project charter. |
| 3 | docs/01_GAME_DESIGN_DOCUMENT.md | Product | 062482093b95d9a58ad2b19af50f86a34bf1cd18 | Read; proposed-for-playtest baseline. |
| 4 | docs/02_GAME_RULES_SPECIFICATION.md | Gameplay | a2f6984782bd517236ddb0c55940f593b789cdd6 | Read; contains Alive-to-Dead lifecycle wording on main. |
| 5 | docs/03_PLAYER_EXPERIENCE_AND_CORE_LOOP.md | Product/UX | 22b635f501060d43a290ce3d9f94fd39b404e275 | Read; proposed-for-playtest. |
| 6 | docs/GLOSSARY.md | Terminology | 8ced9412f7083b9036c3dceac69ed5ac8ae38c46 | Read; definitions should be reconciled with ADR-007 branch. |
| 7 | docs/systems/COLLAPSE_SYSTEM.md | Gameplay system | 884c8f7c74afa0c1b1c866f8d5eacc1f64a6ac1b | Read; says Dead/Spirit ambiguously at zero. |
| 8 | docs/systems/SUFFERING_TRANSFER_SYSTEM.md | Gameplay system | 7201cd6a790677f0468edd4136d1ced4e8dfcc81 | Read; atomic transfer and idempotency proposed. |
| 9 | docs/systems/BETRAYAL_SYSTEM.md | Gameplay system | 812ad48618fadb7086ee9a8e853f778ca2b9c185 | Read; threshold effects remain unresolved. |
| 10 | docs/systems/COMBAT_SYSTEM.md | Gameplay system | 80db2be0b1d9f155a094204e7bb52bbaf5a83e63 | Read; weapon values are prototype hypotheses. |
| 11 | docs/systems/SPIRIT_SYSTEM.md | Gameplay system | 04ee5858b7eb215383973f17c76e03acbee32c37 | Read; contains Dead-to-Spirit wording. |
| 12 | docs/systems/ENDGAME_SYSTEM.md | Gameplay system | 273e3d798f01cf3adc0e9ee527d81ec9b73494c6 | Read; proposed vote/endgame behavior. |
| 13 | docs/architecture/TECHNICAL_ARCHITECTURE.md | Architecture | d47547ab5879686206963e06bde1b1e929e71e4e | Read; proposed layer model. |
| 14 | docs/architecture/NETWORK_ARCHITECTURE.md | Network | 37dc72d3af536894aed4e54585e99ec1496ce990 | Read; no pinned FishNet package evidence. |
| 15 | docs/architecture/BACKEND_AND_HOSTING.md | Architecture/Operations | 04bb62bd6869efba6b9e8670c560b97a8989dd75 | Read; provider/hosting decision proposed. |
| 16 | docs/architecture/DATA_ARCHITECTURE.md | Data | a30161459ab0dfda071135069abd3c246d14eec0 | Read; persistence and retention decisions remain TBD. |
| 17 | docs/architecture/DEPLOYMENT_ARCHITECTURE.md | Deployment | 79bb7ad4922d59e569b596bc71bc54ca3430b2fc | Read; deployment plan is proposed. |
| 18 | docs/security/SECURITY_ARCHITECTURE.md | Security | 4064c163e8af079ca68d822be425e0894aebc3d6 | Read; principles require control/test mapping. |
| 19 | docs/security/TRUST_BOUNDARIES.md | Security | 0215a75e0c9b1caacf9815420deebd5873470206 | Read; trust-boundary proposal. |
| 20 | docs/security/THREAT_MODEL.md | Security | d0102f1351c3fd7fe7a290fe11c405d0c0f87d30 | Read; requires threat-to-test evidence. |
| 21 | docs/security/SECRETS_MANAGEMENT.md | Security/Operations | 5fc4880b76245f0551a48b331afc313ec73ff2e9 | Read; policy proposed, no runtime proof. |
| 22 | docs/security/DATA_PRIVACY.md | Security/Privacy | 084b9e2720ad4b771e8f9ad8484e586de5f7bda3 | Read; data classes/retention remain TBD. |
| 23 | docs/security/INCIDENT_RESPONSE.md | Security/Operations | 8dc012891176b0a203260be7a7c8a00c0ae518ad | Existence and blob fetched; full content review scheduled. |
| 24 | docs/quality/TEST_STRATEGY.md | Quality | 8b595546292799c2e28a4a78712217044c96c28a | Read; explicitly proposed, no test claim from this document. |
| 25 | docs/quality/SECURITY_TEST_PLAN.md | Quality/Security | 618318a0c802ce0af2cc0118015bc230358ef093 | Read; proposed security checks. |
| 26 | docs/quality/PLAYTEST_PLAN.md | Quality/Gameplay | d9872eca8d4c2c3a8e7a95504b576df5e47eeb75 | Read; minimum 10 valid sessions before ADR-004 decision. |
| 27 | docs/quality/PERFORMANCE_BUDGET.md | Quality/Performance | aea708cb5e23170e69fece689117f5785d837ef8 | Read; no measured baseline. |
| 28 | docs/quality/PERFORMANCE_TEST_PROTOCOL.md | Quality/Performance | da31438e535fa45f89c12742d437aa336c3b87d1 | Read; proposed reproducible protocol. |
| 29 | docs/quality/DEVICE_COMPATIBILITY.md | Quality/Platform | 392535cf33cc1e7cd66e23c10943547374a6b359 | Read; device floor not established. |
| 30 | docs/operations/BUILD_AND_RELEASE.md | Operations | 350897940aead441077811f9327d0236b3a14a64 | Read; release flow proposed, not implemented. |
| 31 | docs/operations/MONITORING_AND_LOGGING.md | Operations | 62bb089dc0c1d25bf3a22d3986cb130ca9410c48 | Read; thresholds depend on baseline. |
| 32 | docs/operations/BACKUP_AND_RECOVERY.md | Operations | 48222f75c0b256cbe4d43241429871b765465e07 | Read; explicitly no implemented backup/DR evidence. |
| 33 | docs/roadmap/MVP_SCOPE.md | Roadmap | a5f180d88ab58c3c5a830494c54d3db40d52b853 | Read; proposed MVP scope. |
| 34 | docs/roadmap/ACCEPTANCE_CRITERIA.md | Roadmap/Quality | 635ec80080709ddcc7303cba1fd4885d2267013d | Read; no VERIFIED item asserted in the baseline. |
| 35 | docs/roadmap/DELIVERY_PLAN.md | Roadmap | c763f151f7dce2c611298cd6f3d851be1fa2d33f | Read; sequence proposed, no calendar commitments. |
| 36 | docs/governance/DECISIONS_LOG.md | Governance | 8cb7e4f4d2d30436bdeb05eebb61cf55b9e4c59b | Read; indexes ADR-001…004 on main. |
| 37 | docs/governance/REQUIREMENTS_TRACEABILITY.md | Governance | 204d02d19e2bd7fa23a749ec818cf26f7096b514 | Read; initial requirement examples, not an 85-TBD mapping. |
| 38 | docs/governance/adr/ADR-001-TECH-STACK.md | ADR | 9d2ad3cd4f0819ab1156f84d7d1fed40900c68b1 | Read; PROPOSED. |
| 39 | docs/governance/adr/ADR-002-SERVER-AUTHORITY.md | ADR/Security | 15acdb4180818a28b569519675a70d2a027a5012 | Read; APPROVED principle, implementation not established by ADR. |
| 40 | docs/governance/adr/ADR-003-BACKEND-AND-HOSTING.md | ADR/Architecture | b5ade5f008b6fa299bcfd5a731ddb9705ccf9a49 | Read; PROPOSED. |
| 41 | docs/governance/adr/ADR-004-GAMEPLAY-RULES-BASELINE.md | ADR/Gameplay | d1f035dd0034f5b87eecd1c3186660903c0ff26f | Read; PROPOSED FOR PLAYTEST. |

## 2. Index target validation

The paths linked from the main documentation index were fetched by exact path from the new main-based reconciliation branch. All 41 paths in the indexed set returned file content. The new index additionally links the governance and audit deliverables created by this reconciliation branch; those paths are created on this same branch.

This check validates target existence at the inspected ref. It does not validate semantic consistency of every cross-document statement or all inline links inside every document.

## 3. Structural findings

- The main documentation package is organized by product, systems, architecture, security, quality, operations, roadmap, and governance.
- The status vocabulary used by some main documentation combines decision states and evidence states. The new governance baseline proposes independent axes; this proposal does not retroactively change every source document.
- The main rules and the PR #6 branch ADR-007 conflict on the player lifecycle at zero Vitality.
- Collapse and Spirit system descriptions use ambiguous Dead/Spirit language and must be reconciled against the owner-approved lifecycle source before technical implementation.
- Backend, hosting, performance budgets, device floors, operations and production controls are proposals; no production runtime proof follows from the documents.
- The main requirements traceability document provides initial gameplay/Core examples and does not map the separate 85-item TBD register.

## 4. Exclusions

This inventory does not claim:
- Complete repository-wide recursive path enumeration.
- Full review of non-Markdown or binary assets.
- Unity project tree or package lock completeness.
- Complete review of PR #6 feature-only files.
- Complete Project chat or Project Library source enumeration.
- Approval of package numbering or final canonical paths.
