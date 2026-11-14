# Playtest Harness — v0.1 (PROPOSED)
**الحالة: PROPOSED**

أداة لتشغيل جلسات لعب محاكاة موثقة وتوليد سجلات مطلوبة لبوابة اعتماد ADR-004:
على الأقل 10 جلسات موثقة مع مدة المباراة وزمن الموت وعدد عمليات النقل وتوزيع النتائج الخمس ومعدل الانسحاب وفهم اللاعبين.

## الاستخدام
```bash
python3 tools/playtest/run_sessions.py --sessions 10 --preset default --output docs/quality/playtest-logs/
python3 tools/playtest/run_sessions.py --sessions 10 --preset fast-decay --seed 100
```

## المخرجات
- `session-*.json` — سجل مفصل لكل جلسة (أحداث، أصوات، نقل، انهيارات).
- `summary.csv` — جدول مجمع لتوزيع النتائج ومتوسطات المدة.
- `report.md` — تقرير جاهز للإرفاق في ADR-004 وDELIVERY_PLAN.

كل سجل يحمل طابعاً زمنياً ومعرف جلسة وإصدار المحاكي — لا يمكن اعتباره VERIFIED قبل تشغيله على بِلد فعلي.
