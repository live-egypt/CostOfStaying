# Secrets Management — v2.1
**الحالة: PROPOSED | لا يوجد مخزن أسرار منفذ في هذه الحزمة**

لا تحفظ API keys أو tokens أو signing keys أو passwords في Git أو ScriptableObjects أو إعدادات العميل أو logs. التعمية ليست حماية للأسرار.

عند وجود بناء: أسرار خارج المستودع، أقل صلاحية، فصل البيئات، وتدوير عند الاشتباه. قوالب GitHub Actions الحالية ليست فحص أسرار ناجحاً. لا يمر إصدار مع secret مؤكد عند وجود خط إصدار.
