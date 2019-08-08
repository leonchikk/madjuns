using Common.Core.Models;

namespace Users.Core.Domain
{
    public class Address : BaseEntity
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
    }
}
