namespace CostOfStaying.Core.Domain
{
    using System.Collections.Generic;
    using CostOfStaying;

    /// <summary>
    /// Bounded FIFO for trusted server-side command envelopes.
    /// Capacity covers both queued and dispatched-but-not-completed commands.
    /// Single-writer/server-serialized; gameplay validation occurs after dequeue.
    /// </summary>
    public sealed class CommandAdmissionQueue<TCommand>
    {
        private readonly Queue<CommandEnvelope<TCommand>> _pending;
        private readonly CommandSequenceTracker _sequenceTracker;
        private readonly Dictionary<SessionId, HashSet<ulong>> _inFlight =
            new Dictionary<SessionId, HashSet<ulong>>();

        internal CommandAdmissionQueue(
            ulong maximumSequenceJump,
            int maximumTrackedSessions,
            int capacity)
        {
            if (capacity <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(capacity), "Queue capacity must be positive.");

            _sequenceTracker = new CommandSequenceTracker(maximumSequenceJump, maximumTrackedSessions);
            _pending = new Queue<CommandEnvelope<TCommand>>(capacity);
            Capacity = capacity;
        }

        public int Capacity { get; }
        public int PendingCount => _pending.Count;
        public int InFlightCount
        {
            get
            {
                int count = 0;
                foreach (HashSet<ulong> sequences in _inFlight.Values)
                    count += sequences.Count;
                return count;
            }
        }

        public int OutstandingCount => PendingCount + InFlightCount;

        /// <summary>
        /// Enqueues only a valid envelope with a newly admitted sequence.
        /// A full outstanding-work budget rejects before consuming the sequence so the sender
        /// may retry after backpressure clears. Once admitted, the sequence is consumed even
        /// if later gameplay validation rejects the command.
        /// </summary>
        internal Result TryEnqueue(CommandEnvelope<TCommand> envelope)
        {
            if (!envelope.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            if (OutstandingCount >= Capacity)
                return Result.Failure(CoreErrorCode.CapacityExceeded);

            Result sequenceResult = _sequenceTracker.TryAccept(envelope.SessionId, envelope.SequenceId);
            if (sequenceResult.IsFailure)
                return sequenceResult;

            _pending.Enqueue(envelope);
            return Result.Success();
        }

        /// <summary>
        /// Records a command handed to an application handler. The admission service calls
        /// this only after dequeue-time session revalidation succeeds.
        /// </summary>
        internal Result MarkInFlight(CommandEnvelope<TCommand> envelope)
        {
            if (!envelope.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            if (!_sequenceTracker.TryGetLastAccepted(envelope.SessionId, out CommandSequenceId lastAccepted) ||
                envelope.SequenceId.Value > lastAccepted.Value)
            {
                return Result.Failure(CoreErrorCode.InvalidState);
            }

            if (!_inFlight.TryGetValue(envelope.SessionId, out HashSet<ulong>? sequences))
            {
                sequences = new HashSet<ulong>();
                _inFlight.Add(envelope.SessionId, sequences);
            }

            return sequences.Add(envelope.SequenceId.Value)
                ? Result.Success()
                : Result.Failure(CoreErrorCode.Conflict);
        }

        /// <summary>
        /// Releases one dispatched command after its application handler has finished,
        /// including rejected or faulted processing paths.
        /// </summary>
        internal Result CompleteInFlight(SessionId sessionId, CommandSequenceId sequenceId)
        {
            if (!sessionId.IsValid || !sequenceId.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            if (!_inFlight.TryGetValue(sessionId, out HashSet<ulong>? sequences) ||
                !sequences.Remove(sequenceId.Value))
            {
                return Result.Failure(CoreErrorCode.NotFound);
            }

            if (sequences.Count == 0)
                _inFlight.Remove(sessionId);

            return Result.Success();
        }

        /// <summary>
        /// Releases replay state after a session is permanently removed.
        /// Reset is refused while commands from that session remain queued or in flight.
        /// </summary>
        internal Result ResetSession(SessionId sessionId)
        {
            if (!sessionId.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            foreach (CommandEnvelope<TCommand> pending in _pending)
            {
                if (pending.SessionId == sessionId)
                    return Result.Failure(CoreErrorCode.InvalidState);
            }

            if (_inFlight.TryGetValue(sessionId, out HashSet<ulong>? inFlight) && inFlight.Count > 0)
                return Result.Failure(CoreErrorCode.InvalidState);

            return _sequenceTracker.Reset(sessionId)
                ? Result.Success()
                : Result.Failure(CoreErrorCode.NotFound);
        }

        /// <summary>Removes the oldest pending command. Returns false and default when the queue is empty.</summary>
        internal bool TryDequeue(out CommandEnvelope<TCommand> envelope)
        {
            if (_pending.Count == 0)
            {
                envelope = default;
                return false;
            }

            envelope = _pending.Dequeue();
            return true;
        }
    }
}