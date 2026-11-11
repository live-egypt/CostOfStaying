# Game Design Document — v2.1
**الحالة: PROPOSED FOR PLAYTEST**

## الملخص
The Cost of Staying لعبة بقاء نفسي جماعية. كل ثانية تستنزف Vitality، والانهيار يزيد التآكل ويترك Debuffs. يستطيع اللاعب نقل آخر عبء قابل للنقل إلى لاعب آخر، لكنه يراكم Betrayal. الموت لا ينهي المشاركة؛ بل يحول اللاعب إلى Spirit محايد له هدف مستقل محدود.

## نطاق اللعبة
- النوع: Multiplayer Psychological Survival.
- المنصة الأولى: Android.
- النمط البصري: 3D Dark Low-Poly.
- MVP: أربعة لاعبين، خريطة واحدة، مباراة كاملة.
- مدة الجولة المستهدفة: 10–16 دقيقة، والحد الأقصى الأولي 20 دقيقة وفق RULE-05.
- المنظور: TBD.

## أعمدة اللعب
1. ضغط بقاء متزايد وواضح عبر Vitality وCollapse.
2. نقل المعاناة قرار تكتيكي كامل النقل وله كلفة Betrayal.
3. Spirit دور لاحق للموت، مستقل ومحايد.
4. Dedicated Server يحسم النتائج الحساسة.
5. نهاية مفهومة وفق RULE-05: Last Survivor أو Collective Surrender أو Time Limit أو Draw أو Mutual Collapse.

## الحلقة الأساسية
استكشف → راقب Vitality وموعد Collapse → تعامل مع Debuff → انقل آخر عبء أو احتفظ به → واجه أو تجنّب → عند الموت العب دور Spirit → شاهد نتيجة واضحة من النتائج الخمس.

## النهاية
عند بقاء ثلاثة أحياء يفتح تصويت Collective Surrender لمدة 20 ثانية. الإجماع ينهي المبارة بلا فائز فردي. أي No أو امتناع يعيد اللعب. بعد ذلك تنطبق RULE-05: آخر حي، أو أعلى Vitality عند حد 20 دقيقة، أو Draw عند التعادل، أو Mutual Collapse إذا مات الجميع.

## محتوى MVP
خريطة واحدة، أربعة لاعبين، Vitality/Collapse، Debuffs محدودة، نقل ذري، Betrayal بدون عتبات معتمدة، Combat أساسي بأرقام غير ملزمة، Spirit/Echoes، النتائج الخمس، وعميل Android وخادم Linux بعد prototype.

## القرارات
الخط الأساسي في [مواصفات قواعد اللعبة](02_GAME_RULES_SPECIFICATION.md). هذه الوثيقة لا تستبدله. أرقام التوازن ما زالت بحاجة إلى Playtest.
