namespace CostOfStaying.Core.Services
{
    using System;
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// First server-side admission gate: validates envelope shape and authenticated session
    /// before sequence/replay admission into the bounded FIFO. Gameplay/phase/actor-specific
    /// validation remains in the command handler after dequeue.
    /// Single-writer/server-serialized.
    /// </summary>
    public sealed class CommandAdmissionService<TCommand> : ICommandAdmissionService<TCommand>
    {
        private readonly ISessionRegistry _sessions;
        private readonly CommandAdmissionQueue<TCommand> _queue;

        internal CommandAdmissionService(
            ISessionRegistry sessions,
            CommandAdmissionQueue<TCommand> queue)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
        }

        public Result TryAdmit(CommandEnvelope<TCommand> envelope)
        {
            if (!envelope.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            Result<ISessionLifecycle> lookup = _sessions.Get(envelope.SessionId);
            if (lookup.IsFailure)
                return Result.Failure(lookup.ErrorCode);

            if (!IsAuthenticatedBinding(lookup.Value, envelope.SessionId))
                return Result.Failure(CoreErrorCode.Unauthorized);

            return _queue.TryEnqueue(envelope);
        }

        /// <summary>
        /// Dequeues the next command and revalidates its session immediately before handing
        /// the command and server-resolved ActorId to the application handler. Stale commands
        /// are discarded and return Unauthorized; their sequence remains consumed.
        /// </summary>
        public Result<AuthenticatedCommand<TCommand>> TryTakeNext()
        {
            if (!_queue.TryDequeue(out CommandEnvelope<TCommand> envelope))
                return Result<AuthenticatedCommand<TCommand>>.Failure(CoreErrorCode.Unavailable);

            Result<ISessionLifecycle> lookup = _sessions.Get(envelope.SessionId);
            if (lookup.IsFailure)
                return Result<AuthenticatedCommand<TCommand>>.Failure(lookup.ErrorCode);

            if (!IsAuthenticatedBinding(lookup.Value, envelope.SessionId))
                return Result<AuthenticatedCommand<TCommand>>.Failure(CoreErrorCode.Unauthorized);

            return Result<AuthenticatedCommand<TCommand>>.Success(
                new AuthenticatedCommand<TCommand>(lookup.Value.ActorId!.Value, envelope));
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
