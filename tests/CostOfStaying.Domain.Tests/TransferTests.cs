using NUnit.Framework;
using System;
using CostOfStaying.Domain;

namespace CostOfStaying.Domain.Tests
{
    [TestFixture]
    public class TransferTests
    {
        private static (Match match, ManualClock clock, Guid a, Guid b) SetupTwoPlayers()
        {
            var config = new MatchConfig
            {
                StartingVitality = 100,
                BaseDecayPerSecond = 0.1,
                CollapseDecayIncreasePerSecond = 0.02,
                CollapseIntervalSeconds = 1000, // avoid collapse interference
                CollapseIntervalThreeAliveSeconds = 1000,
                DebuffDurationSeconds = 30,
                DebuffDecayBonusPerSecond = 0.05,
                TransferCooldownSeconds = 5,
                MaxActiveDebuffs = 3,
                TransferRangeMeters = 2.0
            };
            var clock = new ManualClock(0);
            var match = new Match(Guid.NewGuid(), config, clock);
            var a = Guid.NewGuid();
            var b = Guid.NewGuid();
            match.AddPlayer(a);
            match.AddPlayer(b);
            match.Start();
            // Give A a debuff via manual injection for deterministic test
            match.Players[a].Debuffs.Add(new Debuff("test-debuff", 0.05, 30, true));
            return (match, clock, a, b);
        }

        [Test]
        public void Transfer_Moves_DeBuff_And_Increments_Betrayal()
        {
            var (match, clock, a, b) = SetupTwoPlayers();
            var result = match.TryTransfer(a, b, distanceMeters: 1.5, requestId: "req-1");
            Assert.That(result, Is.EqualTo(Match.TransferResult.Success));
            Assert.That(match.Players[a].Debuffs.Count, Is.EqualTo(0));
            Assert.That(match.Players[b].Debuffs.Count, Is.EqualTo(1));
            Assert.That(match.Players[b].Debuffs[0].Id, Is.EqualTo("test-debuff"));
            Assert.That(match.Players[a].BetrayalPoints, Is.EqualTo(1));
            Assert.That(match.Players[b].BetrayalPoints, Is.EqualTo(0));
        }

        [Test]
        public void Transfer_Preserves_Remaining_Duration()
        {
            var (match, clock, a, b) = SetupTwoPlayers();
            match.Players[a].Debuffs[0].RemainingSeconds = 12.5;
            match.TryTransfer(a, b, 1.0, "req-2");
            Assert.That(match.Players[b].Debuffs[0].RemainingSeconds, Is.EqualTo(12.5).Within(1e-9));
        }

        [Test]
        public void Transfer_Fails_OutOfRange_NoSideEffects()
        {
            var (match, clock, a, b) = SetupTwoPlayers();
            var debCountA = match.Players[a].Debuffs.Count;
            var betrayalA = match.Players[a].BetrayalPoints;
            var result = match.TryTransfer(a, b, distanceMeters: 2.5, requestId: "req-3");
            Assert.That(result, Is.EqualTo(Match.TransferResult.RejectedOutOfRange));
            Assert.That(match.Players[a].Debuffs.Count, Is.EqualTo(debCountA));
            Assert.That(match.Players[a].BetrayalPoints, Is.EqualTo(betrayalA));
        }

        [Test]
        public void Transfer_Fails_Cooldown_NoSideEffects()
        {
            var (match, clock, a, b) = SetupTwoPlayers();
            match.TryTransfer(a, b, 1.0, "req-4");
            // Give A another debuff
            match.Players[a].Debuffs.Add(new Debuff("second", 0.05, 30, true));
            // Immediate second attempt should fail cooldown
            var r2 = match.TryTransfer(a, b, 1.0, "req-5");
            Assert.That(r2, Is.EqualTo(Match.TransferResult.RejectedCooldown));
            Assert.That(match.Players[a].BetrayalPoints, Is.EqualTo(1)); // not incremented again
            Assert.That(match.Players[a].Debuffs.Count, Is.EqualTo(1)); // second debuff still there
        }

        [Test]
        public void Transfer_Fails_TargetFull()
        {
            var (match, clock, a, b) = SetupTwoPlayers();
            // Fill target
            match.Players[b].Debuffs.Add(new Debuff("x1", 0.01, 10, true));
            match.Players[b].Debuffs.Add(new Debuff("x2", 0.01, 10, true));
            match.Players[b].Debuffs.Add(new Debuff("x3", 0.01, 10, true));
            var result = match.TryTransfer(a, b, 1.0, "req-6");
            Assert.That(result, Is.EqualTo(Match.TransferResult.RejectedTargetFull));
            Assert.That(match.Players[a].BetrayalPoints, Is.EqualTo(0));
        }

        [Test]
        public void Transfer_Fails_SelfTarget()
        {
            var (match, clock, a, b) = SetupTwoPlayers();
            var result = match.TryTransfer(a, a, 0, "req-7");
            Assert.That(result, Is.EqualTo(Match.TransferResult.RejectedSelfTarget));
        }

        [Test]
        public void Transfer_Fails_NoTransferableDebuff()
        {
            var (match, clock, a, b) = SetupTwoPlayers();
            // Replace with non-transferable
            match.Players[a].Debuffs.Clear();
            match.Players[a].Debuffs.Add(new Debuff("nt", 0.02, 10, false));
            var result = match.TryTransfer(a, b, 1.0, "req-8");
            Assert.That(result, Is.EqualTo(Match.TransferResult.RejectedNoDebuff));
        }

        [Test]
        public void Transfer_Idempotency_DuplicateRequestId_NoDoubleApply()
        {
            var (match, clock, a, b) = SetupTwoPlayers();
            var r1 = match.TryTransfer(a, b, 1.0, "dup-id");
            Assert.That(r1, Is.EqualTo(Match.TransferResult.Success));
            // Re-add a debuff to source to test duplicate should not re-apply
            match.Players[a].Debuffs.Add(new Debuff("again", 0.05, 30, true));
            var r2 = match.TryTransfer(a, b, 1.0, "dup-id");
            Assert.That(r2, Is.EqualTo(Match.TransferResult.RejectedDuplicateRequest));
            // Betrayal not double-counted beyond first success
            Assert.That(match.Players[a].BetrayalPoints, Is.EqualTo(1));
            // Second debuff still on source (not moved)
            Assert.That(match.Players[a].Debuffs.Count, Is.EqualTo(1));
        }

        [Test]
        public void Transfer_LastTransferable_Is_Chosen()
        {
            var (match, clock, a, b) = SetupTwoPlayers();
            match.Players[a].Debuffs.Clear();
            match.Players[a].Debuffs.Add(new Debuff("first", 0.01, 10, true));
            match.Players[a].Debuffs.Add(new Debuff("second", 0.02, 10, true));
            match.Players[a].Debuffs.Add(new Debuff("third", 0.03, 10, false)); // non-transferable at end
            // Last transferable should be "second" (third is non-transferable, so skip it)
            var r = match.TryTransfer(a, b, 1.0, "req-last");
            Assert.That(r, Is.EqualTo(Match.TransferResult.Success));
            Assert.That(match.Players[b].Debuffs[0].Id, Is.EqualTo("second"));
            // first and third remain on source
            Assert.That(match.Players[a].Debuffs.Count, Is.EqualTo(2));
        }

        [Test]
        public void Transfer_Atomic_NoPartialOnFailure()
        {
            var (match, clock, a, b) = SetupTwoPlayers();
            // Kill target (make not alive)
            match.Players[b].State = PlayerState.Spirit;
            var beforeA = match.Players[a].Debuffs.Count;
            var beforeB = match.Players[b].Debuffs.Count;
            var result = match.TryTransfer(a, b, 1.0, "req-atomic");
            Assert.That(result, Is.EqualTo(Match.TransferResult.RejectedNotAlive));
            Assert.That(match.Players[a].Debuffs.Count, Is.EqualTo(beforeA));
            Assert.That(match.Players[b].Debuffs.Count, Is.EqualTo(beforeB));
        }
    }
}
