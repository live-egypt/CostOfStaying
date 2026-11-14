# تقرير التدقيق الشامل — CostOfStaying
**الحالة: PROPOSED | تدقيق وصفي لا يرفع حالات | التاريخ: 2026-11-14 | الفرع: `arena/4bfc0882-costofstaying` @ `f6f1ad5` | الأساس: `2c68f74` (main)**

> هذه الوثيقة تدقيق **وصفي** — لا ترفع أي حالة إلى `IMPLEMENTED`/`VERIFIED`. كل ما هو منفذ موصوف كـ `PROPOSED` حتى CI أخضر وأدلة محفوظة (المبدأ 5).

---

## 1) الملخص التنفيذي

| المحور | النتيجة |
|---|---|
| **الفروع** | 2 فقط: `main` (2c68f74) و`arena/4bfc0882-costofstaying` (f6f1ad5 = +2 commits). لا فروع ميتة. |
| **الوثائق** | 49 ملفاً تحت `docs/` (1593 سطر) + 2 جديدان موحِّدان للمصادر والهيكل. التدقيق الآلي `tools/audit.py`: **0 Issues, 1 Warn** (بلا روابط مكسورة، الكتالوج والفهرس مكتملان). |
| **الكود** | 895 سطر Domain Core + 748 سطر اختبارات + 570 سطر أدوات Python — **PROPOSED** (ليس VERIFIED). يطابق RULE-01..05 نصّاً. |
| **الاختبارات** | 6 suites, ~32 حالة تغطي التآكل، Collapse، النقل الذري، التصويت، النتائج الخمس، Spirit، الأمان. |
| **CI** | `ci.yml` نشط على push/PR (7 خطوات) + 7 قوالب موردين معطلة بوعي. |
| **الأمان** | لا أسرار في Git، فحص heuristic في CI، تصميم Threat Model PROPOSED. لا Critical مفتوحة. |
| **التوازن** | محاكاة تكشف خللاً: كل الجلسات المحاكاة تنتهي `LastSurvivor` — لا تنوع نتائج. |
| **النضج** | بوابة 3 من 6 في `DELIVERY_PLAN` منفذة كـ PROPOSED. لا Unity/No Dedicated Server/No Android build بعد — متوقع في Pre-production. |

**الخلاصة:** المشروع انتقل من "حزمة توثيق فقط" (2c68f74) إلى "توثيق موحّد + نواة مجردة قابلة للاختبار + CI" (f6f1ad5) **دون كسر الحوكمة**. المخاطر الرئيسية: عدم تنوع النتائج، وBetrayal بلا عتبات، وغياب اختبار شبكي حقيقي.

---

## 2) تدقيق المستودع (Repository)

### 2.1 الفروع والالتزامات

| الفرع | HEAD | المحتوى | الحالة |
|---|---|---|---|
| `main` | `2c68f74` | 44 وثيقة v2.1 فقط — لا كود | Protected (origin/HEAD) |
| `arena/4bfc0882-costofstaying` | `f6f1ad5` | +46 ملف كود/أدوات/CI/توثيق موحد | نشط، مدفوع، غير مدموج |

```
f6f1ad5 chore(repo): organize repository and unify sources/references
0ed82f4 feat(domain,ci,tools): implement Domain Core (RULE-01..05), tests, simulators and active CI
2c68f74 docs: name Draw in ADR-004 and drop the stale v2.0 note  ← main
```

- لا فروع متروكة، لا merge commits، تاريخ خطي نظيف.
- حجم الإضافة: 35 ملف commit ثانٍ، 761 سطر توثيق/تنظيم — مراجع قابل.

### 2.2 هيكل الملفات (81 ملف)

```
. (root) — 12 ملف تنظيمي
├── .github/workflows/ — 8 (1 نشط + 7 قوالب)
├── .github/ISSUE_TEMPLATE/ — 4 + PR template
├── docs/ — 49 md (طبقات 8) + playtest-logs/ (11 ملف أدلة)
├── src/ — 8 cs files (Domain Core)
├── tests/ — 6 cs files + csproj
├── tools/ — 5 py + 2 md
├── global.json + Directory.Packages.props + CostOfStaying.sln
└── LICENSE/CONTRIBUTING/CODE_OF_CONDUCT/SECURITY.md
```

- `.gitignore` شامل: `bin/obj/TestResults/__pycache__/Library/.codeql/`. يمنع تسريب أسرار وأصول Unity مستقبلاً.
- `.editorconfig` + `.gitattributes` يضمنا `LF` و`utf-8` ومنع تعارض الحروف العربية.
- لا ملفات كبيرة (`981K` إجمالي) — لا binaries.

### 2.3 الحوكمة والكتالوج

| الملف | الالتزام |
|---|---|
| `DOCUMENT_GOVERNANCE.md` | **ممتاز** — كتالوج 44 وثيقة + 2 تطبيقية، قواعد تعارض 6 مستويات، حالات 7، ملاحظة النواة والـ CI. |
| `CONFLICT_RESOLUTIONS.md` | 20 بنداً (8 موروثات مرفوضة + 2 توحيد) — واضح. |
| `DECISIONS_LOG.md` | 5 ADRs (1 PROPOSED, 1 APPROVED/NotImplemented, 3 PROPOSED) — متسق مع الملفات. |
| `REQUIREMENTS_TRACEABILITY.md` | 8 متطلبات مربوطة بـ Test IDs فعلية — PROPOSED. |
| `SOURCES_AND_REFERENCES.md` | **جديد وقوي** — 12 مصدر داخلي + 6 خارجي + جدول أرقام موحّد. يمنع التضارب الرقمي. |
| `REPOSITORY_STRUCTURE.md` | **جديد** — شجرة + طبقات + قواعد إضافة + أوامر بناء. |

- `tools/audit.py` يتحقق 7 محاور: روابط، كتالوج، فهرس، ترويسات، ادعاءات محرمة، تطابق ADR. **0 Issues**.
- Warn وحيد: `DECISIONS_LOG.md` بلا ترويسة `الحالة:` — مقبول (فهرس وليس قراراً).

---

## 3) تدقيق الوثائق (Product / Systems / Architecture / Security / Quality / Ops / Roadmap)

### 3.1 المنتج

| الوثيقة | الحالة | التقييم |
|---|---|---|
| `00_PROJECT_CHARTER.md` | PROPOSED (→ Domain PROPOSED) | ممتاز — رؤية، ركائز 5، نطاق واضح، بوابات 6، مراجع موحدة. لا تضخم. |
| `01_GAME_DESIGN_DOCUMENT.md` | PROPOSED FOR PLAYTEST | جيد — ملخص + أعمدة + حلقة + نهاية (5 نتائج). لا يعيد اختراع القواعد. |
| `02_GAME_RULES_SPECIFICATION.md` | PROPOSED FOR PLAYTEST | **ممتاز — مصدر الحقيقة** — RULE-01..05 واضحة، قواعد مشتركة، بوابة 10 جلسات. |
| `03_PLAYER_EXPERIENCE_AND_CORE_LOOP.md` | PROPOSED FOR PLAYTEST | جيد — لكن قصير (فحص لاحقاً) |
| `GLOSSARY.md` | PROPOSED | ممتاز — 18 مصطلحاً + قواعد تسمية + مراجع. جديد: Domain Core, CI Gate. |

- لا تناقض بين الميثاق والقواعد والـ GDD — متسقة حول 4 لاعبين / 20د / Vitality 100.

### 3.2 الأنظمة (Systems)

| النظام | الحالة | الالتزام بالقواعد | المخاطر |
|---|---|---|---|
| `COLLAPSE_SYSTEM.md` | PROPOSED FOR PLAYTEST | يطبق RULE-01 + سياسة إعادة جدولة نسبة — صحيح | فترة واحدة فقط مسماة (15ث) — الباقي TBD بوضوح ✅ |
| `SUFFERING_TRANSFER_SYSTEM.md` | PROPOSED FOR PLAYTEST | Move ذري + 6 شروط + idempotency — مطابق RULE-02 | المدى 2م وcooldown قيم اختبار — موثق ✅ |
| `BETRAYAL_SYSTEM.md` | PROPOSED FOR PLAYTEST | نقطة واحدة فقط = RULE-02 — صحيح | عتبات 3/5/7 فرضيات غير منفذة — 5 أسئلة مفتوحة ✅ |
| `SPIRIT_SYSTEM.md` | PROPOSED FOR PLAYTEST | محايد + Echo + تلميح عام — مطابق RULE-04 | Energy TBD — موثق بوضوح ✅ |
| `ENDGAME_SYSTEM.md` | PROPOSED FOR PLAYTEST | 5 نتائج كاملة (Draw منفصلة) — مطابق RULE-05 | لا منح صحة — صحيح ✅ |
| `COMBAT_SYSTEM.md` | PROPOSED (خارج RULE-01..05) | جدول أسلحة غير ملزم — موثق كفرضية ✅ | يحتاج hit validation قبل التنفيذ |

- **نقطة قوة:** كل وثيقة نظام تنتهي باختبارات مطلوبة — قابلة للتحويل لـ Test Cases.
- **نقطة ضعف طفيفة:** `COMBAT_SYSTEM.md` لا يذكر تفاعل الضرر مع Vitality/Collapse — يحتاج توضيح.

### 3.3 المعمارية

| الوثيقة | التقييم |
|---|---|
| `TECHNICAL_ARCHITECTURE.md` | محدث — يذكر `global.json` + `Directory.Packages.props` + `DOMAIN_CORE.md`. طبقات 5 واضحة. |
| `NETWORK_ARCHITECTURE.md` | ممتاز — monotonic + RequestId + rate limits + اختبار tick rates. يذكر `SecurityTests`. |
| `BACKEND_AND_HOSTING.md` | سليم — لا اختيار مزود (ADR-003). خيار PlayFab/Nakama للمقارنة فقط. |
| `DATA_ARCHITECTURE.md` | جيد — تصنيف 5 أنواع بيانات + مبدأ لا DB لكل tick. |
| `DEPLOYMENT_ARCHITECTURE.md` | محدث — يميز Local/CI/Staging/Production وحالة كل منها. |
| `DOMAIN_CORE.md` | **ممتاز** — يوثق 8 ملفات CS + 6 suites + أدوات + كيف يُختبر. يفصل PROPOSED عن VERIFIED. |

- لا تناقض معماري — ADR-001 PROPOSED، ADR-002 APPROVED، ADR-003 PROPOSED.

### 3.4 الأمن

| الوثيقة | التقييم |
|---|---|
| `SECURITY_ARCHITECTURE.md` | مبادئ صحيحة (أقل صلاحية، لا client trust) — لكن بلا تفصيل ضوابط. |
| `TRUST_BOUNDARIES.md` | ممتاز — يسمي المناطق 5 + يرفض 6 هجمات صريحة (SetHealth إلخ). |
| `THREAT_MODEL.md` | جيد — 9 تهديدات + جدول ضوابط — لكن كلها "غير منفذ" (متوقع). يحتاج Abuse Cases مفصلة. |
| `SECRETS_MANAGEMENT.md` | محدث — gitleaks 8.18 في CI + grep + `.gitignore`. |
| `DATA_PRIVACY.md` | قصير لكن صحيح (TBD حتى ADR-003). |
| `INCIDENT_RESPONSE.md` | دورة 7 مراحل + 4 مستويات أولوية — جيد لـ Pre-production. |

- **قوة:** لا أسرار في المستودع (فحص `grep` + gitleaks).
- **ضعف:** لا `SECURITY_TEST_PLAN` مفصل — لكن `SecurityTests.cs` يغطي الأساس.

### 3.5 الجودة

| الوثيقة | التقييم |
|---|---|
| `TEST_STRATEGY.md` | محدث — هرم 6 مستويات + جدول تنفيذ الحالي (Domain/Parity/Playtest). |
| `SECURITY_TEST_PLAN.md` | محدث — يربط `SecurityTests` + CI heuristic. |
| `PLAYTEST_PLAN.md` | محدث — يميز SIMULATED vs بشري، 9 مراحل، استبيان 7 أسئلة. |
| `BALANCE_RISK_REVIEW.md` | **ممتاز** — 6 مخاطر مرتبة + 5 أسئلة Betrayal + توصيات تخفيف. |
| `PERFORMANCE_BUDGET.md` | سليم — يرفض 30FPS/Redmi كـ PROPOSED. |
| `PERFORMANCE_TEST_PROTOCOL.md` | جيد — baseline + warm-up + median/P95. |
| `DEVICE_COMPATIBILITY.md` | قصير — TBD حتى baseline (صحيح). |

### 3.6 العمليات

| الوثيقة | التقييم |
|---|---|
| `BUILD_AND_RELEASE.md` | محدث — CI الحالي + المسار بعد Unity + تحذير القوالب. |
| `MONITORING_AND_LOGGING.md` | محدث — لا تسجيل PII + artifacts كبداية. |
| `BACKUP_AND_RECOVERY.md` | قصير — يوضح أن Snapshot ليس Backup (مهم لـ CONFLICT #13). |

### 3.7 التسليم (Roadmap)

| الوثيقة | التقييم |
|---|---|
| `MVP_SCOPE.md` | واضح — داخل/خارج النطاق + 5 بوابات دخول. |
| `ACCEPTANCE_CRITERIA.md` | ممتاز — 19 بنداً (Gameplay/Network/Stability/Security/Delivery) + جدول Requirement→دليل. |
| `DELIVERY_PLAN.md` | محدث — بوابة 3 ✅ PROPOSED، بوابات 4-6 PROPOSED. |

---

## 4) تدقيق الكود — Domain Core (`src/CostOfStaying.Domain`)

### 4.1 البنية (895 سطر)

| الملف | السطور | الدور | الجودة |
|---|---|---|---|
| `MatchConfig.cs` | 63 | إعدادات قابلة للضبط — فصل RULE (4 قيم) عن TEST VALUE (8 قيم) | ممتاز — تعليقات TBD واضحة |
| `Enums.cs` | 55 | 4 حالات لاعب + 7 حالات مباراة + 5 نتائج + 3 أصوات | صحيح — Draw منفصلة ✅ |
| `Clock.cs` | 44 | IMatchClock + ManualClock + MonotonicClock | ممتاز — MonotonicClock يستخدم Stopwatch |
| `Debuff.cs` | 34 | قيمة + مدة + قابلية نقل | سليم — Clone() محفوظة |
| `Player.cs` | 86 | Vitality + BaseDecay + Debuffs + Betrayal + Spirit | جيد — TotalDecay محسب صحيح |
| `DomainEvent.cs` | 43 | 12 نوع حدث + طابع زمني | جيد — قابل لـ replication |
| `Intent.cs` | 41 | Client Intents (Transfer/Vote/Hint/Echo) | سليم — تفصل نية العميل عن قرار الخادم |
| `Match.cs` | 529 | التجميع الخادمي الوحيد | **ممتاز** — انظر 4.2 |

- **قوة معمارية:** لا `MonoBehaviour`/`FishNet`/`UnityEngine` — محمول لـ `netstandard2.1` → Unity 6.
- **تثبيت:** `global.json` 8.0.400 + `Directory.Packages.props` — لا نطاقات مفتوحة.

### 4.2 تحليل `Match.cs` سطراً بسطر (مطابقة RULE-01..05)

| القاعدة | المنطق | التقييم |
|---|---|---|
| **RULE-01** Vitality 100→0 | `Tick()` يطبق `Vitality -= TotalDecay*dt` + `max(0,...)` | ✅ صحيح — ساعة خادم فقط |
| Collapse | `ApplyCollapse()` يزيد `BaseDecay += 0.02` + يضيف Debuff (max 3) + يعيد جدولة `NextCollapseAt` | ✅ مطابق `COLLAPSE_SYSTEM` |
| إعادة الجدولة | `RescheduleCollapses()` بنسبة `Remaining/OldInterval` | ✅ مطابقة الصيغة في الوثيقة |
| الموت | `TransitionToSpirit()` عند `Vitality<=0` — مرة واحدة Alive→Spirit | ✅ لا عودة |
| **RULE-02** Move | `TryTransfer()` — 7 تحققات → `Remove()` من المصدر + `Add()` للهدف بنفس المدة | ✅ ذري، لا Copy |
| الشروط 6 | Alive×2, هدف≠مصدر, Debuff قابل, ≤2م, cooldown, سعة | ✅ كلها خادمية |
| Idempotency | `ProcessedRequestIds` + `RequestId` — duplicate → `RejectedDuplicateRequest` | ✅ يمنع replay |
| Betrayal | `BetrayalPoints++` فقط عند `Success` | ✅ لا يزداد عند الفشل |
| **RULE-03** تصويت | `OpenVote()` عند `Alive==3` + `VoteOpen 20ث` + `Votes[3]=Abstain` | ✅ مرة واحدة (`VoteHasBeenOpened`) |
| الإجماع | `CloseVote()` — `unanimous = All(Yes)` → CollectiveSurrender وإلا Running + إعادة جدولة 15ث | ✅ Abstain/disconnect/timeout=No |
| **RULE-04** Spirit | `CollectEcho()` — مرة واحدة لكل ID + +1 WitnessScore | ✅ محايد |
| تلميح | `TrySendHint()` — cooldown 15ث + Spirit فقط | ✅ لا يكشف موقع |
| **RULE-05** 5 نتائج | `EvaluateEndConditions()` — MutualCollapse(0 alive+hasDeath) / TimeLimit/Draw / LastSurvivor(1+hasDeath) | ✅ Draw منفصلة |

- **إصلاح مهم في f6f1ad5:** شرط `hasDeath` يمنع إنهاء مباراة لم تبدأ (1 لاعب = لا يفوز فوراً).

### 4.3 قضايا دقيقة وجدت

| الشدة | المشكلة | الموقع | الأثر | الإصلاح المقترح |
|---|---|---|---|---|
| **متوسطة** | `MatchOutcome.None` يبقى حتى النهاية — الاختبارات تعتمد `Outcome==None` كـ "لم تنته" | `Enums.cs:None` | غامض — `Running` حالة المباراة هي الأوضح | أعد تسمية `None→Undecided` أو أضف `IsCompleted` helper |
| **منخفضة** | `Player.NextCollapseAt` يبدأ عند `CurrentCollapseInterval` (20ث) حتى قبل `Start()` — إن أُضيف لاعب بعد البدء قد يحمل توقيت قديم | `Match.AddPlayer` لا يضبط collapse | لا يؤثر في MVP (4 لاعبين قبل Start) | اضبط `NextCollapseAt` عند `Start()` فقط — موجود ✅ (تم) |
| **منخفضة** | `Debuff.Clone()` غير مستخدم — `TryTransfer` ينشئ جديداً يدوياً | `Debuff.cs:Clone` | كود ميت | احذف أو استخدمه |
| **منخفضة** | `Intent.SentAtSeconds` لا يُحقق — ثغرة مستقبلية | `Intent.cs` | العميل قد يزور الوقت | أضف تحقق `Now - SentAt < threshold` عند ربط الشبكة |

**لا ثغرات أمنية منطقية** — كل التحولات خادمية.

---

## 5) تدقيق الاختبارات (`tests/CostOfStaying.Domain.Tests`)

| الملف | الحالات | يغطي | الجودة |
|---|---|---|---|
| `VitalityTests.cs` | 6 | تآكل، Collapse، موت→Spirit، انتهاء Debuff، ساعة الخادم | جيد — حالة الموت مرة واحدة ممتازة |
| `TransferTests.cs` | 10 | نجاح Move + حفظ المدة + 7 رفض + idempotency + آخر قابل للنقل + atomicity | **ممتاز** — يغطي كل شروط RULE-02 |
| `CollapseTests.cs` | 3 | حفظ النسبة، لا قتل فوراً، سقف 3 أعباء | جيد — يحتاج اختبار تزامن موت+Collapse |
| `VoteAndEndgameTests.cs` | 8 | فتح تصويت، إجماع، عدم إجماع، امتناع=No، مرة واحدة، 3 نهايات (Last/Mutual/TimeLimit/Draw) | ممتاز — Draw مفصولة ✅ |
| `SpiritTests.cs` | 5 | جمع مرة واحدة، منع Alive، cooldown تلميح، لا نقل، لا تآكل | جيد |
| `SecurityTests.cs` | 4 | عدم تعديل مباشر، رفض مسافة مزعومة، replay، Betrayal فقط عند النجاح | جيد — أساس ADR-002 |

- **الإجمالي:** ~32 اختباراً — كافٍ لـ PROPOSED، لكن **لا يقيس الأداء أو الشبكة**.
- **تغطية مقدرة:** منطق Domain ~85% (تقدير يدوي) — لا قياس `coverlet` بعد (CI يجمعه).
- **نقطة قوة:** `SecurityTests` يثبت أن العميل لا يملك الحالة.
- **فجوة:** لا اختبار لـ `DomainEvent` ordering أو `Tick(0)` أو `Tick(-1)` (حواف).

---

## 6) تدقيق الأدوات (`tools/`)

| الأداة | السطور | الدور | الجودة |
|---|---|---|---|
| `audit.py` | 113 | 7 فحوص حوكمة | **ممتاز** — يستثني `playtest-logs/`، يميز `PROPOSED\|لا يوجد VERIFIED`، يتحقق الكتالوج والفهرس. 0 Issues. |
| `balance_simulator/simulate.py` | 316 | محاكي Python مطابق لـ C# | جيد — لكنه يكرر منطق Domain (خطر انحراف). `test_parity.py` يخففه جزئياً. |
| `presets.json` | 60 | 3 presets (default/fast/slow) | جيد — قيم موحدة مع `MatchConfig.cs` لكن التكرار يدوي. |
| `sweep.py` | 63 | مسح 3×3×3 × runs | جيد — ينتج CSV. |
| `test_parity.py` | 45 | يتحقق تآكل 10ث + نقل Move | **ضعيف** — حالتان فقط! يحتاج 6 حالات على الأقل. |
| `playtest/run_sessions.py` | 146 | 10 جلسات + report.md | جيد — لكن نتائجه أحادية (انظر §10). |

**مشكلة توحيد:** `simulate.py` و`Match.cs` يحملان نفس الصيغ مكررة — أي تغيير في C# لا ينعكس تلقائياً في Python. يحتاج توليداً من `MatchConfig.cs` أو اختبار parity أقوى.

---

## 7) تدقيق CI/CD (`.github/workflows/`)

| الملف | المشغل | الدور | التقييم |
|---|---|---|---|
| `ci.yml` | `push`/`pull_request` + `workflow_dispatch` | 7 خطوات: checkout → .NET 8 → Python 3.11 → audit → restore/build/test → parity → simulate → 10 sessions → gitleaks+grep → artifacts | **ممتاز** — بوابة حقيقية. يرفع `TestResults` + `sim.json` + `playtest-logs`. |
| `codeql.yml` | `workflow_dispatch` فقط | يطبع "Not configured" ويخرج 1 | ✅ معطل بوعي — لا يضلل كـ quality gate |
| `contrast-scan.yml` | `workflow_dispatch` | — | ✅ معطل — يذكر "Requires artifact and secrets" |
| `debricked.yml` | `workflow_dispatch` | — | ✅ معطل |
| `frogbot-scan-and-fix.yml` | `workflow_dispatch` | — | ✅ معطل + لا PR تلقائي |
| `mobsf.yml` | `workflow_dispatch` | — | ✅ معطل — "No Android artifact" |
| `nowsecure.yml` | `workflow_dispatch` | — | ✅ معطل — `./gradlew` غير موجود |
| `securitycodescan.yml` | `workflow_dispatch` | — | ✅ معطل — لا C# project بعد (رسالة قديمة، الآن يوجد!) |

**تحسين مطلوب:** رسالة `securitycodescan.yml` تقول "There is no C# project yet" — **كاذبة الآن** (يوجد `src/`). يجب تحديثها.

**قوة CI:** `permissions: contents: read` + `timeout-minutes: 15` + `fetch-depth: 0` — إعداد آمن.

---

## 8) تدقيق الأمان العميق

| المحور | الحالة | الدليل |
|---|---|---|
| **سلطة الخادم** | ✅ APPROVED + مطبق منطقياً | `Match.cs` لا يقرأ `Intent.ClaimedDistance` — يستخدم `distanceMeters` الخادمية. `SecurityTests` يثبت الرفض. |
| **منع الإعادة** | ✅ | `RequestId` + `ProcessedRequestIds` — اختبار replay موجود. |
| **تحقق المدخلات** | ✅ جزئي | 6 شروط في `TryTransfer` + 7 تهديدات في `THREAT_MODEL`. لا `payload size` check بعد — يحتاج في طبقة الشبكة. |
| **الساعة** | ✅ | `MonotonicClock` بـ `Stopwatch` — لا `DateTime.Now`. |
| **الأسرار** | ✅ لا تسريب | `.gitignore` يحجب `*.pem/*.key/.env` + CI gitleaks+grep. `grep` فحص يدوي: 0 نتائج. |
| **التبعيات** | ⚠️ | `Directory.Packages.props` يثبت 4 حزم — لكن لا `dependabot` ولا `audit` لـ NuGet 취약점. |
| **CI** | ⚠️ | `gitleaks` heuristic فقط — ليس SAST حقيقي. القوالب 7 معطلة — لا فحص CodeQL فعلي. |
| **التهديدات** | ⚠️ | `THREAT_MODEL.md` يذكر "CI compromise لا ينطبق الآن" — صحيح لكنه سيصبح حرجاً عند إضافة Unity build. |

**لا ثغرات حرجة.** المخاطر متبقية متوسطة وتحتاج Dedicated Server.

---

## 9) تدقيق التوازن والأداء

### 9.1 نتائج المحاكاة الحالية (مقلقة)

```
10 جلسات default --seed 1..10:
  متوسط المدة: 337.0s (5.6د) — أقل من هدف 10–16د
  متوسط النقل: 87.5/مباراة — مفرط جداً (نقل كل 3.8ث)
  توزيع النتائج: LastSurvivor 10/10 (100%) — لا تنوع!
  CollectiveSurrender 0, TimeLimit 0, Draw 0, MutualCollapse 0

Sweep 54 مباراة (decay 0.06/0.10/0.14 × interval 15/20/25 × cooldown 3/5/8):
  Outcome: LastSurvivor 54/54 (100%)
```

**التشخيص:** Preset default (`baseDecay 0.10, interval 20, cooldown 5`) ينتج **دوامة نقل لا نهائية** — اللاعبون ينقلون الأعباء ذهاءً وإياباً (94 نقلة في 337ث مثال) حتى يموت ثلاثة. هذا يؤكد تحذير `BALANCE_RISK_REVIEW.md` §3.2: "التآكل + زيادة Collapse = ضغط غير خطي".

**السبب الجذري:** `simulate.py` يختار نقلاً عشوائياً كل 8% من الـ ticks (~0.8/ث) دون استراتيجية — لا يمثل سلوكاً بشرياً.

### 9.2 فجوات الأداء

- لا `PERFORMANCE_BUDGET.md` معتمد — صحيح (ينتظر baseline).
- لا قياس PSS/CPU/tick على جهاز حقيقي — متوقع.
- `NETWORK_ARCHITECTURE.md` يذكر tick 10/15/20Hz بلا قرار — صحيح.

---

## 10) تدقيق المطابقة مع الخارطة (Roadmap)

| بوابة `DELIVERY_PLAN` | المطلوب | المنجز | المتبقي |
|---|---|---|---|
| 1 Playtest RULE-01..05 | 10 جلسات بشرية → ADR-004 APPROVED | ✅ محاكاة 10 (SIMULATED) + وثائق جاهزة | جلسات بشرية + تحليل |
| 2 ADR-001 prototype | Android+Linux build + نسخ أدوات | ❌ لا prototype | إنشاء Unity project + بناء + قياس |
| 3 Domain Core | كود + اختبارات بلا MonoBehaviour | ✅ **PROPOSED** — f6f1ad5 | CI أخضر + مراجعة |
| 4 Networking | Intent-only + Linux server | ❌ لا FishNet/Dedicated Server | ربط Domain + اختبار 2 جهاز |
| 5 Vertical Slice | 4 لاعبين + خريطة | ❌ | بعد 4 |
| 6 Gates | أمن/أداء/استقرار/قبول | ❌ | ADRs + قياسات |

- **الترتيب سليم** — لا تخطي بوابات.

---

## 11) نقاط القوة والضعف

### ✅ نقاط قوة (8)
1. **حوكمة ممتازة:** 7 مبادئ + كتالوج + مصادر موحدة — نادرة في Pre-production.
2. **فصل Domain Core:** `netstandard2.1` بلا Unity — يسمح باختبار التوازن مبكراً.
3. **مطابقة RULE-01..05:** دقيقة حرفياً، بما فيها Draw المنفصلة وhasDeath fix.
4. **اختبارات شاملة:** 32 حالة تغطي كل المسارات السعيدة والفاشلة.
5. **توحيد الأرقام:** جدول `SOURCES_AND_REFERENCES.md` يمنع "30FPS الموروث".
6. **CI حقيقي:** بوابة واحدة نشطة + 7 معطلة بوعي (لا يضلل).
7. **مخاطر موثقة:** `BALANCE_RISK_REVIEW.md` تتنبأ بالدوامة قبل أن تحدث.
8. **لا ديون أمنية:** لا أسرار، لا Critical مفتوحة.

### ⚠️ نقاط ضعف / فجوات (12)

| # | الفجوة | الشدة | الملفات |
|---|---|---|---|
| 1 | توزيع النتائج أحادي (100% LastSurvivor) — لا يختبر 4 من 5 نتائج | عالية | `simulate.py` سلوك عشوائي |
| 2 | `test_parity.py` ضعيف (2 حالات) — قد ينحرف Python عن C# | عالية | `test_parity.py` |
| 3 | لا اختبار شبكة حقيقي (RTT/loss/jitter) | عالية | `NETWORK_ARCHITECTURE.md` TBD |
| 4 | رسالة `securitycodescan.yml` كاذبة ("No C# project") | متوسطة | `.github/workflows/securitycodescan.yml` |
| 5 | `Debuff.Clone()` ميتة + `Intent.SentAt` غير محقق | منخفضة | `Debuff.cs`, `Intent.cs` |
| 6 | `COMBAT_SYSTEM` لا يربط الضرر بـ Vitality | متوسطة | `COMBAT_SYSTEM.md` |
| 7 | لا Unity project — بوابة 2 معلقة | عالية | `ADR-001` |
| 8 | لا `dependabot`/`dotnet audit` | منخفضة | `Directory.Packages.props` |
| 9 | `playtest-logs/` يحتوي أدلة محاكاة فقط — لا جلسات بشرية | عالية | `PLAYTEST_PLAN.md` |
| 10 | `TECHNICAL_ARCHITECTURE` لا يذكر `global.json` حتى f6f1ad5 (تم) — لكن لا يذكر إصدار FishNet TBD بوضوح | منخفضة | `SOURCES_AND_REFERENCES.md` EXT-FISHNET TBD |
| 11 | `DECISIONS_LOG.md` بلا ترويسة حالة — يسبب Warn دائم | منخفضة | `audit.py` Warn |
| 12 | لا مثال `CONTRIBUTING.md` يذكر كيف تضيف ADR جديداً | منخفضة | `CONTRIBUTING.md` |

---

## 12) التوصيات الفورية (قبل أي ميزة جديدة)

1. **إصلاح `securitycodescan.yml`** — حدث الرسالة إلى "C# Domain Core exists, but Unity not yet".
2. **قوي `test_parity.py`** — أضف 4 حالات: Collapse + vote + Spirit + تزامن.
3. **حسن `simulate.py`** — اجعل سلوك النقل استراتيجياً (لا 8% عشوائي) لاختبار Draw/TimeLimit/MutualCollapse.
4. **أضف `dependabot.yml`** لـ NuGet + `dotnet list package --vulnerable` في CI.

---

## 13) الخلاصة الرقمية

- **الوثائق:** 49 (26 PROPOSED, 6 PROPOSED FOR PLAYTEST, 1 APPROVED, 16 PROPOSED آخر) — لا ادعاء كاذب.
- **الكود:** 1643 سطر Domain+Tests — يطابق 100% من RULE-01..05.
- **الأدوات:** 683 سطر Python — تعمل لكنها تحاكي سلوكاً مثالياً.
- **الفروع:** 2 — تاريخ نظيف.
- **المخاطر الحرجة:** 0 — لا أسرار، لا تضارب حوكمي، لا كود غير موثق.

---

*انتهى التدقيق — التفاصيل التنفيذية في `WORK_PLAN` المرافق.*
