namespace CostOfStaying.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;

    /// <summary>
    /// Match-scoped, bounded membership registry. Registration order is deterministic.
    /// Single-writer/server-serialized; removal changes membership only, not player lifecycle.
    /// </summary>
    public sealed class MatchPlayerRegistry : IMatchPlayerRegistry
    {
        private readonly Dictionary<ActorId, IVitalityAccount> _accounts =
            new Dictionary<ActorId, IVitalityAccount>();
        private readonly List<ActorId> _orderedIds = new List<ActorId>();

        internal MatchPlayerRegistry(MatchId matchId, int capacity)
        {
            if (!matchId.IsValid)
                throw new ArgumentException("Match identity must be non-zero.", nameof(matchId));
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Player capacity must be positive.");

            MatchId = matchId;
            Capacity = capacity;
        }

        public MatchId MatchId { get; }
        public int Capacity { get; }
        public int Count => _accounts.Count;

        public Result Register(IVitalityAccount account)
        {
            if (account is null || !account.PlayerId.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            if (_accounts.ContainsKey(account.PlayerId))
                return Result.Failure(CoreErrorCode.Conflict);

            if (_accounts.Count >= Capacity)
                return Result.Failure(CoreErrorCode.CapacityExceeded);

            _accounts.Add(account.PlayerId, account);
            _orderedIds.Add(account.PlayerId);
            return Result.Success();
        }

        public Result<IVitalityAccount> Get(ActorId actorId)
        {
            if (!actorId.IsValid)
                return Result<IVitalityAccount>.Failure(CoreErrorCode.InvalidArgument);

            return _accounts.TryGetValue(actorId, out IVitalityAccount? account)
                ? Result<IVitalityAccount>.Success(account!)
                : Result<IVitalityAccount>.Failure(CoreErrorCode.NotFound);
        }

        public Result Remove(ActorId actorId)
        {
            if (!actorId.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            if (!_accounts.Remove(actorId))
                return Result.Failure(CoreErrorCode.NotFound);

            _orderedIds.Remove(actorId);
            return Result.Success();
        }

        public IReadOnlyList<ActorId> GetPlayerIdsSnapshot()
        {
            return Array.AsReadOnly(_orderedIds.ToArray());
        }
    }
}
