# لقطة حالة المشروع — 2026-11-12

**المشروع:** Cost of Staying (ثمن البقاء)  
**الإصدار التوثيقي:** v2.1  
**الحالة:** Pre-production  
**الغرض:** نقطة استئناف مستقلة عند نقل العمل إلى مشروع ChatGPT جديد.

## 1. تعريف المنتج

لعبة بقاء نفسي جماعية Multiplayer Psychological Survival. الفكرة المركزية: البقاء له ثمن؛ تتآكل Vitality بمرور الوقت، ويمكن نقل المعاناة إلى لاعب آخر لتأجيل الانهيار مقابل عاقبة Betrayal، وينتقل اللاعب بعد الموت إلى دور Spirit.

## 2. النطاق الحالي

| البند | الحالة المسجلة |
|---|---|
| المنصة ذات الأولوية | Android |
| MVP | 4 لاعبين، خريطة واحدة، مباراة مكتملة |
| الشكل البصري المقترح | 3D Dark Low-Poly |
| المرحلة | Pre-production |
| القواعد | PROPOSED FOR PLAYTEST ما لم يسجل ADR حالة مختلفة |
| الكود/Unity/الخادم | لا يوجد على `main` ضمن لقطة 2026-11-12 |
| الاختبارات | README على `main` يصرح بعدم وجود اختبارات لعبة منفذة |
| الجاهزية للإنتاج | لم تصدر |

## 3. التكدس والقرار المعماري

التكدس المفترض في تعليمات المشروع: Unity 6.3 LTS، C#، FishNet 4.7.4R، Linux Dedicated Server. هذا افتراض تشغيل/خطة عمل، وليس إثباتاً بأن هذه الإصدارات مثبتة أو مختبرة في المشروع.

| ADR | الموضوع | الحالة على main | حد الدليل |
|---|---|---|---|
| ADR-001 | Unity/URP/C#/FishNet/Linux | PROPOSED | يحتاج prototype واختبار توافق |
| ADR-002 | Server Authority | APPROVED كمبدأ أمني | التنفيذ NOT IMPLEMENTED |
| ADR-003 | Backend/Hosting | PROPOSED | لا مزود إنتاج مختار |
| ADR-004 | قواعد اللعب الخمس | PROPOSED FOR PLAYTEST | 10 جلسات لعب موثقة على الأقل قبل الاعتماد |

## 4. خط الأساس لقواعد اللعب

بحسب `docs/02_GAME_RULES_SPECIFICATION.md` وADR-004 على `main`:

1. الموت عند وصول Vitality إلى الصفر؛ Collapse يسرّع التآكل ولا يقتل فوراً.
2. نقل Debuff هو Move كامل وليس Copy، والعملية ذرية؛ النقل الناجح فقط يزيد Betrayal.
3. Collective Surrender عند ثلاثة لاعبين Alive يتطلب إجماع 3/3 خلال 20 ثانية؛ الامتناع/الانقطاع/انتهاء المهلة = No.
4. Spirit شاهد محايد يجمع Echo Fragments ويرسل تلميحاً عاماً؛ Energy غير معتمد.
5. خمس نتائج متميزة: Last Survivor، Collective Surrender، Time Limit، Draw، Mutual Collapse.

جميعها خط أساس للاختبار؛ لا تحولها إلى APPROVED نهائياً دون بوابة ADR-004.

## 5. مبادئ الهندسة الحاكمة

- الخادم هو مصدر الحقيقة النهائي؛ العميل يرسل Intent فقط.
- Core منطق C# نقي بلا UnityEngine أو FishNet؛ طبقتا Net وUnity أغلفة رفيعة.
- أي تغيير حساس في Vitality أو Betrayal أو Match State يُتحقق منه ويُطبق على الخادم فقط.
- لا ادعاء IMPLEMENTED أو VERIFIED دون كود قابل للتنفيذ واختبار قابل لإعادة الإنتاج.
- كل وظيفة منطقية في Core تحتاج NUnit test.
- لا API غير مؤكدة: استخدم “needs verification”.
- لا `Update()` في كود الشبكة؛ استخدم أحداث tick من FishNet بعد التحقق من API.
- ADR قبل أي قرار معماري رئيسي.
- لا تعمل على `main`؛ feature branch ثم commit ثم PR.
- لا تغيير صامت في النطاق أو حالة القرار.

## 6. حالة التوثيق والحوكمة

المستودع `main` يحتوي على ميثاق، GDD، قواعد اللعبة، مواصفات الأنظمة، معمارية، أمن، جودة، عمليات، roadmap، حوكمة، وأربعة ADRs. تفاصيل المسارات محفوظة في [Repository Snapshot](GITHUB_REPOSITORY_SNAPSHOT_2026-11-12.md).

الحالات المعتمدة في الحوكمة مستقلة:
- Decision Status: PROPOSED / APPROVED / REJECTED / SUPERSEDED.
- Evidence Maturity: DOCUMENTED / REVIEWED / IMPLEMENTED / TESTED / VERIFIED / OPERATIONALLY PROVEN.
- Work State: OPEN / PARTIAL / TBD / PENDING / DEFERRED.
لا تستبدل محوراً بآخر.

## 7. مسار العمل عند لقطة النقل

- M1 — Source & Provenance Inventory/Recovery: PARTIAL / OPEN.
- M2 — Decision and Conflict Traceability: NOT STARTED كمرحلة مستقلة.
- M3 — Reconciliation and Governance Integration: NOT STARTED كمرحلة مستقلة.
- M4 — Shared Context across Notion/GitHub/Linear/tldraw: NOT STARTED كمرحلة مستقلة.
- DOC-PACKAGE-01: OPEN؛ لا توجد موافقة نهائية على المسار المرجعي بين PR #8/#9/#11.
- Risk Register وTechnical Debt Register: مؤجلان لحين توفيق المصادر والمعرفات.
- Code Gate وFinal Readiness: لم يفتحا/لم يصدر حكم نهائي وفق سجل الاستئناف المتاح.

**ملاحظة تسلسل:** توجد فروع PR معنونة A5/PDR-012 إلى PDR-014، بينما سجل استئناف سابق وصف A5 بأنه لم يفتح رسمياً. يُحفظ هذا كاختلاف بين سجل المحادثة ووجود وثائق مراجعة على فروع غير مدمجة؛ لا يُسوّى بالاستنتاج.

## 8. مراقبة GitHub الدورية

مهمة ChatGPT سابقة تم إنشاؤها:
- Automation ID: `6af5e16db62881918f79a5d0165e4671`.
- الاسم: متابعة GitHub لمشروع Cost of Staying.
- الموعد: كل خميس 09:00 بتوقيت Africa/Tripoli.
- النمط: قراءة فقط؛ تراقب main والفروع وPR #5–#11 و#18–#20 وأي PR جديد؛ لا تعدّل المستودع ولا تبدأ M2.
- هذه المهمة مرتبطة ببيئة ChatGPT، وليست GitHub Action. يجب التحقق من استمرارها بعد حذف المشروع القديم.

## 9. مرجع هذه اللقطة

- Repository: https://github.com/live-egypt/CostOfStaying
- Main commit: `2c68f748360b78a8bb4d86b8bf693b4c2ce07216`
- Last push recorded at snapshot: `2026-11-12T08:31:42Z`
- Repository was public, not archived, default branch `main`.
