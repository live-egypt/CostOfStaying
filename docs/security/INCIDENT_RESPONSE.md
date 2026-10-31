# Incident Response — v2.0
**الحالة: PROPOSED**

دورة الاستجابة: Detection → Triage → Containment → Eradication → Recovery → Verification → Postmortem.

P0: اختراق signing key أو إصدار ضار أو RCE أو سيطرة على المستودع/الإنتاج. P1: تجاوز سلطة الخادم أو تسريب بيانات أو ثغرة حرجة. P2: فشل ضابط/إعداد خاطئ/ثغرة متوسطة. P3: مشكلة توثيق منخفضة.

احفظ وقت UTC وcommit SHA وworkflow run وartifact hash والسجلات المنقحة وخطوات إعادة الإنتاج ونطاق التأثير. لا تحذف الأدلة قبل حفظها. بعد الحادث: سبب جذري، ضوابط، regression tests، مسؤول وموعد، ثم تحقق إغلاق.
