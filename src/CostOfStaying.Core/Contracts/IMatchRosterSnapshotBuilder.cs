namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>Builds immutable, server-side snapshots of match phase and player lifecycle/vitality.</summary>
    public interface IMatchRosterSnapshotBuilder
    {
        Result<MatchRosterSnapshot> Build(ServerTick serverTick, StateRevision revision);
    }
}
