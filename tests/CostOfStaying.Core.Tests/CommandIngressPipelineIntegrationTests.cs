namespace CostOfStaying.Core.Tests
{
    using CostOfStaying;
    using CostOfStaying.Core;
    using CostOfStaying.Core.Contracts;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CommandIngressPipelineIntegrationTests
    {
        private sealed class PayloadValidator : ICommandPayloadValidator<string>
        {
            public int Calls { get; private set; }

            public Result Validate(string command)
            {
                Calls++;
                return command == "move"
                    ? Result.Success()
                    : Result.Failure(CoreErrorCode.InvalidArgument);
            }
        }

        private sealed class RunningAlivePolicy : IMatchCommandAuthorizationPolicy<string>
        {
            public int Calls { get; private set; }

            public Result Authorize(MatchCommandAuthorizationContext<string> context)
            {
                Calls++;
                return context.MatchPhase == MatchPhase.Running &&
                       context.PlayerState == PlayerLifecycleState.Alive
                    ? Result.Success()
                    : Result.Failure(CoreErrorCode.Unauthorized);
            }
        }

        [Test]
        public void Pipeline_ValidatesPayloadThenAuthenticatesMembershipAndAuthorizesBeforeQueue()
        {
            IMatchCommandPayloadValidationService<string> pipeline = CreatePipeline(
                out SessionLifecycle session,
                out CommandAdmissionQueue<string> queue,
                out PayloadValidator validator,
                out RunningAlivePolicy policy);

            Result invalid = pipeline.TryAdmit(CreateEnvelope(session.SessionId, 1, "invalid"));

            Assert.That(invalid.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(validator.Calls, Is.EqualTo(1));
            Assert.That(policy.Calls, Is.Zero);
            Assert.That(queue.PendingCount, Is.Zero);

            Result valid = pipeline.TryAdmit(CreateEnvelope(session.SessionId, 1, "move"));

            Assert.That(valid.IsSuccess, Is.True);
            Assert.That(validator.Calls, Is.EqualTo(2));
            Assert.That(policy.Calls, Is.EqualTo(1));
            Assert.That(queue.PendingCount, Is.EqualTo(1));
        }

        [Test]
        public void Pipeline_RevalidatesAuthorizationAndPayloadBeforeReturningDequeuedCommand()
        {
            IMatchCommandPayloadValidationService<string> pipeline = CreatePipeline(
                out SessionLifecycle session,
                out CommandAdmissionQueue<string> queue,
                out PayloadValidator validator,
                out RunningAlivePolicy policy);

            Assert.That(pipeline.TryAdmit(CreateEnvelope(session.SessionId, 1, "move")).IsSuccess, Is.True);
            Assert.That(queue.PendingCount, Is.EqualTo(1));
            Assert.That(validator.Calls, Is.EqualTo(1));
            Assert.That(policy.Calls, Is.EqualTo(1));

            Result<AuthenticatedCommand<string>> taken = pipeline.TryTakeNext();

            Assert.That(taken.IsSuccess, Is.True);
            Assert.That(taken.Value.SessionId, Is.EqualTo(session.SessionId));
            Assert.That(taken.Value.ActorId, Is.EqualTo(new ActorId(77)));
            Assert.That(taken.Value.Command, Is.EqualTo("move"));
            Assert.That(queue.PendingCount, Is.Zero);
            Assert.That(validator.Calls, Is.EqualTo(2));
            Assert.That(policy.Calls, Is.EqualTo(2));
        }

        [Test]
        public void Pipeline_DiscardsQueuedCommandWhenCurrentMatchPhaseNoLongerAuthorizesIt()
        {
            IMatchCommandPayloadValidationService<string> pipeline = CreatePipeline(
                out SessionLifecycle session,
                out CommandAdmissionQueue<string> queue,
                out PayloadValidator validator,
                out RunningAlivePolicy policy,
                out MatchLifecycle match);

            Assert.That(pipeline.TryAdmit(CreateEnvelope(session.SessionId, 1, "move")).IsSuccess, Is.True);
            Assert.That(match.TryTransition(MatchPhase.Ending).IsSuccess, Is.True);

            Result<AuthenticatedCommand<string>> taken = pipeline.TryTakeNext();

            Assert.That(taken.ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);
            Assert.That(validator.Calls, Is.EqualTo(1));
            Assert.That(policy.Calls, Is.EqualTo(2));
        }

        [Test]
        public void Pipeline_DiscardsQueuedCommandAfterSessionDisconnectBeforeHandlerHandoff()
        {
            IMatchCommandPayloadValidationService<string> pipeline = CreatePipeline(
                out SessionLifecycle session,
                out CommandAdmissionQueue<string> queue,
                out PayloadValidator validator,
                out RunningAlivePolicy policy);

            Assert.That(pipeline.TryAdmit(CreateEnvelope(session.SessionId, 1, "move")).IsSuccess, Is.True);
            Assert.That(session.TryDisconnect().IsSuccess, Is.True);

            Result<AuthenticatedCommand<string>> taken = pipeline.TryTakeNext();

            Assert.That(taken.ErrorCode, Is.EqualTo(CoreErrorCode.Unauthorized));
            Assert.That(queue.PendingCount, Is.Zero);
            Assert.That(validator.Calls, Is.EqualTo(1));
            Assert.That(policy.Calls, Is.EqualTo(1));
        }

        private static IMatchCommandPayloadValidationService<string> CreatePipeline(
            out SessionLifecycle session,
            out CommandAdmissionQueue<string> queue,
            out PayloadValidator validator,
            out RunningAlivePolicy policy,
            out MatchLifecycle match)
        {
            CoreComposition composition = CoreComposition.Create();
            var sessions = composition.CreateSessionRegistry(4);
            match = (MatchLifecycle)composition.CreateMatchLifecycle(new MatchId(9));
            Assert.That(match.TryTransition(MatchPhase.Waiting).IsSuccess, Is.True);
            Assert.That(match.TryTransition(MatchPhase.Starting).IsSuccess, Is.True);
            Assert.That(match.TryTransition(MatchPhase.Running).IsSuccess, Is.True);

            IMatchPlayerRegistry players = composition.CreateMatchPlayerRegistry(match.MatchId, 4);
            var actorId = new ActorId(77);
            Assert.That(players.Register(new VitalityAccount(actorId)).IsSuccess, Is.True);

            session = new SessionLifecycle(new SessionId(12));
            Assert.That(sessions.Register(session).IsSuccess, Is.True);
            Assert.That(session.TryAuthenticate(actorId).IsSuccess, Is.True);

            queue = composition.CreateCommandAdmissionQueue<string>(8, 4, 4);
            policy = new RunningAlivePolicy();
            IMatchCommandAuthorizationService<string> authorization =
                composition.CreateMatchCommandAuthorizationService(
                    sessions, match, players, queue, policy);
            validator = new PayloadValidator();

            return composition.CreateMatchCommandPayloadValidationService(authorization, validator);
        }

        private static IMatchCommandPayloadValidationService<string> CreatePipeline(
            out SessionLifecycle session,
            out CommandAdmissionQueue<string> queue,
            out PayloadValidator validator,
            out RunningAlivePolicy policy)
        {
            return CreatePipeline(out session, out queue, out validator, out policy, out _);
        }

        private static CommandEnvelope<string> CreateEnvelope(
            SessionId sessionId,
            ulong sequence,
            string command)
        {
            return new CommandEnvelope<string>(
                sessionId,
                new CommandSequenceId(sequence),
                new ServerTick(1),
                command);
        }
    }
}
