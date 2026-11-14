using System;

namespace CostOfStaying.Domain
{
    /// <summary>Domain event kinds emitted by the server for replication to clients.</summary>
    public enum DomainEventKind
    {
        MatchStarted,
        PlayerDied,
        CollapseApplied,
        DebuffTransferred,
        TransferRejected,
        BetrayalIncreased,
        VoteOpened,
        VoteCast,
        VoteClosed,
        EchoCollected,
        HintSent,
        MatchEnded
    }

    /// <summary>
    /// Immutable record of a server-side state change. The network adapter
    /// replicates these to clients; clients never originate them (ADR-002).
    /// </summary>
    public sealed class DomainEvent
    {
        public DomainEventKind Kind { get; }
        public double AtSeconds { get; }
        public Guid? PlayerId { get; }
        public Guid? TargetId { get; }
        public string? Detail { get; }

        public DomainEvent(DomainEventKind kind, double atSeconds, Guid? playerId = null, Guid? targetId = null, string? detail = null)
        {
            Kind = kind;
            AtSeconds = atSeconds;
            PlayerId = playerId;
            TargetId = targetId;
            Detail = detail;
        }
    }
}
