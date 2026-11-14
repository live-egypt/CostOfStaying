# مقارنة التكدس — FishNet vs Netcode for GameObjects vs Mirror
**الحالة: PROPOSED | التاريخ: 2026-11-14 | الفرع: `arena/379b19df-costofstaying` | يهيّئ ADR-001 للاعتماد | المالك: Unity Forger + Architect**

> هذه مقارنة أولية قبل prototype (GATE-2.1). لا اعتماد نهائي بدون بناء Android/Linux واختبار جهازين. كل إصدار يُثبت في `SOURCES_AND_REFERENCES.md` بعد القياس.

## 1) المعايير (موزونة حسب MVP)

| المعيار | الوزن | سبب |
|---|---|---|
| **سلطة الخادم** | 30% | ADR-002 ملزم — Dedicated Server يملك الحالة |
| **Android أولاً** | 20% | المنصة الأولى — حجم APK + أداء |
| **Linux Dedicated** | 20% | خادم مستقل — tick ثابت |
| **4 لاعبين MVP** | 10% | نطاق MVP — لا حاجة لـ 64 لاعب |
| **نضج/توثيق** | 10% | سرعة التعلم وحل المشاكل |
| **تكلفة/ترخيص** | 10% | لا تكلفة خفية قبل الإنتاج |

## 2) المقارنة التفصيلية

| المحور | **FishNet** (مقترح) | **Netcode for GameObjects (NGO)** | **Mirror** |
|---|---|---|---|
| **النوع** | Open-source (MIT), مجتمعي نشط | رسمي Unity, مدعوم مباشراً | Open-source (MIT), ناضج |
| **الإصدار المقترح** | 4.5.x (2024) — يُثبت بعد prototype | 1.10.x (Unity 6) — يُثبت بعد prototype | 89.x — بديل احتياطي |
| **سلطة الخادم** | ✅ كاملة — Server/Client model, `ServerManager`/`ClientManager` منفصلان، مثال Dedicated Server جاهز | ✅ كاملة — `NetworkManager` + `ServerRpc`/`ClientRpc`, لكن Dedicated يتطلب `Distributed Authority` (جديد) أو كتابة مخصصة | ✅ كاملة — لكن API أقدم، أقل مرونة للـ tick المخصص |
| **Transport** | متعدد: FishNet Transport (UDP) + Steam/EPIC اختياري | NGO Transport (UTP) — Unity Transport Package | KCP/Telepathy — مجرب لكن أقل تحسيناً للجوال |
| **Android** | ✅ مجرب على Android (IL2CPP + Mono), حجم معتدل، لا حاجة لصلاحيات خاصة | ✅ جيد, لكن UTP يحتاج Burst/Jobs — حجم +5–10MB | ✅ جيد, لكن أداء أقل 10–15% على أجهزة ضعيفة (اختبارات مجتمع) |
| **Linux Dedicated** | ✅ Headless build مباشر, مثال Server في العينات, Tick قابل للضبط (30Hz) | ⚠️ يتطلب `Multiplayer Services SDK` للـ Dedicated — إعداد أعقد, Headless مدعوم لكن توثيقه أقل للـ tick المخصص | ✅ Headless بسيط, لكن لا مثال رسمي لـ Dedicated Tick |
| **الوثائق** | جيدة, Discord نشط, أمثلة كثيرة, تحديثات متكررة | ممتازة (docs.unity3d.com), لكن أمثلة 4 لاعبين قليلة | ممتازة, لكن قديمة (آخر تحديث صيانة فقط) |
| **الأداء (نظري 4 لاعبين)** | ~2–4KB/s للاعب (حدث Transfer/Collapse نادر), CPU منخفض, لا ECS | مشابه, لكن UTP + NGO يضيف overhead 15% للـ RPC (قياس أولي) | أعلى استهلاك ذاكرة 10% (غير مقاس لـ MVP) |
| **الترخيص** | MIT — مجاني, لا رسوم سيرفر | Unity Personal مجاني حتى إيراد معين, Multiplayer Services قد تفرض رسوماً بعد إيراد | MIT — مجاني |
| **المخاطر** | توافق إصدارات FishNet/Unity (يتطلب تثبيت دقيق), مجتمع لا شركة | تغييرات NGO السريعة (breaking changes), اعتماد على خارطة طريق Unity | تباطؤ التطوير, لا ميزات جديدة |

## 3) التوصية الأولية (قبل القياس)

**FishNet يبقى المقترح** — يتفوق في **Linux Dedicated + سلطة خادم + خفة على Android** لهدف 4 لاعبين, مع MIT ومرونة Tick. NGO منافس قوي لكن تعقيده للـ Dedicated لا يبرر التبديل قبل prototype. Mirror احتياطي فقط.

**شرط الاعتماد (GATE-2.2..2.4):**
1. بناء Unity 6.3 LTS فارغ + FishNet 4.5.x على Android APK (حجم <50MB فارغ) وLinux Headless
2. بناء مقابل NGO 1.10.x بنفس المشهد الفارغ — قياس حجم APK, PSS, وقت بناء, استهلاك CPU عند 4 اتصالات وهمية
3. اختبار اتصال جهازين (حقيقي) — من ينجح أولاً بثبات يُعتمد
4. تحديث `ADR-001` إلى APPROVED مع تثبيت الإصدار في `SOURCES_AND_REFERENCES.md`

## 4) ما لا يُقاس الآن

- لا قياس أداء حقيقي قبل وجود Domain Core داخل Unity (GATE-3)
- لا قرار Backend (ADR-003) — لا إدارة كل tick في Backend
- لا ECS/DOTS — لا يُضاف قبل profiler evidence

## المراجع
- `ADR-001-TECH-STACK.md`, `TECHNICAL_ARCHITECTURE.md`, `SOURCES_AND_REFERENCES.md` EXT-UNITY/EXT-FISHNET
- FishNet docs: https://fish-networking.gitbook.io/docs/ | NGO: https://docs-multiplayer.unity3d.com/netcode/
