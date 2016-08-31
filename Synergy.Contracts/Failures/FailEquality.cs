using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        /// <summary>
        /// Throws exception when two values are equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one CANNOT be <see langword="null" />.</para>
        /// </summary>
        /// <param name="unexpected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfEqual([CanBeNull] object unexpected, [CanBeNull] object actual, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (object.Equals(unexpected, actual))
                throw Fail.Because(message, args);
        }

        /// <summary>
        /// Throws exception when argument value is equal to the <paramref name="unexpected"/> value.
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one CANNOT be <see langword="null" />.</para>
        /// </summary>
        /// <param name="unexpected">The unexpected value.</param>
        /// <param name="argumentValue">The argument value to be checked.</param>
        /// <param name="argumentName">Name of the argument passed to your method.</param>
        [AssertionMethod]
        public static void IfArgumentEqual([CanBeNull] object unexpected, [CanBeNull] object argumentValue, [NotNull] string argumentName)
        {
            Fail.RequiresArgumentName(argumentName);

            if (object.Equals(unexpected, argumentValue))
                throw Fail.Because("Argument '{0}' is equal to {1} and it should NOT be.", argumentName, unexpected);
        }

        // TODO: a.FailIfEqual(b)

        /// <summary>
        /// Throws exception when two values are NOT equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one MUST also be <see langword="null" />.</para>
        /// </summary>
        /// <param name="expected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNotEqual([CanBeNull] object expected, [CanBeNull] object actual, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (object.Equals(expected, actual) == false)
                throw Fail.Because(message, args);
        }

        // TODO: IfArgumentNotEqual
        // TODO: a.FailIfNotEqual(b)
    }
}