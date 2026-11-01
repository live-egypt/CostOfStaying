namespace CostOfStaying.Core.Contracts
{
    using System.Collections.Generic;
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>Bounded in-memory registry of server-issued session lifecycles.</summary>
    public interface ISessionRegistry
    {
        int Capacity { get; }
        int Count { get; }
        Result Register(ISessionLifecycle session);
        Result<ISessionLifecycle> Get(SessionId sessionId);
        IReadOnlyList<SessionId> GetSessionIdsSnapshot();
    }
}
