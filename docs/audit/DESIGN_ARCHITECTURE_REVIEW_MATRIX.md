# Design, Architecture, Network and Security Review Matrix

**Revision:** 2026-11-10  
**Decision Status:** PROPOSED — reconciliation findings; owner/ADR decisions remain unchanged  
**Evidence Maturity:** REVIEWED — main documentation snapshot and PR #6 feature-branch ADR/Core records  
**Scope:** Cross-domain consistency review of gameplay, technical architecture, network, security, quality, operations and delivery.

> This is a review matrix, not a replacement for domain specifications or an approval of new rules. The canonical documents remain their existing domain documents until an explicit path/authority decision is accepted.

## 1. Cross-domain review

| ID | Topic | Source documents / branch | Finding | Current evidence / implementation boundary | Required next action |
|---|---|---|---|---|---|
| XDOM-001 | Player lifecycle at zero Vitality | main RULES-01, COLLAPSE_SYSTEM, SPIRIT_SYSTEM, ADR-004 vs PR #6 branch ADR-007 | Main says Alive → Dead/no revival; Collapse says Dead/Spirit; Spirit says Dead then Spirit; ADR-007 says Alive → Spirit, explicit Resurrect/Eliminate, Eliminated terminal. | ADR-007 and updated Core are branch-scoped; PR #6 unmerged. Main specs remain conflicting. | Revalidate original owner approval event and accept the canonical transition matrix. Update RULES-01, Collapse, Spirit, GDD, glossary, acceptance criteria and tests in one controlled change. |
| XDOM-002 | Spirit participation versus elimination | ADR-007 on Core branch; main player states / Spirit spec | Spirit is a lifecycle state under ADR-007, while main specs use Dead/Spirit as a mixed state and sometimes treat spirit as post-death role. | Core lifecycle tests exist on feature branch; no Unity/FishNet lifecycle integration. | Separate PlayerLifecycle, ConnectionState, MatchMembership and presentation/spectator projection. Validate all state matrices. |
| XDOM-003 | Resurrection policy | ADR-007 on Core branch; RULES-01 main | ADR-007 describes explicit Resurrect from Spirit, but gameplay authorization, cost, cooldown, actor permissions and phase rules are TBD. Main says no revival in MVP. | Core primitive validates domain transition only; does not own permissions or gameplay policy. | Owner decision required before gameplay use; do not add cost/cooldown assumptions to Core. |
| XDOM-004 | Suffering Transfer atomicity | RULE-02, SUFFERING_TRANSFER_SYSTEM, DATA_ARCHITECTURE | Design requires a full Move, atomic server-side transaction, failure without partial mutation, and successful-transfer Betrayal. | PR #6 Core has admission/session/queue primitives, but transfer domain/application handler is not established as implemented. | Specify Debuff aggregate and transaction boundary, then implement pure Core contract and NUnit tests before Net adapter. |
| XDOM-005 | Transfer idempotency | SUFFERING_TRANSFER_SYSTEM, DATA_ARCHITECTURE, ADR-015 on feature branch | Some main docs say one logical application/idempotent events; command identity, sequence replay and durable effect idempotency are separate guarantees. ADR-015 is branch-scoped and PROPOSED. | Session sequence tracker and bounded admission queue are tested on Core branch; no proof of restart-safe effect idempotency. | Approve distinct guarantee boundaries and retention policy; test duplicate effects, reconnect and restart separately. |
| XDOM-006 | Betrayal thresholds | BETRAYAL_SYSTEM, RULE-02/RULE-05 | 3/5/7+ threshold effects are legacy hypotheses; stacking, persistence, caps and debuff-removal effects are unresolved. | No accepted balancing evidence. | Keep proposed values visibly disabled/experimental until playtest decision. |
| XDOM-007 | Combat scope | COMBAT_SYSTEM, MVP_SCOPE, ACCEPTANCE_CRITERIA | Weapon values are prototype assumptions; reload, hitboxes, friendly fire, lag compensation and damage/Collapse interactions remain TBD. | No verified Unity/Net Combat implementation. | Reduce to minimal testable MVP combat contract or explicitly defer. Define validation tests before implementation. |
| XDOM-008 | Endgame and lifecycle coupling | ENDGAME_SYSTEM, RULES-03/RULES-05, ADR-007 | Vote eligibility is based on three Alive players; the interpretation of Spirit/Eliminated/disconnect must remain consistent after lifecycle resolution. | Design only; no integrated match-outcome implementation established. | Define exact eligibility and transition ordering; add deterministic tests for simultaneous death, vote timeout and disconnect. |
| XDOM-009 | Server authority | ADR-002 on main; Network Architecture; PR #6 Core branch | Server authority is APPROVED as a design principle; this does not verify all command ingress or prevent all client-side state spoofing. | Standalone Core tests cover selected primitives only; network adapters not established. | Map every client command to ServerRpc validation, server-owned identity, application handler, state mutation and ObserversRpc/SyncVar replication; add abuse regression tests. |
| XDOM-010 | FishNet version and APIs | ADR-001, Technical/Network Architecture, PR #6 technical master | FishNet is proposed; exact package version is not pinned in a Unity package lock. 4.7.4R remains an assumption. | No Unity project/package lock or FishNet adapter compilation evidence on main. | Verify the exact package release and APIs against the installed package before writing Net code. |
| XDOM-011 | Core/Unity/Net boundaries | Technical Architecture; ADR-005 on PR #6 branch | Desired layering is Client Presentation → Network Adapter → Application → Domain Core → Infrastructure; Core must remain engine-independent. | Core boundary scan passed on specified feature-branch CI commits; Unity assembly boundary not yet present on main. | Keep Core pure C#; define asmdefs only after an actual Unity project/package set exists. |
| XDOM-012 | Tick and ordering policy | Network Architecture; Core technical master; ADR-008+ | Monotonic server time, sequence, queue, tick rates and late-command policy are distinct. Values are proposed or policy TBD. | ServerTick/StateRevision and queue primitives are tested on feature branch; Unity/FishNet tick wiring not implemented. | Approve tick/order policy through ADR before runtime integration. Use FishNet tick events; do not use Update for network state logic. |
| XDOM-013 | Authentication and authorization | TRUST_BOUNDARIES, THREAT_MODEL, Security Architecture; ADR-012 branch | Authentication, authorization, connection identity and SessionId are not interchangeable. | Core authenticated-admission service accepts authenticated session context; credential validation/connection binding remain outside it. | Define server authentication provider/contract, identity binding and operation permission matrix before network commands. |
| XDOM-014 | Session cleanup/revocation | ADR-010/011/013 on feature branch; Network/Security | Pending/authenticated/disconnected/closed lifecycle is distinct from reconnect and identity persistence. | Core session lifecycle/registry/cleanup primitives have CI test evidence on feature branch; transport ownership, credentials, timeout orchestration remain incomplete. | Approve session policy; test late command rejection, cleanup, reconnect and process restart separately. |
| XDOM-015 | Snapshot correctness | DATA_ARCHITECTURE and PR #6 Core branch technical master | Immutable-after-capture is not the same as atomic snapshot capture; revision semantics and client visibility are separate. | Snapshot construction capability is reported/tested on feature branch; full live match/world snapshots and transport replication not verified. | Specify capture owner, atomicity, revision, projection and privacy contract; add Core and integration tests. |
| XDOM-016 | Performance targets | PERFORMANCE_BUDGET, PERFORMANCE_TEST_PROTOCOL, DEVICE_COMPATIBILITY | 30 FPS, 400 MB, APK size and reference phone are targets/hypotheses, not measured baselines. | No Android build/device measurement evidence in reviewed main scope. | Define target device SKUs; capture reproducible CPU/GPU/PSS/thermal/battery/network/server measurements before adopting thresholds. |
| XDOM-017 | Playtest acceptance | ADR-004, PLAYTEST_PLAN | At least 10 valid documented sessions are the proposed gate; count alone does not automatically approve rules. | No completed session dataset in reviewed repository scope. | Record each session with build SHA/ruleset/metrics; decide each rule only after data review. |
| XDOM-018 | Backend/hosting | ADR-003, BACKEND_AND_HOSTING, DATA_ARCHITECTURE | Provider and permanent data services remain proposed/deferred; FishNet is transport, not a hosting provider. | No provider decision or production operational evidence. | Compare provider/hosting after a working Android/Linux prototype and RTT/cost measurements. |
| XDOM-019 | Deployment and operations | DEPLOYMENT_ARCHITECTURE, BUILD_AND_RELEASE, MONITORING_AND_LOGGING, BACKUP_AND_RECOVERY | CI/release, monitoring thresholds, backups/RPO/RTO and rollback are desired procedures, not currently operationally proven systems. | No production runtime evidence. | Turn each operational requirement into a rehearsed runbook and evidence artifact before production. |
| XDOM-020 | Privacy and security evidence | DATA_PRIVACY, SECRETS_MANAGEMENT, THREAT_MODEL, SECURITY_TEST_PLAN | Principles are written, but data retention, threat-to-test mapping, secrets operation and security event retention remain incomplete. | Security scanning runs exist on Core branch; they do not establish full product security. | Complete data inventory, threat model, secret scanning and per-threat security regression evidence. |

## 2. Responsibility boundary

| Layer | Owns | Must not own |
|---|---|---|
| Domain Core | Pure state transitions, invariants, value objects and deterministic rule logic | UnityEngine, FishNet APIs, transport connections, UI, credential provider |
| Application/server services | Authentication context validation, authorization policy, command validation, orchestration and atomic domain operations | Client-authoritative results or presentation |
| Net/FishNet | Transport, RPC ingress/egress, serialization, connection event adaptation and server tick integration | Gameplay truth or client-supplied state |
| Unity presentation | Input-to-intent conversion, view/projection and UI | Authoritative Vitality, Betrayal, voting or match outcome |
| Infrastructure/Operations | Build, deployment, secrets, logs, monitoring, backup and recovery | Changing gameplay rules without ADR/owner approval |
| Governance | Source lineage, decisions, requirements, conflicts and evidence boundaries | Claiming runtime behavior from documents alone |

## 3. Required traceability chain

For each requirement and material security invariant:

`Source → Requirement ID → Decision/ADR → Control/Owner → Core Contract → Application Handler → Net/Unity Adapter → Test ID → CI/Runtime Artifact → Review/Acceptance`

Missing nodes must remain explicitly TBD or NOT IMPLEMENTED. A source mapping or test name does not imply that downstream implementation exists.

## 4. Implementation order — proposed

1. Resolve lifecycle semantics and the canonical player state model before integrating gameplay systems.
2. Close the gameplay contracts for Vitality/Collapse, Transfer/Betrayal and Endgame through owner review and rule-specific tests.
3. Complete Core domain/application services for movement, Debuffs, transfer atomicity, snapshots and outcome resolution.
4. Create the real Unity project and pin actual editor/package versions; then write FishNet adapters against the installed API.
5. Build authentication/session integration, network impairment tests, security regression suite and Dedicated Server runtime.
6. Produce Android and Linux builds, measure target hardware, then conduct the documented playtest and Alpha gates.

## 5. Status

| Area | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| ADR-002 server-authority principle | APPROVED | DOCUMENTED | Implementation partial/branch-scoped |
| ADR-004 gameplay baseline | PROPOSED FOR PLAYTEST | DOCUMENTED | Playtest evidence pending |
| ADR-007 lifecycle on PR #6 branch | Marked APPROVED in that branch | TESTED in standalone Core scope | Original approval trace / main reconciliation open |
| Core standalone slice on PR #6 branch | — | IMPLEMENTED / TESTED | Not merged to main |
| Unity/FishNet integration | PROPOSED | — | NOT IMPLEMENTED / NOT VERIFIED |
| Android/Linux release | PROPOSED | — | NOT VERIFIED |
| Production security and operations | PROPOSED | — | NOT ESTABLISHED |
