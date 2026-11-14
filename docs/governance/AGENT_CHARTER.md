# ميثاق الوكلاء المتخصصين — تقسيم العمل من التوثيق إلى الإنتاج
**الحالة: PROPOSED | التاريخ: 2026-11-14 | الفرع: `arena/6c5729d1-costofstaying` | يرتبط بـ `WORK_PLAN_V2_DETAILED` والبوابات الثماني**

> كل وكيل هو "الأقوى في مجاله" — ليس اسماً بل **مسؤولية + مدخلات + مخرجات + حارس بوابة**. لا وكيل يعمل وحده؛ التسليم دائماً `PR + دليل`.

---

## 1) التعريف العام

| المبدأ | القاعدة |
|---|---|
| **الملكية الوحيدة** | كل مهمة لها مالك وكيل واحد — لا "جماعي". |
| **حد الحجم** | PR واحد = مهمة واحدة <400 سطر + اختبار + توثيق. |
| **الدليل أولاً** | لا تسليم بدون `commit SHA + CI log + artifact hash`. |
| **اللغات** | الوثائق عربية/إنجليزية، الكود إنجليزي، التعليقات توضح RULE. |
| **التناوب** | Governor يراجع كل PR؛ Verifier يشغل CI. |

## 2) الوكلاء العشرة — البطاقات التعريفية

### 1. الحَكم — Governor (Claude — الحوكمة)
**التخصص:** التوثيق، القرارات، التتبع، حل التعارض.
**المدخلات:** `00_CHARTER`, `DOCUMENT_GOVERNANCE`, `CONFLICT_RESOLUTIONS`.
**المخرجات:** ADR جديد/محدث، DECISIONS_LOG، CATALOG، AUDIT_REPORT، WORK_PLAN.
**البوابات:** يحرس كل بوابة — يفتح/يغلق فقط بعد دليل.
**قواعد:** لا يغير حالة ADR دون ملف ADR؛ لا يستخدم IMPLEMENTED لمجرد وثيقة؛ يحجز أرقام ADR قبل الكتابة (CONFLICT #21). يراجع كل PR.

### 2. المعمار — Architect (Kimi — هندسة الأنظمة)
**التخصص:** تقسيم النواة، الحدود البرمجية، NetArchTest، اختيار التقنيات.
**المدخلات:** ADR-001/005/021/023, `TECHNICAL_ARCHITECTURE`.
**المخرجات:** `Directory.Build.props`, خدمة مجزأة، تبعيات نظيفة، مقارنة FishNet/Netcode.
**البوابات:** 0 (قفل 9.0), 1 (تحليل Betrayal), 2 (Stack), 3 (نقل Core), 7 (اختيار Backend).
**قواعد:** Core لا يعرف Unity/FishNet؛ LangVersion 9.0 للنواة؛ TreatWarningsAsErrors.

### 3. الحافظ — Domain Guardian (Grok — قواعد اللعب)
**التخصص:** RULE-01..05، `Match.cs`, `MatchConfig`, الساعات.
**المدخلات:** `02_GAME_RULES_SPECIFICATION`, `DOMAIN_CORE.md`, `presets.json`.
**المخرجات:** `VitalityAccount`, `CollapseScheduler`, `TransferService`, `VoteService`, `SpiritService` + اختبارات.
**البوابات:** 1 (تثبيت أرقام), 2 (Monotonic), 3 (نقل), 4 (Combat حدود).
**قواعد:** كل رقم خارج RULE هو PROPOSED TEST VALUE؛ النقل Move ذري؛ لا صحة عند تصويت.

### 4. الناقل — Net Sage (FishNet خبير شبكات)
**التخصص:** Transport↔Session, Envelope, tick, Dedicated Server.
**المدخلات:** `NETWORK_ARCHITECTURE`, ADR-008/016/017/018.
**المخرجات:** `NetworkAdapter`, `CommandEnvelope`, `DedicatedServer` Linux, اختبارات RTT/loss.
**البوابات:** 2 (prototype), 3 (شبكة), 4 (4 أجهزة).
**قواعد:** Client يرسل Intent فقط؛ Server يملك State؛ Snapshot ذاكرة فقط.

### 5. الحارس — Sentinel (أمن)
**التخصص:** STRIDE, OWASP ASVS/MASVS, AuthZ, Replay, Spam.
**المدخلات:** `THREAT_MODEL`, `TRUST_BOUNDARIES`, ADR-012..020.
**المخرجات:** fail-closed gates, payload validation, rate-limit, gitleaks, CodeQL.
**البوابات:** 3,6,7.
**قواعد:** كل أمر يُرفض افتراضياً حتى يُثبت؛ لا أسرار في Git.

### 6. الباني — Unity Forger (Unity/URP/Android)
**التخصص:** Unity 6.3 LTS, URP, Addressables, APK.
**المدخلات:** `TECHNICAL_ARCHITECTURE`, `COMBAT_SYSTEM`.
**المخرجات:** مشروع Unity فارغ → خريطة Low-Poly → UI → VFX → APK موقع.
**البوابات:** 2,5,6.
**قواعد:** Core netstandard2.1 مستورد كـ package؛ لا منطق قواعد في MonoBehaviour.

### 7. الميزان — Balancer (تصميم رقمي)
**التخصص:** Vitality اقتصادية، Collapse منحنيات، Betrayal عتبات.
**المدخلات:** `BALANCE_RISK_REVIEW`, `presets.json`, `sweep.py`.
**المخرجات:** 3 presets, sweep توزيع, توصيات ADR-004.
**البوابات:** 1,4,5.
**قواعد:** لا اعتماد رقم قبل 10 جلسات؛ جدول Collapse الموروث (8+ 30ث) مرفوض.

### 8. الشاهد — Playtest Oracle (بحث لاعبين)
**التخصص:** سيناريوهات، جلسات بشرية، استبيانات، تحليل نوعي.
**المدخلات:** `PLAYTEST_PLAN`, `BALANCE_RISK_REVIEW`.
**المخرجات:** 3 سيناريوهات PDF, 10 جلسات human JSON, تقرير تحليل.
**البوابات:** 1,4.
**قواعد:** 4 لاعبين/جلسة، 10–16د هدف، تسجيل 6 مقاييس إلزامية.

### 9. القاضي — Verifier (QA/Automation)
**التخصص:** اختبارات، CI، أدلة، ACCEPTANCE.
**المدخلات:** `TEST_STRATEGY`, `REQUIREMENTS_TRACEABILITY`, `ACCEPTANCE_CRITERIA`.
**المخرجات:** `TestResults/`, `playtest-logs/`, coverage, perf baselines.
**البوابات:** 0–7 (مشارك في كل بوابة).
**قواعد:** 100% pass, 80% coverage, لا VERIFIED كاذب.

### 10. المرسل — DevOps Herald (بناء/إصدار)
**التخصص:** CI/CD, إصدارات, مراقبة, نسخ احتياطي.
**المدخلات:** `BUILD_AND_RELEASE`, `DEPLOYMENT_ARCHITECTURE`.
**المخرجات:** `ci.yml` SHA-pinned, APKs, dashboards, Runbooks.
**البوابات:** 0,2,6,7.
**قواعد:** كل action بـ SHA, dependabot نشط, artifacts 30 يوم.

---

## 3) التسليم بين الوكلاء (Handoff)

```
[Governor: سيناريو/ADR] → [Architect: تصميم حدود] → [Domain+Net+Unity: تنفيذ] → [Balancer: ضبط أرقام] → [Verifier: اختبار+دليل] → [Sentinel: تدقيق أمني] → [DevOps: إصدار]
                                      ↑                                                           ↓
                                      └──────────────── Playtest Oracle (ملاحظات) ←────────────────┘
```

- **Definition of Ready (DoR):** المهمة لها مالك + تقدير + قبول + تبعية محلولة.
- **Definition of Done (DoD):** PR مدموج + CI أخضر + artifact hash + تحديث كتالوج + DECISIONS_LOG إن لزم.

## 4) المصفوفة السريعة (RACI مختصر)

| البوابة | Governor | Architect | Domain | Net | Sentinel | Unity | Balancer | Oracle | Verifier | DevOps |
|---|---|---|---|---|---|---|---|---|---|---|
| 0 تثبيت | A | R | C | C | C | I | I | I | R | R |
| 1 قواعد | A | R | R | I | I | I | R | R | R | I |
| 2 تكدس | A | R | C | C | I | R | I | I | R | R |
| 3 شبكة | A | R | R | R | R | I | I | I | R | C |
| 4 Slice | A | C | R | C | I | R | R | R | R | I |
| 5 بوابات | A | I | C | I | R | I | R | I | R | C |
| 6 إطلاق | A | C | I | I | R | R | I | I | R | R |

R=مسؤول، A=مُقرّ، C=مستشار، I=مطّلع

## 5) التشغيل اليومي

- **اجتماع بوابة أسبوعي (30د):** كل مالك يعرض دليله (SHA+artifact)، Governor يقرر فتح/إغلاق.
- **لوحة EXECUTION_BOARD:** عمود لكل بوابة، بطاقة لكل مهمة (#, وكيل, تقدير, حالة).
- **قاعدة 400 سطر:** أي PR أكبر يُقسّم.
- **اللغة:** التعليقات في الكود تشير إلى RULE-0X أو ADR-00X.

## المراجع
- `WORK_PLAN_V2_DETAILED.md`, `DOCUMENT_GOVERNANCE.md`, `SOURCES_AND_REFERENCES.md`
