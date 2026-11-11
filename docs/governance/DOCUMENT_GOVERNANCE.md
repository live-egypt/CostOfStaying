# حوكمة الوثائق — v2.1
**الحالة: PROPOSED | آخر مراجعة: 2026-11-11 | المرحلة: Pre-production**

هذه الوثيقة مصدر الحقيقة لتنظيم التوثيق والحوكمة. لا تغيّر حالة أي ADR ولا تعني أن الأنظمة منفذة.

## 1. الدخول
- الفهرس البشري: [docs/README.md](../README.md)
- هذه الوثيقة: الكتالوج وقواعد التعارض والحالة
- ما حُسم: [CONFLICT_RESOLUTIONS.md](CONFLICT_RESOLUTIONS.md)
- قرارات التصميم: [DECISIONS_LOG.md](DECISIONS_LOG.md) وملفات `adr/`

## 2. الطبقات
| الطبقة | المسار | الدور |
|---|---|---|
| المنتج | `docs/00` إلى `03` و`GLOSSARY.md` | الرؤية، القواعد، الحلقة، المصطلحات |
| الأنظمة | `docs/systems/` | تفصيل قابل للتنفيذ للقواعد؛ لا يخالف مواصفة القواعد |
| المعمارية | `docs/architecture/` | كيف يُبنى النظام؛ يتبع حالة ADR |
| الأمن | `docs/security/` | الضوابط والتهديدات؛ لا يثبت التنفيذ |
| الجودة | `docs/quality/` | كيف يُتحقق من التصميم |
| العمليات | `docs/operations/` | البناء والتشغيل بعد وجود كود |
| الحوكمة | `docs/governance/` | القرارات والتتبع وهذه القواعد |
| التسليم | `docs/roadmap/` | النطاق والقبول وخطة المراحل |

## 3. قواعد التعارض
عند اختلاف نصين، اطبّق هذا الترتيب ولا تستنتج قراراً من النص الأقدم:
1. ملف ADR هو مصدر الحقيقة لحالة قراره. [DECISIONS_LOG.md](DECISIONS_LOG.md) فهرس فقط ويجب أن يطابق الملف.
2. [02_GAME_RULES_SPECIFICATION.md](../02_GAME_RULES_SPECIFICATION.md) هو مصدر الحقيقة لقواعد RULE-01..05 ما دامت ADR-004 في حالة PROPOSED FOR PLAYTEST. وثائق `systems/` تفصّل ولا تستبدل القاعدة.
3. [00_PROJECT_CHARTER.md](../00_PROJECT_CHARTER.md) هو مصدر نطاق المنتج. `roadmap/` يفصّل القبول ولا يوسّع النطاق صامتاً.
4. ADR-002 هو مصدر مبدأ سلطة الخادم. وثائق الشبكة والأمن تشرحه ولا تضعفه.
5. هذا الكتالوج هو قائمة الوثائق المعتمدة داخل الحزمة. وثيقة غير مدرجة هنا ليست جزءاً من الحوكمة حتى تُضاف في نفس الالتزام.

## 4. حالات موحّدة
- **PROPOSED:** مقترح لم يُعتمد.
- **PROPOSED FOR PLAYTEST:** خط أساس يحتاج جلسات قبل الاعتماد.
- **APPROVED:** قرار معتمد تصميمياً. لا يعني منفذاً.
- **IMPLEMENTED:** منفذ في commit محدد قابل للمراجعة.
- **VERIFIED:** اجتاز اختباراً محدداً وله دليل محفوظ.
- **TBD:** لم يُحسم.
- **NOT IMPLEMENTED:** وصف تنفيذ فقط، كما في ADR-002.

لا تستخدم IMPLEMENTED أو VERIFIED لمجرد وجود وثيقة.

## 5. قواعد التحديث
- أي وثيقة جديدة تُضاف في هذا الكتالوج وفي [docs/README.md](../README.md) في نفس الالتزام.
- تغيير حالة ADR يحدّث ملفه و[DECISIONS_LOG.md](DECISIONS_LOG.md) معاً.
- تغيير قاعدة يحدّث مواصفة القواعد ووثيقة النظام المقابل وADR-004 عند الحاجة.
- لا تُعاد تسمية المسارات القائمة دون سبب موثق.
- رؤوس الوثائق الحالية v2.1. رقم الرأس لا يرفع الحالة.

## 6. الكتالوج
| المسار | الطبقة | الحالة |
|---|---|---|
| `00_PROJECT_CHARTER.md` | منتج | PROPOSED |
| `01_GAME_DESIGN_DOCUMENT.md` | منتج | PROPOSED FOR PLAYTEST |
| `02_GAME_RULES_SPECIFICATION.md` | منتج / قواعد | PROPOSED FOR PLAYTEST |
| `03_PLAYER_EXPERIENCE_AND_CORE_LOOP.md` | منتج | PROPOSED FOR PLAYTEST |
| `GLOSSARY.md` | منتج | PROPOSED |
| `systems/COLLAPSE_SYSTEM.md` | نظام | PROPOSED FOR PLAYTEST |
| `systems/SUFFERING_TRANSFER_SYSTEM.md` | نظام | PROPOSED FOR PLAYTEST |
| `systems/BETRAYAL_SYSTEM.md` | نظام | PROPOSED FOR PLAYTEST |
| `systems/COMBAT_SYSTEM.md` | نظام | PROPOSED |
| `systems/SPIRIT_SYSTEM.md` | نظام | PROPOSED FOR PLAYTEST |
| `systems/ENDGAME_SYSTEM.md` | نظام | PROPOSED FOR PLAYTEST |
| `architecture/TECHNICAL_ARCHITECTURE.md` | معمارية | PROPOSED |
| `architecture/NETWORK_ARCHITECTURE.md` | معمارية | PROPOSED |
| `architecture/BACKEND_AND_HOSTING.md` | معمارية | PROPOSED |
| `architecture/DATA_ARCHITECTURE.md` | معمارية | PROPOSED |
| `architecture/DEPLOYMENT_ARCHITECTURE.md` | معمارية | PROPOSED |
| `security/SECURITY_ARCHITECTURE.md` | أمن | PROPOSED |
| `security/TRUST_BOUNDARIES.md` | أمن | PROPOSED |
| `security/THREAT_MODEL.md` | أمن | PROPOSED |
| `security/SECRETS_MANAGEMENT.md` | أمن | PROPOSED |
| `security/DATA_PRIVACY.md` | أمن | PROPOSED |
| `security/INCIDENT_RESPONSE.md` | أمن | PROPOSED |
| `quality/TEST_STRATEGY.md` | جودة | PROPOSED |
| `quality/SECURITY_TEST_PLAN.md` | جودة | PROPOSED |
| `quality/PLAYTEST_PLAN.md` | جودة | PROPOSED |
| `quality/BALANCE_RISK_REVIEW.md` | جودة | PROPOSED |
| `quality/PERFORMANCE_BUDGET.md` | جودة | PROPOSED |
| `quality/PERFORMANCE_TEST_PROTOCOL.md` | جودة | PROPOSED |
| `quality/DEVICE_COMPATIBILITY.md` | جودة | PROPOSED |
| `operations/BUILD_AND_RELEASE.md` | عمليات | PROPOSED |
| `operations/MONITORING_AND_LOGGING.md` | عمليات | PROPOSED |
| `operations/BACKUP_AND_RECOVERY.md` | عمليات | PROPOSED |
| `governance/DOCUMENT_GOVERNANCE.md` | حوكمة | PROPOSED |
| `governance/CONFLICT_RESOLUTIONS.md` | حوكمة | PROPOSED |
| `governance/DECISIONS_LOG.md` | حوكمة | فهرس ADR |
| `governance/REQUIREMENTS_TRACEABILITY.md` | حوكمة | PROPOSED |
| `governance/adr/ADR-001-TECH-STACK.md` | قرار | PROPOSED |
| `governance/adr/ADR-002-SERVER-AUTHORITY.md` | قرار | APPROVED كمبدأ / NOT IMPLEMENTED |
| `governance/adr/ADR-003-BACKEND-AND-HOSTING.md` | قرار | PROPOSED |
| `governance/adr/ADR-004-GAMEPLAY-RULES-BASELINE.md` | قرار | PROPOSED FOR PLAYTEST |
| `roadmap/MVP_SCOPE.md` | تسليم | PROPOSED |
| `roadmap/ACCEPTANCE_CRITERIA.md` | تسليم | PROPOSED |
| `roadmap/DELIVERY_PLAN.md` | تسليم | PROPOSED |

لا كود لعبة ولا خادم ولا اختبار منفذ. ملفات `.github/workflows` قوالب متوقفة على `workflow_dispatch` وليست دليل فحص ناجح.
