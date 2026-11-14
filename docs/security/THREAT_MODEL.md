# Threat Model — v2.1
**الحالة: PROPOSED | نموذج محدث بـ GATE-3 — جزئياً منفذ (IntentValidator) | الضوابط أدناه قيد التنفيذ لا دليل نهائي**

النطاق: عميل Android، الشبكة، Dedicated Server، Backend إن اعتُمد، مفاتيح التوقيع. CI الآن موجود (`ci.yml` SHA-pinned).

| التهديد | الضابط المقصود | الحالة بعد GATE-3 |
|---|---|---|
| تزوير الصحة/الضرر/Vitality | حساب خادمي فقط (ADR-002) | **منفذ PROPOSED** — `Match.cs` هو المالك الوحيد، `SecurityTests` |
| RPC spam / Transfer flood | rate limit + cooldown | **جزئي** — `TransferCooldown 5ث` + `IntentValidator` (نافذة 10ث/6) — اختبارات قادمة |
| Replay / Duplicate | RequestId + idempotency | **منفذ PROPOSED** — `ProcessedRequestIds` + `IsRequestIdValid` + اختبار replay |
| Serialization abuse | حد حجم + schema | **جزئي** — `IsRequestIdValid` (8..64, أحرف) — حد payload كامل GATE-3.4 |
| تزوير SentAt (مستقبل/قديم) | فحص انحراف زمني | **جديد GATE-3** — `IntentValidator.IsSentAtValid ±5ث` + `IntentValidatorTests` |
| تزوير المسافة | قياس خادمي فقط | **منفذ PROPOSED** — `IsDistanceValid` + `TryTransfer(distanceMeters server-measured)` |
| Speed/teleport | تحقق حركة خادمي | غير منفذ — GATE-3.3 (Network) |
| Session theft / hijack | جلسات قابلة للإبطال | غير منفذ — ADR-016..017 |
| Secret leak | أسرار خارج Git | **سياسة + CI** — `gitleaks` heuristic + `persist-credentials:false` |
| Dependency compromise | تثبيت + Dependabot | **منفذ** — `Directory.Packages.props` + `dependabot.yml` + `ci.yml` SHA-pin |
| CI compromise | أقل صلاحية | **منفذ** — `permissions: contents: read` + artifacts 30ي |

لكل تهديد لاحقاً: Asset/Entry/Abuse/Impact/Control/Test/Evidence/Residual. الجدول يظهر التقدم لا الدليل النهائي — كل PROPOSED يحتاج CI أخضر على `main`.

