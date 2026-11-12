# Stage 1 Review — Source & Provenance Recovery

**Project:** Cost of Staying (ثمن البقاء)  
**Review date:** 2026-11-09  
**Status:** `PARTIAL — CLOSURE BLOCKED`  
**Mode:** Read-only source analysis plus documentation-only repository outputs  
**Branch:** `docs/stage1-source-provenance-recovery`

## 1. Executive result

Stage 1 has advanced from an earlier five-attachment inventory to a current snapshot of **15 distinct Project attachment records**, each materialized through the authorized Project-file path and assigned a raw-byte SHA-256 digest. The recursive Project Library listing contains **40 file records and one folder**, inventoried at metadata level.

This is a material improvement in source provenance, but it does not establish complete recovery of all Project conversations or original audit documents. Stage 1 therefore remains **PARTIAL** and its closure gate is not met.

## 2. Verified in this pass (bounded claims)

| Claim | Result | Evidence scope |
|---|---|---|
| Current Project attachment list | 15 distinct file records | Fresh `files.list(surface=conversation)` result. |
| Current attachment bytes | 15 materialized raw files | Authorized Project attachment materialization. |
| Attachment SHA-256 | 15 digests calculated | Exact materialized bytes; values in SOURCE_INVENTORY. |
| Same-basename reports | Four `2 استكمال المحادثة.txt` records are distinct | Separate file IDs, sizes, contents, and digests. |
| Project Library inventory | 40 file records + one folder | Fresh recursive Library metadata listing. |
| Repository identity | `live-egypt/CostOfStaying`, default branch `main` | Read-only GitHub repository search. |
| Branch relationship | Feature branch HEAD `2820285...`; main HEAD `3dd9cfc...`; feature 441 ahead / 0 behind | Read-only GitHub compare. |
| PR #6 | Open Draft, not merged | Read-only GitHub PR search. |

These checks verify the bounded inventory statements above. They do not verify the truth of every report claim or the implementation status of the game.

## 3. Source recovery status

| Source family | Current status | What remains missing |
|---|---|---|
| Project attachments | `RECOVERED — 15 CURRENT RECORDS HASHED` | Earlier attachment-count snapshots need mapping. |
| Project Library | `INVENTORIED — METADATA ONLY` | Raw-byte export is blocked by Stage 0 limitation for identified records. |
| Project chat threads | `PARTIAL` | No complete Project chat-thread index/read operation exposed. |
| DeepSeek-origin chats `1` and `2` | `USER-ATTESTED — CONTENT PENDING` | Full transcripts/thread IDs and authenticated origin. |
| ChatGPT Shared Chat HTML | `LIBRARY RECORD PRESENT — MAPPING PENDING` | Exact mapping to Project chat `2`; raw-byte hash not revalidated. |
| Network Audit original | `SOURCE PENDING` | Complete original source not recovered. |
| Security Audit original | `SOURCE PARTIAL` | Initial reconciliation exists; complete original audit missing. |
| Stage 1 Library outputs | `RECORDS PRESENT; PROVENANCE PARTIAL` | Local-vs-Library source lineage and exact upload origin unresolved. |
| Stage 2 source package | `SOURCE RECONCILIATION REQUIRED` | Conflicts and original evidence mapping remain open. |

## 4. Corrections applied to the earlier inventory understanding

1. The current attachment surface returns 15 records, not 11. The earlier count of 11 is retained as a historical snapshot from Report 05, not overwritten.
2. Earlier reports mention 5 and 9 attachments. These counts may represent different dates or surfaces; they are not automatically contradictory.
3. Four same-basename continuation reports are distinct source artifacts. Their identities and raw-byte digests are now individually recorded.
4. Two `00.txt` attachments are also distinct records with separate IDs and hashes.
5. The current Project Library listing returns 40 file records plus one folder; this corrects the earlier 35-file snapshot in the prior inventory report. Its contents are not treated as raw-byte verified.
6. The feature branch `feature/core-lifecycle-resolution` is 441 commits ahead of `main` and contains the existing mixed-scope PR #6 work. The Stage 1 documentation branch is therefore based directly on the observed `main` SHA to isolate this docs-only PR.

## 5. Blocking gaps

- Complete enumeration and direct reading of all Project conversation threads are unavailable through the current tool surface.
- DeepSeek-origin conversation content and exact thread identity remain unverified.
- The ChatGPT Shared Chat HTML export is not proven to map to Project conversation `2`.
- Original Network Audit has not been recovered.
- Original complete Security Audit has not been recovered.
- Library raw-byte export and upload-audit access remain unavailable under the Stage 0 Negative Attestation.
- Historical attachment counts (5/9/11) need source-snapshot mapping.
- Decision approvals need exact original user-message/thread references.
- Stage 2 requirements, conflicts, and evidence are not fully reconciled.

## 6. Stage 1 exit criteria

| Exit criterion | Result |
|---|---|
| Current exposed attachment surface enumerated | `PASS — CURRENT SNAPSHOT` |
| Current attachment raw bytes hashed | `PASS — 15 RECORDS` |
| Project Library metadata inventoried | `PASS — 40 FILES + 1 FOLDER` |
| Same-basename attachment identity reconciled | `PASS — DISTINCT RECORDS` |
| Complete Project conversation index recovered | `BLOCKED — NOT EXPOSED` |
| DeepSeek-origin chat contents recovered | `BLOCKED — SOURCE PENDING` |
| Original Network Audit recovered | `BLOCKED — SOURCE PENDING` |
| Original Security Audit recovered | `BLOCKED — SOURCE PARTIAL` |
| Decision traceability to original user messages complete | `BLOCKED — PARTIAL` |
| Stage 0 Library provenance limitations resolved | `BLOCKED — RAW BYTE/AUDIT ACCESS UNAVAILABLE` |

**Stage 1 exit decision: NOT SATISFIED.** Do not declare Stage 1 closed and do not proceed to Stage 2 Recovery until the user reviews and explicitly accepts this Stage 1 result and the remaining blockers are dispositioned.

## 7. Required next actions

1. Obtain or expose complete exports/thread identifiers for the project conversations `1` and `2`, and map the HTML candidate.
2. Obtain the original Network Audit and complete Security Audit, if available.
3. Continue source-to-requirement mapping only after the Stage 1 review gate is accepted.
4. Preserve the 5/9/11 historical counts and map each to its exact source snapshot.
5. Continue with Stage 2 only under its existing entry and closure criteria; do not infer closure from a declaration file or a completed package.

## 8. Change-control statement

- This Stage 1 PR changes documentation only.
- No Core, Net, Unity, Tests, ADR files, or gameplay rules were changed.
- No tests were run.
- No Stage 2 Recovery, Risk Register, Technical Debt Register, or Final Assessment was performed.
- No changes were made directly to `main`.
- No PR was merged.
