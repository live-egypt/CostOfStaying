namespace CostOfStaying.Core.Services
{
    using System;
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Prevents session cleanup while a transport connection is still bound.
    /// The same connection registry used by ingress must be supplied here.
    /// All operations are single-writer/server-serialized.
    /// </summary>
    public sealed class ConnectionAwareSessionCleanupService<TCommand> : ISessionCleanupService<TCommand>
    {
        private readonly ISessionCleanupService<TCommand> _inner;
        private readonly IConnectionSessionRegistry _connections;

        internal ConnectionAwareSessionCleanupService(
            ISessionCleanupService<TCommand> inner,
            IConnectionSessionRegistry connections)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public Result TryRemoveClosedSession(SessionId sessionId)
        {
            if (!sessionId.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            if (_connections.IsSessionBound(sessionId))
                return Result.Failure(CoreErrorCode.InvalidState);

            return _inner.TryRemoveClosedSession(sessionId);
        }
    }
}
