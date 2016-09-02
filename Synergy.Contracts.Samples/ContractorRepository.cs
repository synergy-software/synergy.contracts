using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Synergy.Contracts.Samples.Domain;

namespace Synergy.Contracts.Samples
{
    [UsedImplicitly]
    public class ContractorRepository : IContractorRepository
    {
        public Contractor FindContractorByGuid(Guid id)
        {
            Fail.IfArgumentEmpty(value: id, argumentName: nameof(id));

            // WARN: Below is sample code with no sense at all
            return new Contractor();
        }

        public List<Contractor> GetContractorsAged(DateTime minDate, DateTime? maxDate)
        {
            Fail.IfNotMidnight(minDate, "minDate must be a midnight");
            Fail.IfNotMidnight(maxDate, "maxDate must be a midnight");

            // WARN: Below is sample code with no sense at all
            return new List<Contractor>(0);
        }

        public List<Contractor> FilterContractors(ContractorFilterParameters paramaters)
        {
            paramaters.FailIfNull(nameof(paramaters));
            paramaters.FoundedBetween.FailIfNull(nameof(paramaters.FoundedBetween));
            paramaters.FoundedBetween.Max.FailIfNull(nameof(paramaters.FoundedBetween.Max));

            // WARN: Below is sample code with no sense at all
            return new List<Contractor>(0);
        }
    }

    public interface IContractorRepository
    {
        [NotNull, Pure]
        Contractor FindContractorByGuid(Guid id);

        [NotNull, Pure]
        List<Contractor> GetContractorsAged(DateTime minDate, DateTime? maxDate);

        [NotNull, Pure]
        List<Contractor> FilterContractors([NotNull] ContractorFilterParameters paramaters);
    }

    public class ContractorFilterParameters
    {
        [NotNull]
        // ReSharper disable once NotNullMemberIsNotInitialized
        public DateRangeOpen FoundedBetween { get; set; }
    }

    public class DateRangeOpen
    {
        public DateTime Min { get; set; }

        [CanBeNull]
        public DateTime? Max { get; set; }
    }
}