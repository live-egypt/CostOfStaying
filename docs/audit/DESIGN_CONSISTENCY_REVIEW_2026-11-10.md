# Design Consistency Review — 2026-11-10

**Revision:** 2026-11-10  
**Decision Status:** PROPOSED — no gameplay rule changed by this review  
**Evidence Maturity:** REVIEWED — main gameplay, system, architecture, security, quality and roadmap documents inspected  
**Scope:** Current main documentation snapshot at 3dd9cfc27aa48b09b3cde802e266a8221c39f0d3, with branch-scoped ADR-007 treated as a separately identified source.

## 1. Review method

The review compares the project charter, GDD, game-rule specification, player experience, system specifications, architecture, security, quality, operations and MVP roadmap. It distinguishes:
- A direct contradiction.
- Ambiguous terminology.
- An unresolved parameter or design choice.
- A mismatch between intended behavior and evidence of implementation.
- Two documents that describe different aspects without actually contradicting each other.

No design choice is inferred from recency or branch location. Every issue remains open until its source, owner decision, or explicit acceptance rule is identified.

## 2. Findings

| ID | Documents / areas | Observation | Classification | Current status | Required resolution |
|---|---|---|---|---|---|
| DES-001 | Main ADR-004, 02_GAME_RULES_SPECIFICATION, COLLAPSE_SYSTEM, SPIRIT_SYSTEM vs PR #6 branch ADR-007 | Main proposes Alive → Dead at zero Vitality with no revival; branch ADR-007 marks Alive → Spirit canonical, allows explicit Resurrect and Eliminate, and marks Eliminated terminal. | Material player-lifecycle conflict across refs. | OPEN — OWNER TRACE REQUIRED | Recover the exact owner approval source and decide whether/how ADR-007 is adopted into main; reconcile all dependent docs and Core only after that decision. |
| DES-002 | COLLAPSE_SYSTEM.md | The line describing the zero-Vitality transition uses Alive → Dead/Spirit, which leaves two different lifecycle outcomes in one rule. | Ambiguous/contradictory state transition. | OPEN | Replace the mixed expression only after DES-001 is resolved. |
| DES-003 | SPIRIT_SYSTEM.md | The role description says Alive → Dead and then Spirit, while its title and role semantics describe Spirit as the post-death participation state. | Terminology ambiguity tied to lifecycle conflict. | OPEN | Use one canonical transition/state vocabulary after DES-001. |
| DES-004 | 02_GAME_RULES_SPECIFICATION.md RULE-05 vs ENDGAME_SYSTEM.md | RULE-05 groups Time Limit and Mutual Collapse into one result category with two causes; ENDGAME_SYSTEM lists them as two separate outcomes, producing four named outcomes instead of three grouped results. | Outcome taxonomy / presentation ambiguity; may be a valid grouping difference. | OPEN | Decide whether result type is three categories with multiple causes or four distinct terminal result IDs; keep UI, telemetry and acceptance tests consistent. |
| DES-005 | BETRAYAL_SYSTEM.md, 02_GAME_RULES_SPECIFICATION.md, ACCEPTANCE_CRITERIA.md | Betrayal point thresholds at 3/5/7+ are inherited hypotheses; effects are not completely specified, yet tests are requested around their thresholds. | Unresolved gameplay tuning and incomplete effect contract. | PROPOSED FOR PLAYTEST | Keep thresholds non-final; define stacking/replacement, cap, persistence and interaction with Collapse before implementing their effects. |
| DES-006 | SUFFERING_TRANSFER_SYSTEM.md, DATA_ARCHITECTURE.md, SECURITY_ARCHITECTURE.md, ADR-015 on Core branch | The transfer flow refers to RequestId and idempotency, while source material separately distinguishes sequence replay, command-result idempotency, reconnect and restart protection. | Guarantee-boundary ambiguity. | OPEN | Specify key identity, retention boundary, atomic reservation/result persistence, reconnect and restart guarantees; do not treat sequence tracking alone as exactly-once effect. |
| DES-007 | COMBAT_SYSTEM.md vs gameplay/system specifications | Combat mentions applying a Collapse effect after damage “if enabled”, but the trigger, scope, stacking and authority contract are not specified elsewhere in the reviewed main baseline. | Unspecified interaction. | TBD | Decide whether combat can trigger Collapse and define exact rule/limits before implementation. |
| DES-008 | COLLAPSE_SYSTEM.md, SUFFERING_TRANSFER_SYSTEM.md | Collapse suggests a maximum of three active burdens; transfer requires target capacity and moves the last eligible Debuff. | Related constraints currently compatible but incomplete. | OPEN — DETAIL | Define burden capacity by Debuff type, transfer eligibility, expiry, stack behavior and atomic rejection behavior. |
| DES-009 | SPIRIT_SYSTEM.md and PLAYER EXPERIENCE | Spirit Energy, Echo count, hint cooldown and scoring are TBD; the player-experience document says not to present Energy as a final UI rule. | Explicitly deferred design parameters. | TBD — PRESERVE | Do not expose unapproved UI rules or implement placeholder balance as final. |
| DES-010 | DATA_ARCHITECTURE.md, THREAT_MODEL.md, SECURITY_TEST_PLAN.md | Idempotency is listed as a desired control, but no end-to-end integration evidence establishes it across reconnect or server restart. | Design-to-evidence gap. | NOT VERIFIED | Trace guarantee to implementation and reproducible tests after policy approval. |
| DES-011 | PERFORMANCE_BUDGET.md, DEVICE_COMPATIBILITY.md, PERFORMANCE_TEST_PROTOCOL.md | 30 FPS, 400 MB, 100 MB APK and Redmi Note 8 appear as historical/initial targets; exact device SKU and tested baseline are not established. | Target-versus-measurement distinction. | NOT VERIFIED | Freeze device SKU, build, conditions, metric definitions and acceptance thresholds only after measured baseline and owner approval. |
| DES-012 | TEST_STRATEGY.md, ACCEPTANCE_CRITERIA.md, MVP_SCOPE.md | Full four-player match and integration are acceptance goals, while the recovered tests are standalone Core CI only. | Acceptance/evidence gap. | NOT VERIFIED | Add Unity/FishNet/host/dedicated-server integration tests and Android/Linux build evidence when those projects exist. |
| DES-013 | ADR-001, TECHNICAL_ARCHITECTURE, NETWORK_ARCHITECTURE, main README | Unity/FishNet/Linux Dedicated Server stack is proposed; FishNet 4.7.4R is not evidenced as a resolved/pinned dependency. | Decision and dependency verification gap. | PROPOSED / NOT VERIFIED | Resolve exact package/editor version in a real Unity project and verify adapter APIs before writing network code. |
| DES-014 | BACKEND_AND_HOSTING, DATA_ARCHITECTURE, DEPLOYMENT_ARCHITECTURE | Backend provider, persistent data, match recovery, retention and production hosting remain undecided. | Explicit architecture TBD. | PROPOSED | Retain local/staging prototype boundary; decide provider only after a measured prototype and requirements review. |
| DES-015 | TRUST_BOUNDARIES, SECURITY_ARCHITECTURE, ADR-002 | Server authority is approved as a principle, but implementation and end-to-end authority verification are not established by these documents. | Decision/evidence separation. | APPROVED / DOCUMENTED; implementation not established | Bind every sensitive command to server validator, application handler, tests, and retained evidence. |

## 3. Non-conflicts retained

The following items are related and should not be merged into a single issue:
- Three-player surrender vote and the 15-second Collapse interval for three Alive players are consistent as separate rules.
- Three active Debuffs maximum and a target-capacity check are compatible, although the exact Debuff model remains incomplete.
- Spirit Witness Score is a separate score and does not inherently contradict the rule that a living player wins Last Survivor.
- Project stages (Stage 0–5) and engineering phases (Phase 1–3) are different control structures.
- Client intent, server validation, and server-authoritative state ownership are consistent as a principle; runtime enforcement remains an evidence question.

## 4. Design decision gates

No change to the gameplay specification should proceed until these gates are met:

1. Resolve DES-001 from the exact owner decision source; keep branch-specific ADR-007 identified by branch and SHA until adopted.
2. Normalize the player lifecycle vocabulary across rules, Collapse, Spirit, endgame, Core contracts and tests.
3. Resolve terminal result identifiers and presentation for DES-004.
4. Define Betrayal effects, Debuff eligibility/capacity, and combat-to-Collapse interaction before implementing those rules.
5. Define command identity/idempotency/reconnect/restart guarantees as separate contracts.
6. Convert tuning hypotheses into measurable playtest hypotheses and collect the required sessions before ADR-004 approval.
7. Keep implementation status and verification scoped to actual code, test runs, build targets and artifacts.

## 5. Status summary

| Topic | Decision Status | Evidence Maturity | Work State |
|---|---|---|---|
| Player lifecycle | CONFLICT — no new decision | REVIEWED | OPEN |
| Endgame result taxonomy | PROPOSED | REVIEWED | OPEN |
| Betrayal thresholds | PROPOSED FOR PLAYTEST | DOCUMENTED | TBD |
| Transfer idempotency contract | PROPOSED | REVIEWED | OPEN |
| Combat/Collapse interaction | TBD | REVIEWED | NOT SPECIFIED |
| Android performance targets | PROPOSED | DOCUMENTED | NOT MEASURED |
| Unity/FishNet stack | PROPOSED | DOCUMENTED | NOT PINNED / NOT VERIFIED |
| Backend/hosting | PROPOSED | DOCUMENTED | TBD |
| Server-authority principle | APPROVED | DOCUMENTED | Runtime enforcement not verified |

## 6. Non-claims

This review does not approve or reject any gameplay rule, replace ADR-004 or ADR-007, establish the existence of implementation defects, or certify readiness. Its function is to direct the next evidence/decision review.
