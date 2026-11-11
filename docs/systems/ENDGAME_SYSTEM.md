# Endgame System — v2.1
**الحالة: PROPOSED FOR PLAYTEST | يتبع RULE-03 وRULE-05**

عند بقاء ثلاثة لاعبين Alive، يفتح الخادم تصويت Collective Surrender لمدة 20 ثانية، مرة واحدة في MVP.

## نتيجة التصويت
- الإجماع 3/3 Yes ينهي المبارة بـ Collective Surrender ولا فائز فردي.
- No أو الامتناع أو disconnect أو timeout = رفض، وتعود المبارة إلى Running.
- عند الرفض تستمر فترة Collapse لثلاثة أحياء عند 15 ثانية كقيمة أولية فقط.
- لا تُمنح صحة بعد قبول التصويت.

## النتائج القابلة للتمييز
هذه القائمة هي RULE-05 كما هي، لا اختصاراً:
1. Last Survivor: آخر لاعب حي.
2. Collective Surrender: نهاية جماعية بلا فائز فردي.
3. Time Limit: عند 20 دقيقة يفوز أعلى Vitality إذا لم يكن تعادلاً.
4. Draw: تساوي أعلى Vitality عند حد الوقت. ليست Time Limit وليست Mutual Collapse.
5. Mutual Collapse: لا فائز حي. نتيجة الأرواح منفصلة ولا تغيّر هذه النتيجة.

كل تصويت ومهلة ونتيجة يقررها الخادم.
