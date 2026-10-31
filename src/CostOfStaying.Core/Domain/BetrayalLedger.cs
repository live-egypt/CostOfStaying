namespace CostOfStaying.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using CostOfStaying.Core.Contracts;

    public readonly struct BetrayalEvent : IEquatable<BetrayalEvent>
    {
        public BetrayalEvent(EventSequenceId sequenceId, ActorId actorId, VictimId victimId, ulong serverTick)
        {
            SequenceId = sequenceId;
            ActorId = actorId;
            VictimId = victimId;
            ServerTick = serverTick;
        }

        public EventSequenceId SequenceId { get; }
        public ActorId ActorId { get; }
        public VictimId VictimId { get; }
        public ulong ServerTick { get; }

        public bool Equals(BetrayalEvent other) =>
            SequenceId == other.SequenceId &&
            ActorId == other.ActorId &&
            VictimId == other.VictimId &&
            ServerTick == other.ServerTick;

        public override bool Equals(object? obj) => obj is BetrayalEvent other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(SequenceId, ActorId, VictimId, ServerTick);
    }

    /// <summary>
    /// In-memory, single-writer primitive. The server application layer owns sequencing
    /// and must serialize calls; this class intentionally provides no threading or persistence.
    /// </summary>
    public sealed class BetrayalLedger : IBetrayalLedger
    {
        private readonly Dictionary<EventSequenceId, BetrayalEvent> _events =
            new Dictionary<EventSequenceId, BetrayalEvent>();

        private readonly Dictionary<ActorId, ulong> _scores =
            new Dictionary<ActorId, ulong>();

        private bool _hasSequence;
        private ulong _lastSequence;

        public ulong ScoreFor(ActorId actorId) =>
            _scores.TryGetValue(actorId, out ulong score) ? score : 0UL;

        public bool TryRecord(BetrayalEvent betrayalEvent)
        {
            if (_events.TryGetValue(betrayalEvent.SequenceId, out BetrayalEvent existing))
            {
                if (existing.Equals(betrayalEvent))
                    return false;

                throw new InvalidOperationException("An event sequence ID cannot be reused with different event data.");
            }

            if (_hasSequence && betrayalEvent.SequenceId.Value <= _lastSequence)
                throw new InvalidOperationException("Event sequence IDs must be strictly increasing.");

            _events.Add(betrayalEvent.SequenceId, betrayalEvent);
            _lastSequence = betrayalEvent.SequenceId.Value;
            _hasSequence = true;

            ulong current = ScoreFor(betrayalEvent.ActorId);
            if (current == ulong.MaxValue)
                throw new OverflowException("Betrayal score overflow.");

            _scores[betrayalEvent.ActorId] = current + 1UL;
            return true;
        }
    }
}
