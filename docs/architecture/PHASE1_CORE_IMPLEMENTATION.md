# Phase 1 Core — Initial Contracts and Primitives

**Status:** VERIFIED — standalone .NET Core build and NUnit tests passed in CI  
**Architecture ADR:** ADR-005 (APPROVED 2026-11-01)  
**Identifier/ledger ADR:** ADR-006 (PROPOSED — pending owner approval)  
**Technical register:** docs/architecture/PHASE1_TECHNICAL_MASTER.md  
**Gameplay baseline:** v2.1 / PROPOSED FOR PLAYTEST  
**Unity assumption:** 6000.3 LTS — not verified by a Unity project  
**FishNet assumption:** 4.7.4R — not pinned or verified; no FishNet adapter is included.

## Scope delivered

- Engine-independent .NET Standard 2.1 Core project.
- Strongly typed numeric wrappers: ActorId, VictimId and EventSequenceId (ulong).
- IVitalityAccount and IBetrayalLedger pure contracts.
- VitalityAccount with bounded vitality and terminal Alive → Dead behavior.
- BetrayalLedger with monotonic event sequence, exact replay rejection and actor score accumulation.
- Standalone NUnit tests targeting .NET 8, including identifier value-semantics coverage.
- GitHub Actions workflow that restores, builds and tests the standalone Core project.

## Vitality semantics

The v2.1 gameplay specification defines zero Vitality as Dead and prohibits Dead → Alive revival in the MVP.

| Current state | Lose | Restore |
|---|---|---|
| Alive | Apply; reaching zero transitions to Dead | Apply, capped at Maximum |
| Dead | No-op | No-op; cannot revive |

**Lifecycle conflict — ADR-007:** this implementation follows the current repository v2.1 proposal (Alive → Dead, no MVP revival, Spirit separate). An earlier explicit owner-approved Phase 1 decision (2026-10-31) specified Alive / Spirit / Eliminated, automatic Spirit at zero Vitality, and explicit Resurrect / Eliminate operations. ADR-005's Core-boundary approval does not by itself prove that this earlier gameplay decision was revoked. The current code is CI-verified as code, but its lifecycle semantics remain provisional until ADR-007 is resolved.

The account rejects an invalid/default ActorId(0) player identity at construction. The numeric wrapper can still represent zero so default(ActorId) remains detectable; IsValid is the explicit validity check.

## Betrayal ledger semantics

- ActorId, VictimId and EventSequenceId are numeric ulong wrappers. Zero is reserved and invalid for domain events.
- A valid BetrayalEvent requires non-zero sequence, actor and victim IDs; actor and victim must differ.
- The ledger validates BetrayalEvent.IsValid because default(BetrayalEvent) bypasses its constructor.
- New events require a strictly increasing EventSequenceId; gaps are allowed and sequence values need not be contiguous.
- Exact replay returns false and does not increment score, including replay after newer accepted events.
- Reusing a sequence ID with different event data throws InvalidOperationException.
- ServerTick is informational and is not the ledger ordering key. Equal or decreasing tick values do not reorder events; the authoritative pipeline remains responsible for valid tick generation.
- The actor's score increases once per accepted event.
- The server application layer must call this ledger only after a successful authoritative transfer. This ledger does not validate gameplay eligibility or perform Debuff transfer.
- Calls are single-writer/server-serialized. No locking, persistence, clock, or network dependency is included.

## Deliberate exclusions

No Unity project, Unity assemblies, FishNet adapters, connection identity mapping, command transport, server tick scheduler, Debuff model, atomic transfer system, persistence, or backend has been added. IdentifierTests covers numeric preservation, validity, equality/inequality, zero sentinel behavior, maximum values and separation between identifier types. Zero is representable but invalid for runtime identities. No tuning values beyond default Vitality maximum 100 have been invented.

## Verification evidence

- **Commit tested:** e86880a318df49164aa61fe5e43c7a47f9c44560
- **GitHub Actions workflow:** Core CI
- **Workflow run:** 41255553487 — completed successfully
- **Job:** Build and test standalone Core — success
- **Build:** .NET 8 Release build of CostOfStaying.Core — success
- **Tests:** 38 passed, 0 failed, 0 skipped
- **Test duration:** 21 ms
- **Artifact:** core-test-results, artifact ID 13062876559 (TRX result retained by GitHub Actions)
- **Latest workflow rerun:** 41255769742 — all Restore, Release build, NUnit and artifact-upload steps succeeded; artifact ID 13062794038 (TRX result retained). This rerun used the same Core source; subsequent branch commits were documentation/governance-only.
- **Evidence report:** docs/audit/PHASE1_CORE_CI_VERIFICATION.md

This is VERIFIED for the standalone Core build/test scope only. It does not verify Unity import, FishNet integration, server authority across a live network, gameplay balance, dedicated-server operation, or production security. It also does not resolve the conflicting owner-approved player lifecycle contract recorded in ADR-007.

## Remaining status

- Unity/FishNet integration: **NOT IMPLEMENTED**.
- FishNet 4.7.4R compatibility: **NOT VERIFIED**.
- Gameplay rules: remain **PROPOSED FOR PLAYTEST**.
- ADR-006: remains **PROPOSED** pending owner approval.
- Vitality/player lifecycle alignment: **BLOCKED** pending ADR-007 owner decision.
