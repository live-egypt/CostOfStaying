# Workflows

## Active CI
- **`ci.yml`** — Active quality gate for this branch. Runs on `push`/`pull_request`:
  `tools/audit.py` → `dotnet restore/build/test` → Python parity → simulator smoke → playtest 10 sessions → secrets heuristic → upload artifacts.
  This is the documented quality gate (ADR-005, `docs/architecture/DOMAIN_CORE.md`).

## Vendor templates (inactive)
The files below are inactive vendor templates. They run only with `workflow_dispatch`:

- `codeql.yml`, `contrast-scan.yml`, `debricked.yml`, `frogbot-scan-and-fix.yml`, `mobsf.yml`, `nowsecure.yml`, `securitycodescan.yml`

They are not a game CI pipeline, not a quality gate, and not evidence of a successful scan. Do not enable `push`/`pull_request` triggers until a real artifact exists, secrets are stored outside Git, and a documented decision names the tool.

For secrets handling see `docs/security/SECRETS_MANAGEMENT.md`.
