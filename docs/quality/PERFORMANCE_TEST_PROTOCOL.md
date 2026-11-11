# Performance Test Protocol — v2.1
**الحالة: PROPOSED | Baseline: NOT ESTABLISHED**

لا تُعتمد 30FPS أو 400MB أو حجم APK كضمان. هذه أرقام موروثة مذكورة في [PERFORMANCE_BUDGET.md](PERFORMANCE_BUDGET.md) للرفض فقط.

## الهدف
تحويل أي هدف أداء مستقبلي إلى نتائج قابلة لإعادة الإنتاج على أجهزة Android فعلية وخادم Linux، بعد وجود build.

## شروط التجربة
- سجّل طراز الجهاز، SoC/GPU، RAM، Android، دقة الشاشة، وإصدار التطبيق.
- استخدم build ثابتاً مع commit SHA.
- ثبّت المشهد وعدد اللاعبين وحالة الشبكة.
- warm-up ثم جلسة لا تقل عن 15 دقيقة، مكررة ثلاث مرات على الأقل.
- افصل القياس البارد عن القياس بعد thermal soak.

## القياسات
Client: CPU/GPU frame time، FPS، PSS، الذاكرة، GC، وقت التحميل، draw calls، البطارية، والحرارة.
Server: CPU، RSS، tick duration، missed ticks، bandwidth، وعمق الطابور.
Network: RTT، jitter، packet loss، ومدة إعادة الاتصال.

## التقرير
لكل مقياس: median وP95 وworst observed مع ظروف التجربة وartifact hash. لا مقارنة مع baseline قبل أن يوجد baseline محفوظ.

## بوابة الاعتماد
أي ميزانية نهائية تتطلب نتائج على أجهزة مستهدفة وموافقة موثقة. لا VERIFIED بدون تقرير.
