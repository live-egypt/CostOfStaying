# Network Architecture — v2.0
**الحالة: PROPOSED**

Dedicated Server authoritative؛ العميل يرسل Intent فقط. الخادم يحسم الصحة والضرر والذخيرة والمؤقتات والنقل والطاقة والتصويت والنتيجة.

استخدم server monotonic clock، Request IDs، rate limits، حدود حجم الرسائل، validation، Snapshot عند الانضمام/إعادة الاتصال وDelta عند التغيير.

اختبر tick rates 10/15/20Hz ولا تثبت 20Hz قبل قياس CPU وbandwidth وlatency. معدل الشبكة مستقل عن render FPS وUnity physics timestep.

اختبر RTT 100/200/300ms، loss 1/3/5%، jitter، packet reordering، disconnect/reconnect. قواعد مهلة إعادة الاتصال وحفظ الحالة يجب اعتمادها قبل alpha.
