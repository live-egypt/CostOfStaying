namespace CostOfStaying.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class MatchRosterSnapshotBuilderTests
    {
        [Test]
        public void Constructor_RejectsNullDependenciesAndMismatchedMatchIds()
        {
            CoreComposition composition = CoreComposition.Create();
            var match = composition.CreateMatchLifecycle(new MatchId(1));
            var roster = composition.CreateMatchPlayerRegistry(new MatchId(1), 4);

            Assert.Throws<ArgumentNullException>(() =>
                composition.CreateMatchRosterSnapshotBuilder(null!, roster));
            Assert.Throws<ArgumentNullException>(() =>
                composition.CreateMatchRosterSnapshotBuilder(match, null!));
            Assert.Throws<ArgumentException>(() =>
                composition.CreateMatchRosterSnapshotBuilder(
                    match,
                    composition.CreateMatchPlayerRegistry(new MatchId(2), 4)));
        }

        [Test]
        public void Build_ProducesValidEmptySnapshotAndPreservesMetadata()
        {
            CoreComposition composition = CoreComposition.Create();
            var match = composition.CreateMatchLifecycle(new MatchId(1));
            var roster = composition.CreateMatchPlayerRegistry(new MatchId(1), 4);
            IMatchRosterSnapshotBuilder builder = composition.CreateMatchRosterSnapshotBuilder(match, roster);

            Result<MatchRosterSnapshot> result = builder.Build(new ServerTick(0), new StateRevision(0));

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value.MatchId, Is.EqualTo(new MatchId(1)));
            Assert.That(result.Value.Phase, Is.EqualTo(MatchPhase.Created));
            Assert.That(result.Value.ServerTick, Is.EqualTo(new ServerTick(0)));
            Assert.That(result.Value.Revision, Is.EqualTo(new StateRevision(0)));
            Assert.That(result.Value.PlayerCount, Is.Zero);
        }

        [Test]
        public void Build_PreservesRosterOrderAndCapturesAdr007PlayerStates()
        {
            CoreComposition composition = CoreComposition.Create();
            var match = composition.CreateMatchLifecycle(new MatchId(5));
            var roster = composition.CreateMatchPlayerRegistry(new MatchId(5), 4);
            var spirit = composition.CreateVitalityAccount(new ActorId(20), 100);
            spirit.Lose(100);
            var alive = composition.CreateVitalityAccount(new ActorId(10), 75);
            var eliminated = composition.CreateVitalityAccount(new ActorId(30), 100);
            eliminated.TryEliminate();

            Assert.That(roster.Register(spirit).IsSuccess, Is.True);
            Assert.That(roster.Register(alive).IsSuccess, Is.True);
            Assert.That(roster.Register(eliminated).IsSuccess, Is.True);

            IMatchRosterSnapshotBuilder builder = composition.CreateMatchRosterSnapshotBuilder(match, roster);
            Result<MatchRosterSnapshot> result = builder.Build(new ServerTick(12), new StateRevision(9));

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value.PlayerCount, Is.EqualTo(3));
            Assert.That(result.Value.Players[0].ActorId, Is.EqualTo(new ActorId(20)));
            Assert.That(result.Value.Players[0].State, Is.EqualTo(PlayerLifecycleState.Spirit));
            Assert.That(result.Value.Players[0].CurrentVitality, Is.Zero);
            Assert.That(result.Value.Players[1].ActorId, Is.EqualTo(new ActorId(10)));
            Assert.That(result.Value.Players[1].State, Is.EqualTo(PlayerLifecycleState.Alive));
            Assert.That(result.Value.Players[1].CurrentVitality, Is.EqualTo(75u));
            Assert.That(result.Value.Players[1].MaximumVitality, Is.EqualTo(75u));
            Assert.That(result.Value.Players[2].ActorId, Is.EqualTo(new ActorId(30)));
            Assert.That(result.Value.Players[2].State, Is.EqualTo(PlayerLifecycleState.Eliminated));
            Assert.That(result.Value.Phase, Is.EqualTo(MatchPhase.Created));
        }

        [Test]
        public void Build_ReturnsImmutableSnapshotUnaffectedByLaterDomainMutation()
        {
            CoreComposition composition = CoreComposition.Create();
            var match = composition.CreateMatchLifecycle(new MatchId(7));
            var roster = composition.CreateMatchPlayerRegistry(new MatchId(7), 2);
            var account = composition.CreateVitalityAccount(new ActorId(1), 100);
            roster.Register(account);
            IMatchRosterSnapshotBuilder builder = composition.CreateMatchRosterSnapshotBuilder(match, roster);

            MatchRosterSnapshot snapshot = builder.Build(new ServerTick(3), new StateRevision(4)).Value;
            account.Lose(25);

            Assert.That(snapshot.Players[0].CurrentVitality, Is.EqualTo(100u));
            Assert.Throws<NotSupportedException>(() =>
                ((IList<PlayerStateSnapshot>)snapshot.Players)[0] = default);
        }

        [Test]
        public void Build_RejectsLifecycleAndVitalityInconsistency()
        {
            CoreComposition composition = CoreComposition.Create();
            var match = composition.CreateMatchLifecycle(new MatchId(8));
            var roster = composition.CreateMatchPlayerRegistry(new MatchId(8), 2);
            roster.Register(new InvalidVitalityAccount(new ActorId(9)));
            IMatchRosterSnapshotBuilder builder = composition.CreateMatchRosterSnapshotBuilder(match, roster);

            Result<MatchRosterSnapshot> result = builder.Build(new ServerTick(1), new StateRevision(1));

            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
        }

        [Test]
        public void Build_RejectsInvalidMatchPhase()
        {
            var match = new InvalidMatchLifecycle(new MatchId(9));
            var roster = new MatchPlayerRegistry(new MatchId(9), 2);
            var builder = new MatchRosterSnapshotBuilder(match, roster);

            Assert.That(builder.Build(new ServerTick(0), new StateRevision(0)).ErrorCode,
                Is.EqualTo(CoreErrorCode.InvalidState));
        }

        private sealed class InvalidVitalityAccount : IVitalityAccount
        {
            public InvalidVitalityAccount(ActorId playerId) => PlayerId = playerId;
            public ActorId PlayerId { get; }
            public PlayerLifecycleState State => PlayerLifecycleState.Alive;
            public uint Current => 0;
            public uint Maximum => 100;
            public void Lose(uint amount) { }
            public void Restore(uint amount) { }
            public bool TryResurrect(uint requestedVitality) => false;
            public bool TryEliminate() => false;
        }

        private sealed class InvalidMatchLifecycle : IMatchLifecycle
        {
            public InvalidMatchLifecycle(MatchId matchId) => MatchId = matchId;
            public MatchId MatchId { get; }
            public MatchPhase Phase => (MatchPhase)999;
            public Result TryTransition(MatchPhase nextPhase) => Result.Failure(CoreErrorCode.InvalidState);
        }
    }
}
