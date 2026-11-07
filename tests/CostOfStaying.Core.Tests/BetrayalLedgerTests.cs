namespace CostOfStaying.Core.Tests
{
    using System;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class BetrayalLedgerTests
    {
        private static BetrayalEvent CreateEvent(ulong sequence, ulong actor, ulong victim, ulong tick) =>
            new BetrayalEvent(new EventSequenceId(sequence), new ActorId(actor), new VictimId(victim), tick);

        [Test]
        public void Record_IncrementsOnlyTheActorScore()
        {
            var ledger = new BetrayalLedger();
            bool recorded = ledger.TryRecord(CreateEvent(1, 10, 20, 100));
            Assert.That(recorded, Is.True);
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.EqualTo(1UL));
            Assert.That(ledger.ScoreFor(new ActorId(20)), Is.Zero);
        }

        [Test]
        public void ExactReplay_ReturnsFalseAndDoesNotIncrementAgain()
        {
            var ledger = new BetrayalLedger();
            BetrayalEvent entry = CreateEvent(7, 10, 20, 100);
            Assert.That(ledger.TryRecord(entry), Is.True);
            Assert.That(ledger.TryRecord(entry), Is.False);
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.EqualTo(1UL));
        }

        [Test]
        public void ExactOldReplay_AfterNewerEventReturnsFalse()
        {
            var ledger = new BetrayalLedger();
            BetrayalEvent first = CreateEvent(7, 10, 20, 100);
            BetrayalEvent second = CreateEvent(8, 10, 21, 101);
            Assert.That(ledger.TryRecord(first), Is.True);
            Assert.That(ledger.TryRecord(second), Is.True);
            Assert.That(ledger.TryRecord(first), Is.False);
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.EqualTo(2UL));
        }

        [Test]
        public void ReusedSequenceWithDifferentPayload_ThrowsWithoutMutation()
        {
            var ledger = new BetrayalLedger();
            ledger.TryRecord(CreateEvent(7, 10, 20, 100));
            Assert.Throws<InvalidOperationException>(() => ledger.TryRecord(CreateEvent(7, 11, 20, 100)));
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.EqualTo(1UL));
            Assert.That(ledger.ScoreFor(new ActorId(11)), Is.Zero);
        }

        [Test]
        public void SequenceMustBeStrictlyIncreasing()
        {
            var ledger = new BetrayalLedger();
            ledger.TryRecord(CreateEvent(8, 10, 20, 100));
            Assert.Throws<InvalidOperationException>(() => ledger.TryRecord(CreateEvent(7, 10, 21, 101)));
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.EqualTo(1UL));
        }

        [Test]
        public void SameTickEventsAreAllowedWhenSequenceIncreases()
        {
            var ledger = new BetrayalLedger();
            Assert.That(ledger.TryRecord(CreateEvent(1, 10, 20, 100)), Is.True);
            Assert.That(ledger.TryRecord(CreateEvent(2, 10, 21, 100)), Is.True);
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.EqualTo(2UL));
        }

        [Test]
        public void ServerTickDoesNotDefineLedgerOrdering()
        {
            var ledger = new BetrayalLedger();
            Assert.That(ledger.TryRecord(CreateEvent(1, 10, 20, 100)), Is.True);
            Assert.That(ledger.TryRecord(CreateEvent(2, 10, 21, 50)), Is.True);
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.EqualTo(2UL));
        }

        [Test]
        public void Constructor_RejectsZeroSequence()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CreateEvent(0, 10, 20, 1));
        }

        [Test]
        public void Constructor_RejectsZeroActor()
        {
            Assert.Throws<ArgumentException>(() => CreateEvent(1, 0, 20, 1));
        }

        [Test]
        public void Constructor_RejectsZeroVictim()
        {
            Assert.Throws<ArgumentException>(() => CreateEvent(1, 10, 0, 1));
        }

        [Test]
        public void Constructor_RejectsSelfBetrayal()
        {
            Assert.Throws<ArgumentException>(() => CreateEvent(1, 10, 10, 1));
        }

        [Test]
        public void Ledger_RejectsDefaultEventStruct()
        {
            var ledger = new BetrayalLedger();
            Assert.Throws<ArgumentException>(() => ledger.TryRecord(default));
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.Zero);
        }

        [Test]
        public void ScoreFor_RejectsInvalidActorId()
        {
            var ledger = new BetrayalLedger();
            Assert.Throws<ArgumentException>(() => ledger.ScoreFor(new ActorId(0)));
        }

        [Test]
        public void MaximumSequenceCanBeRecorded()
        {
            var ledger = new BetrayalLedger();
            Assert.That(ledger.TryRecord(CreateEvent(ulong.MaxValue, 10, 20, 1)), Is.True);
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.EqualTo(1UL));
        }

        [Test]
        public void DifferentEventsWithOppositeDirectionAreDistinct()
        {
            var ledger = new BetrayalLedger();
            Assert.That(ledger.TryRecord(CreateEvent(1, 10, 20, 1)), Is.True);
            Assert.That(ledger.TryRecord(CreateEvent(2, 20, 10, 1)), Is.True);
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.EqualTo(1UL));
            Assert.That(ledger.ScoreFor(new ActorId(20)), Is.EqualTo(1UL));
        }
    }
}
