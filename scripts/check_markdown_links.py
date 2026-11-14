#!/usr/bin/env python3
"""Check local Markdown links in selected project entry documents."""

from __future__ import annotations

import argparse
import re
from pathlib import Path
from urllib.parse import unquote, urlsplit


INLINE_LINK = re.compile(r"!?[\x5b][^\x5d]*[\x5d][\x28]([^\x29]*)[\x29]")
REFERENCE_DEF = re.compile(r"^[ ]{0,3}[\x5b][^\x5d]+[\x5d]:[ ]*(\S+)", re.MULTILINE)
FENCE = re.compile(r"^[ ]{0,3}(\x60{3,}|~{3,})")


def remove_fenced_blocks(text: str) -> str:
    kept: list[str] = []
    fence_char: str | None = None
    fence_len = 0

    for line in text.splitlines():
        match = FENCE.match(line)
        if match:
            marker = match.group(1)
            if fence_char is None:
                fence_char = marker[0]
                fence_len = len(marker)
            elif marker[0] == fence_char and len(marker) >= fence_len:
                fence_char = None
                fence_len = 0
            continue
        if fence_char is None:
            kept.append(line)
        else:
            kept.append("")
    return "\n".join(kept)


def extract_targets(text: str) -> list[str]:
    visible = remove_fenced_blocks(text)
    targets: list[str] = []

    for match in INLINE_LINK.finditer(visible):
        target = match.group(1).strip()
        if target.startswith("<") and ">" in target:
            target = target[1:target.index(">")]
        else:
            target = target.split(maxsplit=1)[0] if target else ""
        if target:
            targets.append(target)

    targets.extend(match.group(1).strip("<>") for match in REFERENCE_DEF.finditer(visible))
    return targets


def local_target(markdown_file: Path, target: str, repo_root: Path) -> Path | None:
    target = target.strip()
    if not target or target.startswith("#"):
        return None

    parsed = urlsplit(target)
    if parsed.scheme or parsed.netloc:
        return None
    if not parsed.path:
        return None

    decoded = unquote(parsed.path)
    if decoded.startswith("/"):
        return (repo_root / decoded.lstrip("/")).resolve()
    return (markdown_file.parent / decoded).resolve()


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--report", type=Path, required=True)
    parser.add_argument("markdown", nargs="+", type=Path)
    args = parser.parse_args()

    repo_root = Path.cwd().resolve()
    checked = 0
    broken: list[str] = []
    missing_sources: list[str] = []

    for relative_file in args.markdown:
        source = (repo_root / relative_file).resolve()
        if not source.is_file():
            missing_sources.append(str(relative_file))
            continue

        checked += 1
        targets = extract_targets(source.read_text(encoding="utf-8"))
        for target in targets:
            resolved = local_target(source, target, repo_root)
            if resolved is None:
                continue
            try:
                resolved.relative_to(repo_root)
            except ValueError:
                broken.append(f"{relative_file}: link escapes repository root: {target}")
                continue
            if not resolved.exists():
                broken.append(f"{relative_file}: missing target: {target}")

    args.report.parent.mkdir(parents=True, exist_ok=True)
    lines = [
        "CostOfStaying Markdown Link Audit",
        f"Documents checked: {checked}",
        f"Missing source documents: {len(missing_sources)}",
        f"Broken local links: {len(broken)}",
        "",
    ]
    lines.extend(f"MISSING SOURCE: {item}" for item in missing_sources)
    lines.extend(f"BROKEN: {item}" for item in broken)
    if not broken and not missing_sources:
        lines.append("PASS: all checked local Markdown link targets exist.")

    args.report.write_text("\n".join(lines) + "\n", encoding="utf-8")
    print("\n".join(lines))

    return 1 if broken or missing_sources else 0


if __name__ == "__main__":
    raise SystemExit(main())
