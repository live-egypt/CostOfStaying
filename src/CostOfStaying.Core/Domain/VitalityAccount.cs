namespace CostOfStaying.Core.Domain
{
    using System;
    using CostOfStaying.Core.Contracts;

    /// <summary>Vitality lifecycle only. Spirit is a separate post-death role/projection.</summary>
    public enum PlayerVitalityState
    {
        Alive = 0,
        Dead = 1
    }

    /// <summary>Deterministic, engine-independent vitality account with terminal death.</summary>
    public sealed class VitalityAccount : IVitalityAccount
    {
        public const uint DefaultMaximum = 100;

        public VitalityAccount(ActorId playerId, uint maximum = DefaultMaximum)
        {
            if (!playerId.IsValid)
                throw new ArgumentException("Player identity must be non-zero.", nameof(playerId));

            if (maximum == 0)
                throw new ArgumentOutOfRangeException(nameof(maximum), "Maximum vitality must be positive.");

            PlayerId = playerId;
            Maximum = maximum;
            Current = maximum;
            State = PlayerVitalityState.Alive;
        }

        public ActorId PlayerId { get; }
        public PlayerVitalityState State { get; private set; }
        public uint Current { get; private set; }
        public uint Maximum { get; }

        public void Lose(uint amount)
        {
            if (State != PlayerVitalityState.Alive || amount == 0)
                return;

            Current = amount >= Current ? 0 : Current - amount;
            if (Current == 0)
                State = PlayerVitalityState.Dead;
        }

        public void Restore(uint amount)
        {
            if (State != PlayerVitalityState.Alive || amount == 0)
                return;

            ulong restored = (ulong)Current + amount;
            Current = restored >= Maximum ? Maximum : (uint)restored;
        }
    }
}
