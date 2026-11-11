# PDR-012 — Source Inventory Crosswalk

**Workstream:** A5 — Document Control  
**Status:** PARTIAL — CURRENT ATTACHMENT SNAPSHOT INVENTORIED; FULL SOURCE UNIVERSE NOT ESTABLISHED  
**Document class:** Source inventory / crosswalk evidence  
**Repository:** `live-egypt/CostOfStaying`  
**Repository base examined:** `main` at review time  
**Local evidence snapshot:** `/mnt/data`, 2026-11-11  
**Related gaps:** SOURCE-INV-01; SRC-01..03; MSR-001..004; DOC-PACKAGE-01

## 1. Purpose and boundary

PDR-012 requires a crosswalk between project conversations, attachments, Project Library materials, repository files, versions, hashes, and provenance records.

This document records what can be directly inventoried in the current execution environment and what remains unavailable. It does not claim that the current attachment snapshot is the complete ChatGPT Project Library, all historical uploads, all conversation exports, or the complete source universe.

## 2. Directly inventoried local attachment snapshot

A filesystem inventory of `/mnt/data` on 2026-11-11 returned **18 visible files**. File sizes and SHA-256 hashes below were computed from the exact bytes present at that path during this review.

| # | Visible filename | Bytes | SHA-256 |
|---:|---|---:|---|
| 1 | `00.txt` | 12,519 | `b7d134696c9229b988decc7ba204bc9e7b723638eb0bbd117aae401f1c02ab9f` |
| 2 | `01.txt` | 9,565 | `f6a87965829fcd44f787b9e984340d659d1748258e934672de7618f09b44c26b` |
| 3 | `03.txt` | 6,852 | `12bf0b421a6158bca1d0c6c514c72ee5e2a61c7c0bb04130fdc3815772baf393` |
| 4 | `2 استكمال المحادثة.txt` | 8,163 | `5bc108ffd11326d10d83562198d0fdca99fe7a3fe49204952ed020152ecd866d` |
| 5 | `5.txt` | 17,454 | `5cf496c1183f36d23ef46d22b3afb7438728df70175bd17f0f46014c130dce60` |
| 6 | `Governance Baseline Setup.txt` | 6,090 | `da1538c1c6db713114d266fc31813df410d47de0c4f84fe16eb273657b404bfc` |
| 7 | `رفع الملفات على دفعات.txt` | 4,719 | `02688c4ab66f7cb6a32896159854b62028fcab019072967c09fc334c2e438333` |
| 8 | `قسم · قسم · 00.txt` | 21,841 | `45130fc80bf74c52035d7eb65ed1150c291a8e23d242ac951445d4fb77dd4730` |
| 9 | `قسم · قسم · استكمال المحادثة.txt` | 9,625 | `0177fcd7e59f79db23d18ff9b820841618fbc29bd5b071a8f43f6546c35d6f96` |
| 10 | `قسم · قسم · رفع الملفات على دفعات.txt` | 4,972 | `0a0e459edd8717aa2e3fe26d630e3f130f6c88d46ae05b651350312c6de9f698` |
| 11 | `قسم · قسم · قسم · استكمال المحادثة.txt` | 7,437 | `fed4978a62d4d1a54c6f1848c5b36d4581be5dfb79181a1cddf50bdf84ff289f` |
| 12 | `قسم · قسم · قسم · قسم · قسم · مراجعة وجرد الملفات.txt` | 2,794 | `fa7123aebc81d82a22b5f8877cebf3a86c152d40c0ad2b8ca6e02cee46c06ac4` |
| 13 | `قسم · قسم · قسم · مراجعة وجرد الملفات.txt` | 5,740 | `26015708d159cc98e56315420dc4f3fc86c46c9eb35f02f9b2da8e4bd21842a9` |
| 14 | `قسم · قسم · مراجعة وجرد الملفات.txt` | 5,314 | `51105bf049497182fc6edd233533014a65a0d633f25f5d2da4f284b96086d098` |
| 15 | `قسم · مراجعة وجرد الملفات.txt` | 5,314 | `23ebb91ab1e85b562e41aaca7884ccaf98f306aab0d20ce03340ef40794e817d` |
| 16 | `متابعة حالة Cost of Staying أسبوعيًا.txt` | 3,932 | `11d87c3df43259d41cdfc481c967baac992090b4e46aafcee1433806d3f08509` |
| 17 | `محادثة 2.html` | 26,698,846 | `bfc98927949b967853133d218c28a26f974922aacc94c6c0af4b6a20a5e0c506` |
| 18 | `مراجعة وجرد الملفات.txt` | 5,380 | `c9ede33e70ab944241e962b7e8796cd8986e474097a6108522475e439d7f3840` |

**Integrity result:** The 18 visible files have 18 distinct SHA-256 values in this snapshot. This establishes byte-level distinction within this snapshot only; it does not establish unique source identity, completeness, authority, or absence of other historical copies.

## 3. Source groups and crosswalk status

| Source group | Evidence currently available | Crosswalk status | Missing / limitation |
|---|---|---|---|
| Current conversation attachments | 18 filesystem-visible files with size and SHA-256 | INVENTORIED — SNAPSHOT | Upload event IDs, full original upload history, and any overwritten same-name payloads are not fully represented by the filesystem snapshot. |
| Historical conversation exports | `محادثة 2.html` is present; SHA-256 is recorded above | FILE PRESENT — IDENTITY LINKAGE NOT VERIFIED | Its relationship to the conversation labelled Chat 2 must be proven from native metadata/message continuity; filename alone is insufficient. |
| DeepSeek conversations | Historical records refer to Chat 1 and Chat 2 | SOURCE PENDING | Native original exports and reliable conversation identity mapping are not available in the current snapshot. |
| ChatGPT Project Library | Prior records describe a partial inventory | NOT DIRECTLY VERIFIED | No complete direct Project Library listing with file IDs, names, versions, and locations was available to this execution. |
| Repository `main` | Current README, governance decision log, requirements traceability, and project documentation were fetched directly | PARTIAL REPOSITORY INVENTORY | The repository is not a substitute for conversation exports or Project Library inventory. |
| Network Audit original | Prior gap record states the full original was not established among recovered materials | SOURCE PARTIAL / PENDING | Complete original audit and provenance are missing. |
| Security Audit original | Prior gap record distinguishes an initial reconciliation from the full original audit | SOURCE PARTIAL | Complete original audit and provenance are missing. |
| Stage 1 / Stage 2 outputs | Some reports and copied passages are present in the attachment snapshot | PARTIAL | Local existence, canonical version, and approval chain of every historical output are not independently established. |
| Historical PR/package sources | PR #8/#9/#11 are referenced in project records; PR #11 is a proposed baseline only | PARTIAL | Full tree/content comparison and final package authority remain under DOC-PACKAGE-01 / PDR-016. |

## 4. Repository evidence crosswalk

The following files were directly fetched from the examined repository default branch:

| Repository path | Observed role | Blob SHA | Status / limitation |
|---|---|---|---|
| `README.md` | Project overview and status definitions | `3618195bdb715d3fe433adcfce1ebac54f4bfae6` | Current repository overview; not source inventory. |
| `docs/README.md` | Documentation index | `1a69115d9a984d2b8abbda9b9c268a0639ea6d61` | Does not index a canonical source inventory or provenance register. |
| `docs/governance/DECISIONS_LOG.md` | ADR decision index | `8cb7e4f4d2d30436bdeb05eebb61cf55b9e4c59b` | Contains ADR-001..004; not a historical source ledger. |
| `docs/governance/REQUIREMENTS_TRACEABILITY.md` | Proposed requirement/control/test trace examples | `204d02d19e2bd7fa23a749ec818cf26f7096b514` | Explicitly PROPOSED; rows are preliminary examples, not source inventory. |

PDR-009, PDR-010, and PDR-011 review documents are being maintained on separate unmerged feature branches and are not part of `main`. Their existence must not be represented as an approved baseline or as proof that the source gaps have been resolved.

## 5. Reconciliation of historical inventory counts

Earlier project records refer to different counts and scopes, including:

- a prior review of 9 attached files;
- a separate report of 5 Project Attachments;
- an 11-item attachment set at a later snapshot;
- the 18 filesystem-visible files inventoried in this review.

These counts must not be added together or treated as contradictory without matching each item by immutable identity/hash and acquisition context. They appear to describe different snapshots, scopes, or reporting layers. The relationship between them remains **UNRESOLVED**.

A file that describes another file is a report/source record in its own right; it is not equivalent to the original file it describes.

## 6. Known source gaps carried forward

| Gap | Current evidence-derived status | Required crosswalk work |
|---|---|---|
| SOURCE-INV-01 | PARTIAL | Reconcile attachments, Project Library, conversation exports, repository files, hashes, and provenance. |
| SRC-01 | OPEN | Recover native DeepSeek exports and map them to conversation identities. |
| SRC-02 | OPEN | Obtain complete Project Library inventory, including file IDs, names, versions, and locations. |
| SRC-03 | PARTIAL | Recompute SHA-256 for all in-scope original files and establish one-to-one source mapping. |
| MSR-001..004 | OPEN / exact payloads not established in this crosswalk | Recover each original MSR definition and named primary source. |
| DOC-PACKAGE-01 | OPEN | Compare PR #8/#9/#11 and main trees/content before package consolidation. |
| NET-SRC-01 | OPEN | Recover complete original Network Audit. |
| SEC-SRC-01 | OPEN / PARTIAL source context | Recover complete original Security Audit and distinguish it from reconciliation drafts. |
| SO-GAP-02 | OPEN | Verify the identity relationship between `محادثة 2.html` and the conversation called Chat 2. |
| SO-GAP-04 | OPEN | Recover the decisive primary source or record an explicit owner decision suspending recovery with impact. |

No gap is closed by appearing in this table.

## 7. Required next actions

1. Obtain a direct Project Library export/listing from the account holder, preserving platform file IDs and original metadata.
2. Obtain native DeepSeek Chat 1 and Chat 2 exports; record export hashes and conversation identifiers.
3. Recover original Network and Security audit files, not merely summaries or reconciliation reports.
4. Recover exact MSR-001..004 payloads and link each to its named primary source.
5. Compare the prior 9-, 5-, and 11-item snapshots against the current 18-file snapshot using hashes, metadata, and source IDs; do not infer equivalence from filenames.
6. Recompute hashes for the exact original files in each recovered source set.
7. Complete a row-level crosswalk with Source ID, platform, conversation/library identity, original filename, upload/file ID, version/date, size, SHA-256, repository path/commit if applicable, provenance reference, authority class, and recovery status.
8. Reconcile package paths and versions against DOC-PACKAGE-01 and PDR-016 before migration or consolidation.

## 8. Determination

**PDR-012 = PARTIAL — CURRENT ATTACHMENT SNAPSHOT HASHED; COMPLETE SOURCE INVENTORY AND CROSSWALK NOT ESTABLISHED.**

The 18-file local snapshot is directly inventoried and hash-identified. This is a concrete, reproducible sub-result. It does not close SOURCE-INV-01, SRC-01..03, DOC-PACKAGE-01, or the missing-source records.

## 9. Non-effects

- No claim of complete Project Library coverage.
- No claim that all historical conversations or attachments have been recovered.
- No promotion of any source to SOURCE-VERIFIED based on filename or summary alone.
- No resolution of SOURCE-ORIGIN-01 or SO-GAP-04.
- No change to A4 decisions, PDR-009/010/011, or other open PDRs.
- No deletion, deduplication, migration, renaming, or package consolidation.
- No change to `main`; no code, gameplay, Unity, FishNet, CI, or runtime changes.

## 10. Status summary

| Item | Status |
|---|---|
| Current `/mnt/data` attachment snapshot | IMPLEMENTED — 18 files inventoried |
| SHA-256 computation for snapshot | VERIFIED — exact bytes in current runtime |
| Complete Project Library inventory | NOT AVAILABLE / TBD |
| DeepSeek native exports | NOT RECOVERED |
| Complete source-to-decision provenance | PARTIAL |
| SOURCE-INV-01 | PARTIAL |
| PDR-012 | PARTIAL — NOT CLOSED |
| A5 PDR-012 closure | NOT ACHIEVED |
