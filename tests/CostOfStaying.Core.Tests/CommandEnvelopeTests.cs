using NUnit.Framework;
using CostOfStaying.Core;
using System;

namespace CostOfStaying.Core.Tests
{
    [TestFixture]
    public class CommandEnvelopeTests
    {
        [Test]
        public void ValidEnvelope()
        {
            var e = new CommandEnvelope(Guid.NewGuid(), 1, 10, 1, 5.0, "Transfer");
            Assert.That(e.Sequence, Is.EqualTo(1));
        }

        [Test]
        public void ZeroSentinel_Rejected()
        {
            Assert.Throws<ArgumentException>(() => new CommandEnvelope(Guid.NewGuid(), 0, 10, 1, 0, "Transfer"));
            Assert.Throws<ArgumentException>(() => new CommandEnvelope(Guid.Empty, 1, 10, 1, 0, "Transfer"));
        }

        [Test]
        public void SequenceValidator_Monotonic()
        {
            var v = new CommandSequenceValidator();
            var e1 = new CommandEnvelope(Guid.NewGuid(), 1, 10, 1, 0, "Transfer");
            var e2 = new CommandEnvelope(Guid.NewGuid(), 1, 10, 2, 1, "Transfer");
            var eOld = new CommandEnvelope(Guid.NewGuid(), 1, 10, 1, 2, "Transfer");

            Assert.That(v.TryAccept(e1, out _), Is.True);
            Assert.That(v.TryAccept(e2, out _), Is.True);
            Assert.That(v.TryAccept(eOld, out var reason), Is.False);
            Assert.That(reason, Does.Contain("sequence 1 <= last 2"));
        }

        [Test]
        public void SequenceValidator_Gap_Allows()
        {
            var v = new CommandSequenceValidator();
            Assert.That(v.TryAccept(new CommandEnvelope(Guid.NewGuid(), 1, 10, 1, 0, "A"), out _), Is.True);
            Assert.That(v.TryAccept(new CommandEnvelope(Guid.NewGuid(), 1, 10, 5, 1, "A"), out _), Is.True); // gap OK — increasing
            Assert.That(v.LastFor(10), Is.EqualTo(5));
        }
    }
}
