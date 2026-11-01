namespace CostOfStaying.Core.Domain
{
    using System.Collections.Generic;
    using CostOfStaying;

    /// <summary>
    /// Bounded FIFO for trusted server-side command envelopes.
    /// Single-writer/server-serialized; gameplay validation occurs after dequeue.
    /// </summary>
    public sealed class CommandAdmissionQueue<TCommand>
    {
        private readonly Queue<CommandEnvelope<TCommand>> _pending;
        private readonly CommandSequenceTracker _sequenceTracker;

        public CommandAdmissionQueue(
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

        /// <summary>
        /// Enqueues only a valid envelope with a newly admitted sequence.
        /// A full queue rejects before consuming the sequence so the sender may retry after backpressure clears.
        /// Once admitted, the sequence is consumed even if later gameplay validation rejects the command.
        /// </summary>
        public Result TryEnqueue(CommandEnvelope<TCommand> envelope)
        {
            if (!envelope.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            if (_pending.Count >= Capacity)
                return Result.Failure(CoreErrorCode.CapacityExceeded);

            Result sequenceResult = _sequenceTracker.TryAccept(envelope.SessionId, envelope.SequenceId);
            if (sequenceResult.IsFailure)
                return sequenceResult;

            _pending.Enqueue(envelope);
            return Result.Success();
        }

        /// <summary>
        /// Releases replay state after a session is permanently removed.
        /// Reset is refused while commands from that session remain queued.
        /// </summary>
        public Result ResetSession(SessionId sessionId)
        {
            if (!sessionId.IsValid)
                return Result.Failure(CoreErrorCode.InvalidArgument);

            foreach (CommandEnvelope<TCommand> pending in _pending)
            {
                if (pending.SessionId == sessionId)
                    return Result.Failure(CoreErrorCode.InvalidState);
            }

            return _sequenceTracker.Reset(sessionId)
                ? Result.Success()
                : Result.Failure(CoreErrorCode.NotFound);
        }

        /// <summary>Removes the oldest pending command. Returns false and default when the queue is empty.</summary>
        public bool TryDequeue(out CommandEnvelope<TCommand> envelope)
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
