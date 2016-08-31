using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailCollectionTest
    {
        [Test]
        public void IfCollectionContains()
        {
            var kolekcja = new[] {new object(), "element"};

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionContains(kolekcja, e => object.Equals(e, "element"), "ta kolekcja ma 'ala'")
            );

            Fail.IfCollectionContains(kolekcja, e => object.Equals(e, "dziwny"), "ta kolekcja NIE ma elementu dziwnego");
        }

        [Test]
        public void IfCollectionContainsNull()
        {
            var zNullem = new[] {new object(), null};
            IEnumerable<string> pełna = Enumerable.Repeat("element", 2);

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionContainsNull(zNullem, "zNullem")
            );

            Fail.IfCollectionContainsNull(pełna, "pełna");
        }

        [Test]
        public void IfCollectionNullOrEmpty()
        {
            IEnumerable<object> pusta = Enumerable.Empty<object>();
            IEnumerable<object> nullowata = null;
            IEnumerable<string> pełna = Enumerable.Repeat("element", 2);

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionEmpty(pusta, "collection")
            );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionEmpty(nullowata, "collection")
            );

            Fail.IfCollectionEmpty(pełna, "collection");
        }

        [Test]
        public void IfCollectionsAreNotEquivalent()
        {
            var kolekcja1 = new[] {"ala", "olo"};
            var kolekcja1InnaKolejność = new[] {"olo", "ala"};
            var kolekcja2 = new[] {"ala", "inna"};
            var pusta1 = new string[0];
            var pusta2 = new List<string>();

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionsAreNotEquivalent(kolekcja1, kolekcja2, "są różne")
            );

            Fail.IfCollectionsAreNotEquivalent(kolekcja1, kolekcja1InnaKolejność, "są różne");
            Fail.IfCollectionsAreNotEquivalent(pusta1, pusta2, "są różne");
        }
    }
}