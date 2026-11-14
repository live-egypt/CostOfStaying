# مصفوفة RACI — 10 وكلاء × 8 بوابات × 40 مهمة
**الحالة: PROPOSED | التاريخ: 2026-11-14 | الفرع: `arena/6c5729d1-costofstaying` | يلتزم بها كل PR**

**R=مسؤول تنفيذ، A=مُقر نهائي، C=مستشار، I=مطّلع**

---

## 1) RACI بالبوابات

| البوابة | Governor | Architect | Domain | Net Sage | Sentinel | Unity Forger | Balancer | Oracle | Verifier | DevOps |
|---|---|---|---|---|---|---|---|---|---|---|
| **0 تثبيت** | A | R | C | C | C | I | I | I | R | R |
| **1 قواعد** | A | R | R | I | I | I | R | R | R | I |
| **2 تكدس** | A | R | C | C | I | R | I | I | R | R |
| **3 شبكة** | A | R | R | R | R | I | I | I | R | C |
| **4 Slice** | A | C | R | C | I | R | R | R | R | I |
| **5 بوابات** | A | I | C | I | R | I | R | I | R | C |
| **6 إطلاق** | A | C | I | I | R | R | I | I | R | R |
| **7 LiveOps** | A | R | R | R | R | R | R | R | R | R |

## 2) RACI بالمهام الحرجة (عينة)

| # | المهمة الحرجة | R | A | C | I |
|---|---|---|---|---|---|
| 0.1 | قفل 9.0 + WarnAsError | Architect | Governor | Verifier | All |
| 1.2 | 10 جلسات بشرية (إلزامي) | Verifier | Governor | Oracle, Domain | Architect |
| 1.3 | تحليل Betrayal 5 أسئلة | Balancer | Governor | Architect, Oracle | Verifier |
| 2.1 | FishNet vs Netcode | Unity | Governor | Architect, Net Sage | Verifier |
| 3.1 | نقل Domain→Core | Architect | Governor | Domain, Net | Sentinel |
| 3.4 | AuthZ fail-closed | Sentinel | Governor | Domain, Net | Verifier |
| 4.1 | خريطة Slice | Unity | Governor | Domain, Balancer | Verifier |
| 5.1 | CodeQL/SAST | Sentinel | Governor | Verifier | DevOps |
| 5.2 | PERF_BUDGET | Verifier | Governor | Unity, Net | Sentinel |
| 6.3 | إغلاق 7 ADRs | Governor | Governor | All | All |

## 3) قواعد RACI

1. **R واحد فقط** لكل مهمة — لا "مسؤولان".
2. **A واحد فقط** — دائماً Governor للتوثيق، والمالك التقني للتنفيذ.
3. **C قبل البدء:** أي مهمة تحتاج C يجب استشارته بـ issue/comment قبل PR.
4. **I بعد الدمج:** يُبلغ عبر `EXECUTION_BOARD.md` + إشعار PR.

## 4) التسليم بين R→A

```
R ينفذ → يفتح PR + دليل → A يراجع (24س) → Verifier يشغل CI → A يدمج → Governor يحدّث BOARD
```

لا مهمة تُغلق بدون `SHA + artifact hash` في `REQUIREMENTS_TRACEABILITY`.
