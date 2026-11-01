namespace CostOfStaying.Core.Domain
{
    using System;

    /// <summary>
    /// Command paired with the ActorId re-resolved from the authenticated session
    /// immediately before dequeue handoff to the application handler.
    /// </summary>
    public readonly struct AuthenticatedCommand<TCommand>
    {
        internal AuthenticatedCommand(ActorId actorId, CommandEnvelope<TCommand> envelope)
        {
            if (!actorId.IsValid)
                throw new ArgumentException("Actor identity must be non-zero.", nameof(actorId));
            if (!envelope.IsValid)
                throw new ArgumentException("Command envelope must be valid.", nameof(envelope));

            ActorId = actorId;
            Envelope = envelope;
        }

        public ActorId ActorId { get; }
        public CommandEnvelope<TCommand> Envelope { get; }
        public SessionId SessionId => Envelope.SessionId;
        public CommandSequenceId SequenceId => Envelope.SequenceId;
        public ServerTick ReceivedAt => Envelope.ReceivedAt;
        public TCommand Command => Envelope.Command;
    }
}
