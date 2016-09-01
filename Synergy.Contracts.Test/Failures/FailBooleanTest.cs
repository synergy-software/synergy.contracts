using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailBooleanTest
    {
        #region Fail.IfFalse

        [Test]
        public void IfFalse()
        {
            // ARRANGE
            bool someFalseValue = false;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfFalse(someFalseValue, "this should be true")
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be true"));
        }

        [Test]
        public void IfFalseSuccess()
        {
            // ARRANGE
            var someTrueValue = true;

            // ACT
            Fail.IfFalse(someTrueValue, "this should be true");
        }

        #endregion

        #region Fail.IfTrue

        [Test]
        public void IfTrue()
        {
            // ARRANGE
            var someTrueValue = true;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfTrue(someTrueValue, "this should be false")
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be false"));
        }

        [Test]
        public void IfTrueSuccess()
        {
            // ARRANGE
            var someFalseValue = false;

            // ACT
            Fail.IfTrue(someFalseValue, "this should be false");
        }

        #endregion
    }
}