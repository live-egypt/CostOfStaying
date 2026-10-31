# ADR-005 — Core Executable Boundary and First Implementation Slice

**Status:** APPROVED — project-owner approval received 2026-11-01  
**Date:** 2026-11-01  
**Related:** ADR-001, ADR-002, ADR-004; `docs/02_GAME_RULES_SPECIFICATION.md`

## Context

The repository currently contains the v2.1 documentation baseline but no executable Unity project, Core source, networking source, test assemblies, or package manifest. The uploaded Phase 1 Markdown is an iterative transcript with competing snapshots; it cannot safely be copied as a single implementation.

The game rules establish a server-authoritative match model. Vitality begins at 100 and reaches terminal Dead at zero; successful suffering transfer is a full move of an eligible Debuff, requires both players Alive in the same match, a different target within 2 metres, source cooldown readiness and target capacity, and must mutate neither player if rejected. Only a successful transfer increments the source Betrayal score. These rules remain PROPOSED FOR PLAYTEST under ADR-004.

## Decision proposal

Build one canonical, engine-independent Core and treat Unity/FishNet as outer adapters.

1. **Core purity:** domain logic uses ordinary C# and has no dependency on `UnityEngine`, FishNet, `MonoBehaviour`, `NetworkConnection`, Unity time, or Unity logging.
2. **Authority:** Core state mutation is invoked only by server-owned application services. Network clients submit intent; sender identity is derived from the authenticated server-side connection/session mapping, never trusted from a client payload.
3. **Contracts first:** define pure interfaces and value objects before concrete services. Each public state transition and rule function receives unit tests before it is considered complete.
4. **Single canonical type per responsibility:** one definition each for identifiers, result types, match/player state, command envelope, lifecycle service, movement command and snapshot contracts. Historical transcript variants are not imported.
5. **Deterministic time:** Core receives a server tick/time value from its caller. It does not read wall-clock time or generate authoritative time internally.
6. **Atomic transfer:** validate every transfer precondition before mutation; a failed transfer changes no player state, cooldown, ledger or score. A successful transfer moves the full selected Debuff and increments Betrayal exactly once. Idempotency/replay protection is part of the application boundary.
7. **No speculative infrastructure:** persistence, backend, prediction, ECS, custom transport and production hosting remain outside the first Core slice unless a separate approved ADR changes scope.

## First implementation slice

Implement and test in this order:

1. Common result/error representation and validated identifiers.
2. Match/player lifecycle primitives matching the documented state matrix.
3. Vitality state with bounded values and terminal death semantics.
4. Debuff model and transfer eligibility/rules.
5. Atomic transfer application and Betrayal score/event recording.
6. Command admission and replay/idempotency boundary.
7. Server tick orchestration and immutable state projection.
8. Thin Unity/FishNet adapters only after the exact package versions and API signatures are available.

The first slice must not silently decide unspecified balance parameters such as vitality decay rate, Collapse accumulation, Debuff duration, cooldown duration, or stack capacity. Those remain configuration inputs or TBD values until playtest evidence exists.

## Assembly and test boundary

Proposed Unity layout:

```text
Assets/CostOfStaying/
  Core/       CostOfStaying.Core.asmdef (no engine references)
  Net/        CostOfStaying.Net.asmdef (references Core and the installed FishNet assembly)
  Unity/      Unity-facing bootstrap/presentation adapters
  Tests/
    EditMode/ pure Core tests
    PlayMode/ actual Unity/FishNet integration tests
```

The exact FishNet assembly reference must be read from the installed package; it must not be guessed. FishNet version 4.7.4R is only a transcript assumption until a real package manifest/lock and installed source establish the version.

## Consequences

### Positive

- Prevents historical transcript revisions from becoming duplicate/conflicting source files.
- Keeps game rules testable without Unity runtime dependencies.
- Makes authority and identity boundaries explicit.
- Allows deterministic unit tests before network integration.
- Avoids freezing untested balance values into Core.

### Costs and risks

- Requires an initial canonical contract pass before implementation.
- Unity project/package metadata must be added before FishNet integration can be compiled.
- Some domain details remain intentionally configurable until playtest.
- Core tests alone cannot prove FishNet integration, Host mode, Dedicated Server behavior or production security.

## Rejected alternatives

- **Bulk-copy the uploaded transcript:** rejected because it contains repeated declarations, evolving contracts, incomplete adapter stubs and malformed C# examples.
- **Implement FishNet adapters first:** rejected because the repository does not pin or contain FishNet and because the sender/session trust boundary must be designed before transport wiring.
- **Implement all persistence and backend infrastructure in Phase 1:** rejected as premature and outside the documented MVP proof requirements.

## Acceptance criteria

The project owner approved the proposed Core boundary and first-slice scope on 2026-11-01. Code may be marked IMPLEMENTED only when committed; VERIFIED requires reproducible compilation and test evidence. Gameplay rules remain PROPOSED FOR PLAYTEST until the playtest gate in ADR-004 is satisfied.
