# Source Inventory — Cost of Staying

> **Governance status axes:** Decision Status and Evidence Maturity are independent. This document's existence does not approve its claims or prove implementation. See [Project Governance Baseline](PROJECT_GOVERNANCE_BASELINE.md).

**Revision date:** 2026-11-09  
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
| Project conversation `1` | USER-ATTESTED | Existence of Project chat labelled `1` is attested; origin label is disputed by surfaced records | Full thread, thread ID, transcript, and authoritative platform-origin evidence unavailable |
| Project conversation `2` | USER-ATTESTED | Existence of Project chat labelled `2` is attested; origin label is disputed by surfaced records | Full thread, thread ID, transcript, and exact mapping to `محادثة 2.html` unavailable |
| `استكمال المحادثة` | Context surfaced | Summary/context available | Full thread object not available |
| `قسم · استكمال المحادثة` | Context surfaced | Summary/context available | Full thread object not available |
| `2 استكمال المحادثة` | Context surfaced | Summary/context available | Not equivalent to the similarly named TXT report attachments |
| `رفع الملفات على دفعات` | Context surfaced | Related intake reports available | Full thread object not available |
| `مراجعة شاملة للمشروع` | Context surfaced | Summary/context available | Full thread object not available |
| Current active thread | CURRENT CONTEXT | Current messages available | No separate full export generated |

**Origin-attribution conflict — OPEN:** Earlier provenance notes record chats `1` and `2` as formerly associated with DeepSeek. However, Project attachment `file_00000000343482109ecb2381153c4550` explicitly states: “المقصود هو محادثات ChatGPT داخل المشروع، وليس محادثات تطبيق DeepSeek.” The available evidence supports that `1` and `2` are Project conversation labels, but does not establish their original platform origin. Preserve both statements as conflicting source claims; do not label the chats DeepSeek-origin or DeepSeek-app transcripts until the exact source statement/thread metadata is recovered.

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
| DeepSeek-related project chats `1` and `2` | USER-ATTESTED | Direct content and authenticated platform origin not recovered |
| Gemini-related discussion | CONTEXT-SURFACED | No Gemini-native export in the current inventory |
| Embedded external references | NOT INDIVIDUALLY INVENTORIED | URLs/citations inside documents require later extraction |

## 8. Open source-recovery items

| ID | Item | Status |
|---|---|---|
| SRC-01 | Complete Project conversation/thread enumeration | OPEN — interface does not expose a complete thread index |
| SRC-02 | Direct content and thread IDs for chats `1` and `2` | SOURCE PENDING |
| SRC-03 | Exact mapping of `محادثة 2.html` to project chat `2` | UNVERIFIED |
| SRC-04 | Full provenance of Gemini-related discussion | SOURCE PENDING |
| SRC-05 | Individual extraction of embedded web/external references | NOT STARTED |
| SRC-06 | Reconcile prior Library inventory snapshots against current listing | OPEN |
| SRC-07 | Recheck Library record count and metadata after future uploads | OPEN |

## 9. Stage status boundary

This inventory is a current source index, not a Stage closure declaration.

- Stage 0: ACCEPTED WITH DOCUMENTED CONSTRAINT (per recorded owner decision).
- Stage 1: PARTIAL — file surfaces inventoried; complete conversation enumeration not available.
- Stage 2: PARTIAL — closure gate remains BLOCKED in the current recorded status.
- Stage 3: BLOCKED in the current recorded status.
- Stage 4–5: NOT STARTED in the current recorded status.
- Phase 1–3 remain separate from Stage 0–5 and must not be conflated.

No claim of full project conversation coverage, full source recovery, or final project readiness is made.
