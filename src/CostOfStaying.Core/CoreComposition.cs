namespace CostOfStaying.Core
{
    using System;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Composition root for one server-owned Core simulation scope.
    /// Create one instance per match scope; it owns the match-scoped ledger and creates match/player aggregates.
    /// All mutation remains single-writer/server-serialized.
    /// </summary>
    public sealed class CoreComposition
    {
        private readonly IBetrayalLedger _betrayalLedger;

        private CoreComposition()
        {
            _betrayalLedger = new BetrayalLedger();
        }

        public IBetrayalLedger BetrayalLedger => _betrayalLedger;

        public static CoreComposition Create() => new CoreComposition();

        public IVitalityAccount CreateVitalityAccount(ActorId playerId, uint maximum = VitalityAccount.DefaultMaximum)
        {
            return new VitalityAccount(playerId, maximum);
        }

        public IMatchLifecycle CreateMatchLifecycle(MatchId matchId)
        {
            return new MatchLifecycle(matchId);
        }

        public CommandSequenceTracker CreateCommandSequenceTracker(ulong maximumSequenceJump, int maximumTrackedSessions)
        {
            return new CommandSequenceTracker(maximumSequenceJump, maximumTrackedSessions);
        }
    }
}
