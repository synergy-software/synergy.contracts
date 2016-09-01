using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        /// <summary>
        /// Return the exception to be thrown when enum value is not supported.
        /// <para>REMARKS: It is usually used in switch statements (<see langword="default"/>).</para>
        /// </summary>
        /// <param name="value">Value of the enum</param>
        /// <returns>Exception to throw.</returns>
        /// <example>
        /// <code>
        ///  [CanBeNull, Pure]
        ///  public string GetName()
        ///  {
        ///      switch (this.Type)
        ///      {
        ///          case ContractorType.Company:
        ///              return this.CompanyName;
        ///          case ContractorType.Person:
        ///              return this.FirstName + " " + this.LastName;
        ///          default:
        ///              throw Fail.BecauseEnumOutOfRange(this.Type);
        ///      }
        ///  }
        /// </code>
        /// </example>
        [NotNull, Pure]
        public static DesignByContractViolationException BecauseEnumOutOfRange([NotNull] Enum value)
        {
            Fail.RequiresEnumValue(value);

            return new DesignByContractViolationException($"Unsupported {value.GetType() .Name} value: {value}");
        }

        [ExcludeFromCodeCoverage]
        private static void RequiresEnumValue([NotNull] Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
        }
    }
}