# Technical Architecture — v2.1
**الحالة: PROPOSED | القرار في ADR-001 ولا يُحسم هنا**

التكدس المقترح: Unity 6.3 LTS + URP، C#، FishNet، Linux Dedicated Server، وAndroid أولاً. لا يُعتمد قبل prototype ونسخ مثبتة. Backend الإنتاج يتبع ADR-003 وليس جزءاً من هذا التكدس الآن.

## الطبقات
Client Presentation → Network Adapter → Application Layer → Domain Core → Infrastructure.

Dedicated Server يملك حالة المباراة وفق ADR-002. Domain Core قواعد C# عادية بدون MonoBehaviour أو FishNet. Backend لا يدير كل tick.

## تدفق الأمر
Client Intent → تحقق الجلسة → تحقق الخادم → قاعدة Domain → تحديث ذري → replication → عرض على العميل.

لا تضف ECS أو Rust/C++ أو بروتوكولاً ثنائياً خاصاً قبل قياس. لا نطاقات إصدارات مفتوحة في البناء بعد التثبيت.
