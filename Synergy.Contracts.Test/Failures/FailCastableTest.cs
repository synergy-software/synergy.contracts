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

            "tekst".AsOrFail<string>();
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
                () => Fail.IfNotCastable<IQueryable>(toCheck: new object(), message: "nie zrzutuje")
                );

            Fail.IfNotCastable<IList<string>>(toCheck: new List<string>(), message: "zrzutuje");
        }

        [Test]
        public void IfNotCastableWithType()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotCastable(value: new object(), expectedType: typeof (IQueryable), message: "nie zrzutuje")
                );

            Fail.IfNotCastable(value: new List<string>(), expectedType: typeof (IList<string>), message: "zrzutuje");
        }

        [Test]
        public void IfNullOrNotCastable()
        {
            Fail.IfNullOrNotCastable<IList<string>>(value: new List<string>());
            Fail.IfNullOrNotCastable<IList<string>>(value: new List<string>(), message: "wszystko ok");

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNullOrNotCastable<IQueryable>(value: new object())
                );
        }
    }
}