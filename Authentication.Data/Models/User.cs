using Common.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Data.Models
{
    public class User: BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
