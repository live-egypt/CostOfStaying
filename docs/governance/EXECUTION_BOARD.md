# لوحة التنفيذ — 8 بوابات / 40 مهمة
**الحالة: PROPOSED | التاريخ: 2026-11-14 | الفرع: `arena/379b19df-costofstaying` | المالك: Governor + Verifier | تحدّث مع كل PR**

> كل مهمة = بطاقة واحدة. الحالات: `TODO → DOING → REVIEW → DONE (SHA)`. لا DONE بدون SHA+artifact.

---

## GATE-0 — التثبيت (2ي) — فتح: الآن | إغلاق: audit 0/0

| # | المهمة | الوكيل | الحجم | الحالة | الدليل |
|---|---|---|---|---|---|
| 0.1 | قفل `Directory.Build.props` 9.0 + WarnAsError | Architect | S | DONE `cb57b25`→`379b19df` | `dotnet build` log |
| 0.2 | `dependabot.yml` + SHA-pin (منجز) | DevOps | S | DONE `4294eff`→`379b19df` | `ci.yml` + `dependabot.yml` |
| 0.3 | `test_parity.py` 6 حالات | Domain | M | DONE `379b19df` | `parity 6/6` |
| 0.4 | `simulate.py` استراتيجي (cooldown/capacity/range-aware, archetypes, vote vitality-aware) | Balancer | M | DONE `379b19df-2` | `sweep.csv` 3 نتائج (529/540 LastSurvivor, 9 Mutual, 2 Collective vs سابق 100% واحد) |
| 0.5 | تنظيف `Clone()` + حد `SentAt` | Domain | S | DONE `379b19df` | `grep Clone` + `Intent SentAt` docs |
| 0.6 | ترويسة `DECISIONS_LOG` | Governor | XS | DONE `379b19df` | `audit.py` 0/0 — لا Warn |

## GATE-1 — القواعد (3أ) — فتح: بعد GATE-0

| # | المهمة | الوكيل | الحجم | الحالة | الدليل |
|---|---|---|---|---|---|
| 1.1 | 3 سيناريوهات Playtest (SOC/TAC/SPI) | Oracle | M | DONE `379b19df` | `scenarios/*.md` 3 سيناريوهات + README |
| 1.2 | 10 جلسات بشرية | Verifier+Oracle | L | TODO | 10 JSON human |
| 1.3 | تحليل + BALANCE_RISK_REVIEW | Architect+Balancer | M | TODO | report.md |
| 1.4 | تحديث ADR-004 | Governor | S | TODO | PR |
| 1.5 | توضيح COMBAT vs Vitality (فصل GATE-1.5) | Domain | S | DONE `379b19df` | `COMBAT_SYSTEM` فصل + `GLOSSARY` مصطلح Combat Damage |

## GATE-2 — التكدس (4أ) — فتح: بعد GATE-1

| # | المهمة | الوكيل | الحجم | الحالة | الدليل |
|---|---|---|---|---|---|
| 2.1 | مقارنة FishNet/Netcode + ADR-001 | Unity+Architect | M | DONE `379b19df` | `STACK_COMPARISON.md` (3 بدائل, 8 معايير, توصية FishNet + شرط prototype) |
| 2.2 | Unity فارغ + APK + Linux | DevOps | M | TODO | APK artifact |
| 2.3 | Baseline PSS/CPU | Verifier | S | TODO | perf_baseline.json |
| 2.4 | ADR-001 APPROVED | Governor | XS | TODO | ADR file |

## GATE-3 — الشبكة (6أ) — فتح: بعد GATE-2

| # | المهمة | الوكيل | الحجم | الحالة | الدليل |
|---|---|---|---|---|---|
| 3.1 | نقل Domain→Core + NetArchTest (Core+BetrayalLedger+boundary) | Architect | L | DONE `379b19df` — جزئي: Core project + BetrayalLedger (ADR-006) + boundary tests | 242 tests |


| 3.2 | NetworkAdapter + Envelope (ADR-008 Sequence) | Net Sage | M | DONE `379b19df` — CommandEnvelope + SequenceValidator 4 tests | adapter tests |
| 3.3 | DedicatedServer + Snapshot + إعادة اتصال | Net Sage | L | TODO | server log |
| 3.4 | AuthZ + Payload fail-closed (IntentValidator) | Sentinel | M | DONE `379b19df` | `IntentValidator.cs` + `THREAT_MODEL` + `NETWORK_ARCH` حدود موثوقة |
| 3.5 | اختبارات RTT/loss/jitter | Verifier | M | TODO | net_tests.log |
| 3.6 | إثبات 4 أجهزة + فيديو | Verifier | M | TODO | video.mp4 |

## GATE-4 — Vertical Slice (8أ) — فتح: بعد GATE-3

| # | المهمة | الوكيل | الحجم | الحالة | الدليل |
|---|---|---|---|---|---|
| 4.1 | خريطة + لمس + UI | Unity | L | TODO | APK |
| 4.2 | Transfer FX + Betrayal | Domain+Unity | M | TODO | screenshot |
| 4.3 | Spirit + Echo + Hint | Domain | M | TODO | demo |
| 4.4 | Combat 4 أسلحة | Domain | M | TODO | hit validation |
| 4.5 | Endgame 5 نتائج | Unity | S | TODO | UI video |

## GATE-5 — بوابات الإصدار (4أ) — فتح: بعد GATE-4

| # | المهمة | الوكيل | الحجم | الحالة | الدليل |
|---|---|---|---|---|---|
| 5.1 | CodeQL + SAST (dependabot + SHA-pin منفذان) | Sentinel | M | DONE جزئي `379b19df` | `THREAT_MODEL` + `dependabot.yml` (كامل بعد GATE-5.1) |
| 5.2 | قياس Android+Server — هيكل baseline | Performance | M | DONE `379b19df` | `PERFORMANCE_BUDGET v2.1` هيكل + بروتوكول (بلا أرقام معتمدة) |
| 5.3 | 50 مباراة stability | Verifier | M | TODO | 50 logs |
| 5.4 | ACCEPTANCE SHA+hash | Governor | S | TODO | ACCEPTANCE.md |

## GATE-6 — الإطلاق (3أ) — فتح: بعد GATE-5

| # | المهمة | الوكيل | الحجم | الحالة | الدليل |
|---|---|---|---|---|---|
| 6.1 | Release + Play Internal | DevOps | M | TODO | RC |
| 6.2 | Monitoring + Backup + Backend | Architect+DevOps | M | TODO | dashboards |
| 6.3 | إغلاق 7 ADRs + Alpha | Governor | S | TODO | DELIVERY_PLAN |

## GATE-7 — LiveOps (∞) — فتح: بعد GATE-6

متجر، خرائط، Voice، iOS — كل بقرار نطاق جديد.

---

## مقاييس لوحة التحكم (تحدّث أسبوعياً بواسطة Verifier)

- **WIP حد:** 3 مهام DOING لكل وكيل كحد أقصى.
- **Lead time مستهدف:** S=1ي، M=1أ، L=2أ.
- **Health:** `audit 0/0`, `tests 100%`, `CI أخضر`, `gitleaks 0`.
