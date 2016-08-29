using System;

namespace Synergy.Contracts.Samples.Domain
{
    public class Contractor
    {
        public Guid Id { get; set; }

        public ContractorType Type { get; set; }

        public string CompanyName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

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