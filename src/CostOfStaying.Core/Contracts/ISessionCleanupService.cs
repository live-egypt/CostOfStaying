namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>Coordinates safe removal of closed sessions and their command replay state.</summary>
    public interface ISessionCleanupService<TCommand>
    {
        Result TryRemoveClosedSession(SessionId sessionId);
    }
}
