using System;
using System.Collections.Generic;

namespace CostOfStaying.Domain
{
    /// <summary>
    /// Server-owned player state. The client never writes these fields
    /// directly; it sends intents that the server validates (ADR-002).
    /// </summary>
    public sealed class Player
    {
        public Guid Id { get; }

        public PlayerState State { get; internal set; } = PlayerState.Connecting;

        /// <summary>Current vitality. Death occurs only when this reaches zero (RULE-01).</summary>
        public double Vitality { get; internal set; }

        /// <summary>
        /// Current base decay rate. Increases with each collapse event (RULE-01).
        /// Debuff contributions are added on top at tick time.
        /// </summary>
        public double BaseDecayPerSecond { get; internal set; }

        /// <summary>Active debuffs, in application order. The last transferable one is the transfer candidate.</summary>
        public List<Debuff> Debuffs { get; } = new List<Debuff>();

        /// <summary>Betrayal points. Cost only; never win points (RULE-05).</summary>
        public int BetrayalPoints { get; internal set; }

        /// <summary>Server time of the last successful transfer (source cooldown).</summary>
        public double LastTransferAtSeconds { get; internal set; } = double.NegativeInfinity;

        /// <summary>Server time at which the next collapse fires for this player.</summary>
        public double NextCollapseAtSeconds { get; internal set; }

        /// <summary>Interval used for the current collapse schedule (for ratio-preserving reschedule).</summary>
        public double LastCollapseIntervalSeconds { get; internal set; }

        // ---- Spirit (RULE-04) ----

        public int EchoFragments { get; internal set; }
        public int WitnessScore { get; internal set; }
        public double LastHintAtSeconds { get; internal set; } = double.NegativeInfinity;
        public HashSet<string> CollectedEchoIds { get; } = new HashSet<string>();

        public Player(Guid id, MatchConfig config)
        {
            Id = id;
            Vitality = config.StartingVitality;
            BaseDecayPerSecond = config.BaseDecayPerSecond;
        }

        /// <summary>Total decay per second: base rate plus active debuff bonuses.</summary>
        public double TotalDecayPerSecond
        {
            get
            {
                double total = BaseDecayPerSecond;
                foreach (Debuff debuff in Debuffs)
                {
                    total += debuff.DecayBonusPerSecond;
                }
                return total;
            }
        }

        /// <summary>Last active transferable debuff, or null. This is the transfer candidate (RULE-02).</summary>
        public Debuff? FindLastTransferableDebuff()
        {
            for (int i = Debuffs.Count - 1; i >= 0; i--)
            {
                if (Debuffs[i].Transferable)
                {
                    return Debuffs[i];
                }
            }
            return null;
        }

        public bool HasDebuffCapacity(MatchConfig config)
        {
            return Debuffs.Count < config.MaxActiveDebuffs;
        }
    }
}
