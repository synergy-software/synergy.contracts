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