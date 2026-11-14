#!/usr/bin/env python3
"""Fail a CI job when the .NET 10 NuGet vulnerability report contains findings."""

from __future__ import annotations

import argparse
import json
import sys
from pathlib import Path
from typing import Any


def analyze(payload: Any) -> tuple[int, list[str]]:
    if not isinstance(payload, dict) or payload.get("version") != 1:
        raise ValueError("Unsupported NuGet package-list JSON schema (expected version 1).")

    parameters = payload.get("parameters")
    sources = payload.get("sources")
    if not isinstance(parameters, str) or "--vulnerable" not in parameters:
        raise ValueError("NuGet report was not generated with the --vulnerable filter.")
    if not isinstance(sources, list) or not sources:
        raise ValueError("NuGet report contains no package/audit sources; refusing to assume a clean scan.")

    projects = payload.get("projects")
    if not isinstance(projects, list):
        raise ValueError("NuGet report projects field is missing or invalid.")

    findings: list[str] = []
    frameworks_seen = 0
    package_records_seen = 0

    # With --vulnerable, a valid report may contain no projects when no packages match.
    # The required version/parameters/sources metadata distinguishes that from an empty file.
    for project in projects:
        if not isinstance(project, dict):
            raise ValueError("NuGet report contains an invalid project entry.")
        project_path = str(project.get("path", "<unknown-project>"))
        frameworks = project.get("frameworks")
        if frameworks is None:
            # The .NET 10 --vulnerable JSON contract emits a path-only project
            # object when that project has no vulnerable direct/transitive packages.
            if set(project.keys()) == {"path"}:
                continue
            raise ValueError(f"NuGet report has an unexpected path-only project shape for {project_path}.")
        if not isinstance(frameworks, list):
            raise ValueError(f"NuGet report framework list is invalid for {project_path}.")

        for framework in frameworks:
            if not isinstance(framework, dict):
                raise ValueError(f"NuGet report contains an invalid framework entry for {project_path}.")
            frameworks_seen += 1
            framework_name = str(framework.get("framework", "<unknown-framework>"))

            for collection_name in ("topLevelPackages", "transitivePackages"):
                packages = framework.get(collection_name, [])
                if not isinstance(packages, list):
                    raise ValueError(
                        f"NuGet report field {collection_name} is not a list for {project_path}/{framework_name}."
                    )

                for package in packages:
                    if not isinstance(package, dict):
                        raise ValueError("NuGet report contains an invalid package entry.")
                    package_records_seen += 1
                    package_id = str(package.get("id", "<unknown-package>"))
                    vulnerabilities = package.get("vulnerabilities")
                    if not isinstance(vulnerabilities, list):
                        raise ValueError(
                            f"Package {package_id} has no vulnerability list; refusing to assume it is clean."
                        )

                    for vulnerability in vulnerabilities:
                        if not isinstance(vulnerability, dict):
                            raise ValueError(f"Package {package_id} contains an invalid vulnerability entry.")
                        severity = str(vulnerability.get("severity", "Unknown"))
                        advisory = str(vulnerability.get("advisoryurl", "<advisory URL unavailable>"))
                        findings.append(
                            f"{project_path} [{framework_name}] {package_id}: {severity} — {advisory}"
                        )

    return package_records_seen, findings


def self_test() -> None:
    clean = {
        "version": 1,
        "parameters": "--vulnerable --include-transitive",
        "sources": ["https://api.nuget.org/v3/index.json"],
        "projects": [{
            "path": "tests/Example.csproj",
        }],
    }
    vulnerable = {
        "version": 1,
        "parameters": "--vulnerable --include-transitive",
        "sources": ["https://api.nuget.org/v3/index.json"],
        "projects": [{
            "path": "tests/Example.csproj",
            "frameworks": [{
                "framework": "net10.0",
                "topLevelPackages": [{
                    "id": "Example.Vulnerable",
                    "vulnerabilities": [{
                        "severity": "High",
                        "advisoryurl": "https://github.com/advisories/example",
                    }],
                }],
                "transitivePackages": [],
            }],
        }],
    }
    _, clean_findings = analyze(clean)
    _, vulnerable_findings = analyze(vulnerable)
    if clean_findings or len(vulnerable_findings) != 1:
        raise SystemExit("NuGet audit gate self-test failed.")
    print("NuGet audit gate self-test passed (clean input accepted; vulnerable input rejected).")


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("report", nargs="?", type=Path)
    parser.add_argument("--self-test", action="store_true")
    parser.add_argument("--restore-log", type=Path)
    args = parser.parse_args()

    try:
        if args.self_test:
            self_test()
            return 0
        if args.report is None:
            parser.error("provide a NuGet JSON report or --self-test")
        if args.restore_log is not None:
            restore_output = args.restore_log.read_text(encoding="utf-8", errors="replace")
            unavailable = [
                line for line in restore_output.splitlines()
                if "NU1900" in line or "NU1905" in line
            ]
            if unavailable:
                raise ValueError(
                    "NuGet audit data source was unavailable or unsupported:\n" + "\n".join(unavailable)
                )
        payload = json.loads(args.report.read_text(encoding="utf-8"))
        package_records, findings = analyze(payload)
    except (OSError, json.JSONDecodeError, ValueError) as exc:
        print(f"NuGet audit failed closed: {exc}", file=sys.stderr)
        return 2

    print(f"NuGet report parsed: {package_records} package record(s) inspected.")
    if findings:
        print(f"Known vulnerability findings: {len(findings)}", file=sys.stderr)
        for finding in findings:
            print(f"::error::{finding}", file=sys.stderr)
        return 1

    print("No known NuGet vulnerabilities were reported for the audited dependency graph.")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
