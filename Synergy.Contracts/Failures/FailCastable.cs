using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    //TODO: Add [AssertionCondition] below
    public static partial class Fail
    {
        private const string NotCastableMessage = "Expected object of type '{0}' but was '{1}'";

        //TODO: public static void FailIfArgumentNotCastable<T>([CanBeNull, AssertionCondition(conditionType: AssertionConditionType.IS_NOT_NULL)] string argumentValue)

        /// <summary>
        /// Throws exception when specified value is not castable to the specified type. It also returns the casted object or <see langword="null"/>.
        /// <para>REMARKS: You can pass <see langword="null"/> to this method and will NOT throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="value">Value to check if it can be casted to specified type.</param>
        /// <returns>The casted object (or <see langword="null"/>).</returns>
        [DebuggerStepThrough]
        [ContractAnnotation("value: null => null; value: notnull => notnull")]
        [CanBeNull]
        [AssertionMethod]
        public static T AsOrFail<T>([CanBeNull] this object value)
        {
            Fail.IfNotCastable<T>(value, Fail.NotCastableMessage, typeof(T), value);

            return (T) value;
        }

        /// <summary>
        /// Throws exception when specified value is not castable to the specified type. It also returns the casted object.
        /// <para>REMARKS: You CANNOT pass <see langword="null"/> to this method as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="value">Value to check if it can be casted to specified type.</param>
        /// <returns>The casted object. This method will NEVER return <see langword="null"/>.</returns>
        [DebuggerStepThrough]
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        [NotNull]
        [AssertionMethod]
        public static T CastOrFail<T>([CanBeNull] this object value)
        {
            Fail.IfNull(value, Fail.NotCastableMessage, typeof(T), "null");
            Fail.IfNotCastable<T>(value, Fail.NotCastableMessage, typeof(T), value);
            return (T) value;
        }

        /// <summary>
        /// Throws exception when specified value is not castable to the specified type.
        /// <para>REMARKS: You can pass <see langword="null"/> to this method and will NOT throw the exception.</para>
        /// </summary>
        /// <param name="value">Value to check if it can be casted to specified type.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNotCastable([CanBeNull] object value, [NotNull] Type expectedType, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.RequiresType(expectedType);
            Fail.RequiresMessage(message, args);

            if (value == null)
                return;

            if (expectedType.IsInstanceOfType(value) == false)
                throw Fail.Because(message, args);
        }

        /// <summary>
        /// Throws exception when specified value is not castable to the specified type.
        /// <para>REMARKS: You can pass <see langword="null"/> to this method and will NOT throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected Type.</typeparam>
        /// <param name="value">Value to check if it can be casted to specified type.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNotCastable<T>([CanBeNull] object value, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.IfNotCastable(value, typeof(T), message, args);
        }

        /// <summary>
        /// Throws exception when specified value is not castable to the specified type.
        /// <para>REMARKS: You CANNOT pass <see langword="null"/> to this method as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected Type.</typeparam>
        /// <param name="value">Value to check if it can be casted to specified type.</param>
        [DebuggerStepThrough]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNullOrNotCastable<T>([CanBeNull] object value)
        {
            Fail.IfNullOrNotCastable<T>(value, Fail.NotCastableMessage, typeof(T), value);
        }

        /// <summary>
        /// Throws exception when specified value is not castable to the specified type.
        /// <para>REMARKS: You CANNOT pass <see langword="null"/> to this method as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">The expected Type.</typeparam>
        /// <param name="value">Value to check if it can be casted to specified type.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [DebuggerStepThrough]
        [ContractAnnotation("value: null => halt")]
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNullOrNotCastable<T>(
            [CanBeNull] object value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            Fail.IfNull(value, message, args);
            Fail.IfNotCastable(value, typeof(T), message, args);
        }

        [ExcludeFromCodeCoverage]
        private static void RequiresType([NotNull] Type expectedType)
        {
            if (expectedType == null)
                throw new ArgumentNullException(nameof(expectedType));
        }
    }
}