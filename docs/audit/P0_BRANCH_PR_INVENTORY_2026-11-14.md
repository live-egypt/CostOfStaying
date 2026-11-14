# P0 — جرد الفروع وطلبات السحب وحماية main

**تاريخ اللقطة:** 14 نوفمبر 2026  
**المستودع:** live-egypt/CostOfStaying  
**فرع الإصلاح:** fix/p0-ci-domain-tests-2026-11-14  
**طلب السحب:** [#23 — ci: harden P0 checks and Domain test access](https://github.com/live-egypt/CostOfStaying/pull/23)  
**الهدف:** feature/core-lifecycle-resolution — دون تعديل main

## نطاق الحماية والمحافظة

أُنشئ فرع P0 مستقل من رأس فرع Core المستخدم في PR #6 عند SHA:
2820285021b1a3d7ae9acd953b3bb65677c33512.

جميع الفروع السابقة بقيت موجودة. لم يُحذف أو يُعاد تسمية أو يُكتب فوق أي فرع. لم يحدث أي تعديل مباشر على main، ولم يُدمج PR #6 أو PR #23 أو أي طلب توثيقي.

الفرع audit/p0-ci-and-architecture-decisions لم يُستخدم قاعدة للإصلاح؛ إذ كان متأخراً عن feature/core-lifecycle-resolution بسبعة commits عند المقارنة، مع اختلاف مستقل في اختبار واحد. اختير رأس PR #6 الأحدث لتجنّب إعادة إدخال هذا الانحراف.

## جرد الفروع

أظهر بحث GitHub في اللقطة 24 فرعاً:

1. arena/4bfc0882-costofstaying
2. audit/p0-ci-and-architecture-decisions
3. audit/phase1-uploaded-source-review
4. docs/a5-pdr-002-organizational-structure
5. docs/a5-pdr-005-lif-dec-01-supersession
6. docs/a5-pdr-008-adr-classification-review
7. docs/a5-pdr-009-tbd-register
8. docs/a5-pdr-010-provenance-register
9. docs/a5-pdr-011-dropped-status-review
10. docs/a5-pdr-012-source-inventory-crosswalk
11. docs/a5-pdr-013-diff-xxx-review
12. docs/a5-pdr-014-source-origin-01-review
13. docs/chatgpt-project-transfer-archive-2026-11-12
14. docs/project-reconciliation-baseline-2026-11-10
15. docs/source-provenance-reconciliation-2026-11-09
16. docs/stage1-source-provenance-recovery
17. docs/stage1-source-reconciliation
18. feature/core-lifecycle-resolution
19. feature/hotfix-cs0246
20. feature/phase1-core-foundation
21. feature/phase1-technical-consolidation
22. fix/p0-ci-domain-tests-2026-11-14
23. main
24. review/unity-source-package-2026-11-01

هذه لقطة أسماء فروع، وليست حكماً على صلاحية كل فرع للدمج أو الحذف. يلزم فحص المحتوى والتاريخ والفرق قبل أي تنظيف مستقبلي.

## طلبات السحب المفتوحة

بعد فتح PR #23، أظهر الجرد 11 طلب سحب مفتوحاً:

| PR | العنوان | الحالة / التفسير |
|---:|---|---|
| #23 | ci: harden P0 checks and Domain test access | مسودة P0؛ تستهدف feature/core-lifecycle-resolution |
| #22 | docs: preserve project state and ChatGPT migration handoff | مفتوح؛ الأرشيف الثنائي موثق بأنه لم يُرفع بعد |
| #11 | docs: consolidate project governance and evidence baseline | مفتوح؛ حزمة توثيق تتداخل مع حزم أخرى |
| #20 | docs(governance): PDR-014 SOURCE-ORIGIN-01 review | مفتوح؛ فجوة مصدر الأصل باقية |
| #18 | docs(governance): PDR-012 source inventory crosswalk | مفتوح؛ المطابقة عبر المصادر جزئية |
| #19 | docs(governance): PDR-013 DIFF-XXX review | مفتوح؛ النطاق الدلالي جزئي |
| #17 | docs(governance): PDR-011 dropped-from-status review | مفتوح؛ تاريخ العناصر المحذوفة غير محسوم |
| #8 | docs: establish Stage 1 source and provenance baseline | مفتوح؛ يتداخل مع PR #9 و#11 |
| #9 | docs: reconcile project sources and 85 TBD requirements | مفتوح؛ يتداخل مع PR #8 و#11 |
| #6 | Resolve player lifecycle and align Core vitality contract | مسودة / مفتوح؛ Core غير مدمج |
| #5 | docs(audit): review supplied Unity source package | مفتوح؛ نقطة تدقيق، لا يثبت بناء Unity |

لا يُغلق أي طلب أو يُدمج تلقائياً بناءً على وجود طلب أحدث. يجب مراجعة التداخلات، الـbase/head، الملفات المتغيرة، وأدلة الاختبارات قبل قرار الدمج.

## نتيجة فحص صلاحية الاختبارات

يحتوي فرع Core مسبقاً على:
src/CostOfStaying.Core/Properties/AssemblyInfo.cs

ويتضمن:
[assembly: InternalsVisibleTo("CostOfStaying.Core.Tests")]

مسح ملفات NUnit الستة عشر لم يعثر على الأنواع أو الخصائص المذكورة في وصف الخطأ: Player.Vitality أو Player.State أو Debuff.RemainingSeconds. هذه العناصر غير موجودة في مصدر Core الحالي، لذلك لم تُنشأ خصائص بديلة ولم تُفتح setters في Domain.

الموضع الوحيد الذي عُثر فيه على إسناد setter في اختبار كان كائناً وهمياً خاصاً باسم MalformedAuthenticatedSession. عُدّل ليُمرّر SessionState عبر المنشئ ويحتفظ بـ private set. لا يغيّر هذا عقد الإنتاج أو سلطة المجال.

## مراجعة حماية main

- أعاد endpoint العام لقواعد المستودع rulesets قائمة فارغة في وقت الفحص.
- أعاد endpoint حماية الفرع main حالة HTTP 403: Resource not accessible by integration.
- صلاحيات الاتصال المرتبط تعرض admin = false.

لذلك لم يمكن التحقق من وجود أو تفاصيل حماية الفرع الكلاسيكية لـ main. القائمة الفارغة من rulesets لا تثبت عدم وجود حماية كلاسيكية. لم تُغيّر أي قاعدة حماية.

توصيات للمسؤول/المالك للتحقق والتطبيق:

1. اشتراط Pull Request ومنع الدفع المباشر وForce Push.
2. منع حذف main أو تحديثه بالقوة.
3. اشتراط نجاح Core CI وGitleaks وNuGet Audit وDocumentation Audit وCodeQL وSecurityCodeScan عندما تكون الملفات ضمن النطاق.
4. اشتراط مراجعة مستقلة وإبطال الموافقات القديمة بعد تحديثات جوهرية، عند توفر مراجع مستقل.
5. اشتراط حل كل محادثات المراجعة وتقييد صلاحيات تجاوز الحماية لأقل عدد ممكن.
6. اختيار أسماء status checks المطلوبة من نتائج فعلية على SHA نفسه، لا من أسماء متوقعة أو قديمة.

## حدود P0

- لا تغيير مباشر في main.
- لا حذف أو إعادة كتابة لأي فرع سابق.
- لا دمج تلقائي لـPR #6 أو PR #23 أو PRs التوثيقية.
- لا ادعاء باكتمال Unity/FishNet أو Android أو Linux Dedicated Server أو الأمن الإنتاجي.
- حماية main الكلاسيكية ما زالت تتطلب تحققاً من مالك/مسؤول يملك صلاحية القراءة والإدارة.
