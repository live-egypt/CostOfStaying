namespace CostOfStaying.Core.Domain
{
    using System;
    using CostOfStaying.Core.Contracts;

    public enum PlayerVitalityState
    {
        Alive = 0,
        Spirit = 1,
        Eliminated = 2
    }

    /// <summary>
    /// Deterministic vitality primitive. This is a domain primitive, not permission
    /// for the MVP application layer to revive a player; MVP rules prohibit revival.
    /// </summary>
    public sealed class VitalityAccount : IVitalityAccount
    {
        public const uint DefaultMaximum = 100;

        public VitalityAccount(ActorId playerId, uint maximum = DefaultMaximum)
        {
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
            if (State != PlayerVitalityState.Alive)
                return;

            if (amount == 0)
                return;

            Current = amount >= Current ? 0 : Current - amount;
            if (Current == 0)
                State = PlayerVitalityState.Spirit;
        }

        public void Restore(uint amount)
        {
            if (State != PlayerVitalityState.Alive || amount == 0)
                return;

            ulong restored = (ulong)Current + amount;
            Current = restored >= Maximum ? Maximum : (uint)restored;
        }

        public void Resurrect(uint vitality)
        {
            EnsureSpirit(nameof(Resurrect));
            if (vitality == 0 || vitality > Maximum)
                throw new ArgumentOutOfRangeException(nameof(vitality), "Resurrection vitality must be within (0, Maximum].");

            Current = vitality;
            State = PlayerVitalityState.Alive;
        }

        public void Eliminate()
        {
            EnsureSpirit(nameof(Eliminate));
            Current = 0;
            State = PlayerVitalityState.Eliminated;
        }

        private void EnsureSpirit(string operation)
        {
            if (State != PlayerVitalityState.Spirit)
                throw new InvalidOperationException($"{operation} is only valid while the player is a Spirit.");
        }
    }
}
