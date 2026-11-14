namespace CostOfStaying.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;

    /// <summary>
    /// Bounded, bidirectional mapping between a server-issued transport connection ID
    /// and a registered Pending/Authenticated session. It stores no transport objects,
    /// sockets, peer addresses, credentials or client-provided identity.
    ///
    /// A connection ID must be allocated by the trusted transport adapter and must not be
    /// reused during one server-process lifetime. One connection maps to one session and
    /// one session maps to one connection. Different sessions may map to the same ActorId;
    /// per-actor multi-session policy is intentionally not enforced here.
    ///
    /// All mutations are single-writer/server-serialized; this type is not thread-safe.
    /// </summary>
    public sealed class ConnectionSessionRegistry : IConnectionSessionRegistry
    {
        private readonly ISessionRegistry _sessions;
        private readonly Dictionary<TransportConnectionId, SessionId> _sessionByConnection =
            new Dictionary<TransportConnectionId, SessionId>();
        private readonly Dictionary<SessionId, TransportConnectionId> _connectionBySession =
            new Dictionary<SessionId, TransportConnectionId>();

        internal ConnectionSessionRegistry(ISessionRegistry sessions, int capacity)
        {
            _sessions = sessions ?? throw new ArgumentNullException(nameof(sessions));
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Connection capacity must be positive.");

            Capacity = capacity;
        }

        public int Capacity { get; }
        public int Count => _sessionByConnection.Count;

        /// <summary>
        /// Binds a fresh server-issued connection ID to a registered Pending session.
        /// Binding happens before authentication so the trusted ingress can resolve the
        /// session while validating credentials. It never authenticates the session itself.
        /// </summary>
        public Result TryBind(TransportConnectionId connectionId, SessionId sessionId)
        {
            if (!connectionId.IsValid || !sessionId.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            Result<ISessionLifecycle> lookup = _sessions.Get(sessionId);
            if (lookup.IsFailure)
                return Result.Failure(lookup.ErrorCode);

            if (lookup.Value.State != SessionState.Pending)
                return Result.Failure(CoreErrorCode.InvalidState);

            if (_sessionByConnection.ContainsKey(connectionId) ||
                _connectionBySession.ContainsKey(sessionId))
            {
                return Result.Failure(CoreErrorCode.Conflict);
            }

            if (Count >= Capacity)
                return Result.Failure(CoreErrorCode.CapacityExceeded);

            _sessionByConnection.Add(connectionId, sessionId);
            _connectionBySession.Add(sessionId, connectionId);
            return Result.Success();
        }

        /// <summary>
        /// Resolves only a currently Pending or Authenticated session. A disconnected or
        /// closed session is never returned as a usable connection binding.
        /// </summary>
        public Result<SessionId> GetSessionId(TransportConnectionId connectionId)
        {
            if (!connectionId.IsValid)
                return Result<SessionId>.Failure(CoreErrorCode.InvalidArgument);

            if (!_sessionByConnection.TryGetValue(connectionId, out SessionId sessionId))
                return Result<SessionId>.Failure(CoreErrorCode.NotFound);

            Result<ISessionLifecycle> lookup = _sessions.Get(sessionId);
            if (lookup.IsFailure)
                return Result<SessionId>.Failure(lookup.ErrorCode);

            if (!IsLiveBindingState(lookup.Value.State))
                return Result<SessionId>.Failure(CoreErrorCode.Unauthorized);

            return Result<SessionId>.Success(sessionId);
        }

        /// <summary>Resolves the active connection for a currently Pending/Authenticated session.</summary>
        public Result<TransportConnectionId> GetConnectionId(SessionId sessionId)
        {
            if (!sessionId.IsValid)
                return Result<TransportConnectionId>.Failure(CoreErrorCode.InvalidArgument);

            if (!_connectionBySession.TryGetValue(sessionId, out TransportConnectionId connectionId))
                return Result<TransportConnectionId>.Failure(CoreErrorCode.NotFound);

            Result<ISessionLifecycle> lookup = _sessions.Get(sessionId);
            if (lookup.IsFailure)
                return Result<TransportConnectionId>.Failure(lookup.ErrorCode);

            if (!IsLiveBindingState(lookup.Value.State))
                return Result<TransportConnectionId>.Failure(CoreErrorCode.Unauthorized);

            return Result<TransportConnectionId>.Success(connectionId);
        }

        /// <summary>
        /// Performs the lifecycle disconnect transition before removing the mapping.
        /// This makes queued commands fail the existing dequeue-time session revalidation.
        /// Pending connections are closed; authenticated ones become Disconnected.
        /// </summary>
        public Result<SessionId> TryDetach(TransportConnectionId connectionId)
        {
            if (!connectionId.IsValid)
                return Result<SessionId>.Failure(CoreErrorCode.InvalidArgument);

            if (!_sessionByConnection.TryGetValue(connectionId, out SessionId sessionId))
                return Result<SessionId>.Failure(CoreErrorCode.NotFound);

            Result<ISessionLifecycle> lookup = _sessions.Get(sessionId);
            if (lookup.IsFailure)
                return Result<SessionId>.Failure(lookup.ErrorCode);

            Result transition;
            switch (lookup.Value.State)
            {
                case SessionState.Pending:
                    transition = lookup.Value.TryClose();
                    break;
                case SessionState.Authenticated:
                    transition = lookup.Value.TryDisconnect();
                    break;
                case SessionState.Disconnected:
                case SessionState.Closed:
                    transition = Result.Success();
                    break;
                default:
                    return Result<SessionId>.Failure(CoreErrorCode.InvalidState);
            }

            if (transition.IsFailure)
                return Result<SessionId>.Failure(transition.ErrorCode);

            _sessionByConnection.Remove(connectionId);
            _connectionBySession.Remove(sessionId);
            return Result<SessionId>.Success(sessionId);
        }

        public bool IsSessionBound(SessionId sessionId)
        {
            return sessionId.IsValid && _connectionBySession.ContainsKey(sessionId);
        }

        internal bool IsBackedBy(ISessionRegistry sessions)
        {
            return ReferenceEquals(_sessions, sessions);
        }

        private static bool IsLiveBindingState(SessionState state)
        {
            return state == SessionState.Pending || state == SessionState.Authenticated;
        }
    }
}
