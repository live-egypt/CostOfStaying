# Provenance Register — Cost of Staying

**Snapshot:** 2026-11-09  
**Status:** `PARTIAL — 15 CURRENT ATTACHMENTS HASHED; LIBRARY AND CHAT PROVENANCE LIMITED`  
**Companion:** [SOURCE_INVENTORY.md](SOURCE_INVENTORY.md)

## 1. Provenance rules

1. An original conversation, an exported conversation, a summary, an audit report derived from a conversation, a Library copy, and a Git commit are distinct provenance objects.
2. A matching filename is not proof of matching content.
3. `USER-ATTESTED` is not independent content verification.
4. `RECORDED` is not `SOURCE VERIFIED`; `APPROVED` is not `IMPLEMENTED`; `IMPLEMENTED` is not `VERIFIED`.
5. For Project attachments, the exact file IDs and raw-byte SHA-256 values in the companion inventory identify the materialized attachment versions.
6. For Library objects, the current evidence is metadata and extracted text only. Stage 0's environment-scoped Negative Attestation remains binding: no raw-byte SHA-256, byte-level comparison, archive-content assertion, or uploader attribution may be claimed unless a later authorized source provides it.
7. No report derived from a source may silently replace the original source.
8. A source's claim about another file is not equivalent to possession of that file.

## 2. Conversation provenance

| ID | Object | Origin / label | Current evidence | Status | Missing proof |
|---|---|---|---|---|---|
| CHAT-001 | Project conversation `1` | User says it originated from DeepSeek and was renamed | User-attested project context | `USER-ATTESTED — CONTENT NOT RECOVERED` | Full thread, thread ID, original export, authenticated platform origin. |
| CHAT-002 | Project conversation `2` | User says it originated from DeepSeek and was renamed | User-attested project context; HTML export candidate exists | `USER-ATTESTED — EXPORT MAPPING PENDING` | Full thread, thread ID, proof that the HTML export maps to this project chat. |
| CHAT-003 | `محادثة 2.html` | ChatGPT Shared Chat export candidate | Library metadata and indexed text/header | `LIBRARY RECORD PRESENT — TEXT/METADATA ONLY` | Raw Library bytes, independently verified message count/hash, exact project-thread mapping. |
| CHAT-004 | Current active project thread | ChatGPT Project | Current conversation messages | `CURRENT CONTEXT AVAILABLE` | A separately generated, complete export is not available. |

Earlier reports state that the HTML export contains 449 messages (226 user and 223 assistant) and record a SHA-256 value. These are retained as **historical report claims**, not re-certified facts, because raw Library bytes were not available for this pass.

## 3. Current Project attachment provenance

The following attachment IDs are separate provenance objects. Exact byte sizes and hashes are maintained in [SOURCE_INVENTORY.md](SOURCE_INVENTORY.md).

| ID | File ID suffix | Logical classification | Status |
|---|---|---|---|
| ATT-001 | `35308210ba764fa41dca76ef` | Report 02 — Source Recovery & Decision Traceability | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-002 | `8adc821088a91de351f5ada2` | Broad multi-source audit | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-003 | `ab5482109ff976a094c0b9fe` | Report 03 — Consistency Audit | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-004 | `72e08210b2d34211cd183832` | Report 04 — Repository Forensic Audit | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-005 | `e5b88210973c72a025e1cdb8` | Batch intake: gameplay, playtesting, forensic risk, repository audit | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-006 | `75a882108078bf80ee654734` | RECONCILIATION-01…06 | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-007 | `5bb4821095c73b6c4a99e6cf` | Security audit intake | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-008 | `f4588210afbd7b7c9b873a51` | Earlier Stage 1 report | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-009 | `4f408210a23cc447faaf4aa8` | Package Structure Reconciliation approval record | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-010 | `343482109ecb2381153c4550` | Document mapping / package reconciliation | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-011 | `be5081f5b0278b728bd9a43e` | Security audit reconciliation | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-012 | `333c8210889b5112bd178f0c` | Network/Security initial reconciliation | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-013 | `4f3882108d820c554b6290b8` | Traceability Reconciliation — Initial Pass | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-014 | `b4b48210ae17137a2377fb16` | Report 05 — Source Inventory & Provenance Reconciliation | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |
| ATT-015 | `f2748210a36cd79959759fba` | Staged documentation plan / resumption report | `RAW BYTES MATERIALIZED; SHA-256 CALCULATED` |

The full IDs, display names, sizes, line counts, and digests are in the companion inventory. Attachment hashes authenticate the 15 supplied file streams only; they do not prove the truth of every claim inside those files.

## 4. Project Library provenance

The recursive Library listing returned 35 file records and one folder. The Library contains, among other records, `SOURCE_INVENTORY.md`, `PROVENANCE_REGISTER.md`, `REVIEW_INDEX.md`, `TBD_REGISTER.md`, `CONSISTENCY_MATRIX.md`, `STAGE_1.md`, `STAGE_2.md`, `STAGE_2_CLOSURE_DECLARATION.md`, several progress logs, ZIP archives, diffs, manifests, and `محادثة 2.html`.

### Mandatory Library limitation

The Stage 0 Negative Attestation applies to the identified Library records whose raw-byte export was unavailable. For those records:

- Metadata and extracted text may be cited within their actual scope.
- Raw-byte equality or difference is not established.
- SHA-256 of original Library payload bytes is not established.
- ZIP member lists and archive contents are not established unless separately read through an authorized route.
- Uploader, initiating action, and causal upload sequence are not established.

A Library record named `STAGE_2_CLOSURE_DECLARATION.md` whose extracted content says it was not written to the Library is a provenance contradiction. It does not identify who caused the record to exist. Preserve the record; do not delete, rename, overwrite, or infer authorship.

## 5. Derived reports and source dependencies

| Provenance ID | Derived record | Parent/source objects | Limitation |
|---|---|---|---|
| PROV-001 | Report 02 | Project Files attachment ATT-001 | Report, not original chat transcript. |
| PROV-002 | Broad multi-source audit | Project Files attachment ATT-002 | Contains claims about multiple sources; each claim needs source-level traceability. |
| PROV-003 | Report 03 | Project Files attachment ATT-003 | Reconciliation report; original DeepSeek content remains absent. |
| PROV-004 | Report 04 | Project Files attachment ATT-004 | Repository findings are historical; compare with live repo snapshot before reuse. |
| PROV-005 | Security Audit Reconciliation | Project Files attachment ATT-011 | Explicitly initial/proposed; original full Security Audit not recovered. |
| PROV-006 | Network/Security initial reconciliation | Project Files attachment ATT-012 | Mapping draft; original Network Audit not recovered. |
| PROV-007 | Traceability initial pass | Project Files attachment ATT-013 | Historical source report; no automatic implementation proof. |
| PROV-008 | Report 05 | Project Files attachment ATT-014 | Snapshot count is 11 at that point in time; current attachment listing now returns 15. |
| PROV-009 | Earlier Stage 1 output description | Project Files attachment ATT-008 and Library copies | Local-output claims are historical; current Library records must be treated under Stage 0 limits. |

## 6. Repository provenance

| Object | Reference | Status |
|---|---|---|
| Repository | `live-egypt/CostOfStaying` | Live repository metadata queried read-only. |
| Default branch | `main` | `main` |
| Main snapshot | `3dd9cfc27aa48b09b3cde802e266a8221c39f0d3` | Observed in GitHub comparison. |
| Existing development branch | `feature/core-lifecycle-resolution` | Observed in branch search. |
| Development branch HEAD | `2820285021b1a3d7ae9acd953b3bb65677c33512` | Observed via reverse compare. |
| PR #6 | `Resolve player lifecycle and align Core vitality contract` | Open Draft PR per GitHub PR search; not merged. |
| Stage 1 docs branch | `docs/stage1-source-provenance-recovery` | Created from the observed `main` SHA to isolate documentation-only changes. |

PR #6 contains a mixture of Core, Tests, ADRs, documentation, and CI changes. The Stage 1 docs branch was intentionally created from `main` rather than from the PR #6 head to avoid importing those unrelated changes into a docs-only PR.

## 7. Provenance state vocabulary

- `USER-ATTESTED`: identity/existence stated by the user; content not independently recovered.
- `RECORDED`: a report or register mentions the source or claim.
- `LIBRARY RECORD PRESENT`: metadata confirms a Library record exists.
- `RAW BYTES MATERIALIZED; SHA-256 CALCULATED`: exact Project attachment bytes were materialized and hashed.
- `TEXT/METADATA ONLY`: raw bytes are not available; no byte-level claims.
- `SOURCE PARTIAL`: some derived or partial material exists, but the original source is incomplete.
- `SOURCE PENDING`: no sufficient source content is available.
- `VERIFIED`: use only for a specific claim with a specific reproducible evidence path; never apply globally to a source family.

## 8. Open provenance questions

1. What are the exact thread IDs and full exports for project conversations `1` and `2`?
2. Does `محادثة 2.html` map to project conversation `2`, or is it a different shared chat?
3. Can the original Network Audit be supplied?
4. Can the original full Security Audit be supplied?
5. Can the source conversations behind the reports be enumerated and mapped?
6. Can the Stage 0 Library raw-byte and upload-audit limitations be superseded by authorized evidence for the exact records?
7. Which source snapshot explains earlier attachment counts of 5, 9, and 11 versus the current count of 15?

**Current result:** Provenance is materially improved for the 15 current Project attachments, but overall provenance remains PARTIAL.
