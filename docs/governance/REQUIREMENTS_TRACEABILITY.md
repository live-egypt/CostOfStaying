# تتبع المتطلبات — v2.1
**الحالة: PROPOSED | المعرفات أولية ولا تثبت وجود اختبارات**

| Requirement ID | المتطلب | Control ID | Test ID المقترح | الدليل المطلوب |
|---|---|---|---|---|
| GAME-001 | Vitality خادمية والموت عند الصفر فقط | GAME-CTRL-001 | UNIT-GAME-001 | تقرير Unit |
| COL-001 | مؤقت Collapse؛ 15 ثانية لثلاثة أحياء قيمة أولية فقط | COL-CTRL-001 | UNIT-COL-001 | تقرير Unit |
| TRF-001 | نقل Debuff ذرياً دون Copy | TRF-CTRL-001 | INT-TRF-001 | تقرير Integration |
| BET-001 | نقطة Betrayal بعد النجاح فقط؛ بلا عتبات | BET-CTRL-001 | UNIT-BET-001 | تقرير Unit |
| SP-001 | الروح لا تغير حالة الأحياء | SP-CTRL-001 | INT-SP-001 | تقرير Integration |
| END-001 | النتائج الخمس يقررها الخادم بما فيها Draw | END-CTRL-001 | NET-END-001 | سجل مباراة |
| NET-001 | العميل يرسل Intent فقط | TRUST-CTRL-001 | SEC-NET-001 | تقرير أمني |
| PERF-001 | قياس Android بعد اعتماد ميزانية؛ لا ميزانية الآن | PERF-CTRL-001 | PERF-ANDROID-001 | تقرير جهاز فعلي |

لا تمنح حالة VERIFIED لأن الصف موجود. كل دليل يحتاج commit وبيئة ونتيجة فعلية.
