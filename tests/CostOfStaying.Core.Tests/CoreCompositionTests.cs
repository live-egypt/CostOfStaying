namespace CostOfStaying.Core.Tests
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CoreCompositionTests
    {
        [Test]
        public void Create_ProvidesStableMatchScopedBetrayalLedger()
        {
            CoreComposition composition = CoreComposition.Create();

            Assert.That(composition.BetrayalLedger, Is.Not.Null);
            Assert.That(composition.BetrayalLedger, Is.SameAs(composition.BetrayalLedger));
        }

        [Test]
        public void SeparateCompositions_IsolateBetrayalLedgers()
        {
            CoreComposition first = CoreComposition.Create();
            CoreComposition second = CoreComposition.Create();

            Assert.That(first.BetrayalLedger, Is.Not.SameAs(second.BetrayalLedger));
        }

        [Test]
        public void CreateVitalityAccount_UsesRequestedIdentityAndMaximum()
        {
            CoreComposition composition = CoreComposition.Create();
            var account = composition.CreateVitalityAccount(new ActorId(7), 150);

            Assert.That(account.PlayerId, Is.EqualTo(new ActorId(7)));
            Assert.That(account.Current, Is.EqualTo(150u));
            Assert.That(account.Maximum, Is.EqualTo(150u));
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Alive));
        }

        [Test]
        public void CreateCommandSequenceTracker_UsesConfiguredMaximumJump()
        {
            CoreComposition composition = CoreComposition.Create();
            var tracker = composition.CreateCommandSequenceTracker(12);

            Assert.That(tracker.MaximumSequenceJump, Is.EqualTo(12UL));
        }

        [Test]
        public void CreateMatchLifecycle_UsesRequestedMatchId()
        {
            CoreComposition composition = CoreComposition.Create();
            var match = composition.CreateMatchLifecycle(new MatchId(9));

            Assert.That(match.MatchId, Is.EqualTo(new MatchId(9)));
            Assert.That(match.Phase, Is.EqualTo(MatchPhase.Created));
        }
    }
}
