# Attachment Snapshot Delta — 2026-11-10

**Snapshot:** Current project conversation attachment context, after the earlier 15-record Project Files snapshot  
**Evidence maturity:** REVIEWED — mounted local bytes hashed for the five listed objects  
**Purpose:** Preserve the five additional attachment objects without rewriting the earlier 15-record snapshot.

## 1. Reconciliation result

- Earlier Project Files snapshot recorded in the source inventory: **15 attachment objects**.
- Current project conversation context: **20 attachment objects**.
- Delta: **5 additional attachment objects**, identified by distinct Project file IDs.
- Current local materialization: **13 unique text-file paths** (the earlier 10 paths plus 3 additional paths). The 13-path runtime set is not equivalent to the 20-object attachment set because repeated basenames and attachment-object availability are separate dimensions.
- The prior 15-record inventory remains historically valid for its snapshot. This addendum records the later delta; it does not assert a complete Project Library or conversation inventory.

## 2. Five additional attachment objects

| # | Display name | Project file ID | Bytes | Lines | SHA-256 of mounted bytes | Content classification |
|---:|---|---|---:|---:|---|---|
| 16 | `Governance Baseline Setup.txt` | `file_00000000074881f4943706c6ccc431e2` | 6,090 | 137 | `da1538c1c6db713114d266fc31813df410d47de0c4f84fe16eb273657b404bfc` | Proposed A1–A5 governance foundation and reconciliation plan; source itself marks proposal/design-only. |
| 17 | `03.txt` | `file_00000000789081f4b111774b32cdf4fa` | 6,852 | 117 | `12bf0b421a6158bca1d0c6c514c72ee5e2a61c7c0bb04130fdc3815772baf393` | Repository/source reconciliation progress report; PR #8/#9/#10 and source-mapping status claims are historical until rechecked. |
| 18 | `متابعة حالة Cost of Staying أسبوعيًا.txt` | `file_000000000028821095370421e94071c9` | 3,932 | 44 | `11d87c3df43259d41cdfc481c967baac992090b4e46aafcee1433806d3f08509` | Scope correction for A3 source coverage; preserves A1-HOLD-01…06 and WO-A3 source-recovery blockers. |
| 19 | `مراجعة وجرد الملفات.txt` | `file_00000000f84082108edfadf90f93f71c` | 5,380 | 102 | `c9ede33e70ab944241e962b7e8796cd8986e474097a6108522475e439d7f3840` | Earlier inventory report; its stated 19-upload count is a prior snapshot, not the current 20-object count. |
| 20 | `قسم · مراجعة وجرد الملفات.txt` | `file_0000000045108210837f806761f19d7b` | 8,696 | 202 | `950ea1da49077e3d56763d0c1586e5f954ffade54d183092a239020bbe726dbc` | WO-DOC-01 proposal and five-package governance plan; approval is evidenced separately by the current owner message. |

The SHA-256 values identify the exact mounted bytes at the listed local filenames. They do not establish uploader identity, original platform origin, Library byte equality, or the completeness of any source conversation.

## 3. Runtime materialization delta

Three additional local text-file paths were present beyond the earlier ten-path runtime manifest:

| Runtime filename | Bytes | Lines | SHA-256 |
|---|---:|---:|---|
| `متابعة حالة Cost of Staying أسبوعيًا.txt` | 3,932 | 44 | `11d87c3df43259d41cdfc481c967baac992090b4e46aafcee1433806d3f08509` |
| `مراجعة وجرد الملفات.txt` | 5,380 | 102 | `c9ede33e70ab944241e962b7e8796cd8986e474097a6108522475e439d7f3840` |
| `قسم · مراجعة وجرد الملفات.txt` | 8,696 | 202 | `950ea1da49077e3d56763d0c1586e5f954ffade54d183092a239020bbe726dbc` |

The two other additional attachment objects (`03.txt` and `Governance Baseline Setup.txt`) were already among the earlier ten materialized runtime paths and therefore add attachment IDs, not new local paths.

**Updated local runtime aggregate:** 13 paths, 105,404 bytes, 13 distinct SHA-256 values. This aggregate is specific to the mounted files available in this session.

## 4. Relationship to canonical records

- The 15-record historical attachment inventory remains in `docs/governance/SOURCE_INVENTORY.md`.
- The historical provenance and limitations remain in `docs/governance/PROVENANCE_REGISTER.md`.
- The ten-path earlier runtime matrix/manifest remain historical snapshots; this addendum records the later delta rather than rewriting their measurement dates.
- The Project Library snapshot of 40 file records plus one folder is a separate metadata surface. No direct new Library enumeration was performed here.
- Full Project conversation enumeration and original thread IDs remain unavailable.
- ChatGPT/DeepSeek origin attribution remains OPEN.
- A1-HOLD-01…06 and the two mandatory WO-A3 amendments remain SOURCE BLOCKED; this attachment set contains reports about the blockers, not the original decision events.

## 5. Status

**Current attachment-context count:** 20 objects  
**Earlier attachment snapshot:** 15 objects  
**New delta:** 5 objects — IDs and mounted-byte hashes recorded  
**Current local materialization:** 13 unique paths / 105,404 bytes  
**Project Library:** historical metadata snapshot only; not re-enumerated  
**Complete conversation inventory:** NOT VERIFIED  
**Stage 1 closure:** NOT DECLARED
