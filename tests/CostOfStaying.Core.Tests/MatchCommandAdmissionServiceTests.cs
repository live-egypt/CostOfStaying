namespace CostOfStaying.Core.Tests
{
    using CostOfStaying;
    using CostOfStaying.Core;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;
    using CostOfStaying.Core.Services;
    using NUnit.Framework;

    [TestFixture]
    public sealed class MatchCommandAdmissionServiceTests
    {
        [Test]
        public void Constructor_RejectsNullDependencies()
        {
            var sessions = new SessionRegistry(4);
            var players = new MatchPlayerRegistry(new MatchId(1), 4);
            var queue = new CommandAdmissionQueue<string>(8, 4, 4);
            var inner = new CommandAdmissionService<string>(sessions, queue);

            Assert.Throws<System.ArgumentNullException>(() =>
                new MatchCommandAdmissionService<string>(null!, players, inner));
            Assert.Throws<System.ArgumentNullException>(() =>
                new MatchCommandAdmissionService<string>(sessions, null!, inner));
            Assert.Throws<System.ArgumentNullException>(() =>
                new MatchCommandAdmissionService<string>(sessions, players, null!));
        }

        [Test]
        public void TryAdmit_RejectsDefaultEnvelope()
        {
            var service = CreateService(out _, out _, out CommandAdmissionQueue<string> queue);

            Assert.That(service.TryAdmit(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryAdmit_PropagatesUnknownSessionFailure()
        {
            var service = CreateService(out _, out _, out CommandAdmissionQueue<string> queue);
            var envelope = CreateEnvelope(new SessionId(99), 1, "move");

            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryAdmit_RejectsPendingSessionWithoutConsumingQueueOrSequence()
        {
            var service = CreateService(out SessionRegistry sessions, out IMatchPlayerRegistry players, out CommandAdmissionQueue<string> queue);
            var session = RegisterPending(sessions, new SessionId(10));
            var envelope = CreateEnvelope(session.SessionId, 1, "move");

            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);

            Assert.That(session.TryAuthenticate(new ActorId(7)).IsSuccess, Is.True);
            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);

            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);

            Assert.That(players.Register(new VitalityAccount(new ActorId(7))).IsSuccess, Is.True);
            Assert.That(service.TryAdmit(envelope).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(1));
        }

        [Test]
        public void TryAdmit_RejectsAuthenticatedActorNotInThisMatchWithoutConsumingSequence()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue);
            var session = RegisterAuthenticated(sessions, new SessionId(10), new ActorId(7));
            var envelope = CreateEnvelope(session.SessionId, 1, "move");

            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);

            Assert.That(players.Register(new VitalityAccount(new ActorId(7))).IsSuccess, Is.True);
            Assert.That(service.TryAdmit(envelope).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(1));
        }

        [Test]
        public void TryAdmit_RejectsActorWhoIsOnlyMemberOfAnotherMatch()
        {
            var composition = CoreComposition.Create();
            var sessions = composition.CreateSessionRegistry(4);
            IMatchPlayerRegistry targetMatch = composition.CreateMatchPlayerRegistry(new MatchId(1), 4);
            IMatchPlayerRegistry otherMatch = composition.CreateMatchPlayerRegistry(new MatchId(2), 4);
            var queue = composition.CreateCommandAdmissionQueue<string>(8, 4, 4);
            IMatchCommandAdmissionService<string> service =
                composition.CreateMatchCommandAdmissionService(sessions, targetMatch, queue);
            var actorId = new ActorId(70);
            var session = RegisterAuthenticated(sessions, new SessionId(10), actorId);
            Assert.That(otherMatch.Register(new VitalityAccount(actorId)).IsSuccess, Is.True);

            Assert.That(service.MatchId, Is.EqualTo(new MatchId(1)));
            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1, "move")).ErrorCode,
                Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryAdmit_AcceptsAuthenticatedActorInThisMatch()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue);
            var actorId = new ActorId(42);
            var session = RegisterAuthenticated(sessions, new SessionId(20), actorId);
            Assert.That(players.Register(new VitalityAccount(actorId)).IsSuccess, Is.True);

            Result result = service.TryAdmit(CreateEnvelope(session.SessionId, 1, "move"));

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(1));
        }

        [Test]
        public void TryTakeNext_ReturnsServerAuthenticatedCommandForCurrentMatchMember()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue);
            var actorId = new ActorId(42);
            var session = RegisterAuthenticated(sessions, new SessionId(20), actorId);
            players.Register(new VitalityAccount(actorId));
            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1, "move")).IsSuccess, Is.True);

            Result<AuthenticatedCommand<string>> result = service.TryTakeNext();

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value.ActorId, Is.EqualTo(actorId));
            Assert.That(result.Value.SessionId, Is.EqualTo(session.SessionId));
            Assert.That(result.Value.Command, Is.EqualTo("move"));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryTakeNext_RejectsCommandWhenActorLeavesMatchAfterAdmission()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue);
            var actorId = new ActorId(42);
            var session = RegisterAuthenticated(sessions, new SessionId(20), actorId);
            players.Register(new VitalityAccount(actorId));
            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1, "move")).IsSuccess, Is.True);

            Assert.That(players.Remove(actorId).IsSuccess, Is.True);
            Assert.That(service.TryTakeNext().ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryTakeNext_RejectsCommandWhenSessionDisconnectsAfterAdmission()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue);
            var actorId = new ActorId(42);
            var session = RegisterAuthenticated(sessions, new SessionId(30), actorId);
            players.Register(new VitalityAccount(actorId));
            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1, "move")).IsSuccess, Is.True);

            Assert.That(session.TryDisconnect().IsSuccess, Is.True);
            Assert.That(service.TryTakeNext().ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryAdmit_DoesNotImposeUnapprovedOneSessionPerActorPolicy()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue);
            var actorId = new ActorId(42);
            var first = RegisterAuthenticated(sessions, new SessionId(20), actorId);
            var second = RegisterAuthenticated(sessions, new SessionId(21), actorId);
            players.Register(new VitalityAccount(actorId));

            Assert.That(service.TryAdmit(CreateEnvelope(first.SessionId, 1, "first")).IsSuccess, Is.True);
            Assert.That(service.TryAdmit(CreateEnvelope(second.SessionId, 1, "second")).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(2));
        }

        private static IMatchCommandAdmissionService<string> CreateService(
            out SessionRegistry sessions,
            out IMatchPlayerRegistry players,
            out CommandAdmissionQueue<string> queue)
        {
            CoreComposition composition = CoreComposition.Create();
            sessions = composition.CreateSessionRegistry(8);
            players = composition.CreateMatchPlayerRegistry(new MatchId(5), 8);
            queue = composition.CreateCommandAdmissionQueue<string>(8, 8, 8);
            return composition.CreateMatchCommandAdmissionService(sessions, players, queue);
        }

        private static SessionLifecycle RegisterPending(SessionRegistry sessions, SessionId sessionId)
        {
            var session = new SessionLifecycle(sessionId);
            Assert.That(sessions.Register(session).IsSuccess, Is.True);
            return session;
        }

        private static SessionLifecycle RegisterAuthenticated(
            SessionRegistry sessions,
            SessionId sessionId,
            ActorId actorId)
        {
            var session = RegisterPending(sessions, sessionId);
            Assert.That(session.TryAuthenticate(actorId).IsSuccess, Is.True);
            return session;
        }

        private static CommandEnvelope<string> CreateEnvelope(SessionId sessionId, ulong sequence, string command)
        {
            return new CommandEnvelope<string>(
                sessionId,
                new CommandSequenceId(sequence),
                new ServerTick(1),
                command);
        }
    }
}
