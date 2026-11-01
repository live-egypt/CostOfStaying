namespace CostOfStaying.Core.Tests
{
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;
    using CostOfStaying.Core.Services;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CommandAdmissionServiceTests
    {
        private const ulong MaximumSequenceJump = 8UL;

        private sealed class MalformedAuthenticatedSession : ISessionLifecycle
        {
            public MalformedAuthenticatedSession(SessionId sessionId)
            {
                SessionId = sessionId;
                State = SessionState.Pending;
            }

            public SessionId SessionId { get; }
            public SessionState State { get; set; }
            public ActorId? ActorId => null;

            public Result TryAuthenticate(ActorId actorId) => Result.Failure(CoreErrorCode.InvalidState);
            public Result TryDisconnect() => Result.Failure(CoreErrorCode.InvalidState);
            public Result TryClose() => Result.Failure(CoreErrorCode.InvalidState);
        }

        [Test]
        public void Constructor_RejectsNullDependencies()
        {
            var queue = new CommandAdmissionQueue<string>(MaximumSequenceJump, 4, 4);
            var sessions = new SessionRegistry(4);
            Assert.Throws<System.ArgumentNullException>(() => new CommandAdmissionService<string>(null!, queue));
            Assert.Throws<System.ArgumentNullException>(() => new CommandAdmissionService<string>(sessions, null!));
        }

        [Test]
        public void TryAdmit_RejectsDefaultEnvelope()
        {
            var service = CreateService(out _, out _);
            Assert.That(service.TryAdmit(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
        }

        [Test]
        public void TryAdmit_RejectsUnknownSession()
        {
            var service = CreateService(out _, out _);
            var envelope = new CommandEnvelope<string>(new SessionId(10), new CommandSequenceId(1), new ServerTick(0), "move");
            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [TestCase(SessionState.Pending)]
        [TestCase(SessionState.Disconnected)]
        [TestCase(SessionState.Closed)]
        public void TryAdmit_RejectsSessionsThatAreNotAuthenticated(SessionState state)
        {
            var service = CreateService(out SessionRegistry sessions, out CommandAdmissionQueue<string> queue);
            var session = new SessionLifecycle(new SessionId(10));
            sessions.Register(session);
            if (state == SessionState.Disconnected)
            {
                session.TryAuthenticate(new ActorId(1));
                session.TryDisconnect();
            }
            else if (state == SessionState.Closed)
            {
                session.TryClose();
            }
            var envelope = new CommandEnvelope<string>(session.SessionId, new CommandSequenceId(1), new ServerTick(0), "move");
            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryAdmit_RejectsAuthenticatedSessionWithoutValidActorBinding()
        {
            var sessions = new SessionRegistry(4);
            var queue = new CommandAdmissionQueue<string>(MaximumSequenceJump, 4, 4);
            var malformed = new MalformedAuthenticatedSession(new SessionId(10));
            sessions.Register(malformed);
            malformed.State = SessionState.Authenticated;
            var service = new CommandAdmissionService<string>(sessions, queue);
            var envelope = new CommandEnvelope<string>(malformed.SessionId, new CommandSequenceId(1), new ServerTick(0), "move");

            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryAdmit_AcceptsAuthenticatedSessionAndEnqueues()
        {
            var service = CreateService(out SessionRegistry sessions, out CommandAdmissionQueue<string> queue);
            var session = new SessionLifecycle(new SessionId(10));
            sessions.Register(session);
            session.TryAuthenticate(new ActorId(1));
            var envelope = new CommandEnvelope<string>(session.SessionId, new CommandSequenceId(1), new ServerTick(5), "move");
            Assert.That(service.TryAdmit(envelope).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(1));
            Assert.That(queue.TryDequeue(out CommandEnvelope<string> admitted), Is.True);
            Assert.That(admitted.Command, Is.EqualTo("move"));
            Assert.That(admitted.SessionId, Is.EqualTo(session.SessionId));
        }

        [Test]
        public void TryAdmit_RejectsReplayForAuthenticatedSession()
        {
            var service = CreateService(out SessionRegistry sessions, out _);
            var session = new SessionLifecycle(new SessionId(10));
            sessions.Register(session);
            session.TryAuthenticate(new ActorId(1));
            var envelope = new CommandEnvelope<string>(session.SessionId, new CommandSequenceId(1), new ServerTick(5), "move");
            Assert.That(service.TryAdmit(envelope).IsSuccess, Is.True);
            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.Conflict));
        }

        [Test]
        public void TryAdmit_FullQueueRejectsWithoutConsumingSequence()
        {
            var sessions = new SessionRegistry(4);
            var queue = new CommandAdmissionQueue<string>(MaximumSequenceJump, 4, 1);
            var service = new CommandAdmissionService<string>(sessions, queue);
            var session = new SessionLifecycle(new SessionId(10));
            sessions.Register(session);
            session.TryAuthenticate(new ActorId(1));
            var first = new CommandEnvelope<string>(session.SessionId, new CommandSequenceId(1), new ServerTick(1), "first");
            var second = new CommandEnvelope<string>(session.SessionId, new CommandSequenceId(2), new ServerTick(2), "second");
            Assert.That(service.TryAdmit(first).IsSuccess, Is.True);
            Assert.That(service.TryAdmit(second).ErrorCode, Is.EqualTo(CoreErrorCode.CapacityExceeded));
            Assert.That(queue.TryDequeue(out _), Is.True);
            Assert.That(service.TryAdmit(second).IsSuccess, Is.True);
        }

        [Test]
        public void TryAdmit_DoesNotConsumeSequenceBeforeSessionAuthentication()
        {
            var service = CreateService(out SessionRegistry sessions, out _);
            var session = new SessionLifecycle(new SessionId(10));
            sessions.Register(session);
            var envelope = new CommandEnvelope<string>(session.SessionId, new CommandSequenceId(1), new ServerTick(0), "move");
            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            session.TryAuthenticate(new ActorId(1));
            Assert.That(service.TryAdmit(envelope).IsSuccess, Is.True);
        }

        [Test]
        public void TryRemoveClosedSession_RejectsInvalidAndUnknownIds()
        {
            var service = CreateService(out _, out _);

            Assert.That(service.TryRemoveClosedSession(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(service.TryRemoveClosedSession(new SessionId(99)).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [Test]
        public void TryRemoveClosedSession_RequiresClosedState()
        {
            var service = CreateService(out SessionRegistry sessions, out _);
            var session = new SessionLifecycle(new SessionId(10));
            sessions.Register(session);

            Assert.That(service.TryRemoveClosedSession(session.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(sessions.Get(session.SessionId).IsSuccess, Is.True);
        }

        [Test]
        public void TryRemoveClosedSession_WaitsForPendingCommandsToDrain()
        {
            var service = CreateService(out SessionRegistry sessions, out CommandAdmissionQueue<string> queue);
            var session = new SessionLifecycle(new SessionId(10));
            sessions.Register(session);
            session.TryAuthenticate(new ActorId(1));
            var envelope = new CommandEnvelope<string>(session.SessionId, new CommandSequenceId(1), new ServerTick(1), "move");
            Assert.That(service.TryAdmit(envelope).IsSuccess, Is.True);
            session.TryClose();

            Assert.That(service.TryRemoveClosedSession(session.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(sessions.Get(session.SessionId).IsSuccess, Is.True);

            Assert.That(queue.TryDequeue(out _), Is.True);
            Assert.That(service.TryRemoveClosedSession(session.SessionId).IsSuccess, Is.True);
            Assert.That(sessions.Get(session.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
            Assert.That(queue.ResetSession(session.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [Test]
        public void TryRemoveClosedSession_AllowsClosedSessionWithoutPriorCommands()
        {
            var service = CreateService(out SessionRegistry sessions, out _);
            var session = new SessionLifecycle(new SessionId(10));
            sessions.Register(session);
            session.TryClose();

            Assert.That(service.TryRemoveClosedSession(session.SessionId).IsSuccess, Is.True);
            Assert.That(sessions.Get(session.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        private static CommandAdmissionService<string> CreateService(out SessionRegistry sessions, out CommandAdmissionQueue<string> queue)
        {
            sessions = new SessionRegistry(4);
            queue = new CommandAdmissionQueue<string>(MaximumSequenceJump, 4, 4);
            return new CommandAdmissionService<string>(sessions, queue);
        }
    }
}