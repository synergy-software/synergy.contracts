using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailStringTest
    {
        [Test]
        public void IfArgumentNullOrEmpty()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentNullOrEmpty(null, "null")
            );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentNullOrEmpty("", "empty")
            );

            Fail.IfArgumentNullOrEmpty("nie pusty", "nie-pusty");
            Fail.IfArgumentNullOrEmpty("  ", "bia³e-znaki");
        }

        [Test]
        public void IfNullOrEmpty()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNullOrEmpty("", "message")
                );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNullOrEmpty(null, "message")
                );

            Fail.IfNullOrEmpty("   ", "message");
            Fail.IfNullOrEmpty("aa", "message");
        }

        [Test]
        public void IfArgumentNullOrWhiteSpace()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentNullOrWhiteSpace(null, "null")
            );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentNullOrWhiteSpace("", "empty")
            );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentNullOrWhiteSpace("   ", "bia³e-znaki")
            );

            Fail.IfArgumentNullOrWhiteSpace("nie pusty", "nie-pusty");
        }

        [Test]
        public void IfNullOrWhitespace()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNullOrWhitespace("", "message")
                );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNullOrWhitespace(null, "message")
                );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNullOrWhitespace("   ", "message")
                );

            Fail.IfNullOrWhitespace("aa", "message");
        }
    }
}