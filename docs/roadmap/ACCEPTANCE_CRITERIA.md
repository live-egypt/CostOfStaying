# معايير قبول MVP — v2.1
**الحالة: PROPOSED | لا يوجد بند VERIFIED | ربط التنفيذ في REQUIREMENTS_TRACEABILITY.md**

لا يعد MVP مكتملاً حتى تتحقق البنود التالية على build محدد مع أدلة محفوظة.

## Gameplay
- مباراة لأربعة لاعبين تبدأ وتنتهي دون تدخل المطور.
- Vitality تبدأ عند 100 وتتآكل وفق معدل خادمي؛ الموت عند الصفر. Collapse لا يقتل فوراً.
- Collapse يزيد التآكل ويطبق Debuff. القيمة المسماة فقط هي 15 ثانية لثلاثة أحياء كقيمة أولية.
- Transfer ينقل آخر Debuff كاملاً (Move لا Copy) بمعاملة ذرية.
- Betrayal يزداد نقطة بعد النقل الناجح فقط ولا يمنح نقاط فوز. عتبات الأثر ليست معيار قبول حتى تُسجّل في قاعدة.
- Spirit محايد ويجمع Echo Fragments ولا يؤثر مباشرة في صحة الأحياء أو مواقعهم.
- تصويت ثلاثة أحياء يتطلب إجماعاً. لا صحة إضافية عند القبول.
- النتائج الخمس واضحة ومتمايزة: Last Survivor وCollective Surrender وTime Limit وDraw وMutual Collapse.

## Networking
- الخادم يملك الحالة ويحسب النتائج الحساسة. العميل يرسل Intent فقط.
- اتصال لاعبين على جهازين مختلفين ثم اختبار أربعة لاعبين.
- معالجة disconnect/reconnect وفق سياسة موثقة ومعتمدة قبل Alpha. لا سياسة معتمدة الآن.
- لا يسبب replay أو RPC spam تغييراً غير مشروع للحالة.

## Stability and Performance
- لا crash أو فساد حالة معروف في سيناريو القبول.
- أداء Android مقاس على أجهزة مستهدفة بعد تحديدها. لا ميزانية معتمدة قبل baseline.
- أداء الخادم مقاس عند أربعة لاعبين مع هامش CPU/RAM موثق.

## Security
- لا أسرار في المستودع أو العميل أو artifacts.
- لا Critical/High مفتوحة دون قبول مخاطر مكتوب.
- اختبارات regression للأخطاء الأمنية المكتشفة.
- سجلات الحوادث لا تحتوي أسراراً أو بيانات شخصية غير لازمة.

## Delivery Evidence

لكل بند: Requirement ID وTest ID وcommit SHA وartifact hash وبيئة الاختبار والنتيجة وتاريخ التحقق. لا VERIFIED بدون دليل.

| البند | Requirement | الدليل الحالي (PROPOSED) | المتبقي |
|---|---|---|---|
| Vitality/Collapse | GAME-001/COL-001 | `VitalityTests`, `CollapseTests` + محاكاة | CI أخضر + Playtest بشري |
| Transfer | TRF-001 | `TransferTests` (10 حالات) | اختبار شبكة حقيقي |
| Betrayal | BET-001 | `TransferTests` + `SecurityTests` | بوابة 4 |
| Spirit | SP-001 | `SpiritTests` | اختبار تفاعلي |
| Endgame 5 outcomes | END-001 | `VoteAndEndgameTests` + `playtest-logs/` | جلسات بشرية |
| Server Authority | NET-001 | `SecurityTests` + `Intent.cs` | Dedicated Server |

انظر `REQUIREMENTS_TRACEABILITY.md` و`architecture/DOMAIN_CORE.md` للتفصيل.
