# Balance Simulator — v0.1 (PROPOSED)
**الحالة: PROPOSED | أداة مقترحة وليست قياساً معتمداً**

محاكي توازن مكتوب بـ Python يعكس منطق `src/CostOfStaying.Domain` (النطاق الخادمي) بدقة.
الغرض: اختبار معدلات التآكل وفترات Collapse وتأثير Transfer قبل اعتماد قيم في ADR-004.

> ⚠️ كل ما ينتجه هذا المحاكي هو **فرضية اختبار** وليس أداءً معتمداً.
> لا يُستخدم كلمة VERIFIED إلا بعد جلسات لعب موثقة على بِلد فعلي.

## التشغيل
```bash
python3 tools/balance_simulator/simulate.py --preset default --seed 42
python3 tools/balance_simulator/simulate.py --sweep decay
python3 tools/balance_simulator/sweep.py --runs 100 --output docs/quality/playtest-logs/sweep-$(date +%Y%m%d).csv
```

## الملفات
- `simulate.py` — محاكاة مباراة واحدة (4 لاعبين، قرارات عشوائية قابلة للتكرار).
- `sweep.py` — مسح باراميتري لمعدلات التآكل وفترات Collapse.
- `presets.json` — قيم افتراضية مطابقة لـ `MatchConfig` في C#.

## الربط مع C#
المنطق في `simulate.py` مطابق لمنطق `Match.cs` (نفس الصيغ، نفس سياسة إعادة الجدولة).
عند تغيير C# يجب تحديث هذا الملف والعكس — الاختبارات في `test_parity.py` تتحقق من التطابق.
