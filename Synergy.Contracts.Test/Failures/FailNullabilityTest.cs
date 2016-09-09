using System;
using NUnit.Framework;
using Synergy.Contracts.Samples;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailNullabilityTest
    {
        #region variable.FailIfNull(nameof(variable))

        [Test]
        public void FailIfNull()
        {
            // ARRANGE
            object someNullObject = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => someNullObject.FailIfNull(nameof(someNullObject))
            );

           // ASSERT
           Assert.That(exception.Message, Is.EqualTo("'someNullObject' is null and it shouldn't be"));
        }

        [Test]
        public void FailIfNullOnNullableValue()
        {
            // ARRANGE
            long? someNullableLong = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => someNullableLong.FailIfNull(nameof(someNullableLong))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'someNullableLong' is null and it shouldn't be"));
        }

        [Test]
        public void FailIfNullSuccess()
        {
            // ARRANGE
            var thisIsNotNull = new object();

            // ACT
            thisIsNotNull.FailIfNull(nameof(thisIsNotNull));

            long? itIsNotNull = 123;
            itIsNotNull.FailIfNull(nameof(itIsNotNull));
        }

        [Test]
        public void FailIfNullSuccessOnNullableValue()
        {
            // ARRANGE
            long? thisIsNotNull = 123;

            // ACT
            thisIsNotNull.FailIfNull(nameof(thisIsNotNull));
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

        #endregion

        #region variable.OrFail()

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

        #endregion

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