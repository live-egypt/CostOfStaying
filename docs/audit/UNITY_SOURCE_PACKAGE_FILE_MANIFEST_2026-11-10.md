# Unity Source ZIP File Manifest — 2026-11-10

**Revision:** 2026-11-10  
**Decision Status:** — (archive inventory; not an implementation approval)  
**Evidence Maturity:** REVIEWED — all 94 ZIP central-directory entries enumerated from the exact repository blob  
**Source:** review/unity-source-package-2026-11-01, PR #5  
**Git blob SHA:** 319a584046d3ece7ce74242233015738758ec210  
**Archive size:** 56,217 bytes after Base64 decoding

> This manifest records archive entry names and entry types. It does not include a SHA-256 for each internal file because individual file bytes were not all extracted and hashed separately. It is not a recursive repository tree and does not establish compilation or runtime behavior.

## 1. Aggregate

| Entry type | Count |
|---|---:|
| Directories | 7 |
| C# source/test files | 81 |
| Assembly Definition files | 4 |
| Markdown files | 2 |
| **Total ZIP entries** | **94** |
| **Total files (all file types)** | **87** |

## 2. Directory entries (7)

1. Assets/
2. Assets/CostOfStaying/
3. Assets/CostOfStaying/Core/
4. Assets/CostOfStaying/Net/
5. Assets/CostOfStaying/Tests/
6. Assets/CostOfStaying/Tests/EditMode/
7. Assets/CostOfStaying/Tests/Net/

## 3. Core source files

- Assets/CostOfStaying/Core/PlayerId.cs
- Assets/CostOfStaying/Core/CommandQueueOptions.cs
- Assets/CostOfStaying/Core/BetrayalCommandIngress.cs
- Assets/CostOfStaying/Core/MovementCommandIngress.cs
- Assets/CostOfStaying/Core/CommandKind.cs
- Assets/CostOfStaying/Core/PlayerSnapshot.cs
- Assets/CostOfStaying/Core/SessionRepository.cs
- Assets/CostOfStaying/Core/MovementService.cs
- Assets/CostOfStaying/Core/SessionId.cs
- Assets/CostOfStaying/Core/BetrayalService.cs
- Assets/CostOfStaying/Core/MovementState.cs
- Assets/CostOfStaying/Core/MovementRules.cs
- Assets/CostOfStaying/Core/CommandEnvelope.cs
- Assets/CostOfStaying/Core/BetrayalCommand.cs
- Assets/CostOfStaying/Core/MatchSnapshot.cs
- Assets/CostOfStaying/Core/CommandAdmissionRules.cs
- Assets/CostOfStaying/Core/MatchRules.cs
- Assets/CostOfStaying/Core/CoreSnapshot.cs
- Assets/CostOfStaying/Core/CommandOutcome.cs
- Assets/CostOfStaying/Core/AuthoritativeState.cs
- Assets/CostOfStaying/Core/CoreComposition.cs
- Assets/CostOfStaying/Core/CommandEnvelopeFactory.cs
- Assets/CostOfStaying/Core/PresenceState.cs
- Assets/CostOfStaying/Core/VitalityState.cs
- Assets/CostOfStaying/Core/PlayerSession.cs
- Assets/CostOfStaying/Core/CoreSnapshotFactory.cs
- Assets/CostOfStaying/Core/MovementInput.cs
- Assets/CostOfStaying/Core/ServerTick.cs
- Assets/CostOfStaying/Core/Result.cs
- Assets/CostOfStaying/Core/CoreTickState.cs
- Assets/CostOfStaying/Core/MatchState.cs
- Assets/CostOfStaying/Core/MovementCommand.cs
- Assets/CostOfStaying/Core/MatchStateContainer.cs
- Assets/CostOfStaying/Core/CommandSequenceTracker.cs
- Assets/CostOfStaying/Core/SessionLifecycleService.cs
- Assets/CostOfStaying/Core/CorePipelineResult.cs
- Assets/CostOfStaying/Core/PlayerStateRepository.cs
- Assets/CostOfStaying/Core/PlayerState.cs
- Assets/CostOfStaying/Core/MatchLifecycleState.cs
- Assets/CostOfStaying/Core/BetrayalRules.cs
- Assets/CostOfStaying/Core/StateRevision.cs
- Assets/CostOfStaying/Core/CorePipeline.cs
- Assets/CostOfStaying/Core/ParticipationState.cs

## 4. Core Assembly Definition (1)

- Assets/CostOfStaying/Core/CostOfStaying.Core.asmdef

## 5. Net source files

- Assets/CostOfStaying/Net/IServerIdentityResolver.cs
- Assets/CostOfStaying/Net/MovementInputBroadcast.cs
- Assets/CostOfStaying/Net/ConnectionSessionMap.cs
- Assets/CostOfStaying/Net/CoreRejectBroadcast.cs
- Assets/CostOfStaying/Net/ClientAdapterState.cs
- Assets/CostOfStaying/Net/CoreServerSessionBridge.cs
- Assets/CostOfStaying/Net/FishNetClientAdapter.cs
- Assets/CostOfStaying/Net/IServerCommandIngress.cs
- Assets/CostOfStaying/Net/CoreSnapshotBroadcast.cs
- Assets/CostOfStaying/Net/CommandOutcomeBroadcast.cs
- Assets/CostOfStaying/Net/IServerConnectionRejector.cs
- Assets/CostOfStaying/Net/IServerRejectPublisher.cs
- Assets/CostOfStaying/Net/IClientCoreEvents.cs
- Assets/CostOfStaying/Net/CoreServerCommandIngress.cs
- Assets/CostOfStaying/Net/ClientProjectionState.cs
- Assets/CostOfStaying/Net/FishNetServerAdapter.cs
- Assets/CostOfStaying/Net/ClientAuthenticationState.cs
- Assets/CostOfStaying/Net/IClientCommandSender.cs
- Assets/CostOfStaying/Net/BetrayalRequestBroadcast.cs
- Assets/CostOfStaying/Net/PendingConnectionRegistry.cs
- Assets/CostOfStaying/Net/IServerNetworkEvents.cs
- Assets/CostOfStaying/Net/ClientConnectionState.cs
- Assets/CostOfStaying/Net/CoreSnapshotMapper.cs
- Assets/CostOfStaying/Net/FishNetServerRejectPublisher.cs
- Assets/CostOfStaying/Net/IServerSessionBridge.cs

## 6. Net Assembly Definition (1)

- Assets/CostOfStaying/Net/CostOfStaying.Net.asmdef

## 7. EditMode test files

- Assets/CostOfStaying/Tests/EditMode/MatchStateTests.cs
- Assets/CostOfStaying/Tests/EditMode/CoreCompositionTests.cs
- Assets/CostOfStaying/Tests/EditMode/PlayerStateTests.cs
- Assets/CostOfStaying/Tests/EditMode/SessionLifecycleServiceTests.cs
- Assets/CostOfStaying/Tests/EditMode/CommandSequenceTrackerTests.cs

## 8. EditMode Assembly Definition (1)

- Assets/CostOfStaying/Tests/EditMode/CostOfStaying.Core.Tests.asmdef

## 9. Net test files

- Assets/CostOfStaying/Tests/Net/PendingConnectionRegistryTests.cs
- Assets/CostOfStaying/Tests/Net/ServerClientAdapterPlayModeTests.cs
- Assets/CostOfStaying/Tests/Net/ClientProjectionStateTests.cs
- Assets/CostOfStaying/Tests/Net/ConnectionSessionMapTests.cs
- Assets/CostOfStaying/Tests/Net/ServerAdapterPlayModeTests.cs
- Assets/CostOfStaying/Tests/Net/ClientAdapterStateTests.cs
- Assets/CostOfStaying/Tests/Net/ClientAdapterPlayModeTests.cs
- Assets/CostOfStaying/Tests/Net/CoreServerCommandIngressTests.cs

## 10. Net test Assembly Definition (1)

- Assets/CostOfStaying/Tests/Net/CostOfStaying.Net.Tests.asmdef

## 11. Markdown files at archive root

- README.md
- docs/audit/UNITY_SOURCE_PACKAGE_REVIEW.md

## 12. Missing Unity project files observed in this archive

The 94-entry central directory contains no:
- Packages/manifest.json
- Packages/packages-lock.json
- ProjectSettings/ProjectVersion.txt
- ProjectSettings/EditorBuildSettings.asset
- .unity scene files
- .meta asset metadata files

Therefore the archive is an Assets/source package and not a complete self-contained Unity project.

## 13. Reconciliation boundary

- This archive inventory is scoped to the ZIP stored on PR #5's review branch.
- It is not the same source set as the standalone Core projects on PR #6.
- It is not present on main in the inspected baseline.
- The archive-level inventory does not hash each embedded file separately and does not certify that the files compile.
- PR #5's body and checksum manifest conflict with the current committed ZIP. See UNITY_SOURCE_PACKAGE_RECONCILIATION_2026-11-10.md.
