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
        public void AsOrFailSuccess()
        {
            // ARRANGE 
            object somethingCastable = "text";

            // ACT
            somethingCastable.AsOrFail<string>();
        }

        [Test]
        public void AsOrFailSuccessWithNull()
        {
            // ARRANGE 
            object somethingCastable = null;

            // ACT
            // ReSharper disable once ExpressionIsAlwaysNull
            somethingCastable.AsOrFail<string>();
        }

        #endregion

        #region object.CastOrFail<T>()

        [Test]
        public void CastOrFail()
        {
            // ARRANGE
            object somethingNotCastable = 1;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => somethingNotCastable.CastOrFail<string>()
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Expected object of type 'System.String' but was '1'"));
        }

        [Test]
        public void CastOrFailWithNull()
        {
            // ARRANGE
            object somethingNotCastable = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => somethingNotCastable.CastOrFail<string>()
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Expected object of type 'System.String' but was 'null'"));
        }

        [Test]
        public void CastOrFailSuccess()
        {
            // ARRANGE
            var somethingCastable = "text";

            // ACT
            somethingCastable.CastOrFail<string>();
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