# وثائق ثمن البقاء — v2.1

## التصميم
- [ميثاق المشروع](00_PROJECT_CHARTER.md)
- [Game Design Document](01_GAME_DESIGN_DOCUMENT.md)
- [مواصفات قواعد اللعبة](02_GAME_RULES_SPECIFICATION.md)
- [تجربة اللاعب والحلقة الأساسية](03_PLAYER_EXPERIENCE_AND_CORE_LOOP.md)

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

## الجودة والاختبار
- [استراتيجية الاختبار](quality/TEST_STRATEGY.md)
- [خطة الاختبارات الأمنية](quality/SECURITY_TEST_PLAN.md)
- [ميزانية الأداء](quality/PERFORMANCE_BUDGET.md)
- [بروتوكول قياس الأداء](quality/PERFORMANCE_TEST_PROTOCOL.md)
- [توافق الأجهزة](quality/DEVICE_COMPATIBILITY.md)

## العمليات
- [البناء والإصدار](operations/BUILD_AND_RELEASE.md)
- [المراقبة والسجلات](operations/MONITORING_AND_LOGGING.md)
- [النسخ الاحتياطي والاستعادة](operations/BACKUP_AND_RECOVERY.md)

## الحوكمة والتسليم
- [سجل القرارات](governance/DECISIONS_LOG.md)
- [تتبع المتطلبات](governance/REQUIREMENTS_TRACEABILITY.md)
- [ADR-001: التكدس التقني](governance/adr/ADR-001-TECH-STACK.md)
- [ADR-002: سلطة الخادم](governance/adr/ADR-002-SERVER-AUTHORITY.md)
- [ADR-003: Backend والاستضافة](governance/adr/ADR-003-BACKEND-AND-HOSTING.md)
- [ADR-004: خط أساس قواعد اللعب](governance/adr/ADR-004-GAMEPLAY-RULES-BASELINE.md)
- [نطاق MVP](roadmap/MVP_SCOPE.md)
- [معايير قبول MVP](roadmap/ACCEPTANCE_CRITERIA.md)
- [خطة التسليم](roadmap/DELIVERY_PLAN.md)

## حالات الوثائق
- **PROPOSED:** مقترح لم يعتمد بعد.
- **APPROVED:** قرار معتمد.
- **TBD:** يحتاج قراراً قبل التنفيذ.
- **IMPLEMENTED:** منفذ في كود محدد.
- **VERIFIED:** اجتاز اختباراً وله دليل محفوظ.

لا تستخدم IMPLEMENTED أو VERIFIED لمجرد وجود وثيقة أو تصميم.
