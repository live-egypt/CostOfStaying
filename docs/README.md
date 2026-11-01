# وثائق ثمن البقاء — v2.1

هذه الوثائق تصف التصميم المقترح وخطط التحقق. لا تعني أن أي نظام منفذ أو مختبر ما لم يذكر دليل قابل للمراجعة.

## ابدأ هنا
- [سجل حالة المشروع وتسلسل التنفيذ والتحقق](PROJECT_STATUS_AND_EXECUTION_REGISTER.md)
- [تدقيق تحقق دورة حياة اللاعب ADR-007](audit/ADR007_LIFECYCLE_CI_VERIFICATION_2026-11-01.md)
- [ميثاق المشروع](00_PROJECT_CHARTER.md)
- [Game Design Document](01_GAME_DESIGN_DOCUMENT.md)
- [مواصفات قواعد اللعبة](02_GAME_RULES_SPECIFICATION.md)
- [تجربة اللاعب والحلقة الأساسية](03_PLAYER_EXPERIENCE_AND_CORE_LOOP.md)
- [مسرد المصطلحات](GLOSSARY.md)

## الأنظمة
- [Collapse](systems/COLLAPSE_SYSTEM.md)
- [Suffering Transfer](systems/SUFFERING_TRANSFER_SYSTEM.md)
- [Betrayal](systems/BETRAYAL_SYSTEM.md)
- [Combat](systems/COMBAT_SYSTEM.md)
- [Spirit](systems/SPIRIT_SYSTEM.md)
- [Endgame](systems/ENDGAME_SYSTEM.md)

## المعمارية
- [المواصفة التقنية الموحدة لـ Phase 1](architecture/PHASE1_TECHNICAL_MASTER.md)
- [حالة تنفيذ Core في Phase 1](architecture/PHASE1_CORE_IMPLEMENTATION.md)
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

## التدقيق وأدلة التحقق
- [تدقيق مصدر Phase 1 المرفوع](audit/UPLOADED_PHASE1_SOURCE_AUDIT.md)
- [دليل تحقق Core عبر GitHub Actions](audit/PHASE1_CORE_CI_VERIFICATION.md)
- [تدقيق تشغيل CI ومشكلات ماسحات الأمان](audit/GITHUB_ACTIONS_PIPELINE_AUDIT_2026-11-01.md)

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

## الحوكمة والتسليم
- [سجل القرارات](governance/DECISIONS_LOG.md)
- [تتبع المتطلبات](governance/REQUIREMENTS_TRACEABILITY.md)
- [ADR-001: التكدس التقني](governance/adr/ADR-001-TECH-STACK.md)
- [ADR-002: سلطة الخادم](governance/adr/ADR-002-SERVER-AUTHORITY.md)
- [ADR-003: Backend والاستضافة](governance/adr/ADR-003-BACKEND-AND-HOSTING.md)
- [ADR-004: خط أساس قواعد اللعب](governance/adr/ADR-004-GAMEPLAY-RULES-BASELINE.md)
- [ADR-005: حدود Core التنفيذي](governance/adr/ADR-005-CORE-EXECUTABLE-BOUNDARY.md)
- [ADR-006: ثوابت معرفات Core وسجل Betrayal](governance/adr/ADR-006-CORE-IDENTIFIER-AND-LEDGER-INVARIANTS.md)
- [ADR-007: تعارض دورة حياة اللاعب وVitality](governance/adr/ADR-007-PLAYER-LIFECYCLE-VITALITY-CONFLICT.md)

## حالات الوثائق
- **PROPOSED:** مقترح لم يعتمد.
- **PROPOSED FOR PLAYTEST:** خط أساس يحتاج جلسات اختبار قبل اعتماده.
- **APPROVED:** قرار معتمد تصميمياً.
- **IMPLEMENTED:** منفذ في كود محدد.
- **VERIFIED:** اجتاز اختباراً وله دليل محفوظ.
- **TBD:** لم يحسم بعد.

لا تستخدم IMPLEMENTED أو VERIFIED لمجرد وجود تصميم أو وثيقة.
