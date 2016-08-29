using System;
using NUnit.Framework;
using Synergy.Contracts.Samples;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailGuidTest
    {
        [Test]
        public void IfArgumentEmpty()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEmpty(Guid.Empty, "argumentName"));

            Fail.IfArgumentEmpty(Guid.NewGuid(), "argumentName");
        }

        [Test]
        public void IfArgumentEmptySample()
        {
            // ARRANGE
            Console.WriteLine("Guid.Empty = " + Guid.Empty);
            Console.WriteLine("new Guid() = " + new Guid());

            IContractorRepository contractorRepository = new ContractorRepository();
            var id = new Guid();

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => contractorRepository.FindContractorByGuid(id: id));

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo($"Argument '{nameof(id)}' was an empty Guid."));
        }

        [Test]
        public void IfEmpty()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEmpty(Guid.Empty, "guid is empty"));

            Fail.IfEmpty(Guid.NewGuid(), "guid is empty");
        }
    }
}