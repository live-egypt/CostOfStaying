namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>Server-owned lifecycle for one authenticated transport session.</summary>
    public interface ISessionLifecycle
    {
        SessionId SessionId { get; }
        SessionState State { get; }
        ActorId? ActorId { get; }
        Result TryAuthenticate(ActorId actorId);
        Result TryDisconnect();
        Result TryClose();
    }
}
