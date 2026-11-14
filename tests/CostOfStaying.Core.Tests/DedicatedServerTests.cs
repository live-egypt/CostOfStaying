using NUnit.Framework;
using CostOfStaying.Core;

namespace CostOfStaying.Core.Tests
{
    [TestFixture]
    public class DedicatedServerTests
    {
        [Test]
        public void Create_And_Snapshot()
        {
            var clock = new ManualClock(0);
            var srv = new DedicatedServer(clock);
            var m = srv.CreateMatch(1);
            m.BindTransport(10, "conn-1");
            clock.Advance(1);
            srv.Tick(1.0/10);
            var snap = srv.Snapshot(1);
            Assert.That(snap.MatchId, Is.EqualTo(1));
            Assert.That(snap.SessionCount, Is.EqualTo(1));
        }

        [Test]
        public void BindTransport_Rejects_Duplicate_Connected()
        {
            var srv = new DedicatedServer(new ManualClock());
            var m = srv.CreateMatch(2);
            m.BindTransport(10, "c1");
            Assert.Throws<System.InvalidOperationException>(() => m.BindTransport(10, "c2"));
        }

        [Test]
        public void Zero_MatchId_Rejected()
        {
            var srv = new DedicatedServer(new ManualClock());
            Assert.Throws<System.ArgumentException>(() => srv.CreateMatch(0));
        }
    }
}
