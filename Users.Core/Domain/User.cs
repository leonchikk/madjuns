using Common.Core.Models;
using System;
using System.Collections.ObjectModel;

namespace Users.Core.Domain
{
    public class User : BaseEntity
    {
        public Guid AccountId { get; set; }
        public Profile Profile { get; set; }
        public Collection<UserSetting> Settings { get; set; }
        public Collection<User> Friends { get; set; }
        public Collection<User> Subscribers { get; set; }
        public Collection<User> BlackList { get; set; }
    }
}
