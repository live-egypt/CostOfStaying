# Technical Architecture — v2.1
**الحالة: PROPOSED | القرار في ADR-001 ولا يُحسم هنا | التنفيذ الجزئي في `DOMAIN_CORE.md` (ADR-005)**

التكدس المقترح: Unity 6.3 LTS + URP، C#، FishNet، Linux Dedicated Server، وAndroid أولاً. لا يُعتمد قبل prototype ونسخ مثبتة. Backend الإنتاج يتبع ADR-003 وليس جزءاً من هذا التكدس الآن. الإصدارات المثبتة في `governance/SOURCES_AND_REFERENCES.md` (EXT-UNITY/EXT-FISHNET/EXT-DOTNET).

## الطبقات
Client Presentation → Network Adapter → Application Layer → **Domain Core** (`src/CostOfStaying.Domain`, `netstandard2.1`) → Infrastructure.

Dedicated Server يملك حالة المباراة وفق ADR-002. Domain Core قواعد C# عادية بدون MonoBehaviour أو FishNet — ✅ منفذ كـ PROPOSED (انظر `DOMAIN_CORE.md` و`global.json` + `Directory.Packages.props`). Backend لا يدير كل tick.

## تدفق الأمر
Client Intent (`Intent.cs`) → تحقق الجلسة → تحقق الخادم → قاعدة Domain (`Match.cs::TryTransfer`) → تحديث ذري → DomainEvent → replication → عرض على العميل.

لا تضف ECS أو Rust/C++ أو بروتوكولاً ثنائياً خاصاً قبل قياس. لا نطاقات إصدارات مفتوحة في البناء بعد التثبيت (`Directory.Packages.props` يثبت الإصدارات).

## المراجع الموحدة
- المصادر: `governance/SOURCES_AND_REFERENCES.md` EXT-UNITY/EXT-FISHNET/EXT-DOTNET
- التنفيذ: `architecture/DOMAIN_CORE.md` + `REPOSITORY_STRUCTURE.md`
