# Threat Model — v2.1
**الحالة: PROPOSED | نموذج لم يُراجَع على نظام منفذ**

النطاق المستهدف: عميل Android، الشبكة، Dedicated Server، Backend إن اعتُمد، ومفاتيح التوقيع. CI غير موجود؛ تهديد CI ينطبق فقط بعد إنشاء خط بناء حقيقي.

| التهديد | الضابط المقصود | الحالة |
|---|---|---|
| تزوير الصحة والضرر | حساب خادمي | غير منفذ |
| RPC spam | rate limits | غير منفذ |
| Replay | RequestId وidempotency | غير منفذ |
| Serialization abuse | schema وحد الحجم | غير منفذ |
| Speed/teleport | تحقق حركة خادمي | غير منفذ |
| Session theft | جلسات قابلة للإبطال | غير منفذ |
| Secret leak | أسرار خارج Git | سياسة فقط |
| Dependency compromise | تثبيت ومراجعة | لا كود بعد |
| CI compromise | أقل صلاحية عند وجود CI | لا ينطبق الآن |

لكل تهديد لاحقاً: Asset وEntry Point وAbuse Case وImpact وControl وTest وEvidence وResidual Risk. لا تُعامل هذه الجدول كدليل على أن الضابط موجود.
