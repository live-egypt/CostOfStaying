namespace CostOfStaying.Core.Contracts
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Application-supplied, server-owned, side-effect-free authorization decision.
    /// It must explicitly allow a command or return a failure; no permissive default policy exists.
    /// Implementations must not mutate match/player state or perform I/O.
    /// </summary>
    public interface IMatchCommandAuthorizationPolicy<TCommand>
    {
        Result Authorize(MatchCommandAuthorizationContext<TCommand> context);
    }
}
