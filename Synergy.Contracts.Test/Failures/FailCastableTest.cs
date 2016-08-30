using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailCastableTest
    {
        [Test]
        public void CastOrFail()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => new object().AsOrFail<string>()
                );

            "text".AsOrFail<string>();
            ((object) null).AsOrFail<string>();
        }

        [Test]
        public void CastOrFailNotNull()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => new object().CastOrFail<string>()
                );

            Assert.Throws<DesignByContractViolationException>(
                () => ((object) null).CastOrFail<string>()
                );

            "tekst".CastOrFail<string>();
        }

        [Test]
        public void IfNotCastable()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotCastable<IQueryable>(new object(), "wrong type 1")
                );

            Fail.IfNotCastable<IList<string>>(new List<string>(), "wrong type 2");
        }

        [Test]
        public void IfNotCastableWithType()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotCastable(new object(), typeof (IQueryable), "wrong type 1")
                );

            Fail.IfNotCastable(new List<string>(),typeof (IList<string>), "wrong type 2");
        }

        [Test]
        public void IfNullOrNotCastable()
        {
            Fail.IfNullOrNotCastable<IList<string>>(new List<string>());
            Fail.IfNullOrNotCastable<IList<string>>(new List<string>(), "wrong type 1");

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNullOrNotCastable<IQueryable>(new object())
                );
        }
    }
}