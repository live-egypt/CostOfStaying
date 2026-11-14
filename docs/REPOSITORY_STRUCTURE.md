# هيكل المستودع — v1.0
**الحالة: PROPOSED | مرجع تنظيمي | آخر مراجعة: 2026-11-14**

```
CostOfStaying/
├── .github/
│   ├── workflows/
│   │   ├── ci.yml                # ✅ بوابة الجودة النشطة (ADR-005) — push/PR
│   │   ├── codeql.yml …          # ⏸ قوالب موردين — workflow_dispatch فقط
│   │   └── README.md             # شرح أي workflow نشط وأيها قالب
│   ├── ISSUE_TEMPLATE/           # قوالب البلاغات
│   └── PULL_REQUEST_TEMPLATE.md
├── docs/                         # الحوكمة والتوثيق (v2.1)
│   ├── 00_PROJECT_CHARTER.md … GLOSSARY.md   # طبقة المنتج
│   ├── systems/                  # تفصيل القواعد (لا يخالف SRC-RULES)
│   ├── architecture/             # كيف يُبنى (DOMAIN_CORE.md جديد)
│   ├── security/                 # ضوابط وتهديدات
│   ├── quality/                  # اختبار وتوازن وأداء + playtest-logs/
│   ├── operations/               # بناء وتشغيل
│   ├── governance/               # حوكمة + ADR + SOURCES_AND_REFERENCES
│   ├── roadmap/                  # نطاق وقبول وتسليم
│   ├── README.md                 # الفهرس البشري
│   └── REPOSITORY_STRUCTURE.md   # هذا الملف
├── src/
│   └── CostOfStaying.Domain/     # النواة المجردة netstandard2.1 (ADR-005)
│       ├── Match.cs / Player.cs / Debuff.cs / Clock.cs / MatchConfig.cs
│       └── Intent.cs / DomainEvent.cs / Enums.cs
├── tests/
│   └── CostOfStaying.Domain.Tests/  # NUnit — 6 suites
├── tools/
│   ├── audit.py                  # تدقيق الروابط والكتالوج والحالات
│   ├── balance_simulator/        # محاكي Python (presets, simulate, sweep, parity)
│   └── playtest/                 # مولد جلسات ≥10 موثقة
├── CostOfStaying.sln
├── global.json                   # تثبيت SDK
├── Directory.Packages.props      # تثبيت إصدارات NuGet (مرة واحدة)
├── .editorconfig / .gitattributes
├── .gitignore
├── CONTRIBUTING.md / CODE_OF_CONDUCT.md / SECURITY.md / LICENSE
└── README.md
```

## الطبقات الحاكمة

| المسار | يمثل | مصدر الحقيقة | يُعدّل مع |
|---|---|---|---|
| `docs/02_GAME_RULES_SPECIFICATION.md` | قواعد اللعب | ADR-004 | `docs/systems/*` + `ADR-004` + `MatchConfig.cs` |
| `src/CostOfStaying.Domain/` | منطق اللعبة المجرد | `DOMAIN_CORE.md` | `tests/` + `tools/balance_simulator/presets.json` |
| `tools/` | أدوات التحقق | `SOURCES_AND_REFERENCES.md` | نفس commit كود النواة |
| `.github/workflows/ci.yml` | بوابة الجودة | ADR-005 | `tools/audit.py` + `dotnet test` |

## قواعد الإضافة

1. **وثيقة جديدة في `docs/`** → أضفها في `DOCUMENT_GOVERNANCE.md` الكتالوج (§6) **و** `docs/README.md` الفهرس في **نفس الالتزام** (§5).
2. **كود جديد في `src/`** → حدّث `DOMAIN_CORE.md` و`REQUIREMENTS_TRACEABILITY.md` و`SOURCES_AND_REFERENCES.md` إن غيّر رقماً.
3. **أداة جديدة في `tools/`** → أضف `README.md` واختبار parity إن كررت منطق Domain.
4. **workflow جديد** → وثّقه في `.github/workflows/README.md` ولا تفعّل `push` بلا ADR يسمي الأداة.

## البناء والتحقق المحلي

```bash
# الوثائق
python3 tools/audit.py

# النواة
dotnet restore && dotnet build -c Release
dotnet test -c Release --collect:"XPlat Code Coverage"

# المحاكاة
python3 tools/balance_simulator/test_parity.py
python3 tools/balance_simulator/simulate.py --preset default --seed 42 --json
python3 tools/playtest/run_sessions.py --sessions 10 --preset default --output docs/quality/playtest-logs

# الأسرار (heuristic)
grep -R -nE "(AKIA[0-9A-Z]{16}|ghp_)" --exclude-dir=.git .
```

CI يشغل كل هذا على `ubuntu-latest` عند كل `push`/`pull_request`.

## الإصدارات والفروع

- `main` — خط أساس التوثيق v2.1
- `arena/4bfc0882-costofstaying` — تنفيذ Domain Core + CI (PROPOSED)
- لا ترفع حالة `IMPLEMENTED/VERIFIED` إلا بـ commit SHA + artifact hash + تقرير CI أخضر.
