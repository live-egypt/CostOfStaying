namespace CostOfStaying.Core.Tests
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class MatchPlayerRegistryTests
    {
        [Test]
        public void Constructor_RejectsInvalidMatchId()
        {
            Assert.Throws<System.ArgumentException>(() => new MatchPlayerRegistry(default, 4));
        }

        [Test]
        public void Constructor_RejectsNonPositiveCapacity()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new MatchPlayerRegistry(new MatchId(1), 0));
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new MatchPlayerRegistry(new MatchId(1), -1));
        }

        [Test]
        public void Register_AddsPlayerAndGetReturnsSameAccount()
        {
            var registry = new MatchPlayerRegistry(new MatchId(1), 4);
            var account = new VitalityAccount(new ActorId(10));

            Result result = registry.Register(account);
            Result<CostOfStaying.Core.Contracts.IVitalityAccount> get = registry.Get(new ActorId(10));

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(get.IsSuccess, Is.True);
            Assert.That(get.Value, Is.SameAs(account));
            Assert.That(registry.Count, Is.EqualTo(1));
        }

        [Test]
        public void Register_RejectsNullAccount()
        {
            var registry = new MatchPlayerRegistry(new MatchId(1), 4);
            Assert.That(registry.Register(null!).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(registry.Count, Is.Zero);
        }

        [Test]
        public void Register_RejectsDuplicateActorIdWithoutReplacingAccount()
        {
            var registry = new MatchPlayerRegistry(new MatchId(1), 4);
            var first = new VitalityAccount(new ActorId(10));
            var second = new VitalityAccount(new ActorId(10));

            Assert.That(registry.Register(first).IsSuccess, Is.True);
            Assert.That(registry.Register(second).ErrorCode, Is.EqualTo(CoreErrorCode.Conflict));
            Assert.That(registry.Get(new ActorId(10)).Value, Is.SameAs(first));
            Assert.That(registry.Count, Is.EqualTo(1));
        }

        [Test]
        public void Register_EnforcesConfiguredCapacity()
        {
            var registry = new MatchPlayerRegistry(new MatchId(1), 1);
            Assert.That(registry.Register(new VitalityAccount(new ActorId(10))).IsSuccess, Is.True);
            Assert.That(registry.Register(new VitalityAccount(new ActorId(20))).ErrorCode, Is.EqualTo(CoreErrorCode.CapacityExceeded));
            Assert.That(registry.Count, Is.EqualTo(1));
        }

        [Test]
        public void Get_RejectsInvalidAndUnknownActorIds()
        {
            var registry = new MatchPlayerRegistry(new MatchId(1), 4);
            Assert.That(registry.Get(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(registry.Get(new ActorId(99)).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [Test]
        public void Remove_ChangesMembershipOnly()
        {
            var registry = new MatchPlayerRegistry(new MatchId(1), 4);
            var account = new VitalityAccount(new ActorId(10));
            registry.Register(account);

            Result result = registry.Remove(new ActorId(10));

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(registry.Count, Is.Zero);
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Alive));
            Assert.That(registry.Get(new ActorId(10)).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [Test]
        public void Remove_RejectsInvalidAndUnknownActorIds()
        {
            var registry = new MatchPlayerRegistry(new MatchId(1), 4);
            Assert.That(registry.Remove(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(registry.Remove(new ActorId(99)).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [Test]
        public void Remove_ReleasesCapacity()
        {
            var registry = new MatchPlayerRegistry(new MatchId(1), 1);
            registry.Register(new VitalityAccount(new ActorId(10)));
            Assert.That(registry.Remove(new ActorId(10)).IsSuccess, Is.True);
            Assert.That(registry.Register(new VitalityAccount(new ActorId(20))).IsSuccess, Is.True);
        }

        [Test]
        public void Snapshot_PreservesRegistrationOrderAndIsImmutable()
        {
            var registry = new MatchPlayerRegistry(new MatchId(1), 4);
            registry.Register(new VitalityAccount(new ActorId(20)));
            registry.Register(new VitalityAccount(new ActorId(10)));

            var snapshot = registry.GetPlayerIdsSnapshot();

            Assert.That(snapshot, Is.EqualTo(new[] { new ActorId(20), new ActorId(10) }));
            Assert.Throws<System.NotSupportedException>(() =>
                ((System.Collections.Generic.IList<ActorId>)snapshot)[0] = new ActorId(99));
            Assert.That(registry.GetPlayerIdsSnapshot()[0], Is.EqualTo(new ActorId(20)));
        }

        [Test]
        public void Registries_AreMatchScopedAndIndependent()
        {
            var first = new MatchPlayerRegistry(new MatchId(1), 4);
            var second = new MatchPlayerRegistry(new MatchId(2), 4);
            first.Register(new VitalityAccount(new ActorId(10)));

            Assert.That(first.Count, Is.EqualTo(1));
            Assert.That(second.Count, Is.Zero);
            Assert.That(first.MatchId, Is.Not.EqualTo(second.MatchId));
        }
    }
}
