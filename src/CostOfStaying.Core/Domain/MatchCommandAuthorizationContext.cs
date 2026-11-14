namespace CostOfStaying.Core.Domain
{
    using System;

    /// <summary>
    /// Trusted immutable facts supplied to a server-owned command authorization policy.
    /// All identities and state values are resolved by Core; none are copied from client claims.
    /// </summary>
    public readonly struct MatchCommandAuthorizationContext<TCommand>
    {
        public MatchCommandAuthorizationContext(
            SessionId sessionId,
            ActorId actorId,
            MatchId matchId,
            MatchPhase matchPhase,
            PlayerLifecycleState playerState,
            ServerTick receivedAt,
            TCommand command)
        {
            if (!sessionId.IsValid)
                throw new ArgumentException("Session identity must be non-zero.", nameof(sessionId));
            if (!actorId.IsValid)
                throw new ArgumentException("Actor identity must be non-zero.", nameof(actorId));
            if (!matchId.IsValid)
                throw new ArgumentException("Match identity must be non-zero.", nameof(matchId));
            if (!Enum.IsDefined(typeof(MatchPhase), matchPhase))
                throw new ArgumentOutOfRangeException(nameof(matchPhase));
            if (!Enum.IsDefined(typeof(PlayerLifecycleState), playerState))
                throw new ArgumentOutOfRangeException(nameof(playerState));
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            SessionId = sessionId;
            ActorId = actorId;
            MatchId = matchId;
            MatchPhase = matchPhase;
            PlayerState = playerState;
            ReceivedAt = receivedAt;
            Command = command;
        }

        public SessionId SessionId { get; }
        public ActorId ActorId { get; }
        public MatchId MatchId { get; }
        public MatchPhase MatchPhase { get; }
        public PlayerLifecycleState PlayerState { get; }
        public ServerTick ReceivedAt { get; }
        public TCommand Command { get; }

        public bool IsValid =>
            SessionId.IsValid &&
            ActorId.IsValid &&
            MatchId.IsValid &&
            Enum.IsDefined(typeof(MatchPhase), MatchPhase) &&
            Enum.IsDefined(typeof(PlayerLifecycleState), PlayerState) &&
            Command is not null;
    }
}
