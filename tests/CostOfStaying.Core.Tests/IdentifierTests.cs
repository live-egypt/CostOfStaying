namespace CostOfStaying.Core.Tests
{
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class IdentifierTests
    {
        [Test]
        public void ActorId_PreservesNumericValue()
        {
            var id = new ActorId(42UL);
            Assert.That(id.Value, Is.EqualTo(42UL));
            Assert.That(id.ToString(), Is.EqualTo("42"));
            Assert.That(id.IsValid, Is.True);
        }

        [Test]
        public void ActorId_EqualityUsesNumericValue()
        {
            Assert.That(new ActorId(7UL), Is.EqualTo(new ActorId(7UL)));
            Assert.That(new ActorId(7UL), Is.Not.EqualTo(new ActorId(8UL)));
            Assert.That(new ActorId(7UL) == new ActorId(7UL), Is.True);
            Assert.That(new ActorId(7UL) != new ActorId(8UL), Is.True);
        }

        [Test]
        public void ActorId_ZeroIsRepresentableButInvalid()
        {
            var id = new ActorId(0UL);
            Assert.That(id.Value, Is.Zero);
            Assert.That(id.IsValid, Is.False);
        }

        [Test]
        public void ActorId_SupportsMaximumValue()
        {
            var id = new ActorId(ulong.MaxValue);
            Assert.That(id.Value, Is.EqualTo(ulong.MaxValue));
            Assert.That(id.IsValid, Is.True);
        }

        [Test]
        public void VictimId_EqualityUsesNumericValue()
        {
            Assert.That(new VictimId(11UL), Is.EqualTo(new VictimId(11UL)));
            Assert.That(new VictimId(11UL), Is.Not.EqualTo(new VictimId(12UL)));
        }

        [Test]
        public void VictimId_ZeroIsRepresentableButInvalid()
        {
            var id = new VictimId(0UL);
            Assert.That(id.Value, Is.Zero);
            Assert.That(id.IsValid, Is.False);
        }

        [Test]
        public void VictimId_SupportsMaximumValue()
        {
            var id = new VictimId(ulong.MaxValue);
            Assert.That(id.Value, Is.EqualTo(ulong.MaxValue));
            Assert.That(id.IsValid, Is.True);
        }

        [Test]
        public void EventSequenceId_EqualityUsesNumericValue()
        {
            Assert.That(new EventSequenceId(99UL), Is.EqualTo(new EventSequenceId(99UL)));
            Assert.That(new EventSequenceId(99UL), Is.Not.EqualTo(new EventSequenceId(100UL)));
        }

        [Test]
        public void EventSequenceId_ZeroIsRepresentableButInvalid()
        {
            var id = new EventSequenceId(0UL);
            Assert.That(id.Value, Is.Zero);
            Assert.That(id.IsValid, Is.False);
        }

        [Test]
        public void EventSequenceId_SupportsMaximumValue()
        {
            var id = new EventSequenceId(ulong.MaxValue);
            Assert.That(id.Value, Is.EqualTo(ulong.MaxValue));
            Assert.That(id.IsValid, Is.True);
        }

        [Test]
        public void DifferentIdentifierTypes_AreDistinctTypes()
        {
            Assert.That(typeof(ActorId), Is.Not.EqualTo(typeof(VictimId)));
            Assert.That(typeof(ActorId), Is.Not.EqualTo(typeof(EventSequenceId)));
            Assert.That(typeof(VictimId), Is.Not.EqualTo(typeof(EventSequenceId)));
        }
    }
}
