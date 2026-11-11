# Performance Budget — v2.1
**الحالة: PROPOSED | لا توجد ميزانية معتمدة**

الأرقام الموروثة 30FPS وRAM أقل من 400MB وAPK أقل من 100MB وRedmi Note 8 ليست متطلبات للحزمة v2.1 وليست نتائج. حزمة v1 استُبدلت. لا تُعتمد هذه الأرقام قبل تحديد SKU وإصدار Android وقياس فعلي وفق [PERFORMANCE_TEST_PROTOCOL.md](PERFORMANCE_TEST_PROTOCOL.md).

30FPS يساوي 33.3ms للإطار إذا اختير هذا الهدف لاحقاً. قس CPU وGPU منفصلين، PSS، الذاكرة، GC، التحميل، draw calls، الحرارة، البطارية، bandwidth ومدة tick الخادم.

المنهج عند وجود build: مشهد ثابت، warm-up، جلسة 15 دقيقة على الأقل، قياس قبل وبعد الحرارة، median وP95 وworst-case. لا تحسين بلا profiler evidence. لا Golden Baseline قبل أول قياس محفوظ.
