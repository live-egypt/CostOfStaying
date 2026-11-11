# خطة التسليم — v2.1
**الحالة: PROPOSED | تتبع بوابات ميثاق المشروع**

هذا الترتيب هو الملزم. لا تبدأ مرحلة لاحقة قبل دليل المرحلة السابقة.

1. Playtest لـ RULE-01..05 قبل اعتماد القواعد: 10 جلسات موثقة على الأقل وفق [PLAYTEST_PLAN.md](../quality/PLAYTEST_PLAN.md) مع مدخلات [BALANCE_RISK_REVIEW.md](../quality/BALANCE_RISK_REVIEW.md). بعدها يُحدّث ADR-004 إلى APPROVED أو يبقى PROPOSED FOR PLAYTEST مع التعديلات. الجلسات قد تكون ورقية أو على prototype. لا تُؤجّل إلى Closed Alpha.
2. حسم ADR-001 بعد prototype لـ Android وLinux ونسخ أدوات مثبتة. ADR-002 معتمد كمبدأ وغير منفذ. ADR-003 لا يُعتمد قبل قياس التكلفة والكمون.
3. Domain Core واختباراته بدون MonoBehaviour أو FishNet.
4. اتصال شبكي وخادم Linux مستقل يثبت أن العميل يرسل Intent فقط.
5. Vertical Slice لأربعة لاعبين وخريطة واحدة.
6. بوابات الأمن والأداء والاستقرار والقبول بأدلة محفوظة. Closed Alpha يأتي بعد هذه البوابة.

لا مدد زمنية قبل معرفة حجم الفريق. قوالب `.github/workflows` يدوية وليست بوابة CI.
