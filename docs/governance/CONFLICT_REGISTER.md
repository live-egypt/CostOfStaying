# Conflict Register — Cost of Staying

**Revision:** 2026-11-10  
**Decision Status:** PROPOSED — resolutions require the relevant owner/source gate  
**Evidence Maturity:** REVIEWED — initial cross-branch and supplied-record reconciliation  
**Scope:** Source, requirements, design, architecture, repository delivery, and CI-evidence conflicts.

> This register preserves conflicts and snapshot differences. It does not resolve a conflict merely by documenting it.

## 1. Closure rules

A conflict may be closed only when the entry records:
1. The exact conflict identifier and disputed values.
2. Whether it is a true contradiction, a snapshot difference, a branch-scope difference, or a provenance gap.
3. The selected source/resolution and the reason for retaining or superseding alternatives.
4. Explicit owner acceptance when the issue changes a product or architecture decision.
5. Reproducible evidence for technical claims.
6. All affected canonical documents and references updated.

## 2. Source and provenance conflicts

| ID | Subject | Observed values / source claims | Classification | State | Closure evidence |
|---|---|---|---|---|---|
| SRC-001 | Project attachment totals | Earlier snapshots report 5, 9, 11 and 15 attachments; current conversation context exposes 20 distinct attachment objects. Current mounted runtime contains 13 unique text-file paths (not 20 paths). | Snapshot/surface difference; the five-object delta over the prior 15-object snapshot is recorded with IDs and mounted-byte hashes in ATTACHMENT_SNAPSHOT_DELTA_2026-11-10.md. | PARTIAL — 5-ID DELTA RECONCILED | Fresh Library enumeration and a complete attachment-ID-to-materialized-path crosswalk remain required. |
| SRC-002 | Repeated attachment names | Four records named 2 استكمال المحادثة.txt are reported as distinct IDs and hashes. Current runtime contains only one path with that basename. | Distinct-source identity plus current materialization limitation. | PARTIAL | Preserve source IDs and exact hashes; establish missing-file mapping. |
| SRC-003 | Project Library inventory | Earlier snapshot reports 35 files + 1 folder; later PR #8/#9 snapshot reports 40 files + 1 folder. | Different inventory snapshots. | OPEN | Exact timestamped Library listings and metadata. |
| SRC-004 | Chat 1 and Chat 2 origin | Earlier material describes DeepSeek association; attachment file_00000000343482109ecb2381153c4550 says these are ChatGPT Project conversations, not DeepSeek-app conversations. | Origin attribution conflict. | OPEN | Full original threads, IDs, and platform-origin evidence. |
| SRC-005 | محادثة 2.html identity | Reported as a 449-message ChatGPT Shared Chat export; exact mapping to Project chat 2 is unverified. | Export-to-thread mapping gap. | OPEN | Exact export metadata and matching source thread ID. |
| SRC-006 | Network Audit original | Reconciliation notes describe Network topics, but the complete original audit is not present in recovered material. | Derived material / missing original. | SOURCE PARTIAL | Complete original audit or authoritative export. |
| SRC-007 | Security Audit original | 5.txt is an initial reconciliation, not the complete original SECURITY_AUDIT.md. | Derived material / missing original. | SOURCE PARTIAL | Complete original audit and version history. |
| SRC-008 | Library raw bytes and ZIP contents | Stage 0 Negative Attestation limits current Library evidence to metadata/extracted text for specified records. | Evidence-access limitation. | BINDING LIMITATION | Authorized raw-byte acquisition for exact records and hashes. |
| SRC-009 | Project Library/local-only status | Some reports describe outputs as local-only while Library metadata now shows similarly named records. | Provenance/operation history gap. | OPEN | Exact byte identity and authenticated upload/audit event; do not infer uploader. |
| SRC-010 | Unity ZIP branch presence and checksum | PR #5 body and SHA manifest state the corrected ZIP is not committed; the PR changed-file list and exact branch path show a ZIP present with Git blob 319a584046d3ece7ce74242233015738758ec210. Computed SHA-256 for the exact blob bytes is 5f92947c8b23d1ee15d7c744fb62ef0cf7af361bfdda277fcdaeff61dab51ac4, while the manifest declares 351190c6789b0a146be0978606c831f48b951509e7f353bb3b469374eb9b15d9. | Branch/PR description conflict plus checksum mismatch; package lineage/manifest inconsistency. | OPEN — PRESERVE BOTH HASHES | Identify the archive version intended by the manifest, correct the checksum record in a dedicated reviewed change, and reconcile the PR body; do not overwrite source history or infer uploader. |
| SRC-010 | Unity ZIP branch presence and checksum | PR #5 body and SHA manifest state the corrected ZIP is not committed; the PR changed-file list and exact branch path show a ZIP present with Git blob 319a584046d3ece7ce74242233015738758ec210. Computed SHA-256 for the exact blob bytes is 5f92947c8b23d1ee15d7c744fb62ef0cf7af361bfdda277fcdaeff61dab51ac4, while the manifest declares 351190c6789b0a146be0978606c831f48b951509e7f353bb3b469374eb9b15d9. | Branch/PR description conflict plus checksum mismatch; package lineage/manifest inconsistency. | OPEN — PRESERVE BOTH HASHES | Identify the archive version intended by the manifest, correct the checksum record in a dedicated reviewed change, and reconcile the PR body; do not overwrite source history or infer uploader. |

## 3. Requirement and identifier conflicts

| ID | Subject | Observed values / source claims | Classification | State | Closure evidence |
|---|---|---|---|---|---|
| GOV-001 / CONFLICT-01 | TBD-37 owner | Main TBD-21…58 table reports Core Owner / Application Owner; Binding Clarifications reports Core Owner. | Owner-field contradiction. | OPEN | Original approved requirement source or explicit owner correction. |
| GOV-002 / CONFLICT-02 | TBD-45 responsibility | Sources list Core Owner / Application Owner; primary owner and split are not settled. | Responsibility allocation incomplete. | OPEN | Explicit primary/supporting owner decision. |
| GOV-003 | RISK-013 identity | Same identifier is used for Suffering Transfer Strategic Dominance and Suffering Transfer Abuse. | Identifier collision; could be separate risk records. | OPEN | Both original risk statements and approved mapping. |
| GOV-004 | TBD-01…20 source status | Register marks 01…20 pending source while recovered D04 event mapping is reported for turns 284–295. | Evidence-type mismatch, not automatic source resolution. | PARTIAL | Match original requirement text and user acceptance separately for each item. |
| GOV-005 | TBD-53 subtask expansion | TBD-53 is reported to require 54 subtask mappings; individual mapping details remain unavailable. | Incomplete decomposition. | OPEN | Exact 54 source mappings and IDs. |
| GOV-006 | 109-record matrix completeness | Matrix reports 109 records, 30 groups, 24 pending-source entries; primary group completeness remains pending. | Dataset completeness limitation. | PARTIAL | Source-complete row review and unresolved group assignments. |

## 4. Design and architecture conflicts

| ID | Subject | Main / source position | Branch-specific position | Classification | State |
|---|---|---|---|---|---|
| DESIGN-001 | Player lifecycle at zero Vitality | Main v2.1 rules state Alive → Dead and no revival in MVP. | PR #6 branch ADR-007 says Alive → Spirit; explicit Resurrect and Eliminate; Eliminated terminal. | Material gameplay-contract conflict across branches. | OPEN — OWNER TRACE REQUIRED |
| DESIGN-002 | ADR-004 and ADR-007 relationship | Main ADR-004 remains PROPOSED FOR PLAYTEST. | Branch ADR-007 claims an earlier owner-approved lifecycle decision and says only conflicting v2.1 lifecycle text is superseded. | Distinct decision scopes require direct trace. | OPEN |
| DESIGN-003 | ADR-006 invariants | Main does not contain ADR-006. | PR #6 branch has ADR-006 as PROPOSED. | Branch-scope divergence. | OPEN — NOT ON MAIN |
| DESIGN-004 | ADR-008…015 policy | Main ADR set ends at ADR-004. | PR #6 branch adds ADR-008…015; many remain PROPOSED. | Branch-only architecture proposals. | OPEN — OWNER REVIEW REQUIRED |
| DESIGN-005 | C1 / B3 / A1×C / S-C1 design claims | Recovered reports summarize conditional or exploratory architecture directions. | Original decision messages/source traces are incomplete. | Report-derived proposal, not final architecture approval. | SOURCE TRACE PENDING |
| DESIGN-006 | RequestId / CommandSequenceId / CommandId / effect idempotency | Trust Boundaries and Suffering Transfer refer to RequestId; inspected Core implements session-scoped CommandSequenceId; PR #6 branch ADR-015 proposes a stable CommandId independent of session sequence and remains PROPOSED. | No one-to-one mapping or retention/replay contract is approved or implemented in the inspected Core. | Identity and guarantee-boundary mapping gap (also DES-006 / CORE-REV-03). | OPEN | Define which identifier is created at which trust boundary, its scope, atomic reservation/result behavior, and reconnect/restart retention; do not equate sequence replay with effect idempotency.
| DESIGN-007 | ADR-015 internal status wording | ADR-015 header and main decision sections remain PROPOSED, while Option B in its alternatives table uses the word "معتمد". | Could mean preferred proposal or owner-approved alternative. | Internal status wording ambiguity; do not infer owner approval. | OPEN — CLARIFICATION REQUIRED | Align the wording with the actual owner approval event, or replace the ambiguous term with a clearly proposed/preferred label; no ADR edit is made by this review. |
| STAGE-001 | Stage 1 accepted vs Stage 1 partial | A supplied report records Stage 1 as ACCEPTED; PR #8 and Stage 1 review material describe source/provenance recovery as PARTIAL and do not issue a closure declaration. | Acceptance of a checkpoint/report may be different from Stage 1 exit/closure. | OPEN — SCOPE/MEANING RECONCILIATION | Recover the exact owner acceptance wording and distinguish checkpoint acceptance, deliverable acceptance, and formal stage closure. No stage is closed by inference. |
| DESIGN-007 | Dead/Spirit terminology in Collapse and Spirit | Main COLLAPSE_SYSTEM says Alive → Dead/Spirit; SPIRIT_SYSTEM describes Alive → Dead followed by Spirit. | One player lifecycle is not expressed consistently in main specs. | Ambiguous lifecycle wording, linked to DESIGN-001. | OPEN |
| DESIGN-008 | Endgame result taxonomy | RULE-05 groups Time Limit and Mutual Collapse; ENDGAME_SYSTEM names them as separate outcomes. | Three grouped result categories vs four named terminal outcomes. | Taxonomy/presentation ambiguity. | OPEN |
| DESIGN-009 | Combat-triggered Collapse | COMBAT_SYSTEM says apply Collapse if enabled, but trigger/stacking/scope are not specified in main rules. | Gameplay interaction is underspecified. | OPEN — DESIGN DECISION REQUIRED |
| ADR-REV-001 | ADR-005/ADR-007 original approval trace | Feature-branch ADRs state owner approval, while the inspected decision trace does not provide the exact original user message/thread reference. | Approval status recorded in branch ADR; primary event mapping incomplete. | OPEN — SOURCE TRACE PENDING |
| ADR-REV-002 | ADR-015 status language | ADR-015 header is PROPOSED, while the alternatives table marks Option B as "معتمد". | Internal status wording ambiguity. | OPEN — DOCUMENT CLARIFICATION |
| ADR-REV-003 | Session cleanup and in-flight commands | ADR-013 and SessionCleanupService require the caller to wait for dequeued/in-flight command processing; queue does not track in-flight handlers. | Explicit application integration obligation, not a Core-enforced invariant. | OPEN — INTEGRATION REQUIREMENT |
| ADR-REV-004 | CommandId implementation | ADR-015 proposes stable CommandId and in-memory result idempotency but explicitly says no code change; inspected Core identifiers/services show no CommandId/result registry. | Proposed guarantee not implemented in inspected Core scope. | RECORDED — NOT IMPLEMENTED |
| ADR-REV-005 | ActorId/VictimId numeric namespace | ADR-006 rejects a BetrayalEvent when ActorId.Value equals VictimId.Value; ADR-014 leaves permitted victim categories TBD. | Equality across two typed IDs assumes the same numeric entity namespace; that assumption depends on the unresolved victim domain. | OPEN — DOMAIN SCOPE REQUIRED |
| CORE-REV-001 | Session cleanup and in-flight handler | Cleanup checks queued commands and resets replay state, but queue does not track commands already dequeued and still executing. | Caller/application must serialize completion before cleanup; no Core-enforced in-flight barrier. | OPEN — INTEGRATION REQUIREMENT |
| CORE-REV-002 | Trusted connection/session binding | Core admission checks authenticated session and ActorId binding but does not authenticate credentials or prove transport ownership of SessionId. | Explicit external responsibility; no Net adapter reviewed in this branch slice. | NOT IMPLEMENTED IN CORE |
| CORE-REV-003 | BetrayalLedger retention | Ledger retains accepted events in an in-memory dictionary; no explicit cap/clear is exposed. | Lifetime and maximum event volume depend on per-match composition ownership. | OPEN — RESOURCE LIFECYCLE |
| CORE-REV-004 | Match-scoped composition enforcement | CoreComposition docs say one per match, but the API can create multiple match registries/lifecycles from one composition. | Convention is documented, not mechanically enforced. | OPEN — COMPOSITION INVARIANT |
| CORE-REV-005 | MatchRosterSnapshot scope | Snapshot includes phase, tick, revision and roster lifecycle/vitality only; excludes world, Debuff, Collapse, vote and connection state. | A bounded roster projection, not a full world/match snapshot. | DOCUMENTED LIMITATION |
| CORE-REV-006 | Snapshot atomicity and StateRevision ownership | Builder reads player accounts sequentially under a single-writer assumption; StateRevision is a primitive with no central mutation/revision coordinator. | Atomic capture/revision semantics need an application-level contract before replication. | OPEN — INTEGRATION DESIGN |
| CORE-REV-007 | Atomic Suffering Transfer | BetrayalLedger records events/scores but does not move Debuffs or transact source/target state. No transfer service/model found in inspected Core scope. | Gameplay transaction remains absent from inspected Core slice. | NOT IMPLEMENTED |
| UNITY-REV-001 | FishNet adapter type and messaging pattern | PR #5 ZIP FishNetServerAdapter derives from MonoBehaviour and registers Broadcast handlers; the project network contract requires FishNet NetworkBehaviour and approved ServerRpc/ObserversRpc patterns. | Source-package architecture deviation; package is branch-scoped and FishNet API compatibility is not verified. | OPEN — ADR/IMPLEMENTATION REVIEW | Rework adapter shape or obtain explicit ADR approval for an alternative; compile against the actual pinned FishNet package and add integration tests. |
| UNITY-REV-002 | Betrayal versus Suffering Transfer | PR #5 ZIP BetrayalService calls victimState.ApplyDamage(_rules.VitalityDamage); no Debuff Move/source removal/target capacity/transfer transaction appears in that service. | Material gameplay-contract mismatch with RULE-02 and Suffering Transfer spec. | NOT IMPLEMENTED — TRANSFER CONTRACT ABSENT IN PACKAGE | Implement the approved full Move/atomic transaction only after domain contract and Core tests are agreed; do not label direct damage as Transfer. |
| UNITY-REV-003 | Movement commands per tick | ZIP MovementCommandIngress allows a queue sized at MaximumCommandsPerTick × 4; CorePipeline drains the entire queue in a while loop; MovementService applies FixedDeltaTime for each command. | Queue storage bound is not a per-tick processing cap; multiple accepted commands can produce multiple fixed-delta movements in one server tick. | OPEN — POTENTIAL MOVEMENT MULTIPLICATION | Add a true server-side per-tick execution budget or aggregate input semantics and regression tests for command bursts. |
| UNITY-REV-004 | Disconnect and reconnect policy | ZIP SessionLifecycleService.Disconnect removes presence, participation, PlayerState and match membership immediately; CoreServerSessionBridge removes the connection/session mapping. | Immediate removal is implemented in this branch package; no reconnect grace/continuity is established. | OPEN — GAMEPLAY/SESSION POLICY | Decide desired disconnect/reconnect behavior and test identity continuity, timeout and match membership before Alpha. |
| UNITY-REV-005 | Command outcome publication | ZIP CorePipeline produces CommandOutcome values, but inspected FishNetServerAdapter publishes CoreSnapshotBroadcast only; no CommandOutcomeBroadcast publication is present in the inspected path. | Client command-result feedback path incomplete. | NOT IMPLEMENTED IN INSPECTED ADAPTER | Define rejection/success delivery semantics and implement/verify server-to-client publication with authorization and tests. |
| UNITY-REV-006 | Unity package test coverage | ZIP contains 5 EditMode and 8 Net test files; no MovementServiceTests, BetrayalServiceTests or CorePipelineTests appears in the archive listing. | Critical movement/transfer/pipeline paths lack corresponding named fixtures in this package snapshot. | NOT ESTABLISHED | Add NUnit/EditMode/PlayMode tests for movement limits, transfer atomicity, pipeline ordering and network ingress before implementation claims. |
| UNITY-REV-007 | Credential authentication responsibility | ZIP CoreServerSessionBridge authenticates the Core session after binding; FishNetServerAdapter obtains PlayerId through IServerIdentityResolver, but no concrete credential/account identity provider is included. | Authentication assurance is external to this package and depends on host integration. | NOT ESTABLISHED | Define and test trusted identity resolution/credential validation in the host application; do not infer credential verification from Session.IsAuthenticated alone. |
| UNITY-REV-008 | Cross-type command ordering | ZIP CommandSequenceTracker stores separate Movement and Betrayal sequence dictionaries; CorePipeline drains Movement queue completely before Betrayal queue. | A single global session command order is not preserved across command types; same sequence number can be accepted once per type. | OPEN — ORDERING CONTRACT REQUIRED | Define global versus per-type sequence semantics and deterministic cross-command ordering; test interleaved commands. |
| UNITY-REV-009 | Movement collision and world constraints | ZIP MovementService updates planar position using input × speed × FixedDeltaTime; no map bounds, collision or navigation validation is present in the inspected service. | Deterministic vector movement only; not a complete authoritative movement simulation. | NOT IMPLEMENTED IN PACKAGE | Define server movement/world constraints and collision handling; add tests for bounds, obstruction and invalid movement. |
| UNITY-REV-010 | Sprint eligibility | ZIP MovementService chooses SprintSpeed directly from the client-provided Sprint flag. No server-side stamina/cooldown/eligibility policy is visible in the inspected path. | Whether sprint is unrestricted or constrained is not established by the inspected rules. | TBD — RULE POLICY | Confirm intended sprint policy; if constrained, enforce it server-side and test spoofed/overuse requests. |
| UNITY-REV-011 | Player lifecycle and match outcome model | ZIP PlayerState/VitalityState expose only IsAlive = Vitality > 0 and clamp damage to zero; MatchLifecycleState contains Waiting/Running/Finished/Closed. No Spirit/Eliminated/Resurrect or vote/endgame phase model appears in the inspected archive inventory. | PR #5 package conflicts with the PR #6 branch ADR-007 Alive/Spirit/Eliminated lifecycle and the proposed endgame model; this is a separate branch implementation. | OPEN — CONTRACT RECONCILIATION REQUIRED | Do not integrate package as-is. Select canonical lifecycle authority, map all states/transitions, and add lifecycle/endgame tests before integration. |
| CI-REV-001 | SecurityCodeScan SARIF ingestion | Inspected workflow preserves SARIF as an artifact but does not upload it to GitHub Code Scanning. | Findings may be retained as artifacts without appearing as repository code-scanning alerts. | REVIEW REQUIRED |
| CI-REV-002 | Dependabot nested NuGet coverage | Dependabot NuGet directory is repository root while project files are nested. | Actual nested project discovery/update behavior not established from YAML alone. | VERIFY WITH ACTUAL PR ACTIVITY |
| CI-REV-003 | Unity/Net/Android/Linux CI coverage | Current Core CI scans/builds standalone Core; no Unity/FishNet adapters or Android/Linux builds exist in inspected branch scope. | Product release pipeline remains unbuilt. | NOT IMPLEMENTED |

## 5. Repository and delivery conflicts

| ID | Subject | Observed facts | Classification | State | Required action |
|---|---|---|---|---|---|
| REPO-001 | PR #6 | Open Draft; base main at 3dd9cfc...; head feature/core-lifecycle-resolution at 2820285...; 94 changed files; not merged. | Branch-scoped implementation, not main delivery. | OPEN | Review and owner approval before merge. |
| REPO-002 | PR #7 | Merged into feature/core-lifecycle-resolution, not into main. | Feature-branch integration. | RECORDED | Preserve target branch in status claims. |
| REPO-003 | PR #8 | Open Draft; 7 documentation files; based on main. | Source/provenance package candidate. | OPEN |
| REPO-004 | PR #9 | Open Draft; 14 documentation files; based on Core feature branch. | Overlapping package with different base and content. | OPEN |
| REPO-005 | PR #8 vs #9 | Both modify docs README and add source/provenance/document-control functions at different paths. | Competing package layouts and bases. | OPEN — DO NOT MERGE AS-IS |
| REPO-006 | PR #10 | Closed without merge and explicitly superseded. | Historical-only branch/PR. | SUPERSEDED |
| REPO-007 | Full repository tree | Connector can fetch known files and compare refs but did not expose a complete recursive tree listing in this session. | Inventory completeness limitation. | OPEN | Do not call changed-file lists a complete tree. |
| REPO-008 | Unity source package delivery | A 94-entry ZIP with 81 C# files and 4 asmdefs is committed on review/unity-source-package-2026-11-01 and included in PR #5's changed-file list, despite the PR body/manifest saying it is not committed. The archive lacks Unity Packages/ProjectSettings/scenes. | Branch-scoped Assets/source package; not a complete Unity project; contradictory delivery record. | OPEN — SOURCE PACKAGE PRESENT / INCOMPLETE | Review and verify archive contents/checksum, pin real Unity/FishNet packages, then import/compile and run tests before implementation claims. Keep it separate from main until reviewed/merged. |
| REPO-008 | Unity source package delivery | A 94-entry ZIP with 81 C# files and 4 asmdefs is committed on review/unity-source-package-2026-11-01 and included in PR #5's changed-file list, despite the PR body/manifest saying it is not committed. The archive lacks Unity Packages/ProjectSettings/scenes. | Branch-scoped Assets/source package; not a complete Unity project; contradictory delivery record. | OPEN — SOURCE PACKAGE PRESENT / INCOMPLETE | Review and verify archive contents/checksum, pin real Unity/FishNet packages, then import/compile and run tests before implementation claims. Keep it separate from main until reviewed/merged. |

## 6. CI and evidence conflicts

| ID | Subject | Recovered evidence | Classification | State |
|---|---|---|---|---|
| EVID-001 | 148 tests | Run 41331400978, commit 94c5789b0e82493290ca85133fc692e83ffd9884; job log reports 148 passed, 0 failed, 0 skipped. | Historical Core CI snapshot. | TESTED |
| EVID-002 | 177 tests | Run 41332759086, commit 26a77dcf1394d35d3db48ea0f5b7cde4c118998f; job log reports 177 passed, 0 failed, 0 skipped. | Historical Core CI snapshot. | TESTED |
| EVID-003 | 177 run-ID discrepancy | Older report cites run 41332755494; fetched workflow association and job log for the stated commit identify 41332759086. | Run-ID mismatch in historical documentation. | OPEN — HISTORICAL REFERENCE |
| EVID-004 | 181 tests | Run 41333575156, commit 8a193190fb5f2e10c05773fb0789171011bfd2eb; job log reports 181 passed, 0 failed, 0 skipped. | Historical Core CI snapshot. | TESTED |
| EVID-005 | 187 tests | Run 42383513646, commit 2820285021b1a3d7ae9acd953b3bb65677c33512; job log reports 187 passed, 0 failed, 0 skipped. | Historical Core CI snapshot. | TESTED |
| EVID-006 | CI count changes | Counts differ across commits/workflow runs. | Versioned snapshots, not a cumulative score. | RECORDED |
| EVID-007 | Unity/FishNet/Android/Linux validation | No Unity project/package lock or FishNet adapter/integration test evidence established on main. | Integration evidence gap. | NOT VERIFIED |
| EVID-008 | ADR-012/ADR-013 cited Core CI | Run 41332989910 on commit c33faf84aa39856d46181ccc1c9decbfba2777d2; job 124926820979 reports 181 passed, 0 failed, 0 skipped; artifact 13093814339. | Separate historical 181-test snapshot from run 41333575156 on a different commit. | TESTED |

All CI rows above are scoped to standalone Core and the named workflow runs. They do not establish end-to-end game readiness.

## 7. Cross-document design review references

The companion [Design Consistency Review](../audit/DESIGN_CONSISTENCY_REVIEW_2026-11-10.md) records findings DES-001…DES-015. The [ADR and Core Contract Review](../audit/ADR_RECONCILIATION_REVIEW_2026-11-10.md) records branch-specific ADR status, implementation and evidence boundaries. DES identifiers are review-finding IDs, not replacements for conflict IDs. DES-001 maps to DESIGN-001; DES-002 and DES-003 map to DESIGN-007; DES-004 maps to DESIGN-008; DES-006 maps to DESIGN-006; DES-007 maps to DESIGN-009.

## 8. Non-equivalence rules

Do not collapse the following into one record or guarantee:

- TBD-37 and TBD-45.
- Requirement, Decision, Threat, Risk, Finding, Technical Debt, and Evidence.
- Stage 0–5 and Phase 1–3.
- Authentication and Authorization.
- Connection identity, authenticated player identity, and SessionId.
- CommandSequence, CommandId, effect idempotency, reconnect protection, and restart protection.
- Immutable-after-capture and atomic snapshot capture.
- Core CI and Unity/FishNet integration verification.
- Project attachments and Library records.
- Original conversations, exports, derived reports, and repository copies.

## 9. Closure status

No open item in this register is closed by the current reconciliation commit. Resolved identity facts (for example, distinct files sharing a basename) do not resolve broader source completeness or provenance gaps.
