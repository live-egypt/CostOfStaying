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

        public CommandAdmissionService(
            ISessionRegistry sessions,
            CommandAdmissionQueue<TCommand> queue)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
        }

        public Result TryRemoveClosedSession(SessionId sessionId)
        {
            if (!sessionId.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            Result<ISessionLifecycle> lookup = _sessions.Get(sessionId);
            if (lookup.IsFailure)
                return Result.Failure(lookup.ErrorCode);

            if (lookup.Value.State != SessionState.Closed)
                return Result.Failure(CoreErrorCode.InvalidState);

            Result reset = _queue.ResetSession(sessionId);
            if (reset.IsFailure && reset.ErrorCode != CoreErrorCode.NotFound)
                return reset;

            return _sessions.Remove(sessionId);
        }

        public Result TryAdmit(CommandEnvelope<TCommand> envelope)
        {
            if (!envelope.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            Result<ISessionLifecycle> lookup = _sessions.Get(envelope.SessionId);
            if (lookup.IsFailure)
                return Result.Failure(lookup.ErrorCode);

            if (lookup.Value.State != SessionState.Authenticated ||
                !lookup.Value.ActorId.HasValue ||
                !lookup.Value.ActorId.Value.IsValid)
            {
                return Result.Failure(CoreErrorCode.Unauthorized);
            }

            return _queue.TryEnqueue(envelope);
        }
    }
}
