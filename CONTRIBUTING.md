# المساهمة — Contributing (ثمن البقاء / CostOfStaying)

شكراً لاهتمامك! هذا المشروع في **Pre-production** ويتبع 7 مبادئ صارمة. اقرأها قبل أول PR.

## المبادئ السبعة (ملزمة)

1. لا قرار معماري مهم بلا **ADR** في `docs/governance/adr/`.
2. لا ادعاء أداء بلا **قياس قابل للتكرار** (SHA + بيئة + أداة).
3. لا تغيير حالة حساس بناءً على **نتيجة يرسلها العميل** (سلطة الخادم ADR-002).
4. اربط كل متطلب بـ **ضابط واختبار ودليل** (`REQUIREMENTS_TRACEABILITY.md`).
5. افصل **PROPOSED / APPROVED / IMPLEMENTED / VERIFIED / TBD**.
6. أي وثيقة جديدة تدخل **الكتالوج والفهرس في نفس الالتزام** (`DOCUMENT_GOVERNANCE.md` §5).
7. عند التعارض: **ADR > مواصفة القواعد > وثيقة النظام**.

## قبل الكود

- اقرأ `README.md` → `docs/README.md` → `docs/governance/DOCUMENT_GOVERNANCE.md` → `GLOSSARY.md`.
- راجع `docs/governance/SOURCES_AND_REFERENCES.md` — لا تضف مصدراً خارجياً دون تسجيله.
- راجع `docs/00_PROJECT_CHARTER.md` للنطاق (MVP: 4 لاعبين، خريطة واحدة).

## سير العمل

```bash
git checkout -b feature/your-feature     # من main أو الفرع النشط
# ... اعمل تغييرك ...
python3 tools/audit.py                      # يجب أن يمر بلا Issues
dotnet build -c Release && dotnet test -c Release
python3 tools/balance_simulator/test_parity.py
git commit -m "feat(...): ..."
git push origin feature/your-feature
# افتح PR — سيشغل ci.yml تلقائياً
```

## معايير الـ PR

- [ ] العنوان يتبع Conventional Commits (`feat:`, `fix:`, `docs:`, `chore:`)
- [ ] الوثائق المحدثة + الكتالوج + الفهرس (إن أضفت وثيقة)
- [ ] لا أسرار (`SECRETS_MANAGEMENT.md`) — CI يفحص heuristic
- [ ] لا أرقام توازن جديدة بلا وسم `PROPOSED TEST VALUE`
- [ ] لا ترفع حالة إلى `IMPLEMENTED/VERIFIED` بلا CI أخضر وartifact
- [ ] كل رابط نسبي صحيح (`tools/audit.py` يتحقق)

## الهيكل

انظر `docs/REPOSITORY_STRUCTURE.md`. الملخص:

- `docs/` — التوثيق (v2.1)
- `src/CostOfStaying.Domain/` — النواة المجردة (`netstandard2.1`)
- `tests/` — اختبارات NUnit
- `tools/` — محاكيات وادوات قياس

## الإبلاغ عن مشكلة

استخدم قوالب `.github/ISSUE_TEMPLATE/` — اختر Bug / Feature / ADR Proposal.

## الأمان

لا تفتح Issue علنية لثغرة أمنية — راجع `SECURITY.md`.

## الترخيص

بالمساهمة توافق على ترخيص المشروع `LICENSE` (MIT).
