# ADR-007 Lifecycle Implementation — CI Verification

**Verification date:** 2026-11-01  
**Source commit:** `48ee4d47d5f63b89b3210b59e8a289dd4ee8ba05`  
**Branch:** `feature/core-lifecycle-resolution`  
**Workflow:** Core CI  
**Run:** `41325988534`  
**Job:** Build and test standalone Core — SUCCESS

## Verified checks

- .NET 8 setup: passed.
- Restore NUnit test project: passed.
- Core Release build: passed.
- Test project Release build: passed.
- NUnit execution: **45 passed, 0 failed, 0 skipped**.
- TRX upload: passed.

Artifact:
- Name: `core-test-results`
- ID: `13091132468`
- SHA-256: `f45d6d749463f2401906659e8c9f534977a392e2df3b0107be270186529ba783`

## Lifecycle behavior covered

- New account starts Alive at Maximum.
- Vitality loss reduces the resource and clamps at zero.
- Reaching zero automatically transitions Alive → Spirit.
- Restore is Alive-only and cannot revive Spirit.
- Resurrect is explicit, Spirit-only and requires positive requested vitality.
- Resurrect clamps the restored value to Maximum.
- Eliminate explicitly transitions Alive or Spirit → Eliminated.
- Eliminated is terminal; loss, restore and resurrection cannot mutate it.
- Rejected transitions preserve state.
- Accounts remain independent.

## Scope boundary

This run verifies the standalone pure-C# Core and NUnit tests for the exact source commit above. It does not verify Unity import/compilation, FishNet integration, Android builds, multiplayer authentication, Host/Dedicated Server behavior, gameplay balance, persistence or production security.

The workflow run also triggered inherited scanner workflows. Their results are separate from Core CI and do not affect the NUnit pass count. A failed scanner is not a clean security scan.

**Status:** ADR-007 lifecycle implementation is VERIFIED for the standalone Core scope at the source commit above. Later documentation-only commits do not change the tested source files. Any subsequent source change requires a new CI run.
