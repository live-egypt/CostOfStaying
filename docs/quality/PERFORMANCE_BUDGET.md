# Performance Budget — v2.0
**الحالة: PROPOSED؛ baseline غير متوفر**

أهداف v1 (30FPS، RAM أقل من 400MB، APK أقل من 100MB، Redmi Note 8) أهداف أولية لا نتائج مثبتة. حدد SKU وإصدار Android قبل اعتمادها.

30FPS = 33.3ms/frame. قس CPU/GPU frame time منفصلين، PSS، managed/native memory، GC، loading، draw calls، thermal state، battery delta، bandwidth وserver tick duration.

المنهج: build/scene ثابتان، warm-up، جلسة 15 دقيقة على الأقل، قياس قبل وبعد thermal soak، median/P95/worst-case، ومقارنة Golden Baseline. لا تحسين بلا profiler evidence.
