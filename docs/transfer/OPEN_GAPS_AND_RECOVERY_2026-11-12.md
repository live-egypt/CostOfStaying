# الفجوات، التعارضات وخطة الاستعادة — 2026-11-12

## 1. حالة M1

**M1 = PARTIAL / OPEN.** حُفظت لقطة الملفات المتاحة وسجلات GitHub الأساسية، لكن لا توجد أدلة كافية لإغلاق المصدر الأصلي أو اكتمال محادثات المشروع.

| Gap | الوصف | الحالة | ما يغلق الفجوة |
|---|---|---|---|
| G01 | المحادثات الأصلية المستهدفة ومعرّفات threads | OPEN | تصدير أصلي كامل/معرفات threads ومطابقة مستقلة |
| G02 | جميع كائنات المرفقات التاريخية، خاصة الملفات المتشابهة بالاسم | PARTIAL | Crosswalk كامل بين file IDs والبايتات والمسارات والنسخ |
| G03 | جرد كامل لمكتبة المشروع ومحتوى الملفات الخام | PARTIAL | listing حديث كامل + raw export حيث يسمح النظام + checksums |
| G04 | محادثة 1/2، محادثة 2.html، DeepSeek/ChatGPT | PARTIAL | المصدر الأولي، thread ID، إثبات المنصة، واكتمال المقاطع |
| G05 | مشروع ZIP وحزمة Unity الأصلية | PARTIAL | استعادة ZIP الأصلي والتحقق من checksum والملفات الداخلية وسلسلة المنشأ |
| G06 | تاريخ GitHub وكل الفروق والحزم | PARTIAL | تدقيق كل commits/branches/PRs والملفات والأدلة، لا الأوصاف فقط |

## 2. حدود محادثة 1/2 وHTML

سجلات PR #11 المحدثة 2026-11-12 تنقل ما يلي:
- مالك المشروع attests أن مراجع DeepSeek وتسميتي Project chat 1 و2 و`محادثة 2.html` تخص محادثة واحدة underlying.
- HTML يعرّف نفسه كـ ChatGPT Shared Chat، ويرتبط بعنوان chatgpt.com/share.
- جرى في سجل الفرع وصف 79,302 سطر و449 turn element (226 user و223 assistant).
- المالك حذر من أن بعض أجزاء المحادثة قد توجد في أماكن أخرى ولا تكون موجودة في هذا التصدير.

**هذه علاقة attested وبيانات وصفية منقولة في branch docs؛ ليست تحققاً تقنياً من thread ID أو من منصة المنشأ أو اكتمال الملف.** لا تحسم هل المحادثة الأصلية ChatGPT أم DeepSeek بالاستنتاج من HTML فقط. الملف HTML الخام غير متاح ضمن أرشيف TXT الـ18 المرفق مع هذه الحزمة.

## 3. أعداد المرفقات والمكتبة — لا تجمع اللقطات

| اللقطة | العدد | تفسيرها |
|---|---:|---|
| Source inventory PR #8 بتاريخ 2026-11-09 | 15 attachment objects | كائنات مرفقات Project في لقطة محددة |
| Delta الأول في PR #11 | +5 = 20 | لقطة تالية |
| Delta الثاني في PR #11 | +1 = 21 | لقطة تالية لاحقة |
| mounted TXT archive الحالي | 18 files | ملفات TXT المتاحة في runtime لهذه المحادثة |
| Project Library snapshot PR #8 | 40 files + 1 folder | بيانات metadata بتاريخ اللقطة |
| Project Library snapshot PR #11 | 45 file IDs + 1 folder | listing لاحق؛ سجل الفرع يقول 40/40 تاريخية بقيت و5 إضافية |
| main tree 2026-11-12 | 53 blobs + 11 trees | Git tree؛ ليس عدداً لمرفقات ChatGPT |

هذه الأعداد تنتمي إلى أسطح وأزمنة مختلفة. لا تُجمع ولا تستبدل بعضها بعضاً.

## 4. تعارضات ووثائق غير محسومة

| البند | الحالة |
|---|---|
| DOC-PACKAGE-01: حسم المسار المرجعي لحزم #8/#9/#11 | OPEN |
| PDR-012 / #18 | PARTIAL؛ source universe وtechnical thread mapping غير مكتملين |
| PDR-013 / #19 | PARTIAL؛ تعريف DIFF-XXX الأصلي غير مستعاد |
| PDR-014 / #20 | CANNOT RESOLVE؛ SO-GAP-04 primary source غير مستعاد |
| PDR-016 | OPEN؛ المقارنة الدلالية للتسعة shared paths مهمة مستقلة |
| ChatGPT / DeepSeek origin | OPEN |
| 85 TBD rows | مسجلة؛ لا يعني تسجيلها حلها أو التحقق من مصادرها |
| 109 consistency records | سجل بنيوي منفصل عن 85 TBD؛ لا تدمج العدّين |
| Network Audit original | SOURCE PARTIAL؛ الوثائق المتوفرة توفيقات أولية وليست الأصل كاملاً |
| Security Audit original | SOURCE PARTIAL؛ ملف 5.txt توفيق أولي وليس المصدر الأصلي كاملاً |
| Risk Register / Technical Debt | مؤجلان حتى توفيق المصادر والمعرفات |
| Final readiness | NOT ISSUED |

## 5. فروق حوكمة يجب المحافظة عليها

- حالة القرار مستقلة عن نضج الدليل.
- APPROVED لا تعني IMPLEMENTED.
- IMPLEMENTED لا تعني VERIFIED.
- وجود وثيقة أو فرع أو PR لا يثبت صحة محتواه أو تنفيذه.
- نتيجة اختبار فرعية لا تثبت Unity/FishNet/Android/Linux runtime.
- PDR أو ADR لا يتغير status إلا بقرار صاحب الصلاحية وبمصدر مسجل.
- لا تحول تعارضاً إلى توافق عبر صياغة ملخصية؛ سجل الادعاءات والأدلة المتعارضة منفصلة.

## 6. ترتيب الاستكمال بعد النقل

1. التأكد من حفظ الأرشيف المحلي ووجوده داخل المستودع على فرع migration بعد مراجعة PR.
2. M1-G01/G04: استعادة المصدر الأصلي للمحادثات ومعرّفاتها، ومطابقة HTML دون افتراض منصة المنشأ.
3. M1-G02/G03: إعادة جرد Project Files وLibrary بمعرفات ثابتة ونسخ خام حيث يسمح النظام.
4. M1-G05: استعادة ZIP المشروع/Unity الأصلي ومقارنة SHA والقائمة الداخلية.
5. M1-G06 وDOC-PACKAGE-01: semantic crosswalk للمسارات التسعة، PDR-013/016، ثم قرار المالك بشأن المرجع canonical.
6. لا تبدأ M2 تلقائياً ولا تنشئ Risk/Technical Debt/Code Gate قبل إغلاق مدخلات المرحلة أو قرار موثق بتعليقها.

## 7. شرط إغلاق M1

لا يغلق M1 بمجرد وجود inventory في GitHub. يلزم على الأقل:
- source/thread inventory له حدود معلنة ومعرّفات قابلة للتتبع.
- crosswalk المرفقات وLibrary IDs والنسخ وبصماتها حيث raw bytes متاحة.
- تعارض المنصة والمحادثات إما محسوم بدليل أولي أو مسجل كـ suspended/unresolved بقرار صاحب الصلاحية.
- أصل Network/Security/Unity source محدد أو gap disposition موثق.
- GitHub branches/PRs والمراجع المتقاطعة موثقة.
- مراجعة مستقلة وقبول owner قبل أي stage closure.

## 8. فجوة حفظ أرشيف النقل داخل GitHub

**TRANSFER-ARCHIVE-01 = VERIFIED LOCALLY / NOT COMMITTED TO REPOSITORY.**

- ملف ZIP موجود كتنزيل في محادثة ChatGPT الحالية، وحجمه 60,375 bytes.
- SHA-256 المتوقع: `572222b1185c25483aad71b30c80441215b803190ab8cb56d14ac3ba56e0266b`.
- فحص ZIP نجح؛ 20 entries؛ 18 TXT files؛ جميع أحجام/أسطر/SHA-256 للـ18 ملف تطابقت مع `SOURCE_MANIFEST.json`.
- محتوى ZIP الثنائي نفسه غير موجود في فرع GitHub حتى لقطة هذه المراجعة. الموجود في PR #22 هو manifest والوثائق التي تصف الأرشيف وحدوده.

### الإجراء لإغلاق فجوة النقل

1. نزّل ZIP من المحادثة الحالية واحفظ نسختين محليتين.
2. افتح GitHub branch `docs/chatgpt-project-transfer-archive-2026-11-12` من صفحة المستودع.
3. اختر **Add file → Upload files**، وارفع ZIP إلى المسار `docs/transfer/source-attachments/CostOfStaying_uploaded_text_sources_2026-11-12.zip`.
4. اجعل commit على فرع النقل نفسه، لا على `main`، برسالة مثل `docs: add verified source attachment archive`.
5. بعد الرفع، أعد تنزيل الملف من GitHub واحسب SHA-256 محلياً. لا تغلق TRANSFER-ARCHIVE-01 إلا إذا تطابق الحجم والبصمة مع القيم أعلاه.
6. تحقق من ظهور الملف ضمن Changes في PR #22 قبل الدمج.

هذه فجوة خاصة بأرشيف النقل ولا تغير سجل M1-G01…G06 ولا تعني أن M1 أُغلق.
