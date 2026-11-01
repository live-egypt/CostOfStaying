namespace CostOfStaying.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;

    /// <summary>
    /// Bounded, deterministic registry keyed only by server-issued SessionId.
    /// It does not impose per-actor multi-session policy or own connection mapping.
    /// A session must be Closed before removal; the caller must first drain queued commands and reset replay state. Single-writer/server-serialized.
    /// </summary>
    public sealed class SessionRegistry : ISessionRegistry
    {
        private readonly Dictionary<SessionId, ISessionLifecycle> _sessions =
            new Dictionary<SessionId, ISessionLifecycle>();
        private readonly List<SessionId> _orderedIds = new List<SessionId>();

        internal SessionRegistry(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Session capacity must be positive.");

            Capacity = capacity;
        }

        public int Capacity { get; }
        public int Count => _sessions.Count;

        public Result Register(ISessionLifecycle session)
        {
            if (session is null || !session.SessionId.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            if (session.State != SessionState.Pending)
                return Result.Failure(CoreErrorCode.InvalidState);

            if (_sessions.ContainsKey(session.SessionId))
                return Result.Failure(CoreErrorCode.Conflict);

            if (_sessions.Count >= Capacity)
                return Result.Failure(CoreErrorCode.CapacityExceeded);

            _sessions.Add(session.SessionId, session);
            _orderedIds.Add(session.SessionId);
            return Result.Success();
        }

        public Result<ISessionLifecycle> Get(SessionId sessionId)
        {
            if (!sessionId.IsValid)
                return Result<ISessionLifecycle>.Failure(CoreErrorCode.InvalidArgument);

            return _sessions.TryGetValue(sessionId, out ISessionLifecycle? session)
                ? Result<ISessionLifecycle>.Success(session!)
                : Result<ISessionLifecycle>.Failure(CoreErrorCode.NotFound);
        }

        internal Result RemoveClosedSession(SessionId sessionId)
        {
            if (!sessionId.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            if (!_sessions.TryGetValue(sessionId, out ISessionLifecycle? session))
                return Result.Failure(CoreErrorCode.NotFound);

            if (session.State != SessionState.Closed)
                return Result.Failure(CoreErrorCode.InvalidState);

            _sessions.Remove(sessionId);
            _orderedIds.Remove(sessionId);
            return Result.Success();
        }

        public IReadOnlyList<SessionId> GetSessionIdsSnapshot()
        {
            return Array.AsReadOnly(_orderedIds.ToArray());
        }
    }
}
