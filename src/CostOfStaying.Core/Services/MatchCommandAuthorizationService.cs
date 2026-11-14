namespace CostOfStaying.Core.Services
{
    using System;
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Applies an application-supplied authorization policy using server-resolved session,
    /// actor, match phase and player lifecycle facts. The policy is evaluated before queue
    /// admission and again after dequeue before returning a command to the gameplay handler.
    ///
    /// This service provides no default gameplay policy. Missing policy is a construction error.
    /// Policy implementations must be pure/side-effect-free and must not perform I/O.
    /// All operations are single-writer/server-serialized; this type is not thread-safe.
    /// </summary>
    public sealed class MatchCommandAuthorizationService<TCommand> : IMatchCommandAuthorizationService<TCommand>
    {
        private readonly ISessionRegistry _sessions;
        private readonly IMatchLifecycle _match;
        private readonly IMatchPlayerRegistry _players;
        private readonly IMatchCommandAdmissionService<TCommand> _admission;
        private readonly IMatchCommandAuthorizationPolicy<TCommand> _policy;

        internal MatchCommandAuthorizationService(
            ISessionRegistry sessions,
            IMatchLifecycle match,
            IMatchPlayerRegistry players,
            IMatchCommandAdmissionService<TCommand> admission,
            IMatchCommandAuthorizationPolicy<TCommand> policy)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
            _match = match ?? throw new ArgumentNullException(nameof(match));
            _players = players ?? throw new ArgumentNullException(nameof(players));
            _admission = admission ?? throw new ArgumentNullException(nameof(admission));
            _policy = policy ?? throw new ArgumentNullException(nameof(policy));

            if (!_match.MatchId.IsValid ||
                _players.MatchId != _match.MatchId ||
                _admission.MatchId != _match.MatchId)
            {
                throw new ArgumentException("Match lifecycle, player roster and admission gate must use the same valid MatchId.");
            }

            MatchId = _match.MatchId;
        }

        public MatchId MatchId { get; }

        /// <summary>
        /// Evaluates trusted identity, current phase and current player lifecycle before
        /// queue admission. Rejected policy decisions do not consume sequence or queue capacity.
        /// </summary>
        public Result TryAdmit(CommandEnvelope<TCommand> envelope)
        {
            if (!envelope.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            Result<MatchCommandAuthorizationContext<TCommand>> context =
                ResolveContext(envelope.SessionId, expectedActorId: null, envelope.ReceivedAt, envelope.Command);

            if (context.IsFailure)
                return Result.Failure(context.ErrorCode);

            Result authorization = EvaluatePolicy(context.Value);
            if (authorization.IsFailure)
                return authorization;

            return _admission.TryAdmit(envelope);
        }

        /// <summary>
        /// Re-evaluates the policy with current phase and player lifecycle after the
        /// authenticated membership gate dequeues the command. A stale command is discarded
        /// rather than handed to gameplay; its queue sequence remains consumed.
        /// </summary>
        public Result<AuthenticatedCommand<TCommand>> TryTakeNext()
        {
            Result<AuthenticatedCommand<TCommand>> admitted = _admission.TryTakeNext();
            if (admitted.IsFailure)
                return admitted;

            Result<MatchCommandAuthorizationContext<TCommand>> context =
                ResolveContext(
                    admitted.Value.SessionId,
                    admitted.Value.ActorId,
                    admitted.Value.ReceivedAt,
                    admitted.Value.Command);

            if (context.IsFailure)
                return Result<AuthenticatedCommand<TCommand>>.Failure(context.ErrorCode);

            Result authorization = EvaluatePolicy(context.Value);
            if (authorization.IsFailure)
                return Result<AuthenticatedCommand<TCommand>>.Failure(authorization.ErrorCode);

            return admitted;
        }

        private Result<MatchCommandAuthorizationContext<TCommand>> ResolveContext(
            SessionId sessionId,
            ActorId? expectedActorId,
            ServerTick receivedAt,
            TCommand command)
        {
            Result<ISessionLifecycle> sessionLookup = _sessions.Get(sessionId);
            if (sessionLookup.IsFailure)
                return Result<MatchCommandAuthorizationContext<TCommand>>.Failure(sessionLookup.ErrorCode);

            ISessionLifecycle session = sessionLookup.Value;
            if (session.SessionId != sessionId ||
                session.State != SessionState.Authenticated ||
                !session.ActorId.HasValue ||
                !session.ActorId.Value.IsValid ||
                (expectedActorId.HasValue && session.ActorId.Value != expectedActorId.Value))
            {
                return Result<MatchCommandAuthorizationContext<TCommand>>.Failure(CoreErrorCode.Unauthorized);
            }

            Result<IVitalityAccount> playerLookup = _players.Get(session.ActorId.Value);
            if (playerLookup.IsFailure)
            {
                return Result<MatchCommandAuthorizationContext<TCommand>>.Failure(
                    playerLookup.ErrorCode == CoreErrorCode.NotFound ||
                    playerLookup.ErrorCode == CoreErrorCode.InvalidArgument
                        ? CoreErrorCode.Unauthorized
                        : playerLookup.ErrorCode);
            }

            IVitalityAccount player = playerLookup.Value;
            if (player is null || player.PlayerId != session.ActorId.Value)
                return Result<MatchCommandAuthorizationContext<TCommand>>.Failure(CoreErrorCode.InvalidState);

            try
            {
                return Result<MatchCommandAuthorizationContext<TCommand>>.Success(
                    new MatchCommandAuthorizationContext<TCommand>(
                        sessionId,
                        session.ActorId.Value,
                        MatchId,
                        _match.Phase,
                        player.State,
                        receivedAt,
                        command));
            }
            catch (ArgumentException)
            {
                return Result<MatchCommandAuthorizationContext<TCommand>>.Failure(CoreErrorCode.InvalidState);
            }
        }

        private Result EvaluatePolicy(MatchCommandAuthorizationContext<TCommand> context)
        {
            Result result;
            try
            {
                result = _policy.Authorize(context);
            }
            catch (Exception)
            {
                return Result.Failure(CoreErrorCode.Unavailable);
            }

            if (result.IsSuccess)
                return Result.Success();

            return result.ErrorCode == CoreErrorCode.Uninitialized
                ? Result.Failure(CoreErrorCode.InvalidState)
                : result;
        }
    }
}
