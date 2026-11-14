# ميثاق المشروع — v2.1
**الحالة: PROPOSED | المرحلة: Pre-production → Domain Core منفذ كـ PROPOSED (ADR-005) | آخر مراجعة: 2026-11-14**

## 1. الرؤية
The Cost of Staying لعبة بقاء نفسي جماعية تتمحور حول أن البقاء له ثمن: تتآكل Vitality مع الوقت، ويمكن نقل المعاناة إلى لاعب آخر لتأجيل الانهيار مقابل تراكم Betrayal، ولا ينتهي دور اللاعب عند الموت بل ينتقل إلى Spirit.

## 2. ركائز التجربة
1. ضغط بقاء متصاعد يمكن للاعب فهمه وقراءته.
2. نقل المعاناة قرار تكتيكي كامل النقل وله عاقبة على المصدر.
3. Spirit دور لاحق للموت، محدود ومحايد وله هدف مستقل.
4. الخادم يحسم حالة المباراة والنتائج الحساسة.
5. اختبار MVP صغير قبل إضافة المحتوى أو زيادة عدد اللاعبين.

## 3. نطاق المنتج الأولي
- المنصة الأولى: Android.
- النمط: Multiplayer Psychological Survival.
- الشكل البصري المقترح: 3D Dark Low-Poly.
- MVP: أربعة لاعبين، خريطة واحدة، مباراة كاملة.
- مدة الجولة المستهدفة للتجربة: 10–16 دقيقة؛ حد زمني أولي مقترح 20 دقيقة.
- المنظور، أدوات التحكم النهائية، والمتطلبات الدنيا للأجهزة: TBD وتحتاج اختباراً.

## 4. الأنظمة داخل MVP
Vitality/Collapse، Debuffs، Suffering Transfer، Betrayal، Combat أساسي، Spirit/Echoes، Endgame، Android Client، وLinux Dedicated Server تجريبي بعد اعتماد التكدس.

## 5. خارج النطاق
متجر، اقتصاد دائم، Meta progression، خرائط متعددة، Voice Chat، iOS production، دعم 8–16 لاعباً، ومكافحة غش تجارية. لا تضاف إلا بقرار نطاق جديد.

## 6. المبادئ الهندسية
- أبسط تقنية تحقق متطلبات المرحلة.
- لا تحسين بلا profiler evidence وقياس متكرر.
- العميل يرسل Intent؛ الخادم يتحقق ويقرر الحالة (ADR-002).
- افصل Domain Core عن Unity وFishNet قدر الإمكان — ✅ منفذ في `src/CostOfStaying.Domain` (ADR-005, `DOMAIN_CORE.md`).
- اربط المتطلبات بالضوابط والاختبارات والأدلة (`REQUIREMENTS_TRACEABILITY.md`).
- وثّق القرارات المعمارية في ADR مع بدائل ومخاطر وخطة تراجع.

## 7. حالة المشروع
المرحلة لا تزال Pre-production تصميمياً، لكن **النواة المجردة وCI منفذان كـ PROPOSED** على فرع `arena/4bfc0882-costofstaying` (انظر `architecture/DOMAIN_CORE.md` و`governance/SOURCES_AND_REFERENCES.md`):
- `src/CostOfStaying.Domain` + `tests/` + `tools/balance_simulator` + `tools/playtest` + `ci.yml` (بوابة 3 في `DELIVERY_PLAN.md`).
- لا Unity project ولا Dedicated Server شبكي ولا Android build بعد؛ لا بند VERIFIED قبل CI أخضر على GitHub بأدلة محفوظة.
يجب تحديث الحالة فقط عند وجود commits وأدلة قابلة للفحص (مبدأ 5).

## 8. بوابات الانتقال إلى الإنتاج
1. اختبار قواعد RULE-01..05 وتحديث ADR-004 بناءً على نتائج موثقة — جلسات محاكاة ≥10 متوفرة (SIMULATED)، جلسات بشرية مطلوبة.
2. حسم التكدس التقني ونسخ الأدوات المثبتة (ADR-001).
3. بناء Domain Core واختباراته — ✅ منفذ كـ PROPOSED (ADR-005).
4. إثبات اتصال شبكي وخادم مستقل.
5. إكمال Vertical Slice لأربعة لاعبين.
6. اجتياز بوابات الأمن والأداء والاستقرار والقبول.

## المراجع الموحدة
- التوثيق: `governance/DOCUMENT_GOVERNANCE.md` + `governance/SOURCES_AND_REFERENCES.md`
- الهيكل: `REPOSITORY_STRUCTURE.md`
