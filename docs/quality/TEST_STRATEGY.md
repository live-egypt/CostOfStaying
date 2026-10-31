# استراتيجية الاختبار — v2.1
**الحالة: PROPOSED | لا توجد اختبارات منفذة مثبتة في هذه الحزمة**

## هرم الاختبار
Unit → Integration → PlayMode/System → Network → Manual UX/Playtest → Performance/Security. يجب اختيار مستوى الاختبار وفق الخطر؛ لا يغني اختبار Unit عن اختبار الشبكة أو الجهاز الفعلي.

## نطاق الاختبار الوظيفي
- Collapse: Vitality decay، المؤقتات، إعادة الجدولة، وحدود الموت.
- Transfer: Move atomicity، السعة، المدى، cooldown، replay، concurrency.
- Betrayal: اكتساب النقاط والعتبات بعد حسم آثارها.
- Combat: server validation، ammo، fire rate، range، line of sight.
- Spirit: انتقال الموت، Echo uniqueness، cooldown للتلميح، ومنع التأثير المباشر على الأحياء.
- Endgame: التصويت، الإجماع، timeout، disconnect، time limit، mutual collapse.
- Match lifecycle: إنشاء المباراة، الانضمام، البدء، الانتهاء، وإعادة الاتصال وفق سياسة معتمدة.

## أدوات مقترحة
Unity Test Framework وNUnit بعد اعتماد Unity. لا تفترض أن الأدوات أو test assemblies أضيفت إلى المستودع.

## سيناريوهات الشبكة
RTT 100/200/300ms، loss 1/3/5%، jitter، packet reorder، reconnect، والطلبات المتزامنة. هذه نقاط اختبار مقترحة وليست مواصفات تشغيل مثبتة.

## توثيق كل اختبار
كل اختبار يجب أن يحمل ID وهدفاً وخطوات ونتيجة متوقعة وفعلية وcommit/build والجهاز/البيئة وسجل أو artifact. اربط الاختبار بمتطلب في REQUIREMENTS_TRACEABILITY.md.

## بوابات الجودة المستهدفة
لا Critical defects معروفة، لا crash أو data corruption معروف في سيناريو القبول، مباراة كاملة لأربعة لاعبين، وأداء قابل للتكرار على أجهزة مستهدفة. هذه معايير قبول مستقبلية وليست حالة حالية.
