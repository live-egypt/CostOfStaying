# الوثيقة التقنية v1.0
**Technical Design Document**

**المشروع:** ثمن البقاء  
**المحرك:** Unity 2022 LTS / Unity 6 + URP  
**الشبكة:** FishNet ≥ 4.6.13  
**Backend:** Nakama  
**التاريخ:** 30 أكتوبر 2026  
**الحالة:** معتمد

---

## 1. التكدس التقني النهائي

| الطبقة | الأداة | ملاحظات |
|--------|--------|---------|
| المحرك | Unity 2022 LTS / Unity 6 | URP فقط (لا HDRP) |
| الشبكة | FishNet (MIT) | Server-Authoritative |
| Backend | Nakama | مصادقة + مباريات + بيانات |
| التحكم | Unity Input System | + On-Screen Controls |
| الصوت | Unity Audio | لاحقاً يمكن FMOD |
| الضغط | ASTC 6x6 (بيئة) + 4x4 (UI) | ضروري للجوال |
| التحسين | Object Pooling + LOD + Occlusion | مدمج |

**قرارات ثابتة:**
- لا Photon (تكلفة)
- لا Netcode for GameObjects كخيار أول
- لا إضافات تحكم لمس مدفوعة

---

## 2. المعمارية العامة

```
Client (Unity)
 ├── Touch Input
 ├── Player Controller (Prediction للحركة فقط)
 ├── Debuff Visualizer
 ├── Combat Client (إرسال طلبات فقط)
 ├── Spirit Client
 └── FishNet Network Client

Server (FishNet)
 ├── Match Manager
 ├── Per-Player Collapse Timer
 ├── Dynamic Timer Controller
 ├── Suffering Transfer Validator
 ├── Debuff Applicator (Snapshot + Delta)
 ├── Combat Hit Registration
 ├── Spirit Ability Manager
 ├── Vote Manager
 └── Anti-Cheat Validator

Backend (Nakama)
 ├── Authentication
 ├── Matchmaking
 ├── Player Data
 └── Match History
```

**المبدأ:** كل منطق حساس على السيرفر. العميل يتنبأ بالحركة فقط.

---

## 3. هيكل المجلدات (Unity)

```
Assets/
├── Scenes/
│   ├── MainMenu.unity
│   ├── Lobby.unity
│   ├── Match.unity
│   └── Tutorial.unity
├── Scripts/
│   ├── Player/
│   ├── Systems/
│   │   ├── Collapse/
│   │   ├── Transfer/
│   │   ├── Combat/
│   │   ├── Spirit/
│   │   └── EndGame/
│   ├── Network/
│   └── UI/
├── ScriptableObjects/
│   ├── Debuffs/
│   └── Weapons/
├── Prefabs/
├── Materials/
├── Textures/
├── Audio/
└── Addons/
    ├── FishNet/
    └── Nakama/
```

---

## 4. الأنظمة التقنية الرئيسية

### 4.1 نظام الانهيار
- مؤقت فردي لكل لاعب على السيرفر.
- الفترة تتغير حسب عدد الأحياء (D-004).
- تطبيق Debuff عبر Server فقط.
- مزامنة: Snapshot عند الانضمام + Delta عند كل تغيير.

### 4.2 نظام نقل المعاناة
- التحقق الكامل على السيرفر (مسافة + تبريد + وجود Debuff).
- Rate Limit: طلب واحد كل 12–18 ثانية.
- تسجيل نقطة خيانة على السيرفر.

### 4.3 نظام القتال
- كل الضرر يُحسب على السيرفر.
- التحقق من زاوية التصويب والمدى والذخيرة.
- الإصابة تسرّع الانهيار التالي (20%).

### 4.4 نظام الروح
- تحويل الحالة على السيرفر.
- الطاقة والتبريد تُدار على السيرفر.
- الأفعال تُنفذ عبر ServerRpc مع تحقق.

### 4.5 نظام النهاية
- Vote Manager على السيرفر.
- تحقق دوري من شروط النهاية.
- عند التضحية: تطبيق +15% صحة ثم إنهاء.

---

## 5. المزامنة الشبكية

| الحدث | ما يُرسل | التوقيت |
|-------|---------|---------|
| انضمام لاعب | Snapshot كامل للـ Debuffs | فور الاتصال |
| تطبيق Debuff | Delta (id + stacks) | فوراً |
| نقل Debuff | Delta مزدوج | فوراً |
| حركة | Input + تصحيح | 20 Hz |
| قتال / همس / تصويت | أحداث | فوري |

- Client-side Prediction: للحركة فقط.
- Interest Management: يُفعّل لاحقاً عند 16 لاعباً.

---

## 6. الأداء (أهداف)

| الجهاز | الهدف |
|--------|------|
| Redmi Note 8 | 30 FPS مستقر |
| RAM | < 400 MB |
| حجم APK | < 100 MB (يفضل < 80) |
| استهلاك بطارية | < 15% لجولة 15 دقيقة |

تقنيات إلزامية:
- ASTC
- Object Pooling
- LOD
- Occlusion Culling
- Fixed Timestep ≈ 0.0333 (30 Hz)

---

## 7. الأمان (Anti-Cheat أساسي)

- كل الضرر والحسابات على السيرفر.
- التحقق من سرعة الحركة (≤ 6–7.2 م/ث).
- التحقق من زاوية التصويب.
- Rate Limiting لكل RPC حساس.
- Session Tokens عبر Nakama.

---

## 8. خطة بديلة

| السيناريو | البديل |
|----------|--------|
| FishNet مشاكل على الجوال | Mirror |
| Nakama معقد للـ MVP | Host/Client يدوي + حفظ محلي مؤقت |
| أداء ضعيف | تقليل Render Scale + إيقاف الظلال |

---

**نهاية الوثيقة التقنية v1.0**