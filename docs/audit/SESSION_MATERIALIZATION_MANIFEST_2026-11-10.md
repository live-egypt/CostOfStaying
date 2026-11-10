# Current Session Materialization Manifest — 2026-11-10

**Revision:** 2026-11-10  
**Decision Status:** —  
**Evidence Maturity:** DOCUMENTED — SHA-256 computed from the exact bytes present in the active runtime  
**Scope:** Ten text-file paths currently materialized in /mnt/data for this session.

> This manifest identifies the ten files available in the active runtime at the time of inspection. It does not assert that these ten paths are the complete Project attachment surface. Repeated basenames in earlier Project inventories may represent separate attachment IDs; this runtime does not expose all such records as separate paths. Do not infer a missing file's content from its name.

| # | Runtime filename | Bytes | Lines | SHA-256 |
|---:|---|---:|---:|---|
| 1 | 00.txt | 12,519 | 227 | b7d134696c9229b988decc7ba204bc9e7b723638eb0bbd117aae401f1c02ab9f |
| 2 | 01.txt | 9,565 | 196 | f6a87965829fcd44f787b9e984340d659d1748258e934672de7618f09b44c26b |
| 3 | 03.txt | 6,852 | 117 | 12bf0b421a6158bca1d0c6c514c72ee5e2a61c7c0bb04130fdc3815772baf393 |
| 4 | 2 استكمال المحادثة.txt | 8,163 | 199 | 5bc108ffd11326d10d83562198d0fdca99fe7a3fe49204952ed020152ecd866d |
| 5 | 5.txt | 17,454 | 248 | 5cf496c1183f36d23ef46d22b3afb7438728df70175bd17f0f46014c130dce60 |
| 6 | Governance Baseline Setup.txt | 6,090 | 137 | da1538c1c6db713114d266fc31813df410d47de0c4f84fe16eb273657b404bfc |
| 7 | رفع الملفات على دفعات.txt | 4,719 | 43 | 02688c4ab66f7cb6a32896159854b62028fcab019072967c09fc334c2e438333 |
| 8 | قسم · قسم · استكمال المحادثة.txt | 9,625 | 145 | 0177fcd7e59f79db23d18ff9b820841618fbc29bd5b071a8f43f6546c35d6f96 |
| 9 | قسم · قسم · رفع الملفات على دفعات.txt | 4,972 | 120 | 0a0e459edd8717aa2e3fe26d630e3f130f6c88d46ae05b651350312c6de9f698 |
| 10 | قسم · قسم · قسم · استكمال المحادثة.txt | 7,437 | 155 | fed4978a62d4d1a54c6f1848c5b36d4581be5dfb79181a1cddf50bdf84ff289f |

## 1. Aggregate

- Materialized runtime paths: 10
- Total bytes: 87,396
- Distinct contents by SHA-256: 10
- Original Project attachment completeness: NOT ESTABLISHED
- Full Project Library byte-level access: NOT ESTABLISHED

## 2. Provenance boundary

These hashes prove the identity of the bytes measured in this runtime only. They do not establish original uploader, source application, original conversation thread, Library byte equality, or archive contents. The manifest must not be used to overwrite the 15-attachment historical source snapshot reported in PR #8/#9. Those are different snapshots/surfaces until an exact crosswalk is available.

## 3. Reproduction

From the directory containing the exact materialized files:

```bash
sha256sum -- *.txt
wc -lc -- *.txt
```

Re-running those commands against a different attachment set or a later session produces a different snapshot and must be recorded separately.
