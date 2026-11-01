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
            var tracker = composition.CreateCommandSequenceTracker(12, 16);

            Assert.That(tracker.MaximumSequenceJump, Is.EqualTo(12UL));
            Assert.That(tracker.MaximumTrackedSessions, Is.EqualTo(16));
        }

        [Test]
        public void CreateSessionLifecycle_UsesRequestedSessionId()
        {
            CoreComposition composition = CoreComposition.Create();
            var session = composition.CreateSessionLifecycle(new SessionId(77));

            Assert.That(session.SessionId, Is.EqualTo(new SessionId(77)));
            Assert.That(session.State, Is.EqualTo(SessionState.Pending));
        }

        [Test]
        public void CreateMatchPlayerRegistry_UsesRequestedMatchAndCapacity()
        {
            CoreComposition composition = CoreComposition.Create();
            var registry = composition.CreateMatchPlayerRegistry(new MatchId(8), 12);

            Assert.That(registry.MatchId, Is.EqualTo(new MatchId(8)));
            Assert.That(registry.Capacity, Is.EqualTo(12));
            Assert.That(registry.Count, Is.Zero);
        }

        [Test]
        public void CreateCommandAdmissionQueue_UsesExplicitBounds()
        {
            CoreComposition composition = CoreComposition.Create();
            var queue = composition.CreateCommandAdmissionQueue<string>(12, 16, 32);

            Assert.That(queue.Capacity, Is.EqualTo(32));
            Assert.That(queue.PendingCount, Is.Zero);
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
