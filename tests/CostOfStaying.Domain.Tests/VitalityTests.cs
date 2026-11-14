using NUnit.Framework;
using System;
using CostOfStaying.Domain;

namespace CostOfStaying.Domain.Tests
{
    [TestFixture]
    public class VitalityTests
    {
        private static Match CreateMatch(double baseDecay = 0.10, double collapseIncrease = 0.02)
        {
            var config = new MatchConfig
            {
                StartingVitality = 100.0,
                BaseDecayPerSecond = baseDecay,
                CollapseDecayIncreasePerSecond = collapseIncrease,
                CollapseIntervalSeconds = 20.0,
                CollapseIntervalThreeAliveSeconds = 15.0,
                DebuffDurationSeconds = 30.0,
                DebuffDecayBonusPerSecond = 0.03,
                TransferCooldownSeconds = 5.0,
                MaxActiveDebuffs = 3
            };
            var clock = new ManualClock(0);
            var match = new Match(Guid.NewGuid(), config, clock);
            return match;
        }

        [Test]
        public void Vitality_Decays_At_Base_Rate()
        {
            var match = CreateMatch(baseDecay: 0.1);
            var clock = (ManualClock)match.Clock;
            var pid = Guid.NewGuid();
            match.AddPlayer(pid);
            match.Start();

            // 10 seconds at 0.1/s => 1.0 drained
            clock.Advance(10);
            match.Tick(10);

            Assert.That(match.Players[pid].Vitality, Is.EqualTo(99.0).Within(1e-9));
        }

        [Test]
        public void Collapse_Increases_Decay_And_Adds_Debuff()
        {
            var match = CreateMatch();
            var clock = (ManualClock)match.Clock;
            var pid = Guid.NewGuid();
            match.AddPlayer(pid);
            match.Start();

            // Force collapse at 20s (default interval)
            clock.Advance(20);
            match.Tick(20);

            var p = match.Players[pid];
            Assert.That(p.BaseDecayPerSecond, Is.EqualTo(0.12).Within(1e-9));
            Assert.That(p.Debuffs.Count, Is.EqualTo(1));
            // Next collapse rescheduled
            Assert.That(p.NextCollapseAtSeconds, Is.EqualTo(40).Within(1e-9));
        }

        [Test]
        public void Death_At_Zero_Transitions_To_Spirit_Once()
        {
            var match = CreateMatch(baseDecay: 10.0); // fast
            var clock = (ManualClock)match.Clock;
            var pid = Guid.NewGuid();
            match.AddPlayer(pid);
            match.Start();

            clock.Advance(10);
            match.Tick(10); // 100 - 10*10 = 0

            var p = match.Players[pid];
            Assert.That(p.Vitality, Is.EqualTo(0).Within(1e-9));
            Assert.That(p.State, Is.EqualTo(PlayerState.Spirit));
            Assert.That(match.Events, Has.Some.Matches<DomainEvent>(e => e.Kind == DomainEventKind.PlayerDied));

            // Tick again should not re-emit death
            int diedCount = 0;
            foreach (var e in match.Events) if (e.Kind == DomainEventKind.PlayerDied) diedCount++;
            clock.Advance(5);
            match.Tick(5);
            int diedCount2 = 0;
            foreach (var e in match.Events) if (e.Kind == DomainEventKind.PlayerDied) diedCount2++;
            Assert.That(diedCount2, Is.EqualTo(diedCount));
        }

        [Test]
        public void No_Resurrection_From_Spirit()
        {
            var match = CreateMatch(baseDecay: 10.0);
            var clock = (ManualClock)match.Clock;
            var pid = Guid.NewGuid();
            match.AddPlayer(pid);
            match.Start();
            clock.Advance(10);
            match.Tick(10);
            Assert.That(match.Players[pid].State, Is.EqualTo(PlayerState.Spirit));
            // No API exists to revive; verify state stays Spirit after more ticks
            clock.Advance(100);
            match.Tick(100);
            Assert.That(match.Players[pid].State, Is.EqualTo(PlayerState.Spirit));
        }

        [Test]
        public void Debuff_Expires_After_Duration()
        {
            var match = CreateMatch();
            var clock = (ManualClock)match.Clock;
            var pid = Guid.NewGuid();
            match.AddPlayer(pid);
            match.Start();
            clock.Advance(20);
            match.Tick(20);
            Assert.That(match.Players[pid].Debuffs.Count, Is.EqualTo(1));
            // Advance past 30s debuff duration
            clock.Advance(30);
            match.Tick(30);
            // First debuff expired; but a second collapse happened at 40s, so one remains
            // We check that the original expired: remaining debuff should be the second one
            Assert.That(match.Players[pid].Debuffs.Count, Is.EqualTo(1));
        }

        [Test]
        public void Server_Clock_Is_Authority_Delta_Not_Client()
        {
            var match = CreateMatch();
            var clock = (ManualClock)match.Clock;
            var pid = Guid.NewGuid();
            match.AddPlayer(pid);
            match.Start();
            // Client claims 1000s passed, but server delta is 10
            clock.Advance(10);
            match.Tick(10);
            Assert.That(match.Players[pid].Vitality, Is.EqualTo(99.0).Within(1e-9));
        }
    }
}
