using Common.Core.Models;
using System;

namespace Users.Core.Domain
{
    public class Profile : BaseEntity
    {
        public virtual Address Address { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DayOfBirth { get; set; }

        public void Update(Profile newProfile)
        {
            Address = newProfile.Address;
            UserName = newProfile.UserName;
            Email = newProfile.Email;
            DayOfBirth = newProfile.DayOfBirth;
        }
    }
}
