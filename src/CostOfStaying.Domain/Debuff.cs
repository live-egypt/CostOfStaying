namespace CostOfStaying.Domain
{
    /// <summary>
    /// A negative effect with a value, a remaining duration and a transferability
    /// flag (GLOSSARY: Debuff). Transfer moves the whole debuff including its
    /// remaining duration and decay contribution (RULE-02: Move, not Copy).
    /// </summary>
    public sealed class Debuff
    {
        public string Id { get; }

        /// <summary>Extra vitality decay per second while this debuff is active.</summary>
        public double DecayBonusPerSecond { get; }

        /// <summary>Seconds remaining. Decremented by the server tick.</summary>
        public double RemainingSeconds { get; internal set; }

        /// <summary>Whether this debuff can be moved by Suffering Transfer.</summary>
        public bool Transferable { get; }

        public Debuff(string id, double decayBonusPerSecond, double durationSeconds, bool transferable)
        {
            Id = id;
            DecayBonusPerSecond = decayBonusPerSecond;
            RemainingSeconds = durationSeconds;
            Transferable = transferable;
        }

        internal Debuff Clone()
        {
            return new Debuff(Id, DecayBonusPerSecond, RemainingSeconds, Transferable);
        }
    }
}
