using System;

namespace CostOfStaying.Domain
{
    /// <summary>
    /// Server-side validation of client intents (GATE-3 Security hardening).
    /// All time checks use server monotonic clock — client SentAt is never trusted.
    /// </summary>
    public static class IntentValidator
    {
        /// <summary>Maximum allowed skew between client SentAt and server Now (seconds).</summary>
        public const double MaxSentAtSkewSeconds = 5.0;

        /// <summary>Simple per-player rate limit for Transfer intents (requests/sec window).</summary>
        public const double TransferRateWindowSeconds = 10.0;
        public const int MaxTransfersPerWindow = 6;

        /// <summary>
        /// Validates SentAt is within bounded skew of server clock.
        /// Prevents future-dated and replay-old intents (THREAT_MODEL: Replay, Serialization abuse).
        /// </summary>
        public static bool IsSentAtValid(double serverNowSeconds, double clientSentAtSeconds, double maxSkewSeconds = MaxSentAtSkewSeconds)
        {
            // Client clock may be slightly ahead/behind; reject if |delta| > skew
            double delta = Math.Abs(serverNowSeconds - clientSentAtSeconds);
            return delta <= maxSkewSeconds + 1e-9;
        }

        /// <summary>
        /// Fail-closed distance check — server-measured distance only (ADR-002).
        /// Client-claimed distance is ignored (COMBAT_SYSTEM).
        /// </summary>
        public static bool IsDistanceValid(double serverMeasuredMeters, double maxRangeMeters)
        {
            return serverMeasuredMeters <= maxRangeMeters + 1e-9 && serverMeasuredMeters >= 0;
        }

        /// <summary>
        /// Check payload size / id length to prevent serialization abuse.
        /// </summary>
        public static bool IsRequestIdValid(string? requestId)
        {
            if (string.IsNullOrEmpty(requestId)) return false;
            // 8..64 chars, alphanumeric + -_
            if (requestId.Length < 8 || requestId.Length > 64) return false;
            foreach (char c in requestId)
            {
                if (!(char.IsLetterOrDigit(c) || c == '-' || c == '_'))
                    return false;
            }
            return true;
        }
    }
}
