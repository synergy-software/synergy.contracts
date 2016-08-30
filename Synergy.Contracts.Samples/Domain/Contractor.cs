using System;
using JetBrains.Annotations;

namespace Synergy.Contracts.Samples.Domain
{
    public class Contractor
    {
        public static Contractor CreateCompany(string name)
        {
            return new Contractor
            {
                Id = Guid.NewGuid(),
                Type = ContractorType.Company,
                CompanyName = name
            };
        }

        public Guid Id { get; set; }

        public ContractorType Type { get; set; }

        [CanBeNull]
        public string CompanyName { get; set; }

        [CanBeNull]
        public string LastName { get; set; }

        [CanBeNull]
        public string FirstName { get; set; }

        [CanBeNull, Pure]
        public string GetName()
        {
            switch (this.Type)
            {
                case ContractorType.Company:
                    return this.CompanyName;
                case ContractorType.Person:
                    return this.FirstName + " " + this.LastName;
                default:
                    throw Fail.Because("Unsupported ContractorType: {0}. Maybe someone extended enum and forgot about this logic?", this.Type);
            }
        }
    }
}