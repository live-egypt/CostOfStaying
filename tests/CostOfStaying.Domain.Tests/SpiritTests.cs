using NUnit.Framework;
using System;
using CostOfStaying.Domain;

namespace CostOfStaying.Domain.Tests
{
    [TestFixture]
    public class SpiritTests
    {
        private static (Match match, ManualClock clock, Guid alive, Guid spirit) Setup()
        {
            var config = new MatchConfig
            {
                StartingVitality = 100,
                BaseDecayPerSecond = 0.1,
                CollapseIntervalSeconds = 1000,
                CollapseIntervalThreeAliveSeconds = 1000,
                HintCooldownSeconds = 15,
                PlayerCount = 2
            };
            var clock = new ManualClock(0);
            var match = new Match(Guid.NewGuid(), config, clock);
            var alive = Guid.NewGuid();
            var dead = Guid.NewGuid();
            match.AddPlayer(alive);
            match.AddPlayer(dead);
            match.Start();
            // Kill one to make spirit
            match.Players[dead].Vitality = 0;
            clock.Advance(1);
            match.Tick(1);
            Assert.That(match.Players[dead].State, Is.EqualTo(PlayerState.Spirit));
            return (match, clock, alive, dead);
        }

        [Test]
        public void Spirit_CanCollectEcho_Once_Per_Id()
        {
            var (match, clock, alive, spirit) = Setup();
            Assert.That(match.CollectEcho(spirit, "echo-1"), Is.True);
            Assert.That(match.Players[spirit].EchoFragments, Is.EqualTo(1));
            Assert.That(match.Players[spirit].WitnessScore, Is.EqualTo(1));
            // Duplicate
            Assert.That(match.CollectEcho(spirit, "echo-1"), Is.False);
            Assert.That(match.Players[spirit].EchoFragments, Is.EqualTo(1));
        }

        [Test]
        public void Alive_CannotCollectEcho()
        {
            var (match, clock, alive, spirit) = Setup();
            Assert.That(match.CollectEcho(alive, "echo-1"), Is.False);
        }

        [Test]
        public void Spirit_Hint_Respects_Cooldown()
        {
            var (match, clock, alive, spirit) = Setup();
            Assert.That(match.TrySendHint(spirit, "hint1"), Is.True);
            // Immediate second should fail
            Assert.That(match.TrySendHint(spirit, "hint2"), Is.False);
            clock.Advance(15);
            match.Tick(15);
            Assert.That(match.TrySendHint(spirit, "hint3"), Is.True);
        }

        [Test]
        public void Spirit_CannotTransferOrVote()
        {
            var (match, clock, alive, spirit) = Setup();
            // Spirit cannot be source of transfer
            match.Players[spirit].Debuffs.Add(new Debuff("d", 0.01, 10, true));
            var r = match.TryTransfer(spirit, alive, 1.0, "req-spirit");
            Assert.That(r, Is.EqualTo(Match.TransferResult.RejectedNotAlive));
        }

        [Test]
        public void Spirit_Has_No_Vitality_Drain()
        {
            var (match, clock, alive, spirit) = Setup();
            double v = match.Players[spirit].Vitality;
            clock.Advance(100);
            match.Tick(100);
            Assert.That(match.Players[spirit].Vitality, Is.EqualTo(0).Within(1e-9));
        }
    }
}
