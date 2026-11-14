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

        public SessionRegistry CreateSessionRegistry(int capacity)
        {
            return new SessionRegistry(capacity);
        }

        public IConnectionSessionRegistry CreateConnectionSessionRegistry(ISessionRegistry sessions, int capacity)
        {
            return new ConnectionSessionRegistry(sessions, capacity);
        }

        public IMatchPlayerRegistry CreateMatchPlayerRegistry(MatchId matchId, int capacity)
        {
            return new MatchPlayerRegistry(matchId, capacity);
        }

        public IMatchRosterSnapshotBuilder CreateMatchRosterSnapshotBuilder(
            IMatchLifecycle match,
            IMatchPlayerRegistry players)
        {
            return new MatchRosterSnapshotBuilder(match, players);
        }

        public ICommandAdmissionService<TCommand> CreateCommandAdmissionService<TCommand>(
            ISessionRegistry sessions,
            CommandAdmissionQueue<TCommand> queue)
        {
            return new CommandAdmissionService<TCommand>(sessions, queue);
        }

        public IMatchCommandAdmissionService<TCommand> CreateMatchCommandAdmissionService<TCommand>(
            ISessionRegistry sessions,
            IMatchPlayerRegistry players,
            CommandAdmissionQueue<TCommand> queue)
        {
            ICommandAdmissionService<TCommand> inner = new CommandAdmissionService<TCommand>(sessions, queue);
            return new MatchCommandAdmissionService<TCommand>(sessions, players, inner);
        }

        public IMatchCommandAuthorizationService<TCommand> CreateMatchCommandAuthorizationService<TCommand>(
            ISessionRegistry sessions,
            IMatchLifecycle match,
            IMatchPlayerRegistry players,
            CommandAdmissionQueue<TCommand> queue,
            IMatchCommandAuthorizationPolicy<TCommand> policy)
        {
            IMatchCommandAdmissionService<TCommand> admission =
                CreateMatchCommandAdmissionService(sessions, players, queue);
            return new MatchCommandAuthorizationService<TCommand>(
                sessions,
                match,
                players,
                admission,
                policy);
        }

        public IMatchCommandPayloadValidationService<TCommand> CreateMatchCommandPayloadValidationService<TCommand>(
            IMatchCommandAuthorizationService<TCommand> authorization,
            ICommandPayloadValidator<TCommand> validator)
        {
            return new MatchCommandPayloadValidationService<TCommand>(authorization, validator);
        }

        public ISessionCleanupService<TCommand> CreateSessionCleanupService<TCommand>(
            SessionRegistry sessions,
            CommandAdmissionQueue<TCommand> queue,
            IConnectionSessionRegistry connections)
        {
            if (!(connections is ConnectionSessionRegistry connectionRegistry) ||
                !connectionRegistry.IsBackedBy(sessions))
            {
                throw new ArgumentException(
                    "Connection registry must use the same SessionRegistry instance as cleanup.",
                    nameof(connections));
            }

            ISessionCleanupService<TCommand> inner = new SessionCleanupService<TCommand>(sessions, queue);
            return new ConnectionAwareSessionCleanupService<TCommand>(inner, connections);
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
