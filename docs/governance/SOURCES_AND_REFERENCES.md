# المصادر والمراجع الموحدة — v1.0
**الحالة: PROPOSED | مصدر الحقيقة لتوحيد الاستشهادات | آخر مراجعة: 2026-11-14**

هذه الوثيقة توحّد كل المصادر التي تستند إليها وثائق المشروع. أي وثيقة جديدة يجب أن تستشهد من هنا، ولا تستورد مرجعاً خارجياً دون إضافته هنا أولاً (مبدأ عدم التضارب).

## 1. القواعد الذهبية للاستشهاد

1. **لا رقم بلا مصدر:** كل قيمة رقمية (Vitality 100، تصويت 20ث، 15ث، حد 20د) مصدرها `02_GAME_RULES_SPECIFICATION.md` و`ADR-004`. إن لم تكن هناك فهي `PROPOSED TEST VALUE`.
2. **لا أداء بلا قياس:** أي ادعاء FPS/ذاكرة/شبكة يجب أن يحمل commit SHA + بيئة + أداة قياس. انظر `PERFORMANCE_TEST_PROTOCOL.md`.
3. **لا أمن بلا دليل:** أي ادعاء حماية يجب أن يحمل Test ID في `REQUIREMENTS_TRACEABILITY.md` ونتيجة تشغيل في CI.
4. **الأولوية عند التعارض:** `ADR` > `02_GAME_RULES_SPECIFICATION` > `docs/systems/*` > `docs/architecture/*` (محدد في `DOCUMENT_GOVERNANCE.md` §3).

## 2. المصادر الداخلية (Canonical)

| المعرف | الوثيقة | يغطي | الحالة |
|---|---|---|---|
| SRC-CHARTER | `00_PROJECT_CHARTER.md` | الرؤية والنطاق وبوابات التسليم | PROPOSED |
| SRC-RULES | `02_GAME_RULES_SPECIFICATION.md` | RULE-01..05 (الأرقام المعتمدة فقط) | PROPOSED FOR PLAYTEST |
| SRC-GDD | `01_GAME_DESIGN_DOCUMENT.md` | الحلقة وأعمدة اللعب | PROPOSED FOR PLAYTEST |
| SRC-GLOSSARY | `GLOSSARY.md` | تعريفات المصطلحات | PROPOSED |
| SRC-DOMAIN | `architecture/DOMAIN_CORE.md` + `src/CostOfStaying.Domain/` | تنفيذ النواة المجردة | PROPOSED |
| SRC-ADR-001 | `governance/adr/ADR-001-TECH-STACK.md` | تكدس Unity/FishNet | PROPOSED |
| SRC-ADR-002 | `governance/adr/ADR-002-SERVER-AUTHORITY.md` | سلطة الخادم | APPROVED (مبدأ) |
| SRC-ADR-003 | `governance/adr/ADR-003-BACKEND-AND-HOSTING.md` | Backend | PROPOSED |
| SRC-ADR-004 | `governance/adr/ADR-004-GAMEPLAY-RULES-BASELINE.md` | خط أساس القواعد | PROPOSED FOR PLAYTEST |
| SRC-ADR-005 | `governance/adr/ADR-005-CI-AND-DOMAIN-CORE.md` | CI + Domain Core | PROPOSED |
| SRC-CONFLICT | `governance/CONFLICT_RESOLUTIONS.md` | حسم التعارضات (18 بنداً) | PROPOSED |
| SRC-TRACE | `governance/REQUIREMENTS_TRACEABILITY.md` | ربط المتطلب بالاختبار | PROPOSED |

> أي وثيقة في `docs/systems/` أو `architecture/` أو `security/` أو `quality/` يجب أن تذكر في رأسها أي SRC تعتمد عليه. لا تفترض قاعدة غير مذكورة في SRC-RULES.

## 3. المصادر الخارجية (External)

| المعرف | المصدر | الإصدار المثبت | الاستخدام | الرابط |
|---|---|---|---|---|
| EXT-UNITY | Unity LTS | **6.3 LTS** (مقترح، ADR-001) | محرك اللعبة | https://unity.com/releases/editor/whats-new/unity-6 |
| EXT-URP | Universal Render Pipeline | ضمن Unity 6.3 | الرسم | https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest |
| EXT-FISHNET | FishNet | TBD (يُثبت بعد prototype) | الشبكة | https://fish-networking.gitbook.io/docs/ |
| EXT-DOTNET | .NET SDK | 8.0.x (CI) / netstandard2.1 (Domain) | بناء النواة | https://dotnet.microsoft.com/ |
| EXT-NUNIT | NUnit | 4.2.2 | اختبارات Domain | https://nunit.org/ |
| EXT-GITLEAKS | gitleaks | 8.18.0 (CI heuristic) | فحص الأسرار | https://github.com/gitleaks/gitleaks |
| EXT-PYTHON | Python | 3.11 (CI) | أدوات المحاكاة | https://www.python.org/ |

**قاعدة التثبيت:** لا يُعتمد إصدار خارجي نهائياً قبل تثبيته في `global.json` / `Directory.Packages.props` / `presets.json` واختباره في CI. الأرقام أعلاه مقترحة (ADR-001 PROPOSED).

### مراجع الأمان والأداء

| المعرف | المصدر | الاستخدام |
|---|---|---|
| EXT-OWASP-ASVS | OWASP ASVS 4.0 | معيار ضوابط الأمان (مشار إليه في `SECURITY_ARCHITECTURE.md`) |
| EXT-OWASP-MASVS | OWASP MASVS | أمان Android (مرجع لـ `SECURITY_TEST_PLAN.md`) |
| EXT-STRIDE | STRIDE | نموذج التهديد (`THREAT_MODEL.md`) |
| EXT-CWE | CWE Top 25 | تصنيف الثغرات |

## 4. توحيد الأرقام — ما هو معتمد وما هو فرضية

| الرقم | القيمة | المصدر | الحالة |
|---|---|---|---|
| Vitality الابتدائية | 100 | SRC-RULES RULE-01 | PROPOSED FOR PLAYTEST |
| مدة التصويت | 20 ثانية | SRC-RULES RULE-03 | PROPOSED FOR PLAYTEST |
| فترة Collapse عند 3 أحياء | 15 ثانية | SRC-RULES RULE-03 | قيمة أولية (initial value) |
| حد المباراة | 20 دقيقة (1200ث) | SRC-RULES RULE-05 | قيمة أولية |
| مدى Transfer | 2 متر | SRC-RULES RULE-02 | قيمة اختبار أولية |
| معدل التآكل | 0.10/s | `MatchConfig.cs` / `presets.json` | PROPOSED TEST VALUE (TBD) |
| زيادة Collapse | 0.02/s | `MatchConfig.cs` | PROPOSED TEST VALUE |
| مدة Debuff | 30 ثانية | `MatchConfig.cs` | PROPOSED TEST VALUE |
| التبريد | 5 ثوانٍ | `MatchConfig.cs` | PROPOSED TEST VALUE |
| أرقام الأسلحة | جدول Combat | `COMBAT_SYSTEM.md` | فرضية غير ملزمة |
| عتبات Betrayal 3/5/7 | — | `BETRAYAL_SYSTEM.md` | فرضية غير معتمدة (18) |
| 30FPS / Redmi Note 8 | — | — | موروث مرفوض (CONFLICT #8) |

> **لا تستشهد بجدول فترات Collapse الموروث (8+ أحياء 30ث ...) كقاعدة.** الوحيد المسمى في SRC-RULES هو 15ث.

## 5. كيف تستشهد في وثيقة جديدة

```markdown
## المراجع
- القاعدة: `02_GAME_RULES_SPECIFICATION.md` RULE-02 (Move ذري)
- التنفيذ: `src/CostOfStaying.Domain/Match.cs::TryTransfer`
- الاختبار: `tests/.../TransferTests.cs::Transfer_Moves_DeBuff`
- الأداة: `tools/balance_simulator/simulate.py` preset=default
```

## 6. الصيانة

- أي تغيير في RULE يحدّث `SRC-RULES` + وثيقة النظام + `SRC-ADR-004` + `MatchConfig.cs` + هذا الملف.
- أي تثبيت إصدار خارجي جديد يحدّث `EXT-*` و`global.json` / `Directory.Packages.props` و`TECHNICAL_ARCHITECTURE.md`.
- المراجعة الدورية: عند كل ADR جديد أو كل 10 جلسات Playtest.
