# Performance Budget — v2.1
**الحالة: PROPOSED | لا ميزانية معتمدة قبل baseline | GATE-5 — هيكل القياس**

الأرقام الموروثة 30FPS/RAM<400MB/APK<100MB/Redmi Note 8 **ليست متطلبات v2.1** — مرفوضة في CONFLICT #8 حتى وجود baseline. حزمة v1 استُبدلت. لا تُعتمد قبل SKU وإصدار Android وقياس فعلي وفق `PERFORMANCE_TEST_PROTOCOL.md`.

## 1) ما نقيسه (قبل أي ميزانية)

| البعد | المقياس | الأداة | الهدف الأولي (غير معتمد) |
|---|---|---|---|
| **Android Render** | frame time (CPU+GPU), draw calls, triangles | Unity Profiler, Frame Debugger | ≤33.3ms لـ 30FPS إن اعتُمد لاحقاً |
| **ذاكرة** | PSS, Java heap, Native heap | Android Profiler, dumpsys | لا Golden قبل أول قياس محفوظ |
| **حرارة/بطارية** | °C, %/ساعة | Battery Historian | مراقبة فقط |
| **APK** | حجم + تحميل | Build Report | فارغ <50MB مقترح |
| **شبكة** | bandwidth KB/s, RTT, loss, tick time | Wireshark, Server log | 2–4KB/s/لاعب تقديري |
| **خادم** | tick time, CPU%, GC | Linux `perf` + logs | <16ms @10Hz |

## 2) بروتوكول Baseline (GATE-2.3 + GATE-5.2)

1. **مشهد ثابت** — فارغ أو Vertical Slice — warm-up 2د
2. **جلسة 15د** — 4 لاعبين محاكاة + 1 حقيقي (عند وجوده)
3. **قبل/بعد حرارة** — قياس بارد ثم بعد 10د
4. **إحصاء:** median, P95, worst-case — لا متوسط وحيد
5. **لا تحسين بلا profiler evidence** — كل تحسن بقياس قبل/بعد

## 3) Golden Baseline (يُنشأ بعد أول قياس محفوظ)

لا Golden قبل `main@<SHA>` + `perf_baseline.json` في artifacts. بعد القياس الأول:
- وثّق: `commit SHA + جهاز + إصدار Unity + إصدار FishNet + preset + median/P95`
- الميزانية تُعتمد فقط كإطار تفاوت (مثلاً "لا يتجاوز Golden+15%")

## 4) مسؤوليات البوابات

- **GATE-2.3:** baseline فارغ (APK + Linux) — أول رقم محفوظ
- **GATE-3.3:** tick time شبكي 10/15/20Hz
- **GATE-5.2:** baseline كامل 4 لاعبين حقيقيين — تحويل هذا الملف إلى APPROVED مقترح

## المراجع
- `PERFORMANCE_TEST_PROTOCOL.md`, `DEVICE_COMPATIBILITY.md`, `STACK_COMPARISON.md`
