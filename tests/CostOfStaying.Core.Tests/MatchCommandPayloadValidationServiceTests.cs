namespace CostOfStaying.Core.Tests
{
    using System;
    using CostOfStaying;
    using CostOfStaying.Core;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class MatchCommandPayloadValidationServiceTests
    {
        private sealed class MutablePayload
        {
            public bool IsValid { get; set; }
        }

        private sealed class DelegateValidator : ICommandPayloadValidator<MutablePayload>
        {
            private readonly Func<MutablePayload, Result> _validate;

            public DelegateValidator(Func<MutablePayload, Result> validate)
            {
                _validate = validate ?? throw new ArgumentNullException(nameof(validate));
            }

            public int Calls { get; private set; }

            public Result Validate(MutablePayload command)
            {
                Calls++;
                return _validate(command);
            }
        }

        private sealed class AllowAllPolicy : IMatchCommandAuthorizationPolicy<MutablePayload>
        {
            public Result Authorize(MatchCommandAuthorizationContext<MutablePayload> context)
            {
                return Result.Success();
            }
        }

        [Test]
        public void TryAdmit_RejectsDefaultEnvelopeWithoutInvokingValidator()
        {
            var service = CreateService(out _, out _, out _, out _, out DelegateValidator validator, _ => Result.Success());

            Assert.That(service.TryAdmit(default).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(validator.Calls, Is.Zero);
        }

        [Test]
        public void TryAdmit_PayloadRejectionDoesNotConsumeSequenceAndSameSequenceCanRetry()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<MutablePayload> queue,
                out _,
                out DelegateValidator validator,
                payload => payload.IsValid ? Result.Success() : Result.Failure(CoreErrorCode.InvalidArgument));
            SessionLifecycle session = RegisterAuthenticated(sessions, new SessionId(10), new ActorId(7));
            players.Register(new VitalityAccount(new ActorId(7)));
            var payload = new MutablePayload { IsValid = false };
            var envelope = CreateEnvelope(session.SessionId, 1, payload);

            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(queue.PendingCount, Is.Zero);
            Assert.That(validator.Calls, Is.EqualTo(1));

            payload.IsValid = true;
            Assert.That(service.TryAdmit(envelope).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(1));
            Assert.That(validator.Calls, Is.EqualTo(2));
        }

        [Test]
        public void TryAdmit_UninitializedValidatorResultFailsClosed()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<MutablePayload> queue,
                out _,
                out _,
                _ => default);
            SessionLifecycle session = RegisterAuthenticated(sessions, new SessionId(10), new ActorId(7));
            players.Register(new VitalityAccount(new ActorId(7)));

            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1, new MutablePayload { IsValid = true })).ErrorCode,
                Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        [Test]
        public void TryAdmit_ValidatorExceptionFailsClosedAndDoesNotConsumeSequence()
        {
            bool throwException = true;
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<MutablePayload> queue,
                out _,
                out _,
                _ =>
                {
                    if (throwException)
                        throw new InvalidOperationException("synthetic validator exception");
                    return Result.Success();
                });
            SessionLifecycle session = RegisterAuthenticated(sessions, new SessionId(10), new ActorId(7));
            players.Register(new VitalityAccount(new ActorId(7)));
            var envelope = CreateEnvelope(session.SessionId, 1, new MutablePayload { IsValid = true });

            Assert.That(service.TryAdmit(envelope).ErrorCode, Is.EqualTo(CoreErrorCode.Unavailable));
            Assert.That(queue.PendingCount, Is.Zero);

            throwException = false;
            Assert.That(service.TryAdmit(envelope).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(1));
        }

        [Test]
        public void TryTakeNext_RevalidatesPayloadAfterDequeueBeforeReturningToHandler()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<MutablePayload> queue,
                out _,
                out DelegateValidator validator,
                payload => payload.IsValid ? Result.Success() : Result.Failure(CoreErrorCode.InvalidArgument));
            SessionLifecycle session = RegisterAuthenticated(sessions, new SessionId(10), new ActorId(7));
            players.Register(new VitalityAccount(new ActorId(7)));
            var payload = new MutablePayload { IsValid = true };
            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1, payload)).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(1));
            Assert.That(validator.Calls, Is.EqualTo(1));

            payload.IsValid = false;
            Assert.That(service.TryTakeNext().ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(queue.PendingCount, Is.Zero);
            Assert.That(validator.Calls, Is.EqualTo(2));
        }

        [Test]
        public void TryTakeNext_ReturnsPayloadOnlyAfterAuthorizationAndValidationSucceed()
        {
            var service = CreateService(
                out SessionRegistry sessions,
                out IMatchPlayerRegistry players,
                out CommandAdmissionQueue<MutablePayload> queue,
                out _,
                out _,
                payload => payload.IsValid ? Result.Success() : Result.Failure(CoreErrorCode.InvalidArgument));
            SessionLifecycle session = RegisterAuthenticated(sessions, new SessionId(10), new ActorId(7));
            players.Register(new VitalityAccount(new ActorId(7)));
            var payload = new MutablePayload { IsValid = true };
            Assert.That(service.TryAdmit(CreateEnvelope(session.SessionId, 1, payload)).IsSuccess, Is.True);

            Result<AuthenticatedCommand<MutablePayload>> result = service.TryTakeNext();

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value.ActorId, Is.EqualTo(new ActorId(7)));
            Assert.That(result.Value.Command, Is.SameAs(payload));
            Assert.That(queue.PendingCount, Is.Zero);
        }

        private static IMatchCommandPayloadValidationService<MutablePayload> CreateService(
            out SessionRegistry sessions,
            out IMatchPlayerRegistry players,
            out CommandAdmissionQueue<MutablePayload> queue,
            out IMatchLifecycle match,
            out DelegateValidator validator,
            Func<MutablePayload, Result> validate)
        {
            CoreComposition composition = CoreComposition.Create();
            sessions = composition.CreateSessionRegistry(8);
            match = composition.CreateMatchLifecycle(new MatchId(5));
            players = composition.CreateMatchPlayerRegistry(match.MatchId, 8);
            queue = composition.CreateCommandAdmissionQueue<MutablePayload>(8, 8, 8);
            IMatchCommandAuthorizationService<MutablePayload> authorization =
                composition.CreateMatchCommandAuthorizationService(sessions, match, players, queue, new AllowAllPolicy());
            validator = new DelegateValidator(validate);
            return composition.CreateMatchCommandPayloadValidationService(authorization, validator);
        }

        private static SessionLifecycle RegisterAuthenticated(SessionRegistry sessions, SessionId sessionId, ActorId actorId)
        {
            var session = new SessionLifecycle(sessionId);
            Assert.That(sessions.Register(session).IsSuccess, Is.True);
            Assert.That(session.TryAuthenticate(actorId).IsSuccess, Is.True);
            return session;
        }

        private static CommandEnvelope<MutablePayload> CreateEnvelope(SessionId sessionId, ulong sequence, MutablePayload payload)
        {
            return new CommandEnvelope<MutablePayload>(sessionId, new CommandSequenceId(sequence), new ServerTick(12), payload);
        }
    }
}
