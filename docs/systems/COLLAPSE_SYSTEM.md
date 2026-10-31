# Collapse System — v2.1
**الحالة: PROPOSED FOR PLAYTEST**

يمتلك كل لاعب Alive مؤقت Collapse مستقلاً وVitality تبدأ عند 100. تتآكل Vitality تدريجياً بمعدل خادمي، ويزيد كل Collapse معدل التآكل ويطبق Debuff. الموت عند Vitality = 0.

الفترات الموروثة أولياً: 8+ أحياء 30ث، 6–7 25ث، 4–5 20ث، 3 15ث، 2 12ث. هذه قيم اختبار وليست معتمدة.

## سياسة إعادة الجدولة
عند تغير عدد الأحياء، حافظ على نسبة الوقت المتبقي:
RemainingRatio = clamp((NextCollapseAt - Now) / OldInterval, 0, 1)
NewNextCollapseAt = Now + RemainingRatio * NewInterval

## Vitality
Vitality(t+dt) = max(0, Vitality(t) - DecayRate * dt)
Collapse يزيد DecayRate ويضيف Debuff. معدلات التآكل والزيادة والحد الأقصى تحتاج tuning؛ لا تثبت قبل Playtest. إذا بلغت Vitality صفراً، ينفذ الخادم انتقال Alive → Dead/Spirit مرة واحدة.

## Debuffs
لكل Debuff تعريف للقيمة، التكديس، الحد الأقصى، المدة، قابلية النقل، وتأثيره على DecayRate أو خصائص اللاعب. حد أولي مقترح لثلاثة أعباء نشطة؛ لا يقبل عبء جديد إذا لم تتوفر سعة.

## القبول
مؤقت مستقل؛ إعادة جدولة صحيحة؛ لا تكرار للحدث؛ لا client clock؛ اختبار تآكل Vitality؛ ومعالجة آمنة لتزامن الموت مع انتهاء المؤقت.
