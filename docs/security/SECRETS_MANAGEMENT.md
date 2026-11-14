# Secrets Management — v2.1
**الحالة: PROPOSED | فحص heuristic منفذ (CI) | لا مخزن أسرار إنتاجي**

لا تحفظ API keys أو tokens أو signing keys أو passwords في Git أو ScriptableObjects أو إعدادات العميل أو logs. التعمية ليست حماية للأسرار.

عند وجود بناء: أسرار خارج المستودع، أقل صلاحية، فصل البيئات، وتدوير عند الاشتباه.

## الفحص الحالي
- `ci.yml` يشغل **gitleaks 8.18.0** (heuristic) + `grep` للأنماط الشائعة (`AKIA*`, `ghp_*`) — يفشل الـ PR إن وُجد سر ظاهر.
- القوالب `codeql.yml` إلخ تبقى `workflow_dispatch` — ليست بوابة جودة (انظر `.github/workflows/README.md`).
- لا يمر إصدار مع secret مؤكد عند وجود خط إصدار.

## المراجع الموحدة
- `governance/SOURCES_AND_REFERENCES.md` EXT-GITLEAKS
- `REPOSITORY_STRUCTURE.md` + `.gitignore` (أسرار مستبعدة)
