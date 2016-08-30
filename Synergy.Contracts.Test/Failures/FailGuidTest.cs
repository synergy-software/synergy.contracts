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
            // ARRANGE
            var someEmptyGuid = Guid.Empty;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEmpty(someEmptyGuid, nameof(someEmptyGuid)));

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Argument 'someEmptyGuid' is an empty Guid."));
        }

        [Test]
        public void IfArgumentEmptySuccess()
        {
            // ARRANGE
            var someGuid = Guid.NewGuid();

            // ACT
            Fail.IfArgumentEmpty(someGuid, nameof(someGuid));
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
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => contractorRepository.FindContractorByGuid(id: id));

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo($"Argument '{nameof(id)}' is an empty Guid."));
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