using Common.Core.Models;
using System;

namespace Users.Core.Domain
{
    public class Profile : BaseEntity
    {
        public Address Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DayOfBirth { get; set; }
    }
}
