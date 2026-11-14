# Technical Architecture — v2.1
**الحالة: PROPOSED | القرار في ADR-001 ولا يُحسم هنا | التنفيذ الجزئي في `DOMAIN_CORE.md` (ADR-005 Core + ADR-021 Domain→Core)**

التكدس المقترح: Unity 6.3 LTS + URP، C#، FishNet، Linux Dedicated Server، وAndroid أولاً. لا يُعتمد قبل prototype ونسخ مثبتة. Backend الإنتاج يتبع ADR-003 وليس جزءاً من هذا التكدس الآن. الإصدارات المثبتة في `governance/SOURCES_AND_REFERENCES.md` (EXT-UNITY/EXT-FISHNET/EXT-DOTNET).

## الطبقات
Client Presentation → Network Adapter → Application Layer → **Domain Core** (`src/CostOfStaying.Domain`, `netstandard2.1`, `LangVersion 9.0` لتوافق Unity 6) → Infrastructure. النواة الصارمة `CostOfStaying.Core` هي الحد الشرعي (ADR-005) — نقل Domain في ADR-021.

Dedicated Server يملك حالة المباراة وفق ADR-002. Domain Core قواعد C# عادية بدون MonoBehaviour أو FishNet — ✅ منفذ كـ PROPOSED (انظر `DOMAIN_CORE.md` و`global.json` + `Directory.Packages.props`; النواة الصارمة Core تُحفظ `TreatWarningsAsErrors=true`). Backend لا يدير كل tick.

## تدفق الأمر
Client Intent (`Intent.cs`) → تحقق الجلسة → تحقق الخادم → قاعدة Domain (`Match.cs::TryTransfer`) → تحديث ذري → DomainEvent → replication → عرض على العميل. حسم دورة الحياة `Alive/Spirit/Eliminated` مع `Resurrect` معطل في MVP (ADR-023) — لا `Alive→Dead` نهائي مباشر.

لا تضف ECS أو Rust/C++ أو بروتوكولاً ثنائياً خاصاً قبل قياس. لا نطاقات إصدارات مفتوحة في البناء بعد التثبيت (`Directory.Packages.props` يثبت الإصدارات, `LangVersion 9.0` للنواة).

## المراجع الموحدة
- المصادر: `governance/SOURCES_AND_REFERENCES.md` EXT-UNITY/EXT-FISHNET/EXT-DOTNET + ADR-021..023
- التنفيذ: `architecture/DOMAIN_CORE.md` + `REPOSITORY_STRUCTURE.md` + `AUDIT_REPORT_2026-11-14.md` §2
