# Stage 1 — Source and Provenance Review

**Revision:** 2026-11-10  
**Decision Status:** PROPOSED — review checkpoint, not a stage-closure decision  
**Evidence Maturity:** REVIEWED — bounded source and repository inspection  
**Stage:** Stage 1 — Source & Provenance Recovery  
**Overall result:** PARTIAL — OPEN

## 1. Purpose

This review evaluates whether the currently exposed source surfaces are sufficiently inventoried and attributable to support later decision and requirements reconciliation. It does not certify complete Project conversation enumeration, full original audit recovery, or Stage 1 closure.

## 2. Inventory result

| Source surface | Current record | Evidence boundary |
|---|---|---|
| Current-session materialized files | 10 text-file paths; 87,396 bytes; exact SHA-256 values in the session manifest | Exact bytes in this runtime only; not a complete Project attachment set. |
| Project conversation attachments | PR #8/#9 source snapshots report 15 distinct attachment records | Historical source-surface snapshot; full mapping to current runtime paths remains incomplete. |
| Project Library | PR #8/#9 inventory reports 40 file records + 1 folder | Metadata/text scope where raw-byte access was not available; no Library payload hash or archive-content claim. |
| Project conversation threads | Project chats labelled 1 and 2 are user-attested | Full thread contents and exact thread IDs unavailable. |
| ChatGPT HTML export | محادثة 2.html reported as a 449-message candidate | Exact mapping to Project chat 2 and raw-byte revalidation not established. |
| DeepSeek exports | No independent original export recovered in the current set | SOURCE PENDING. |
| Repository | Public repository live-egypt/CostOfStaying; default branch main | Current branch/commit claims are scoped to inspected refs. |

## 3. Provenance findings

1. Four same-basename Project attachment records named 2 استكمال المحادثة.txt were reported as separate file IDs with distinct sizes and hashes. They must not be deduplicated by display name.
2. The current runtime exposes only one materialized file path with that basename. This is a runtime materialization limitation, not evidence that the other attachment records do not exist.
3. Earlier descriptions associate chats 1 and 2 with DeepSeek, while attachment file_00000000343482109ecb2381153c4550 says the relevant sources are ChatGPT Project conversations, not DeepSeek-app chats. Origin remains OPEN.
4. Derived reports, exported conversations, Project Library records, repository copies, and Git commits are separate provenance objects.
5. The Stage 0 Negative Attestation limits claims about specific Library objects. Do not assert Library raw-byte hashes, ZIP contents, byte equality, or uploader attribution without authorized raw-byte evidence.
6. The complete original Network Audit and Security Audit have not been recovered.

## 4. Decision and requirements traceability

- PR #9 provides a transcribed 85-item TBD register and source/event mappings. Those mappings do not resolve the underlying requirement or prove complete original authorship/content.
- TBD-01…20 have reported D04 conversation-event evidence for assistant/user turns 284–295, while the source register retains PENDING SOURCE. This is an evidence-type reconciliation item, not an automatic resolution.
- TBD-37 and TBD-45 retain distinct owner/responsibility conflicts.
- The 109-record Consistency Matrix reports 30 groups and 24 pending-source records; primary-group completeness remains pending.
- TBD-53's 54 individual subtask mappings remain unavailable.
- RISK-013 appears with two different descriptions. The Risk Register remains deferred until source and identifier reconciliation is complete.

## 5. Repository/documentation package reconciliation

| Candidate | Base | Head | Files changed | Current state |
|---|---|---|---:|---|
| PR #8 | main | docs/stage1-source-provenance-recovery | 7 | Open / Draft / unmerged |
| PR #9 | feature/core-lifecycle-resolution | docs/source-provenance-reconciliation-2026-11-09 | 14 | Open / Draft / unmerged |
| This branch | main | docs/project-reconciliation-baseline-2026-11-10 | Consolidated source, governance, audit and evidence records | Review candidate; not merged |

PR #8 and PR #9 overlap in source inventory, provenance, traceability, document control and index changes, but have different paths and bases. This branch is a consolidation candidate, not an automatic acceptance of either package. PR #8/#9 must not be merged as-is.

## 6. Repository and CI evidence limits

- PR #6 contains standalone Core source and NUnit tests on feature/core-lifecycle-resolution; it is not merged to main.
- PR #7 is merged into that feature branch, not main.
- Core CI snapshots of 148, 177, 181 and 187 tests have been individually linked to distinct commits, workflow runs, jobs and artifacts in CI_EVIDENCE_REGISTER.md.
- These are standalone Core results only. They do not establish Unity/FishNet integration, Android/Linux builds, end-to-end network security, or production readiness.
- A complete recursive repository tree listing was not available through the connector. PR changed-file lists are not substituted for a full tree.

## 7. Stage 1 review conclusion

**Stage 1 remains PARTIAL — OPEN.** The currently visible source surfaces and branch-level evidence have been organized into a repository review candidate, but source completeness and several original-source/identity gaps remain unresolved.

### Open exit criteria

| ID | Required exit criterion | State |
|---|---|---|
| ST1-01 | Complete Project chat enumeration and direct thread IDs | OPEN |
| ST1-02 | Resolve ChatGPT/DeepSeek origin attribution for chats 1 and 2 | OPEN |
| ST1-03 | Map محادثة 2.html to the correct original conversation | OPEN |
| ST1-04 | Reconcile the 15-attachment snapshot with currently materialized files | PARTIAL |
| ST1-05 | Obtain complete original Network Audit | SOURCE PENDING |
| ST1-06 | Obtain complete original Security Audit | SOURCE PENDING |
| ST1-07 | Reconcile all source identifiers, TBDs, and 109 consistency records | PARTIAL |
| ST1-08 | Complete recursive repository document inventory | OPEN |
| ST1-09 | Owner review of canonical document paths and control baseline | PENDING REVIEW |
| ST1-10 | Owner acceptance of Stage 1 exit report | NOT REQUESTED / NOT ISSUED |

## 8. Explicit non-claims

This review does not declare:
- Stage 1 closed.
- Stage 2 closed.
- All conversations recovered.
- All 85 TBD requirements resolved.
- The 109-record consistency matrix complete.
- Network or Security Audit complete.
- Risk or Technical Debt registers ready for authoritative use.
- Core integrated with Unity/FishNet.
- Project production-ready.
