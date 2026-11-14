#!/usr/bin/env python3
"""Audit CostOfStaying docs: links, catalog, status headers, forbidden claims."""
import os, re, sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
DOCS = os.path.join(ROOT, "docs")

issues = []
warnings = []

def rel(p):
    return os.path.relpath(p, ROOT)

# 1. Collect all md files under docs/
all_md = []
for dirpath, _, files in os.walk(DOCS):
    for f in files:
        if f.endswith(".md"):
            all_md.append(os.path.join(dirpath, f))
all_md.sort()

# 2. Check relative links in every md file (docs + root README)
link_re = re.compile(r'\[[^\]]*\]\(([^)]+)\)')
for path in all_md + [os.path.join(ROOT, "README.md")]:
    with open(path, encoding="utf-8") as fh:
        content = fh.read()
    for m in link_re.finditer(content):
        target = m.group(1).strip()
        if target.startswith(("http://", "https://", "mailto:", "#")):
            continue
        target = target.split("#")[0]
        if not target:
            continue
        resolved = os.path.normpath(os.path.join(os.path.dirname(path), target))
        if not os.path.exists(resolved):
            issues.append(f"BROKEN LINK: {rel(path)} -> {target}")

# 3. Catalog completeness: every docs/*.md must appear in DOCUMENT_GOVERNANCE catalog
# Evidence logs under quality/playtest-logs are not governed docs.
EXCLUDED_PREFIXES = ("quality/playtest-logs/", "quality/evidence/")
gov_path = os.path.join(DOCS, "governance", "DOCUMENT_GOVERNANCE.md")
with open(gov_path, encoding="utf-8") as fh:
    gov = fh.read()
for path in all_md:
    r = rel(path).replace("docs/", "", 1)
    if r.startswith(EXCLUDED_PREFIXES):
        continue
    # catalog uses paths relative to docs/
    if r not in gov:
        issues.append(f"NOT IN CATALOG: {r}")

# 4. Index completeness: every docs/*.md must be linked from docs/README.md
with open(os.path.join(DOCS, "README.md"), encoding="utf-8") as fh:
    index = fh.read()
for path in all_md:
    if os.path.basename(path) == "README.md" and os.path.dirname(path) == DOCS:
        continue
    r = rel(path).replace("docs/", "", 1)
    if r.startswith(EXCLUDED_PREFIXES):
        continue
    if r not in index:
        issues.append(f"NOT IN INDEX: {r}")

# 5. Status header check: every doc (except README index) should declare a status
status_re = re.compile(r'\*\*(الحالة|Status):', re.IGNORECASE)
for path in all_md:
    if os.path.basename(path) == "README.md":
        continue
    if rel(path).replace("docs/", "", 1).startswith(EXCLUDED_PREFIXES):
        continue
    with open(path, encoding="utf-8") as fh:
        head = fh.read(600)
    if not status_re.search(head):
        warnings.append(f"NO STATUS HEADER: {rel(path)}")

# 6. Forbidden claims: IMPLEMENTED/VERIFIED used as current status without evidence
# The header format is **Status: VALUE | optional note** — only the value before |
# counts as the declared status; notes mentioning VERIFIED do not count.
for path in all_md:
    with open(path, encoding="utf-8") as fh:
        content = fh.read()
    head = content[:600]
    m = re.search(r'\*\*(الحالة|Status):\s*([^*]+)\*\*', head)
    if m:
        raw = m.group(2).strip()
        # Declared status is the part before '|' (or whole string if no pipe)
        declared = raw.split("|")[0].strip().upper()
        if re.search(r'\b(IMPLEMENTED|VERIFIED)\b', declared):
            issues.append(f"UNSUBSTANTIATED STATUS: {rel(path)} -> {raw}")

# 7. ADR consistency: DECISIONS_LOG statuses vs ADR file headers
dec_path = os.path.join(DOCS, "governance", "DECISIONS_LOG.md")
with open(dec_path, encoding="utf-8") as fh:
    dec = fh.read()
adr_dir = os.path.join(DOCS, "governance", "adr")
for f in sorted(os.listdir(adr_dir)):
    if not f.endswith(".md"):
        continue
    with open(os.path.join(adr_dir, f), encoding="utf-8") as fh:
        head = fh.read(600)
    m = re.search(r'\*\*(الحالة|Status):\s*([^*]+)\*\*', head)
    adr_id = f.split("-")[0] + "-" + f.split("-")[1]
    if m and adr_id not in dec:
        issues.append(f"ADR NOT IN DECISIONS LOG: {adr_id}")

print(f"Docs scanned: {len(all_md)}")
print(f"Issues: {len(issues)}")
for i in issues:
    print("  [ISSUE] " + i)
print(f"Warnings: {len(warnings)}")
for w in warnings:
    print("  [WARN] " + w)
sys.exit(1 if issues else 0)
