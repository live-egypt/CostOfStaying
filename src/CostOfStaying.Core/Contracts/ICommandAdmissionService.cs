namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>Admits server-enveloped commands only for currently authenticated sessions.</summary>
    public interface ICommandAdmissionService<TCommand>
    {
        Result TryAdmit(CommandEnvelope<TCommand> envelope);

        /// <summary>Removes a Closed session only after its pending commands have drained and replay state is reset.</summary>
        Result TryRemoveClosedSession(SessionId sessionId);
    }
}
