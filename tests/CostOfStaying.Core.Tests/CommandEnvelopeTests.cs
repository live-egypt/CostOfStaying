namespace CostOfStaying.Core.Tests
{
    using System;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CommandEnvelopeTests
    {
        [Test]
        public void Constructor_PreservesServerResolvedMetadataAndCommand()
        {
            var command = new TestCommand(17);
            var envelope = new CommandEnvelope<TestCommand>(
                new SessionId(3),
                new CommandSequenceId(9),
                new ServerTick(0),
                command);

            Assert.That(envelope.IsValid, Is.True);
            Assert.That(envelope.SessionId, Is.EqualTo(new SessionId(3)));
            Assert.That(envelope.SequenceId, Is.EqualTo(new CommandSequenceId(9)));
            Assert.That(envelope.ReceivedAt, Is.EqualTo(new ServerTick(0)));
            Assert.That(envelope.Command, Is.SameAs(command));
        }

        [Test]
        public void DefaultEnvelope_IsInvalid()
        {
            CommandEnvelope<TestCommand> envelope = default;
            Assert.That(envelope.IsValid, Is.False);
        }

        [Test]
        public void Constructor_RejectsInvalidSessionId()
        {
            Assert.Throws<ArgumentException>(() =>
                new CommandEnvelope<TestCommand>(default, new CommandSequenceId(1), new ServerTick(0), new TestCommand(1)));
        }

        [Test]
        public void Constructor_RejectsInvalidSequenceId()
        {
            Assert.Throws<ArgumentException>(() =>
                new CommandEnvelope<TestCommand>(new SessionId(1), default, new ServerTick(0), new TestCommand(1)));
        }

        [Test]
        public void Constructor_RejectsNullCommand()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CommandEnvelope<TestCommand>(new SessionId(1), new CommandSequenceId(1), new ServerTick(0), null!));
        }

        private sealed class TestCommand
        {
            public TestCommand(int value) => Value = value;
            public int Value { get; }
        }
    }
}
