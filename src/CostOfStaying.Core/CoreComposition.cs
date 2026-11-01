namespace CostOfStaying.Core
{
    using System;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;
    using CostOfStaying.Core.Services;

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

        public ISessionLifecycle CreateSessionLifecycle(SessionId sessionId)
        {
            return new SessionLifecycle(sessionId);
        }

        public ISessionRegistry CreateSessionRegistry(int capacity)
        {
            return new SessionRegistry(capacity);
        }

        public IMatchPlayerRegistry CreateMatchPlayerRegistry(MatchId matchId, int capacity)
        {
            return new MatchPlayerRegistry(matchId, capacity);
        }

        public ICommandAdmissionService<TCommand> CreateCommandAdmissionService<TCommand>(
            ISessionRegistry sessions,
            CommandAdmissionQueue<TCommand> queue)
        {
            return new CommandAdmissionService<TCommand>(sessions, queue);
        }

        public ISessionCleanupService<TCommand> CreateSessionCleanupService<TCommand>(
            ISessionRegistry sessions,
            CommandAdmissionQueue<TCommand> queue)
        {
            return new SessionCleanupService<TCommand>(sessions, queue);
        }

        public CommandSequenceTracker CreateCommandSequenceTracker(ulong maximumSequenceJump, int maximumTrackedSessions)
        {
            return new CommandSequenceTracker(maximumSequenceJump, maximumTrackedSessions);
        }

        public CommandAdmissionQueue<TCommand> CreateCommandAdmissionQueue<TCommand>(
            ulong maximumSequenceJump,
            int maximumTrackedSessions,
            int capacity)
        {
            return new CommandAdmissionQueue<TCommand>(maximumSequenceJump, maximumTrackedSessions, capacity);
        }
    }
}
