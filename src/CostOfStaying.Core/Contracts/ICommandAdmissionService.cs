namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>Admits server-enveloped commands only for currently authenticated sessions.</summary>
    public interface ICommandAdmissionService<TCommand>
    {
        Result TryAdmit(CommandEnvelope<TCommand> envelope);
        Result<AuthenticatedCommand<TCommand>> TryTakeNext();
    }
}
