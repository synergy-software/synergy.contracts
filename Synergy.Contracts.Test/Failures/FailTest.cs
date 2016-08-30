using NUnit.Framework;
using Synergy.Contracts.Samples.Domain;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailTest
    {
        [Test]
        public void Because()
        {
            // ACT
            DesignByContractViolationException exception = Fail.Because("Always");

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Always"));
        }
    }
}