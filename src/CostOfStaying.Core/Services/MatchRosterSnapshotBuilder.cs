namespace CostOfStaying.Core.Services
{
    using System;
    using System.Collections.Generic;
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;

    /// <summary>
    /// Creates a deterministic snapshot from the match lifecycle and roster.
    /// The caller supplies the authoritative tick and revision; this builder never advances either.
    /// Single-writer/server-serialized.
    /// </summary>
    public sealed class MatchRosterSnapshotBuilder : IMatchRosterSnapshotBuilder
    {
        private readonly IMatchLifecycle _match;
        private readonly IMatchPlayerRegistry _players;

        internal MatchRosterSnapshotBuilder(IMatchLifecycle match, IMatchPlayerRegistry players)
        {
            _match = match ?? throw new ArgumentNullException(nameof(match));
            _players = players ?? throw new ArgumentNullException(nameof(players));

            if (!_match.MatchId.IsValid || _match.MatchId != _players.MatchId)
                throw new ArgumentException("Match lifecycle and player registry must refer to the same valid MatchId.");
        }

        public Result<MatchRosterSnapshot> Build(ServerTick serverTick, StateRevision revision)
        {
            MatchPhase phase = _match.Phase;
            if (!Enum.IsDefined(typeof(MatchPhase), phase))
                return Result<MatchRosterSnapshot>.Failure(CoreErrorCode.InvalidState);

            IReadOnlyList<ActorId> ids = _players.GetPlayerIdsSnapshot();
            if (ids is null)
                return Result<MatchRosterSnapshot>.Failure(CoreErrorCode.InvalidState);

            var seen = new HashSet<ActorId>();
            var snapshots = new List<PlayerStateSnapshot>(ids.Count);

            for (int index = 0; index < ids.Count; index++)
            {
                ActorId actorId = ids[index];
                if (!actorId.IsValid || !seen.Add(actorId))
                    return Result<MatchRosterSnapshot>.Failure(CoreErrorCode.InvalidState);

                Result<IVitalityAccount> lookup = _players.Get(actorId);
                if (lookup.IsFailure)
                    return Result<MatchRosterSnapshot>.Failure(lookup.ErrorCode);

                IVitalityAccount account = lookup.Value;
                if (account is null || account.PlayerId != actorId)
                    return Result<MatchRosterSnapshot>.Failure(CoreErrorCode.InvalidState);

                var player = new PlayerStateSnapshot(
                    actorId,
                    account.State,
                    account.Current,
                    account.Maximum);

                if (!player.IsValid)
                    return Result<MatchRosterSnapshot>.Failure(CoreErrorCode.InvalidState);

                snapshots.Add(player);
            }

            return Result<MatchRosterSnapshot>.Success(
                new MatchRosterSnapshot(_match.MatchId, phase, serverTick, revision, snapshots.ToArray()));
        }
    }
}
