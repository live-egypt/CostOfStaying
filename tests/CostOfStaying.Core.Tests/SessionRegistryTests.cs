namespace CostOfStaying.Core.Tests
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class SessionRegistryTests
    {
        [Test]
        public void Constructor_RejectsNonPositiveCapacity()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new SessionRegistry(0));
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new SessionRegistry(-1));
        }

        [Test]
        public void Register_AddsPendingSessionAndGetReturnsSameInstance()
        {
            var registry = new SessionRegistry(4);
            var session = new SessionLifecycle(new SessionId(1));

            Result result = registry.Register(session);
            Result<ISessionLifecycle> get = registry.Get(new SessionId(1));

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(get.IsSuccess, Is.True);
            Assert.That(get.Value, Is.SameAs(session));
            Assert.That(registry.Count, Is.EqualTo(1));
        }

        [Test]
        public void Register_RejectsNullSession()
        {
            var registry = new SessionRegistry(4);
            Assert.That(registry.Register(null!).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(registry.Count, Is.Zero);
        }

        [Test]
        public void Register_RejectsDuplicateSessionIdWithoutReplacingSession()
        {
            var registry = new SessionRegistry(4);
            var first = new SessionLifecycle(new SessionId(1));
            var second = new SessionLifecycle(new SessionId(1));

            Assert.That(registry.Register(first).IsSuccess, Is.True);
            Assert.That(registry.Register(second).ErrorCode, Is.EqualTo(CoreErrorCode.Conflict));
            Assert.That(registry.Get(new SessionId(1)).Value, Is.SameAs(first));
            Assert.That(registry.Count, Is.EqualTo(1));
        }

        [Test]
        public void Register_RejectsSessionThatIsNotPending()
        {
            var registry = new SessionRegistry(4);
            var session = new SessionLifecycle(new SessionId(1));
            session.TryAuthenticate(new ActorId(7));

            Assert.That(registry.Register(session).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(registry.Count, Is.Zero);
        }

        [Test]
        public void Register_EnforcesExplicitCapacity()
        {
            var registry = new SessionRegistry(1);
            Assert.That(registry.Register(new SessionLifecycle(new SessionId(1))).IsSuccess, Is.True);
            Assert.That(registry.Register(new SessionLifecycle(new SessionId(2))).ErrorCode, Is.EqualTo(CoreErrorCode.CapacityExceeded));
            Assert.That(registry.Count, Is.EqualTo(1));
        }

        [Test]
        public void Get_RejectsInvalidAndUnknownSessionIds()
        {
            var registry = new SessionRegistry(4);
            Assert.That(registry.Get(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(registry.Get(new SessionId(99)).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [Test]
        public void Remove_RejectsSessionUntilItIsClosed()
        {
            var registry = new SessionRegistry(4);
            registry.Register(new SessionLifecycle(new SessionId(1)));

            Assert.That(registry.Remove(new SessionId(1)).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(registry.Count, Is.EqualTo(1));
            Assert.That(registry.Get(new SessionId(1)).IsSuccess, Is.True);
        }

        [Test]
        public void Remove_ClosedSessionReleasesCapacity()
        {
            var registry = new SessionRegistry(1);
            var session = new SessionLifecycle(new SessionId(1));
            registry.Register(session);
            session.TryClose();

            Assert.That(registry.Remove(new SessionId(1)).IsSuccess, Is.True);
            Assert.That(registry.Count, Is.Zero);
            Assert.That(registry.Register(new SessionLifecycle(new SessionId(2))).IsSuccess, Is.True);
        }

        [Test]
        public void Remove_RejectsInvalidAndUnknownSessionIds()
        {
            var registry = new SessionRegistry(4);
            Assert.That(registry.Remove(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(registry.Remove(new SessionId(99)).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [Test]
        public void Snapshot_PreservesRegistrationOrderAndIsImmutable()
        {
            var registry = new SessionRegistry(4);
            registry.Register(new SessionLifecycle(new SessionId(20)));
            registry.Register(new SessionLifecycle(new SessionId(10)));

            var snapshot = registry.GetSessionIdsSnapshot();

            Assert.That(snapshot, Is.EqualTo(new[] { new SessionId(20), new SessionId(10) }));
            Assert.Throws<System.NotSupportedException>(() =>
                ((System.Collections.Generic.IList<SessionId>)snapshot)[0] = new SessionId(99));
            Assert.That(registry.GetSessionIdsSnapshot()[0], Is.EqualTo(new SessionId(20)));
        }

        [Test]
        public void Registries_AreIndependent()
        {
            var first = new SessionRegistry(4);
            var second = new SessionRegistry(4);
            first.Register(new SessionLifecycle(new SessionId(1)));

            Assert.That(first.Count, Is.EqualTo(1));
            Assert.That(second.Count, Is.Zero);
        }
    }
}
