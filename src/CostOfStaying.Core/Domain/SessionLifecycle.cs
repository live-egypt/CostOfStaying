namespace CostOfStaying.Core.Domain
{
    using System;
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;

    /// <summary>Lifecycle of one server-issued session identity.</summary>
    public enum SessionState : byte
    {
        Pending = 0,
        Authenticated = 1,
        Disconnected = 2,
        Closed = 3
    }

    /// <summary>
    /// Deterministic, in-memory session lifecycle. A disconnected session cannot be
    /// re-authenticated; reconnect policy must create and authorize a new session
    /// in the application layer. No credentials, timers or transport objects are stored here.
    /// Single-writer/server-serialized.
    /// </summary>
    public sealed class SessionLifecycle : ISessionLifecycle
    {
        public SessionLifecycle(SessionId sessionId)
        {
            if (!sessionId.IsValid)
                throw new ArgumentException("Session identity must be non-zero.", nameof(sessionId));

            SessionId = sessionId;
            State = SessionState.Pending;
        }

        public SessionId SessionId { get; }
        public SessionState State { get; private set; }
        public ActorId? ActorId { get; private set; }

        /// <summary>Associates a pending session with one validated actor exactly once.</summary>
        public Result TryAuthenticate(ActorId actorId)
        {
            if (!actorId.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            if (State != SessionState.Pending)
                return Result.Failure(CoreErrorCode.InvalidState);

            ActorId = actorId;
            State = SessionState.Authenticated;
            return Result.Success();
        }

        /// <summary>Marks an authenticated session disconnected; it cannot be reactivated.</summary>
        public Result TryDisconnect()
        {
            if (State != SessionState.Authenticated)
                return Result.Failure(CoreErrorCode.InvalidState);

            State = SessionState.Disconnected;
            return Result.Success();
        }

        /// <summary>Closes a pending, authenticated or disconnected session permanently.</summary>
        public Result TryClose()
        {
            if (State == SessionState.Closed)
                return Result.Failure(CoreErrorCode.InvalidState);

            State = SessionState.Closed;
            return Result.Success();
        }
    }
}
