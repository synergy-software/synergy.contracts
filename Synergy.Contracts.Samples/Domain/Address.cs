using JetBrains.Annotations;

namespace Synergy.Contracts.Samples.Domain
{
    public class Address
    {
        [NotNull]
        public string City { get; set; }
    }
}