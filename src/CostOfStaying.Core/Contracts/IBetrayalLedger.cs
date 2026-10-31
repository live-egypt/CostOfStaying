namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Records server-authored betrayal events in strictly increasing sequence order.
    /// Returns false only for an exact replay of an already-recorded event.
    /// </summary>
    public interface IBetrayalLedger
    {
        ulong ScoreFor(ActorId actorId);
        bool TryRecord(BetrayalEvent betrayalEvent);
    }
}
