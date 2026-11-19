# Unity Source Package Review

**Review date:** 2026-11-01  
**Source:** `CostOfStaying_Final.zip` supplied in this conversation  
**Status:** Static review and proposed source corrections completed locally; Unity build and test verification NOT RUN.  
**Target assumptions:** Unity 6000.3 / Unity 6.3 LTS and FishNet 4.7.4R. These are assumptions only; no Unity project manifest, lock file, ProjectVersion.txt, or installed FishNet package was supplied.

## Inventory

- 81 C# files under `Assets/CostOfStaying`.
- Core: engine-independent domain/application logic.
- Net: Unity and FishNet integration adapters.
- Tests: EditMode and Net/PlayMode test assemblies.
- `Superseded/`: legacy server runtime files marked as superseded; excluded from runtime paths.

## Static corrections prepared in the reviewed package

1. `CoreServerCommandIngress` uses the actual `CommandEnvelopeFactory` and constructor, resolves the server-owned ClientId from the stored session, requires an authenticated Core session, and passes the authoritative server tick to both command queues. Client-supplied requested ticks are not trusted.
2. Movement and betrayal ingress calls pass the required `currentTick` argument.
3. `SessionLifecycleServiceTests` use the actual five-argument constructor and assert player/match cleanup after disconnect.
4. `ClientProjectionStateTests` import the Core namespace for `Result`.
5. `CommandSequenceTracker` enforces `MaximumSequenceJump`; a regression test covers excessive future sequence jumps.
6. `CorePipeline` exposes session sequence cleanup; the server session bridge invokes it on disconnect.
7. `CoreServerSessionBridge` authenticates the Core session after identity resolution and binding, rolling back if authentication fails.
8. `FishNetServerAdapter` subscribes to FishNet `TimeManager.OnTick`, executes the single composed CorePipeline, and publishes per-session snapshots to authenticated connections.
9. FishNet broadcast registration disables the built-in FishNet authentication gate because the adapter performs its own Core session authentication check. This must be reviewed with the host application's actual authentication flow.

## Blocking implementation gaps

- `BetrayalService` currently applies direct Vitality damage. The proposed v2.1 gameplay baseline describes a complete Debuff Move/Transfer with capacity, range, cooldown, and atomicity. This is a gameplay-contract mismatch.
- `MovementService` applies deterministic planar movement but does not validate collision/world geometry or authoritative navigation constraints.
- `CorePipeline` processes Movement and Betrayal only. Vitality decay, Collapse transitions/debuffs, Endgame voting, Spirit behavior, and match outcome resolution are not implemented here.
- The host application must call `AuthenticateConnection` through a trusted identity/authentication flow. A complete account/session authentication protocol is not included.
- Server-side command outcome delivery is not implemented; the adapter currently publishes snapshots.
- Disconnect currently removes the player from authoritative match state. The intended disconnect/reconnect gameplay policy remains unresolved.
- Superseded files are not runtime alternatives and must not be wired back into the active pipeline.

## Verification matrix

| Check | Status |
|---|---|
| File inventory | PASS — 81 C# files |
| Core references to UnityEngine/FishNet | PASS — none found by static scan |
| Duplicate type declarations | PASS — no actual duplicate types found; generic and non-generic Result are valid overloads |
| Known constructor/argument mismatches | Corrected in the local reviewed package; compiler not run |
| Unity compilation | NOT RUN |
| NUnit EditMode/PlayMode | NOT RUN |
| FishNet 4.7.4R compatibility | NOT VERIFIED against the installed package |
| Dedicated server/host integration | NOT RUN |
| Gameplay conformance | NOT VERIFIED; gaps above remain |

## GitHub delivery status

This branch contains this audit report and the source package checksum. The GitHub connector available in this session supports text-file writes but does not expose a binary repository upload operation; therefore the corrected ZIP itself has **not** been committed to the repository. The corrected local package is available as a separate conversation attachment. No claim is made that the source files are present in GitHub until they are uploaded as repository files.

## Required verification gate

1. Import the source into a clean Unity 6000.3 project.
2. Pin the exact FishNet package version and preserve package-lock evidence.
3. Compile runtime and test assemblies.
4. Run EditMode and PlayMode tests and retain logs/results.
5. Resolve the Betrayal Transfer contract and disconnect policy through ADRs before implementing those behaviors.
6. Add server integration tests for authentication, tick execution, snapshot revision monotonicity, and command outcome delivery.

No source is labelled VERIFIED until these gates have reproducible evidence.
