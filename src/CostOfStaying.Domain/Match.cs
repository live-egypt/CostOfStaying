using System;
using System.Collections.Generic;
using System.Linq;

namespace CostOfStaying.Domain
{
    /// <summary>
    /// Server-authoritative match aggregate. All state transitions are validated
    /// here; clients send intents that the server checks (ADR-002, RULE-01..05).
    /// This class has no Unity or FishNet dependencies.
    /// </summary>
    public sealed class Match
    {
        public Guid Id { get; }
        public MatchConfig Config { get; }
        public IMatchClock Clock { get; }
        public MatchState State { get; private set; } = MatchState.Created;
        public MatchOutcome Outcome { get; private set; } = MatchOutcome.None;
        public Guid? WinnerId { get; private set; }

        /// <summary>Players by id. Includes Alive and Spirit participants.</summary>
        public Dictionary<Guid, Player> Players { get; } = new Dictionary<Guid, Player>();

        /// <summary>Ordered domain events emitted since match creation.</summary>
        public List<DomainEvent> Events { get; } = new List<DomainEvent>();

        /// <summary>Idempotency store for transfer request ids (SUFFERING_TRANSFER_SYSTEM).</summary>
        public HashSet<string> ProcessedRequestIds { get; } = new HashSet<string>();

        // --- Vote state (RULE-03) ---
        public double? VoteOpenedAtSeconds { get; private set; }
        public double? VoteDeadlineSeconds { get; private set; }
        public Dictionary<Guid, VoteChoice> Votes { get; } = new Dictionary<Guid, VoteChoice>();
        public bool VoteHasBeenOpened { get; private set; }

        // --- Time limit reference ---
        public double? StartedAtSeconds { get; private set; }

        // Pending collapse reschedule flag after state changes
        private bool _collapseRescheduleNeeded;

        public Match(Guid id, MatchConfig config, IMatchClock clock)
        {
            Id = id;
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        // -----------------------------------------------------------------
        // Lifecycle
        // -----------------------------------------------------------------

        public Player AddPlayer(Guid playerId)
        {
            if (State != MatchState.Created && State != MatchState.Waiting)
                throw new InvalidOperationException($"Cannot add player in state {State}.");
            if (Players.ContainsKey(playerId))
                throw new InvalidOperationException($"Player {playerId} already in match.");
            if (Players.Count >= Config.PlayerCount)
                throw new InvalidOperationException($"Match is full ({Config.PlayerCount}).");

            var player = new Player(playerId, Config);
            Players[playerId] = player;

            if (State == MatchState.Created)
                State = MatchState.Waiting;

            return player;
        }

        public void Start()
        {
            if (State != MatchState.Waiting && State != MatchState.Created)
                throw new InvalidOperationException($"Cannot start from {State}.");
            if (Players.Count == 0)
                throw new InvalidOperationException("Cannot start with no players.");

            State = MatchState.Running;
            StartedAtSeconds = Clock.NowSeconds;

            foreach (Player player in Players.Values)
            {
                player.State = PlayerState.Alive;
                player.NextCollapseAtSeconds = Clock.NowSeconds + CurrentCollapseIntervalSeconds();
                player.LastCollapseIntervalSeconds = CurrentCollapseIntervalSeconds();
            }

            Emit(DomainEventKind.MatchStarted, null, null, $"players={Players.Count}");
        }

        // -----------------------------------------------------------------
        // Tick — server heartbeat (vitality drain, debuff expiry, collapse, vote, time limit)
        // -----------------------------------------------------------------

        /// <summary>
        /// Advance server simulation by deltaSeconds. Caller is the dedicated
        /// server tick using its monotonic clock. Delta is derived from
        /// NowSeconds difference; this method does not read wall time.
        /// </summary>
        public void Tick(double deltaSeconds)
        {
            if (State == MatchState.Completed || State == MatchState.Ending)
                return;
            if (State != MatchState.Running && State != MatchState.VoteOpen)
                return;
            if (deltaSeconds <= 0)
                return;

            double now = Clock.NowSeconds;

            // 1) Vitality drain and debuff expiry for alive players
            foreach (Player player in Players.Values.Where(p => p.State == PlayerState.Alive))
            {
                double decay = player.TotalDecayPerSecond;
                player.Vitality = Math.Max(0.0, player.Vitality - decay * deltaSeconds);

                // Tick down debuff durations and collect expired ones
                for (int i = player.Debuffs.Count - 1; i >= 0; i--)
                {
                    player.Debuffs[i].RemainingSeconds -= deltaSeconds;
                    if (player.Debuffs[i].RemainingSeconds <= 0)
                    {
                        player.Debuffs.RemoveAt(i);
                    }
                }
            }

            // 2) Death transitions (RULE-01: death only at zero, one transition)
            var newlyDead = new List<Player>();
            foreach (Player player in Players.Values.Where(p => p.State == PlayerState.Alive && p.Vitality <= 0))
            {
                newlyDead.Add(player);
            }
            foreach (Player player in newlyDead)
            {
                TransitionToSpirit(player, now);
            }

            if (newlyDead.Count > 0)
            {
                _collapseRescheduleNeeded = true;
            }

            // 3) Collapse events per alive player (RULE-01)
            if (State == MatchState.Running || State == MatchState.VoteOpen)
            {
                // Snapshot to avoid modification during iteration
                var aliveSnapshot = Players.Values.Where(p => p.State == PlayerState.Alive).ToList();
                foreach (Player player in aliveSnapshot)
                {
                    if (now >= player.NextCollapseAtSeconds)
                    {
                        ApplyCollapse(player, now);
                    }
                }
            }

            // 4) Reschedule collapses if alive count changed
            if (_collapseRescheduleNeeded)
            {
                RescheduleCollapses(now);
                _collapseRescheduleNeeded = false;
            }

            // 5) Vote timeout check (RULE-03)
            if (State == MatchState.VoteOpen && VoteDeadlineSeconds.HasValue && now >= VoteDeadlineSeconds.Value)
            {
                CloseVote(now);
                // CloseVote may have ended the match; if so, stop further checks
                if (State == MatchState.Completed || State == MatchState.Ending)
                    return;
            }

            // 6) Maybe open vote (RULE-03: when alive becomes three, once per match)
            if (State == MatchState.Running && !VoteHasBeenOpened && AliveCount() == 3)
            {
                OpenVote(now);
            }

            // 7) End conditions (RULE-05)
            EvaluateEndConditions(now);
        }

        // -----------------------------------------------------------------
        // Collapse (RULE-01 + COLLAPSE_SYSTEM reschedule policy)
        // -----------------------------------------------------------------

        private void ApplyCollapse(Player player, double now)
        {
            player.BaseDecayPerSecond += Config.CollapseDecayIncreasePerSecond;

            if (player.HasDebuffCapacity(Config))
            {
                var debuff = new Debuff(
                    id: $"collapse-{Guid.NewGuid():N}",
                    decayBonusPerSecond: Config.DebuffDecayBonusPerSecond,
                    durationSeconds: Config.DebuffDurationSeconds,
                    transferable: true);
                player.Debuffs.Add(debuff);
            }

            double interval = CurrentCollapseIntervalSeconds();
            player.NextCollapseAtSeconds = now + interval;
            player.LastCollapseIntervalSeconds = interval;

            Emit(DomainEventKind.CollapseApplied, player.Id, null, $"interval={interval:F1}s decay={player.BaseDecayPerSecond:F3}");
        }

        private double CurrentCollapseIntervalSeconds()
        {
            return AliveCount() == 3
                ? Config.CollapseIntervalThreeAliveSeconds
                : Config.CollapseIntervalSeconds;
        }

        private void RescheduleCollapses(double now)
        {
            double newInterval = CurrentCollapseIntervalSeconds();
            foreach (Player player in Players.Values.Where(p => p.State == PlayerState.Alive))
            {
                double oldInterval = player.LastCollapseIntervalSeconds;
                if (oldInterval <= 0)
                {
                    player.NextCollapseAtSeconds = now + newInterval;
                    player.LastCollapseIntervalSeconds = newInterval;
                    continue;
                }

                double remaining = player.NextCollapseAtSeconds - now;
                double ratio = Math.Max(0.0, Math.Min(1.0, remaining / oldInterval));
                player.NextCollapseAtSeconds = now + ratio * newInterval;
                player.LastCollapseIntervalSeconds = newInterval;
            }
        }

        // -----------------------------------------------------------------
        // Death -> Spirit
        // -----------------------------------------------------------------

        private void TransitionToSpirit(Player player, double now)
        {
            player.State = PlayerState.Spirit;
            // Spirit starts with zero vitality, no further decay
            player.Vitality = 0;
            Emit(DomainEventKind.PlayerDied, player.Id, null, "Alive->Spirit");
        }

        // -----------------------------------------------------------------
        // Suffering Transfer (RULE-02)
        // -----------------------------------------------------------------

        public enum TransferResult
        {
            Success,
            RejectedDifferentMatch, // not applicable in single-match aggregate, kept for API completeness
            RejectedSelfTarget,
            RejectedNotAlive,
            RejectedNoDebuff,
            RejectedOutOfRange,
            RejectedCooldown,
            RejectedTargetFull,
            RejectedDuplicateRequest
        }

        /// <summary>
        /// Attempt a suffering transfer. All conditions are checked atomically;
        /// on failure no state changes (RULE-02 atomic transaction).
        /// distanceMeters is the server-measured distance; the client never
        /// decides range.
        /// </summary>
        public TransferResult TryTransfer(Guid sourceId, Guid targetId, double distanceMeters, string requestId)
        {
            double now = Clock.NowSeconds;

            // Idempotency: duplicate request id is a no-op rejection without side effects
            if (!string.IsNullOrEmpty(requestId) && ProcessedRequestIds.Contains(requestId))
            {
                Emit(DomainEventKind.TransferRejected, sourceId, targetId, $"duplicate requestId={requestId}");
                return TransferResult.RejectedDuplicateRequest;
            }

            if (sourceId == targetId)
            {
                Emit(DomainEventKind.TransferRejected, sourceId, targetId, "self-target");
                return TransferResult.RejectedSelfTarget;
            }

            if (!Players.TryGetValue(sourceId, out Player? source) ||
                !Players.TryGetValue(targetId, out Player? target))
            {
                Emit(DomainEventKind.TransferRejected, sourceId, targetId, "unknown player");
                return TransferResult.RejectedNotAlive;
            }

            if (source.State != PlayerState.Alive || target.State != PlayerState.Alive)
            {
                Emit(DomainEventKind.TransferRejected, sourceId, targetId, "not alive");
                return TransferResult.RejectedNotAlive;
            }

            Debuff? candidate = source.FindLastTransferableDebuff();
            if (candidate == null)
            {
                Emit(DomainEventKind.TransferRejected, sourceId, targetId, "no transferable debuff");
                return TransferResult.RejectedNoDebuff;
            }

            if (distanceMeters > Config.TransferRangeMeters + 1e-9)
            {
                Emit(DomainEventKind.TransferRejected, sourceId, targetId, $"out of range {distanceMeters:F2}m > {Config.TransferRangeMeters:F2}m");
                return TransferResult.RejectedOutOfRange;
            }

            if (now - source.LastTransferAtSeconds < Config.TransferCooldownSeconds - 1e-9)
            {
                Emit(DomainEventKind.TransferRejected, sourceId, targetId, "cooldown");
                return TransferResult.RejectedCooldown;
            }

            if (!target.HasDebuffCapacity(Config))
            {
                Emit(DomainEventKind.TransferRejected, sourceId, targetId, "target full");
                return TransferResult.RejectedTargetFull;
            }

            // ---- Atomic move ----
            source.Debuffs.Remove(candidate);
            // Preserve remaining duration and bonus
            var moved = new Debuff(candidate.Id, candidate.DecayBonusPerSecond, candidate.RemainingSeconds, candidate.Transferable);
            target.Debuffs.Add(moved);

            source.BetrayalPoints += 1;
            source.LastTransferAtSeconds = now;

            if (!string.IsNullOrEmpty(requestId))
                ProcessedRequestIds.Add(requestId);

            Emit(DomainEventKind.DebuffTransferred, sourceId, targetId, $"debuff={candidate.Id}");
            Emit(DomainEventKind.BetrayalIncreased, sourceId, null, $"betrayal={source.BetrayalPoints}");

            return TransferResult.Success;
        }

        // -----------------------------------------------------------------
        // Vote (RULE-03)
        // -----------------------------------------------------------------

        private void OpenVote(double now)
        {
            VoteHasBeenOpened = true;
            State = MatchState.VoteOpen;
            VoteOpenedAtSeconds = now;
            VoteDeadlineSeconds = now + Config.VoteDurationSeconds;
            Votes.Clear();
            // Voters are the currently alive players; store Abstain as default
            foreach (Player player in Players.Values.Where(p => p.State == PlayerState.Alive))
            {
                Votes[player.Id] = VoteChoice.Abstain;
            }
            Emit(DomainEventKind.VoteOpened, null, null, $"deadline={VoteDeadlineSeconds.Value:F1}s voters={Votes.Count}");
        }

        public bool CastVote(Guid playerId, VoteChoice choice)
        {
            if (State != MatchState.VoteOpen)
                return false;
            if (!Votes.ContainsKey(playerId))
                return false;
            if (Players.TryGetValue(playerId, out Player? player) && player.State != PlayerState.Alive)
                return false;

            Votes[playerId] = choice;
            Emit(DomainEventKind.VoteCast, playerId, null, choice.ToString());
            return true;
        }

        private void CloseVote(double now)
        {
            // Unanimous Yes among the voters alive at open time counts;
            // Abstain/disconnect/timeout = No (RULE-03).
            bool unanimous = Votes.Count > 0 && Votes.Values.All(v => v == VoteChoice.Yes);

            Emit(DomainEventKind.VoteClosed, null, null, unanimous ? "unanimous Yes" : "not unanimous");

            // Players who disconnected are already counted as No; we treat missing
            // or Abstain as No, so only unanimous Yes succeeds.
            if (unanimous)
            {
                // Collective Surrender ends match immediately
                Outcome = MatchOutcome.CollectiveSurrender;
                WinnerId = null;
                State = MatchState.Ending;
                Emit(DomainEventKind.MatchEnded, null, null, "CollectiveSurrender");
                State = MatchState.Completed;
                return;
            }

            // Not unanimous: return to Running, keep collapse interval for 3 alive
            State = MatchState.Running;
            VoteOpenedAtSeconds = null;
            VoteDeadlineSeconds = null;
            Votes.Clear();

            // Reschedule collapses to the 3-alive interval (15s initial value)
            RescheduleCollapses(now);
        }

        // -----------------------------------------------------------------
        // Spirit (RULE-04)
        // -----------------------------------------------------------------

        public bool CollectEcho(Guid playerId, string echoId)
        {
            if (!Players.TryGetValue(playerId, out Player? player))
                return false;
            if (player.State != PlayerState.Spirit)
                return false;
            if (player.CollectedEchoIds.Contains(echoId))
                return false;

            player.CollectedEchoIds.Add(echoId);
            player.EchoFragments += 1;
            player.WitnessScore += 1; // 1:1 for MVP; balance may change after playtest
            Emit(DomainEventKind.EchoCollected, playerId, null, echoId);
            return true;
        }

        public bool TrySendHint(Guid spiritId, string hintText)
        {
            if (!Players.TryGetValue(spiritId, out Player? spirit))
                return false;
            if (spirit.State != PlayerState.Spirit)
                return false;

            double now = Clock.NowSeconds;
            if (now - spirit.LastHintAtSeconds < Config.HintCooldownSeconds - 1e-9)
                return false;

            spirit.LastHintAtSeconds = now;
            Emit(DomainEventKind.HintSent, spiritId, null, hintText);
            return true;
        }

        // -----------------------------------------------------------------
        // End conditions (RULE-05)
        // -----------------------------------------------------------------

        private void EvaluateEndConditions(double now)
        {
            if (State == MatchState.Completed || State == MatchState.Ending)
                return;

            int alive = AliveCount();
            bool hasDeath = Players.Values.Any(p => p.State == PlayerState.Spirit || p.State == PlayerState.Dead);

            // Mutual Collapse: no alive left — only if someone actually died
            if (alive == 0 && hasDeath)
            {
                Outcome = MatchOutcome.MutualCollapse;
                WinnerId = null;
                State = MatchState.Ending;
                Emit(DomainEventKind.MatchEnded, null, null, "MutualCollapse");
                State = MatchState.Completed;
                return;
            }

            // Last Survivor: exactly one alive, and no time-limit draw logic preempts it
            // Note: if time limit coincides with last survivor, last survivor takes precedence
            // as mutual collapse also handled above. We check time limit after.
            // However spec says at 3 alive vote may end; that is handled in CloseVote.

            // Time Limit (20 min initial value): highest vitality wins, tie => Draw
            if (StartedAtSeconds.HasValue && now - StartedAtSeconds.Value >= Config.MatchTimeLimitSeconds - 1e-9)
            {
                var alivePlayers = Players.Values.Where(p => p.State == PlayerState.Alive).ToList();
                double maxVitality = alivePlayers.Max(p => p.Vitality);
                var top = alivePlayers.Where(p => Math.Abs(p.Vitality - maxVitality) < 1e-9).ToList();

                if (top.Count > 1)
                {
                    Outcome = MatchOutcome.Draw;
                    WinnerId = null;
                    Emit(DomainEventKind.MatchEnded, null, null, $"Draw at {maxVitality:F2}");
                }
                else
                {
                    Outcome = MatchOutcome.TimeLimit;
                    WinnerId = top[0].Id;
                    Emit(DomainEventKind.MatchEnded, top[0].Id, null, $"TimeLimit winner vitality={maxVitality:F2}");
                }

                State = MatchState.Ending;
                State = MatchState.Completed;
                return;
            }

            if (alive == 1 && hasDeath)
            {
                Player winner = Players.Values.First(p => p.State == PlayerState.Alive);
                Outcome = MatchOutcome.LastSurvivor;
                WinnerId = winner.Id;
                State = MatchState.Ending;
                Emit(DomainEventKind.MatchEnded, winner.Id, null, "LastSurvivor");
                State = MatchState.Completed;
                return;
            }
        }

        // -----------------------------------------------------------------
        // Helpers
        // -----------------------------------------------------------------

        public int AliveCount()
        {
            int count = 0;
            foreach (Player player in Players.Values)
            {
                if (player.State == PlayerState.Alive)
                    count++;
            }
            return count;
        }

        private void Emit(DomainEventKind kind, Guid? playerId, Guid? targetId, string? detail)
        {
            Events.Add(new DomainEvent(kind, Clock.NowSeconds, playerId, targetId, detail));
        }
    }
}
