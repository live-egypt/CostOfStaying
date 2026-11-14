namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Validates typed command payloads before queue admission and again after dequeue
    /// before returning the command to an application handler.
    /// </summary>
    public interface IMatchCommandPayloadValidationService<TCommand>
    {
        MatchId MatchId { get; }
        Result TryAdmit(CommandEnvelope<TCommand> envelope);
        Result<AuthenticatedCommand<TCommand>> TryTakeNext();
    }
}
