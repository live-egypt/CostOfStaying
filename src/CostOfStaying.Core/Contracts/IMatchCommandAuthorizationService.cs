namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Final pure-Core authorization gate after session and match membership resolution.
    /// Evaluates an application-supplied policy both before queue admission and before handler handoff.
    /// </summary>
    public interface IMatchCommandAuthorizationService<TCommand>
    {
        MatchId MatchId { get; }
        Result TryAdmit(CommandEnvelope<TCommand> envelope);
        Result<AuthenticatedCommand<TCommand>> TryTakeNext();
    }
}
