using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        /// <summary>
        /// Throws exception when the collection is <see langword="null" /> or empty.
        /// </summary>
        /// <typeparam name="T">Type of the collection element.</typeparam>
        /// <param name="collection">Collection to check against being <see langword="null" /> or empty.</param>
        /// <param name="collectionName">Name of the collection.</param>
        [DebuggerStepThrough]
        [ContractAnnotation("collection: null => halt")]
        [AssertionMethod]
        public static void IfCollectionNullOrEmpty<T>(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection,
            [NotNull] string collectionName)
        {
            RequiresCollectionName(collectionName);

            if (collection == null)
                throw Because("Collection '{0}' should not be null but it is.", collectionName);

            if (collection.Any() == false)
                throw Because("Collection '{0}' should not be empty but it is.", collectionName);
        }

        /// <summary>
        /// Throws exception when the collection contains null.
        /// <para>REMARKS: The provided collection CANNOT by <see langword="null"/> as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">Type of the collection element.</typeparam>
        /// <param name="collection">Collection to investigate whether contains null.</param>
        /// <param name="collectionName">Name of the collection</param>
        [DebuggerStepThrough]
        [ContractAnnotation("collection: null => halt")]
        [AssertionMethod]
        public static void IfCollectionContainsNull<T>(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection,
            [NotNull] string collectionName) where T : class
        {
            RequiresCollectionName(collectionName);

            IfArgumentNull(collection, nameof(collection));
            IfTrue(collection.Contains(null), "Collection '{0}' contains null", collectionName);
        }

        /// <summary>
        /// Throws the exception when collection contains element meeting specified criteria.
        /// <para>REMARKS: The provided collection CANNOT by <see langword="null"/> as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">Type of the collection element.</typeparam>
        /// <param name="collection">Collection to investigate whether contains null.</param>
        /// <param name="func">Function with criteria that at least one element must meet.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("collection: null => halt")]
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

        //TODO: public static void IfCollectionDoesNotContain<T>([CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection,) 

        /// <summary>
        /// Throws exception when the specified collections are not equivalent. Equivalent collection contain the same elements in any order.
        /// <para>REMARKS: The provided collection CANNOT by <see langword="null"/> as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">Type of the collection element.</typeparam>
        /// <param name="collection1">First collection to compare.</param>
        /// <param name="collection2">Second collection to compare.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("collection1: null => halt; collection2: null => halt")]
        [AssertionMethod]
        public static void IfCollectionsAreNotEquivalent<T>(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection1,
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection2,
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

        /// <summary>
        /// Checks if collection name was provided.
        /// </summary>
        [ExcludeFromCodeCoverage]
        private static void RequiresCollectionName([NotNull] string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName));
        }
    }
}