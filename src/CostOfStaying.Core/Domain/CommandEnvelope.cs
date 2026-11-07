namespace CostOfStaying.Core.Domain
{
    using System;

    /// <summary>
    /// Server-side envelope created after resolving the authenticated connection to a Core SessionId.
    /// This is not a client wire DTO; SessionId and ReceivedAt must be supplied by trusted server code.
    /// </summary>
    public readonly struct CommandEnvelope<TCommand>
    {
        public CommandEnvelope(SessionId sessionId, CommandSequenceId sequenceId, ServerTick receivedAt, TCommand command)
        {
            if (!sessionId.IsValid)
                throw new ArgumentException("Session identity must be non-zero.", nameof(sessionId));
            if (!sequenceId.IsValid)
                throw new ArgumentException("Command sequence must be non-zero.", nameof(sequenceId));
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            SessionId = sessionId;
            SequenceId = sequenceId;
            ReceivedAt = receivedAt;
            Command = command;
        }

        public SessionId SessionId { get; }
        public CommandSequenceId SequenceId { get; }
        public ServerTick ReceivedAt { get; }
        public TCommand Command { get; }

        /// <summary>Default(struct) bypasses constructor validation; ingress must check this before processing.</summary>
        public bool IsValid =>
            SessionId.IsValid &&
            SequenceId.IsValid &&
            Command is not null;
    }
}
