namespace CostOfStaying.Core.Tests
{
    using System;
    using CostOfStaying;
    using CostOfStaying.Core;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class MatchCommandAuthorizationServiceTests
    {
        private sealed class DelegatePolicy : IMatchCommandAuthorizationPolicy<string>
        {
            private readonly Func<MatchCommandAuthorizationContext<string>, Result> _evaluate;

            public DelegatePolicy(Func<MatchCommandAuthorizationContext<string>, Result> evaluate)
            {
                _evaluate = evaluate ?? throw new ArgumentNullException(nameof(evaluate));
            }

            public int Calls { get; private set; }
            public MatchCommandAuthorizationContext<string> LastContext { get; private set; }

            public Result Authorize(MatchCommandAuthorizationContext<string> context)
            {
                Calls++;
                LastContext = context;
                return _evaluate(context);
            }
        }

        [Test]
        public void TryAdmit_RejectsDefaultEnvelopeBeforePolicy()
        {
            var service = CreateService(out _, out _, out _, out _, out DelegatePolicy policy, _ => Result.Success());

            Assert.That(service.TryAdmit(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(policy.Calls, Is.Zero);
        }

        [Test]
        public void TryAdmit_RejectsNonMemberBeforePolicy()
        {
            var service = CreateService(out SessionRegistry sessions, out _, out _, out CommandAdmissionQueue<string> queue, out DelegatePolicy policy, _ => Result.Success());
            var session = RegisterAuthenticated(sessions, new SessionId(10), new ActorId(7));

            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1)).ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(policy.Calls, Is.Zero);
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryAdmit_PolicyDenialDoesNotConsumeSequenceAndSameSequenceCanRetry()
        {
            bool allowed = false;
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchLifecycle match,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue,
                out DelegatePolicy policy,
                context => allowed ? Result.Success() : Result.Failure(CoreErrorCode.Unauthorized));
            MoveToRunning(match);
            ActorId actor = new ActorId(42);
            SessionLifecycle session = RegisterAuthenticated(sessions, new SessionId(20), actor);
            Assert.That(players.Register(new VitalityAccount(actor)).IsSuccess, Is.True);
            CommandEnvelope<string> envelope = CreateEnvelope(session.SessionId, 1);

            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);
            Assert.That(policy.LastContext.SessionId, Is.EqualTo(session.SessionId));
            Assert.That(policy.LastContext.ActorId, Is.EqualTo(actor));
            Assert.That(policy.LastContext.MatchId, Is.EqualTo(match.MatchId));
            Assert.That(policy.LastContext.MatchPhase, Is.EqualTo(MatchPhase.Running));
            Assert.That(policy.LastContext.PlayerState, Is.EqualTo(PlayerLifecycleState.Alive));
            Assert.That(policy.LastContext.ReceivedAt, Is.EqualTo(envelope.ReceivedAt));
            Assert.That(policy.LastContext.Command, Is.EqualTo("move"));

            allowed = true;
            Assert.That(service.TryAdmit(envelope).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(1));
        }

        [Test]
        public void TryAdmit_UninitializedPolicyResultFailsClosed()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out _,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue,
                out _,
                _ => default);
            ActorId actor = new ActorId(42);
            SessionLifecycle session = RegisterAuthenticated(sessions, new SessionId(20), actor);
            players.Register(new VitalityAccount(actor));

            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1)).ErrorCode,
                Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryAdmit_PolicyExceptionFailsClosedWithoutConsumingSequenceAndCanRetry()
        {
            bool throwException = true;
            var service = CreateService(
                out SessionRegistry sessions,
                out _,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue,
                out _,
                _ =>
                {
                    if (throwException)
                        throw new InvalidOperationException("synthetic policy exception");
                    return Result.Success();
                });
            ActorId actor = new ActorId(42);
            SessionLifecycle session = RegisterAuthenticated(sessions, new SessionId(20), actor);
            players.Register(new VitalityAccount(actor));
            CommandEnvelope<string> envelope = CreateEnvelope(session.SessionId, 1);

            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.Unavailable));
            Assert.That(queue.PendingCount, Is.Zero);

            throwException = false;
            Assert.That(service.TryAdmit(envelope).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(1));
        }

        [Test]
        public void TryAdmit_AllowsOnlyWhenInjectedPolicyExplicitlySucceeds()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchLifecycle match,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue,
                out _,
                context => context.MatchPhase == MatchPhase.Running &&
                           context.PlayerState == PlayerLifecycleState.Alive
                    ? Result.Success()
                    : Result.Failure(CoreErrorCode.Unauthorized));
            MoveToRunning(match);
            ActorId actor = new ActorId(42);
            SessionLifecycle session = RegisterAuthenticated(sessions, new SessionId(20), actor);
            players.Register(new VitalityAccount(actor));

            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1)).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(1));
        }

        [Test]
        public void TryTakeNext_RechecksCurrentMatchPhaseBeforeHandlerHandoff()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchLifecycle match,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue,
                out _,
                context => context.MatchPhase == MatchPhase.Running
                    ? Result.Success()
                    : Result.Failure(CoreErrorCode.Unauthorized));
            MoveToRunning(match);
            ActorId actor = new ActorId(42);
            SessionLifecycle session = RegisterAuthenticated(sessions, new SessionId(20), actor);
            players.Register(new VitalityAccount(actor));
            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1)).IsSuccess, Is.True);

            Assert.That(match.TryTransition(MatchPhase.Ending).IsSuccess, Is.True);
            Assert.That(service.TryTakeNext().ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryTakeNext_RechecksPlayerLifecycleBeforeHandlerHandoff()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out _,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue,
                out _,
                context => context.PlayerState == PlayerLifecycleState.Alive
                    ? Result.Success()
                    : Result.Failure(CoreErrorCode.Unauthorized));
            ActorId actor = new ActorId(42);
            SessionLifecycle session = RegisterAuthenticated(sessions, new SessionId(20), actor);
            var player = new VitalityAccount(actor);
            players.Register(player);
            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1)).IsSuccess, Is.True);

            player.Lose(player.Current);
            Assert.That(player.State, Is.EqualTo(PlayerLifecycleState.Spirit));
            Assert.That(service.TryTakeNext().ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryTakeNext_RechecksSessionAuthenticationBeforePolicyHandoff()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out _,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<string> queue,
                out DelegatePolicy policy,
                _ => Result.Success());
            ActorId actor = new ActorId(42);
            SessionLifecycle session = RegisterAuthenticated(sessions, new SessionId(20), actor);
            players.Register(new VitalityAccount(actor));
            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1)).IsSuccess, Is.True);
            int callsBeforeTake = policy.Calls;

            Assert.That(session.TryDisconnect().IsSuccess, Is.True);
            Assert.That(service.TryTakeNext().ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(policy.Calls, Is.EqualTo(callsBeforeTake));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        private static IMatchCommandAuthorizationService<string> CreateService(
            out SessionRegistry sessions,
            out IMatchLifecycle match,
            out IMatchPlayerRegistry players,
            out CommandAdmissionQueue<string> queue,
            out DelegatePolicy policy,
            Func<MatchCommandAuthorizationContext<string>, Result> evaluate)
        {
            CoreComposition composition = CoreComposition.Create();
            sessions = composition.CreateSessionRegistry(8);
            match = composition.CreateMatchLifecycle(new MatchId(5));
            players = composition.CreateMatchPlayerRegistry(match.MatchId, 8);
            queue = composition.CreateCommandAdmissionQueue<string>(8, 8, 8);
            policy = new DelegatePolicy(evaluate);
            return composition.CreateMatchCommandAuthorizationService(sessions, match, players, queue, policy);
        }

        private static void MoveToRunning(IMatchLifecycle match)
        {
            Assert.That(match.TryTransition(MatchPhase.Waiting).IsSuccess, Is.True);
            Assert.That(match.TryTransition(MatchPhase.Starting).IsSuccess, Is.True);
            Assert.That(match.TryTransition(MatchPhase.Running).IsSuccess, Is.True);
        }

        private static SessionLifecycle RegisterAuthenticated(SessionRegistry sessions, SessionId sessionId, ActorId actorId)
        {
            var session = new SessionLifecycle(sessionId);
            Assert.That(sessions.Register(session).IsSuccess, Is.True);
            Assert.That(session.TryAuthenticate(actorId).IsSuccess, Is.True);
            return session;
        }

        private static CommandEnvelope<string> CreateEnvelope(SessionId sessionId, ulong sequence)
        {
            return new CommandEnvelope<string>(sessionId, new CommandSequenceId(sequence), new ServerTick(7), "move");
        }
    }
}
