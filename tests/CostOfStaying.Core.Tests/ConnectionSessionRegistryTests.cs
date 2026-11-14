namespace CostOfStaying.Core.Tests
{
    using CostOfStaying;
    using CostOfStaying.Core;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ConnectionSessionRegistryTests
    {
        [Test]
        public void Constructor_RejectsNullSessionsAndNonPositiveCapacity()
        {
            Assert.Throws<System.ArgumentNullException>(() => new ConnectionSessionRegistry(null!, 4));
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new ConnectionSessionRegistry(new SessionRegistry(4), 0));
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new ConnectionSessionRegistry(new SessionRegistry(4), -1));
        }

        [Test]
        public void TryBind_RegistersBidirectionalMappingForPendingSession()
        {
            CreateRegistries(out SessionRegistry sessions, out ConnectionSessionRegistry connections);
            var session = RegisterPending(sessions, new SessionId(10));
            var connectionId = new TransportConnectionId(100);

            Result result = connections.TryBind(connectionId, session.SessionId);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(connections.Count, Is.EqualTo(1));
            Assert.That(connections.GetSessionId(connectionId).Value, Is.EqualTo(session.SessionId));
            Assert.That(connections.GetConnectionId(session.SessionId).Value, Is.EqualTo(connectionId));
            Assert.That(connections.IsSessionBound(session.SessionId), Is.True);
            Assert.That(session.State, Is.EqualTo(SessionState.Pending));
        }

        [Test]
        public void TryBind_RejectsInvalidIdentifiersWithoutMutation()
        {
            CreateRegistries(out SessionRegistry sessions, out ConnectionSessionRegistry connections);
            RegisterPending(sessions, new SessionId(10));

            Assert.That(connections.TryBind(default, new SessionId(10)).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(connections.TryBind(new TransportConnectionId(100), default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(connections.Count, Is.Zero);
        }

        [Test]
        public void TryBind_RejectsUnknownSession()
        {
            CreateRegistries(out _, out ConnectionSessionRegistry connections);

            Assert.That(
                connections.TryBind(new TransportConnectionId(100), new SessionId(99)).ErrorCode,
                Is.EqualTo(CoreErrorCode.NotFound));
            Assert.That(connections.Count, Is.Zero);
        }

        [Test]
        public void TryBind_RequiresRegisteredPendingSession()
        {
            CreateRegistries(out SessionRegistry sessions, out ConnectionSessionRegistry connections);
            var session = RegisterPending(sessions, new SessionId(10));
            Assert.That(session.TryAuthenticate(new ActorId(7)).IsSuccess, Is.True);

            Assert.That(
                connections.TryBind(new TransportConnectionId(100), session.SessionId).ErrorCode,
                Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(connections.Count, Is.Zero);
            Assert.That(session.State, Is.EqualTo(SessionState.Authenticated));
        }

        [Test]
        public void TryBind_RejectsDuplicateConnectionAndDuplicateSessionWithoutChangingExistingBinding()
        {
            CreateRegistries(out SessionRegistry sessions, out ConnectionSessionRegistry connections);
            var first = RegisterPending(sessions, new SessionId(10));
            RegisterPending(sessions, new SessionId(11));
            var firstConnection = new TransportConnectionId(100);

            Assert.That(connections.TryBind(firstConnection, first.SessionId).IsSuccess, Is.True);
            Assert.That(
                connections.TryBind(firstConnection, new SessionId(11)).ErrorCode,
                Is.EqualTo(CoreErrorCode.Conflict));
            Assert.That(
                connections.TryBind(new TransportConnectionId(101), first.SessionId).ErrorCode,
                Is.EqualTo(CoreErrorCode.Conflict));

            Assert.That(connections.Count, Is.EqualTo(1));
            Assert.That(connections.GetSessionId(firstConnection).Value, Is.EqualTo(first.SessionId));
            Assert.That(connections.GetConnectionId(first.SessionId).Value, Is.EqualTo(firstConnection));
            Assert.That(connections.GetSessionId(new TransportConnectionId(101)).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [Test]
        public void TryBind_EnforcesExplicitConnectionCapacity()
        {
            var sessions = new SessionRegistry(4);
            var connections = new ConnectionSessionRegistry(sessions, 1);
            var first = RegisterPending(sessions, new SessionId(1));
            var second = RegisterPending(sessions, new SessionId(2));

            Assert.That(connections.TryBind(new TransportConnectionId(1), first.SessionId).IsSuccess, Is.True);
            Assert.That(
                connections.TryBind(new TransportConnectionId(2), second.SessionId).ErrorCode,
                Is.EqualTo(CoreErrorCode.CapacityExceeded));
            Assert.That(connections.Count, Is.EqualTo(1));
            Assert.That(connections.IsSessionBound(second.SessionId), Is.False);
        }

        [Test]
        public void GetMethods_RejectInvalidAndUnknownIdentifiers()
        {
            CreateRegistries(out _, out ConnectionSessionRegistry connections);

            Assert.That(connections.GetSessionId(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(connections.GetConnectionId(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(connections.GetSessionId(new TransportConnectionId(99)).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
            Assert.That(connections.GetConnectionId(new SessionId(99)).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [Test]
        public void GetMethods_RefuseDisconnectedOrClosedBindings()
        {
            CreateRegistries(out SessionRegistry sessions, out ConnectionSessionRegistry connections);
            var session = RegisterPending(sessions, new SessionId(10));
            var connectionId = new TransportConnectionId(100);
            connections.TryBind(connectionId, session.SessionId);
            session.TryAuthenticate(new ActorId(7));
            session.TryDisconnect();

            Assert.That(connections.GetSessionId(connectionId).ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(connections.GetConnectionId(session.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(connections.IsSessionBound(session.SessionId), Is.True);
        }

        [Test]
        public void TryDetach_ClosesPendingSessionAndRemovesBothDirections()
        {
            CreateRegistries(out SessionRegistry sessions, out ConnectionSessionRegistry connections);
            var session = RegisterPending(sessions, new SessionId(10));
            var connectionId = new TransportConnectionId(100);
            connections.TryBind(connectionId, session.SessionId);

            Result<SessionId> detached = connections.TryDetach(connectionId);

            Assert.That(detached.IsSuccess, Is.True);
            Assert.That(detached.Value, Is.EqualTo(session.SessionId));
            Assert.That(session.State, Is.EqualTo(SessionState.Closed));
            Assert.That(sessions.Get(session.SessionId).IsSuccess, Is.True);
            Assert.That(connections.IsSessionBound(session.SessionId), Is.False);
            Assert.That(connections.GetSessionId(connectionId).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
            Assert.That(connections.GetConnectionId(session.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
            Assert.That(connections.Count, Is.Zero);
        }

        [Test]
        public void TryDetach_DisconnectsAuthenticatedSessionBeforeRemovingMapping()
        {
            CreateRegistries(out SessionRegistry sessions, out ConnectionSessionRegistry connections);
            var session = RegisterPending(sessions, new SessionId(10));
            var connectionId = new TransportConnectionId(100);
            connections.TryBind(connectionId, session.SessionId);
            session.TryAuthenticate(new ActorId(7));

            Result<SessionId> detached = connections.TryDetach(connectionId);

            Assert.That(detached.IsSuccess, Is.True);
            Assert.That(detached.Value, Is.EqualTo(session.SessionId));
            Assert.That(session.State, Is.EqualTo(SessionState.Disconnected));
            Assert.That(session.ActorId, Is.EqualTo(new ActorId(7)));
            Assert.That(sessions.Get(session.SessionId).IsSuccess, Is.True);
            Assert.That(connections.IsSessionBound(session.SessionId), Is.False);
        }

        [Test]
        public void TryDetach_RemovesAlreadyClosedOrDisconnectedSessionBinding()
        {
            CreateRegistries(out SessionRegistry sessions, out ConnectionSessionRegistry connections);
            var closed = RegisterPending(sessions, new SessionId(10));
            var disconnected = RegisterPending(sessions, new SessionId(11));
            connections.TryBind(new TransportConnectionId(100), closed.SessionId);
            connections.TryBind(new TransportConnectionId(101), disconnected.SessionId);
            closed.TryClose();
            disconnected.TryAuthenticate(new ActorId(7));
            disconnected.TryDisconnect();

            Assert.That(connections.TryDetach(new TransportConnectionId(100)).IsSuccess, Is.True);
            Assert.That(connections.TryDetach(new TransportConnectionId(101)).IsSuccess, Is.True);
            Assert.That(closed.State, Is.EqualTo(SessionState.Closed));
            Assert.That(disconnected.State, Is.EqualTo(SessionState.Disconnected));
            Assert.That(connections.Count, Is.Zero);
        }

        [Test]
        public void TryDetach_RejectsInvalidOrUnknownConnectionWithoutMutation()
        {
            CreateRegistries(out SessionRegistry sessions, out ConnectionSessionRegistry connections);
            var session = RegisterPending(sessions, new SessionId(10));
            var connectionId = new TransportConnectionId(100);
            connections.TryBind(connectionId, session.SessionId);

            Assert.That(connections.TryDetach(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(connections.TryDetach(new TransportConnectionId(999)).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
            Assert.That(connections.Count, Is.EqualTo(1));
            Assert.That(session.State, Is.EqualTo(SessionState.Pending));
        }

        [Test]
        public void DifferentSessionsForSameActor_AreNotRejectedByConnectionRegistry()
        {
            CreateRegistries(out SessionRegistry sessions, out ConnectionSessionRegistry connections);
            var first = RegisterPending(sessions, new SessionId(10));
            var second = RegisterPending(sessions, new SessionId(11));

            Assert.That(connections.TryBind(new TransportConnectionId(100), first.SessionId).IsSuccess, Is.True);
            Assert.That(connections.TryBind(new TransportConnectionId(101), second.SessionId).IsSuccess, Is.True);
            Assert.That(first.TryAuthenticate(new ActorId(7)).IsSuccess, Is.True);
            Assert.That(second.TryAuthenticate(new ActorId(7)).IsSuccess, Is.True);

            Assert.That(connections.GetSessionId(new TransportConnectionId(100)).Value, Is.EqualTo(first.SessionId));
            Assert.That(connections.GetSessionId(new TransportConnectionId(101)).Value, Is.EqualTo(second.SessionId));
            Assert.That(connections.Count, Is.EqualTo(2));
        }

        [Test]
        public void Detach_MakesPreviouslyQueuedCommandFailDequeueAuthenticationRevalidation()
        {
            CoreComposition composition = CoreComposition.Create();
            var sessions = composition.CreateSessionRegistry(4);
            IConnectionSessionRegistry connections = composition.CreateConnectionSessionRegistry(sessions, 4);
            var queue = composition.CreateCommandAdmissionQueue<string>(8, 4, 4);
            ICommandAdmissionService<string> admission = composition.CreateCommandAdmissionService(sessions, queue);
            var session = composition.CreateSessionLifecycle(new SessionId(10));
            sessions.Register(session);
            var connectionId = new TransportConnectionId(100);
            Assert.That(connections.TryBind(connectionId, session.SessionId).IsSuccess, Is.True);
            session.TryAuthenticate(new ActorId(7));

            var envelope = new CommandEnvelope<string>(session.SessionId, new CommandSequenceId(1), new ServerTick(1), "move");
            Assert.That(admission.TryAdmit(envelope).IsSuccess, Is.True);
            Assert.That(connections.TryDetach(connectionId).IsSuccess, Is.True);

            Assert.That(admission.TryTakeNext().ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        private static void CreateRegistries(out SessionRegistry sessions, out ConnectionSessionRegistry connections)
        {
            sessions = new SessionRegistry(8);
            connections = new ConnectionSessionRegistry(sessions, 8);
        }

        private static SessionLifecycle RegisterPending(SessionRegistry sessions, SessionId sessionId)
        {
            var session = new SessionLifecycle(sessionId);
            Assert.That(sessions.Register(session).IsSuccess, Is.True);
            return session;
        }
    }
}
