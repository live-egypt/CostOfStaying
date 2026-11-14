using NUnit.Framework;
using System;
using CostOfStaying.Domain;

namespace CostOfStaying.Domain.Tests
{
    [TestFixture]
    public class CollapseTests
    {
        [Test]
        public void Collapse_Reschedule_Preserves_Ratio_When_AliveCount_Changes()
        {
            var config = new MatchConfig
            {
                StartingVitality = 100,
                BaseDecayPerSecond = 0.1,
                CollapseDecayIncreasePerSecond = 0.02,
                CollapseIntervalSeconds = 20,
                CollapseIntervalThreeAliveSeconds = 15,
                DebuffDurationSeconds = 30,
                DebuffDecayBonusPerSecond = 0.03,
                PlayerCount = 4
            };
            var clock = new ManualClock(0);
            var match = new Match(Guid.NewGuid(), config, clock);
            var ids = new Guid[4];
            for (int i = 0; i < 4; i++) { ids[i] = Guid.NewGuid(); match.AddPlayer(ids[i]); }
            match.Start();

            // At start interval is 20 (alive=4), next at 20
            Assert.That(match.Players[ids[0]].NextCollapseAtSeconds, Is.EqualTo(20).Within(1e-9));

            // Advance 10s (50% remaining)
            clock.Advance(10);
            match.Tick(10);

            // Kill one player to make alive=3, should reschedule to preserve ratio
            // Manually set vitality to 0 for one player
            match.Players[ids[3]].Vitality = 0;
            clock.Advance(0.1);
            match.Tick(0.1); // triggers death and reschedule

            // Before death remaining was 10s of 20s => ratio 0.5, new interval 15 => 7.5s from now (~10.1)
            // So next collapse ~ 10.1 + 7.5 = 17.6
            double next = match.Players[ids[0]].NextCollapseAtSeconds;
            Assert.That(next, Is.EqualTo(17.6).Within(0.05));
            Assert.That(match.AliveCount(), Is.EqualTo(3));
        }

        [Test]
        public void Collapse_Does_Not_Kill_Instantly()
        {
            var config = new MatchConfig
            {
                StartingVitality = 100,
                BaseDecayPerSecond = 0.01,
                CollapseDecayIncreasePerSecond = 0.01,
                CollapseIntervalSeconds = 5,
                CollapseIntervalThreeAliveSeconds = 15,
                DebuffDurationSeconds = 30,
                DebuffDecayBonusPerSecond = 0.01,
                PlayerCount = 1
            };
            var clock = new ManualClock(0);
            var match = new Match(Guid.NewGuid(), config, clock);
            var pid = Guid.NewGuid();
            match.AddPlayer(pid);
            match.Start();

            clock.Advance(5);
            match.Tick(5);
            // Collapse applied but player still alive
            Assert.That(match.Players[pid].State, Is.EqualTo(PlayerState.Alive));
            Assert.That(match.Players[pid].Vitality, Is.GreaterThan(0));
        }

        [Test]
        public void Collapse_MaxDebuffs_Respected()
        {
            var config = new MatchConfig
            {
                StartingVitality = 100,
                BaseDecayPerSecond = 0.01,
                CollapseDecayIncreasePerSecond = 0.01,
                CollapseIntervalSeconds = 5,
                CollapseIntervalThreeAliveSeconds = 15,
                DebuffDurationSeconds = 1000, // long so they accumulate
                DebuffDecayBonusPerSecond = 0.01,
                MaxActiveDebuffs = 2,
                PlayerCount = 1
            };
            var clock = new ManualClock(0);
            var match = new Match(Guid.NewGuid(), config, clock);
            var pid = Guid.NewGuid();
            match.AddPlayer(pid);
            match.Start();

            for (int i = 0; i < 5; i++)
            {
                clock.Advance(5);
                match.Tick(5);
            }
            Assert.That(match.Players[pid].Debuffs.Count, Is.EqualTo(2));
        }
    }
}
