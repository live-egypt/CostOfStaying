namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Resolves a server-issued transport connection identity to one registered session.
    /// A live connection and a live session have a one-to-one mapping; this says nothing
    /// about how many distinct sessions may belong to the same ActorId.
    /// </summary>
    public interface IConnectionSessionRegistry
    {
        int Capacity { get; }
        int Count { get; }

        Result TryBind(TransportConnectionId connectionId, SessionId sessionId);
        Result<SessionId> GetSessionId(TransportConnectionId connectionId);
        Result<TransportConnectionId> GetConnectionId(SessionId sessionId);

        /// <summary>
        /// Detaches a transport connection. Pending sessions are closed; authenticated
        /// sessions become Disconnected. Already terminal/disconnected sessions are detached
        /// without another lifecycle transition.
        /// </summary>
        Result<SessionId> TryDetach(TransportConnectionId connectionId);

        /// <summary>Returns whether any live transport connection remains mapped to the session.</summary>
        bool IsSessionBound(SessionId sessionId);
    }
}
