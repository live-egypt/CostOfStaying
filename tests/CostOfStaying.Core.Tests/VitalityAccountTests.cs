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
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Alive));
            Assert.That(account.Current, Is.EqualTo(100u));
            Assert.That(account.Maximum, Is.EqualTo(100u));
        }

        [Test]
        public void Lose_ReducesVitalityWithoutChangingStateBeforeZero()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(40);
            Assert.That(account.Current, Is.EqualTo(60u));
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Alive));
        }

        [Test]
        public void Lose_ReachingZeroAutomaticallyTransitionsToSpirit()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(100);
            Assert.That(account.Current, Is.Zero);
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Spirit));
        }

        [Test]
        public void Lose_AboveCurrentClampsToZeroAndEntersSpirit()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(uint.MaxValue);
            Assert.That(account.Current, Is.Zero);
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Spirit));
        }

        [Test]
        public void Restore_CapsAtMaximumWhileAlive()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(20);
            account.Restore(50);
            Assert.That(account.Current, Is.EqualTo(100u));
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Alive));
        }

        [Test]
        public void Spirit_LoseAndRestoreAreNoOps()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(100);
            account.Lose(10);
            account.Restore(50);
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Spirit));
            Assert.That(account.Current, Is.Zero);
        }

        [Test]
        public void ZeroLoss_IsNoOp()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(0);
            Assert.That(account.Current, Is.EqualTo(100u));
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Alive));
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
        public void Resurrect_OnlySpiritAndRequiresPositiveVitality()
        {
            var account = new VitalityAccount(new ActorId(1));
            Assert.That(account.TryResurrect(50), Is.False);
            account.Lose(100);
            Assert.That(account.TryResurrect(0), Is.False);
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Spirit));
            Assert.That(account.Current, Is.Zero);
        }

        [Test]
        public void Resurrect_FromSpiritRestoresRequestedVitality()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(100);
            Assert.That(account.TryResurrect(35), Is.True);
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Alive));
            Assert.That(account.Current, Is.EqualTo(35u));
        }

        [Test]
        public void Resurrect_ClampsRequestedVitalityToMaximum()
        {
            var account = new VitalityAccount(new ActorId(1), 250);
            account.Lose(250);
            Assert.That(account.TryResurrect(uint.MaxValue), Is.True);
            Assert.That(account.Current, Is.EqualTo(250u));
        }

        [Test]
        public void Resurrect_CannotReviveEliminatedPlayer()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(100);
            account.TryEliminate();
            Assert.That(account.TryResurrect(50), Is.False);
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Eliminated));
            Assert.That(account.Current, Is.Zero);
        }

        [Test]
        public void Eliminate_FromAliveIsExplicitAndTerminal()
        {
            var account = new VitalityAccount(new ActorId(1));
            Assert.That(account.TryEliminate(), Is.True);
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Eliminated));
            Assert.That(account.Current, Is.Zero);
            Assert.That(account.TryEliminate(), Is.False);
        }

        [Test]
        public void Eliminate_FromSpiritIsExplicitAndTerminal()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(100);
            Assert.That(account.TryEliminate(), Is.True);
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Eliminated));
            Assert.That(account.Current, Is.Zero);
        }

        [Test]
        public void Eliminated_LoseRestoreAndResurrectAreNoOps()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.TryEliminate();
            account.Lose(uint.MaxValue);
            account.Restore(uint.MaxValue);
            Assert.That(account.TryResurrect(100), Is.False);
            Assert.That(account.State, Is.EqualTo(PlayerLifecycleState.Eliminated));
            Assert.That(account.Current, Is.Zero);
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
        public void CustomMaximum_IsRespected()
        {
            var account = new VitalityAccount(new ActorId(1), 250);
            Assert.That(account.Maximum, Is.EqualTo(250u));
            Assert.That(account.Current, Is.EqualTo(250u));
        }

        [Test]
        public void Accounts_AreIndependent()
        {
            var first = new VitalityAccount(new ActorId(1));
            var second = new VitalityAccount(new ActorId(2));
            first.Lose(100);
            Assert.That(first.State, Is.EqualTo(PlayerLifecycleState.Spirit));
            Assert.That(second.State, Is.EqualTo(PlayerLifecycleState.Alive));
            Assert.That(second.Current, Is.EqualTo(100u));
        }
    }
}
