# Threat Model — v2.0
**الحالة: PROPOSED**

النطاق: Android Client، الشبكة، Dedicated Server، Backend، البيانات، CI/CD، GitHub، مفاتيح التوقيع والنشر.

| التهديد | الضابط |
|---|---|
| تزوير الصحة/الضرر | حساب خادمي |
| RPC spam | rate limits وحدود رسائل |
| Replay | Request IDs وidempotency |
| Serialization abuse | schema validation وحدود الحجم |
| Speed/teleport | server movement validation |
| Session theft | جلسات قابلة للإبطال |
| Secret leak | secret manager وrotation |
| Dependency compromise | pinning ومراجعة |
| CI compromise | least privilege وحماية branches |

لكل تهديد وثّق Asset → Entry Point → Abuse Case → Impact → Control → Test → Evidence → Residual Risk. راجع النموذج عند تغيير الشبكة أو الجلسات أو الحفظ أو Backend أو CI.
