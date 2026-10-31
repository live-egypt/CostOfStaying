namespace CostOfStaying.Core.Tests
{
    using System;
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
        public void Lose_ReducesVitality_AndZeroTransitionsToSpirit()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(40);
            Assert.That(account.Current, Is.EqualTo(60u));
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Alive));

            account.Lose(60);
            Assert.That(account.Current, Is.Zero);
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Spirit));
        }

        [Test]
        public void Lose_ClampsAtZero()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(ulong.MaxValue > uint.MaxValue ? uint.MaxValue : (uint)ulong.MaxValue);
            Assert.That(account.Current, Is.Zero);
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Spirit));
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
        public void Spirit_LoseAndRestoreAreNoOps()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(100);
            account.Lose(10);
            account.Restore(50);
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Spirit));
            Assert.That(account.Current, Is.Zero);
        }

        [Test]
        public void Eliminated_IsTerminalForLoseAndRestore()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(100);
            account.Eliminate();
            account.Lose(10);
            account.Restore(50);
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Eliminated));
            Assert.That(account.Current, Is.Zero);
        }

        [Test]
        public void Resurrect_IsAllowedOnlyFromSpirit()
        {
            var account = new VitalityAccount(new ActorId(1));
            Assert.Throws<InvalidOperationException>(() => account.Resurrect(50));
            account.Lose(100);
            account.Resurrect(50);
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Alive));
            Assert.That(account.Current, Is.EqualTo(50u));
        }

        [Test]
        public void Eliminate_IsAllowedOnlyFromSpirit()
        {
            var account = new VitalityAccount(new ActorId(1));
            Assert.Throws<InvalidOperationException>(() => account.Eliminate());
            account.Lose(100);
            account.Eliminate();
            Assert.That(account.State, Is.EqualTo(PlayerVitalityState.Eliminated));
        }

        [Test]
        public void Resurrect_RejectsZeroOrAboveMaximum()
        {
            var account = new VitalityAccount(new ActorId(1));
            account.Lose(100);
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Resurrect(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Resurrect(101));
        }

        [Test]
        public void Constructor_RejectsZeroMaximum()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new VitalityAccount(new ActorId(1), 0));
        }
    }
}
