using NUnit.Framework;
using Synergy.Contracts.Samples.Domain;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailTest
    {
        #region Fail.Because

        [Test]
        public void Because()
        {
            // ACT
            DesignByContractViolationException exception = Fail.Because("Always");

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Always"));
        }

        [Test]
        public void BecauseSample()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => Contractor.CreatePerson("Marcin", "Celej"));

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Not implemented yet"));
        }

        #endregion
    }
}