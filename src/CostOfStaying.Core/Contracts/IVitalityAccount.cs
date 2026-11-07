namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Pure domain contract. Implementations must not read clocks, access Unity,
    /// perform I/O, or infer authority from client-supplied data.
    /// </summary>
    public interface IVitalityAccount
    {
        ActorId PlayerId { get; }
        PlayerLifecycleState State { get; }
        uint Current { get; }
        uint Maximum { get; }

        void Lose(uint amount);
        void Restore(uint amount);
        bool TryResurrect(uint requestedVitality);
        bool TryEliminate();
    }
}
