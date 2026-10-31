# Secrets Management — v2.0
**الحالة: PROPOSED**

لا تحفظ API keys أو tokens أو signing keys أو passwords في Git أو ScriptableObjects أو إعدادات العميل أو logs. Obfuscation ليست حماية للأسرار.

استخدم GitHub Actions Secrets/Environments أو secret manager؛ أقل صلاحية؛ فصل dev/staging/prod؛ تدوير وإبطال المفاتيح عند الاشتباه؛ فحص secrets آلياً قبل الدمج والبناء. لا يمر إصدار مع secret مؤكد.
