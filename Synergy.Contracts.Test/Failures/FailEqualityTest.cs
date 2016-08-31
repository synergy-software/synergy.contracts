using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailEqualityTest
    {
        [Test]
        public void IfEqual()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEqual("s1", "s1", "s¹ równe"));

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEqual(null, null, "s¹ równe"));

            Fail.IfEqual("s1", "s2", "nie s¹ równe");
        }

        [Test]
        public void IfArgumentEqual([Values(0)]int argument)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEqual(argument, argument, nameof(argument)));

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Argument 'argument' is equal to 0 and it should NOT be."));
        }

        [Test]
        public void IfArgumentEqualSuccess([Values(1)] int argument)
        {
            // ACT
            Fail.IfArgumentEqual(argument - 1, argument, nameof(argument));
        }

        [Test]
        public void IfNotEqual()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotEqual("s1", "s2", "values differ - first check")
                );

            Fail.IfNotEqual("s1", "s1", "values differ - second check");
            Fail.IfNotEqual(null, null, "values differ - third check");
        }
    }
}