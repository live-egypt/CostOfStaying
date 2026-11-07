namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying.Core.Domain;

    /// <summary>Server-owned match phase state machine.</summary>
    public interface IMatchLifecycle
    {
        MatchId MatchId { get; }
        MatchPhase Phase { get; }
        CostOfStaying.Result TryTransition(MatchPhase nextPhase);
    }
}
