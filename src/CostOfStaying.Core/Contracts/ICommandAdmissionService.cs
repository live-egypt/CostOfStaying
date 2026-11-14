namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>Admits server-enveloped commands only for currently authenticated sessions.</summary>
    public interface ICommandAdmissionService<TCommand>
    {
        Result TryAdmit(CommandEnvelope<TCommand> envelope);
        Result<AuthenticatedCommand<TCommand>> TryTakeNext();

        /// <summary>
        /// Completes one command previously returned by TryTakeNext.
        /// The application must call this exactly once after processing finishes,
        /// including rejection or exception paths.
        /// </summary>
        Result Complete(AuthenticatedCommand<TCommand> command);
    }
}