# خطة التسليم — v2.1
**الحالة: PROPOSED | تتبع بوابات ميثاق المشروع | بوابة 3 منفذة كـ PROPOSED (ADR-005)**

هذا الترتيب هو الملزم. لا تبدأ مرحلة لاحقة قبل دليل المرحلة السابقة.

1. **Playtest لـ RULE-01..05** قبل اعتماد القواعد: 10 جلسات موثقة على الأقل وفق [PLAYTEST_PLAN.md](../quality/PLAYTEST_PLAN.md) مع مدخلات [BALANCE_RISK_REVIEW.md](../quality/BALANCE_RISK_REVIEW.md). بعدها يُحدّث ADR-004 إلى APPROVED أو يبقى PROPOSED FOR PLAYTEST مع التعديلات. الجلسات قد تكون ورقية أو على prototype. لا تُؤجّل إلى Closed Alpha. — *جلسات محاكاة ≥10 متوفرة في `docs/quality/playtest-logs/` (SIMULATED)؛ جلسات بشرية لا تزال مطلوبة.*
2. **حسم ADR-001** بعد prototype لـ Android وLinux ونسخ أدوات مثبتة. ADR-002 معتمد كمبدأ وغير منفذ منطقياً في Domain. ADR-003 لا يُعتمد قبل قياس التكلفة والكمون.
3. **Domain Core واختباراته** بدون MonoBehaviour أو FishNet — ✅ منفذ كـ **PROPOSED** في `src/CostOfStaying.Domain` + `tests/` + `tools/` (ADR-005) بانتظار CI أخضر على GitHub.
4. **اتصال شبكي وخادم Linux مستقل** يثبت أن العميل يرسل Intent فقط — PROPOSED (بوابة تالية).
5. **Vertical Slice** لأربعة لاعبين وخريطة واحدة.
6. **بوابات الأمن والأداء والاستقرار والقبول** بأدلة محفوظة. Closed Alpha يأتي بعد هذه البوابة.

لا مدد زمنية قبل معرفة حجم الفريق. بوابة CI نشطة هي `ci.yml` (ADR-005)؛ بقية القوالب تبقى يدوية فقط (انظر `.github/workflows/README.md`).

## المراجع الموحدة
- المصادر: `governance/SOURCES_AND_REFERENCES.md` SRC-CHARTER/SRC-ADR-005
- الهيكل: `REPOSITORY_STRUCTURE.md`
