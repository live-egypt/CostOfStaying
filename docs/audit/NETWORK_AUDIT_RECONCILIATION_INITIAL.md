# Network Audit Reconciliation — Initial

**Revision:** 2026-11-10  
**Decision Status:** PROPOSED — findings and mapping require source completion  
**Evidence Maturity:** REVIEWED — supplied reconciliation records and repository documents  
**Source boundary:** The complete original Network Audit was not recovered in the available source set.

## 1. Purpose and limitation

This document consolidates the network-related findings visible in supplied reconciliation material. It is not a replacement for the missing original Network Audit and is not a claim that every network issue exists in the current code.

The source material identifies unresolved design/verification areas. These are retained as open review items until the original audit, exact code, package configuration, and reproducible tests are available.

## 2. Initial network review map

| Area | What the source material establishes | Current state |
|---|---|---|
| Transport / Connection | Relationship between transport connection and authenticated session needs a concrete contract. | OPEN |
| Session Lifecycle | Must reconcile connection events, SessionId, disconnect, cleanup, revocation, and reconnect with ADR-010/ADR-013 on the Core branch. | OPEN |
| Command Ordering | Transport sequence/order must remain distinct from stable command identity. | OPEN |
| Replay Protection | Session-scoped sequence tracking is not equivalent to command-result idempotency or restart protection. | OPEN |
| Server Authority | Design declares the server authoritative; enforcement must be demonstrated by tests. | NOT VERIFIED end-to-end |
| Snapshot Consistency | Immutable-after-capture and atomic snapshot capture are different guarantees; capture/revision semantics remain unresolved. | OPEN |
| Disconnect / Reconnect | Identity continuity, expiry, ownership and pending-command behavior require explicit policy and tests. | OPEN |
| FishNet Integration | Actual resolved package version, APIs, serialization and runtime adapters are not evidenced on main. | NOT VERIFIED |

## 3. Network ↔ Security ↔ Architecture responsibility map

| Concern | Network responsibility | Security responsibility | Architecture/governance responsibility |
|---|---|---|---|
| Connection identity | Supply trusted connection context | Prevent identity spoofing | Define identity ownership and lifecycle |
| Session binding | Bind transport context to server-owned SessionId | Reject cross-session use | Define session contract and cleanup |
| Command admission | Deliver an intent to server ingress | Validate identity and authorization | Define admission/service boundaries |
| Ordering | Handle delivery order/sequence | Detect suspicious replay | Define CommandId and sequence semantics |
| Idempotency | Transport does not guarantee one-time effects | Prevent duplicate effects | Define durable scope and atomic reservation |
| Revocation | Reject late traffic | Reject revoked sessions | Define cleanup and terminal state |
| Snapshot integrity | Replicate approved projections | Prevent unauthorized disclosure/tampering | Define atomic capture and revision |
| Audit trail | Supply connection context | Define security events | Define event ownership and retention |

This is an initial responsibility map, not an approved architecture decision.

## 4. Non-equivalence constraints

Keep these concepts separate:
- Authentication is not Authorization.
- SessionId is not Player Identity.
- CommandSequenceId is not CommandId.
- Network delivery is not exactly-once execution.
- Sequence replay protection is not effect idempotency.
- Reconnect protection is not restart protection.
- Immutable After Capture is not Atomic Snapshot Capture.

## 5. Required network verification functions

The following are proposed validation functions; no claim is made that these tests currently exist or passed.

| ID | Proposed check | Pass condition |
|---|---|---|
| NET-FN-01 | Connection/session isolation | A connection cannot operate as another session. |
| NET-FN-02 | Authenticated ingress | Unauthenticated or revoked connections cannot mutate match state. |
| NET-FN-03 | Ordering and stale sequence | Old/out-of-order commands cannot roll state backwards. |
| NET-FN-04 | Command identity | A repeated CommandId cannot apply its effect twice within the approved retention boundary. |
| NET-FN-05 | Reconnect continuity | Reconnect follows an explicit, tested identity and expiry policy. |
| NET-FN-06 | Snapshot atomicity | Snapshot values represent one valid authoritative capture/revision. |
| NET-FN-07 | Malformed/oversized payloads | Invalid payloads are rejected before domain mutation. |
| NET-FN-08 | Network impairment | Behavior is tested under declared latency, jitter, loss and reordering conditions. |
| NET-FN-09 | FishNet API compatibility | Adapter compiles and runs against the exact resolved FishNet package. |

Do not assign numeric limits for latency, rate limits, payload size, tick rate, or reconnect grace until the requirements and target environment are approved and measured.

## 6. Related repository evidence

- main contains a proposed Network Architecture document describing server-authoritative behavior and proposed latency/loss test scenarios.
- PR #6 branch contains Core session and command-admission primitives and ADR-008 through ADR-015. Those files are not on main in the inspected snapshot.
- PR #6 remains open Draft and unmerged.
- FishNet 4.7.4R is an assumption in project context, not a verified resolved package version.
- No Unity project, package manifest/lock, Net adapter assemblies, or FishNet integration test results were established on main.

## 7. Source and evidence still required

| ID | Required item | Priority | State |
|---|---|---:|---|
| NET-SRC-01 | Complete original Network Audit and its source/version history | HIGH | SOURCE PENDING |
| NET-TR-01 | Original connection/session identity contract | HIGH | PENDING |
| NET-TR-02 | Command ordering, replay, idempotency and restart guarantees | HIGH | PENDING |
| NET-TR-03 | Atomic snapshot capture/revision contract | HIGH | PENDING |
| NET-TR-04 | Disconnect/reconnect and revocation policy | HIGH | PENDING |
| NET-FN-01 | Requirement-to-network test mapping | HIGH | PENDING |
| NET-FN-02 | Exact FishNet resolved package and reproducible adapter build | HIGH | NOT ESTABLISHED |

## 8. Status

| Area | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| This reconciliation | PROPOSED | REVIEWED | INITIAL / OPEN |
| Complete Network Audit source | — | PARTIAL | SOURCE PENDING |
| FishNet integration | PROPOSED | — | NOT IMPLEMENTED / NOT VERIFIED |
| Network integration tests | PROPOSED | — | NOT ESTABLISHED |
| Network readiness | — | — | NOT ASSESSED |

