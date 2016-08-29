using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        /// <summary>
        ///     Rzuca wyj¹tek gdy argument przekazany do metody jest <see langword="null" /> lub "" (pusty string).
        /// </summary>
        [DebuggerStepThrough]
        [ContractAnnotation("argumentValue: null => halt")]
        [AssertionMethod]
        public static void IfArgumentNullOrEmpty(
            [CanBeNull, AssertionCondition(conditionType: AssertionConditionType.IS_NOT_NULL)] string argumentValue,
            [NotNull] string argumentName)
        {
            RequiresArgumentName(argumentName: argumentName);

            IfArgumentNull(argumentValue: argumentValue, argumentName: argumentName);

            if (argumentValue.Length == 0)
                throw Because("Argument '{0}' was empty.", argumentName);
        }

        /// <summary>
        /// Rzuca wyj¹tek gdy testowany parametr jest <see langword="null" /> lub "" (pusty string).
        /// </summary>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNullOrEmpty(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            RequiresMessage(message, args);

            IfNull(value, message, args);

            if (value.Length == 0)
                throw Because(message, args);
        }

        /// <summary>
        ///     Rzuca wyj¹tek gdy argument przekazany do metody jest <see langword="null" /> lub sk³ada siê tylko z bia³ych znaków.
        /// </summary>
        [DebuggerStepThrough]
        [ContractAnnotation("argumentValue: null => halt")]
        [AssertionMethod]
        public static void IfArgumentNullOrWhiteSpace(
            [CanBeNull, AssertionCondition(conditionType: AssertionConditionType.IS_NOT_NULL)] string argumentValue,
            [NotNull] string argumentName)
        {
            RequiresArgumentName(argumentName: argumentName);

            IfArgumentNull(argumentValue: argumentValue, argumentName: argumentName);

            if (string.IsNullOrWhiteSpace(value: argumentValue))
                throw Because("Argument '{0}' was empty.", argumentName);
        }

        /// <summary>
        /// Rzuca wyj¹tek gdy testowany parametr jest <see langword="null" /> 
        /// lub jest tekstem zawieraj¹cym jedynie bia³e znaki.
        /// </summary>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNullOrWhitespace(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            RequiresMessage(message, args);

            IfNull(value, message, args);

            if (String.IsNullOrWhiteSpace(value))
                throw Because(message, args);
        }

        //public static void FailIfArgumentNotCastable<T>([CanBeNull, AssertionCondition(conditionType: AssertionConditionType.IS_NOT_NULL)] string argumentValue)

        /// <summary>
        ///     Sprawdza czy przekazano argument 'argumentName'.
        /// </summary>
        [ExcludeFromCodeCoverage]
        private static void RequiresArgumentName([NotNull] string argumentName)
        {
            if (string.IsNullOrWhiteSpace(value: argumentName))
                throw new ArgumentNullException(nameof(argumentName));
        }
    }
}