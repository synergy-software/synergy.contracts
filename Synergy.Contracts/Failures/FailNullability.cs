using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        /// <summary>
        /// Throws exception when provided value is <see langword="null"/>.
        /// <para>REMARKS: This is the most commonly used method to check argument and parameter nullability.</para>
        /// </summary>
        /// <typeparam name="T">Type of the value to check against nullability.</typeparam>
        /// <param name="value">Value to check against nullability.</param>
        /// <param name="name">Name of the checked argument / parameter to check the nullability of.</param>
        /// <returns>The same value that was provided to the function but now it is NOT nullable.</returns>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        [NotNull]
        [AssertionMethod]
        public static T FailIfNull<T>(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] this T value,
            [NotNull] string name)
        {
            Fail.RequiresArgumentName(argumentName: name);

            if (value == null)
                throw Fail.Because("'{0}' is null and it shouldn't be", name);

            return value;
        }

        /// <summary>
        /// Throws exception when provided value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to check against nullability.</typeparam>
        /// <param name="value">Value to check against nullability.</param>
        /// <param name="callerMemberName">Parameter automatically provided by compiler. DO NOT PROVIDE IT.</param>
        /// <param name="callerSourceFilePath">Parameter automatically provided by compiler. DO NOT PROVIDE IT.</param>
        /// <param name="callserSourceLineNumber">Parameter automatically provided by compiler. DO NOT PROVIDE IT.</param>
        /// <returns></returns>
        [ContractAnnotation("value: null => halt; value: notnull => notnull")]
        [AssertionMethod]
        [NotNull]
        public static T OrFail<T>(
            [AssertionCondition(AssertionConditionType.IS_NOT_NULL), CanBeNull] this T value,
            [NotNull, CallerMemberName] string callerMemberName = "",
            [NotNull, CallerFilePath] string callerSourceFilePath = "",
            [CallerLineNumber] int callserSourceLineNumber = 0)
        {
            Fail.IfArgumentWhiteSpace(callerMemberName, nameof(callerMemberName));
            Fail.IfArgumentWhiteSpace(callerSourceFilePath, nameof(callerSourceFilePath));
            Fail.IfArgumentEqual(0, callserSourceLineNumber, nameof(callserSourceLineNumber));

            Fail.IfNull(value, $"Object of type {typeof(T).Name} is null in {callerMemberName}() method [{callerSourceFilePath}({callserSourceLineNumber})]");

            return value;
        }

        /// <summary>
        /// Throws exception when specified argument value is <see langword="null" />.
        /// </summary>
        /// <param name="argumentValue">Value of the argument to check against being <see langword="null" />.</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        [ContractAnnotation("argumentValue: null => halt")]
        [AssertionMethod]
        public static void IfArgumentNull(
            [CanBeNull] [AssertionCondition(conditionType: AssertionConditionType.IS_NOT_NULL)] object argumentValue,
            [NotNull] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName: argumentName);

            if (argumentValue == null)
                throw Fail.Because("Argument '{0}' was null.", argumentName);
        }

        /// <summary>
        /// Throws exception when specified value is <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being <see langword="null" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: null => halt")]
        [AssertionMethod]
        public static void IfNull(
            [CanBeNull] [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] object value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (value == null)
                throw Fail.Because(message, args);
        }

        /// <summary>
        /// Throws exception when specified value is NOT <see langword="null" />.
        /// </summary>
        /// <param name="value">Value to check against being NOT <see langword="null" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: notnull => halt")]
        [AssertionMethod]
        public static void IfNotNull([CanBeNull] object value, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (value != null)
                throw Fail.Because(message, args);
        }
    }
}