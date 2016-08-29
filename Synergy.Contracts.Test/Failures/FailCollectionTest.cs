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
            var kolekcja = new[] {new object(), "ala"};

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionContains(collection: kolekcja, func: e => Equals(objA: e, objB: "ala"), message: "ta kolekcja ma 'ala'")
                );

            Fail.IfCollectionContains(collection: kolekcja, func: e => Equals(objA: e, objB: "dziwny"), message: "ta kolekcja NIE ma elementu dziwnego");
        }

        [Test]
        public void IfCollectionContainsNull()
        {
            var zNullem = new[] {new object(), null};
            IEnumerable<string> pe³na = Enumerable.Repeat("element", 2);

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionContainsNull(collection: zNullem, collectionName: "zNullem")
                );

            Fail.IfCollectionContainsNull(collection: pe³na, collectionName: "pe³na");
        }

        [Test]
        public void IfCollectionNullOrEmpty()
        {
            IEnumerable<object> pusta = Enumerable.Empty<object>();
            IEnumerable<object> nullowata = null;
            IEnumerable<string> pe³na = Enumerable.Repeat("element", 2);

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionNullOrEmpty(collection: pusta, collectionName: "collection")
                );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionNullOrEmpty(collection: nullowata, collectionName: "collection")
                );

            Fail.IfCollectionNullOrEmpty(collection: pe³na, collectionName: "collection");
        }

        [Test]
        public void IfCollectionsAreNotEquivalent()
        {
            var kolekcja1 = new[] {"ala", "olo"};
            var kolekcja1InnaKolejnoœæ = new[] {"olo", "ala"};
            var kolekcja2 = new[] {"ala", "inna"};
            var pusta1 = new string[0];
            var pusta2 = new List<string>();

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionsAreNotEquivalent(collection1: kolekcja1, collection2: kolekcja2, message: "s¹ ró¿ne")
                );

            Fail.IfCollectionsAreNotEquivalent(collection1: kolekcja1, collection2: kolekcja1InnaKolejnoœæ, message: "s¹ ró¿ne");
            Fail.IfCollectionsAreNotEquivalent(collection1: pusta1, collection2: pusta2, message: "s¹ ró¿ne");
        }
    }
}