# Technical Architecture — v2.0
**الحالة: PROPOSED**

## التكدس المقترح
Unity 6.3 LTS + URP، C#، FishNet، Linux Dedicated Server. Android أولاً. Backend يؤجل حتى تثبت حاجة الحسابات/المطابقة/البيانات/الاستضافة.

## الطبقات
Client Presentation → Network Adapter → Application Layer → Domain Core → Infrastructure.
Dedicated Server يستضيف المباراة ويملك الحالة. Domain Core قواعد C# عادية دون MonoBehaviour أو FishNet. Backend لا يدير كل tick.

## تدفق الأمر
Client Intent → Session validation → Server validation → Domain rule → Atomic state update → Replication/event → Client presentation.

لا تضف ECS أو Rust/C++ أو بروتوكولاً ثنائياً خاصاً قبل profiling evidence. ثبّت إصدارات الأدوات بعد prototype، ولا تستخدم نطاقات إصدارات مفتوحة في البناء.
