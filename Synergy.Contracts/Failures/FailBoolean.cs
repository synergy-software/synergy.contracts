using System.Diagnostics;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        /// <summary>
        /// Throws exception when checked value is <see langword="false" />.
        /// </summary>
        /// <param name="value">The value checked against being <see langword="false" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: false => halt")]
        [AssertionMethod]
        public static void IfFalse(
            [AssertionCondition(conditionType: AssertionConditionType.IS_FALSE)] bool value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (value == false)
                throw Fail.Because(message, args);
        }

        /// <summary>
        /// Throws exception when checked value is <see langword="true" />.
        /// </summary>
        /// <param name="value">The value checked against being <see langword="true" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: true => halt")]
        [AssertionMethod]
        public static void IfTrue(
            [AssertionCondition(conditionType: AssertionConditionType.IS_TRUE)] bool value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (value)
                throw Fail.Because(message, args);
        }
    }
}