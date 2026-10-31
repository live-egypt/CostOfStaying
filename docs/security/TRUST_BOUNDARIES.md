# Trust Boundaries — v2.0
**الحالة: PROPOSED**

مناطق الثقة: Player Device (غير موثوق)، Public Network (غير موثوق)، Dedicated Server (مصدر حقيقة المباراة)، Backend (موثوق فقط ضمن تفويضه)، CI/Repository (سطح عالي الحساسية)، Build Artifacts (يجب التحقق منها).

القاعدة: العميل يرسل Intent لا Results. ارفض SetHealth وApplyDamage وTransferSucceeded وSetAmmo وVotePassed وSetEnergy كحقائق يحددها العميل.

الخادم يتحقق من الهوية والحالة والمدى والتبريد والملكية والذخيرة والسرعة والطاقة والتوقيت وRequestId وحجم الرسالة.
