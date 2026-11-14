using System;
using System.Collections.Generic;

namespace CostOfStaying.Core
{
    /// <summary>
    /// ADR-006 — Betrayal ledger invariants (GATE-3.1).
    /// Server-owned, monotonic, replay-safe.
    /// Invariants enforced here:
    /// - Ids are ulong, 0 is sentinel invalid (matchId, actorId, victimId non-zero, actor≠victim)
    /// - Sequence strictly increasing per match (no gaps/duplicates allowed by caller)
    /// - Replay detection via (matchId, sequence) key — conflicting replay throws.
    /// - Betrayal is cost-only (ADR-022) — this ledger tracks count, not win.
    /// </summary>
    public sealed class BetrayalLedger
    {
        private readonly Dictionary<(ulong matchId, ulong sequence), BetrayalEvent> _byKey = new();

        public IReadOnlyCollection<BetrayalEvent> Events => _byKey.Values;

        public BetrayalEvent Record(ulong matchId, ulong actorId, ulong victimId, ulong sequence, double atSeconds)
        {
            if (matchId == 0) throw new ArgumentException("matchId 0 is sentinel invalid", nameof(matchId));
            if (actorId == 0) throw new ArgumentException("actorId 0 invalid", nameof(actorId));
            if (victimId == 0) throw new ArgumentException("victimId 0 invalid", nameof(victimId));
            if (actorId == victimId) throw new ArgumentException("actor≠victim required");
            if (sequence == 0) throw new ArgumentException("sequence 0 invalid — starts at 1");
            if (atSeconds < 0) throw new ArgumentException("atSeconds negative");

            var key = (matchId, sequence);
            if (_byKey.TryGetValue(key, out var existing))
            {
                // Idempotent replay: same actor/victim/at → return existing
                if (existing.ActorId == actorId && existing.VictimId == victimId && Math.Abs(existing.AtSeconds - atSeconds) < 1e-9)
                    return existing;
                throw new InvalidOperationException($"Conflicting replay for match {matchId} seq {sequence}");
            }

            // Strictly increasing: caller must ensure monotonic — we verify previous exists unless seq==1
            if (sequence > 1 && !_byKey.ContainsKey((matchId, sequence - 1)))
                throw new InvalidOperationException($"Sequence gap: missing {sequence - 1} for match {matchId}");

            var ev = new BetrayalEvent(matchId, actorId, victimId, sequence, atSeconds);
            _byKey[key] = ev;
            return ev;
        }

        public int CountFor(ulong matchId, ulong actorId)
        {
            int c = 0;
            foreach (var e in _byKey.Values)
                if (e.MatchId == matchId && e.ActorId == actorId) c++;
            return c;
        }
    }

    public sealed class BetrayalEvent
    {
        public ulong MatchId { get; }
        public ulong ActorId { get; }
        public ulong VictimId { get; }
        public ulong Sequence { get; }
        public double AtSeconds { get; }

        public BetrayalEvent(ulong matchId, ulong actorId, ulong victimId, ulong sequence, double atSeconds)
        {
            MatchId = matchId;
            ActorId = actorId;
            VictimId = victimId;
            Sequence = sequence;
            AtSeconds = atSeconds;
        }
    }
}
