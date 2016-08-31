using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailEqualityTest
    {
        #region Fail.IfEqual()

        [Test]
        public void IfEqual()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEqual("s1", "s1", "values are equal and shouldn't be")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("values are equal and shouldn't be"));
        }

        [Test]
        public void IfEqualNulls()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEqual(null, null, "values are equal and shouldn't be")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("values are equal and shouldn't be"));
        }

        [Test]
        public void IfEqualSuccess()
        {
            // ACT
            Fail.IfEqual("s1", "s2", "values are equal and shouldn't be");
        }

        #endregion

        #region Fail.IfArgumentEqual()

        [Test]
        public void IfArgumentEqual([Values(0)] int argument)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEqual(argument, argument, nameof(argument))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Argument 'argument' is equal to 0 and it should NOT be."));
        }

        [Test]
        public void IfArgumentEqualSuccess([Values(1)] int argument)
        {
            // ACT
            Fail.IfArgumentEqual(argument - 1, argument, nameof(argument));
        }

        #endregion

        #region Fail.IfNotEqual()

        [Test]
        public void IfNotEqual()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotEqual("s1", "s2", "values differ and should be equal")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("values differ and should be equal"));
        }

        [Test]
        public void IfNotEqualNulls()
        {
            // ACT
            Fail.IfNotEqual("s1", "s1", "values differ and should be equal");
        }

        [Test]
        public void IfNotEqualSuccess()
        {
            Fail.IfNotEqual(null, null, "values differ and should be equal");
        }

        #endregion
    }
}