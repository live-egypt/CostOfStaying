# Source Inventory — Cost of Staying

**Snapshot:** 2026-11-09  
**Stage:** Stage 1 — Source & Provenance Recovery  
**Status:** `PARTIAL — EXPOSED FILE SURFACES INVENTORIED; PROJECT CHAT ENUMERATION INCOMPLETE`  
**Scope:** Read-only inventory of current Project attachments, Project Library metadata, and GitHub repository references.

## 1. Completeness boundary

This inventory records the sources exposed by the current Project Files surface and the read-only GitHub connector at the snapshot time. It is **not** a complete index of every ChatGPT Project conversation. The available Files interface exposes file attachments and Library records, but does not expose a complete Project chat-thread list or a direct read operation for every prior thread.

The user has attested that Project conversations labelled `1` and `2` exist. The original platform/origin description is conflicting: earlier notes associate them with DeepSeek, while attachment `file_00000000343482109ecb2381153c4550` explicitly says the relevant conversations are ChatGPT Project conversations, not conversations in the DeepSeek application. Their existence is `USER-ATTESTED`; their full contents, thread IDs, and original platform origin remain unresolved.

## 2. Current Project conversation attachments

A fresh `files.list(surface=conversation)` returned **15 distinct file records** in this snapshot. Each record has a distinct Project file ID. All 15 were materialized through the authorized Project-file raw-file path into a controlled working directory and SHA-256 was calculated from the resulting bytes.

The four records named `2 استكمال المحادثة.txt` are separate report artifacts. Matching basenames do not establish duplicate content.

| # | Display name | Project file ID | Bytes | Lines | SHA-256 | Content identity |
|---:|---|---|---:|---:|---|---|
| 1 | `2 استكمال المحادثة.txt` | `file_0000000035308210ba764fa41dca76ef` | 8,163 | 199 | `5bc108ffd11326d10d83562198d0fdca99fe7a3fe49204952ed020152ecd866d` | Report 02 — Source Recovery & Decision Traceability |
| 2 | `2 استكمال المحادثة.txt` | `file_000000008adc821088a91de351f5ada2` | 12,518 | 235 | `62daf362f021b2ecffb8aeec7dd6c576c50c325a0afb022d4ffd27b4eb2534aa` | Broad multi-source audit / first-round findings |
| 3 | `2 استكمال المحادثة.txt` | `file_00000000ab5482109ff976a094c0b9fe` | 9,828 | 251 | `ab52d5c2a632b1e209b1d3c3cc0bd3070d63c2605bcc93189671890d916374e2` | Report 03 — Source Recovery & Consistency Audit |
| 4 | `2 استكمال المحادثة.txt` | `file_0000000072e08210b2d34211cd183832` | 14,149 | 467 | `cf8f199c83c5025408eeaa31731e868bb053bf75fcd4da4b1d1f17445bd0a6ec` | Report 04 — Repository Forensic Audit |
| 5 | `رفع الملفات على دفعات.txt` | `file_00000000e5b88210973c72a025e1cdb8` | 4,719 | 43 | `02688c4ab66f7cb6a32896159854b62028fcab019072967c09fc334c2e438333` | Batch intake log — Gameplay, Playtesting, Forensic Risk, Repository/Architecture |
| 6 | `رفع الملفات على دفعات.txt` | `file_0000000075a882108078bf80ee654734` | 3,585 | 62 | `e4351e191907a038a4340118a888e19a962eb3e173a21152ab6f40d6ea5a58df` | RECONCILIATION-01…06 intake / accepted direction record |
| 7 | `قسم · قسم · رفع الملفات على دفعات.txt` | `file_000000005bb4821095c73b6c4a99e6cf` | 4,972 | 120 | `0a0e459edd8717aa2e3fe26d630e3f130f6c88d46ae05b651350312c6de9f698` | Security audit intake / domain scope |
| 8 | `قسم · قسم · قسم · استكمال المحادثة.txt` | `file_00000000f4588210afbd7b7c9b873a51` | 7,437 | 155 | `fed4978a62d4d1a54c6f1848c5b36d4581be5dfb79181a1cddf50bdf84ff289f` | Earlier Stage 1 source inventory/provenance report |
| 9 | `قسم · قسم · رفع الملفات على دفعات.txt` | `file_000000004f408210a23cc447faaf4aa8` | 1,979 | 26 | `d1a9e4c94d8f10a5d60c0e333cf5f0af55bc291c645085d419d2e9fa6fe86f4e` | Package Structure Reconciliation approval record (design-only at that time) |
| 10 | `قسم · قسم · رفع الملفات على دفعات.txt` | `file_00000000343482109ecb2381153c4550` | 11,881 | 201 | `4eb0b34fa66c06b4fbcb9b5afdc17a132298bbf3a32882e15c14ebf77da5a618` | Document mapping / package reconciliation review |
| 11 | `5.txt` | `file_00000000be5081f5b0278b728bd9a43e` | 17,454 | 248 | `5cf496c1183f36d23ef46d22b3afb7438728df70175bd17f0f46014c130dce60` | SECURITY_AUDIT RECONCILIATION — INITIAL |
| 12 | `00.txt` | `file_00000000333c8210889b5112bd178f0c` | 12,519 | 227 | `b7d134696c9229b988decc7ba204bc9e7b723638eb0bbd117aae401f1c02ab9f` | Network/Security initial reconciliation and open gaps |
| 13 | `00.txt` | `file_000000004f3882108d820c554b6290b8` | 12,581 | 222 | `6e403e3e05fa6eb9d0b89041139931f0a67a604435a75ad0d7c9d0d1f5125971` | Traceability Reconciliation — Initial Pass |
| 14 | `قسم · قسم · استكمال المحادثة.txt` | `file_00000000b4b48210ae17137a2377fb16` | 9,625 | 145 | `0177fcd7e59f79db23d18ff9b820841618fbc29bd5b071a8f43f6546c35d6f96` | Report 05 — Source Inventory & Provenance Reconciliation |
| 15 | `01.txt` | `file_00000000f2748210a36cd79959759fba` | 9,565 | 196 | `f6a87965829fcd44f787b9e984340d659d1748258e934672de7618f09b44c26b` | Resumption plan / staged documentation proposal |

**Hash scope:** these digests authenticate the exact 15 Project attachment byte streams materialized in this snapshot. They do not authenticate the separate Library records listed below.

## 3. Project Library inventory

A fresh recursive Library listing returned **40 file records + 1 folder = 41 items** under `/لعبة`. This is a metadata inventory. Per the Stage 0 Negative Attestation, Library raw-byte export was not available through the current authorized path; no Library payload SHA-256, byte-level comparison, or ZIP-content claim is made here.

### 3.1 Library file records

1. `CONSISTENCY_MATRIX.md`
2. `core-test-results-42383513646.zip`
3. `COST_OF_STAYING_CONTEXT_AUDIT_RECORD_2026-11-08.md`
4. `CostOfStaying_Docs_Diff_2026-11-08.zip`
5. `CostOfStaying_STAGE1_SourceInventory_Package.zip`
6. `CostOfStaying_STAGE2_LOCAL.zip`
7. `CostOfStaying_Stage2_Review_Package_2026-11-08.zip`
8. `DECISION_AND_CONFLICT_LOG.md`
9. `DIFF.patch`
10. `DIFF_SUMMARY.md`
11. `FILE_MANIFEST.json`
12. `FINAL_REVIEW_REPORT.md`
13. `FULL_DOCUMENTS_REVIEW.md`
14. `NEGATIVE_ATTESTATION_STAGE0.md`
15. `PHASED_WORK_PLAN.md`
16. `PROGRESS_LOG(1).md`
17. `PROGRESS_LOG.md`
18. `PROVENANCE_RECONCILIATION_NOTE_STAGE0.md`
19. `PROVENANCE_REGISTER.md`
20. `README.diff`
21. `REVIEW_INDEX.md`
22. `SHA256SUMS-LOCAL.txt`
23. `SHA256SUMS-PROPOSED.txt`
24. `SHA256SUMS-STAGE1-LOCAL.txt`
25. `SHA256SUMS-STAGE1.txt`
26. `SHA256SUMS-STAGE2.txt`
27. `SHA256SUMS.txt`
28. `SOURCE_INVENTORY.md`
29. `SOURCE_TRACEABILITY_REGISTER.md`
30. `STAGE1_PROGRESS_LOG.md`
31. `STAGE2_EVIDENCE_RECONCILIATION_REPORT.md`
32. `STAGE_1.md`
33. `STAGE_2.md`
34. `STAGE_2_CLOSURE_DECLARATION.md`
35. `STAGE_2_REVIEW.md`
36. `STAGE_2_REVIEW_FINDINGS.md`
37. `TBD_REGISTER.md`
38. `VALIDATION_REPORT.md`
39. `ما هو CodeQL؟_2293753019127521858.md`
40. `محادثة 2.html`

The 40 filenames above correspond to the 40 file records returned by the current listing. The remaining item is the `/لعبة` folder itself. This corrects the earlier Library inventory report that recorded 35 file records + 1 folder; the current 40-file listing is the controlling snapshot for this document version.

### 3.2 Library families

- **Conversation/export candidate:** `محادثة 2.html`.
- **Audit/review:** `FULL_DOCUMENTS_REVIEW.md`, `REVIEW_INDEX.md`, `FINAL_REVIEW_REPORT.md`, `STAGE_2_REVIEW_FINDINGS.md`, `STAGE_2_REVIEW.md`, `STAGE_2.md`, `STAGE_1.md`, `STAGE2_EVIDENCE_RECONCILIATION_REPORT.md`, `COST_OF_STAYING_CONTEXT_AUDIT_RECORD_2026-11-08.md`, `VALIDATION_REPORT.md`.
- **Registers:** `TBD_REGISTER.md`, `SOURCE_TRACEABILITY_REGISTER.md`, `CONSISTENCY_MATRIX.md`, `DECISION_AND_CONFLICT_LOG.md`, `PROGRESS_LOG.md`, `PROGRESS_LOG(1).md`.
- **Plans and metadata:** `PHASED_WORK_PLAN.md`, `FILE_MANIFEST.json`, `DIFF_SUMMARY.md`.
- **Diffs/manifests:** `DIFF.patch`, `README.diff`, `SHA256SUMS-LOCAL.txt`, `SHA256SUMS-PROPOSED.txt`, `SHA256SUMS-STAGE1-LOCAL.txt`, `SHA256SUMS-STAGE1.txt`, `SHA256SUMS-STAGE2.txt`, `SHA256SUMS.txt`.
- **Archives/artifacts:** `CostOfStaying_Docs_Diff_2026-11-08.zip`, `CostOfStaying_STAGE1_SourceInventory_Package.zip`, `CostOfStaying_STAGE2_LOCAL.zip`, `CostOfStaying_Stage2_Review_Package_2026-11-08.zip`, `core-test-results-42383513646.zip`.
- **Stage 0 provenance:** `PROVENANCE_RECONCILIATION_NOTE_STAGE0.md`, `NEGATIVE_ATTESTATION_STAGE0.md`, `STAGE_2_CLOSURE_DECLARATION.md`.
- **Technical reference:** `ما هو CodeQL؟_2293753019127521858.md`.

The filenames are metadata only. Do not infer archive contents, original author, uploader, or source turn from a filename.

## 4. Conversation provenance records

| ID | Source | Current status | Boundary |
|---|---|---|---|
| CHAT-001 | Project conversation labelled `1`, user-attested as formerly associated with DeepSeek | `USER-ATTESTED — CONTENT NOT RECOVERED` | No complete thread export or thread ID available. |
| CHAT-002 | Project conversation labelled `2`, user-attested as formerly associated with DeepSeek | `USER-ATTESTED — EXACT EXPORT MAPPING PENDING` | The HTML file below is only a candidate; exact mapping is not established. |
| CHAT-003 | Library file `محادثة 2.html` | `LIBRARY RECORD PRESENT — TEXT/METADATA SCOPE` | Historical reports describe 449 messages (226 user / 223 assistant), but this count and prior SHA are not revalidated from raw Library bytes in this pass. |
| CHAT-004 | Current active project conversation | `CURRENT CONTEXT AVAILABLE` | The current messages are visible; no separate export generated. |

Other project conversation titles surfaced in summaries include `استكمال المحادثة`, `قسم · استكمال المحادثة`, `2 استكمال المحادثة`, `رفع الملفات على دفعات`, and `مراجعة شاملة للمشروع`. These references are not a complete thread index.

## 5. GitHub repository snapshot

Repository: `live-egypt/CostOfStaying` (public; default branch `main`).

| Item | Snapshot result | Evidence boundary |
|---|---|---|
| `main` | HEAD `3dd9cfc27aa48b09b3cde802e266a8221c39f0d3` | Read-only GitHub comparison at snapshot time. |
| `feature/core-lifecycle-resolution` | HEAD `2820285021b1a3d7ae9acd953b3bb65677c33512` | Read-only GitHub comparison at snapshot time. |
| Branch comparison | Feature branch is 441 commits ahead of `main`, 0 behind | Does not mean PR #6 is approved or merged. |
| PR #6 | Open Draft PR; base `main`; head `feature/core-lifecycle-resolution`; 94 files per PR metadata/report | Unmerged; contains code, tests, ADRs, docs, and CI changes. |
| New documentation branch | `docs/stage1-source-provenance-recovery` | Created from the observed `main` SHA; contains only this Stage 1 documentation work. |

The separate documentation branch is intentionally based on `main`, not on the existing 441-commit feature branch, so the Stage 1 documentation PR does not inherit the unrelated Core/Tests/ADR/CI changes from PR #6.

## 6. Stage 1 inventory result

- Current Project attachments: **15 distinct file records; 15 raw SHA-256 values calculated**.
- Project Library: **40 file records + 1 folder**, metadata-only.
- Four same-basename continuation reports: distinct content identities confirmed.
- Complete Project conversation enumeration: **not exposed**.
- DeepSeek-related project chat contents: **not recovered**.
- ChatGPT HTML mapping and raw-byte integrity: **not revalidated**.
- Original Network Audit: **not independently recovered**.
- Original Security Audit: **source partial; reconciliation material available**.

**Stage 1 status remains PARTIAL.** A source inventory is not evidence that every conversation, original audit, or historical decision has been recovered.
