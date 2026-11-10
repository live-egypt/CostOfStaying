# وثائق ثمن البقاء — v2.1

هذه الوثائق تصف التصميم المقترح وخطط التحقق. لا تعني أن أي نظام منفذ أو مختبر ما لم يذكر دليل قابل للمراجعة. ابدأ من الميثاق ثم مواصفات القواعد والأنظمة، واستخدم سجلات الحوكمة لتتبع المصدر والقرار والدليل دون إنشاء مصادر حقيقة متوازية.

## ابدأ هنا
- [ميثاق المشروع](00_PROJECT_CHARTER.md)
- [Game Design Document](01_GAME_DESIGN_DOCUMENT.md)
- [مواصفات قواعد اللعبة](02_GAME_RULES_SPECIFICATION.md)
- [تجربة اللاعب والحلقة الأساسية](03_PLAYER_EXPERIENCE_AND_CORE_LOOP.md)
- [مسرد المصطلحات](GLOSSARY.md)
- [حالة المشروع والتنفيذ](PROJECT_STATUS_AND_EXECUTION_REGISTER.md)

## الأنظمة
- [Collapse](systems/COLLAPSE_SYSTEM.md)
- [Suffering Transfer](systems/SUFFERING_TRANSFER_SYSTEM.md)
- [Betrayal](systems/BETRAYAL_SYSTEM.md)
- [Combat](systems/COMBAT_SYSTEM.md)
- [Spirit](systems/SPIRIT_SYSTEM.md)
- [Endgame](systems/ENDGAME_SYSTEM.md)

## المعمارية
- [المعمارية التقنية](architecture/TECHNICAL_ARCHITECTURE.md)
- [معمارية الشبكة](architecture/NETWORK_ARCHITECTURE.md)
- [Backend والاستضافة](architecture/BACKEND_AND_HOSTING.md)
- [معمارية البيانات](architecture/DATA_ARCHITECTURE.md)
- [النشر والتشغيل](architecture/DEPLOYMENT_ARCHITECTURE.md)

## الأمن والخصوصية
- [المعمارية الأمنية](security/SECURITY_ARCHITECTURE.md)
- [حدود الثقة](security/TRUST_BOUNDARIES.md)
- [نموذج التهديدات](security/THREAT_MODEL.md)
- [إدارة الأسرار](security/SECRETS_MANAGEMENT.md)
- [خصوصية البيانات](security/DATA_PRIVACY.md)
- [الاستجابة للحوادث](security/INCIDENT_RESPONSE.md)
- [مراجعة Security Audit الأولية](audit/SECURITY_AUDIT_RECONCILIATION_INITIAL.md)

## الجودة والاختبار
- [استراتيجية الاختبار](quality/TEST_STRATEGY.md)
- [خطة الاختبارات الأمنية](quality/SECURITY_TEST_PLAN.md)
- [خطة اختبار اللعب](quality/PLAYTEST_PLAN.md)
- [ميزانية الأداء](quality/PERFORMANCE_BUDGET.md)
- [بروتوكول قياس الأداء](quality/PERFORMANCE_TEST_PROTOCOL.md)
- [توافق الأجهزة](quality/DEVICE_COMPATIBILITY.md)

## العمليات
- [البناء والإصدار](operations/BUILD_AND_RELEASE.md)
- [المراقبة والسجلات](operations/MONITORING_AND_LOGGING.md)
- [النسخ الاحتياطي والاستعادة](operations/BACKUP_AND_RECOVERY.md)

## الحوكمة والتحكم بالمصادر

- [خط أساس الحوكمة](governance/PROJECT_GOVERNANCE_BASELINE.md)
- [ضبط الوثائق والمرجعيات](governance/DOCUMENT_CONTROL_REGISTER.md)
- [جرد المصادر](governance/SOURCE_INVENTORY.md)
- [سجل Provenance](governance/PROVENANCE_REGISTER.md)
- [سجل القرارات وفهرس ADRs](governance/DECISIONS_LOG.md)
- [تتبع القرارات إلى مصادرها](governance/DECISION_TRACEABILITY_REGISTER.md)
- [سجل التعارضات الموحد](governance/CONFLICT_REGISTER.md)
- [تتبع المتطلبات الحالية](governance/REQUIREMENTS_TRACEABILITY.md)
- [سجل TBD — 85 بنداً](governance/TBD_REGISTER.md)
- [Source Traceability Register](governance/SOURCE_TRACEABILITY_REGISTER.md)
- [Source-to-Requirement Matrix](governance/SOURCE_TO_REQUIREMENT_MATRIX.md)
- [TBD Cross-Reference Map](governance/TBD_CROSS_REFERENCE_MAP.md)
- [Consistency Matrix — 109 سجلات](governance/CONSISTENCY_MATRIX.md)
- [Requirement Evidence Matrix](governance/REQUIREMENT_EVIDENCE_MATRIX.md)

## التدقيق والأدلة

- [سجل تقدم التوفيق](audit/RECONCILIATION_PROGRESS.md)
- [مراجعة اتساق التصميم والأنظمة](audit/DESIGN_CONSISTENCY_REVIEW_2026-11-10.md)
- [فحص بنية سجلات المتطلبات 85/109](audit/REQUIREMENT_REGISTERS_QA_2026-11-10.md)
- [مراجعة ADRs وعقود Core](audit/ADR_RECONCILIATION_REVIEW_2026-11-10.md)
- [مراجعة تنفيذ Core](audit/CORE_IMPLEMENTATION_REVIEW_2026-11-10.md)
- [مراجعة CI والأتمتة الأمنية](audit/CI_WORKFLOW_SECURITY_REVIEW_2026-11-10.md)
- [سجل أدلة Core CI](audit/CI_EVIDENCE_REGISTER.md)
- [مراجعة Network Audit الأولية](audit/NETWORK_AUDIT_RECONCILIATION_INITIAL.md)
- [مراجعة Security Audit الأولية](audit/SECURITY_AUDIT_RECONCILIATION_INITIAL.md)
- [بصمات المواد المادية في جلسة 2026-11-10](audit/SESSION_MATERIALIZATION_MANIFEST_2026-11-10.md)

## ADRs الموجودة على main
- [ADR-001: التكدس التقني](governance/adr/ADR-001-TECH-STACK.md)
- [ADR-002: سلطة الخادم](governance/adr/ADR-002-SERVER-AUTHORITY.md)
- [ADR-003: Backend والاستضافة](governance/adr/ADR-003-BACKEND-AND-HOSTING.md)
- [ADR-004: خط أساس قواعد اللعب](governance/adr/ADR-004-GAMEPLAY-RULES-BASELINE.md)

## نطاق MVP والتسليم
- [نطاق MVP](roadmap/MVP_SCOPE.md)
- [معايير قبول MVP](roadmap/ACCEPTANCE_CRITERIA.md)
- [خطة التسليم](roadmap/DELIVERY_PLAN.md)

## حالات القرار ونضج الدليل

Decision Status وEvidence Maturity محوران مستقلان:

- **Decision Status:** PROPOSED، APPROVED، REJECTED، SUPERSEDED.
- **Evidence Maturity:** DOCUMENTED، REVIEWED، IMPLEMENTED، TESTED، VERIFIED، OPERATIONALLY PROVEN.

وتستخدم أوصاف العمل مثل TBD وOPEN وPARTIAL وPENDING وDEFERRED في حقل منفصل. لا تعني APPROVED أن التنفيذ موجود، ولا تعني IMPLEMENTED أن القرار معتمد، ولا تعني نتيجة اختبار واحدة أن النظام متحقق منه بالكامل.

لا تستخدم IMPLEMENTED أو VERIFIED لوصف مشروع كامل لمجرد وجود كود أو نجاح CI جزئي. يجب أن يحدد كل ادعاء فرعه وcommit ونطاق الاختبار والدليل.
