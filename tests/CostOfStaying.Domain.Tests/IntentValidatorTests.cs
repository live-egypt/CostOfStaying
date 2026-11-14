using NUnit.Framework;
using System;
using CostOfStaying.Domain;

namespace CostOfStaying.Domain.Tests
{
    [TestFixture]
    public class IntentValidatorTests
    {
        [TestCase(100, 100, true)]
        [TestCase(100, 104.9, true)]
        [TestCase(100, 105, true)]  // boundary
        [TestCase(100, 106, false)]
        [TestCase(100, 94, false)]  // too old
        [TestCase(100, 200, false)] // future-dated
        public void SentAt_BoundedSkew(double serverNow, double clientSentAt, bool expected)
        {
            Assert.That(IntentValidator.IsSentAtValid(serverNow, clientSentAt), Is.EqualTo(expected));
        }

        [TestCase(1.5, 2.0, true)]
        [TestCase(2.0, 2.0, true)]
        [TestCase(2.001, 2.0, false)]
        [TestCase(-0.1, 2.0, false)]
        public void Distance_Valid(double measured, double max, bool expected)
        {
            Assert.That(IntentValidator.IsDistanceValid(measured, max), Is.EqualTo(expected));
        }

        [TestCase("req-abc12345", true)]
        [TestCase("short", false)] // <8
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("req with space", false)]
        public void RequestId_Valid(string? id, bool expected)
        {
            Assert.That(IntentValidator.IsRequestIdValid(id), Is.EqualTo(expected));
        }
    }
}
