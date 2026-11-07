namespace CostOfStaying.Core.Contracts
{
    using System.Collections.Generic;
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>Match-scoped player membership registry; it does not own gameplay authority.</summary>
    public interface IMatchPlayerRegistry
    {
        MatchId MatchId { get; }
        int Capacity { get; }
        int Count { get; }
        Result Register(IVitalityAccount account);
        Result<IVitalityAccount> Get(ActorId actorId);
        Result Remove(ActorId actorId);
        IReadOnlyList<ActorId> GetPlayerIdsSnapshot();
    }
}
