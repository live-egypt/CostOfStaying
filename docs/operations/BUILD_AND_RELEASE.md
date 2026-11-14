# Build and Release — v2.1
**الحالة: PROPOSED | CI النواة منفذ (ADR-005) | لا Unity/Android CI بعد**

كل build مستقبلي يحمل version وcommit SHA وbuild number وإصدار Unity وقفل الحزم والهدف وhash للـ artifact.

## CI الحالي (Domain Core)

- **بوابة نشطة:** `.github/workflows/ci.yml` — يعمل عند `push`/`pull_request` (ADR-005):
  `tools/audit.py` → `dotnet restore/build/test` → `test_parity.py` → محاكاة التوازن → 10 جلسات Playtest محاكاة → فحص أسرار heuristic → رفع `TestResults/` و`playtest-logs/` كـ artifacts.
- **الإصدارات المثبتة:** `global.json` (SDK 8.0.400) + `Directory.Packages.props` (NUnit 4.2.2 إلخ) — انظر `SOURCES_AND_REFERENCES.md` EXT-*.
- **ما ليس موجوداً:** بناء Unity/Android/Linux Dedicated Server — يبقى PROPOSED حتى prototype ADR-001.

## المسار المستهدف بعد Unity

مراجعة وADR → اختبارات → فحص أسرار واعتماديات → بناء Android وLinux → smoke على staging → التحقق من التوقيع والـ hash → نشر متدرج → مراقبة ورجوع.

مفاتيح التوقيع خارج المستودع. افصل debug عن release. لا تضع server credentials داخل العميل.

## قوالب GitHub Actions الأخرى

الملفات `codeql.yml`, `contrast-scan.yml`, `debricked.yml`, `frogbot-scan-and-fix.yml`, `mobsf.yml`, `nowsecure.yml`, `securitycodescan.yml` تبقى قوالب موردين على `workflow_dispatch` فقط (انظر `.github/workflows/README.md`). لا تُفعّل على `push` قبل وجود artifact حقيقي وأسرار مُعدّة وADR يسمي الأداة.

## المراجع الموحدة
- المصادر: `governance/SOURCES_AND_REFERENCES.md` EXT-DOTNET/EXT-NUNIT
- التنفيذ: `architecture/DOMAIN_CORE.md`
- الهيكل: `REPOSITORY_STRUCTURE.md`
