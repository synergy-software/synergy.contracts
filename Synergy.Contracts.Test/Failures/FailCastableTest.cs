using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailCastableTest
    {
        #region object.AsOrFail<T>()

        [Test]
        public void AsOrFail()
        {
            // ARRANGE
            var someObjectButSurelyNotString = new object();

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => someObjectButSurelyNotString.AsOrFail<string>()
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Expected object of type 'System.String' but was 'System.Object'"));
        }

        [Test]
        [TestCase("text")]
        [TestCase(null)]
        public void AsOrFailSuccess(string toCast)
        {
            // ACT
            toCast.AsOrFail<string>();
        }

        #endregion

        #region object.CastOrFail<T>()

        [Test]
        public void CastOrFail()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => new object().CastOrFail<string>()
            );

            Assert.Throws<DesignByContractViolationException>(
                () => ((object) null).CastOrFail<string>()
            );

            "tekst".CastOrFail<string>();
        }

        #endregion

        #region Fail.IfNotCastable<T>()

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
                () => Fail.IfNotCastable(new object(), typeof(IQueryable), "wrong type 1")
            );

            Fail.IfNotCastable(new List<string>(), typeof(IList<string>), "wrong type 2");
        }

        #endregion

        #region Fail.IfNullOrNotCastable<T>()

        [Test]
        public void IfNullOrNotCastable()
        {
            Fail.IfNullOrNotCastable<IList<string>>(new List<string>());
            Fail.IfNullOrNotCastable<IList<string>>(new List<string>(), "wrong type 1");

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNullOrNotCastable<IQueryable>(new object())
            );
        }

        #endregion
    }
}