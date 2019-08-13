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
            Friends = new HashSet<User>();
            Subscribers = new HashSet<User>();
            BlackList = new HashSet<User>();
        }

        public User(Guid accountId, Profile profile)
        {
            Id = new Guid();
            AccountId = accountId;
            Profile = profile;
            Settings = new HashSet<UserSetting>();
            Friends = new HashSet<User>();
            Subscribers = new HashSet<User>();
            BlackList = new HashSet<User>();
        }

        public Guid AccountId { get; set; }
        public Profile Profile { get; set; }
        public ICollection<UserSetting> Settings { get; set; }
        public ICollection<User> Friends { get; set; }
        public ICollection<User> Subscribers { get; set; }
        public ICollection<User> BlackList { get; set; }

        public void Update(Profile profile)
        {
            Profile.Update(profile);
        }
    }
}
