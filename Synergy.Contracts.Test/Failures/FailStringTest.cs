using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailStringTest
    {
        [Test]
        public void IfArgumentEmpty()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEmpty(null, "null")
            );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEmpty("", "empty")
            );

            Fail.IfArgumentEmpty("nie pusty", "nie-pusty");
            Fail.IfArgumentEmpty("  ", "bia³e-znaki");
        }

        [Test]
        public void IfEmpty()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEmpty("", "message")
                );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEmpty(null, "message")
                );

            Fail.IfEmpty("   ", "message");
            Fail.IfEmpty("aa", "message");
        }

        [Test]
        public void IfArgumentWhiteSpace()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentWhiteSpace(null, "null")
            );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentWhiteSpace("", "empty")
            );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentWhiteSpace("   ", "bia³e-znaki")
            );

            Fail.IfArgumentWhiteSpace("nie pusty", "nie-pusty");
        }

        [Test]
        public void IfWhitespace()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfWhitespace("", "message")
                );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfWhitespace(null, "message")
                );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfWhitespace("   ", "message")
                );

            Fail.IfWhitespace("aa", "message");
        }
    }
}