# Build and Release — v2.0
**الحالة: PROPOSED**

كل build يحمل version وcommit SHA وbuild number وUnity version وpackage lock وtarget وartifact hash.

المسار: review/ADR → CI/tests → secret/dependency scan → Android + Linux server build → staging smoke test → verify hash/signing → staged rollout → monitoring/rollback.

Signing keys خارج المستودع. افصل debug/release. لا تشحن server credentials داخل العميل. احتفظ بآخر artifact سليم. هذه خطة مستهدفة وليست وصفاً لـ CI منفذ.
