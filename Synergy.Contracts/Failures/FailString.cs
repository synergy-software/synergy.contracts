using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        /// <summary>
        /// Throws exception if the specified argument is <see langword="null"/> or empty string ("").
        /// </summary>
        /// <param name="argumentValue">Value of the argument to check against <see langword="null"/> or emptiness.</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        [DebuggerStepThrough]
        [ContractAnnotation("argumentValue: null => halt")]
        [AssertionMethod]
        public static void IfArgumentNullOrEmpty(
            [CanBeNull, AssertionCondition(conditionType: AssertionConditionType.IS_NOT_NULL)] string argumentValue,
            [NotNull] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName);

            Fail.IfArgumentNull(argumentValue, argumentName);

            if (argumentValue.Length == 0)
                throw Fail.Because("Argument '{0}' was empty.", argumentName);
        }

        /// <summary>
        /// Throws exception if the specified value is <see langword="null"/> or empty string ("").
        /// </summary>
        /// <param name="value">Value to check against <see langword="null"/> or emptiness.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNullOrEmpty(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            Fail.IfNull(value, message, args);

            if (value.Length == 0)
                throw Fail.Because(message, args);
        }

        /// <summary>
        ///  Throws exception if the specified argument value is <see langword="null"/> or white space.
        /// </summary>
        /// <param name="argumentValue">Value of the argument to check</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        [DebuggerStepThrough]
        [ContractAnnotation("argumentValue: null => halt")]
        [AssertionMethod]
        public static void IfArgumentNullOrWhiteSpace(
            [CanBeNull, AssertionCondition(conditionType: AssertionConditionType.IS_NOT_NULL)] string argumentValue,
            [NotNull] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName);

            Fail.IfArgumentNull( argumentValue,  argumentName);

            if (string.IsNullOrWhiteSpace(value: argumentValue))
                throw Fail.Because("Argument '{0}' was empty.", argumentName);
        }

        /// <summary>
        /// Throws exception if the specified value is <see langword="null"/> or white space.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNullOrWhitespace(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            Fail.IfNull(value, message, args);

            if (string.IsNullOrWhiteSpace(value))
                throw Fail.Because(message, args);
        }


        /// <summary>
        /// Checks if argument name was provided.
        /// </summary>
        [ExcludeFromCodeCoverage]
        private static void RequiresArgumentName([NotNull] string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argumentName))
                throw new ArgumentNullException(nameof(argumentName));
        }
    }
}