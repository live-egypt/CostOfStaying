# Security Audit Reconciliation — Initial

**Revision:** 2026-11-10  
**Decision Status:** PROPOSED — findings and controls require source completion  
**Evidence Maturity:** REVIEWED — supplied initial Security Audit reconciliation only  
**Source boundary:** The complete original SECURITY_AUDIT.md was not recovered in the available source set.

## 1. Purpose and limitation

The supplied file 5.txt explicitly identifies itself as an initial Security Audit reconciliation. It is not the complete original audit. The findings below are verification gaps and source claims; they are not proof that all listed weaknesses are present in the current code.

## 2. Trust boundary model

The relevant trust-boundary questions are:
- What information originates from an untrusted client?
- What server-owned identity/session context is attached to a request?
- Which layer performs authentication, authorization, and gameplay validation?
- Where can state mutate, and which component owns that mutation?
- Which events are retained for incident review?
- Which data is safe to replicate to each client?

Do not treat a client-supplied health value, damage result, transfer result, vote result, energy value, PlayerId, SessionId, server tick, or state revision as authoritative.

## 3. Authority ownership — initial mapping

| Operation | Request source | Final decision owner | Required validation | Status |
|---|---|---|---|---|
| Vitality mutation | Client intent / server event | Server gameplay logic | Value bounds and valid lifecycle transition | PROPOSED |
| Collapse | Gameplay state / server event | Server gameplay logic | Eligibility and authoritative timing | PROPOSED |
| Suffering Transfer | Client command | Server gameplay logic | Eligibility, target, capacity, cooldown, atomicity | PROPOSED |
| Betrayal mutation | Validated gameplay event | Server gameplay logic | Rule and event context | PROPOSED |
| Spirit transition | Server gameplay state | Server lifecycle logic | Valid transition and authorization boundary | ADR conflict open |
| Vote admission | Client command | Server match logic | Eligibility, phase, duplicate vote | PROPOSED |
| Vote outcome | Validated votes | Server match logic | Counting and resolution | PROPOSED |
| Endgame | Match state / server event | Server match authority | End condition and terminal transition | PROPOSED |
| Match result | Match resolution | Server | Single consistent result | PROPOSED |
| Session authentication | Connection input | Server authentication layer | Credential and identity validation | TBD |
| Session revocation | Server session event | Server session authority | Reject subsequent commands | PROPOSED |
| Match lifecycle | Validated server event | Server match authority | Explicit state transitions | PROPOSED |

For each operation, distinguish:
- Design Ownership: who should own the decision?
- Implementation Ownership: which executable component enforces it?
- Verification Ownership: which reproducible test proves it cannot be bypassed?

## 4. Initial security findings

| ID | Finding | Why it matters | Source status |
|---|---|---|---|
| SEC-REC-01 | Original Security Audit not fully recovered | Coverage cannot be certified | OPEN |
| SEC-REC-02 | Authentication implementation not established by the supplied audit source | Connection identity cannot be assumed | NOT VERIFIED |
| SEC-REC-03 | Connection-to-Session binding not established | Cross-session identity confusion risk | NOT VERIFIED |
| SEC-REC-04 | Authorization boundary not demonstrated | Authentication alone does not grant gameplay permission | NOT VERIFIED |
| SEC-REC-05 | Command-result idempotency reported absent in earlier source findings | Sequence tracking alone does not prevent duplicate effects | REPORTED NOT IMPLEMENTED — RECHECK REQUIRED |
| SEC-REC-06 | Session revocation enforcement not demonstrated | Late commands may arrive after revocation | NOT VERIFIED |
| SEC-REC-07 | End-to-end authority verification coverage not established | Design does not prove enforcement | NOT VERIFIED |
| SEC-REC-08 | Security audit trail not demonstrated | Incident reconstruction may be incomplete | TBD |
| SEC-REC-09 | Secrets management evidence missing | Operational secrets protection is not established | TBD |
| SEC-REC-10 | Abuse resistance/rate limiting not demonstrated | Command ingress may be abused | NOT VERIFIED |

### 4.1 Important distinction: replay and idempotency

Keep these guarantees separate:
1. Sequence Replay Protection.
2. Command Result Idempotency.
3. Reconnect Protection.
4. Restart Protection.

The Core branch includes a session-scoped CommandSequenceTracker and command-admission primitives. That is evidence of a bounded sequence/admission implementation, not by itself proof of durable command-result idempotency across reconnects or server restarts. ADR-015 on the Core branch remains a separate policy/decision record and is not on main.

## 5. Security fitness functions — proposed, not executed here

| ID | Function | Pass condition |
|---|---|---|
| SF-01 | Client cannot mutate authoritative state | All client state changes pass through validated server commands. |
| SF-02 | Unauthorized command rejection | A caller without operation permission is rejected without mutation. |
| SF-03 | Duplicate command protection | A repeated command effect is not applied twice within the approved guarantee boundary. |
| SF-04 | Revoked-session rejection | Commands from revoked sessions are rejected. |
| SF-05 | Session identity isolation | A connection cannot act through another session. |
| SF-06 | Invalid lifecycle transition rejection | Invalid transitions fail without partial mutation. |
| SF-07 | Atomic gameplay mutation | Failed transaction leaves source, target, cooldown and ledger unchanged. |
| SF-08 | Sensitive-data isolation | Clients receive only approved projections. |
| SF-09 | Security-event traceability | A security event can be linked to session, command and outcome. |
| SF-10 | Abuse resistance | Invalid traffic does not prevent valid commands from being processed within the approved service objectives. |

These are proposed test functions. No numeric rate limits, retention periods, or service thresholds are assigned here.

## 6. Cross-domain responsibilities

| Topic | Security | Network | Architecture | Operations |
|---|---|---|---|---|
| Client authority | Defines what clients must not decide | Checks ingress path | Defines authoritative owner | Monitors bypass attempts |
| Authentication | Defines identity assurance | Supplies connection context | Defines session boundary | Manages secrets/configuration |
| Replay/idempotency | Defines protection guarantees | Observes transport sequence | Defines command identity and retention | Monitors anomalies |
| Session revocation | Defines rejection requirements | Handles late traffic | Defines lifecycle/cleanup | Preserves incident evidence |
| Rate limiting | Defines abuse-resistance objective | Measures request flow | Defines enforcement boundary | Tunes operations policy |
| Snapshot integrity | Protects against tampering/disclosure | Replicates projections | Defines atomic capture/revision | Monitors consistency |
| Secrets | Defines protection requirements | Does not own secrets | Defines dependency boundary | Owns operational storage/rotation |
| Audit trail | Defines security events | Adds connection context | Defines event semantics | Retains and monitors logs |

Each domain document should define its own responsibility and link to the authoritative definition, rather than duplicating the same risk statement.

## 7. Required source and verification work

| ID | Required item | Priority | State |
|---|---|---:|---|
| SEC-SRC-01 | Complete original Security Audit | HIGH | SOURCE PENDING |
| SEC-TRUST-01 | Match Trust Boundary Matrix to original design | HIGH | PENDING |
| SEC-AUTH-01 | Define Authentication owner and contract | HIGH | PENDING |
| SEC-AUTH-02 | Define Authorization owner for each gameplay command | HIGH | PENDING |
| SEC-SESSION-01 | Reconcile session security with ADR-010 and ADR-013 | HIGH | PENDING |
| SEC-REPLAY-01 | Map replay protection to command identity and ordering | HIGH | PENDING |
| SEC-ABUSE-01 | Define rate-limiting and abuse-resistance policy | MEDIUM | PENDING |
| SEC-LOG-01 | Define security event schema and retention | MEDIUM | PENDING |
| SEC-OPS-01 | Review secrets, supply chain and operational security | MEDIUM | PENDING |
| SEC-TEST-01 | Link each security requirement to a reproducible test | HIGH | PENDING |

## 8. Status

| Area | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| This initial reconciliation | PROPOSED | REVIEWED | OPEN |
| Complete Security Audit source | — | PARTIAL | SOURCE PENDING |
| Authentication/authorization integration | PROPOSED | — | NOT ESTABLISHED |
| End-to-end security verification | PROPOSED | — | NOT ESTABLISHED |
| Central Risk Register | PROPOSED | — | DEFERRED pending source/ID reconciliation |

