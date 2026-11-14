namespace CostOfStaying.Core.Tests
{
    using CostOfStaying;
    using CostOfStaying.Core;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;
    using CostOfStaying.Core.Services;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ConnectionAwareSessionCleanupServiceTests
    {
        [Test]
        public void Constructor_RejectsNullDependencies()
        {
            var inner = CreateInner();
            var connections = new ConnectionSessionRegistry(new SessionRegistry(4), 4);

            Assert.Throws<System.ArgumentNullException>(() => new ConnectionAwareSessionCleanupService<string>(null!, connections));
            Assert.Throws<System.ArgumentNullException>(() => new ConnectionAwareSessionCleanupService<string>(inner, null!));
        }

        [Test]
        public void TryRemoveClosedSession_RejectsInvalidId()
        {
            var sessions = new SessionRegistry(4);
            var queue = new CommandAdmissionQueue<string>(4, 4, 4);
            var connections = new ConnectionSessionRegistry(sessions, 4);
            var cleanup = new ConnectionAwareSessionCleanupService<string>(
                new SessionCleanupService<string>(sessions, queue), connections);

            Assert.That(cleanup.TryRemoveClosedSession(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
        }

        [Test]
        public void Cleanup_IsBlockedUntilClosedSessionIsDetachedFromTransportConnection()
        {
            CoreComposition composition = CoreComposition.Create();
            var sessions = composition.CreateSessionRegistry(4);
            IConnectionSessionRegistry connections = composition.CreateConnectionSessionRegistry(sessions, 4);
            var queue = composition.CreateCommandAdmissionQueue<string>(8, 4, 4);
            ISessionCleanupService<string> cleanup = composition.CreateSessionCleanupService(sessions, queue, connections);
            var session = composition.CreateSessionLifecycle(new SessionId(10));
            sessions.Register(session);
            var connectionId = new TransportConnectionId(100);
            Assert.That(connections.TryBind(connectionId, session.SessionId).IsSuccess, Is.True);
            Assert.That(session.TryClose().IsSuccess, Is.True);

            Assert.That(cleanup.TryRemoveClosedSession(session.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(sessions.Get(session.SessionId).IsSuccess, Is.True);
            Assert.That(connections.IsSessionBound(session.SessionId), Is.True);

            Assert.That(connections.TryDetach(connectionId).IsSuccess, Is.True);
            Assert.That(connections.IsSessionBound(session.SessionId), Is.False);
            Assert.That(cleanup.TryRemoveClosedSession(session.SessionId).IsSuccess, Is.True);
            Assert.That(sessions.Get(session.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [Test]
        public void Cleanup_CanRemoveDisconnectedSessionAfterItIsClosedAndDetached()
        {
            CoreComposition composition = CoreComposition.Create();
            var sessions = composition.CreateSessionRegistry(4);
            IConnectionSessionRegistry connections = composition.CreateConnectionSessionRegistry(sessions, 4);
            var queue = composition.CreateCommandAdmissionQueue<string>(8, 4, 4);
            ISessionCleanupService<string> cleanup = composition.CreateSessionCleanupService(sessions, queue, connections);
            var session = composition.CreateSessionLifecycle(new SessionId(10));
            sessions.Register(session);
            var connectionId = new TransportConnectionId(100);
            connections.TryBind(connectionId, session.SessionId);
            session.TryAuthenticate(new ActorId(7));

            Assert.That(connections.TryDetach(connectionId).IsSuccess, Is.True);
            Assert.That(session.State, Is.EqualTo(SessionState.Disconnected));
            Assert.That(session.TryClose().IsSuccess, Is.True);
            Assert.That(cleanup.TryRemoveClosedSession(session.SessionId).IsSuccess, Is.True);
            Assert.That(sessions.Get(session.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        private static ISessionCleanupService<string> CreateInner()
        {
            var sessions = new SessionRegistry(4);
            var queue = new CommandAdmissionQueue<string>(4, 4, 4);
            return new SessionCleanupService<string>(sessions, queue);
        }
    }
}
