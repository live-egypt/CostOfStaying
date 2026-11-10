# Unity Source Package Reconciliation — 2026-11-10

**Revision:** 2026-11-10  
**Decision Status:** — (source/package reconciliation; no code or architecture decision made)  
**Evidence Maturity:** REVIEWED — GitHub branch, PR metadata, binary blob identity, ZIP central directory, README and checksum manifest inspected  
**Repository:** live-egypt/CostOfStaying  
**Source branch:** review/unity-source-package-2026-11-01  
**Source PR:** [PR #5](https://github.com/live-egypt/CostOfStaying/pull/5) — OPEN / DRAFT / NOT MERGED

> This review establishes that an archive is committed on a review branch and describes its archive-level contents. It does not establish that Unity imports or compiles the package, that FishNet APIs match an installed version, or that a playable game is implemented.

## 1. Repository object identity

| Field | Retrieved value |
|---|---|
| Repository path | incoming/unity-source-package/CostOfStaying_Unity_Source_Package.zip |
| Branch | review/unity-source-package-2026-11-01 |
| Git blob SHA | 319a584046d3ece7ce74242233015738758ec210 |
| Decoded archive size | 56,217 bytes |
| ZIP entries | 94 |
| ZIP directory entries | 7 |
| C# files | 81 |
| Assembly Definition files | 4 |
| Markdown files | 2 |
| Top-level roots | Assets/, README.md, docs/ |

The ZIP was retrieved through the GitHub file connector in Base64 form. The line breaks were removed before decoding. The computed Git blob SHA-1 for the exact decoded bytes matched GitHub's recorded blob SHA 319a584046d3ece7ce74242233015738758ec210. This confirms the decoded bytes correspond to the repository blob.

## 2. Archive structure

The archive contains source/test areas under:

- Assets/CostOfStaying/Core/
- Assets/CostOfStaying/Net/
- Assets/CostOfStaying/Tests/EditMode/
- Assets/CostOfStaying/Tests/Net/

The entry inventory contains 81 .cs files, 4 .asmdef files, 2 .md files, and 7 directory entries. The file list includes source names such as CorePipeline.cs, MovementService.cs, BetrayalService.cs, SessionLifecycleService.cs, FishNetServerAdapter.cs, FishNetClientAdapter.cs, CoreServerCommandIngress.cs, and ConnectionSessionMap.cs, as well as EditMode and Net test fixtures.

This is positive evidence that a Unity-oriented source package is present in this review branch. It corrects the earlier report that the corrected ZIP was not present in the branch.

## 3. Checksum conflict

The branch file docs/audit/UNITY_SOURCE_PACKAGE_SHA256.txt declares:

- SHA-256: 351190c6789b0a146be0978606c831f48b951509e7f353bb3b469374eb9b15d9
- Archive size: 55 KB
- Note: corrected archive is attached in the ChatGPT conversation and not committed to this repository branch.

For the exact 56,217 decoded bytes retrieved from the Git blob, SHA-256 was independently computed as:

5f92947c8b23d1ee15d7c744fb62ef0cf7af361bfdda277fcdaeff61dab51ac4

The computed SHA-256 does not match the checksum file. The Git blob identity did match, so the mismatch is not explained by a different file being fetched. Preserve both values; do not silently overwrite the manifest. The source manifest needs a correction/lineage decision identifying which archive version the declared hash was intended to authenticate.

## 4. PR description versus committed file list

PR #5's description says the corrected ZIP is attached to the ChatGPT conversation and is not included in the branch. However:

- list_pr_changed_filenames for PR #5 includes incoming/unity-source-package/CostOfStaying_Unity_Source_Package.zip.
- GitHub fetch_file returns the ZIP at that exact path on the PR #5 head branch.
- The Git blob SHA and decoded archive structure were independently checked.

Therefore the PR body and branch file/manifest disagree. This review records the branch file as present; it does not infer who uploaded it or when the discrepancy arose. PR #5 remains open Draft and unmerged.

## 5. Package completeness boundary

The archive has a Unity Assets tree and assembly definitions, but the inspected ZIP inventory does not contain:

- Packages/manifest.json
- Packages/packages-lock.json
- ProjectSettings/ProjectVersion.txt
- ProjectSettings/EditorBuildSettings.asset
- Unity scene files (.unity)
- Unity asset metadata files (.meta)

Accordingly, the ZIP is an Assets/source package, not a complete self-contained Unity project. A clean Unity project must still supply the editor version, resolved package set, project settings, scenes/build configuration, and any external dependencies. This absence is based on the full 94-entry ZIP central-directory inventory, not on a name-only assumption.

## 6. Static review claims retained from PR #5 report

The companion docs/audit/UNITY_SOURCE_PACKAGE_REVIEW.md states:

- 81 C# files inventoried.
- Static Core scan found no UnityEngine or FishNet references in the Core area.
- Several source corrections were prepared in the reviewed package.
- Unity compilation: NOT RUN.
- NUnit EditMode/PlayMode tests: NOT RUN.
- FishNet 4.7.4R compatibility: NOT VERIFIED against an installed package.
- Dedicated Server/Host integration: NOT RUN.
- Gameplay conformance: NOT VERIFIED.

These are report claims. The current archive presence does not independently validate all static review conclusions or source corrections. The exact package needs extraction, full source review, import and compilation against a real, pinned Unity/FishNet environment.

## 7. Required verification before implementation claims

1. Reconcile and correct the archive SHA-256 manifest with a source/version record.
2. Extract the archive in a controlled working copy and inventory all source and assembly definitions.
3. Create or identify the exact Unity editor project and pin its package manifest/lock and ProjectVersion.
4. Verify FishNet package version from the resolved package and compile against its actual APIs.
5. Run Unity EditMode and PlayMode tests and retain XML/log artifacts.
6. Run Host and Linux Dedicated Server integration tests, including authentication, disconnect, snapshots, tick execution and packet impairment.
7. Reconcile the archive's movement/direct-damage Betrayal behavior with the current proposed Suffering Transfer Move/atomicity specification before treating gameplay conformance as met.

## 8. Status

| Claim | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| ZIP exists on PR #5 review branch | — | VERIFIED — branch path and blob identity fetched | NOT ON main |
| ZIP contains 81 C# files and 4 asmdefs | — | REVIEWED — ZIP central directory | Static archive inventory only |
| Declared SHA-256 is correct for current ZIP | — | REVIEWED — MISMATCH | OPEN |
| PR #5 body accurately describes current branch file list | — | REVIEWED — CONTRADICTION | OPEN |
| ZIP is a complete Unity project | — | REVIEWED — NOT COMPLETE | Asset/source package only |
| Unity compilation / NUnit / FishNet compatibility | — | NOT ESTABLISHED | NOT RUN / NOT VERIFIED |
| Playable integration / Dedicated Server | — | NOT ESTABLISHED | NOT RUN |
| Production readiness | — | NOT ESTABLISHED | NOT CLAIMED |

No ZIP was modified, no source files were extracted into the repository, and no PR #5 change was made by this reconciliation.


## 9. Source-level spot review of critical files

The ZIP central directory was used to locate the following files. Their compressed contents were extracted from the same decoded bytes whose Git blob identity was verified, then read as source text. This is a static inspection only; no compiler or Unity runtime was invoked.

### 9.1 Betrayal is direct damage, not the current Transfer rule

Assets/CostOfStaying/Core/BetrayalService.cs:
- Resolves the attacker from SessionRepository and requires an authenticated session.
- Validates attacker and victim existence, attacker/victim alive state, victim online presence, and rejects self-target.
- Calls victimState.ApplyDamage(_rules.VitalityDamage) and marks authoritative state changed.
- It does not move a Debuff, remove it from the source, check source/target Debuff capacity, apply range or cooldown rules, or atomically commit a Transfer plus Betrayal ledger event.

**Finding:** This is a direct-damage Betrayal mechanic. It does not implement the proposed v2.1 Suffering Transfer Move/atomicity contract. Do not map the class name "Betrayal" to the Suffering Transfer requirement without a rule and implementation reconciliation.

### 9.2 Movement queue can process multiple fixed-delta updates in one tick

Assets/CostOfStaying/Core/MovementCommandIngress.cs:
- Bounds the queue at MaximumCommandsPerTick multiplied by four.
- Validates command age, then queues each command.

Assets/CostOfStaying/Core/CorePipeline.cs:
- On every Execute(tick), drains the MovementCommandIngress queue in a while loop until empty.
- Applies each dequeued movement command through MovementService.Execute.

Assets/CostOfStaying/Core/MovementService.cs:
- Computes displacement as normalized input × speed × MovementRules.FixedDeltaTime for every executed command.

**Finding:** The inspected path limits queue storage but does not enforce MaximumCommandsPerTick as a processing cap. Multiple accepted movement commands can each apply a fixed-delta displacement during the same pipeline tick. The exact maximum depends on runtime CommandQueueOptions, but the source does not show a per-tick execution limit in CorePipeline. A client capable of enqueueing several increasing-sequence inputs before a tick may therefore receive multiple movement updates during one simulation tick.

**Required correction gate:** Define ingress rate and per-tick command budgets, apply a server-side processing cap or aggregate input semantics, and add an NUnit regression test proving that command spam cannot multiply movement per tick. The fix is not applied in this documentation review.

### 9.3 Command order is split by command type

Assets/CostOfStaying/Core/CommandSequenceTracker.cs keeps separate sequence dictionaries for Movement and Betrayal. Assets/CostOfStaying/Core/CorePipeline.cs drains the complete Movement queue first, then drains the Betrayal queue.

**Finding:** The inspected implementation does not preserve one global per-session command order across Movement and Betrayal. A sequence value can be accepted once in each command-type tracker, and a Betrayal request is processed after all queued Movement requests regardless of cross-type arrival order. Whether this is acceptable depends on the required simulation ordering contract; that contract is not established by this ZIP.

**Required design gate:** Define whether sequence numbers are global per session or scoped by command type, and define the deterministic ordering rule across command types. Add tests for interleaved Movement/Betrayal commands and conflicting arrival/order cases.

### 9.4 Movement does not validate world geometry or collision

Assets/CostOfStaying/Core/MovementService.cs normalizes the requested planar vector, chooses WalkSpeed or SprintSpeed, applies FixedDeltaTime and updates PlayerState.Position. The inspected service does not query map bounds, collision geometry, navigation constraints, or blocked movement resolution.

**Finding:** Movement is deterministic planar arithmetic only. It is not a complete authoritative movement/collision system. The Sprint flag is also consumed as an input to speed selection; any intended sprint eligibility, resource cost, or cooldown must be enforced by a server-owned rule if those constraints are part of the design.

### 9.5 Disconnect removes the player rather than preserving reconnect state

Assets/CostOfStaying/Core/SessionLifecycleService.cs:
- Disconnect marks the session disconnected.
- Sets presence offline.
- Removes participation state.
- Removes the player from AuthoritativeState.Players.
- Decrements Match.ConnectedPlayers and removes the session record.

Assets/CostOfStaying/Net/CoreServerSessionBridge.cs removes the connection/session mapping and calls Pipeline.RemoveSession after the session lifecycle disconnect succeeds.

**Finding:** The current package implements immediate player removal on disconnect. It does not preserve a reconnect grace period or identity continuity in the inspected path. This is a concrete behavior that must be reconciled with the intended gameplay and session policy before Alpha.

### 9.6 FishNet adapter shape differs from the project network contract

Assets/CostOfStaying/Net/FishNetServerAdapter.cs:
- Declares FishNetServerAdapter as a Unity MonoBehaviour, not a FishNet NetworkBehaviour.
- Registers MovementInputBroadcast and BetrayalRequestBroadcast through ServerManager.RegisterBroadcast.
- Subscribes and unsubscribes TimeManager.OnTick and executes CorePipeline from OnServerTick.
- Publishes CoreSnapshotBroadcast messages to mapped authenticated connections.
- The inspected publishing path does not send CommandOutcomeBroadcast, although that DTO exists in the archive.
- The broadcast registrations disable FishNet's built-in authentication gate and rely on the package's own connection/session mapping plus Core session checks.

Assets/CostOfStaying/Net/CoreServerCommandIngress.cs:
- Does not trust the client requestedTick; it creates the envelope from Core.Ticks.Current.
- Requires a Core session to exist and be authenticated before enqueueing the movement or betrayal intent.

**Finding:** The server-owned tick and Core session checks are positive source-level controls. However, the adapter uses MonoBehaviour and broadcast ingress/egress rather than the project's required FishNet NetworkBehaviour with the approved ServerRpc/ObserversRpc pattern. That is an architectural deviation requiring correction or explicit ADR review. FishNet API/runtime compatibility remains unverified because the actual resolved package was not supplied.

### 9.7 Authentication is delegated to a host-supplied identity resolver

CoreServerSessionBridge.Connect:
- Creates a pending Core session through SessionLifecycle.Connect.
- Binds ClientId to SessionId in ConnectionSessionMap.
- Calls SessionLifecycle.Authenticate after binding.
- Rolls back the binding/session if authentication fails.

FishNetServerAdapter exposes AuthenticateConnection and obtains PlayerId through IServerIdentityResolver. The ZIP contains the interface, but no concrete credential/account identity provider.

**Finding:** The package assumes that the host calls AuthenticateConnection only after a trusted identity check. The package itself does not provide a credential protocol or concrete identity validation source. Do not interpret the Core session IsAuthenticated flag as proof that account credentials were verified unless the host authentication integration is supplied and tested.

### 9.8 Test fixtures present and missing

The archive contains 5 EditMode test files and 8 Net test files, including:
- CoreCompositionTests
- MatchStateTests
- PlayerStateTests
- SessionLifecycleServiceTests
- CommandSequenceTrackerTests
- ConnectionSessionMapTests
- CoreServerCommandIngressTests
- PendingConnectionRegistryTests
- Client/Server adapter state and PlayMode test files

No fixture named MovementServiceTests, BetrayalServiceTests, or CorePipelineTests appears in the archive inventory. The existing fixtures therefore do not establish regression coverage for the multi-command-per-tick movement finding or the direct-damage-versus-Transfer contract mismatch. Test names and file presence are not execution evidence.

### 9.9 Player lifecycle and match-state model differ from ADR-007

Assets/CostOfStaying/Core/PlayerState.cs:
- Exposes IsAlive as the boolean expression Vitality > 0.
- ApplyDamage clamps Vitality at zero but does not perform a transition to Spirit or Eliminated.

Assets/CostOfStaying/Core/VitalityState.cs:
- Stores integer Vitality values and exposes IsAlive as Get(playerId) > 0.
- Does not model Spirit, resurrection, or terminal elimination transitions.

Assets/CostOfStaying/Core/MatchLifecycleState.cs defines only Waiting, Running, Finished and Closed. The ZIP filename inventory contains no standalone Spirit, Collapse, Endgame, Debuff or Vote source module.

**Finding:** This package's inspected player model is a vitality/boolean Alive model. It does not implement the PR #6 branch ADR-007 lifecycle matrix Alive / Spirit / Eliminated, zero Vitality → Spirit, explicit Resurrect, and terminal Eliminated. The match state enum also does not model the proposed vote/endgame phases.

**Branch boundary:** These are source findings for PR #5's ZIP only. They do not describe the separate PR #6 Core implementation, and must not be conflated with PR #6's tested VitalityAccount lifecycle.

## 10. Static implementation conclusion

The archive is a substantial Unity-oriented source package, not an empty placeholder. It contains Core, Net, and test source, but the inspected gameplay/network paths show material differences from the current project contracts:

- Betrayal applies direct Vitality damage rather than the specified full Debuff Move.
- Movement lacks a visible per-tick processing cap while fixed delta is applied per command.
- Disconnect removes the player from authoritative match state.
- Network adapters use MonoBehaviour/Broadcasts rather than the project's required FishNet NetworkBehaviour/RPC shape.
- Command outcomes are not published by the inspected server adapter path.
- Authentication relies on a host-supplied resolver; no credential provider is included.
- Unity/FishNet import, compile, EditMode/PlayMode execution, Host, Dedicated Server, and device validation remain NOT RUN.

These are review findings against the ZIP on PR #5's branch only. They are not claims about the separate PR #6 Core branch or current main implementation.
