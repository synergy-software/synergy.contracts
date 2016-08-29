using NUnit.Framework;
using Synergy.Contracts.Samples.Domain;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailTest
    {
        //[Test]
        //public void Always()
        //{
        //    Assert.Throws<DesignByContractViolationException>(
        //        () => Fail.Always("Always")
        //        );
        //}

        [Test]
        public void Because()
        {
            // ACT
            DesignByContractViolationException exception = Fail.Because("Always");

            // ASSERT
            Assert.NotNull(anObject: exception);
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
                () => contractor.GetName()
            );

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Unsupported ContractorType: 0. Maybe someone extended enum and forgot about this logic?"));
        }
    }
}