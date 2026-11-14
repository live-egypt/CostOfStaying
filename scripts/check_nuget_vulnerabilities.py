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

    projects = payload.get("projects")
    if not isinstance(projects, list) or not projects:
        raise ValueError("NuGet report has no projects; refusing to treat an empty/invalid report as clean.")

    findings: list[str] = []
    frameworks_seen = 0
    package_records_seen = 0

    for project in projects:
        if not isinstance(project, dict):
            raise ValueError("NuGet report contains an invalid project entry.")
        project_path = str(project.get("path", "<unknown-project>"))
        frameworks = project.get("frameworks")
        if not isinstance(frameworks, list):
            raise ValueError(f"NuGet report has no framework list for {project_path}.")

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

    if frameworks_seen == 0:
        raise ValueError("NuGet report contains no target frameworks; refusing to treat it as clean.")

    return package_records_seen, findings


def self_test() -> None:
    clean = {
        "version": 1,
        "projects": [{
            "path": "tests/Example.csproj",
            "frameworks": [{
                "framework": "net10.0",
                "topLevelPackages": [{"id": "Example.Safe", "vulnerabilities": []}],
                "transitivePackages": [],
            }],
        }],
    }
    vulnerable = {
        "version": 1,
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
    args = parser.parse_args()

    try:
        if args.self_test:
            self_test()
            return 0
        if args.report is None:
            parser.error("provide a NuGet JSON report or --self-test")
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
