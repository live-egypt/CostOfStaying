using NUnit.Framework;
using System;
using CostOfStaying.Domain;

namespace CostOfStaying.Domain.Tests
{
    [TestFixture]
    public class VoteAndEndgameTests
    {
        private static (Match match, ManualClock clock, Guid[] ids) CreateFourPlayerMatch(double timeLimit = 1200)
        {
            var config = new MatchConfig
            {
                StartingVitality = 100,
                BaseDecayPerSecond = 0.01,
                CollapseDecayIncreasePerSecond = 0.01,
                CollapseIntervalSeconds = 1000,
                CollapseIntervalThreeAliveSeconds = 15,
                DebuffDurationSeconds = 30,
                DebuffDecayBonusPerSecond = 0.01,
                TransferCooldownSeconds = 5,
                VoteDurationSeconds = 20,
                MatchTimeLimitSeconds = timeLimit,
                PlayerCount = 4
            };
            var clock = new ManualClock(0);
            var match = new Match(Guid.NewGuid(), config, clock);
            var ids = new Guid[4];
            for (int i = 0; i < 4; i++) { ids[i] = Guid.NewGuid(); match.AddPlayer(ids[i]); }
            match.Start();
            return (match, clock, ids);
        }

        [Test]
        public void Vote_Opens_When_Alive_Becomes_Three()
        {
            var (match, clock, ids) = CreateFourPlayerMatch();
            // Kill one
            match.Players[ids[3]].Vitality = 0;
            clock.Advance(1);
            match.Tick(1);
            Assert.That(match.State, Is.EqualTo(MatchState.VoteOpen));
            Assert.That(match.VoteOpenedAtSeconds, Is.Not.Null);
            Assert.That(match.Votes.Count, Is.EqualTo(3));
        }

        [Test]
        public void Vote_Unanimous_Yes_Ends_As_CollectiveSurrender()
        {
            var (match, clock, ids) = CreateFourPlayerMatch();
            match.Players[ids[3]].Vitality = 0;
            clock.Advance(1);
            match.Tick(1);
            // Cast unanimous Yes
            match.CastVote(ids[0], VoteChoice.Yes);
            match.CastVote(ids[1], VoteChoice.Yes);
            match.CastVote(ids[2], VoteChoice.Yes);
            clock.Advance(20);
            match.Tick(20);
            Assert.That(match.Outcome, Is.EqualTo(MatchOutcome.CollectiveSurrender));
            Assert.That(match.State, Is.EqualTo(MatchState.Completed));
            Assert.That(match.WinnerId, Is.Null);
        }

        [Test]
        public void Vote_NonUnanimous_Returns_To_Running()
        {
            var (match, clock, ids) = CreateFourPlayerMatch();
            match.Players[ids[3]].Vitality = 0;
            clock.Advance(1);
            match.Tick(1);
            match.CastVote(ids[0], VoteChoice.Yes);
            match.CastVote(ids[1], VoteChoice.Yes);
            match.CastVote(ids[2], VoteChoice.No);
            clock.Advance(20);
            match.Tick(20);
            Assert.That(match.State, Is.EqualTo(MatchState.Running));
            Assert.That(match.Outcome, Is.EqualTo(MatchOutcome.None));
        }

        [Test]
        public void Vote_Abstain_Counts_As_No()
        {
            var (match, clock, ids) = CreateFourPlayerMatch();
            match.Players[ids[3]].Vitality = 0;
            clock.Advance(1);
            match.Tick(1);
            match.CastVote(ids[0], VoteChoice.Yes);
            match.CastVote(ids[1], VoteChoice.Yes);
            // ids[2] abstains (default)
            clock.Advance(20);
            match.Tick(20);
            Assert.That(match.State, Is.EqualTo(MatchState.Running));
        }

        [Test]
        public void Vote_Only_Once_Per_Match()
        {
            var (match, clock, ids) = CreateFourPlayerMatch();
            match.Players[ids[3]].Vitality = 0;
            clock.Advance(1);
            match.Tick(1);
            match.CastVote(ids[0], VoteChoice.No);
            match.CastVote(ids[1], VoteChoice.No);
            match.CastVote(ids[2], VoteChoice.No);
            clock.Advance(20);
            match.Tick(20);
            Assert.That(match.State, Is.EqualTo(MatchState.Running));
            // Kill another to get to 2 alive — vote should not reopen
            match.Players[ids[2]].Vitality = 0;
            clock.Advance(1);
            match.Tick(1);
            Assert.That(match.State, Is.EqualTo(MatchState.Running));
            Assert.That(match.VoteHasBeenOpened, Is.True);
        }

        [Test]
        public void Endgame_LastSurvivor()
        {
            var (match, clock, ids) = CreateFourPlayerMatch();
            // Kill 3
            for (int i = 1; i < 4; i++) match.Players[ids[i]].Vitality = 0;
            clock.Advance(1);
            match.Tick(1);
            Assert.That(match.Outcome, Is.EqualTo(MatchOutcome.LastSurvivor));
            Assert.That(match.WinnerId, Is.EqualTo(ids[0]));
        }

        [Test]
        public void Endgame_MutualCollapse_When_All_Die()
        {
            var (match, clock, ids) = CreateFourPlayerMatch();
            foreach (var id in ids) match.Players[id].Vitality = 0;
            clock.Advance(1);
            match.Tick(1);
            Assert.That(match.Outcome, Is.EqualTo(MatchOutcome.MutualCollapse));
        }

        [Test]
        public void Endgame_TimeLimit_HighestVitality_Wins()
        {
            var (match, clock, ids) = CreateFourPlayerMatch(timeLimit: 60);
            // Set distinct vitalities
            match.Players[ids[0]].Vitality = 50;
            match.Players[ids[1]].Vitality = 80;
            match.Players[ids[2]].Vitality = 60;
            match.Players[ids[3]].Vitality = 70;
            clock.Advance(60);
            match.Tick(60);
            Assert.That(match.Outcome, Is.EqualTo(MatchOutcome.TimeLimit));
            Assert.That(match.WinnerId, Is.EqualTo(ids[1]));
        }

        [Test]
        public void Endgame_Draw_When_Top_Tie()
        {
            var (match, clock, ids) = CreateFourPlayerMatch(timeLimit: 60);
            match.Players[ids[0]].Vitality = 80;
            match.Players[ids[1]].Vitality = 80;
            match.Players[ids[2]].Vitality = 60;
            match.Players[ids[3]].Vitality = 70;
            clock.Advance(60);
            match.Tick(60);
            Assert.That(match.Outcome, Is.EqualTo(MatchOutcome.Draw));
            Assert.That(match.WinnerId, Is.Null);
        }
    }
}
