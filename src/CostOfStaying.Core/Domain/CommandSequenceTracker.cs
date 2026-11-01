namespace CostOfStaying.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using CostOfStaying;

    /// <summary>
    /// Per-session monotonic command sequence admission.
    /// Calls must be serialized by the authoritative server; this type is not thread-safe.
    /// </summary>
    public sealed class CommandSequenceTracker
    {
        private readonly Dictionary<SessionId, ulong> _lastAccepted = new Dictionary<SessionId, ulong>();

        public CommandSequenceTracker(ulong maximumSequenceJump, int maximumTrackedSessions)
        {
            if (maximumSequenceJump == 0UL)
                throw new ArgumentOutOfRangeException(nameof(maximumSequenceJump), "Maximum sequence jump must be positive.");
            if (maximumTrackedSessions <= 0)
                throw new ArgumentOutOfRangeException(nameof(maximumTrackedSessions), "Maximum tracked sessions must be positive.");

            MaximumSequenceJump = maximumSequenceJump;
            MaximumTrackedSessions = maximumTrackedSessions;
        }

        public ulong MaximumSequenceJump { get; }
        public int MaximumTrackedSessions { get; }
        public int TrackedSessionCount => _lastAccepted.Count;

        /// <summary>
        /// Accepts a strictly newer sequence if its gap from the last accepted value (zero for a new session)
        /// does not exceed MaximumSequenceJump. Accepted sequence numbers are consumed even if later gameplay
        /// validation rejects the command, preventing replay of the same command envelope.
        /// </summary>
        public Result TryAccept(SessionId sessionId, CommandSequenceId sequenceId)
        {
            if (!sessionId.IsValid || !sequenceId.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            ulong previous = _lastAccepted.TryGetValue(sessionId, out ulong last) ? last : 0UL;
            if (sequenceId.Value == previous)
                return Result.Failure(CoreErrorCode.Conflict);

            if (sequenceId.Value < previous)
                return Result.Failure(CoreErrorCode.InvalidState);

            ulong jump = sequenceId.Value - previous;
            if (jump > MaximumSequenceJump)
                return Result.Failure(CoreErrorCode.OutOfRange);

            if (!_lastAccepted.ContainsKey(sessionId) && _lastAccepted.Count >= MaximumTrackedSessions)
                return Result.Failure(CoreErrorCode.CapacityExceeded);

            _lastAccepted[sessionId] = sequenceId.Value;
            return Result.Success();
        }

        public bool TryGetLastAccepted(SessionId sessionId, out CommandSequenceId sequenceId)
        {
            if (!sessionId.IsValid)
                throw new ArgumentException("Session identity must be non-zero.", nameof(sessionId));

            if (_lastAccepted.TryGetValue(sessionId, out ulong value))
            {
                sequenceId = new CommandSequenceId(value);
                return true;
            }

            sequenceId = default;
            return false;
        }

        /// <summary>Clears replay state when the owning authenticated session is permanently removed.</summary>
        public bool Reset(SessionId sessionId)
        {
            if (!sessionId.IsValid)
                throw new ArgumentException("Session identity must be non-zero.", nameof(sessionId));

            return _lastAccepted.Remove(sessionId);
        }
    }
}
