using System;
using NUnit.Framework;
using Synergy.Contracts.Samples;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailNullabilityTest
    {
        [Test]
        public void FailIfNull()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => ((object)null).FailIfNull("to jest null")
            );

            new object().FailIfNull("to nie null");

            long? itIsNull = null;
            Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => itIsNull.FailIfNull(nameof(itIsNull))
            );

            long? itIsNotNull = 123;
            itIsNotNull.FailIfNull(nameof(itIsNotNull));
        }

        [Test]
        public void OrFail()
        {
            // ARRANGE
            string thisMustBeNull = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => thisMustBeNull.OrFail());

            // ASSERT
            Console.WriteLine(exception.Message);
        }

        [Test]
        public void OrFailSuccess()
        {
            // ARRANGE
            var thisCannotBeNull = "i am not null";

            // ACT
            thisCannotBeNull.OrFail();
        }

        [Test]
        public void FailIfNullSample()
        {
            // ARRANGE
            IContractorRepository repository = new ContractorRepository();
            var parameters = new ContractorFilterParameters();

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => repository.FilterContractors(paramaters: parameters)
            );

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("'FoundedBetween' is null and it shouldn't be"));
        }

        [Test]
        public void IfArgumentNull()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentNull(null, "null")
            );

            Fail.IfArgumentNull(new object(), "nie-null");
        }

        [Test]
        public void IfNotNull()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotNull("not null", "to nie null")
            );

            Fail.IfNotNull(null, "to null");
        }

        [Test]
        public void IfNull()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNull(null, "to jest null")
            );

            Fail.IfNull(value: new object(), message: "to nie null");
        }
    }
}