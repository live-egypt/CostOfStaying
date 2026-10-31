namespace CostOfStaying.Core.Tests
{
    using System;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class BetrayalLedgerTests
    {
        [Test]
        public void Record_IncrementsOnlyTheActorScore()
        {
            var ledger = new BetrayalLedger();
            bool recorded = ledger.TryRecord(new BetrayalEvent(
                new EventSequenceId(1), new ActorId(10), new VictimId(20), 100));

            Assert.That(recorded, Is.True);
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.EqualTo(1UL));
            Assert.That(ledger.ScoreFor(new ActorId(20)), Is.Zero);
        }

        [Test]
        public void ExactReplay_ReturnsFalseAndDoesNotIncrementAgain()
        {
            var ledger = new BetrayalLedger();
            var entry = new BetrayalEvent(
                new EventSequenceId(7), new ActorId(10), new VictimId(20), 100);

            Assert.That(ledger.TryRecord(entry), Is.True);
            Assert.That(ledger.TryRecord(entry), Is.False);
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.EqualTo(1UL));
        }

        [Test]
        public void ReusedSequenceWithDifferentPayload_Throws()
        {
            var ledger = new BetrayalLedger();
            ledger.TryRecord(new BetrayalEvent(
                new EventSequenceId(7), new ActorId(10), new VictimId(20), 100));

            Assert.Throws<InvalidOperationException>(() => ledger.TryRecord(
                new BetrayalEvent(new EventSequenceId(7), new ActorId(11), new VictimId(20), 100)));
        }

        [Test]
        public void SequenceMustBeStrictlyIncreasing()
        {
            var ledger = new BetrayalLedger();
            ledger.TryRecord(new BetrayalEvent(
                new EventSequenceId(8), new ActorId(10), new VictimId(20), 100));

            Assert.Throws<InvalidOperationException>(() => ledger.TryRecord(
                new BetrayalEvent(new EventSequenceId(7), new ActorId(10), new VictimId(21), 101)));
        }

        [Test]
        public void SameTickEventsAreAllowedWhenSequenceIncreases()
        {
            var ledger = new BetrayalLedger();
            Assert.That(ledger.TryRecord(new BetrayalEvent(
                new EventSequenceId(1), new ActorId(10), new VictimId(20), 100)), Is.True);
            Assert.That(ledger.TryRecord(new BetrayalEvent(
                new EventSequenceId(2), new ActorId(10), new VictimId(21), 100)), Is.True);
            Assert.That(ledger.ScoreFor(new ActorId(10)), Is.EqualTo(2UL));
        }
    }
}
