# لقطة GitHub المستودع والفروع وPRs — 2026-11-12

**المستودع:** https://github.com/live-egypt/CostOfStaying  
**المعرف:** 1439184361  
**الرؤية:** Public  
**Default branch:** `main`  
**Main HEAD وقت الفحص:** `2c68f748360b78a8bb4d86b8bf693b4c2ce07216`  
**آخر دفع ظاهر وقت الفحص:** 2026-11-12 08:31:42 UTC  
**اللقطة:** بيانات GitHub الحية كما فُحصت في 2026-11-12؛ لا تفترض أنها الحالة الحالية بعد تاريخ اللقطة.

## 1. حالة main وبنية المستودع

- Git tree recursive response: 64 entries = 53 blobs + 11 tree entries.
- `truncated=false` في الاستجابة.
- لا تظهر على `main` مسارات Core أو Assets أو Packages أو ProjectSettings أو Unity scenes؛ شجرة `main` هي README وملفات وثائق وملفات workflow.
- README على `main` يقول صراحة إن لا يوجد كود لعبة أو Unity project أو Dedicated Server أو اختبارات منفذة، وأن ملفات workflows يدوية فقط وليست CI منفذاً.
- وجود Core على فرع PR #6 أو ZIP على فرع PR #5 لا يغيّر وصف `main`.

## 2. المسارات الـ53 الموجودة على main وقت اللقطة

```text
.github/workflows/README.md
.github/workflows/codeql.yml
.github/workflows/contrast-scan.yml
.github/workflows/debricked.yml
.github/workflows/frogbot-scan-and-fix.yml
.github/workflows/mobsf.yml
.github/workflows/nowsecure.yml
.github/workflows/securitycodescan.yml
README.md
docs/00_PROJECT_CHARTER.md
docs/01_GAME_DESIGN_DOCUMENT.md
docs/02_GAME_RULES_SPECIFICATION.md
docs/03_PLAYER_EXPERIENCE_AND_CORE_LOOP.md
docs/GLOSSARY.md
docs/README.md
docs/architecture/BACKEND_AND_HOSTING.md
docs/architecture/DATA_ARCHITECTURE.md
docs/architecture/DEPLOYMENT_ARCHITECTURE.md
docs/architecture/NETWORK_ARCHITECTURE.md
docs/architecture/TECHNICAL_ARCHITECTURE.md
docs/governance/CONFLICT_RESOLUTIONS.md
docs/governance/DECISIONS_LOG.md
docs/governance/DOCUMENT_GOVERNANCE.md
docs/governance/REQUIREMENTS_TRACEABILITY.md
docs/governance/adr/ADR-001-TECH-STACK.md
docs/governance/adr/ADR-002-SERVER-AUTHORITY.md
docs/governance/adr/ADR-003-BACKEND-AND-HOSTING.md
docs/governance/adr/ADR-004-GAMEPLAY-RULES-BASELINE.md
docs/operations/BACKUP_AND_RECOVERY.md
docs/operations/BUILD_AND_RELEASE.md
docs/operations/MONITORING_AND_LOGGING.md
docs/quality/BALANCE_RISK_REVIEW.md
docs/quality/DEVICE_COMPATIBILITY.md
docs/quality/PERFORMANCE_BUDGET.md
docs/quality/PERFORMANCE_TEST_PROTOCOL.md
docs/quality/PLAYTEST_PLAN.md
docs/quality/SECURITY_TEST_PLAN.md
docs/quality/TEST_STRATEGY.md
docs/roadmap/ACCEPTANCE_CRITERIA.md
docs/roadmap/DELIVERY_PLAN.md
docs/roadmap/MVP_SCOPE.md
docs/security/DATA_PRIVACY.md
docs/security/INCIDENT_RESPONSE.md
docs/security/SECRETS_MANAGEMENT.md
docs/security/SECURITY_ARCHITECTURE.md
docs/security/THREAT_MODEL.md
docs/security/TRUST_BOUNDARIES.md
docs/systems/BETRAYAL_SYSTEM.md
docs/systems/COLLAPSE_SYSTEM.md
docs/systems/COMBAT_SYSTEM.md
docs/systems/ENDGAME_SYSTEM.md
docs/systems/SPIRIT_SYSTEM.md
docs/systems/SUFFERING_TRANSFER_SYSTEM.md
```

## 3. الفروع الـ21 في لقطة ما قبل إنشاء فرع النقل

| Branch | HEAD SHA |
|---|---|
| `audit/p0-ci-and-architecture-decisions` | `0f902cb142e1f5bb9f9dac2e317c8cb9b3323c1f` |
| `audit/phase1-uploaded-source-review` | `251e5ecde3a3be5d8f5552b34c293035c9579b4d` |
| `docs/a5-pdr-002-organizational-structure` | `8f830b52af79d39b16780c6beeca2165de3b53a2` |
| `docs/a5-pdr-005-lif-dec-01-supersession` | `ea82fe73a35a59616e15845b84206f33b8655be3` |
| `docs/a5-pdr-008-adr-classification-review` | `1088026c7d63c3a8bd92d28baa08bc80fb8dcfcc` |
| `docs/a5-pdr-009-tbd-register` | `62bc1266d96c3aa42aa1f88ad02f9ebc5e870e3b` |
| `docs/a5-pdr-010-provenance-register` | `202fc759410697f3550df8a6b5e2356e0d8aff65` |
| `docs/a5-pdr-011-dropped-status-review` | `3280c331e1aa034ce4c0f0a0e86562b8e810bfb2` |
| `docs/a5-pdr-012-source-inventory-crosswalk` | `c941f25cf1c4a7785875231da9078bd20ac2de4b` |
| `docs/a5-pdr-013-diff-xxx-review` | `67139bb5a5407b17f3a76f41d0803af98c24dce9` |
| `docs/a5-pdr-014-source-origin-01-review` | `24ed8e48427580716a50a2077d623b2eb8477d43` |
| `docs/project-reconciliation-baseline-2026-11-10` | `381fdd9a01b5ad0fbe549b7be0906ba783b072a4` |
| `docs/source-provenance-reconciliation-2026-11-09` | `b3237624a73ddd6f534ecef3d966dde3abc90889` |
| `docs/stage1-source-provenance-recovery` | `8e8ab7a9430c281ad4d3178549260196920c6e5e` |
| `docs/stage1-source-reconciliation` | `2f3cb398412a4c8e1fc886a35b7ff1b0776bc470` |
| `feature/core-lifecycle-resolution` | `2820285021b1a3d7ae9acd953b3bb65677c33512` |
| `feature/hotfix-cs0246` | `0c8ab87db82ed58b8ed3062519a2d95a5be35141` |
| `feature/phase1-core-foundation` | `8ad1acdd1646eccf7a0da4fd1b7ddf7083fa0790` |
| `feature/phase1-technical-consolidation` | `b59863240465894eed90f133410184158e44b2dc` |
| `main` | `2c68f748360b78a8bb4d86b8bf693b4c2ce07216` |
| `review/unity-source-package-2026-11-01` | `a5b091f68207437463835cd8fb4b07d27a84088c` |

فرع النقل الذي أنشئ لهذه الحزمة: `docs/chatgpt-project-transfer-archive-2026-11-12`، وهو متفرع من Main SHA أعلاه. أصبح عدد الفروع أعلى من 21 بعد إنشائه.

## 4. سجل PRs الكامل 1–20 في لقطة الفحص

| PR | الحالة | Head branch | Base | الخلاصة |
|---:|---|---|---|---|
| #1 | CLOSED / MERGED | `docs/v2.0` | main | حزمة الوثائق v2.1 المدمجة |
| #2 | CLOSED / NOT MERGED | `audit/phase1-uploaded-source-review` | main | تدقيق مصدر Phase 1 |
| #3 | CLOSED / NOT MERGED | `feature/phase1-core-foundation` | audit branch | Core foundations؛ لا تعتبره على main |
| #4 | CLOSED / NOT MERGED | `feature/phase1-technical-consolidation` | main | توفيق تقني؛ لا يعتبر مدمجاً |
| #5 | OPEN / DRAFT | `review/unity-source-package-2026-11-01` | main | مراجعة Unity source ZIP؛ لا compile/build proof |
| #6 | OPEN / DRAFT | `feature/core-lifecycle-resolution` | main | Core lifecycle branch؛ integration Unity/FishNet/Android/Linux غير مثبتة |
| #7 | CLOSED / MERGED | `feature/hotfix-cs0246` | feature/core-lifecycle-resolution | دمج إلى فرع Core وليس main |
| #8 | OPEN / DRAFT | `docs/stage1-source-provenance-recovery` | main | حزمة مصدر/منشأ تحت `docs/governance/sources/` |
| #9 | OPEN / DRAFT | `docs/source-provenance-reconciliation-2026-11-09` | feature/core-lifecycle-resolution | حزمة مصدر موسعة و85 TBD على مسار governance root |
| #10 | CLOSED / NOT MERGED | `docs/stage1-source-reconciliation` | feature branch | موصوف بأنه superseded |
| #11 | OPEN / DRAFT | `docs/project-reconciliation-baseline-2026-11-10` | main | مرشح توحيد الحوكمة والأدلة؛ لم يعتمد كمرجع نهائي |
| #12 | CLOSED / NOT MERGED | `docs/a5-pdr-002-organizational-structure` | main | PDR-002 |
| #13 | CLOSED / NOT MERGED | `docs/a5-pdr-005-lif-dec-01-supersession` | main | PDR-005 |
| #14 | CLOSED / NOT MERGED | `docs/a5-pdr-008-adr-classification-review` | main | PDR-008 |
| #15 | CLOSED / NOT MERGED | `docs/a5-pdr-009-tbd-register` | main | PDR-009 |
| #16 | CLOSED / NOT MERGED | `docs/a5-pdr-010-provenance-register` | main | PDR-010 |
| #17 | OPEN / DRAFT | `docs/a5-pdr-011-dropped-status-review` | main | PDR-011 |
| #18 | OPEN / DRAFT | `docs/a5-pdr-012-source-inventory-crosswalk` | main | PDR-012 جزئي |
| #19 | OPEN / DRAFT | `docs/a5-pdr-013-diff-xxx-review` | main | PDR-013 جزئي |
| #20 | OPEN / DRAFT | `docs/a5-pdr-014-source-origin-01-review` | main | PDR-014 لا يستطيع الحسم |

حالات PR أعلاه هي حالات GitHub وقت اللقطة. PR CLOSED / NOT MERGED لا يعني حذف الفرع أو إبطال محتواه التاريخي.

## 5. ملاحظات الفروع والحزم

- PR #8 يضيف 7 ملفات، PR #9 يضيف 14 ملفاً، PR #11 يضيف 39 ملفاً في قوائم الملفات الحالية وقت المراجعة السابقة.
- PR #8 مساره `docs/governance/sources/` وقاعدته main.
- PR #9 مساره `docs/governance/` وقاعدته feature/core-lifecycle-resolution.
- PR #11 مساره `docs/governance/` وقاعدته main، وهو مرشح تجميع وليس حزمة معتمدة.
- DOC-PACKAGE-01 لا يزال OPEN.
- PR #18 و#20 يحتويان مراجع blob أقدم من نسخ PR #11 المحدثة في 2026-11-12. هذه مشكلة مراجع/إصدارات، لا دليل على تغير الاستنتاج نفسه.
- PR #19 يوثق اختلاف المسارات والقواعد والنطاقات، لكنه لا يستعيد تعريف DIFF-XXX الأصلي ولا يغلق مقارنة PDR-016 ذات المسارات التسعة.
- لا تدمج أي PR تلقائياً ولا تحذف الفروع التاريخية.

## 6. حزمة Unity / Core خارج main

- PR #5 branch يتضمن مراجعة حزمة Unity Source ZIP بحسب جرد فرعي سابق: 94 مدخلاً، 81 C#، 4 asmdef، 2 Markdown، و7 مدخلات مجلدات. لا يوجد إثبات compile أو Android build أو Linux server runtime.
- مراجعة PR #5 السابقة سجلت اختلاف SHA بين قيمة محسوبة `5f92947c8b23d1ee15d7c744fb62ef0cf7af361bfdda277fcdaeff61dab51ac4` وقيمة manifest `351190c6789b0a146be0978606c831f48b951509e7f353bb3b469374eb9b15d9`. كما وصفت غياب Packages manifest/lock وProjectSettings/ProjectVersion والمشاهد وملفات .meta.
- PR #6 branch يوثق Core وNUnit وقرارات ADR إضافية؛ وصف PR يذكر اختبارات، لكن هذا نطاق branch فقط ولا يثبت Unity/FishNet integration.
- PR #7 merged إلى feature/core-lifecycle-resolution، لا إلى main.

## 7. ما لم تثبته هذه اللقطة

لم يُنفذ في هذه الجولة:
- فحص كل ملف من كل فرع على حدة.
- المقارنة الدلالية الكاملة لكل الوثائق المتشابهة عبر #8/#9/#11.
- تشغيل اختبارات أو builds من الكود.
- استخراج كامل محادثات ChatGPT أو DeepSeek أو Gemini.
- استعادة Library raw bytes أو محتويات أرشيفاتها.
