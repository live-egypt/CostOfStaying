# Stage 1 — Source Inventory Reconciliation

**Project:** Cost of Staying (ثمن البقاء)  
**Review date:** 2026-11-09  
**Status:** `PARTIAL — CURRENT SURFACES RECONCILED; ORIGINAL THREAD ACCESS INCOMPLETE`  
**Branch:** `docs/stage1-source-reconciliation`  
**Scope:** Documentation-only reconciliation of the current Project conversation-file surface, Project Library listing, and existing Stage 1 records.  
**Restrictions:** No Core/Net/Unity/Tests changes; no ADR or gameplay decision changes; no Stage 2 recovery; no merge to `main`.

## 1. Purpose and correction

This report updates the source-surface observations previously recorded in the Stage 1 inventory. It does not replace original sources and does not claim complete access to all Project conversations.

A fresh `files.list(surface="conversation")` returned **15 Project-surface attachment records**, not 11. The earlier 11-record count is therefore stale for the current conversation surface. The recursive Library listing returned **36 items: 1 folder and 35 file records**.

The counts refer to different surfaces and must not be added together or treated as unique source-document counts.

## 2. Current conversation attachment inventory

The current conversation surface exposes these 15 records. Repeated display names are separate records identified by their file IDs.

| # | Display name | Project file ID | Current classification |
|---:|---|---|---|
| 1 | `2 استكمال المحادثة.txt` | `file_0000000035308210ba764fa41dca76ef` | Report 02 — Source Recovery & Decision Traceability; raw hash recorded in prior Stage 1 inventory |
| 2 | `2 استكمال المحادثة.txt` | `file_000000008adc821088a91de351f5ada2` | Broad multi-source audit / first-round findings; raw hash recorded in prior Stage 1 inventory |
| 3 | `2 استكمال المحادثة.txt` | `file_00000000ab5482109ff976a094c0b9fe` | Report 03 — Source Recovery & Consistency Audit; raw hash recorded in prior Stage 1 inventory |
| 4 | `2 استكمال المحادثة.txt` | `file_0000000072e08210b2d34211cd183832` | Report 04 — Repository Forensic Audit; raw hash recorded in prior Stage 1 inventory |
| 5 | `رفع الملفات على دفعات.txt` | `file_00000000e5b88210973c72a025e1cdb8` | Batch intake note; raw hash recorded in prior Stage 1 inventory |
| 6 | `رفع الملفات على دفعات.txt` | `file_0000000075a882108078bf80ee654734` | Newly surfaced attachment; content classification pending |
| 7 | `قسم · قسم · رفع الملفات على دفعات.txt` | `file_000000005bb4821095c73b6c4a99e6cf` | Newly surfaced attachment; content classification pending |
| 8 | `قسم · قسم · قسم · استكمال المحادثة.txt` | `file_00000000f4588210afbd7b7c9b873a51` | Newly surfaced attachment; content classification pending |
| 9 | `قسم · قسم · رفع الملفات على دفعات.txt` | `file_000000004f408210a23cc447faaf4aa8` | Newly surfaced attachment; content classification pending |
| 10 | `قسم · قسم · رفع الملفات على دفعات.txt` | `file_00000000343482109ecb2381153c4550` | Newly surfaced attachment; content classification pending |
| 11 | `5.txt` | `file_00000000be5081f5b0278b728bd9a43e` | Security Audit reconciliation report (initial); not the original full Security Audit |
| 12 | `00.txt` | `file_00000000333c8210889b5112bd178f0c` | Newly surfaced project audit/reconciliation text; detailed source classification pending |
| 13 | `00.txt` | `file_000000004f3882108d820c554b6290b8` | Newly surfaced project audit/reconciliation text; distinct file ID; detailed classification pending |
| 14 | `قسم · قسم · استكمال المحادثة.txt` | `file_00000000b4b48210ae17137a2377fb16` | Newly surfaced source inventory/reconciliation report |
| 15 | `01.txt` | `file_00000000f2748210a36cd79959759fba` | Documentation governance / authorization planning text |

### 2.1 Hash boundary

The five hashes already recorded in the earlier Stage 1 inventory apply only to the five original Project-surface files in rows 1–5. No hash is asserted here for rows 6–15. The fact that those ten records are visible in the current listing does not establish their raw bytes, sizes, original upload time, or relation to earlier files.

The four `2 استكمال المحادثة.txt` records remain distinct; basename equality is not byte identity.

## 3. Project Library inventory — current listing

A fresh recursive Library listing of `/` returned:

- 1 folder: `/لعبة`
- 35 file records under `/لعبة`
- 36 total items

The listing includes the following Stage 1 artifacts:

- `/لعبة/SOURCE_INVENTORY.md`
- `/لعبة/PROVENANCE_REGISTER.md`
- `/لعبة/STAGE1_PROGRESS_LOG.md`
- `/لعبة/SHA256SUMS-STAGE1-LOCAL.txt`
- `/لعبة/STAGE_1.md`

It also includes `TBD_REGISTER.md`, `SOURCE_TRACEABILITY_REGISTER.md`, `DECISION_AND_CONFLICT_LOG.md`, Stage 2 records, review packages, manifests, diffs, and archives. This report does not inspect ZIP contents or infer their contents from filenames.

### 3.1 Important location/status inconsistency

The current Library listing establishes that the five Stage 1 artifacts above have Library records at the listed paths. However, the contents of `STAGE1_PROGRESS_LOG.md` say **“LOCAL ONLY — NOT SYNCED TO PROJECT LIBRARY OR GITHUB”**, and `SOURCE_INVENTORY.md` / `PROVENANCE_REGISTER.md` describe their outputs as local artifacts.

These statements conflict with the current Library record listing. This is a **recorded location/status inconsistency**, not proof of who uploaded the files or when the mismatch arose. Library metadata does not establish the initiating actor or causal upload event. Do not infer uploader identity.

The current evidence supports:
- Library record presence: **VERIFIED — METADATA**
- Extracted document content: **VERIFIED — TEXT**
- Raw-byte identity/checksum of these Library records: **NOT VERIFIED**
- Upload actor / causal history: **TBD**
- Whether these Library records are exact copies of earlier local artifacts: **NOT VERIFIED**

The Stage 0 Negative Attestation applies to these exact three records only: `PROGRESS_LOG(1).md`, `STAGE_2_CLOSURE_DECLARATION.md`, and `CostOfStaying_STAGE2_LOCAL.zip`. Its restrictions must not be expanded to unrelated Library records. Nevertheless, no raw-byte checksum is asserted for any Library file in this report because no authorized raw-byte export was performed for them.

## 4. Conversation and external-source provenance

The existing Stage 1 records identify the following unresolved source-access limits:

| Source | Current supported statement | Status |
|---|---|---|
| Project conversation `1` | User-attested as a former DeepSeek-related conversation renamed `1`; full thread content and thread ID are not exposed by the available file listing | USER-ATTESTED — CONTENT NOT RECOVERED |
| Project conversation `2` | User-attested as a former DeepSeek-related conversation renamed `2`; exact mapping to `محادثة 2.html` is not established | USER-ATTESTED — EXPORT MAPPING PENDING |
| `محادثة 2.html` | Library record exists; indexed header identifies ChatGPT Shared Chat; it is a candidate source, not proven to be Project chat `2` | TEXT/METADATA VERIFIED; MAPPING TBD |
| DeepSeek-native exports | No authenticated external DeepSeek export is identified in the currently listed files | SOURCE NOT RECOVERED |
| Gemini-native export | No Gemini-native export is identified in the current Library listing | SOURCE NOT RECOVERED |
| Network Audit original | Reports describe a Network audit, but the original full source is not identified as a standalone current attachment | SOURCE PARTIAL |
| Security Audit original | `5.txt` is an initial reconciliation report, not the original full Security Audit | SOURCE PARTIAL |

The Files surface does not expose a complete Project chat/thread index. Therefore “all project conversations reviewed” remains an unsupported claim.

## 5. Corrections to prior Stage 1 statements

| Prior statement / assumption | Current correction |
|---|---|
| Current conversation surface contained 11 records | Fresh listing returns 15 records. The earlier count is stale for this review. |
| Stage 1 artifacts are local-only and absent from Library | Current Library listing contains five Stage 1 artifact records. Their internal “local-only” statements are stale or otherwise inconsistent with current metadata; causal history is unknown. |
| `محادثة 2.html` is the original Project chat `2` | Candidate mapping only; not established. |
| DeepSeek originals are simply absent | User-attested Project conversations `1` and `2` are known, but their full contents/thread identities and external origin are not independently verified. |
| Current inventory is exhaustive for conversations | It is exhaustive only for the file records returned by the current conversation-file listing, not for Project chat threads. |

## 6. Required next actions within Stage 1

1. Read and classify the ten newly surfaced attachments (rows 6–10 and 12–15) individually; do not group by basename.
2. Compare their claims against the current Library records and the accessible GitHub branch without treating derived reports as primary sources.
3. Recover direct content/thread identity for Project conversations `1` and `2`, if the product exposes an authorized route; otherwise preserve the limitation.
4. Map TBD-01…20 to original requirement sources. If no source is found, retain `SOURCE NOT RECOVERED` / `PENDING SOURCE`.
5. Continue source validation for TBD-21…85 while preserving CONFLICT-01 and CONFLICT-02 as OPEN.
6. Reconcile the Stage 1 Library-location inconsistency through an authenticated upload/audit trail if one becomes available. Do not rewrite history or attribute the upload.
7. Do not begin Stage 2 Recovery until Stage 1 results are complete, presented, and explicitly accepted.

## 7. Stage gates and status

| Item | Status | Evidence / scope |
|---|---|---|
| Stage 0 | ACCEPTED WITH DOCUMENTED CONSTRAINT | Negative Attestation remains binding for its exact three Library records. |
| Stage 1 | IN PROGRESS — PARTIAL | 15 conversation-file records and 36 Library items inventoried; thread enumeration and source validation incomplete. |
| SOURCE_INVENTORY | IMPLEMENTED — PARTIAL | Existing Library document reviewed; this report records the updated surface count and corrections. |
| PROVENANCE_REGISTER | IMPLEMENTED — PARTIAL | Existing Library document reviewed; source and upload provenance gaps remain. |
| TBD-01…20 | PENDING SOURCE | Original requirement sources not yet mapped in this reconciliation. |
| TBD-21…58 | RECORDED — SOURCE VALIDATION PENDING | Existing register states retained; no new resolution claimed. |
| TBD-59…85 | REGISTERED — UNRESOLVED — SOURCE VERIFICATION PENDING | Existing register states retained. |
| CONFLICT-01 / CONFLICT-02 | OPEN | No owner selected. |
| Stage 2 | PARTIAL — CLOSURE BLOCKED | No Stage 2 Recovery performed by this report. |
| Stage 3 | BLOCKED | Stage 2 closure and explicit user acceptance remain prerequisites. |
| Code / Tests / ADRs | NOT CHANGED | Documentation-only scope. |
| `main` | NOT CHANGED | Branch is based on `feature/core-lifecycle-resolution`; no merge performed. |

**Conclusion:** The Stage 1 source-surface inventory has been corrected from 11 to 15 current conversation attachment records and now explicitly records the Library-presence contradiction for prior “local-only” artifacts. Stage 1 is still **PARTIAL**, not closed. This report does not verify the missing original conversations, resolve TBD items, or authorize Stage 2.
