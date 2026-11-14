# تتبع المتطلبات — v2.1
**الحالة: PROPOSED | المعرفات مرتبطة بتنفيذ فعلي (ADR-005) — لا VERIFIED قبل CI أخضر**

| Requirement ID | المتطلب | Control ID | Test ID / التنفيذ | الدليل المطلوب | المصدر الموحد |
|---|---|---|---|---|---|
| GAME-001 | Vitality خادمية والموت عند الصفر فقط | GAME-CTRL-001 | `VitalityTests.cs` + `CollapseTests.cs` | تقرير Unit `dotnet test` (CI artifact) | SRC-RULES RULE-01 |
| COL-001 | مؤقت Collapse؛ 15 ثانية لثلاثة أحياء قيمة أولية فقط | COL-CTRL-001 | `CollapseTests.cs` + `sweep.py` | تقرير Unit + توزيع محاكاة | SRC-RULES RULE-01/03 |
| TRF-001 | نقل Debuff ذرياً دون Copy | TRF-CTRL-001 | `TransferTests.cs` (10 حالات) | تقرير Integration — ذري + idempotency | SRC-RULES RULE-02 |
| BET-001 | نقطة Betrayal بعد النجاح فقط؛ بلا عتبات | BET-CTRL-001 | `TransferTests.cs` + `SecurityTests.cs` | تقرير Unit — لا يزداد عند الفشل/replay | SRC-RULES RULE-02 |
| SP-001 | الروح لا تغير حالة الأحياء | SP-CTRL-001 | `SpiritTests.cs` | تقرير Integration — Spirit محايد | SRC-RULES RULE-04 |
| END-001 | النتائج الخمس يقررها الخادم بما فيها Draw | END-CTRL-001 | `VoteAndEndgameTests.cs` | سجل مباراة `playtest-logs/*.json` + `report.md` | SRC-RULES RULE-05 |
| NET-001 | العميل يرسل Intent فقط | TRUST-CTRL-001 | `SecurityTests.cs` (`Intent.cs`, `Match.TryTransfer`) | تقرير أمني — مسافة خادمية + replay | SRC-ADR-002 |
| PERF-001 | قياس Android بعد اعتماد ميزانية؛ لا ميزانية الآن | PERF-CTRL-001 | — | تقرير جهاز فعلي — لا يُدّعى قبل Baseline | SRC-ADR-001 |

> **الحالة الحالية:** كل المتطلبات لها تنفيذ **PROPOSED** (كود + اختبارات) لكن **لا VERIFIED** — يتطلب `ci.yml` أخضر على GitHub (SHA + بيئة + artifact hash).

## كيف يُثبت بند
1. شغّل `dotnet test` + `python3 tools/playtest/run_sessions.py --sessions 10` محلياً أو عبر CI.
2. احفظ artifacts: `TestResults/`, `playtest-logs/summary-*.csv`, `report-*.md`.
3. سجل في `ACCEPTANCE_CRITERIA.md`: Requirement ID → Test ID → commit SHA → artifact hash → بيئة → نتيجة → تاريخ.

## المراجع الموحدة
- المصادر: `governance/SOURCES_AND_REFERENCES.md`
- التنفيذ: `architecture/DOMAIN_CORE.md` + `REPOSITORY_STRUCTURE.md`
- الاختبارات: `quality/TEST_STRATEGY.md` + `quality/SECURITY_TEST_PLAN.md`
