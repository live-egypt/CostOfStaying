namespace CostOfStaying.Core.Domain
{
    using System;

    /// <summary>Stable server-assigned player actor identity. Zero is an invalid/default sentinel.</summary>
    public readonly struct ActorId : IEquatable<ActorId>
    {
        public ActorId(ulong value) => Value = value;
        public ulong Value { get; }
        public bool IsValid => Value != 0UL;
        public bool Equals(ActorId other) => Value == other.Value;
        public override bool Equals(object? obj) => obj is ActorId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        public static bool operator ==(ActorId left, ActorId right) => left.Equals(right);
        public static bool operator !=(ActorId left, ActorId right) => !left.Equals(right);
    }

    /// <summary>Match identity. Zero is an invalid/default sentinel.</summary>
    public readonly struct MatchId : IEquatable<MatchId>
    {
        public MatchId(ulong value) => Value = value;
        public ulong Value { get; }
        public bool IsValid => Value != 0UL;
        public bool Equals(MatchId other) => Value == other.Value;
        public override bool Equals(object? obj) => obj is MatchId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        public static bool operator ==(MatchId left, MatchId right) => left.Equals(right);
        public static bool operator !=(MatchId left, MatchId right) => !left.Equals(right);
    }

    /// <summary>Authenticated server-side session identity. Zero is an invalid/default sentinel.</summary>
    public readonly struct SessionId : IEquatable<SessionId>
    {
        public SessionId(ulong value) => Value = value;
        public ulong Value { get; }
        public bool IsValid => Value != 0UL;
        public bool Equals(SessionId other) => Value == other.Value;
        public override bool Equals(object? obj) => obj is SessionId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        public static bool operator ==(SessionId left, SessionId right) => left.Equals(right);
        public static bool operator !=(SessionId left, SessionId right) => !left.Equals(right);
    }

    /// <summary>Transfer target identity. Zero is an invalid/default sentinel.</summary>
    public readonly struct VictimId : IEquatable<VictimId>
    {
        public VictimId(ulong value) => Value = value;
        public ulong Value { get; }
        public bool IsValid => Value != 0UL;
        public bool Equals(VictimId other) => Value == other.Value;
        public override bool Equals(object? obj) => obj is VictimId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        public static bool operator ==(VictimId left, VictimId right) => left.Equals(right);
        public static bool operator !=(VictimId left, VictimId right) => !left.Equals(right);
    }

    /// <summary>Server-assigned monotonic event sequence. Zero is an invalid/default sentinel.</summary>
    public readonly struct EventSequenceId : IEquatable<EventSequenceId>
    {
        public EventSequenceId(ulong value) => Value = value;
        public ulong Value { get; }
        public bool IsValid => Value != 0UL;
        public bool Equals(EventSequenceId other) => Value == other.Value;
        public override bool Equals(object? obj) => obj is EventSequenceId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        public static bool operator ==(EventSequenceId left, EventSequenceId right) => left.Equals(right);
        public static bool operator !=(EventSequenceId left, EventSequenceId right) => !left.Equals(right);
    }
}
