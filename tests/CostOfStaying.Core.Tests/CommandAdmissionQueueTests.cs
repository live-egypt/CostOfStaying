namespace CostOfStaying.Core.Tests
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CommandAdmissionQueueTests
    {
        private sealed class TestCommand
        {
            public TestCommand(int value) => Value = value;
            public int Value { get; }
        }

        private static CommandAdmissionQueue<TestCommand> CreateQueue(
            ulong maximumSequenceJump,
            int maximumTrackedSessions,
            int capacity)
        {
            return new CommandAdmissionQueue<TestCommand>(
                new CommandSequenceTracker(maximumSequenceJump, maximumTrackedSessions),
                capacity);
        }

        [Test]
        public void Constructor_RejectsZeroCapacity()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() =>
                new CommandAdmissionQueue<TestCommand>(new CommandSequenceTracker(4, 2), 0));
        }

        [Test]
        public void Constructor_RejectsInvalidSequenceConfiguration()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new CommandSequenceTracker(0, 2));
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new CommandSequenceTracker(4, 0));
        }

        [Test]
        public void TryEnqueue_RejectsDefaultEnvelope()
        {
            var queue = CreateQueue(4, 2, 2);
            Result result = queue.TryEnqueue(default);

            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryEnqueue_AndTryDequeue_PreserveFifoOrder()
        {
            var queue = CreateQueue(4, 2, 3);
            var first = new CommandEnvelope<TestCommand>(
                new SessionId(1), new CommandSequenceId(1), new ServerTick(5), new TestCommand(10));
            var second = new CommandEnvelope<TestCommand>(
                new SessionId(1), new CommandSequenceId(2), new ServerTick(6), new TestCommand(20));

            Assert.That(queue.TryEnqueue(first).IsSuccess, Is.True);
            Assert.That(queue.TryEnqueue(second).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(2));

            Assert.That(queue.TryDequeue(out var dequeuedFirst), Is.True);
            Assert.That(dequeuedFirst, Is.EqualTo(first));
            Assert.That(queue.TryDequeue(out var dequeuedSecond), Is.True);
            Assert.That(dequeuedSecond, Is.EqualTo(second));
            Assert.That(queue.TryDequeue(out _), Is.False);
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void FullQueue_RejectsWithoutConsumingSequence()
        {
            var queue = CreateQueue(4, 2, 1);
            var first = new CommandEnvelope<TestCommand>(
                new SessionId(1), new CommandSequenceId(1), new ServerTick(1), new TestCommand(1));
            var second = new CommandEnvelope<TestCommand>(
                new SessionId(1), new CommandSequenceId(2), new ServerTick(2), new TestCommand(2));

            Assert.That(queue.TryEnqueue(first).IsSuccess, Is.True);
            Assert.That(queue.TryEnqueue(second).ErrorCode, Is.EqualTo(CoreErrorCode.CapacityExceeded));
            Assert.That(queue.TryDequeue(out _), Is.True);
            Assert.That(queue.TryEnqueue(second).IsSuccess, Is.True);
        }

        [Test]
        public void DuplicateSequence_IsRejected()
        {
            var queue = CreateQueue(4, 2, 4);
            var envelope = new CommandEnvelope<TestCommand>(
                new SessionId(1), new CommandSequenceId(1), new ServerTick(1), new TestCommand(1));

            Assert.That(queue.TryEnqueue(envelope).IsSuccess, Is.True);
            Assert.That(queue.TryEnqueue(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.Conflict));
            Assert.That(queue.PendingCount, Is.EqualTo(1));
        }

        [Test]
        public void ExcessiveSequenceJump_IsRejectedWithoutQueueMutation()
        {
            var queue = CreateQueue(2, 2, 4);
            var envelope = new CommandEnvelope<TestCommand>(
                new SessionId(1), new CommandSequenceId(3), new ServerTick(1), new TestCommand(1));

            Assert.That(queue.TryEnqueue(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.OutOfRange));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void ResetSession_IsRejectedWhileCommandsRemainQueued()
        {
            var queue = CreateQueue(4, 1, 4);
            var envelope = new CommandEnvelope<TestCommand>(
                new SessionId(1), new CommandSequenceId(1), new ServerTick(1), new TestCommand(1));
            Assert.That(queue.TryEnqueue(envelope).IsSuccess, Is.True);

            Result result = queue.ResetSession(new SessionId(1));

            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(queue.PendingCount, Is.EqualTo(1));
        }

        [Test]
        public void ResetSession_ReleasesCapacityAfterQueueDrains()
        {
            var queue = CreateQueue(4, 1, 4);
            var first = new CommandEnvelope<TestCommand>(
                new SessionId(1), new CommandSequenceId(1), new ServerTick(1), new TestCommand(1));
            var second = new CommandEnvelope<TestCommand>(
                new SessionId(2), new CommandSequenceId(1), new ServerTick(2), new TestCommand(2));

            Assert.That(queue.TryEnqueue(first).IsSuccess, Is.True);
            Assert.That(queue.TryDequeue(out _), Is.True);
            Assert.That(queue.ResetSession(new SessionId(1)).IsSuccess, Is.True);
            Assert.That(queue.TryEnqueue(second).IsSuccess, Is.True);
        }

        [Test]
        public void ResetSession_RejectsInvalidOrUnknownSession()
        {
            var queue = CreateQueue(4, 1, 4);
            Assert.That(queue.ResetSession(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(queue.ResetSession(new SessionId(99)).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [Test]
        public void TrackedSessionCapacity_IsEnforcedAcrossQueueLifetime()
        {
            var queue = CreateQueue(4, 1, 4);
            var first = new CommandEnvelope<TestCommand>(
                new SessionId(1), new CommandSequenceId(1), new ServerTick(1), new TestCommand(1));
            var second = new CommandEnvelope<TestCommand>(
                new SessionId(2), new CommandSequenceId(1), new ServerTick(2), new TestCommand(2));

            Assert.That(queue.TryEnqueue(first).IsSuccess, Is.True);
            Assert.That(queue.TryDequeue(out _), Is.True);
            Assert.That(queue.TryEnqueue(second).ErrorCode, Is.EqualTo(CoreErrorCode.CapacityExceeded));
        }
    }
}
