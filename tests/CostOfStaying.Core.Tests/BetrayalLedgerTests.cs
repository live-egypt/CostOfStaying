using NUnit.Framework;
using CostOfStaying.Core;
using System;

namespace CostOfStaying.Core.Tests
{
    [TestFixture]
    public class BetrayalLedgerTests
    {
        [Test]
        public void Record_And_Count()
        {
            var l = new BetrayalLedger();
            l.Record(1, 10, 20, 1, 5.0);
            l.Record(1, 10, 20, 2, 6.0);
            Assert.That(l.CountFor(1, 10), Is.EqualTo(2));
        }

        [Test]
        public void Zero_Sentinel_Rejected()
        {
            var l = new BetrayalLedger();
            Assert.Throws<ArgumentException>(() => l.Record(0, 10, 20, 1, 0));
            Assert.Throws<ArgumentException>(() => l.Record(1, 0, 20, 1, 0));
            Assert.Throws<ArgumentException>(() => l.Record(1, 10, 10, 1, 0));
        }

        [Test]
        public void Sequence_Gap_Rejected()
        {
            var l = new BetrayalLedger();
            l.Record(1, 10, 20, 1, 0);
            Assert.Throws<InvalidOperationException>(() => l.Record(1, 10, 20, 3, 0));
        }

        [Test]
        public void Idempotent_Replay_Same()
        {
            var l = new BetrayalLedger();
            var a = l.Record(1, 10, 20, 1, 5.0);
            var b = l.Record(1, 10, 20, 1, 5.0);
            Assert.That(b, Is.SameAs(a));
        }

        [Test]
        public void Conflicting_Replay_Throws()
        {
            var l = new BetrayalLedger();
            l.Record(1, 10, 20, 1, 5.0);
            Assert.Throws<InvalidOperationException>(() => l.Record(1, 99, 20, 1, 5.0));
        }
    }
}
