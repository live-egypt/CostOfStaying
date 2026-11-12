# Source Inventory — Cost of Staying

> **Governance status axes:** Decision Status and Evidence Maturity are independent. This document's existence does not approve its claims or prove implementation. See [Project Governance Baseline](PROJECT_GOVERNANCE_BASELINE.md).

**Revision date:** 2026-11-12 — DOC-B-03 bounded HTML metadata inspection and owner-attested identity relation; prior tables retained as dated snapshots  
**Record State:** INITIAL RECONCILIATION SNAPSHOT — DOCUMENTATION ONLY; CONTENT AND SOURCE COMPLETENESS REMAIN PARTIAL
**Scope:** Sources visible through the current Project Files surface, Project Library listing, supplied attachments, and read-only GitHub inspection.  
**Stage:** Stage 1 — Source & Provenance Recovery  
**Control:** This inventory does not certify complete enumeration of all Project conversations or all external sources.

## 1. Scope and evidence rules

This inventory separates:
- Project conversation references from exported conversation files.
- Raw Project-surface attachments from Library records.
- Original source material from reports derived from or summarizing sources.
- Repository files from Project Library-only documents.
- Repository metadata from implementation and verification evidence.

The current Files surface exposes files and Library records, not a complete index of all ChatGPT Project conversation threads. Consequently, this document does not claim that every conversation in Project "لعبة" has been enumerated or reviewed.

For Library records, Stage 0 Negative Attestation remains binding: use accessible metadata and extracted text only where available. Do not claim raw-byte hashes, byte equality, archive contents, or uploader attribution for Library payloads unless an authorized raw-byte source becomes available.

## 2. Project conversation references

| Reference | Evidence class | Current coverage | Limitation |
|---|---|---|---|
| Project conversation `1` | OWNER-ATTESTED — IDENTITY RELATION ONLY | Owner states Project chat labels `1` and `2`, DeepSeek references, and `محادثة 2.html` refer to one underlying conversation | Direct thread object/ID, original source-platform evidence, and completeness remain unavailable |
| Project conversation `2` | OWNER-ATTESTED — EXPORT RELATION ONLY | Owner states `محادثة 2.html` is associated with the same underlying conversation as the DeepSeek references and Project chat labels `1`/`2`; the HTML artifact self-identifies as a ChatGPT Shared Chat | Direct thread object/ID and technical mapping remain unverified; owner warns that some parts may exist elsewhere and may be absent from this export |
| `استكمال المحادثة` | Context surfaced | Summary/context available | Full thread object not available |
| `قسم · استكمال المحادثة` | Context surfaced | Summary/context available | Full thread object not available |
| `2 استكمال المحادثة` | Context surfaced | Summary/context available | Not equivalent to the similarly named TXT report attachments |
| `رفع الملفات على دفعات` | Context surfaced | Related intake reports available | Full thread object not available |
| `مراجعة شاملة للمشروع` | Context surfaced | Summary/context available | Full thread object not available |
| Current active thread | CURRENT CONTEXT | Current messages available | No separate full export generated |

**Origin-attribution conflict — OPEN:** Earlier provenance notes associate chats `1` and `2` with DeepSeek. Owner attestation (2026-11-12): the DeepSeek references, Project conversation labels `1` and `2`, and `محادثة 2.html` refer to the same underlying conversation. The owner confirms that the HTML is associated with that same DeepSeek-referenced conversation, while warning that some parts may exist elsewhere and may not be present in this export. This is an attested relationship, not a technically verified source/thread match or completeness finding. The accessible HTML artifact self-identifies as `ChatGPT Shared Chat` and links to `chatgpt.com/share`; that identifies the export/page format, not necessarily the origin of the underlying conversation. A prior report attributes a ChatGPT-Project-versus-DeepSeek-app statement to attachment `file_00000000343482109ecb2381153c4550`, but that attachment is not present in the current conversation or Library file surfaces and was not directly re-inspected in this run. Preserve the owner attestation, the HTML metadata, and the prior reported claim without selecting an origin. Do not infer missing conversation parts.

**Bounded HTML inspection — 2026-11-12 (text/metadata only):** The current conversation Files surface and Library path `/لعبة/محادثة 2.html` expose the same file ID, `file_000000007f6482109c42c450bf73d0b4`; Library metadata reports 26,698,846 bytes. The extracted HTML contains 79,302 lines and identifies itself as `ChatGPT Shared Chat`, with source URL `https://chatgpt.com/share/6aef6082-3c6c-83ea-95c9-ee863196fadc` and displayed export timestamp `٧‏/١١‏/٢٠٢٦، ٧:٣٣:٥٩ م`. Text inspection counted 226 user and 223 assistant turn elements (449 total); the last is `Assistant turn 449`, the file ends with `</html>`, and no literal `DeepSeek` occurrence was found in the extracted HTML. These observations establish the artifact's visible format and contents only; they do not authenticate the underlying source platform, establish the mapping to a Project thread ID, or prove completeness. No Library raw-byte hash or byte-level verification was performed.

## 3. Current Project conversation attachments — raw-byte inventory

Fifteen distinct Project-surface file records are available in the current conversation. They were materialized individually through the authorized Project-file raw-file path into `/mnt/data/cos-source-inputs/`; SHA-256 values below were calculated from those materialized bytes. Same-basename files remain distinct records.

| # | Display name | Project file ID | Bytes | SHA-256 | Classification |
|---:|---|---|---:|---|---|
| 1 | `2 استكمال المحادثة.txt` | `file_0000000035308210ba764fa41dca76ef` | 8,163 | `5bc108ffd11326d10d83562198d0fdca99fe7a3fe49204952ed020152ecd866d` | Report 02 — Source Recovery & Decision Traceability |
| 2 | `2 استكمال المحادثة.txt` | `file_000000008adc821088a91de351f5ada2` | 12,518 | `62daf362f021b2ecffb8aeec7dd6c576c50c325a0afb022d4ffd27b4eb2534aa` | Broad multi-source audit / first-round findings |
| 3 | `2 استكمال المحادثة.txt` | `file_00000000ab5482109ff976a094c0b9fe` | 9,828 | `ab52d5c2a632b1e209b1d3c3cc0bd3070d63c2605bcc93189671890d916374e2` | Report 03 — Source Recovery & Consistency Audit |
| 4 | `2 استكمال المحادثة.txt` | `file_0000000072e08210b2d34211cd183832` | 14,149 | `cf8f199c83c5025408eeaa31731e868bb053bf75fcd4da4b1d1f17445bd0a6ec` | Report 04 — Repository Forensic Audit |
| 5 | `رفع الملفات على دفعات.txt` | `file_00000000e5b88210973c72a025e1cdb8` | 4,719 | `02688c4ab66f7cb6a32896159854b62028fcab019072967c09fc334c2e438333` | Batch intake log |
| 6 | `رفع الملفات على دفعات.txt` | `file_0000000075a882108078bf80ee654734` | 3,585 | `e4351e191907a038a4340118a888e19a962eb3e173a21152ab6f40d6ea5a58df` | Reconciliation direction log |
| 7 | `قسم · قسم · رفع الملفات على دفعات.txt` | `file_000000005bb4821095c73b6c4a99e6cf` | 4,972 | `0a0e459edd8717aa2e3fe26d630e3f130f6c88d46ae05b651350312c6de9f698` | Security Audit reconciliation material |
| 8 | `قسم · قسم · قسم · استكمال المحادثة.txt` | `file_00000000f4588210afbd7b7c9b873a51` | 7,437 | `fed4978a62d4d1a54c6f1848c5b36d4581be5dfb79181a1cddf50bdf84ff289f` | Stage 1 source recovery report |
| 9 | `قسم · قسم · رفع الملفات على دفعات.txt` | `file_000000004f408210a23cc447faaf4aa8` | 1,979 | `d1a9e4c94d8f10a5d60c0e333cf5f0af55bc291c645085d419d2e9fa6fe86f4e` | Package structure reconciliation intake |
| 10 | `قسم · قسم · رفع الملفات على دفعات.txt` | `file_00000000343482109ecb2381153c4550` | 11,881 | `4eb0b34fa66c06b4fbcb9b5afdc17a132298bbf3a32882e15c14ebf77da5a618` | Document mapping and source reconciliation |
| 11 | `5.txt` | `file_00000000be5081f5b0278b728bd9a43e` | 17,454 | `5cf496c1183f36d23ef46d22b3afb7438728df70175bd17f0f46014c130dce60` | Security Audit reconciliation — initial |
| 12 | `00.txt` | `file_00000000333c8210889b5112bd178f0c` | 12,519 | `b7d134696c9229b988decc7ba204bc9e7b723638eb0bbd117aae401f1c02ab9f` | Reconciliation / source audit material |
| 13 | `00.txt` | `file_000000004f3882108d820c554b6290b8` | 12,581 | `6e403e3e05fa6eb9d0b89041139931f0a67a604435a75ad0d7c9d0d1f5125971` | Traceability reconciliation report |
| 14 | `قسم · قسم · استكمال المحادثة.txt` | `file_00000000b4b48210ae17137a2377fb16` | 9,625 | `0177fcd7e59f79db23d18ff9b820841618fbc29bd5b071a8f43f6546c35d6f96` | Report 05 — Source Inventory & Provenance Reconciliation |
| 15 | `01.txt` | `file_00000000f2748210a36cd79959759fba` | 9,565 | `f6a87965829fcd44f787b9e984340d659d1748258e934672de7618f09b44c26b` | Continuation plan and stage sequencing |

**Attachment set total:** 15 records; 140,975 bytes. These hashes authenticate the 15 materialized attachment byte streams only. They do not authenticate Library records or source claims quoted inside the attachments.

## 4. Project Library inventory — metadata snapshot

The recursive Library listing returned **40 file records + 1 folder** under `/لعبة`. The table records metadata only; it does not assert raw-byte availability or payload verification.

| # | Library record | File ID | Library path |
|---:|---|---|---|
| 1 | `CONSISTENCY_MATRIX.md` | `file_0000000062c48210958b0129524e29f3` | `/لعبة/CONSISTENCY_MATRIX.md` |
| 2 | `core-test-results-42383513646.zip` | `file_00000000e7648210b2acf5a323157a01` | `/لعبة/core-test-results-42383513646.zip` |
| 3 | `COST_OF_STAYING_CONTEXT_AUDIT_RECORD_2026-11-08.md` | `file_0000000012488210afe0b3cf594ef292` | `/لعبة/COST_OF_STAYING_CONTEXT_AUDIT_RECORD_2026-11-08.md` |
| 4 | `CostOfStaying_Docs_Diff_2026-11-08.zip` | `file_00000000111c821099d55006be62ba38` | `/لعبة/CostOfStaying_Docs_Diff_2026-11-08.zip` |
| 5 | `CostOfStaying_STAGE1_SourceInventory_Package.zip` | `file_000000005d708210800a33fa21616aef` | `/لعبة/CostOfStaying_STAGE1_SourceInventory_Package.zip` |
| 6 | `CostOfStaying_STAGE2_LOCAL.zip` | `file_00000000811c82109fc643ad9659e393` | `/لعبة/CostOfStaying_STAGE2_LOCAL.zip` |
| 7 | `CostOfStaying_Stage2_Review_Package_2026-11-08.zip` | `file_00000000aaf881f4aa5977536fe649e0` | `/لعبة/CostOfStaying_Stage2_Review_Package_2026-11-08.zip` |
| 8 | `DECISION_AND_CONFLICT_LOG.md` | `file_00000000c83c8210a0932c4a7073271b` | `/لعبة/DECISION_AND_CONFLICT_LOG.md` |
| 9 | `DIFF.patch` | `file_000000005ff482108a77c87e40663035` | `/لعبة/DIFF.patch` |
| 10 | `DIFF_SUMMARY.md` | `file_00000000590c82108df1b08a9d162b1a` | `/لعبة/DIFF_SUMMARY.md` |
| 11 | `FILE_MANIFEST.json` | `file_000000007eb0821083a0fea94996c97d` | `/لعبة/FILE_MANIFEST.json` |
| 12 | `FINAL_REVIEW_REPORT.md` | `file_0000000077a481f48103e84d9cadc89c` | `/لعبة/FINAL_REVIEW_REPORT.md` |
| 13 | `FULL_DOCUMENTS_REVIEW.md` | `file_00000000b7048210be3cf31866d2245a` | `/لعبة/FULL_DOCUMENTS_REVIEW.md` |
| 14 | `NEGATIVE_ATTESTATION_STAGE0.md` | `file_00000000e91c8210ba9707c56b71cb74` | `/لعبة/NEGATIVE_ATTESTATION_STAGE0.md` |
| 15 | `PHASED_WORK_PLAN.md` | `file_00000000721081f4ac5bbff0c7c5cf04` | `/لعبة/PHASED_WORK_PLAN.md` |
| 16 | `PROGRESS_LOG(1).md` | `file_0000000076dc8210bbb913f7c6c05111` | `/لعبة/PROGRESS_LOG(1).md` |
| 17 | `PROGRESS_LOG.md` | `file_0000000008c081f498deb9428fcf117f` | `/لعبة/PROGRESS_LOG.md` |
| 18 | `PROVENANCE_RECONCILIATION_NOTE_STAGE0.md` | `file_000000006b3c8210b38f0a97d1865b22` | `/لعبة/PROVENANCE_RECONCILIATION_NOTE_STAGE0.md` |
| 19 | `PROVENANCE_REGISTER.md` | `file_00000000b5a482108b60d21b8d4ba519` | `/لعبة/PROVENANCE_REGISTER.md` |
| 20 | `README.diff` | `file_000000001ce48210b5906243641e0e6a` | `/لعبة/README.diff` |
| 21 | `REVIEW_INDEX.md` | `file_000000005ab481f4a27b16321bea5a89` | `/لعبة/REVIEW_INDEX.md` |
| 22 | `SHA256SUMS-LOCAL.txt` | `file_000000001bf881f4a33b1d37a42ef19f` | `/لعبة/SHA256SUMS-LOCAL.txt` |
| 23 | `SHA256SUMS-PROPOSED.txt` | `file_00000000a6e8821099a8e19069a1170e` | `/لعبة/SHA256SUMS-PROPOSED.txt` |
| 24 | `SHA256SUMS-STAGE1-LOCAL.txt` | `file_00000000a17c8210b3b11edcb3723370` | `/لعبة/SHA256SUMS-STAGE1-LOCAL.txt` |
| 25 | `SHA256SUMS-STAGE1.txt` | `file_00000000776481f491d5744d840009e8` | `/لعبة/SHA256SUMS-STAGE1.txt` |
| 26 | `SHA256SUMS-STAGE2.txt` | `file_0000000021f881f4ba012a8903076c1b` | `/لعبة/SHA256SUMS-STAGE2.txt` |
| 27 | `SHA256SUMS.txt` | `file_00000000c45c8210a3f18c11be4f54c1` | `/لعبة/SHA256SUMS.txt` |
| 28 | `SOURCE_INVENTORY.md` | `file_000000002c9c82109fac4207c8ece55e` | `/لعبة/SOURCE_INVENTORY.md` |
| 29 | `SOURCE_TRACEABILITY_REGISTER.md` | `file_00000000b7cc82108d6e616fd70b238e` | `/لعبة/SOURCE_TRACEABILITY_REGISTER.md` |
| 30 | `STAGE1_PROGRESS_LOG.md` | `file_000000002b508210bc6f71b0fa97c103` | `/لعبة/STAGE1_PROGRESS_LOG.md` |
| 31 | `STAGE2_EVIDENCE_RECONCILIATION_REPORT.md` | `file_0000000066f88210bcb1c9fc76803d7b` | `/لعبة/STAGE2_EVIDENCE_RECONCILIATION_REPORT.md` |
| 32 | `STAGE_1.md` | `file_0000000084808210a8a9cb936d985dfa` | `/لعبة/STAGE_1.md` |
| 33 | `STAGE_2.md` | `file_00000000bedc81f4a02bb9a41db31733` | `/لعبة/STAGE_2.md` |
| 34 | `STAGE_2_CLOSURE_DECLARATION.md` | `file_00000000d97482108f0ff3d25b0256a0` | `/لعبة/STAGE_2_CLOSURE_DECLARATION.md` |
| 35 | `STAGE_2_REVIEW.md` | `file_0000000008d881f4a6aa5bba0a28b198` | `/لعبة/STAGE_2_REVIEW.md` |
| 36 | `STAGE_2_REVIEW_FINDINGS.md` | `file_00000000bcd481f49bd41e9ee833195c` | `/لعبة/STAGE_2_REVIEW_FINDINGS.md` |
| 37 | `TBD_REGISTER.md` | `file_0000000087cc821087da41b9341877d6` | `/لعبة/TBD_REGISTER.md` |
| 38 | `VALIDATION_REPORT.md` | `file_00000000e9d48210880d7edbe597a49b` | `/لعبة/VALIDATION_REPORT.md` |
| 39 | `ما هو CodeQL؟_2293753019127521858.md` | `file_00000000279c81f4a6fb53aa972c2351` | `/لعبة/ما هو CodeQL؟_2293753019127521858.md` |
| 40 | `محادثة 2.html` | `file_000000007f6482109c42c450bf73d0b4` | `/لعبة/محادثة 2.html` |

No Library payload is raw-byte verified by this table.

## 4.1 Fresh Project Library and repository-tree crosswalk — 2026-11-11

**Library listing observed at:** `2026-11-11T08:55:12.632764Z`  
**Library surface:** recursive listing for `/لعبة`  
**Result:** 46 items: 45 file records with 45 unique file IDs, plus one folder (`/لعبة`); no warnings and no continuation cursor.

Compared by stable `file_id` with the 2026-11-09 table in Section 4:
- All 40 historical file IDs remain present (40/40); no historical record was absent.
- Five additional records are present:

| Additional Library record | File ID | Size (bytes) | Modified (UTC) | Evidence classification |
|---|---|---:|---|---|
| `DUPLICATE_ANALYSIS.md` | `file_00000000f51c8210a26766f67b198342` | 5,437 | 2026-11-09T20:23:25.397473Z | Derivative comparison of eight mounted text objects; not an original source. |
| `MISSING_SOURCES_REGISTER.md` | `file_000000005720821081b9987faf9b1ddb` | 4,394 | 2026-11-09T20:23:28.194165Z | Derivative source-gap register; not an original decision source. |
| `PROVENANCE_REGISTER(1).md` | `file_00000000d75c8210a52cc26371989127` | 4,932 | 2026-11-09T20:23:22.215973Z | Draft provenance assessment of eight mounted files; not primary-source provenance. |
| `SOURCE_INVENTORY(1).md` | `file_00000000da888210b05244cf4300aa66` | 7,245 | 2026-11-09T20:23:18.666570Z | Draft inventory of eight mounted files; not a complete Project inventory. |
| `STAGE_1_WORKING_OUTPUTS.zip` | `file_00000000d5808210b7fb7af30b2e9a67` | 8,717 | 2026-11-09T20:23:30.828866Z | Library metadata only; archive not opened or inspected. |

The four additional text records above were read through extracted text and classified as derivative/draft records. No Library raw-byte hash was calculated. No contents are asserted for the ZIP.

A separate active-conversation Files listing at `2026-11-11T08:55:13.004803Z` returned one attachment: `محادثة 2.html`, file ID `file_000000007f6482109c42c450bf73d0b4`. The same ID appears in the 45-record Library listing. This is an ID-level cross-surface match for one file object only; it does not prove mapping to Project chat `2`, platform origin, or conversation completeness.

The 21 Project attachment IDs in the dated 2026-11-10 snapshots (15 + 5 + 1) have no ID overlap with the current 45 Library file IDs. Preserve these as distinct source surfaces; do not merge records by basename.

**Repository tree at pre-write head:** branch `docs/project-reconciliation-baseline-2026-11-10`, commit `8e7bc1ec0a0306655781755eb281ebb22d1b0ae6`, tree SHA `28aabb1420b5e794566d0001249243b5a3032fca`; 99 tree entries / 87 blobs; `truncated=false`. Comparing all 45 Library basenames against all 87 repository blob paths produces five exact basename matches:
- `CONSISTENCY_MATRIX.md` → `docs/governance/CONSISTENCY_MATRIX.md`
- `PROVENANCE_REGISTER.md` → `docs/governance/PROVENANCE_REGISTER.md`
- `SOURCE_INVENTORY.md` → `docs/governance/SOURCE_INVENTORY.md`
- `SOURCE_TRACEABILITY_REGISTER.md` → `docs/governance/SOURCE_TRACEABILITY_REGISTER.md`
- `TBD_REGISTER.md` → `docs/governance/TBD_REGISTER.md`

The remaining 40 Library records have no exact basename match in that repository tree. PR #11's changed-file list contains 39 paths, all under `docs/`. This is a complete path/basename inventory crosswalk for the exact pre-write tree, not content/byte equality, uploader attribution, or proof that similarly named files are identical.

**DOC-B-02 disposition:** the bounded Library/current-conversation/repository metadata crosswalk is complete for this snapshot. Library raw-byte identity, content equality, and original source lineage remain unverified.

## 5. GitHub repository snapshot — 2026-11-09

Repository: `live-egypt/CostOfStaying`  
Default branch: `main`  
Recorded `main` SHA from the prior source inventory snapshot: `3dd9cfc27aa48b09b3cde802e266a8221c39f0d3` (not independently revalidated in this checkpoint).  
Development branch: `feature/core-lifecycle-resolution`  
Observed development branch HEAD: `2820285021b1a3d7ae9acd953b3bb65677c33512`  
Comparison: development branch is 441 commits ahead of `main`, 0 behind at the time of the comparison.

The following were inspected:
- `docs/README.md`
- `docs/PROJECT_STATUS_AND_EXECUTION_REGISTER.md`
- `docs/governance/REQUIREMENTS_TRACEABILITY.md`
- `docs/governance/adr/ADR-015-COMMAND-IDENTITY-AND-IDEMPOTENCY.md`
- PR search records for #5 and #6.

PR #6 search metadata returned its title/body and identifies it as a Draft review of lifecycle/Core consolidation. The search wrapper did not return a populated state field in this inspection; do not infer PR state solely from that field. The development branch remains unmerged relative to `main` as shown by the 441-commit comparison.

Two Stage 1 documentation PRs were found open during final review: PR #8 (`docs/stage1-source-provenance-recovery` → `main`) and PR #9 (`docs/source-provenance-reconciliation-2026-11-09` → `feature/core-lifecycle-resolution`). Their source/provenance package scopes overlap but their paths and base branches differ. See `docs/governance/DOCUMENT_CONTROL_REGISTER.md`, DOC-PACKAGE-01. Neither was merged or closed.

## 6. Existing repository documentation found

The repository already contains:
- `docs/README.md`
- `docs/PROJECT_STATUS_AND_EXECUTION_REGISTER.md`
- `docs/governance/REQUIREMENTS_TRACEABILITY.md`
- `docs/governance/DECISIONS_LOG.md`
- ADR-001 through ADR-015, including ADR-014 and ADR-015 as PROPOSED where stated.
- `docs/architecture/NETWORK_ARCHITECTURE.md`
- Specialized security documents: `SECURITY_ARCHITECTURE.md`, `TRUST_BOUNDARIES.md`, `THREAT_MODEL.md`, `SECRETS_MANAGEMENT.md`, `DATA_PRIVACY.md`, `INCIDENT_RESPONSE.md`.
- Existing Core CI and audit reports under `docs/audit/`.

Existence of these documents does not establish that their designs are implemented or verified. The Project Library-only records listed above are not automatically repository files.

## 7. External and other-model sources

| Source | Status | Limitation |
|---|---|---|
| DeepSeek-related project chats `1` and `2` | OWNER-ATTESTED SAME UNDERLYING CONVERSATION AS `محادثة 2.html` | Original thread content/IDs and authenticated platform origin not recovered; possible additional parts may exist elsewhere |
| Gemini-related discussion | CONTEXT-SURFACED | No Gemini-native export in the current inventory |
| Embedded external references | NOT INDIVIDUALLY INVENTORIED | URLs/citations inside documents require later extraction |

## 8. Open source-recovery items

| ID | Item | Status |
|---|---|---|
| SRC-01 | Complete Project conversation/thread enumeration | OPEN — interface does not expose a complete thread index |
| SRC-02 | Direct content and thread IDs for chats `1` and `2` | PARTIAL — OWNER-ATTESTED SAME UNDERLYING CONVERSATION; ORIGINAL THREADS/IDS SOURCE PENDING |
| SRC-03 | Exact mapping of `محادثة 2.html` to project chat `2` | OWNER-ATTESTED RELATION; TECHNICAL THREAD MATCH UNVERIFIED |
| SRC-04 | Full provenance of Gemini-related discussion | SOURCE PENDING |
| SRC-05 | Individual extraction of embedded web/external references | NOT STARTED |
| SRC-06 | Reconcile prior Library inventory snapshots against current listing | OPEN |
| SRC-07 | Recheck Library record count and metadata after future uploads | OPEN |
| SRC-08 | Completeness of `محادثة 2.html` relative to the owner-identified underlying conversation | OPEN — OWNER WARNED THAT PARTS MAY EXIST ELSEWHERE; EXPORT COMPLETENESS NOT ESTABLISHED |

## 9. Stage status boundary

This inventory is a current source index, not a Stage closure declaration.

- Stage 0: ACCEPTED WITH DOCUMENTED CONSTRAINT (per recorded owner decision).
- Stage 1: PARTIAL — file surfaces inventoried; complete conversation enumeration not available.
- Stage 2: PARTIAL — closure gate remains BLOCKED in the current recorded status.
- Stage 3: BLOCKED in the current recorded status.
- Stage 4–5: NOT STARTED in the current recorded status.
- Phase 1–3 remain separate from Stage 0–5 and must not be conflated.

No claim of full project conversation coverage, full source recovery, or final project readiness is made.

## 10. Current-session materialization crosswalk

A separate runtime snapshot on 2026-11-10 contains ten materialized text-file paths. Their exact Project attachment IDs, byte sizes, line counts, SHA-256 values, and observed report roles are recorded in [UPLOADED_SOURCE_REVIEW_MATRIX.md](../audit/UPLOADED_SOURCE_REVIEW_MATRIX.md), with the raw measurement manifest in [SESSION_MATERIALIZATION_MANIFEST_2026-11-10.md](../audit/SESSION_MATERIALIZATION_MANIFEST_2026-11-10.md).

This ten-path runtime snapshot is not a replacement for the historical 15-attachment Project Files snapshot recorded above. The exact crosswalk for all 15 historical records is incomplete; two additional current-session records (03.txt and Governance Baseline Setup.txt) were not present in the earlier 15-record table. Do not infer that unmounted historical attachments are deleted or that the current ten paths are a complete Project surface.

## 11. Unity source package repository object — PR #5 branch

A binary source package was discovered and retrieved from the repository review branch:

| Field | Value | Evidence boundary |
|---|---|---|
| Object ID | REPO-BIN-UNITY-001 | Repository binary object, not a Project attachment/Library record |
| Path | incoming/unity-source-package/CostOfStaying_Unity_Source_Package.zip | Exact path on review branch |
| Branch / PR | review/unity-source-package-2026-11-01 / PR #5 | Open Draft, not merged |
| Git blob SHA | 319a584046d3ece7ce74242233015738758ec210 | Independently recomputed Git blob SHA-1 matched |
| Decoded bytes | 56,217 | Base64 decoded from the fetched object |
| Archive contents | 94 ZIP entries: 81 C#, 4 asmdef, 2 Markdown, 7 directory entries | Central-directory inventory; no source compilation |
| Computed SHA-256 | 5f92947c8b23d1ee15d7c744fb62ef0cf7af361bfdda277fcdaeff61dab51ac4 | Computed over exact decoded object bytes |
| Declared SHA-256 | 351190c6789b0a146be0978606c831f48b951509e7f353bb3b469374eb9b15d9 | Value in branch SHA256 manifest; mismatch |
| Completeness | Assets/source package only | No Packages manifest/lock, ProjectSettings/ProjectVersion, scenes or .meta files in 94-entry inventory |

The ZIP's presence in PR #5's changed-file list conflicts with the PR body and checksum manifest, both of which state the corrected ZIP is not committed. The archive is present on the review branch but not in the inspected main baseline. See [UNITY_SOURCE_PACKAGE_RECONCILIATION_2026-11-10.md](../audit/UNITY_SOURCE_PACKAGE_RECONCILIATION_2026-11-10.md).

Do not conflate this repository binary object with the 10 currently materialized text attachments or the historical 15-attachment Project surface. No uploader attribution is inferred. No source files were extracted into main or merged.


## 10. Dated attachment-context delta — 2026-11-10

The current project conversation context exposes 20 distinct attachment objects. The earlier 15-object snapshot remains a valid historical snapshot; five additional attachment IDs and mounted-byte SHA-256 values are recorded in [ATTACHMENT_SNAPSHOT_DELTA_2026-11-10.md](../audit/ATTACHMENT_SNAPSHOT_DELTA_2026-11-10.md).

This is not a fresh Project Library listing and does not establish full conversation enumeration. The active runtime materialized 13 unique text-file paths; attachment-object count and local-path count are different dimensions. Preserve prior snapshot dates and counts rather than rewriting them as if they were measured simultaneously.


## 5.1 Additional surfaced attachment snapshot — 2026-11-10

A later bounded project-context snapshot exposes one additional distinct attachment object beyond the 20-object checkpoint: `file_0000000022c481f4b8d45ee51c9bf603`, displayed as `قسم · قسم · مراجعة وجرد الملفات.txt`. The exact mounted bytes are 5314 bytes / 152 lines with SHA-256 `23ebb91ab1e85b562e41aaca7884ccaf98f306aab0d20ce03340ef40794e817d`. See [Attachment Snapshot Delta 02](../audit/ATTACHMENT_SNAPSHOT_DELTA_2026-11-10-02.md) for its classification and limitations.

This updates the bounded surfaced count to 21 attachment objects and the mounted runtime to 14 distinct paths / 110,718 bytes. It does not re-enumerate Project Library or establish complete conversation coverage. The earlier 15-object inventory and five-object delta remain historical snapshots.
