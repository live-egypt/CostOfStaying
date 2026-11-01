namespace CostOfStaying.Core.Domain
{
    using System;
    using CostOfStaying.Core.Contracts;

    /// <summary>Canonical player participation lifecycle.</summary>
    public enum PlayerLifecycleState
    {
        Alive = 0,
        Spirit = 1,
        Eliminated = 2
    }

    /// <summary>
    /// Deterministic vitality and lifecycle primitive.
    /// Calls are single-writer/server-serialized; authorization belongs to the server application layer.
    /// </summary>
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
            State = PlayerLifecycleState.Alive;
        }

        public ActorId PlayerId { get; }
        public PlayerLifecycleState State { get; private set; }
        public uint Current { get; private set; }
        public uint Maximum { get; }

        /// <summary>Reduces vitality while Alive; reaching zero automatically enters Spirit.</summary>
        public void Lose(uint amount)
        {
            if (State != PlayerLifecycleState.Alive || amount == 0)
                return;

            Current = amount >= Current ? 0 : Current - amount;
            if (Current == 0)
                State = PlayerLifecycleState.Spirit;
        }

        /// <summary>Restores vitality only while Alive. This operation never resurrects.</summary>
        public void Restore(uint amount)
        {
            if (State != PlayerLifecycleState.Alive || amount == 0)
                return;

            ulong restored = (ulong)Current + amount;
            Current = restored >= Maximum ? Maximum : (uint)restored;
        }

        /// <summary>
        /// Explicitly resurrects a Spirit. Authorization, costs and cooldowns are checked by the caller.
        /// Returns false without mutation unless the player is Spirit and requestedVitality is positive.
        /// </summary>
        public bool TryResurrect(uint requestedVitality)
        {
            if (State != PlayerLifecycleState.Spirit || requestedVitality == 0)
                return false;

            Current = requestedVitality >= Maximum ? Maximum : requestedVitality;
            State = PlayerLifecycleState.Alive;
            return true;
        }

        /// <summary>Explicitly eliminates an Alive player or Spirit; Eliminated is terminal.</summary>
        public bool TryEliminate()
        {
            if (State == PlayerLifecycleState.Eliminated)
                return false;

            Current = 0;
            State = PlayerLifecycleState.Eliminated;
            return true;
        }
    }
}
