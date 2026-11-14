# تتبع المتطلبات — v2.1
**الحالة: PROPOSED | المعرفات أولية ولا تثبت وجود اختبارات**

| Requirement ID | المتطلب | Control ID | Test ID المقترح | الدليل المطلوب |
|---|---|---|---|---|
| GAME-001 | Vitality خادمية والموت عند الصفر فقط | GAME-CTRL-001 | VitalityTests, CollapseTests | تقرير Unit من `tests/CostOfStaying.Domain.Tests` |
| COL-001 | مؤقت Collapse؛ 15 ثانية لثلاثة أحياء قيمة أولية فقط | COL-CTRL-001 | CollapseTests | تقرير Unit + `tools/balance_simulator/sweep.py` |
| TRF-001 | نقل Debuff ذرياً دون Copy | TRF-CTRL-001 | TransferTests | تقرير Integration — معاملة ذرية + idempotency |
| BET-001 | نقطة Betrayal بعد النجاح فقط؛ بلا عتبات | BET-CTRL-001 | TransferTests, SecurityTests | تقرير Unit — لا يزداد عند الفشل |
| SP-001 | الروح لا تغير حالة الأحياء | SP-CTRL-001 | SpiritTests | تقرير Integration — Spirit محايد |
| END-001 | النتائج الخمس يقررها الخادم بما فيها Draw | END-CTRL-001 | VoteAndEndgameTests | سجل مباراة `docs/quality/playtest-logs/` |
| NET-001 | العميل يرسل Intent فقط | TRUST-CTRL-001 | SecurityTests (`Intent.cs`) | تقرير أمني — مسافة خادمية + replay |
| PERF-001 | قياس Android بعد اعتماد ميزانية؛ لا ميزانية الآن | PERF-CTRL-001 | PERF-ANDROID-001 | تقرير جهاز فعلي — لا يُدّعى قبل Baseline |

لا تمنح حالة VERIFIED لأن الصف موجود. كل دليل يحتاج commit وبيئة ونتيجة فعلية.
