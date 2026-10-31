# Security Architecture — v2.0
**الحالة: PROPOSED**

الأصول: حالة المباراة، الهوية والجلسات، نتائج المباريات، خادم اللعبة، مفاتيح Backend والتوقيع، artifacts، ومستودع المصدر.

المبادئ: server authority، least privilege، validate all inputs، deny by default، rate limit، replay protection، secrets خارج Git، سجلات لا تكشف البيانات الحساسة، ومراجعة dependencies.

Anti-cheat لا يعوض عن server authority. العميل بيئة غير موثوقة؛ لا يمكن ضمان منع كل التلاعب به، لكن يجب حماية منطق المباراة ونتائجها.

بوابة الإصدار: لا Critical/High findings مفتوحة دون قبول مخاطر موثق، ولا أسرار في Git history أو artifacts.
