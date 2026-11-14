# استراتيجية الاختبار — v2.1
**الحالة: PROPOSED | النواة منفذة كـ PROPOSED (ADR-005) | لا VERIFIED قبل CI أخضر**

## هرم الاختبار
Unit → Integration → PlayMode/System → Network → Playtest → Performance/Security. لا يغني اختبار الوحدة عن الشبكة أو الجهاز الفعلي.

## النطاق

- **Collapse:** التآكل، المؤقتات، إعادة الجدولة، والموت عند الصفر فقط. لا تُعتمد فترات غير مسماة في RULE-03.
- **Transfer:** Move ذري، السعة، المدى الأولي، cooldown، replay، والتزامن.
- **Betrayal:** نقطة واحدة بعد النجاح فقط، وعدم الزيادة عند الفشل أو replay. لا تُكتب عتبات 3/5/7 كاختبارات قواعد قبل تسجيلها.
- **Combat:** تحقق خادمي. أرقام الأسلحة ليست متطلبات.
- **Spirit:** انتقال واحد إلى Spirit، فرادة Echo، ومنع التأثير في الأحياء. Energy ليس نطاقاً حتى يُحسم.
- **Endgame:** التصويت، الإجماع، الرفض، ثم النتائج الخمس بما فيها Draw منفصلة عن Time Limit وMutual Collapse.
- **دورة المباراة وإعادة الاتصال** بعد اعتماد سياسة. لا سياسة معتمدة الآن.

## التنفيذ الحالي (Domain Core)

| الطبقة | الملف | الحالة |
|---|---|---|
| Domain Unit | `tests/CostOfStaying.Domain.Tests/VitalityTests.cs`, `CollapseTests.cs` | PROPOSED |
| Domain Integration | `TransferTests.cs`, `VoteAndEndgameTests.cs`, `SpiritTests.cs` | PROPOSED |
| Security | `SecurityTests.cs` (سلطة الخادم، replay، مسافة خادمية) | PROPOSED |
| Parity | `tools/balance_simulator/test_parity.py` | PROPOSED |
| Playtest محاكاة | `tools/playtest/run_sessions.py` (≥10 جلسات) | SIMULATED |

> CI يشغلها في `ci.yml`. النتيجة `VERIFIED` تتطلب CI أخضر + SHA + بيئة موثقة (انظر `REQUIREMENTS_TRACEABILITY.md`).

## أدوات

- **النواة:** `NUnit 4.2.2` + `Microsoft.NET.Test.Sdk 17.11.1` (مثبتان في `Directory.Packages.props`).
- **Unity:** Unity Test Framework بعد اعتماد Unity في ADR-001 — ليس مضافاً بعد (`TECHNICAL_ARCHITECTURE.md`).
- لا منطق Domain يعتمد على `MonoBehaviour`/`FishNet` — يبقى قابلاً للاختبار مستقلاً.

## الشبكة

RTT 100/200/300ms وloss 1/3/5% وjitter وreorder وreconnect نقاط اختبار مقترحة، ليست مواصفات تشغيل مثبتة. tick rate لم يُحسم. انظر `NETWORK_ARCHITECTURE.md`.

## التوثيق

كل اختبار يحمل ID وهدفاً وخطوات ونتيجة متوقعة وفعلية وcommit وبيئة. اربطه بمتطلب في `REQUIREMENTS_TRACEABILITY.md`. لا VERIFIED بلا تنفيذ.

## المراجع الموحدة
- المصادر: `governance/SOURCES_AND_REFERENCES.md` SRC-TRACE/SRC-DOMAIN
- التنفيذ: `architecture/DOMAIN_CORE.md`
