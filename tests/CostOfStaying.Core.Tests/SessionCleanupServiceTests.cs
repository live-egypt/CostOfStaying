namespace CostOfStaying.Core.Tests
{
    using CostOfStaying;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;
    using CostOfStaying.Core.Services;
    using NUnit.Framework;

    [TestFixture]
    public sealed class SessionCleanupServiceTests
    {
        [Test]
        public void Constructor_RejectsNullDependencies()
        {
            var sessions = new SessionRegistry(4);
            var queue = new CommandAdmissionQueue<string>(4, 4, 4);
            Assert.Throws<System.ArgumentNullException>(() => new SessionCleanupService<string>(null!, queue));
            Assert.Throws<System.ArgumentNullException>(() => new SessionCleanupService<string>(sessions, null!));
        }

        [Test]
        public void TryRemoveClosedSession_RejectsInvalidAndUnknownIds()
        {
            var service = CreateService(out _, out _);
            Assert.That(service.TryRemoveClosedSession(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(service.TryRemoveClosedSession(new SessionId(99)).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        [Test]
        public void TryRemoveClosedSession_RequiresClosedSession()
        {
            var service = CreateService(out SessionRegistry sessions, out _);
            var session = new SessionLifecycle(new SessionId(1));
            sessions.Register(session);

            Assert.That(service.TryRemoveClosedSession(session.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(sessions.Get(session.SessionId).IsSuccess, Is.True);
        }

        [Test]
        public void TryRemoveClosedSession_RejectsWhileCommandsRemainQueued()
        {
            var service = CreateService(out SessionRegistry sessions, out CommandAdmissionQueue<string> queue);
            var session = RegisterAuthenticated(sessions, new SessionId(1));
            var envelope = new CommandEnvelope<string>(session.SessionId, new CommandSequenceId(1), new ServerTick(1), "pending");
            queue.TryEnqueue(envelope);
            session.TryClose();

            Result result = service.TryRemoveClosedSession(session.SessionId);

            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(sessions.Get(session.SessionId).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(1));
        }

        [Test]
        public void TryRemoveClosedSession_RemovesAfterQueueDrainAndReleasesReplayCapacity()
        {
            var service = CreateService(out SessionRegistry sessions, out CommandAdmissionQueue<string> queue);
            var first = RegisterAuthenticated(sessions, new SessionId(1));
            var envelope = new CommandEnvelope<string>(first.SessionId, new CommandSequenceId(1), new ServerTick(1), "done");
            Assert.That(queue.TryEnqueue(envelope).IsSuccess, Is.True);
            Assert.That(queue.TryDequeue(out _), Is.True);
            first.TryClose();

            Assert.That(service.TryRemoveClosedSession(first.SessionId).IsSuccess, Is.True);
            Assert.That(sessions.Get(first.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));

            var second = RegisterAuthenticated(sessions, new SessionId(2));
            var next = new CommandEnvelope<string>(second.SessionId, new CommandSequenceId(1), new ServerTick(2), "next");
            Assert.That(queue.TryEnqueue(next).IsSuccess, Is.True);
        }

        [Test]
        public void TryRemoveClosedSession_AllowsSessionThatNeverAdmittedCommands()
        {
            var service = CreateService(out SessionRegistry sessions, out _);
            var session = new SessionLifecycle(new SessionId(1));
            sessions.Register(session);
            session.TryClose();

            Assert.That(service.TryRemoveClosedSession(session.SessionId).IsSuccess, Is.True);
            Assert.That(sessions.Get(session.SessionId).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
        }

        private static SessionCleanupService<string> CreateService(
            out SessionRegistry sessions,
            out CommandAdmissionQueue<string> queue)
        {
            sessions = new SessionRegistry(4);
            queue = new CommandAdmissionQueue<string>(4, 1, 4);
            return new SessionCleanupService<string>(sessions, queue);
        }

        private static SessionLifecycle RegisterAuthenticated(SessionRegistry sessions, SessionId sessionId)
        {
            var session = new SessionLifecycle(sessionId);
            sessions.Register(session);
            session.TryAuthenticate(new ActorId(7));
            return session;
        }
    }
}
