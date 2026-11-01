namespace CostOfStaying.Core.Domain
{
    using System;
    using CostOfStaying;

    /// <summary>Authoritative match phases defined by the gameplay baseline.</summary>
    public enum MatchPhase
    {
        Created = 0,
        Waiting = 1,
        Starting = 2,
        Running = 3,
        VoteOpen = 4,
        Ending = 5,
        Completed = 6
    }

    /// <summary>
    /// Deterministic match phase state machine.
    /// Player-count, timer, voting and outcome preconditions are enforced by application services.
    /// </summary>
    public sealed class MatchLifecycle : Contracts.IMatchLifecycle
    {
        internal MatchLifecycle(MatchId matchId)
        {
            if (!matchId.IsValid)
                throw new ArgumentException("Match identity must be non-zero.", nameof(matchId));

            MatchId = matchId;
            Phase = MatchPhase.Created;
        }

        public MatchId MatchId { get; }
        public MatchPhase Phase { get; private set; }

        public Result TryTransition(MatchPhase nextPhase)
        {
            if (!Enum.IsDefined(typeof(MatchPhase), nextPhase))
                return Result.Failure(CoreErrorCode.InvalidArgument);

            if (!CanTransition(Phase, nextPhase))
                return Result.Failure(CoreErrorCode.InvalidState);

            Phase = nextPhase;
            return Result.Success();
        }

        private static bool CanTransition(MatchPhase current, MatchPhase next)
        {
            switch (current)
            {
                case MatchPhase.Created:
                    return next == MatchPhase.Waiting;
                case MatchPhase.Waiting:
                    return next == MatchPhase.Starting;
                case MatchPhase.Starting:
                    return next == MatchPhase.Running;
                case MatchPhase.Running:
                    return next == MatchPhase.VoteOpen || next == MatchPhase.Ending;
                case MatchPhase.VoteOpen:
                    return next == MatchPhase.Running || next == MatchPhase.Ending;
                case MatchPhase.Ending:
                    return next == MatchPhase.Completed;
                case MatchPhase.Completed:
                default:
                    return false;
            }
        }
    }
}
