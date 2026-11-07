namespace CostOfStaying.Core.Tests
{
    using System;
    using CostOfStaying;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CommandSequenceTrackerTests
    {
        [Test]
        public void Constructor_RejectsZeroMaximumJump()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CommandSequenceTracker(0, 1));
        }

        [Test]
        public void Constructor_RejectsZeroTrackedSessionCapacity()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CommandSequenceTracker(1, 0));
        }

        [Test]
        public void Constructor_RejectsNegativeTrackedSessionCapacity()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CommandSequenceTracker(1, -1));
        }

        [Test]
        public void FirstSequence_IsAcceptedWithinConfiguredJump()
        {
            var tracker = new CommandSequenceTracker(5, 4);
            Result result = tracker.TryAccept(new SessionId(1), new CommandSequenceId(5));

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(tracker.TryGetLastAccepted(new SessionId(1), out CommandSequenceId last), Is.True);
            Assert.That(last.Value, Is.EqualTo(5UL));
        }

        [Test]
        public void FirstSequence_AboveMaximumJumpIsRejectedWithoutMutation()
        {
            var tracker = new CommandSequenceTracker(5, 4);
            Result result = tracker.TryAccept(new SessionId(1), new CommandSequenceId(6));

            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.OutOfRange));
            Assert.That(tracker.TryGetLastAccepted(new SessionId(1), out _), Is.False);
        }

        [Test]
        public void NextSequence_AcceptsGapWithinMaximumJump()
        {
            var tracker = new CommandSequenceTracker(4, 4);
            Assert.That(tracker.TryAccept(new SessionId(1), new CommandSequenceId(3)).IsSuccess, Is.True);
            Result result = tracker.TryAccept(new SessionId(1), new CommandSequenceId(7));

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(tracker.TryGetLastAccepted(new SessionId(1), out CommandSequenceId last), Is.True);
            Assert.That(last.Value, Is.EqualTo(7UL));
        }

        [Test]
        public void DuplicateSequence_IsRejectedAsConflictWithoutMutation()
        {
            var tracker = new CommandSequenceTracker(5, 4);
            tracker.TryAccept(new SessionId(1), new CommandSequenceId(2));
            Result result = tracker.TryAccept(new SessionId(1), new CommandSequenceId(2));

            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.Conflict));
            Assert.That(tracker.TryGetLastAccepted(new SessionId(1), out CommandSequenceId last), Is.True);
            Assert.That(last.Value, Is.EqualTo(2UL));
        }

        [Test]
        public void OlderSequence_IsRejectedWithoutMutation()
        {
            var tracker = new CommandSequenceTracker(5, 4);
            tracker.TryAccept(new SessionId(1), new CommandSequenceId(3));
            Result result = tracker.TryAccept(new SessionId(1), new CommandSequenceId(2));

            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(tracker.TryGetLastAccepted(new SessionId(1), out CommandSequenceId last), Is.True);
            Assert.That(last.Value, Is.EqualTo(3UL));
        }

        [Test]
        public void ExcessiveSequenceJump_IsRejectedWithoutMutation()
        {
            var tracker = new CommandSequenceTracker(3, 4);
            tracker.TryAccept(new SessionId(1), new CommandSequenceId(2));
            Result result = tracker.TryAccept(new SessionId(1), new CommandSequenceId(6));

            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.OutOfRange));
            Assert.That(tracker.TryGetLastAccepted(new SessionId(1), out CommandSequenceId last), Is.True);
            Assert.That(last.Value, Is.EqualTo(2UL));
        }

        [Test]
        public void Sessions_HaveIndependentSequenceWindows()
        {
            var tracker = new CommandSequenceTracker(2, 4);
            Assert.That(tracker.TryAccept(new SessionId(1), new CommandSequenceId(2)).IsSuccess, Is.True);
            Assert.That(tracker.TryAccept(new SessionId(2), new CommandSequenceId(1)).IsSuccess, Is.True);
        }

        [Test]
        public void Capacity_RejectsNewSessionsUntilAnExistingSessionIsReset()
        {
            var tracker = new CommandSequenceTracker(5, 2);
            Assert.That(tracker.TryAccept(new SessionId(1), new CommandSequenceId(1)).IsSuccess, Is.True);
            Assert.That(tracker.TryAccept(new SessionId(2), new CommandSequenceId(1)).IsSuccess, Is.True);

            Result full = tracker.TryAccept(new SessionId(3), new CommandSequenceId(1));
            Assert.That(full.ErrorCode, Is.EqualTo(CoreErrorCode.CapacityExceeded));
            Assert.That(tracker.TrackedSessionCount, Is.EqualTo(2));

            Assert.That(tracker.Reset(new SessionId(1)), Is.True);
            Assert.That(tracker.TryAccept(new SessionId(3), new CommandSequenceId(1)).IsSuccess, Is.True);
            Assert.That(tracker.TrackedSessionCount, Is.EqualTo(2));
        }

        [Test]
        public void ExistingSessionCanAdvanceWhenCapacityIsFull()
        {
            var tracker = new CommandSequenceTracker(5, 1);
            Assert.That(tracker.TryAccept(new SessionId(1), new CommandSequenceId(1)).IsSuccess, Is.True);
            Assert.That(tracker.TryAccept(new SessionId(1), new CommandSequenceId(2)).IsSuccess, Is.True);
            Assert.That(tracker.TrackedSessionCount, Is.EqualTo(1));
        }

        [Test]
        public void Reset_ClearsSessionReplayState()
        {
            var tracker = new CommandSequenceTracker(2, 4);
            tracker.TryAccept(new SessionId(1), new CommandSequenceId(2));

            Assert.That(tracker.Reset(new SessionId(1)), Is.True);
            Assert.That(tracker.TryGetLastAccepted(new SessionId(1), out _), Is.False);
            Assert.That(tracker.TryAccept(new SessionId(1), new CommandSequenceId(1)).IsSuccess, Is.True);
        }

        [Test]
        public void Reset_UnknownSessionReturnsFalse()
        {
            var tracker = new CommandSequenceTracker(2, 4);
            Assert.That(tracker.Reset(new SessionId(1)), Is.False);
        }

        [Test]
        public void TryAccept_RejectsInvalidIdentifiers()
        {
            var tracker = new CommandSequenceTracker(2, 4);
            Result result = tracker.TryAccept(default, new CommandSequenceId(1));
            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(tracker.TryAccept(new SessionId(1), default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
        }

        [Test]
        public void TryGetLastAccepted_RejectsInvalidSession()
        {
            var tracker = new CommandSequenceTracker(2, 4);
            Assert.Throws<ArgumentException>(() => tracker.TryGetLastAccepted(default, out _));
        }

        [Test]
        public void Reset_RejectsInvalidSession()
        {
            var tracker = new CommandSequenceTracker(2, 4);
            Assert.Throws<ArgumentException>(() => tracker.Reset(default));
        }
    }
}
