# Uploaded Phase 1 Source — Consolidation and Technical Audit

**Audit date:** 2026-11-01  
**Repository baseline:** `live-egypt/CostOfStaying`, branch `main`, commit `4b13d71a49d67ed0b69ba5c3e576aa7b0fb66f31`  
**Audited source:** `Based on the instructions, let's start Phase 1_4504147581725275717.md`  
**Source SHA-256:** `b9432e212ca8b71a93903bb00125309f8470662aa0a2f1115377d909ad2a7319`  
**Audit status:** Static document audit completed; executable implementation not established.

## 1. Executive finding

The uploaded Markdown file is not a canonical source tree or a single final implementation. It is a long, iterative design-and-code transcript containing successive proposed implementations, corrections, alternative contracts, repeated file snapshots, test sketches, ADR drafts, and explicit statements that the code was not compiled, run, or integrated.

The repository baseline is documentation-only. Its recursive Git tree contains no `Core/`, `Net/`, `Tests/`, C# source, Unity project metadata, FishNet package manifest, or test results. Therefore the uploaded file can inform a controlled implementation, but cannot be treated as code already present in the repository.

**Decision:** Do not bulk-copy the transcript into the repository. First extract and normalize one canonical design, resolve contradictions, then implement in a clean branch with tests. This audit document is the first repository change; no gameplay code has been represented as implemented.

## 2. Source inventory and measurable characteristics

The uploaded file was materialized and scanned in full.

| Metric | Observed |
|---|---:|
| File size | 2,088,678 bytes |
| Text characters | 2,001,338 |
| Lines | 85,794 |
| Words | 145,630 |
| Markdown headings | 1,995 |
| Fenced code blocks | 1,694 pairs |
| C# fenced blocks | 1,372 |
| JSON fenced blocks | 22 |
| Bash fenced blocks | 6 |
| Type declarations across all transcript snapshots | 1,039 |
| Distinct type names across those snapshots | 586 |
| Type names declared more than once | 193 |
| `Guid.NewGuid()` occurrences inside C# blocks | 60 |
| C# lines containing malformed index-from-end tokens such as `[^1_0]` | 54 |
| Explicit `TODO` markers in source/code | 3 |

These counts describe the **entire transcript**, not the repository and not a single compilable project. Repeated type declarations mostly reflect later revisions of earlier examples; they are not proof of duplicate classes in a real assembly. They do, however, make direct copy/paste integration unsafe.

## 3. Document structure and evolution

The transcript evolves through several broad design waves:

1. **Initial Vitality and Betrayal primitives** (approximately lines 1–789): `IVitalitySystem`, `IBetrayalLedger`, concrete classes, NUnit examples, and initial assembly definitions.
2. **Ledger validation and betrayal command flow** (approximately lines 790–4,731): validation contracts, match state, command service, server identity/tick boundaries, FishNet adapter sketches, replay handling, ledger export and snapshots.
3. **Network identity, lifecycle and vitality integration** (approximately lines 4,732–20,584): server/client identity mapping, player spawn, network projection, atomic betrayal discussions, and ADR drafts including ADR-0003 through ADR-0006.
4. **Core redesign and security hardening** (approximately lines 20,585–52,823): a much larger proposed Core model covering identifiers, sessions, match state, commands, movement, server tick, command admission, lifecycle, snapshots, persistence, event logs, and composition.
5. **Core implementation pass** (approximately lines 52,824–60,548): a proposed Core v0 file set, tests, betrayal domain, idempotency, session registry, snapshot export/restore and pipeline refinements.
6. **FishNet adapter design** (approximately lines 60,549–69,446): broadcast contracts, connection/session map, server/client adapters, authentication, reject handling, tick adapter, and later corrections to identity and registration.
7. **Core completion and integration revisions** (approximately lines 69,447–84,599): result types, identifiers, session lifecycle, movement/betrayal services, snapshots, client projection, server snapshot publishing, server ingress, connection/session map, and a second client-adapter pass.
8. **Final handoff and environment requirements** (approximately lines 84,600–85,794): the transcript explicitly requests the actual source tree, warns against treating prior generated code as final, and lists Unity/FishNet/test setup requirements.

This sequence is useful as design history, but it is not a clean release artifact. Later sections revise earlier decisions and sometimes introduce new names or structures without a single authoritative migration record.

## 4. High-priority defects and integration risks in the uploaded source

### P0 — Do not treat the transcript as a buildable project

- The source itself states near the beginning that the code has not been compiled, run, or integrated.
- It repeatedly labels FishNet integration, serialization, Unity compilation, PlayMode and dedicated-server checks as not verified.
- The final handoff explicitly says the prior responses may contain types and names not matching the real repository and requests actual project files before claiming compilation readiness.
- The current repository has no executable project files against which these snippets can be compiled.

**Required action:** all code in the upload remains `PROPOSED` until selected, reconciled, placed in the repository, and built.

### P0 — Multiple competing versions of the same domain and infrastructure types

The transcript contains repeated declarations and revisions of types including (not exhaustive):

- `Result`, `PlayerId`, `SessionId`, `ServerTick`, `StateRevision`
- `VitalityState`, `VitalityStore`, `BetrayalLedger`
- `BetrayalValidator`, `BetrayalResolutionService`, `BetrayalRules`
- `MatchCore`, `MatchState`, `MatchStateContainer`, `MatchSnapshot`
- `CommandEnvelope`, `CommandQueue`, `CorePipeline`, `CoreComposition`
- `FishNetServerAdapter`, `FishNetClientAdapter`, `CoreSnapshotBroadcast`

The same names appear in multiple historical code blocks with evolving signatures and responsibilities. A merge must select one final definition per type and remove obsolete variants; combining snippets from different points in the transcript would predictably create duplicate declarations, incompatible constructors, and namespace/API mismatches.

### P0 — Malformed C# index-from-end expressions

There are 54 C# source lines containing forms such as:

```csharp
snapshot[^1_0]
snapshot[^2_1]
collection[^1_0]
```

These are not valid C# index-from-end expressions. The likely intended forms are `snapshot[^1]`, `snapshot[^2]`, etc., but each occurrence must be checked in context rather than mechanically rewritten. The scan found these inside code fences, including early NUnit ledger tests.

**Required action:** repair and compile-check every selected code block before it is admitted to the canonical source set.

### P1 — Transaction semantics changed over time

The early ledger design records a betrayal fact but deliberately does not apply Vitality penalties. Later revisions introduce atomic resolution, rollback/transaction abstractions, event logs, idempotency, and persistence ordering. These are materially different contracts.

The transcript also discusses several transaction order alternatives and drafts ADR-0003, ADR-0006, and ADR-0007 around atomic betrayal, transaction order, and Core invariants. These decisions must be reconciled into one approved contract before implementation.

**Required action:** define one transaction boundary for betrayal, including exactly what is validated, what mutates, how failure rolls back, and when the event/ledger entry becomes durable. Do not implement the early ledger and later transaction service as independent competing authorities.

### P1 — Player life state and eligibility model evolved

Early `IVitalitySystem` semantics treat zero Vitality as terminal for that instance and prohibit implicit restoration after death. Later revisions introduce richer player participation/lifecycle states, such as Alive, Spirit, Eliminated, spectator/disconnected states, and rules for whether a player may issue commands.

These models are not automatically equivalent. Vitality, life state, network presence, session status, and command eligibility must remain separate concepts with explicit transition rules.

**Required action:** use the project's authoritative rules matrix as the source for state transitions. The transcript alone is not enough to silently choose between conflicting revisions.

### P1 — Identity and authentication are repeatedly redesigned

The transcript explores client-supplied identifiers, server-side identity resolution, connection/session maps, authentication lifecycle ownership, reconnect handling, and cleanup. Several sections explicitly identify spoofing and duplicate-map risks.

The final direction is sound as a proposed trust boundary: derive the sender from the authenticated FishNet connection on the server, map it to a server-owned session/player identity, and never accept a client-provided `PlayerId` as proof of sender identity. However, the actual implementation and authenticator configuration are absent.

**Required action:** implement a single authoritative connection-to-session mapping owner; define authentication completion, reconnect, disconnect, stale-session and connection-ID-reuse behavior; reject commands without a live authenticated mapping.

### P1 — FishNet API compatibility is not established

The document assumes FishNet **4.7.4R**, but the repository contains no `Packages/manifest.json`, FishNet package source, Unity project version, or assembly definitions. The transcript contains multiple adapter iterations and explicitly leaves API compatibility, Broadcast registration/unregistration, serialization, RPC execution, Host mode and Dedicated Server behavior unverified.

**Required action:** treat 4.7.4R as an assumption only. Before implementing adapters, obtain the actual package version and inspect its installed source/asmdef. Compile against the exact signatures; do not rely on snippets copied from an external revision.

### P1 — `Guid.NewGuid()` ownership is mixed

The transcript contains 60 occurrences in C# code blocks. They appear in proposed factories, tests, player bootstrap/identity examples, and other contexts. This count does not prove that all are production calls, but it does prove that a blanket rule cannot be validated from a search count alone.

**Required action:** classify each occurrence by responsibility. Test fixtures may generate IDs; pure Core should receive IDs as inputs or use an injected factory where explicitly designed; authoritative identity/session/event IDs must be generated at the server-owned boundary. Client-provided IDs must never establish identity or authority.

### P1 — Snapshot, event log and persistence scope expanded repeatedly

Later transcript revisions add schema versions, snapshot validation/build/apply, atomic save/load coordination, event sequence, checksums, replay protection and restoration. These are useful design topics, but they substantially exceed the initial Vitality/Betrayal slice and introduce ordering, consistency and recovery obligations.

**Required action:** keep persistence out of the first gameplay Core slice unless an approved milestone requires it. If persistence is in scope, create/approve a dedicated ADR and test snapshot round trips, schema compatibility, partial failure, monotonic revision, and replay behavior independently.

### P2 — Incomplete adapter examples and TODOs

The source contains explicit TODOs for resolving a victim from server-owned FishNet objects, sending a result to a client, and completing a network smoke test. Other sections state that PlayMode and end-to-end tests remain unverified.

**Required action:** no adapter is accepted as complete while a production path contains TODO/NotImplemented behavior or lacks a real FishNet integration test.

## 5. Proposed canonical architecture extracted from the source

The following is a consolidation recommendation, not an assertion that the transcript already contains a consistent implementation:

```text
Assets/CostOfStaying/
  Core/
    Common/          Result, guards, domain errors
    Identity/        PlayerId, SessionId, MatchId
    Time/             ServerTick, tick contracts
    Match/            match state and lifecycle
    Players/          player state, participation, presence policy
    Vitality/         vitality state/store/service
    Betrayal/         request, rules, validator, resolution, ledger
    Movement/         input, rules, authoritative simulation
    Commands/         envelope, admission, sequence/idempotency
    Snapshots/        immutable snapshot DTOs and validation
    Pipeline/         one deterministic server tick/command executor
    CoreComposition.cs

  Net/
    Contracts/        FishNet broadcast payloads
    Identity/         authenticated connection-to-session mapping
    Server/           ingress, command routing, snapshot publishing
    Client/           command sender and read-only projection
    FishNetServerAdapter.cs
    FishNetClientAdapter.cs

  Unity/
    Bootstrap/        composition/bootstrap wrappers
    Presentation/     projection into Unity-facing components

  Tests/
    EditMode/         pure Core and policy tests
    PlayMode/         actual FishNet/Unity integration tests
```

Core must remain free of `UnityEngine`, FishNet, `MonoBehaviour`, `NetworkConnection`, Unity time APIs and Unity logging. The server pipeline must be the sole owner of authoritative state mutation. Client projections are read-only.

## 6. Required ADR decisions before implementation

The transcript drafts multiple ADRs, but they are not present in the current repository tree. They must not be assumed approved merely because a draft appears in the uploaded file.

| Decision | Required treatment |
|---|---|
| ADR-0003 — Atomic Betrayal Resolution | Reconcile with the final transaction and rollback contract; approve before implementation. |
| ADR-0004 — Authentication Flow | Confirm one authentication lifecycle owner and authoritative connection/session binding. |
| ADR-0005 — Betrayal Request Transport | Confirm transport payload and sender identity rules against installed FishNet. |
| ADR-0006 — Betrayal Transaction Order | Reconcile with ADR-0003/0007; avoid competing transaction-order decisions. |
| ADR-0007 — Core Invariants and Betrayal Transaction | Consolidate the final invariant set and transaction boundary. |
| ADR-0008 — Server Position Authority | Required before trusting movement state or accepting client position claims. |
| ADR-0009 — Core Command Gates | Define admission, rate limits, sequence checks, match phase and eligibility. |
| ADR-0010 — Match Persistence Boundary | Defer unless persistence is in current MVP; if retained, define schema and atomicity. |

**Governance note:** the current repository already has ADR-001 through ADR-004 under `docs/governance/adr/`. The uploaded transcript's ADR numbering is a separate draft series. Do not copy these draft ADRs into the existing sequence without checking for identifier collision and reconciling the current decision log.

## 7. Repository baseline cross-check

At the audited `main` commit, the repository contains a documentation tree under `docs/` and a root README. The README states that there is no code implementation, Unity project, Dedicated Server, CI, or executed test suite.

Observed absent paths/artifacts:

- `Assets/`, `Packages/manifest.json`, `ProjectSettings/ProjectVersion.txt`
- `Core/`, `Net/`, `Unity/`, `Tests/`
- `*.cs`, `*.asmdef`, `*.csproj`, `*.sln`
- FishNet package lock/version and Unity Test Framework test results

This is consistent with the uploaded transcript's final handoff, which asks for the actual project source before performing line-by-line repair. The audit therefore cannot report compile errors in project code; no project code exists at this baseline.

## 8. Implementation sequence recommended

1. **Freeze scope:** confirm the first executable slice and the Alive/Spirit/Eliminated lifecycle matrix from the current game rules documents.
2. **Resolve ADRs:** consolidate the relevant draft decisions, eliminate numbering collisions, and record accepted decisions in the repository.
3. **Create Core project structure:** pure C# assembly and Unity asmdef with engine references disabled.
4. **Define interfaces first:** stable contracts for identity, vitality, betrayal, lifecycle, command admission and tick execution.
5. **Write tests before services:** one unit-test suite per core contract and transition matrix row.
6. **Implement Core:** deterministic state transitions, explicit error results, idempotency, bounded queues and single-owner composition root.
7. **Verify Core in a standalone .NET test project:** this is a fast repeatable gate before Unity integration.
8. **Add Unity project metadata and exact packages:** pin Unity editor patch, FishNet version and Test Framework package; capture lock/manifest.
9. **Implement Net adapters:** only after checking installed FishNet signatures and assembly names.
10. **Run EditMode, PlayMode, Host and Dedicated Server tests:** retain logs and XML test reports as evidence.

## 9. Status matrix

| Item | Status | Evidence / limitation |
|---|---|---|
| Uploaded file inventory | **VERIFIED (document scan)** | Full file materialized; byte size, line/word counts and code-fence counts measured. |
| Repository tree baseline | **VERIFIED (Git tree scan)** | No executable source/project/test artifacts at the audited commit. |
| Transcript is a canonical buildable source tree | **REJECTED** | It is an iterative transcript with multiple competing snapshots. |
| Core interfaces and services | **PROPOSED** | Snippets exist in the upload; not in repository and not compiled. |
| Core unit tests | **PROPOSED** | Test snippets exist; no test assembly or execution results. |
| FishNet adapters | **PROPOSED** | Multiple revisions; package version and API compatibility not verified. |
| Server authority | **APPROVED AS DESIGN PRINCIPLE / NOT IMPLEMENTED** | Repository documentation states the principle; no executable enforcement exists. |
| Build / compilation | **NOT VERIFIED** | No Unity project or .NET solution in repository. |
| Security tests | **NOT VERIFIED** | No executable test suite or evidence artifacts. |
| PlayMode / Host / Dedicated Server | **NOT VERIFIED** | No Unity/FishNet runtime project or test results. |

## 10. Explicit non-actions

This audit did **not**:

- Copy the 85,794-line transcript into the codebase.
- Create speculative Core/Net classes from unselected historical snippets.
- Delete or overwrite existing project documents.
- Claim compilation, test success, FishNet compatibility, security verification or playtesting.
- Modify `main`.

The safe next implementation unit is a canonical Core contract and test design, after the conflicting rules and ADR drafts are reconciled.