# Session Handoff — 2026-11-12

## الحالة المختصرة

- المشروع في Pre-production، v2.1.
- `main` الحالي وقت لقطة النقل: `2c68f748360b78a8bb4d86b8bf693b4c2ce07216`.
- مستودع GitHub Public، default branch main.
- شجرة main التي فُحصت: 53 ملفاً و11 مجلداً، دون ملفات Core/Unity project.
- فرع Core وملفات Unity المصدرية موجودة في فروع مراجعة غير مدمجة؛ لا تنسبها إلى main.
- M1 = PARTIAL / OPEN. لا يبدأ M2 تلقائياً.
- الأرشيف النصي الحالي: 18 TXT / 146,407 bytes قبل الضغط، موثق بالبصمات في SOURCE_ARCHIVE_MANIFEST.

## ما تم إنجازه في سياق النقل

1. إعادة فحص GitHub repository metadata وmain head وrecursive tree.
2. استعراض 21 فرعاً قبل إضافة فرع النقل.
3. استخراج حالات PR #1–#20.
4. جلب المحتوى المباشر لسجلات المصدر/provenance الرئيسية في PR #8/#9/#11.
5. مراجعة PR #18/#19/#20 والتمييز بين PDR evidence وقرار الإغلاق.
6. حفظ 18 TXT من الملفات الظاهرة في runtime داخل ZIP محلياً، وتسجيل SHA-256 لكل ملف.
7. إنشاء فرع نقل مستقل من main، دون تغيير main.

## ملاحظات تاريخية يجب عدم تسويتها

- 15 ثم 20 ثم 21 attachment objects هي snapshots متسلسلة، وليست أعداداً متناقضة بعد قراءة دلتا PR #11.
- 40 ثم 45 Library file records هي metadata snapshots. لا raw byte verification.
- 18 ملفات TXT محلية مختلفة عن أعداد attachment objects وعن Library file IDs.
- HTML metadata كما دوّنها PR #11 لا تثبت منشأ underlying chat أو اكتماله.
- PDR-013 لا يغطي تلقائياً جميع عناصر DIFF-XXX؛ PDR-016 مقارنة مستقلة للمسارات التسعة.
- PR #8/#9/#11 لم تحسم canonical package؛ DOC-PACKAGE-01 OPEN.
- بعض PDRs المشار إليها على فروع A5، بينما وصف تاريخي للمشروع قال إن A5 لم يبدأ؛ احفظها كاختلاف حالة إلى أن يحسمها owner.

## أول إجراء بعد استئناف المشروع الجديد

1. تحقق من وجود أرشيف المصادر 18 TXT وفك ضغطه ومطابقة SHA-256.
2. افحص GitHub live: main، branches، PRs، status checks، وآخر commits.
3. أعد قراءة هذا handoff وسجل الفجوات.
4. أكمل M1-G01 إلى G06 فقط؛ لا تبدأ M2.
5. ارفع سؤال owner عند الحاجة إلى قرار DOC-PACKAGE-01 أو حسم أصل المحادثة أو تغيير ADR.

## ما لم يتم

- لم تُدمج أو تُغلق أي PR.
- لم يُعدل main.
- لم تُجر اختبارات Core أو Unity أو FishNet أو Android أو Linux في مهمة النقل.
- لم يتم استعادة كل محادثات ChatGPT أو Library raw bytes.
- لم يُعتمد canonical path.
- لم يُغلق M1 ولم يبدأ M2.
