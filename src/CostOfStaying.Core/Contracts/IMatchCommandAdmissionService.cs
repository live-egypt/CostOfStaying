namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Match-scoped command admission. An authenticated session may submit commands
    /// only when its server-resolved ActorId is a member of this match's player registry.
    /// Membership is checked both before queue admission and immediately before handler handoff.
    /// </summary>
    public interface IMatchCommandAdmissionService<TCommand>
    {
        MatchId MatchId { get; }
        Result TryAdmit(CommandEnvelope<TCommand> envelope);
        Result<AuthenticatedCommand<TCommand>> TryTakeNext();
    }
}
