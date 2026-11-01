namespace CostOfStaying.Core.Tests
{
    using System;
    using CostOfStaying;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ResultTests
    {
        [Test]
        public void Result_SuccessHasNoError()
        {
            Result result = Result.Success();
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.IsFailure, Is.False);
            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.None));
        }

        [Test]
        public void Result_FailurePreservesErrorCode()
        {
            Result result = Result.Failure(CoreErrorCode.InvalidState);
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
        }

        [Test]
        public void Result_DefaultValueIsSafeFailure()
        {
            Result result = default;
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.Uninitialized));
        }

        [Test]
        public void Result_FailureRejectsNoneErrorCode()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Result.Failure(CoreErrorCode.None));
        }

        [Test]
        public void ResultT_SuccessCarriesValue()
        {
            Result<int> result = Result<int>.Success(42);
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.EqualTo(42));
            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.None));
        }

        [Test]
        public void ResultT_FailureCarriesNoValueAndPreservesError()
        {
            Result<int> result = Result<int>.Failure(CoreErrorCode.NotFound);
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.NotFound));
            Assert.Throws<InvalidOperationException>(() => _ = result.Value);
        }

        [Test]
        public void ResultT_DefaultValueIsSafeFailure()
        {
            Result<int> result = default;
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.Uninitialized));
            Assert.Throws<InvalidOperationException>(() => _ = result.Value);
        }

        [Test]
        public void ResultT_FailureRejectsNoneErrorCode()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Result<int>.Failure(CoreErrorCode.None));
        }

        [Test]
        public void ResultT_SuccessRejectsNullReference()
        {
            Assert.Throws<ArgumentNullException>(() => Result<string>.Success(null!));
        }

        [Test]
        public void ResultT_SupportsValueTypes()
        {
            Result<int> result = Result<int>.Success(0);
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.Zero);
        }
    }
}
