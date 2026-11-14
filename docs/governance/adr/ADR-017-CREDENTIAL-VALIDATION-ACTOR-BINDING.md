# ADR-017 — Credential Validation and Actor Binding Boundary

**Status:** PROPOSED — boundary requirements documented; no identity provider, credential format or authentication service is selected  
**Date:** 2026-11-14  
**Depends on:** ADR-002, ADR-010, ADR-016  
**Related Core API:** `ISessionLifecycle.TryAuthenticate(ActorId)`, `IConnectionSessionRegistry`

## Context

The trust boundary treats the client and public network as untrusted. ADR-002 requires the server to authenticate the sender and derive gameplay identity from trusted server state. ADR-010 defines a Pending session and permits only trusted server authentication code to call `TryAuthenticate` after credentials and identity have been validated outside Core.

P1.1 adds a bounded transport-neutral mapping from a server-issued `TransportConnectionId` to a registered `SessionId`. That mapping identifies which pending session belongs to a connection; it does not prove who controls the account or which in-match ActorId should be assigned.

The current Core method `SessionLifecycle.TryAuthenticate(ActorId)` accepts an ActorId parameter and changes Pending to Authenticated. It does not validate a credential, account, token, device, or external identity. This is intentional: Core must remain independent of secrets and provider-specific authentication.

## Proposed boundary

1. The trusted transport adapter assigns an opaque, server-issued `TransportConnectionId`; it does not accept a client-supplied connection ID as proof of ownership.
2. The application resolves the connection through `IConnectionSessionRegistry` and obtains the associated registered session. Authentication may proceed only while that session is Pending.
3. An application-layer credential validator, outside deterministic Core, validates the supplied credential with the chosen identity system. It returns a trusted authentication principal/result, not a client-asserted identity.
4. The application resolves the trusted principal to an account identity and then, separately, to a server-owned in-match `ActorId` when match admission rules permit. An account identifier is not automatically an ActorId; the mapping and lifetime must be explicit.
5. Only after credential validation and server-side ActorId assignment succeed may trusted application code call `ISessionLifecycle.TryAuthenticate(actorId)`.
6. Failed, expired, revoked, malformed or unverifiable credentials must never transition the session to Authenticated and must never allow a client-selected ActorId to be bound.
7. Identity-provider timeouts, malformed responses, invalid signatures, unavailable services and unexpected exceptions fail closed. They must not partially mutate match state, reserve an actor indefinitely or leave a session authenticated.
8. Core stores no raw credentials, access/refresh tokens, password hashes, provider SDK objects, account secrets, network objects or external identity records.
9. Credentials and tokens must not appear in logs, metrics, exception messages, crash reports or test artifacts. Logs use a privacy-reviewed opaque correlation ID and redaction policy.
10. Rate limiting, credential retry/lockout, challenge/MFA, token expiry/revocation, refresh, account recovery and provider availability policy belong to the server application/security boundary, not `SessionLifecycle`.
11. Reconnect and concurrent-session policy remain governed by separate owner decisions. This ADR does not override or silently approve ADR-010's still-PROPOSED reconnect wording or ADR-011's multi-session TBD.
12. Core remains synchronous/deterministic with respect to identity decisions: any network/provider I/O and asynchronous timeout/retry handling occur before the trusted application invokes Core state transitions.

## Explicit non-decisions

This ADR does not select:
- An identity provider, OAuth/OIDC implementation, platform account system, guest flow or custom account backend.
- Password, bearer-token, signed-ticket, device-attestation or other credential formats.
- Account identifier format, storage model, token signing keys, rotation or provider SDK.
- Whether accounts are global, per-platform or guest/temporary.
- How a validated account is assigned to an ActorId, how long that ActorId lives, or whether an ActorId may move across matches.
- One active session per account/ActorId versus multiple concurrent sessions.
- Reconnect grace period, credential refresh, session expiry, revocation propagation or persistence.
- Failed-auth retry count, lockout thresholds, rate limits, challenge/MFA or abuse response.
- Privacy retention periods and legal obligations for selected markets.

Each item requires owner/security review and, where appropriate, a dedicated ADR before implementation.

## Required failure invariants

- No valid connection-to-Pending-session mapping: reject; do not create an authenticated session.
- Invalid/expired/revoked credential or untrusted provider result: do not call `TryAuthenticate`.
- Client-supplied account/ActorId mismatch: ignore the asserted identity and reject or resolve only from trusted provider data.
- Provider failure/timeout: fail closed with no partial authoritative match-state mutation.
- Repeated authentication on an already Authenticated, Disconnected or Closed session: reject without replacing ActorId.
- Authentication success: the bound session's ActorId is exactly the server-resolved ActorId.
- Secret/token data is absent from Core memory contracts and diagnostic output.

## Required verification before implementation is accepted

1. Unit tests for valid, invalid, expired, revoked, malformed and mismatched credentials using a deterministic fake identity provider.
2. Tests for provider timeout/unavailability and exceptions, proving no partial Actor/session/match mutation.
3. Tests proving client-supplied ActorId/account identifiers cannot override the authenticated provider principal or server assignment.
4. Tests proving one-time session authentication, duplicate-attempt rejection and immutable ActorId association.
5. Tests proving authentication cannot proceed through an unbound connection or a non-Pending session.
6. Redaction tests for logs, metrics and exception paths.
7. Rate-limit and retry tests after their limits are explicitly approved.
8. Integration tests against the selected provider and actual server transport, including token expiry/revocation and connection disconnect during authentication.
9. Security review of secret storage, key rotation, replay, account takeover, session theft, and privacy/retention.

## Implementation status and evidence boundary

No credential validator, identity provider, account store, ActorId allocator/resolver or transport authentication flow is implemented by this ADR. The P1.1 Core mapping contract is separately implemented and CI-verified in PR #26, but PR #26 remains unmerged and does not test an actual transport adapter. ADR-017 remains PROPOSED until the owner selects the identity and account-to-Actor model and the application design is reviewed.
