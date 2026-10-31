# Test Strategy — v2.0
**الحالة: PROPOSED**

هرم الاختبار: Unit → Integration → PlayMode/System → Network → Manual UX/Playtest → Performance.

الأدوات: Unity Test Framework وNUnit. اختبر Collapse timing، Transfer atomicity/replay/concurrency، Betrayal thresholds، Combat validation/ammo، Spirit energy/cooldown، Endgame votes/timeouts/disconnect، ودورة المباراة.

سيناريوهات الشبكة: RTT 100/200/300ms، loss 1/3/5%، jitter، packet reorder، reconnect.

كل اختبار يحمل ID وهدفاً وخطوات ونتيجة متوقعة وفعلية وcommit/build والجهاز وسجل/Artifact. لا Critical defects، لا crash/data corruption معروف، مباراة 4 لاعبين كاملة، ونتائج أداء قابلة للتكرار.
