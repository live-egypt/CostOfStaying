# Reconciliation Progress — Cost of Staying

**Revision:** 2026-11-10  
**Decision Status:** PROPOSED — work plan and checkpoint record  
**Evidence Maturity:** REVIEWED — repository and currently mounted source snapshot  
**Branch:** docs/project-reconciliation-baseline-2026-11-10  
**Base branch:** main  
**Base SHA observed:** 3dd9cfc27aa48b09b3cde802e266a8221c39f0d3

## 1. Approved work sequence

The owner approved the five-package plan and authorized repository delivery on a feature branch with documentation and PR review. This does not authorize direct main edits, merge without review, or silent promotion of a proposed gameplay/architecture decision.

| Order | Work package | Entry/exit rule | Current state |
|---:|---|---|---|
| 1 | Source inventory and provenance | Inventory visible sources and label limits | INITIAL REPOSITORY PACKAGE CREATED; complete chat/source inventory remains PARTIAL |
| 2 | Decision and conflict traceability | Preserve decisions, source events and open conflicts | INITIAL REGISTERS CREATED; owner/source conflicts remain OPEN |
| 3 | Requirements source mapping | Preserve and map 85 TBD rows and 109 consistency records | IMPORTED TRANSCRIPTIONS/MATRICES; source matching remains PARTIAL |
| 4 | Architecture / Network / Security mapping | Separate responsibilities and link design to implementation/evidence | INITIAL NETWORK AND SECURITY RECONCILIATIONS CREATED; original audits missing |
| 4A | Cross-system design consistency | Compare GDD, rules, systems, architecture, security and quality documents | INITIAL REVIEW CREATED; lifecycle and several rule details remain OPEN |
| 5 | Evidence reconciliation | Bind claims to commit, run, environment, artifact, result and scope | FOUR CORE CI RUNS RECOVERED; integration evidence remains OPEN |
| 6 | Document/package control | Map current documents before final numbering/consolidation | INITIAL DOCUMENT CONTROL REGISTER; canonical path map still needs owner review |
| 7 | Central Risk and Debt registers | Start only after source and ID reconciliation | DEFERRED |
| 8 | Validation and readiness | Define reproducible validation and gates | NOT COMPLETE |
| 9 | Final assessment | Issue only after evidence-backed acceptance | NOT ISSUED |

## 2. Repository inspection performed

- Confirmed repository live-egypt/CostOfStaying is public and default branch is main.
- Observed main SHA: 3dd9cfc27aa48b09b3cde802e266a8221c39f0d3.
- Confirmed PR #6 is open Draft, base main, head feature/core-lifecycle-resolution at 2820285021b1a3d7ae9acd953b3bb65677c33512, 94 changed files, not merged.
- Confirmed PR #7 merged into feature/core-lifecycle-resolution, not main.
- Confirmed PR #8 is open Draft, base main, 7 changed files.
- Confirmed PR #9 is open Draft, base feature/core-lifecycle-resolution, 14 changed files.
- Confirmed PR #10 is closed without merge and explicitly superseded.
- Compared branch refs: Core feature branch is 441 commits ahead and 0 behind the observed main snapshot.
- Compared PR #8 documentation branch: 13 commits ahead of main at its observed base.
- Compared PR #9 documentation branch: 48 commits ahead of its Core branch base.
- Created this new documentation branch from main; main remains unchanged.

## 3. Supplied material in the current runtime

Ten text files are currently materialized in the active working directory, totalling 87,396 bytes. Their names, byte counts, line counts and SHA-256 values are recorded in SESSION_MATERIALIZATION_MANIFEST_2026-11-10.md.

This is not a complete inventory of every Project attachment or Library object. Earlier PR #8/#9 source snapshots report 15 distinct current Project attachments and 40 Library file records plus one folder. The exact crosswalk between all historic attachment IDs and the ten paths mounted in this runtime is not established.

## 4. Evidence recovery

Core CI logs were fetched for the four workflow runs listed in CI_EVIDENCE_REGISTER.md. Job logs explicitly reported 148, 177, 181 and 187 NUnit tests passed, each with zero failed and zero skipped. GitHub also returned the associated artifact IDs/digests.

Historical documentation cited 41332755494 for the 177 run; the recovered association/log for the stated commit is 41332759086. The mismatch is recorded, not silently overwritten.

## 5. Important limitations

1. Complete Project conversation enumeration and direct thread IDs for chats 1 and 2 remain unavailable.
2. The conflicting ChatGPT-versus-DeepSeek origin attributions remain open.
3. The complete original Network Audit and Security Audit have not been recovered.
4. The محادثة 2.html export candidate has not been proven to map to Project chat 2.
5. Library raw bytes and archive contents remain under the Stage 0 Negative Attestation limitations where applicable.
6. Full recursive repository tree enumeration was not available through the connector in this session. Changed-file lists are not treated as a complete tree.
7. No Unity project, pinned FishNet package, integration tests, Android build, or Linux Dedicated Server runtime evidence was established on main.
8. No Risk Register, Technical Debt Register, stage closure, or final readiness verdict is issued.

## 6. Files in this reconciliation checkpoint

The branch adds a consolidated governance/source package, preserves the 85-item and 109-record mapping content imported from the PR #9 documentation branch, and adds initial network/security/evidence reconciliations. The documentation index is being updated to the current canonical paths.

## 7. Cross-system design review

Inspected the six main system specifications, technical/network/backend/data/deployment architecture, six security documents, six quality documents, three operations documents and three roadmap documents. The resulting [Design Consistency Review](DESIGN_CONSISTENCY_REVIEW_2026-11-10.md) records 15 findings. Key unresolved items include player lifecycle, endgame result taxonomy, Betrayal threshold effects, CommandId/idempotency boundaries, combat-to-Collapse interaction, and unmeasured performance targets. No gameplay rule was changed.

Structural QA confirmed 85 unique TBD IDs (TBD-01…85) in the Source-to-Requirement Matrix, with no gaps; the source TBD Register has 87 ID occurrences because TBD-37 and TBD-45 repeat in its binding-clarification section, but still 85 unique IDs. The Consistency Matrix primary table has 109 rows and 109 unique record IDs. These are structural results only; source, implementation, test and verification completeness remain partial. See [Requirements Register QA](REQUIREMENT_REGISTERS_QA_2026-11-10.md).

## 8. Core implementation review

A source-level review inspected Core result/identifier/time primitives, vitality/lifecycle, BetrayalLedger, MatchLifecycle, MatchPlayerRegistry, SessionLifecycle/Registry, command envelope/sequence/queue, authenticated admission, session cleanup, roster snapshots, composition root, associated NUnit fixtures and project files. See [Core Implementation Review](CORE_IMPLEMENTATION_REVIEW_2026-11-10.md).

Key boundaries recorded:
- SessionCleanupService does not know whether a dequeued command handler remains in flight; the caller must coordinate completion before cleanup.
- Core session admission is not credential authentication or proof of transport connection ownership.
- CommandSequenceId is not stable CommandId; ADR-015 idempotency is not implemented in inspected Core.
- BetrayalLedger has in-memory event retention dependent on per-match composition lifetime.
- CoreComposition's one-per-match scope is documented but not enforced by the API.
- MatchRosterSnapshot is a limited roster projection, not a full world snapshot; atomic capture and central StateRevision ownership remain unresolved.
- No Debuff/atomic Suffering Transfer service, movement simulation, authoritative tick executor, complete outcome resolver, Unity/FishNet adapter, Android build or Linux server runtime was established in this review.

No source code or ADR was changed. No fresh test execution was performed.

## 9. CI and security automation review

Reviewed Core CI, CodeQL, SecurityCodeScan, optional Contrast and Dependabot workflow definitions on the Core feature branch. See [CI Workflow and Security Automation Review](CI_WORKFLOW_SECURITY_REVIEW_2026-11-10.md). The review confirms the present Core-only trigger/build scope, SARIF artifact retention without GitHub Code Scanning ingestion in the inspected SecurityCodeScan workflow, optional/manual Contrast scope, and the need to verify nested NuGet Dependabot coverage from actual update PRs. No workflow YAML, permission or secret configuration was changed.

## 9. Status

| Item | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| Source Inventory | — | REVIEWED | PARTIAL |
| Provenance Register | — | REVIEWED | PARTIAL |
| Decision Traceability | — | REVIEWED | OPEN |
| Conflict Register | PROPOSED | REVIEWED | OPEN |
| Requirements 01–85 mapping | — | DOCUMENTED | SOURCE MATCHING PARTIAL |
| 109-record consistency matrix | — | DOCUMENTED | 24 pending-source records reported |
| Network reconciliation | PROPOSED | REVIEWED | INITIAL / OPEN |
| Security reconciliation | PROPOSED | REVIEWED | INITIAL / OPEN |
| CI evidence | — | TESTED | Four historical Core snapshots recovered |
| Risk Register | PROPOSED | — | DEFERRED |
| Technical Debt Register | PROPOSED | — | DEFERRED |
| Stage 1 closure | — | — | NOT DECLARED |
| Stage 2 closure | — | — | BLOCKED in recovered records |
| Final readiness assessment | — | — | NOT ISSUED |


## 10. Documentation index and inventory validation

- The 41 Markdown paths in the inspected main documentation inventory were fetched from this main-based branch.
- The current docs/README.md contains 67 internal Markdown links. All 67 targets were fetched successfully from the same branch ref.
- The link check verifies target existence only; it does not validate semantic correctness of all inline links or prove all repository files have been inventoried.
- See DOCUMENTATION_REVIEW_MATRIX.md and INDEX_LINK_VALIDATION_2026-11-10.md.
- Structural QA was independently rechecked: 85 unique TBD IDs in the Source-to-Requirement Matrix, no missing IDs from TBD-01 through TBD-85; 109 rows and 109 unique IDs in the primary Consistency Matrix table.
