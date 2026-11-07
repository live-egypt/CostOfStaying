namespace CostOfStaying.Core.Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>Immutable projection of one player's lifecycle and vitality at a server revision.</summary>
    public readonly struct PlayerStateSnapshot
    {
        internal PlayerStateSnapshot(
            ActorId actorId,
            PlayerLifecycleState state,
            uint currentVitality,
            uint maximumVitality)
        {
            ActorId = actorId;
            State = state;
            CurrentVitality = currentVitality;
            MaximumVitality = maximumVitality;
        }

        public ActorId ActorId { get; }
        public PlayerLifecycleState State { get; }
        public uint CurrentVitality { get; }
        public uint MaximumVitality { get; }

        /// <summary>False for default values or lifecycle/vitality combinations forbidden by ADR-007.</summary>
        public bool IsValid
        {
            get
            {
                if (!ActorId.IsValid || MaximumVitality == 0 || CurrentVitality > MaximumVitality)
                    return false;

                switch (State)
                {
                    case PlayerLifecycleState.Alive:
                        return CurrentVitality > 0;
                    case PlayerLifecycleState.Spirit:
                    case PlayerLifecycleState.Eliminated:
                        return CurrentVitality == 0;
                    default:
                        return false;
                }
            }
        }
    }

    /// <summary>
    /// Immutable Phase 1 roster projection. It intentionally excludes world transforms,
    /// debuffs, collapse timers, voting, connection state and transport-specific data.
    /// </summary>
    public sealed class MatchRosterSnapshot
    {
        private readonly IReadOnlyList<PlayerStateSnapshot> _players;

        internal MatchRosterSnapshot(
            MatchId matchId,
            MatchPhase phase,
            ServerTick serverTick,
            StateRevision revision,
            PlayerStateSnapshot[] players)
        {
            if (!matchId.IsValid)
                throw new ArgumentException("Match identity must be non-zero.", nameof(matchId));
            if (!Enum.IsDefined(typeof(MatchPhase), phase))
                throw new ArgumentOutOfRangeException(nameof(phase));
            if (players is null)
                throw new ArgumentNullException(nameof(players));

            var copy = (PlayerStateSnapshot[])players.Clone();
            var seen = new HashSet<ActorId>();
            foreach (PlayerStateSnapshot player in copy)
            {
                if (!player.IsValid)
                    throw new ArgumentException("Snapshot contains an invalid player state.", nameof(players));
                if (!seen.Add(player.ActorId))
                    throw new ArgumentException("Snapshot contains a duplicate ActorId.", nameof(players));
            }

            MatchId = matchId;
            Phase = phase;
            ServerTick = serverTick;
            Revision = revision;
            _players = Array.AsReadOnly(copy);
        }

        public MatchId MatchId { get; }
        public MatchPhase Phase { get; }
        public ServerTick ServerTick { get; }
        public StateRevision Revision { get; }
        public IReadOnlyList<PlayerStateSnapshot> Players => _players;
        public int PlayerCount => _players.Count;
    }
}
