## ما التغيير؟
<!-- اربط Issue/ADR إن وجد -->

## الحوكمة
- [ ] لا قرار معماري بلا ADR (أو ذكر سبب عدم الحاجة)
- [ ] الكتالوج `DOCUMENT_GOVERNANCE.md` + الفهرس `docs/README.md` محدثان (إن أضفت وثيقة)
- [ ] الأرقام الجديدة موسومّة `PROPOSED TEST VALUE` حتى Playtest
- [ ] لا أسرار في الـ diff (`SECRETS_MANAGEMENT.md`)

## التحقق
- [ ] `python3 tools/audit.py` ✅
- [ ] `dotnet build/test` ✅ (إن غيّر src/tests)
- [ ] `python3 tools/balance_simulator/test_parity.py` ✅ (إن غيّر منطق Domain)
- [ ] CI أخضر على هذا الفرع

## الأدلة
<!-- SHA / artifact / screenshot إن وجد -->

## المراجع الموحدة
<!-- أضف إدخال SOURCES_AND_REFERENCES.md إن استخدمت مصدراً خارجياً جديداً -->
