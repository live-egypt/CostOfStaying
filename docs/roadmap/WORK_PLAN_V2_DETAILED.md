# خطة التنفيذ الكاملة — من التوثيق إلى الإنتاج النهائي — V2
**الحالة: PROPOSED | التاريخ: 2026-11-14 | الفرع: `arena/6c5729d1-costofstaying` | الأساس: `AUDIT_REPORT_2026-11-14` + `WORK_PLAN_2026-11-14` + ADR-021..023 | يبطل ما قبله ويوحّد بوابات الميثاق الثماني**

> لا تُتخطى بوابة. كل بوابة لها **مالك وكيل + بوابة دخول + بوابة خروج بدليل SHA+artifact**. لا VERIFIED بدون `commit SHA + CI أخضر + hash + بيئة`.

---

## 0) الرؤية ونطاق MVP الملزم

**القيمة:** بقاء نفسي جماعي — الثمن مرئي (Vitality)، النقل تكتيكي (Suffering Transfer)، الروح شاهد محايد (Spirit)، الخادم حكم نهائي.
**MVP الملزم (حتى ADR جديد):** 4 لاعبين، خريطة واحدة، مباراة 10–16د (حد 20د)، 3D Dark Low-Poly، Android أولاً + Linux Dedicated Server.
**خارج MVP:** متجر/اقتصاد دائم/Meta/خرائط متعددة/Voice/iOS/8–16 لاعب/مكافحة غش تجارية.

---

## 1) منظومة الوكلاء المتخصصة — من التوثيق إلى الإنتاج

| الوكيل | المجال الأقوى | المسؤولية الأساسية | البوابات |
|---|---|---|---|
| **الحَكم — Governor** | الحوكمة والتوثيق | كتالوج، ADR، CONFLICT، مصادر، AUDIT | 0–7 (حارس كل بوابة) |
| **المعمار — Architect** | هندسة الأنظمة | Core حدود، تقسيم الخدمات، ADR تقني | 0,1,2,3,7 |
| **الحافظ — Domain Guardian** | قواعد اللعب | RULE-01..05، MatchConfig، parity | 1,2,3,4,5 |
| **الناقل — Net Sage** | شبكات الألعاب | FishNet، Transport↔Session، tick | 2,3,4 |
| **الحارس — Sentinel** | أمن التطبيقات | Threat Model، AuthZ، SAST، gitleaks | 3,6,7 |
| **الباني — Unity Forger** | Unity/URP/Android | مشروع Unity، URP، APK، Addressables | 2,5,6 |
| **الميزان — Balancer** | توازن رقمي | presets، sweep، اقتصاد Vitality | 1,4,5 |
| **الشاهد — Playtest Oracle** | بحث لاعبين | 10 جلسات بشرية، استبيانات، تحليل | 1,4 |
| **القاضي — Verifier** | QA/Automation | اختبارات، CI، أدلة، ACCEPTANCE | 0–7 |
| **المرسل — DevOps Herald** | بناء/إصدار/مراقبة | CI/CD، إصدارات، Logs، Backup | 0,2,6,7 |

**قاعدة التسليم:** GPT (سيناريوهات) → Governor (ADR) → Architect (تصميم) → Domain/Network/Unity (تنفيذ) → Balancer (ضبط) → Verifier (دليل) → Sentinel (أمان) → DevOps (إصدار). كل وكيل يسلّم PR واحد صغير قابل للمراجعة (<400 سطر).

---

## 2) البوابات الثماني — بالتفصيل الدقيق

### البوابة 0 — التثبيت والتصليب (1–2 يوم) — GATE-0
**المالك:** Governor + DevOps Herald | **الدخول:** `main@8196c20` | **الخروج:** `audit.py 0/0 + build -warnaserror + parity 6 حالات`

| # | المهمة | الوكيل | التقدير | القبول (بوابة خروج) | المخاطر |
|---|---|---|---|---|---|
| 0.1 | قفل الإصدارات: `global.json 8.0.400`، `Directory.Build.props` (LangVersion 9.0 + TreatWarningsAsErrors)، `Directory.Packages.props` بدون نطاقات | Architect | 2س | `dotnet build -warnaserror` أخضر | Core كان 10.0 → يفشل Unity |
| 0.2 | قفل CI: SHA-pin (منجز 11d5960/67a3573/a26af69/ea165f8) + `persist-credentials:false` + `dependabot.yml` NuGet | DevOps | 2س | `ci.yml` يستخدم SHA فقط، `grep -r 'uses:.*@v'` =0 | Supply chain |
| 0.3 | تقوية `test_parity.py` من 2 إلى 6 حالات (تآكل/Collapse/نقل/تصويت/Spirit/تزامن) | Domain Guardian + Verifier | 4س | 6 حالات، يفشل عند انحراف Python≠C# | انحراف محاكاة |
| 0.4 | تحسين `simulate.py` من عشوائي 8% إلى استراتيجي (cooldown-aware + capacity-aware) ليغطي 5 نتائج | Balancer + Domain | 1ي | ≥3 نتائج في sweep، لا أحادي LastSurvivor | توازن خادع |
| 0.5 | تنظيف `Debuff.Clone()` / استخدامها + حد `Intent.SentAt` زمني (anti-replay) | Domain + Sentinel | 2س | لا كود ميت، `grep Clone` مستخدم أو محذوف | replay |
| 0.6 | ترويسة `DECISIONS_LOG` + تحديث `AUDIT_REPORT` §2 (32 فرع) | Governor | 1س | `audit.py` 0/0 | — |

**أدلة GATE-0:** `TestResults/`, `sim.json`, `playtest-logs/report-*.md` + `audit.log` محفوظة 30 يوم.

---

### البوابة 1 — تثبيت القواعد (2–3 أسابيع) — GATE-1
**المالك:** Playtest Oracle + Domain Guardian + Balancer | **الدخول:** GATE-0 أخضر | **الخروج:** ADR-004 APPROVED أو PROPOSED مع توصيات + 10 جلسات بشرية

| # | المهمة | الوكيل | التقدير | القبول |
|---|---|---|---|---|
| 1.1 | 3 سيناريوهات ورقية (اجتماعي/تكتيكي/روحي) + أوراق Vitality وCollapse يدوية + قوالب تسجيل | Playtest Oracle + Balancer | 2ي | 3 PDFs في `docs/quality/playtest/scenarios/` |
| 1.2 | 10 جلسات بشرية (4 لاعبين/جلسة) — تسجيل: مدة/موت/نقل/نتيجة/انسحاب/فهم — على Domain CLI أو طاولة | Verifier (تنظيم) + Oracle (إشراف) | 2أ | 10× `playtest-logs/human/session-*.json` + استبيانات |
| 1.3 | تحليل: median/range، توزيع 5 نتائج، أسئلة Betrayal الخمس (تراكمي vs استبدالي، cooldown، سعة، مسافة) | Architect + Balancer | 4ي | `BALANCE_RISK_REVIEW` محدث + توصيات ADR-004 |
| 1.4 | تحديث `ADR-004` + `02_GAME_RULES_SPECIFICATION` إن تغير رقم (100/20ث/15ث/20د) | Governor | 2ي | PR مع `DOCUMENT_GOVERNANCE` محدث |
| 1.5 | ربط `COMBAT_SYSTEM` بـ Vitality أو توضيح "لا تفاعل في MVP" | Domain + Governor | 4س | لا غموض في GLOSSARY |

---

### البوابة 2 — إثبات التكدس (3–4 أسابيع) — GATE-2
**المالك:** Unity Forger + Architect + Verifier | **الدخول:** GATE-1 | **الخروج:** ADR-001 APPROVED + builds

| # | المهمة | الوكيل | التقدير | القبول |
|---|---|---|---|---|
| 2.1 | Unity 6.3 LTS + URP مشروع فارغ — مقارنة FishNet vs Netcode for GameObjects (جدول: ميزات/Android/Linux/تكاليف) | Unity Forger + Net Sage | 1أ | `docs/architecture/STACK_COMPARISON.md` + ADR-001 تحديث |
| 2.2 | APK فارغ + Linux Dedicated Server فارغ — نسخ مثبتة + Addressables | DevOps + Unity Forger | 1أ | Artifacts في CI + `global.json` |
| 2.3 | Baseline أولي: PSS, CPU, حجم APK, tick 30Hz على جهاز منخفض (حتى فارغ) | Verifier | 1أ | `PERFORMANCE_BUDGET baseline` (لا ميزانية بعد) |
| 2.4 | اعتماد ADR-001 | Governor | 2ي | ADR-001 APPROVED |

---

### البوابة 3 — الشبكة والخادم الموثوق (4–6 أسابيع) — GATE-3
**المالك:** Net Sage + Sentinel + Domain | **الدخول:** GATE-2 | **الخروج:** 4 لاعبين على خادم مستقل بدون غش

| # | المهمة | الوكيل | التقدير | القبول |
|---|---|---|---|---|
| 3.1 | نقل Domain إلى Core (ADR-021): `VitalityAccount`, `CollapseScheduler`, `TransferService`, `VoteService`, `SpiritService` + تقسيم `Match.cs` 529 سطر + NetArchTest | Architect + Domain | 2أ | Core حدود صارمة + 242 اختبار خضراء |
| 3.2 | `NetworkAdapter`: `Intent` → `Match.TryTransfer` + `CommandEnvelope`/`Sequence` (ADR-008) + `TransportBinding` (016) | Net Sage | 2أ | العميل لا يرسل Vitality |
| 3.3 | `DedicatedServer` Linux: `Tick(1/tickRate)` + `MonotonicClock` + `NetworkSnapshot` (ذاكرة فقط, ADR-003) | Net Sage + DevOps | 2أ | Server يملك الحالة |
| 3.4 | `Match Roster Admission` (018) + `Authorization Policy` (019) + `Payload Validation` (020) fail-closed | Sentinel + Domain | 1أ | لا اختراق |
| 3.5 | اختبارات شبكة: RTT 100/200/300ms, loss 1/3/5%, jitter, reorder, disconnect/reconnect | Verifier | 1أ | لا crash |
| 3.6 | إثبات 2 جهاز → 4 أجهزة + فيديو | Verifier | 1أ | فيديو + `NETWORK_ARCHITECTURE` محدث |

---

### البوابة 4 — الشريحة العمودية Vertical Slice (6–8 أسابيع) — GATE-4
**المالك:** Unity Forger + Domain + Balancer | **الدخول:** GATE-3 | **الخروج:** مباراة كاملة بدون تدخل مطور

| # | المهمة | الوكيل | التقدير | القبول |
|---|---|---|---|---|
| 4.1 | خريطة Low-Poly + تحكم لمس + UI Vitality/Debuff/تصويت + Collapse VFX | Unity Forger | 3أ | خريطة قابلة للعب |
| 4.2 | `Suffering Transfer` FX + `Betrayal` counter (cost-only) | Domain + Unity Forger | 2أ | مطابق RULE-02 + ADR-022 |
| 4.3 | `Spirit` world + Echo Fragments + Hint عام (cooldown) | Domain | 2أ | محايد (RULE-04) |
| 4.4 | Combat محدود (4 أسلحة, hit validation خادمي) | Domain + Net Sage | 2أ | لا يطغى على Vitality |
| 4.5 | Endgame 5 نتائج UI واضحة (Last/Collective/Time/Draw/Mutual) | Unity Forger | 1أ | يطابق `ENDGAME_SYSTEM` |

---

### البوابة 5 — بوابات الإصدار (4 أسابيع) — GATE-5
**المالك:** Sentinel + Verifier + Performance | **الدخول:** GATE-4 | **الخروج:** Closed Alpha جاهز

| # | المهمة | الوكيل | التقدير | القبول |
|---|---|---|---|---|
| 5.1 | تفعيل CodeQL + Dependabot + SAST كامل + `SECURITY_TEST_PLAN` | Sentinel | 2أ | 0 Critical/High |
| 5.2 | قياس Android + Server عند 4 لاعبين: median/P95, PSS, tick, bandwidth | Performance (Muqaddar) | 2أ | `PERFORMANCE_BUDGET` معتمد |
| 5.3 | 50 مباراة متتالية بدون crash/فساد + reconnect | Verifier | 1أ | 0 crash |
| 5.4 | `ACCEPTANCE_CRITERIA` كل بند له SHA+hash+بيئة | Governor + Verifier | 1أ | 0 VERIFIED كاذب |

---

### البوابة 6 — إعداد الإطلاق (2–3 أسابيع) — GATE-6
**المالك:** DevOps Herald + Governor | **الدخول:** GATE-5 | **الخروج:** Release Candidate

| # | المهمة | الوكيل | التقدير | القبول |
|---|---|---|---|---|
| 6.1 | `BUILD_AND_RELEASE` + `DEPLOYMENT_ARCHITECTURE` + توقيع APK + Play Internal Track | DevOps | 1أ | RC على متجر داخلي |
| 6.2 | `MONITORING_AND_LOGGING` + `BACKUP_AND_RECOVERY` + اختيار Backend (ADR-003 APPROVED) | Architect + DevOps | 1أ | Dashboards + Runbooks |
| 6.3 | Close 7 ADRs → APPROVED/IMPLEMENTED/VERIFIED + إعلان Closed Alpha | Governor | 1أ | `DELIVERY_PLAN` مكتمل |

### البوابة 7 — التشغيل الحي (مستمر) — GATE-7
**المالك:** All | **الدخول:** GATE-6 | LiveOps: اقتصاد، خرائط جديدة، Voice، iOS — كل بقرار نطاق جديد.

---

## 3) التبعيات وخارطة الزمن

```
GATE-0 (2ي) → GATE-1 (3أ) → GATE-2 (4أ) → GATE-3 (6أ) → GATE-4 (8أ) → GATE-5 (4أ) → GATE-6 (3أ) → GATE-7 ∞
                                    └─ لا تبدأ GATE-3 قبل GATE-0 قفل LangVersion (Fix #25)
```

**تراكمي MVP (Closed Alpha):** ~28–30 أسبوع (7 أشهر) بفريق 6–8 متخصصين متفرغين. بدون فريق ثابت: تقدير نسبي فقط — لا تواعد زمني ملزم قبل معرفة السعة.

**المسار الحرج:** GATE-1 (Playtest) → GATE-3 (Network) → GATE-4 (Slice). أي تأخير في Playtest يؤخر اعتماد ADR-004 وكل ما بعده.

---

## 4) مقاييس الأداء ومخاطر مركزية

**KPIs أسبوعية (Verifier ينشر):**
- `audit.py` 0/0، `dotnet test` 100% pass، coverage ≥80%
- Playtest توزيع 5 نتائج + مدة 10–16د + انسحاب <10%
- CI أخضر + artifacts 30 يوم + gitleaks 0
- APK حجم <150MB، PSS <800MB على جهاز متوسط، tick <16ms

**المخاطر:**
| الخطر | التخفيف | الوكيل |
|---|---|---|
| Betrayal عتبات غامضة → دوامة | GATE-1.3 + نموذج تراكمي/استبدالي | Balancer |
| توازن 100% LastSurvivor خادع | GATE-0.4 + 1.2 | Balancer |
| FishNet vs Netcode تردد | GATE-2.1 مقارنة + ADR-001 | Architect |
| لا جلسات بشرية → ADR-004 يبقى PROPOSED | GATE-1.2 إلزامي | Playtest Oracle |
| LangVersion 10 يكسر Unity | GATE-0.1 قفل 9.0 | Architect |

---

## 5) حوكمة التنفيذ

- كل PR <400 سطر، مراجع واحد وكيل + Governor.
- لا VERIFIED بدون `commit SHA + runner + artifact hash + بيئة`.
- ADR-005..020 محجوزة لـ Core — لا إعادة استخدام (CONFLICT #21).
- `REQUIREMENTS_TRACEABILITY`: كل Requirement → Control → Test → Evidence.
- اجتماع بوابة أسبوعي: كل مالك يعرض دليله، Governor يقرر فتح/إغلاق.

## المراجع
- `AUDIT_REPORT_2026-11-14`, `DELIVERY_PLAN`, `DOCUMENT_GOVERNANCE`, `SOURCES_AND_REFERENCES`, ADR-021..023
