using Common.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Users.Core.Domain
{
    public class User : BaseEntity
    {
        private User()
        {
            Settings = new HashSet<UserSetting>();
        }

        public User(Guid accountId, Profile profile)
        {
            Id = new Guid();
            AccountId = accountId;
            Profile = profile;
            Settings = new HashSet<UserSetting>();
        }

        public Guid AccountId { get; set; }
        public Profile Profile { get; set; }
        public ICollection<UserSetting> Settings { get; set; }

        public void Update(Profile profile)
        {
            Profile.Update(profile);
        }
    }
}
