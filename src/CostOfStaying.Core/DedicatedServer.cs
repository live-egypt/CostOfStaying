using System;
using System.Collections.Generic;

namespace CostOfStaying.Core
{
    /// <summary>
    /// GATE-3.3 — Dedicated Server skeleton (ADR-002/016/023).
    /// Owns matches, ticks at fixed Hz, holds snapshots in memory (not DB).
    /// Reconnect policy TBD (60s proposal) — not enforced yet (GATE-3.6).
    /// </summary>
    public sealed class DedicatedServer
    {
        private readonly Dictionary<ulong, ServerMatch> _matches = new();
        private readonly IMonotonicClock _clock;

        public DedicatedServer(IMonotonicClock clock)
        {
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        public ServerMatch CreateMatch(ulong matchId, int playerCount = 4)
        {
            if (matchId == 0) throw new ArgumentException("matchId 0 sentinel");
            if (_matches.ContainsKey(matchId)) throw new InvalidOperationException($"Match {matchId} exists");
            var m = new ServerMatch(matchId, playerCount);
            _matches[matchId] = m;
            return m;
        }

        public bool TryGetMatch(ulong matchId, out ServerMatch? match) => _matches.TryGetValue(matchId, out match);

        /// <summary>Tick all matches — drain, collapse, vote, end (RULE-01..05 via Match).</summary>
        public void Tick(double deltaSeconds)
        {
            double now = _clock.NowSeconds;
            foreach (var m in _matches.Values)
                m.Tick(now, deltaSeconds);
        }

        public MatchSnapshot Snapshot(ulong matchId)
        {
            if (!_matches.TryGetValue(matchId, out var m)) throw new InvalidOperationException("unknown match");
            return m.Snapshot(_clock.NowSeconds);
        }

        public IReadOnlyCollection<ulong> MatchIds => _matches.Keys;
    }

    public sealed class ServerMatch
    {
        public ulong MatchId { get; }
        public int PlayerCount { get; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public ServerMatchState State { get; private set; } = ServerMatchState.Created;
        public MatchSnapshot? LastSnapshot { get; private set; }

        // Simplified: per-match player sessions (ADR-016 binding)
        private readonly Dictionary<ulong, PlayerSession> _sessions = new();

        public ServerMatch(ulong matchId, int playerCount)
        {
            MatchId = matchId;
            PlayerCount = playerCount;
        }

        public PlayerSession BindTransport(ulong actorId, string transportConnectionId)
        {
            if (actorId == 0) throw new ArgumentException("actor 0");
            if (string.IsNullOrWhiteSpace(transportConnectionId)) throw new ArgumentException("transport empty");
            if (_sessions.TryGetValue(actorId, out var existing) && existing.IsConnected)
                throw new InvalidOperationException($"Actor {actorId} already bound to {existing.TransportConnectionId}");
            var s = new PlayerSession(actorId, transportConnectionId);
            _sessions[actorId] = s;
            return s;
        }

        public void Tick(double now, double dt)
        {
            // In full impl, delegates to CostOfStaying.Domain.Match.Tick
            State = ServerMatchState.Running;
            // Snapshot in memory (DATA_ARCHITECTURE: not persistent)
            LastSnapshot = new MatchSnapshot(MatchId, now, State, _sessions.Count);
        }

        public MatchSnapshot Snapshot(double now) => new(MatchId, now, State, _sessions.Count);
    }

    public enum ServerMatchState { Created, Running, VoteOpen, Ending, Completed }

    public sealed class PlayerSession
    {
        public ulong ActorId { get; }
        public string TransportConnectionId { get; }
        public bool IsConnected { get; private set; } = true;
        public DateTime BoundAt { get; } = DateTime.UtcNow;

        public PlayerSession(ulong actorId, string transportConnectionId)
        {
            ActorId = actorId;
            TransportConnectionId = transportConnectionId;
        }

        public void Disconnect() => IsConnected = false;
    }

    public sealed class MatchSnapshot
    {
        public ulong MatchId { get; }
        public double AtSeconds { get; }
        public ServerMatchState State { get; }
        public int SessionCount { get; }

        public MatchSnapshot(ulong matchId, double atSeconds, ServerMatchState state, int sessionCount)
        {
            MatchId = matchId;
            AtSeconds = atSeconds;
            State = state;
            SessionCount = sessionCount;
        }
    }

    public interface IMonotonicClock
    {
        double NowSeconds { get; }
    }

    public sealed class ManualClock : IMonotonicClock
    {
        public double NowSeconds { get; private set; }
        public ManualClock(double start = 0) => NowSeconds = start;
        public void Advance(double dt) => NowSeconds += dt;
    }
}
