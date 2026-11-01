namespace CostOfStaying.Core.Tests
{
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class VitalityAccountTests
    {
        [Test]
        public void NewAccount_StartsAliveAtMaximum()
        {
            var account = new VitalityAccount(new ActorId(1));
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Alive));
            Assert.That(account.Current, Is.EqualTo(100u));
            Assert.That(account.Maximum, Is.EqualTo(100u));
        }

        [Test]
        public void Lose_ReducesVitalityWithoutChangingAliveStateBeforeZero()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(40);
            Assert.That(account.Current, Is.EqualTo(60u));
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Alive));
        }

        [Test]
        public void Lose_ReachingZeroTransitionsToDead()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(100);
            Assert.That(account.Current, Is.Zero);
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Dead));
        }

        [Test]
        public void Lose_ClampsAtZero()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(uint.MaxValue);
            Assert.That(account.Current, Is.Zero);
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Dead));
        }

        [Test]
        public void Restore_CapsAtMaximum()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(20);
            account.Restore(50);
            Assert.That(account.Current, Is.EqualTo(100u));
        }

        [Test]
        public void Dead_LoseAndRestoreAreNoOps()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(100);
            account.Lose(10);
            account.Restore(50);
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Dead));
            Assert.That(account.Current, Is.Zero);
        }

        [Test]
        public void ZeroLoss_IsNoOp()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(0);
            Assert.That(account.Current, Is.EqualTo(100u));
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Alive));
        }

        [Test]
        public void ZeroRestore_IsNoOp()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(25);
            account.Restore(0);
            Assert.That(account.Current, Is.EqualTo(75u));
        }

        [Test]
        public void Constructor_RejectsZeroMaximum()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() =>
                new VitalityAccount(new ActorId(1), 0));
        }

        [Test]
        public void Constructor_RejectsInvalidPlayerId()
        {
            Assert.Throws<System.ArgumentException>(() =>
                new VitalityAccount(new ActorId(0)));
        }

        [Test]
        public void DeadStateCannotBeRevivedByRestore()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(100);
            account.Restore(uint.MaxValue);
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Dead));
            Assert.That(account.Current, Is.Zero);
        }

        [Test]
        public void CustomMaximum_IsRespected()
        {
            var account = new VitalityAccount(new ActorId(1), 250);
            Assert.That(account.Maximum, Is.EqualTo(250u));
            Assert.That(account.Current, Is.EqualTo(250u));
        }
    }
}
