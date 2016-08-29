using System.Diagnostics;
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
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfEqual([CanBeNull] object unexpected, [CanBeNull] object actual, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (object.Equals(unexpected, actual))
                throw Fail.Because(message, args);
        }

        /// <summary>
        /// Throws exception when two values are NOT equal. 
        /// <para>REMARKS: If one of the values is <see langword="null" /> the other one must also be <see langword="null" />.</para>
        /// </summary>
        /// <param name="expected">The unexpected value.</param>
        /// <param name="actual">The actual value to be checked.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        /// <param name="args">Arguments that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNotEqual([CanBeNull] object expected, [CanBeNull] object actual, [NotNull] string message, [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            if (object.Equals(expected, actual) == false)
                throw Fail.Because(message, args);
        }
    }
}