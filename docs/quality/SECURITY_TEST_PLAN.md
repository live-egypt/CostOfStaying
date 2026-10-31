# Security Test Plan — v2.0
**الحالة: PROPOSED**

اختبر: تزوير health/damage/ammo/energy؛ RPC spam؛ replay للنقل والهجوم والتصويت؛ نقل متزامن؛ هدف ميت أو من مباراة أخرى؛ payload مشوه/كبير؛ speed/teleport؛ جلسة منتهية أو مزورة؛ replication غير مصرح؛ وفحص الأسرار.

النجاح: الطلب غير الصالح يرفض دون تغيير الحالة أو crash أو استنزاف غير محدود. كل ثغرة تتحول إلى regression test قبل إغلاقها.
