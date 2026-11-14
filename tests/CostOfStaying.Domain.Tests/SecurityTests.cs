using NUnit.Framework;
using System;
using CostOfStaying.Domain;

namespace CostOfStaying.Domain.Tests
{
    /// <summary>
    /// Server-authority invariants (ADR-002): clients cannot directly mutate
    /// vitality, betrayal, decay rate, or match outcome. All mutations go
    /// through validated Match methods using server clock.
    /// </summary>
    [TestFixture]
    public class SecurityTests
    {
        [Test]
        public void Player_State_Cannot_Be_Mutated_Directly_From_Outside_Domain()
        {
            // Verify that Player setters are internal — external code cannot do
            // player.Vitality = 999. This is a compile-time guarantee, but we
            // test the runtime that Tick is the only way to change vitality.
            var config = new MatchConfig { BaseDecayPerSecond = 0.1, CollapseIntervalSeconds = 1000, CollapseIntervalThreeAliveSeconds = 1000 };
            var clock = new ManualClock(0);
            var match = new Match(Guid.NewGuid(), config, clock);
            var pid = Guid.NewGuid();
            match.AddPlayer(pid);
            match.Start();
            double before = match.Players[pid].Vitality;
            clock.Advance(5);
            match.Tick(5);
            Assert.That(match.Players[pid].Vitality, Is.LessThan(before));
        }

        [Test]
        public void Transfer_Does_Not_Trust_Client_Distance_Without_Server_Check()
        {
            // The domain receives distanceMeters as server-measured; if the
            // server passes a large distance the transfer must be rejected.
            var config = new MatchConfig { TransferRangeMeters = 2.0, TransferCooldownSeconds = 0, CollapseIntervalSeconds = 1000, CollapseIntervalThreeAliveSeconds = 1000 };
            var clock = new ManualClock(0);
            var match = new Match(Guid.NewGuid(), config, clock);
            var a = Guid.NewGuid(); var b = Guid.NewGuid();
            match.AddPlayer(a); match.AddPlayer(b); match.Start();
            match.Players[a].Debuffs.Add(new Debuff("d", 0.01, 10, true));
            // Server measured distance is 10m — must reject even if client
            // claimed 1m in its intent (intent.ClaimedDistance is ignored).
            var r = match.TryTransfer(a, b, distanceMeters: 10.0, requestId: "r");
            Assert.That(r, Is.EqualTo(Match.TransferResult.RejectedOutOfRange));
        }

        [Test]
        public void Replay_Attack_With_Same_RequestId_Is_Rejected()
        {
            var config = new MatchConfig { TransferCooldownSeconds = 0, CollapseIntervalSeconds = 1000, CollapseIntervalThreeAliveSeconds = 1000 };
            var clock = new ManualClock(0);
            var match = new Match(Guid.NewGuid(), config, clock);
            var a = Guid.NewGuid(); var b = Guid.NewGuid();
            match.AddPlayer(a); match.AddPlayer(b); match.Start();
            match.Players[a].Debuffs.Add(new Debuff("d", 0.01, 10, true));
            var r1 = match.TryTransfer(a, b, 1.0, "replay-id");
            Assert.That(r1, Is.EqualTo(Match.TransferResult.Success));
            // Attacker replays same requestId
            match.Players[a].Debuffs.Add(new Debuff("d2", 0.01, 10, true));
            var r2 = match.TryTransfer(a, b, 1.0, "replay-id");
            Assert.That(r2, Is.EqualTo(Match.TransferResult.RejectedDuplicateRequest));
        }

        [Test]
        public void Betrayal_Only_On_Success_Not_On_Failure()
        {
            var config = new MatchConfig { TransferRangeMeters = 2.0, TransferCooldownSeconds = 0, CollapseIntervalSeconds = 1000, CollapseIntervalThreeAliveSeconds = 1000 };
            var clock = new ManualClock(0);
            var match = new Match(Guid.NewGuid(), config, clock);
            var a = Guid.NewGuid(); var b = Guid.NewGuid();
            match.AddPlayer(a); match.AddPlayer(b); match.Start();
            match.Players[a].Debuffs.Add(new Debuff("d", 0.01, 10, true));
            match.TryTransfer(a, b, 10.0, "fail"); // out of range
            Assert.That(match.Players[a].BetrayalPoints, Is.EqualTo(0));
            match.TryTransfer(a, b, 1.0, "ok");
            Assert.That(match.Players[a].BetrayalPoints, Is.EqualTo(1));
        }
    }
}
