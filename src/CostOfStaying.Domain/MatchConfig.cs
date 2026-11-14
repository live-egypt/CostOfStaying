namespace CostOfStaying.Domain
{
    /// <summary>
    /// All tunable match parameters in one place.
    ///
    /// Values marked RULE are named in 02_GAME_RULES_SPECIFICATION.md and are
    /// part of the PROPOSED FOR PLAYTEST baseline (ADR-004). They are not
    /// final balance decisions.
    ///
    /// Values marked PROPOSED TEST VALUE are initial guesses required to make
    /// the domain testable. The rules spec explicitly leaves decay rates,
    /// cooldowns and non-3-alive collapse intervals TBD pending playtest.
    /// Do not treat them as approved balance.
    /// </summary>
    public sealed class MatchConfig
    {
        // ---- RULE values (PROPOSED FOR PLAYTEST baseline) ----

        /// <summary>RULE-01: Vitality starts at 100.</summary>
        public double StartingVitality { get; set; } = 100.0;

        /// <summary>RULE-03: Collective Surrender vote lasts 20 seconds.</summary>
        public double VoteDurationSeconds { get; set; } = 20.0;

        /// <summary>RULE-03: collapse interval at three alive, initial value 15s.</summary>
        public double CollapseIntervalThreeAliveSeconds { get; set; } = 15.0;

        /// <summary>RULE-05: match time limit, initial value 20 minutes.</summary>
        public double MatchTimeLimitSeconds { get; set; } = 20.0 * 60.0;

        /// <summary>RULE-02: transfer range, initial test value 2m.</summary>
        public double TransferRangeMeters { get; set; } = 2.0;

        // ---- PROPOSED TEST VALUES (TBD by playtest; not approved balance) ----

        /// <summary>Base vitality decay per second before any collapse. TBD.</summary>
        public double BaseDecayPerSecond { get; set; } = 0.10;

        /// <summary>Decay increase applied by each collapse event. TBD.</summary>
        public double CollapseDecayIncreasePerSecond { get; set; } = 0.02;

        /// <summary>Collapse interval when alive count is not three. TBD (not part of RULE-03).</summary>
        public double CollapseIntervalSeconds { get; set; } = 20.0;

        /// <summary>Duration of a collapse-applied debuff. TBD.</summary>
        public double DebuffDurationSeconds { get; set; } = 30.0;

        /// <summary>Extra decay per second contributed by one active debuff. TBD.</summary>
        public double DebuffDecayBonusPerSecond { get; set; } = 0.03;

        /// <summary>Source cooldown after a successful transfer. TBD.</summary>
        public double TransferCooldownSeconds { get; set; } = 5.0;

        /// <summary>Maximum active debuffs per player. Proposed initial value (COLLAPSE_SYSTEM).</summary>
        public int MaxActiveDebuffs { get; set; } = 3;

        /// <summary>Spirit hint cooldown. TBD (RULE-04 leaves frequency to testing).</summary>
        public double HintCooldownSeconds { get; set; } = 15.0;

        /// <summary>Number of players in an MVP match (charter scope).</summary>
        public int PlayerCount { get; set; } = 4;
    }
}
