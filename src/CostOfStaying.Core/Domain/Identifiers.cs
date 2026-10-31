namespace CostOfStaying.Core.Domain
{
    using System;

    public readonly struct ActorId : IEquatable<ActorId>
    {
        public ActorId(ulong value) => Value = value;
        public ulong Value { get; }
        public bool Equals(ActorId other) => Value == other.Value;
        public override bool Equals(object? obj) => obj is ActorId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        public static bool operator ==(ActorId left, ActorId right) => left.Equals(right);
        public static bool operator !=(ActorId left, ActorId right) => !left.Equals(right);
    }

    public readonly struct VictimId : IEquatable<VictimId>
    {
        public VictimId(ulong value) => Value = value;
        public ulong Value { get; }
        public bool Equals(VictimId other) => Value == other.Value;
        public override bool Equals(object? obj) => obj is VictimId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        public static bool operator ==(VictimId left, VictimId right) => left.Equals(right);
        public static bool operator !=(VictimId left, VictimId right) => !left.Equals(right);
    }

    public readonly struct EventSequenceId : IEquatable<EventSequenceId>
    {
        public EventSequenceId(ulong value) => Value = value;
        public ulong Value { get; }
        public bool Equals(EventSequenceId other) => Value == other.Value;
        public override bool Equals(object? obj) => obj is EventSequenceId other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        public static bool operator ==(EventSequenceId left, EventSequenceId right) => left.Equals(right);
        public static bool operator !=(EventSequenceId left, EventSequenceId right) => !left.Equals(right);
    }
}
