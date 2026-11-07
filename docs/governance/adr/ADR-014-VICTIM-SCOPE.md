# ADR-014 — Victim Scope

**Status:** PROPOSED  
**Date:** 2026-11-01  
**Related:** ADR-002 (Server Authority), ADR-013 (Safe Session Cleanup), `VictimId`

## Context

Core currently defines a distinct `VictimId` identifier. The available gameplay and domain documentation does not establish whether a victim must be a player or whether other entity categories, such as NPCs, may be victims.

Removing or redefining `VictimId` before that domain question is resolved would introduce a code-level change without a confirmed gameplay requirement.

## Decision (temporary engineering disposition)

1. Defer the domain decision about the permitted categories of victim.
2. Retain the existing `VictimId` type and its current code usage without modification.
3. Make no Core or test changes as a result of this ADR.
4. Treat the permitted victim categories as **TBD** until a gameplay/domain rule is explicitly documented and reviewed.

This is a temporary engineering disposition to preserve the existing contract while the domain question remains open. It is not a gameplay rule and does not resolve the domain scope.

## Explicit non-decisions

- This ADR does **not** approve NPCs as valid victims.
- This ADR does **not** establish that only players can be victims.
- This ADR does **not** finalize the semantics or long-term shape of `VictimId`.
- This ADR does **not** introduce support for any new victim category.
- Retaining `VictimId` is not evidence that any particular non-player entity category is supported.

## Conditions for reopening

Revisit this ADR if any of the following occurs:

1. A documented gameplay rule explicitly defines the permitted victim categories.
2. A confirmed requirement is raised to support NPCs as victims.
3. The Core entity model changes in a way that affects the meaning or ownership of `VictimId`.

## Alternatives considered

- **Remove `VictimId` now:** rejected because no confirmed requirement justifies the change.
- **Decide now that victims are players only:** deferred because the available domain documentation does not establish that rule.
- **Retain the current identifier and defer the domain decision:** selected as the least disruptive temporary engineering disposition.

## Consequences

- **Core:** no code change.
- **Tests:** no test change is required by this ADR.
- **Gameplay rules:** victim categories remain TBD.
- **Documentation:** this ADR and its index entries record the unresolved question and the temporary engineering disposition.
- **Verification:** this documentation does not certify implementation correctness or prove support for any victim category.

## Resolution gate

Before changing the identifier contract or implementing additional victim categories, the domain/design owner must document the allowed victim categories and their invariants. The identifier model can then be reviewed against that rule.
