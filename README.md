# ثمن البقاء | The Cost of Staying

**حزمة التوثيق: v2.1 — حالة المشروع: Pre-production (Domain Core PROPOSED)**

لعبة بقاء نفسي جماعية؛ كل ثانية تستنزف Vitality، ونقل المعاناة يؤخر الانهيار لكنه يضيف كلفة Betrayal، والموت ينقل اللاعب إلى دور Spirit.

## الحالة الحالية
- المنصة المستهدفة أولاً: Android.
- نطاق MVP: مباراة مكتملة لأربعة لاعبين وخريطة واحدة.
- حالة التنفيذ: النواة المجردة `src/CostOfStaying.Domain` واختباراتها ومحاكي التوازن وأداة Playtest منفذة كـ **PROPOSED** على فرع `arena/4bfc0882-costofstaying` (انظر `docs/architecture/DOMAIN_CORE.md` وADR-005). لا Unity project ولا Dedicated Server شبكي ولا Android build بعد؛ لا بند VERIFIED قبل CI أخضر على GitHub.
- حالة قواعد اللعب: خط أساس PROPOSED FOR PLAYTEST، وليس اعتماداً نهائياً.
- حالة التكدس التقني: PROPOSED، باستثناء مبدأ سلطة الخادم المعتمد تصميمياً (والمنطق المنفذ في Domain Core).
- الفحص الآلي: `ci.yml` يعمل عند كل `push`/`PR` (تدقيق وثائق + بناء واختبار النواة + محاكاة + فحص أسرار + رفع Artifacts)؛ بقية القوالب تبقى `workflow_dispatch` فقط.

## نقطة البداية
ابدأ من [فهرس الوثائق](docs/README.md) و[حوكمة الوثائق](docs/governance/DOCUMENT_GOVERNANCE.md) و[سجل حل التعارض](docs/governance/CONFLICT_RESOLUTIONS.md) و[المصادر الموحدة](docs/governance/SOURCES_AND_REFERENCES.md) و[هيكل المستودع](docs/REPOSITORY_STRUCTURE.md)، ثم [ميثاق المشروع](docs/00_PROJECT_CHARTER.md) و[قواعد اللعبة](docs/02_GAME_RULES_SPECIFICATION.md).

## البناء السريع

```bash
python3 tools/audit.py
dotnet restore && dotnet build -c Release
dotnet test -c Release --collect:"XPlat Code Coverage"
python3 tools/balance_simulator/test_parity.py
python3 tools/playtest/run_sessions.py --sessions 10 --preset default
```

## مبادئ العمل
1. لا قرار معماري مهم بلا ADR.
2. لا ادعاء أداء بلا قياس قابل للتكرار.
3. لا تغيير حالة حساس بناءً على نتيجة يرسلها العميل.
4. اربط كل متطلب بضابط واختبار ودليل.
5. افصل المقترح والمعتمد والمنفذ والمتحقق منه.
6. أي وثيقة جديدة تدخل الكتالوج والفهرس في نفس الالتزام.
7. عند التعارض: ADR، ثم مواصفة القواعد، ثم وثيقة النظام.

## التكدس المقترح
Unity 6.3 LTS + URP، C#، FishNet، Linux Dedicated Server. يؤجل Backend الإنتاجي إلى ما بعد prototype. التفاصيل في ADR-001 وADR-003 والإصدارات المثبتة في `governance/SOURCES_AND_REFERENCES.md`.

## المساهمة
انظر `CONTRIBUTING.md` و`CODE_OF_CONDUCT.md` و`SECURITY.md`.

## دلالات الحالة
التعريف الملزم في [حوكمة الوثائق](docs/governance/DOCUMENT_GOVERNANCE.md).
- **PROPOSED:** مقترح لم يعتمد.
- **PROPOSED FOR PLAYTEST:** خط أساس مطلوب اختباره قبل الاعتماد.
- **APPROVED:** قرار معتمد تصميمياً.
- **IMPLEMENTED:** منفذ في كود محدد.
- **VERIFIED:** اجتاز اختباراً محدداً وله دليل محفوظ.
- **TBD:** لم يحسم بعد.

وجود وثيقة لا يعني أن النظام نُفذ أو اختُبر. التنفيذ الوحيد هو النواة المجردة بحالة PROPOSED بانتظار CI أخضر.
