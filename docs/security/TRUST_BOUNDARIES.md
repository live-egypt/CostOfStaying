# Trust Boundaries — v2.1
**الحالة: PROPOSED | تتبع ADR-002 ولا تثبت تنفيذاً**

مناطق الثقة: Player Device غير موثوق، Public Network غير موثوق، Dedicated Server مصدر حقيقة المباراة عند بنائه، Backend موثوق فقط ضمن تفويضه بعد ADR-003، والمستودع سطح حساس.

العميل يرسل Intent لا Results. ارفض SetHealth وApplyDamage وTransferSucceeded وSetAmmo وVotePassed وSetEnergy كحقائق يحددها العميل. رفض SetEnergy لا يعني أن Energy قاعدة معتمدة.

الخادم، عند وجوده، يتحقق من الهوية والحالة والمدى والتبريد والملكية والذخيرة والسرعة والتوقيت وRequestId وحجم الرسالة.
