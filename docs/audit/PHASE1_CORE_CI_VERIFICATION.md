# Phase 1 Core — CI Verification Evidence

**Result:** PASS  
**Scope:** Standalone .NET Core build and NUnit tests only  
**Run date:** 2026-11-01 (UTC)  
**Tested commit:** e86880a318df49164aa61fe5e43c7a47f9c44560

## Workflow

- Workflow: Core CI
- File: .github/workflows/core-ci.yml
- Event: pull_request
- Branch: feature/phase1-technical-consolidation
- Run ID: 41255553487
- Run URL: https://github.com/live-egypt/CostOfStaying/actions/runs/41255553487
- Job ID: 124697847140
- Job conclusion: success

## Environment

- Runner: GitHub-hosted ubuntu-latest
- SDK: .NET 8 (8.0.x)
- Core target: netstandard2.1
- Test target: net8.0
- Test framework: NUnit 3.14.0
- Test adapter: NUnit3TestAdapter 4.6.0
- Microsoft.NET.Test.Sdk: 17.12.0

## Executed gates

1. dotnet restore tests/CostOfStaying.Core.Tests/CostOfStaying.Core.Tests.csproj — PASS
2. dotnet build src/CostOfStaying.Core/CostOfStaying.Core.csproj --configuration Release --no-restore — PASS
3. dotnet test tests/CostOfStaying.Core.Tests/CostOfStaying.Core.Tests.csproj --configuration Release --no-restore — PASS

## NUnit result

- Passed: 38
- Failed: 0
- Skipped: 0
- Total: 38
- Duration: 21 ms

The workflow log reported: "Passed! - Failed: 0, Passed: 38, Skipped: 0, Total: 38".

## Retained artifact

- Artifact name: core-test-results
- Artifact ID: 13062876559
- Size: 5,994 bytes
- SHA-256 of artifact ZIP: 6b5a3b7497e0f48c531cb0354e4e2984acb77071cd623ab2ff4d6d8fd84d0712
- Artifact URL: https://github.com/live-egypt/CostOfStaying/actions/runs/41255553487/artifacts/13062876559

## Scope limitations

This evidence does not establish:
- Unity Editor import or compilation;
- FishNet package compatibility or API correctness;
- Host or Dedicated Server runtime behavior;
- authenticated connection/session enforcement;
- atomic Debuff transfer across live match state;
- multiplayer latency/loss/reconnect behavior;
- gameplay balance or playtest acceptance;
- conformance to the earlier owner-approved Alive / Spirit / Eliminated lifecycle matrix; this conflict is tracked in ADR-007;
- production security or release readiness.

Do not generalize this PASS result beyond the standalone Core build and the 38 NUnit tests in the specified commit.
