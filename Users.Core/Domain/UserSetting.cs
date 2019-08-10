using Common.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Core.Domain
{
    public class UserSetting: BaseEntity
    {
        public User User { get; set; }
        public Setting Setting { get; set; }
    }
}
