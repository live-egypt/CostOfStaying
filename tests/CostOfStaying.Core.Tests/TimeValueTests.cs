namespace CostOfStaying.Core.Tests
{
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class TimeValueTests
    {
        [Test]
        public void ServerTick_ZeroIsAValidInitialTick()
        {
            var tick = new ServerTick(0);
            Assert.That(tick.Value, Is.Zero);
            Assert.That(tick, Is.EqualTo(new ServerTick(0)));
        }

        [Test]
        public void ServerTick_OrdersNumerically()
        {
            Assert.That(new ServerTick(2) > new ServerTick(1), Is.True);
            Assert.That(new ServerTick(1) < new ServerTick(2), Is.True);
            Assert.That(new ServerTick(2) >= new ServerTick(2), Is.True);
        }

        [Test]
        public void StateRevision_ZeroIsInitialRevision()
        {
            var revision = new StateRevision(0);
            Assert.That(revision.Value, Is.Zero);
        }

        [Test]
        public void StateRevision_TryAdvanceIncrementsExactlyOnce()
        {
            var revision = new StateRevision(41);
            Assert.That(revision.TryAdvance(out StateRevision next), Is.True);
            Assert.That(next.Value, Is.EqualTo(42UL));
            Assert.That(revision.Value, Is.EqualTo(41UL));
        }

        [Test]
        public void StateRevision_TryAdvanceRejectsOverflowWithoutMutation()
        {
            var revision = new StateRevision(ulong.MaxValue);
            Assert.That(revision.TryAdvance(out StateRevision next), Is.False);
            Assert.That(next, Is.EqualTo(revision));
            Assert.That(revision.Value, Is.EqualTo(ulong.MaxValue));
        }
    }
}
