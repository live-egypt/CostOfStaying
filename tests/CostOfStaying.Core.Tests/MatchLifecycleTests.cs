namespace CostOfStaying.Core.Tests
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class MatchLifecycleTests
    {
        [Test]
        public void NewMatch_StartsCreated()
        {
            var match = new MatchLifecycle(new MatchId(1));
            Assert.That(match.Phase, Is.EqualTo(MatchPhase.Created));
            Assert.That(match.MatchId, Is.EqualTo(new MatchId(1)));
        }

        [TestCase(MatchPhase.Created, MatchPhase.Waiting)]
        [TestCase(MatchPhase.Waiting, MatchPhase.Starting)]
        [TestCase(MatchPhase.Starting, MatchPhase.Running)]
        [TestCase(MatchPhase.Running, MatchPhase.VoteOpen)]
        [TestCase(MatchPhase.Running, MatchPhase.Ending)]
        [TestCase(MatchPhase.VoteOpen, MatchPhase.Running)]
        [TestCase(MatchPhase.VoteOpen, MatchPhase.Ending)]
        [TestCase(MatchPhase.Ending, MatchPhase.Completed)]
        public void TryTransition_AcceptsDocumentedTransitions(MatchPhase from, MatchPhase to)
        {
            var match = new MatchLifecycle(new MatchId(1));
            MoveTo(match, from);

            Result result = match.TryTransition(to);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(match.Phase, Is.EqualTo(to));
        }

        [TestCase(MatchPhase.Created, MatchPhase.Running)]
        [TestCase(MatchPhase.Waiting, MatchPhase.Completed)]
        [TestCase(MatchPhase.Starting, MatchPhase.VoteOpen)]
        [TestCase(MatchPhase.Running, MatchPhase.Completed)]
        [TestCase(MatchPhase.VoteOpen, MatchPhase.Starting)]
        [TestCase(MatchPhase.Ending, MatchPhase.Running)]
        [TestCase(MatchPhase.Completed, MatchPhase.Running)]
        public void TryTransition_RejectsUndocumentedTransitionsWithoutMutation(MatchPhase from, MatchPhase to)
        {
            var match = new MatchLifecycle(new MatchId(1));
            MoveTo(match, from);

            MatchPhase before = match.Phase;
            Result result = match.TryTransition(to);

            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(match.Phase, Is.EqualTo(before));
        }

        [Test]
        public void TryTransition_RejectsUndefinedEnumValue()
        {
            var match = new MatchLifecycle(new MatchId(1));
            Result result = match.TryTransition((MatchPhase)999);

            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(match.Phase, Is.EqualTo(MatchPhase.Created));
        }

        [Test]
        public void Constructor_RejectsInvalidMatchId()
        {
            Assert.Throws<System.ArgumentException>(() => new MatchLifecycle(default));
        }

        private static void MoveTo(MatchLifecycle match, MatchPhase target)
        {
            MatchPhase[] path =
            {
                MatchPhase.Waiting,
                MatchPhase.Starting,
                MatchPhase.Running,
                MatchPhase.Ending,
                MatchPhase.Completed
            };

            if (target == MatchPhase.Created)
                return;

            foreach (MatchPhase phase in path)
            {
                if (!match.TryTransition(phase).IsSuccess)
                    throw new System.InvalidOperationException("Test setup could not reach " + phase + ".");

                if (phase == target)
                    return;

                if (target == MatchPhase.VoteOpen && phase == MatchPhase.Running)
                {
                    if (!match.TryTransition(MatchPhase.VoteOpen).IsSuccess)
                        throw new System.InvalidOperationException("Test setup could not open vote.");
                    return;
                }
            }

            if (target == MatchPhase.VoteOpen && match.Phase == MatchPhase.Running)
            {
                if (!match.TryTransition(MatchPhase.VoteOpen).IsSuccess)
                    throw new System.InvalidOperationException("Test setup could not open vote.");
            }
        }
    }
}
