using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailBooleanTest
    {
        [Test]
        public void IfFalse()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfFalse(false, "this should be true - first check")
                );

            Fail.IfFalse(true, "this should be true - second check");
        }

        [Test]
        public void IfTrue()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfTrue(true, "this should be false - first check")
                );

            Fail.IfTrue(false, "this should be false - second check");
        }
    }
}