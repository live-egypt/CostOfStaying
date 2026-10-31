# Performance Test Protocol — v2.0
**الحالة: PROPOSED | Baseline: NOT ESTABLISHED**

## الهدف
تحويل أهداف الأداء إلى نتائج قابلة لإعادة الإنتاج على أجهزة Android فعلية وخادم Linux.

## شروط التجربة
- سجل طراز الجهاز الدقيق، SoC/GPU، RAM، Android build، دقة الشاشة، وإصدار التطبيق.
- استخدم build ثابتاً مع commit SHA وإعدادات جودة موثقة.
- ثبّت المشهد وعدد اللاعبين والذكاء الاصطناعي والمؤثرات وحالة الشبكة.
- نفذ warm-up ثم جلسة لا تقل عن 15 دقيقة، وكرر التجربة ثلاث مرات على الأقل.
- سجل الحالة الحرارية قبل الاختبار وبعده؛ افصل القياس البارد عن thermal soak.

## القياسات
Client: CPU/GPU frame time، FPS، PSS، managed/native memory، GC allocations، load time، draw calls، battery delta، thermal state.
Server: CPU، RSS، tick duration، missed ticks، bandwidth، concurrent sessions، queue depth.
Network: RTT، jitter، packet loss، bytes/sec، reconnect duration.

## التقرير
لكل metric اعرض median وP95 وworst observed، مع ظروف التجربة وartifact hash. قارن مع Golden Baseline وفسر أي regression.

## بوابة الاعتماد
لا تعتمد 30 FPS أو 400MB أو حجم APK أو استهلاك البطارية كضمان قبل اختبارها. أي ميزانية نهائية تتطلب نتائج على أجهزة مستهدفة فعلية وموافقة مالك المنتج.
