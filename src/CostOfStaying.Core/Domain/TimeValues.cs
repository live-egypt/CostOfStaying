namespace CostOfStaying.Core.Domain
{
    using System;

    /// <summary>Server-assigned monotonic simulation tick. Zero is a valid initial tick.</summary>
    public readonly struct ServerTick : IEquatable<ServerTick>, IComparable<ServerTick>
    {
        public ServerTick(ulong value) => Value = value;
        public ulong Value { get; }
        public int CompareTo(ServerTick other) => Value.CompareTo(other.Value);
        public bool Equals(ServerTick other) => Value == other.Value;
        public override bool Equals(object? obj) => obj is ServerTick other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        public static bool operator ==(ServerTick left, ServerTick right) => left.Equals(right);
        public static bool operator !=(ServerTick left, ServerTick right) => !left.Equals(right);
        public static bool operator <(ServerTick left, ServerTick right) => left.Value < right.Value;
        public static bool operator >(ServerTick left, ServerTick right) => left.Value > right.Value;
        public static bool operator <=(ServerTick left, ServerTick right) => left.Value <= right.Value;
        public static bool operator >=(ServerTick left, ServerTick right) => left.Value >= right.Value;
    }

    /// <summary>Monotonic authoritative state revision. Zero represents the initial state.</summary>
    public readonly struct StateRevision : IEquatable<StateRevision>, IComparable<StateRevision>
    {
        public StateRevision(ulong value) => Value = value;
        public ulong Value { get; }

        public bool TryAdvance(out StateRevision next)
        {
            if (Value == ulong.MaxValue)
            {
                next = this;
                return false;
            }

            next = new StateRevision(Value + 1UL);
            return true;
        }

        public int CompareTo(StateRevision other) => Value.CompareTo(other.Value);
        public bool Equals(StateRevision other) => Value == other.Value;
        public override bool Equals(object? obj) => obj is StateRevision other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        public static bool operator ==(StateRevision left, StateRevision right) => left.Equals(right);
        public static bool operator !=(StateRevision left, StateRevision right) => !left.Equals(right);
        public static bool operator <(StateRevision left, StateRevision right) => left.Value < right.Value;
        public static bool operator >(StateRevision left, StateRevision right) => left.Value > right.Value;
        public static bool operator <=(StateRevision left, StateRevision right) => left.Value <= right.Value;
        public static bool operator >=(StateRevision left, StateRevision right) => left.Value >= right.Value;
    }
}
