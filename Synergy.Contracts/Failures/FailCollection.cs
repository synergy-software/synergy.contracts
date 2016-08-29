using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        /// <summary>
        /// Sprawdza czy podana kolekcja nie jest <see langword="null" /> lub pusta.
        /// </summary>
        [DebuggerStepThrough]
        [ContractAnnotation("collection: null=> halt")]
        [AssertionMethod]
        public static void IfCollectionNullOrEmpty<T>(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection,
            [NotNull] string collectionName)
        {
            RequiresArgumentName(collectionName);

            if (collection == null)
                throw Because("Collection {0} should not be null but it is.", collectionName);

            if (collection.Any() == false)
                throw Because("Collection {0} should not be empty but it is.", collectionName);
        }

        /// <summary>
        /// Sprawdza czy podana kolekcja nie zawiera <see langword="null" />.
        /// Przekazana kolekcja nie mo¿e byæ null, ale mo¿e byæ pusta.
        /// </summary>
        [DebuggerStepThrough]
        [ContractAnnotation("collection: null=> halt")]
        [AssertionMethod]
        public static void IfCollectionContainsNull<T>(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection,
            [NotNull] string collectionName) where T : class
        {
            RequiresArgumentName(collectionName);
            IfArgumentNull(collection, nameof(collection));

            IfTrue(collection.Contains(null), "Collection {0} contains null", collectionName);
        }

        /// <summary>
        /// Sprawdza czy podana kolekcja nie zawiera elementu spe³niaj¹cego podany warunek.
        /// Przekazana kolekcja nie mo¿e byæ null, ale mo¿e byæ pusta.
        /// </summary>
        [StringFormatMethod("message")]
        [ContractAnnotation("collection: null=> halt")]
        [AssertionMethod]
        public static void IfCollectionContains<T>(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection,
            [NotNull] Func<T, bool> func,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            RequiresMessage(message, args);
            IfArgumentNull(collection, nameof(collection));

            T element = collection.FirstOrDefault(func);
            IfNotNull(element, message, args);
        }

        //public static void IfCollectionDoesNotContain<T>([CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection,) 

        /// <summary>
        /// Sprawdza czy podane kolekcje zawieraj¹ te same elementy (u³o¿one w dowolnej kolejnoœci).
        /// Przekazane kolekcje nie mog¹e byæ null, ale mog¹ byæ puste.
        /// </summary>
        [StringFormatMethod("message")]
        [ContractAnnotation("collection1: null=> halt; collection2: null=> halt")]
        [AssertionMethod]
        public static void IfCollectionsAreNotEquivalent<T>(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection1,
            [NotNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection2,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            RequiresMessage(message, args);
            IfArgumentNull(collection1, nameof(collection1));
            IfArgumentNull(collection2, nameof(collection2));

            int collection1Count = collection1.Count();
            int collection2Count = collection2.Count();
            bool areEquivalent = collection1Count == collection2Count && collection1.Intersect(collection2).Count() == collection1Count;
            IfFalse(areEquivalent, message, args);
        }
    }
}