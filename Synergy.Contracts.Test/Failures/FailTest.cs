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

        [Test]
        public void BecauseSample()
        {
            // ARRANGE
            var contractor = new Contractor
            {
                Type = 0
            };

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => contractor.GetName()
            );

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Unsupported ContractorType: 0. Maybe someone extended enum and forgot about this logic?"));
        }
    }
}