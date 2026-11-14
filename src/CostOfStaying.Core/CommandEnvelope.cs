using System;

namespace CostOfStaying.Core
{
    /// <summary>
    /// ADR-008 — مغلف أوامر الخادم وقبول التسلسل (GATE-3.2).
    /// كل أمر من العميل يصل مغلفاً ببيانات موثقة: MatchId, ActorId, Sequence, SentAt.
    /// الخادم هو من يحسم التسلسل — العميل لا يقرر ترتيباً.
    /// </summary>
    public sealed class CommandEnvelope
    {
        public Guid CommandId { get; }
        public ulong MatchId { get; }
        public ulong ActorId { get; }
        public ulong Sequence { get; }
        public double SentAtSeconds { get; }
        public string CommandType { get; }
        public string PayloadJson { get; }

        public CommandEnvelope(Guid commandId, ulong matchId, ulong actorId, ulong sequence, double sentAtSeconds, string commandType, string payloadJson = "{}")
        {
            if (commandId == Guid.Empty) throw new ArgumentException("CommandId empty", nameof(commandId));
            if (matchId == 0) throw new ArgumentException("matchId 0 sentinel", nameof(matchId));
            if (actorId == 0) throw new ArgumentException("actorId 0", nameof(actorId));
            if (sequence == 0) throw new ArgumentException("sequence 0 invalid — starts at 1", nameof(sequence));
            if (string.IsNullOrWhiteSpace(commandType)) throw new ArgumentException("commandType empty");
            if (sentAtSeconds < 0) throw new ArgumentException("sentAt negative");

            CommandId = commandId;
            MatchId = matchId;
            ActorId = actorId;
            Sequence = sequence;
            SentAtSeconds = sentAtSeconds;
            CommandType = commandType;
            PayloadJson = payloadJson ?? "{}";
        }
    }

    /// <summary>
    /// Validates per-actor monotonic sequence (ADR-008 + IntentValidator).
    /// Rejects old/replayed sequences; gaps allowed but must be monotonic increasing.
    /// </summary>
    public sealed class CommandSequenceValidator
    {
        private readonly System.Collections.Generic.Dictionary<ulong, ulong> _lastSeqByActor = new();

        public bool TryAccept(CommandEnvelope env, out string rejectReason)
        {
            rejectReason = string.Empty;
            if (!_lastSeqByActor.TryGetValue(env.ActorId, out var last))
            {
                // First command for actor — any seq >=1 accepted (but we store)
                _lastSeqByActor[env.ActorId] = env.Sequence;
                return true;
            }
            if (env.Sequence <= last)
            {
                rejectReason = $"sequence {env.Sequence} <= last {last} for actor {env.ActorId}";
                return false;
            }
            _lastSeqByActor[env.ActorId] = env.Sequence;
            return true;
        }

        public void Reset(ulong actorId) => _lastSeqByActor.Remove(actorId);
        public ulong LastFor(ulong actorId) => _lastSeqByActor.TryGetValue(actorId, out var v) ? v : 0;
    }
}
