# Provenance Register — Cost of Staying

> **Governance status axes:** Decision Status and Evidence Maturity are independent. This document's existence does not approve its claims or prove implementation. See [Project Governance Baseline](PROJECT_GOVERNANCE_BASELINE.md).

**Revision date:** 2026-11-12 — DOC-B-03 bounded HTML text/metadata inspection and owner-attested identity relation  
**Record State:** INITIAL RECONCILIATION SNAPSHOT — DOCUMENTATION ONLY; CONTENT AND SOURCE COMPLETENESS REMAIN PARTIAL
**Binding control:** Project Library `NEGATIVE_ATTESTATION_STAGE0.md` (recorded SHA-256: `2340032b4b60a50f159a091cd590a8ec85b10207f576ec48af42004bc8c8131d`).  
**Purpose:** Identify where claims and records came from, what each source can prove, and what remains unverified.

## 1. Mandatory provenance rules

1. A conversation, exported transcript, derived report, Project attachment, Library record, and repository document are separate provenance objects.
2. `USER-ATTESTED` is not independent verification of source content or external platform origin.
3. `RECORDED` is not `SOURCE VERIFIED`; `APPROVED` is not `IMPLEMENTED`; `IMPLEMENTED` is not `VERIFIED`.
4. Project attachment hashes in this register were calculated from actual raw bytes materialized through the authorized Project-file path.
5. Project Library records are governed by Stage 0 Negative Attestation. Metadata/extracted text may be used where accessible; no Library raw-byte hash, byte comparison, archive-content claim, or uploader attribution is made.
6. A reported test result is scoped to its exact commit, workflow run, environment, command, and artifact. It does not prove unrelated integrations or current branch state.
7. When source text, exact approval event, or date is unavailable, record `SOURCE PENDING` or `NOT PRESENT IN EXPORT`; do not reconstruct it from inference.

## 2. Conversation provenance

| ID | Object | Origin / evidence | Status | Missing evidence |
|---|---|---|---|---|
| CHAT-001 | Project chat `1` | Owner attests that Project chat labels `1` and `2` and the DeepSeek references denote one underlying conversation; a prior report attributes a contrary platform statement to attachment `file_00000000343482109ecb2381153c4550`, which is not available for direct inspection in this run | OWNER-ATTESTED IDENTITY RELATION — PLATFORM ORIGIN / THREAD / COMPLETENESS UNVERIFIED | Full original thread, direct thread ID, directly inspected source statement, authenticated platform-origin evidence |
| CHAT-002 | Project chat `2` | Owner attests `محادثة 2.html` is associated with the same underlying conversation as the DeepSeek references and Project chat labels `1`/`2`; owner warns that some parts may be elsewhere | OWNER-ATTESTED EXPORT RELATION — TECHNICAL MATCH / COMPLETENESS UNVERIFIED | Full original thread, direct thread ID, complete segment inventory, authenticated platform-origin evidence |
| CHAT-003 | `محادثة 2.html` | Same file ID is exposed in current conversation and Library path `/لعبة/محادثة 2.html`; extracted HTML header says `ChatGPT Shared Chat`, links to `https://chatgpt.com/share/6aef6082-3c6c-83ea-95c9-ee863196fadc`, and displays export timestamp `٧‏/١١‏/٢٠٢٦، ٧:٣٣:٥٩ م`; text inspection counted 226 user + 223 assistant turn elements (449 total) across 79,302 lines; no literal `DeepSeek` occurrence; terminal `</html>` present | TEXT/METADATA VERIFIED — UNDERLYING ORIGIN / THREAD MATCH / COMPLETENESS NOT VERIFIED | Raw Library bytes/hash; original Project/DeepSeek thread ID and direct source export; evidence that no segments are missing |
| CHAT-004 | Current active Project thread | Current conversation context | CURRENT CONTEXT AVAILABLE | Separate full export |
| CHAT-005+ | Other surfaced Project chat references | Project context summaries | CONTEXT-SURFACED | Full thread objects and complete thread index |

Known surfaced titles include `استكمال المحادثة`, `قسم · استكمال المحادثة`, `2 استكمال المحادثة`, `رفع الملفات على دفعات`, and `مراجعة شاملة للمشروع`. This is not a complete Project chat index.

Owner attestation (2026-11-12): the DeepSeek references, Project conversation labels `1` and `2`, and `محادثة 2.html` refer to the same underlying conversation. The owner confirms that the HTML is associated with that same DeepSeek-referenced conversation, while warning that some parts may exist elsewhere and may not be present in this export. This is an attested relationship, not a technically verified source/thread match or completeness finding.

The accessible HTML artifact self-identifies as `ChatGPT Shared Chat`, links to `https://chatgpt.com/share/6aef6082-3c6c-83ea-95c9-ee863196fadc`, contains 226 user and 223 assistant turn elements, and has no literal `DeepSeek` occurrence in its extracted text. This does not establish the underlying platform origin. The prior report's description of attachment `file_00000000343482109ecb2381153c4550` remains a reported claim only: the attachment was not available in the current conversation/Library listing and was not directly re-inspected. Preserve the conflict.

## 3. Project attachment provenance — raw-byte verified

Each record below was materialized individually from its Project file ID into `/mnt/data/cos-source-inputs/`. SHA-256 was computed using `sha256sum` against the resulting raw file.

| Project file ID | Bytes | SHA-256 | Source classification |
|---|---:|---|---|
| `file_0000000035308210ba764fa41dca76ef` | 8,163 | `5bc108ffd11326d10d83562198d0fdca99fe7a3fe49204952ed020152ecd866d` | Report 02 — Source Recovery & Decision Traceability |
| `file_000000008adc821088a91de351f5ada2` | 12,518 | `62daf362f021b2ecffb8aeec7dd6c576c50c325a0afb022d4ffd27b4eb2534aa` | Broad multi-source audit |
| `file_00000000ab5482109ff976a094c0b9fe` | 9,828 | `ab52d5c2a632b1e209b1d3c3cc0bd3070d63c2605bcc93189671890d916374e2` | Report 03 — Source Recovery & Consistency Audit |
| `file_0000000072e08210b2d34211cd183832` | 14,149 | `cf8f199c83c5025408eeaa31731e868bb053bf75fcd4da4b1d1f17445bd0a6ec` | Report 04 — Repository Forensic Audit |
| `file_00000000e5b88210973c72a025e1cdb8` | 4,719 | `02688c4ab66f7cb6a32896159854b62028fcab019072967c09fc334c2e438333` | Batch intake log |
| `file_0000000075a882108078bf80ee654734` | 3,585 | `e4351e191907a038a4340118a888e19a962eb3e173a21152ab6f40d6ea5a58df` | Reconciliation direction log |
| `file_000000005bb4821095c73b6c4a99e6cf` | 4,972 | `0a0e459edd8717aa2e3fe26d630e3f130f6c88d46ae05b651350312c6de9f698` | Security reconciliation intake |
| `file_00000000f4588210afbd7b7c9b873a51` | 7,437 | `fed4978a62d4d1a54c6f1848c5b36d4581be5dfb79181a1cddf50bdf84ff289f` | Stage 1 source recovery report |
| `file_000000004f408210a23cc447faaf4aa8` | 1,979 | `d1a9e4c94d8f10a5d60c0e333cf5f0af55bc291c645085d419d2e9fa6fe86f4e` | Package structure reconciliation intake |
| `file_00000000343482109ecb2381153c4550` | 11,881 | `4eb0b34fa66c06b4fbcb9b5afdc17a132298bbf3a32882e15c14ebf77da5a618` | Document mapping and source reconciliation |
| `file_00000000be5081f5b0278b728bd9a43e` | 17,454 | `5cf496c1183f36d23ef46d22b3afb7438728df70175bd17f0f46014c130dce60` | Security Audit reconciliation — initial |
| `file_00000000333c8210889b5112bd178f0c` | 12,519 | `b7d134696c9229b988decc7ba204bc9e7b723638eb0bbd117aae401f1c02ab9f` | Reconciliation source report |
| `file_000000004f3882108d820c554b6290b8` | 12,581 | `6e403e3e05fa6eb9d0b89041139931f0a67a604435a75ad0d7c9d0d1f5125971` | Traceability reconciliation report |
| `file_00000000b4b48210ae17137a2377fb16` | 9,625 | `0177fcd7e59f79db23d18ff9b820841618fbc29bd5b071a8f43f6546c35d6f96` | Report 05 — Source Inventory & Provenance Reconciliation |
| `file_00000000f2748210a36cd79959759fba` | 9,565 | `f6a87965829fcd44f787b9e984340d659d1748258e934672de7618f09b44c26b` | Continuation plan |

**Collision rule:** Four records named `2 استكمال المحادثة.txt` have different file IDs and hashes. They are distinct reports. Their common basename does not make them duplicate source transcripts.

## 4. Library provenance — TEXT/METADATA ONLY

The 40-file/one-folder list in `SOURCE_INVENTORY.md` Section 4 is a 2026-11-09 historical snapshot. A fresh recursive listing at 2026-11-11T08:55:12.632764Z returned 45 file records with 45 unique IDs plus one folder; all 40 historical IDs remain, with five additional records. Exact names, IDs, sizes, and modified timestamps are in `SOURCE_INVENTORY.md` Section 4.1. Four additional text records were read and are derivative/draft reports; `STAGE_1_WORKING_OUTPUTS.zip` is metadata-only and was not opened. The active conversation file listing returned one `محادثة 2.html` object with the same ID as the Library record; this does not establish mapping to Project chat `2` or platform origin. The 21 historical attachment IDs from the 2026-11-10 snapshots are separate from the 45 Library IDs. The complete pre-write repository tree contains 87 blobs; five Library basenames match repository paths, as detailed in `SOURCE_INVENTORY.md` Section 4.1. This is a metadata/text-level crosswalk only; no Library raw-byte hash or content-equality claim is made. Stage 0 Negative Attestation applies specifically to the three records in Section 4.1 below and must not be generalized to every Library record. The current Library contains, among others:
- `SOURCE_INVENTORY.md`, `PROVENANCE_REGISTER.md`, `SOURCE_TRACEABILITY_REGISTER.md`
- `TBD_REGISTER.md`, `CONSISTENCY_MATRIX.md`, `DECISION_AND_CONFLICT_LOG.md`
- `FULL_DOCUMENTS_REVIEW.md`, `REVIEW_INDEX.md`, `FINAL_REVIEW_REPORT.md`
- `STAGE_1.md`, `STAGE_2.md`, `STAGE_2_REVIEW.md`, `STAGE_2_CLOSURE_DECLARATION.md`
- `PROGRESS_LOG.md`, `PROGRESS_LOG(1).md`, `PROVENANCE_RECONCILIATION_NOTE_STAGE0.md`, `NEGATIVE_ATTESTATION_STAGE0.md`
- `CostOfStaying_STAGE2_LOCAL.zip`, `CostOfStaying_Stage2_Review_Package_2026-11-08.zip`, `CostOfStaying_Docs_Diff_2026-11-08.zip`, and CI result archive records.

The exact Library record IDs and metadata are in the Project Library listing and the Stage 1 source inventory record. This repository register does not claim byte-level verification of those records.

### 4.1 Binding Stage 0 limitations

The Stage 0 Negative Attestation applies specifically to these three records:
- `PROGRESS_LOG(1).md`
- `STAGE_2_CLOSURE_DECLARATION.md`
- `CostOfStaying_STAGE2_LOCAL.zip`

For those exact records:
- Raw-byte materialization was unavailable through the current authorized path.
- No accessible upload audit record was available.
- The ZIP's internal contents were not inspected.
- Byte-level identity, original Library payload hashes, and uploader attribution remain NOT VERIFIED.
- Provisional labels such as `LOCAL-MISPLACED` must not be promoted to final causal conclusions.

## 5. Repository and CI provenance

| Record | Evidence | Scope / limitation |
|---|---|---|
| Repository `live-egypt/CostOfStaying` | Live read-only GitHub inspection | Repository metadata only |
| `main` | SHA `3dd9cfc27aa48b09b3cde802e266a8221c39f0d3` | Snapshot at inspection time |
| `feature/core-lifecycle-resolution` | SHA `2820285021b1a3d7ae9acd953b3bb65677c33512` | Verified by branch self-comparison |
| Branch comparison | 441 commits ahead, 0 behind `main` | Comparison snapshot; not a merge |
| PR #6 | Search returned title/body describing lifecycle/Core consolidation and Draft review | Search wrapper did not populate state field in this read |
| PR #5 | Search returned title/body describing static Unity-source audit | Body states Unity compilation/NUnit were not run and FishNet compatibility not verified |
| Core CI run `42383513646` | Reported in Project Library inventory; commit `2820285021b1a3d7ae9acd953b3bb65677c33512`; artifact `13578582284`; 187/187 | Historical/run-specific evidence; not rerun in this documentation task |
| Core CI run `41333575156` | Repository status register cites source commit `8a193190fb5f2e10c05773fb0789171011bfd2eb`; 181/181; artifact `13092964341` | Distinct run/commit; not interchangeable with 187/187 |

## 6. Provenance findings and open items

| ID | Finding | Status | Required evidence |
|---|---|---|---|
| PROV-001 | Project chat index is not exposed by current Files surface | OPEN | Complete authorized thread list/export |
| PROV-002 | Direct contents and thread IDs of the owner-identified single underlying conversation are not available | PARTIAL — OWNER-ATTESTED RELATION; ORIGINAL CONTENT / IDS SOURCE PENDING | Full original thread/export segments and direct thread IDs |
| PROV-003 | `محادثة 2.html` mapping to the owner-identified underlying conversation / Project chat labels `1` and `2` | OWNER-ATTESTED RELATION — TECHNICAL MATCH OPEN | Direct thread-to-export ID or authenticated source linkage |
| PROV-004 | Four same-name TXT attachments | VERIFIED DISTINCT RAW FILES | Preserve file IDs and hashes |
| PROV-005 | Stage 0 Library raw-byte and uploader evidence limitations | OPEN — BINDING LIMITATION | Authorized raw export/audit event if available |
| PROV-006 | Stage 2 Library record/content contradictions | OPEN — CAUSAL ORIGIN UNRESOLVED | Authenticated source/audit evidence; no attribution by inference |
| PROV-007 | Embedded external citations and source URLs not individually inventoried | NOT STARTED | Extraction and source mapping |
| PROV-008 | Project Library-only source documents have not all been reconciled to repository files | PARTIAL — COMPLETE TREE NAME-ONLY CROSSWALK (5 matches) | Content-identity mapping; basename matches do not establish byte/text equality |
| PROV-009 | Conflicting descriptions of the original platform/source for the owner-identified underlying conversation | OPEN — OWNER ATTESTATION RECORDED; ORIGIN CONFLICT UNRESOLVED | Direct original thread/export and authenticated platform-origin evidence; inspect the cited attachment if recovered |
| PROV-010 | Completeness of `محادثة 2.html` relative to the owner-identified underlying conversation | OPEN — OWNER WARNED THAT PARTS MAY EXIST ELSEWHERE | Complete original thread or independently matched sequence/segment inventory |

## 7. State vocabulary

- `VERIFIED`: direct evidence, bounded to the inspected object/version/run.
- `IMPLEMENTED`: the file or code exists; correctness is not implied.
- `RECORDED`: a value exists in a record; source validation may remain pending.
- `USER-ATTESTED`: stated by the user; not independently authenticated.
- `TEXT/METADATA ONLY`: extracted text and metadata, not raw-byte proof.
- `SOURCE PENDING`: required source content or identity is missing.
- `UNDER ATTESTATION`: governed by Stage 0 Negative Attestation.
- `DUPLICATE-SUSPECTED`: extracted text may match; byte identity is unproven.

## 8. Current disposition

This register is an implemented repository document with partial coverage. It does not close Stage 1, Stage 2, or Phase 2. It does not authorize changes to ADRs, Core, Net, Unity, Tests, or the Project Library.

## 9. Repository binary source package provenance

| ID | Object | Origin / exact identity | Status | Missing proof / limitation |
|---|---|---|---|---|
| REPO-BIN-UNITY-001 | Unity source ZIP | Path incoming/unity-source-package/CostOfStaying_Unity_Source_Package.zip; branch review/unity-source-package-2026-11-01; PR #5; Git blob SHA 319a584046d3ece7ce74242233015738758ec210 | RAW REPOSITORY BLOB FETCHED; GIT BLOB SHA-1 MATCHED; ZIP DIRECTORY REVIEWED | Branch-only; not on main. Archive is Assets/source package, not a full Unity project. |
| REPO-BIN-UNITY-001-HASH | SHA-256 identity | Computed 5f92947c8b23d1ee15d7c744fb62ef0cf7af361bfdda277fcdaeff61dab51ac4; accompanying branch manifest declares 351190c6789b0a146be0978606c831f48b951509e7f353bb3b469374eb9b15d9 | OPEN — CHECKSUM MISMATCH | Retain both; determine which archive version the manifest intended to identify. |
| REPO-BIN-UNITY-001-DELIVERY | PR #5 delivery statement | PR changed-file list includes the ZIP; PR body and README/manifest text state it is not committed | OPEN — DESCRIPTION/CONTENT CONFLICT | Exact historical uploader/operation event is not established. Do not infer attribution. |

The ZIP central directory lists 81 C# files, 4 assembly definitions, 2 Markdown files and 7 directory entries. This is archive inventory, not proof of compilation or runtime behavior. The full reconciliation and source-level spot review are in [UNITY_SOURCE_PACKAGE_RECONCILIATION_2026-11-10.md](../audit/UNITY_SOURCE_PACKAGE_RECONCILIATION_2026-11-10.md).

No Library payload SHA-256 or archive claim is inferred from this repository object. Repository blob identity and Project Library provenance remain separate evidence classes.


## 10. Additional mounted attachment provenance — 2026-11-10

Five additional attachment objects (IDs 16–20 relative to the prior 15-object snapshot) are identified by stable Project file IDs and mounted-byte SHA-256 in [ATTACHMENT_SNAPSHOT_DELTA_2026-11-10.md](../audit/ATTACHMENT_SNAPSHOT_DELTA_2026-11-10.md).

The hashes prove the identity of the bytes mounted at the recorded local paths for this session only. They do not prove original uploader identity, Project Library byte equality, source conversation identity, or complete inventory coverage. The five-object delta is bounded and reviewed; Project Library and original chat enumeration remain open.


## 3.1 Additional surfaced attachment object — 2026-11-10

| Project file ID | Display name | Bytes | Lines | SHA-256 | Source classification |
|---|---|---:|---:|---|---|
| `file_0000000022c481f4b8d45ee51c9bf603` | `قسم · قسم · مراجعة وجرد الملفات.txt` | 5314 | 152 | `23ebb91ab1e85b562e41aaca7884ccaf98f306aab0d20ce03340ef40794e817d` | Derived inventory/progress report; not an original decision transcript. |

The hash authenticates only the mounted runtime bytes. The current bounded context count is 21 surfaced attachment objects and 14 mounted text paths. See [Attachment Snapshot Delta 02](../audit/ATTACHMENT_SNAPSHOT_DELTA_2026-11-10-02.md) and [Materialization Manifest 02](../audit/SESSION_MATERIALIZATION_MANIFEST_2026-11-10-02.md). Project Library raw-byte access and complete conversation enumeration remain unverified.
