using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        #region Fail.IfFalse

        /// <summary>
        /// Throws exception when checked value is <see langword="false" />.
        /// </summary>
        /// <param name="value">The value checked against being <see langword="false" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: false => halt")]
        [AssertionMethod]
        public static void IfFalse(
            [AssertionCondition(AssertionConditionType.IS_FALSE)] bool value,
            [NotNull] string message)
        {
            Fail.IfFalse(value, message, Fail.nullArgs);
        }

        /// <summary>
        /// Throws exception when checked value is <see langword="false" />.
        /// </summary>
        /// <param name="value">The value checked against being <see langword="false" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="arg1">Meassege argument 1</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: false => halt")]
        [AssertionMethod]
        public static void IfFalse<TArgument1>(
            [AssertionCondition(AssertionConditionType.IS_FALSE)] bool value,
            [NotNull] string message,
            [CanBeNull] TArgument1 arg1)
        {
            Fail.IfFalse(value, message, new object[] { arg1 });
        }

        /// <summary>
        /// Throws exception when checked value is <see langword="false" />.
        /// </summary>
        /// <param name="value">The value checked against being <see langword="false" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="arg1">Meassege argument 1</param>
        /// <param name="arg2">Meassege argument 2</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: false => halt")]
        [AssertionMethod]
        public static void IfFalse<TArgument1, TArgument2>(
            [AssertionCondition(AssertionConditionType.IS_FALSE)] bool value,
            [NotNull] string message,
            [CanBeNull] TArgument1 arg1,
            [CanBeNull] TArgument2 arg2)
        {
            Fail.IfFalse(value, message, new object[] { arg1, arg2});
        }

        /// <summary>
        /// Throws exception when checked value is <see langword="false" />.
        /// </summary>
        /// <param name="value">The value checked against being <see langword="false" />.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="arg1">Meassege argument 1</param>
        /// <param name="arg2">Meassege argument 2</param>
        /// <param name="arg3">Meassege argument 3</param>
        [StringFormatMethod("message")]
        [ContractAnnotation("value: false => halt")]
        [AssertionMethod]
        public static void IfFalse<TArgument1, TArgument2, TArgument3>(
            [AssertionCondition(AssertionConditionType.IS_FALSE)] bool value,
            [NotNull] string message,
            [CanBeNull] TArgument1 arg1,
            [CanBeNull] TArgument2 arg2,
            [CanBeNull] TArgument3 arg3)
        {
            Fail.IfFalse(value, message, new object[] { arg1, arg2, arg3 });
        }

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
            [AssertionCondition(AssertionConditionType.IS_FALSE)] bool value,
            [NotNull] string message,
            [CanBeNull] params object[] args)
        {
            Fail.RequiresMessage(message);

            if (value == false)
                throw Fail.Because(message, args);
        }

        #endregion

        // TODO:mace (from:mace @ 22-10-2016): variable.FailIfFalse(nameof(variable))

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
            [AssertionCondition(AssertionConditionType.IS_TRUE)] bool value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (value)
                throw Fail.Because(message, args);
        }

        // TODO:mace (from:mace @ 22-10-2016): variable.FailIfTrue(nameof(variable))
    }
}