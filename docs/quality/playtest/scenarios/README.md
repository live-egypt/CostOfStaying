# سيناريوهات Playtest — حزمة GATE-1.1
**الحالة: PROPOSED | التاريخ: 2026-11-14 | الوكيل: Playtest Oracle**

3 سيناريوهات ورقية/CLI تغطي كل RULE وتجيب على أسئلة Betrayal الخمس:

| # | الملف | يختبر | السؤال الرئيسي |
|---|---|---|---|
| 01 | `01-social-pressure.md` | RULE-01/02/03 | هل الضغط يخلق خيانة؟ هل 15ث عند 3 أحياء كافية؟ |
| 02 | `02-tactical-tradeoff.md` | RULE-02/05 | هل Transfer مقايضة حقيقية؟ هل 2م+5ث+3 سعة متوازنة؟ |
| 03 | `03-spirit-witness.md` | RULE-04/05 + ADR-023 | هل Spirit محايد وممتع؟ هل MutualCollapse مفهوم؟ |

## الاستخدام

1. اطبع بطاقات Vitality (100→0) وأوراق Debuff + أحجار Betrayal + حلقات 2م.
2. شغّل كل سيناريو مرة واحدة على الأقل (إجمالي 3 جلسات تمهيدية) قبل GATE-1.2 (10 جلسات بشرية رسمية).
3. سجل حسب `PLAYTEST_PLAN §5 + §9` واستبيان 1–5 — احفظ في `playtest-logs/human/` كـ JSON.

## المراجع

- `PLAYTEST_PLAN.md`, `02_GAME_RULES_SPECIFICATION.md`, `BALANCE_RISK_REVIEW.md`, `SYSTEMS/*`, ADR-022/023
- أدوات المحاكاة لا تغني عن هذه الجلسات — SIMULATED ≠ VERIFIED.
