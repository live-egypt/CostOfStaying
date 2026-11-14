namespace CostOfStaying.Core.Services
{
    using System;
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Adds match-roster membership validation to the authenticated-session admission gate.
    /// The wrapped admission service must be composed from the same SessionRegistry instance.
    /// One instance is created per match-scoped Core composition.
    /// Single-writer/server-serialized; not thread-safe.
    ///
    /// This service checks membership only. Match phase, player lifecycle permissions,
    /// command payload rules and account-level multi-session policy remain application decisions.
    /// </summary>
    public sealed class MatchCommandAdmissionService<TCommand> : IMatchCommandAdmissionService<TCommand>
    {
        private readonly ISessionRegistry _sessions;
        private readonly IMatchPlayerRegistry _players;
        private readonly ICommandAdmissionService<TCommand> _inner;

        internal MatchCommandAdmissionService(
            ISessionRegistry sessions,
            IMatchPlayerRegistry players,
            ICommandAdmissionService<TCommand> inner)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
            _players = players ?? throw new ArgumentNullException(nameof(players));
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));

            if (!_players.MatchId.IsValid)
                throw new ArgumentException("Match player registry must have a valid MatchId.", nameof(players));

            MatchId = _players.MatchId;
        }

        public MatchId MatchId { get; }

        /// <summary>
        /// Rejects authenticated sessions whose server-resolved ActorId is not a member
        /// of this match before consuming sequence/replay capacity or queue space.
        /// </summary>
        public Result TryAdmit(CommandEnvelope<TCommand> envelope)
        {
            if (!envelope.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            Result<ISessionLifecycle> lookup = _sessions.Get(envelope.SessionId);
            if (lookup.IsFailure)
                return Result.Failure(lookup.ErrorCode);

            if (!IsAuthenticatedBinding(lookup.Value, envelope.SessionId))
                return Result.Failure(CoreErrorCode.Unauthorized);

            Result membership = ValidateMembership(lookup.Value.ActorId!.Value);
            if (membership.IsFailure)
                return membership;

            return _inner.TryAdmit(envelope);
        }

        /// <summary>
        /// Rechecks match membership after dequeue. A player removed from the match
        /// after admission cannot have the pending command handed to a gameplay handler.
        /// The dequeued command's sequence remains consumed.
        /// </summary>
        public Result<AuthenticatedCommand<TCommand>> TryTakeNext()
        {
            Result<AuthenticatedCommand<TCommand>> admitted = _inner.TryTakeNext();
            if (admitted.IsFailure)
                return admitted;

            Result membership = ValidateMembership(admitted.Value.ActorId);
            if (membership.IsFailure)
                return Result<AuthenticatedCommand<TCommand>>.Failure(membership.ErrorCode);

            return admitted;
        }

        private Result ValidateMembership(ActorId actorId)
        {
            if (!actorId.IsValid)
                return Result.Failure(CoreErrorCode.Unauthorized);

            Result<IVitalityAccount> player = _players.Get(actorId);
            if (player.IsFailure)
            {
                return player.ErrorCode == CoreErrorCode.NotFound ||
                    player.ErrorCode == CoreErrorCode.InvalidArgument
                    ? Result.Failure(CoreErrorCode.Unauthorized)
                    : Result.Failure(player.ErrorCode);
            }

            if (player.Value is null || player.Value.PlayerId != actorId)
                return Result.Failure(CoreErrorCode.InvalidState);

            return Result.Success();
        }

        private static bool IsAuthenticatedBinding(ISessionLifecycle session, SessionId expectedSessionId)
        {
            return session.SessionId == expectedSessionId &&
                session.State == SessionState.Authenticated &&
                session.ActorId.HasValue &&
                session.ActorId.Value.IsValid;
        }
    }
}
