using System;

namespace CostOfStaying.Domain
{
    /// <summary>
    /// Client intents validated by the server (ADR-002: client sends intent,
    /// server is the sole authority). These DTOs carry no authority; they are
    /// the input to Match methods. Transport (FishNet RPC) will wrap them later.
    /// </summary>
    public abstract class ClientIntent
    {
        public Guid PlayerId { get; set; }
        public string RequestId { get; set; } = Guid.NewGuid().ToString("N");
        public double SentAtSeconds { get; set; }
    }

    public sealed class TransferIntent : ClientIntent
    {
        public Guid TargetId { get; set; }
        /// <summary>
        /// Server-measured distance is not set by the client; this field is
        /// populated server-side from positions. Kept here for logging only.
        /// </summary>
        public double ClaimedDistanceMeters { get; set; }
    }

    public sealed class VoteIntent : ClientIntent
    {
        public VoteChoice Choice { get; set; }
    }

    public sealed class HintIntent : ClientIntent
    {
        public string HintText { get; set; } = string.Empty;
    }

    public sealed class CollectEchoIntent : ClientIntent
    {
        public string EchoId { get; set; } = string.Empty;
    }
}
