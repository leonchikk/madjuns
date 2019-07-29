using Common.Core.Models;
using System;

namespace Auth.Data.Entities
{
    public class Account : BaseEntity
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public DateTime BirthDay { get; set; }

        public int Age { get; set; }

        public string Password { get; set; }
    }
}
