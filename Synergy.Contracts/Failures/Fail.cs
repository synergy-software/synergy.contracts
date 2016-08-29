using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    /// <summary>
    /// <para> 
    /// Static class for one-line contract checks. When contract is failed it throws <see cref="DesignByContractViolationException"/>.
    /// Use it widely on every single method.
    /// </para>
    /// <para>
    /// - consider each method separately - what contract this method has |
    /// - check method arguments for contract violation in the beginning of your method |
    /// - keep an empty line after the arguments contract checks |
    /// - keep the contract checks one line long - do not disturb your code coverage withe the contract checks |
    /// - do not unit test the contracts |
    /// - use it with [NotNull] and [CanBeNull] attributes |
    /// </para>
    /// <para>
    /// <strong>Design By Contract Programming is a state of mind not code.</strong>
    /// If all is done properly the <see cref="DesignByContractViolationException"/> will never be seen on a production environment.
    /// The contract checks help developers clarify what is expected. During the development phase when we integrate components 
    /// (simply: when we call method of another class) we may violate the contract and receive the exception, but this is what it is for.
    /// </para>
    /// </summary>
    public static partial class Fail
    {
        /// <summary>
        /// Returns exception that can be thrown when contract is failed.
        /// </summary>
        /// <param name="message">Message that will be passed to the <see cref="DesignByContractViolationException"/>.</param>
        /// <param name="args">Arguments that will be passed to the <see cref="DesignByContractViolationException"/>.</param>
        /// <returns>The exception to throw when contract is violated</returns>
        /// <example>
        /// <code>
        ///  public string GetName()
        ///  {
        ///      switch (this.Type)
        ///      {
        ///          case ContractorType.Company:
        ///              return this.CompanyName;
        ///          case ContractorType.Person:
        ///              return this.FirstName + " " + this.LastName;
        ///          default:
        ///              throw Fail.Because("Unsupported ContractorType: {0}. Maybe someone extended enum and forgot about this logic?", this.Type);
        ///      }
        ///  }
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [NotNull, Pure]
        public static DesignByContractViolationException Because([NotNull] string message, [NotNull] params object[] args)
        {
            Fail.RequiresMessage(message, args);

            return new DesignByContractViolationException(String.Format(message, args));
        }

        [ExcludeFromCodeCoverage]
        private static void RequiresMessage([NotNull] string message, [NotNull] object[] args)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            if (args == null)
                throw new ArgumentNullException(nameof(args));
        }
    }
}