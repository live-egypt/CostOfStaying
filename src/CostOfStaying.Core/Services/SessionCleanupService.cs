namespace CostOfStaying.Core.Services
{
    using System;
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Removes a closed session only after its queued commands have drained and its
    /// sequence/replay state has been released. The caller must invoke this after any
    /// dequeued command for the session has finished processing. Single-writer/server-serialized.
    /// </summary>
    public sealed class SessionCleanupService<TCommand> : ISessionCleanupService<TCommand>
    {
        private readonly ISessionRegistry _sessions;
        private readonly CommandAdmissionQueue<TCommand> _queue;

        public SessionCleanupService(ISessionRegistry sessions, CommandAdmissionQueue<TCommand> queue)
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
    }
}
