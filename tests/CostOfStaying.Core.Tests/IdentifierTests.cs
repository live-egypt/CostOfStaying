namespace CostOfStaying.Core.Tests
{
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class IdentifierTests
    {
        [TestCase(42UL)]
        [TestCase(ulong.MaxValue)]
        public void ActorId_PreservesValidValue(ulong value)
        {
            var id = new ActorId(value);
            Assert.That(id.Value, Is.EqualTo(value));
            Assert.That(id.IsValid, Is.True);
        }

        [Test]
        public void ActorId_ZeroIsRepresentableButInvalid()
        {
            Assert.That(new ActorId(0UL).IsValid, Is.False);
            Assert.That(default(ActorId).IsValid, Is.False);
        }

        [Test]
        public void ActorId_EqualityUsesNumericValue()
        {
            Assert.That(new ActorId(7UL), Is.EqualTo(new ActorId(7UL)));
            Assert.That(new ActorId(7UL), Is.Not.EqualTo(new ActorId(8UL)));
        }

        [Test]
        public void MatchId_PreservesValueAndHasTypedEquality()
        {
            var id = new MatchId(77UL);
            Assert.That(id.Value, Is.EqualTo(77UL));
            Assert.That(id.IsValid, Is.True);
            Assert.That(id, Is.EqualTo(new MatchId(77UL)));
            Assert.That(id, Is.Not.EqualTo(new MatchId(78UL)));
        }

        [Test]
        public void MatchId_ZeroIsInvalid()
        {
            Assert.That(new MatchId(0UL).IsValid, Is.False);
            Assert.That(default(MatchId).IsValid, Is.False);
        }

        [Test]
        public void SessionId_PreservesValueAndHasTypedEquality()
        {
            var id = new SessionId(88UL);
            Assert.That(id.Value, Is.EqualTo(88UL));
            Assert.That(id.IsValid, Is.True);
            Assert.That(id, Is.EqualTo(new SessionId(88UL)));
            Assert.That(id, Is.Not.EqualTo(new SessionId(89UL)));
        }

        [Test]
        public void SessionId_ZeroIsInvalid()
        {
            Assert.That(new SessionId(0UL).IsValid, Is.False);
            Assert.That(default(SessionId).IsValid, Is.False);
        }

        [Test]
        public void VictimId_ZeroIsInvalidAndMaximumIsValid()
        {
            Assert.That(new VictimId(0UL).IsValid, Is.False);
            Assert.That(new VictimId(ulong.MaxValue).IsValid, Is.True);
        }

        [Test]
        public void EventSequenceId_ZeroIsInvalidAndMaximumIsValid()
        {
            Assert.That(new EventSequenceId(0UL).IsValid, Is.False);
            Assert.That(new EventSequenceId(ulong.MaxValue).IsValid, Is.True);
        }

        [Test]
        public void DifferentIdentifierTypes_AreNotInterchangeable()
        {
            Assert.That(typeof(ActorId), Is.Not.EqualTo(typeof(MatchId)));
            Assert.That(typeof(ActorId), Is.Not.EqualTo(typeof(SessionId)));
            Assert.That(typeof(ActorId), Is.Not.EqualTo(typeof(VictimId)));
            Assert.That(typeof(MatchId), Is.Not.EqualTo(typeof(SessionId)));
            Assert.That(typeof(MatchId), Is.Not.EqualTo(typeof(EventSequenceId)));
        }
    }
}
