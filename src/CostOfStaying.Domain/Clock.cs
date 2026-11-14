namespace CostOfStaying.Domain
{
    /// <summary>
    /// Server-side time source. Per ADR-002 the server clock is the reference
    /// for all timers; client time is never trusted for game decisions.
    /// </summary>
    public interface IMatchClock
    {
        double NowSeconds { get; }
    }

    /// <summary>Deterministic clock for tests and simulations.</summary>
    public sealed class ManualClock : IMatchClock
    {
        public double NowSeconds { get; private set; }

        public ManualClock(double startSeconds = 0.0)
        {
            NowSeconds = startSeconds;
        }

        public void Advance(double deltaSeconds)
        {
            NowSeconds += deltaSeconds;
        }

        public void Set(double seconds)
        {
            NowSeconds = seconds;
        }
    }

    /// <summary>
    /// Monotonic clock for the dedicated server. Uses Stopwatch so it is
    /// immune to wall-clock adjustments (NTP, user changes).
    /// </summary>
    public sealed class MonotonicClock : IMatchClock
    {
        private readonly System.Diagnostics.Stopwatch _stopwatch =
            System.Diagnostics.Stopwatch.StartNew();

        public double NowSeconds => _stopwatch.Elapsed.TotalSeconds;
    }
}
