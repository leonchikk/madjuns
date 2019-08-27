using Common.Core.Models;
using System;
using System.Collections.Generic;

namespace Users.Core.Domain
{
    public class User : BaseEntity
    {
        private User()
        {
            Settings = new HashSet<UserSetting>();
            Friends = new HashSet<UserFriend>();
            Subscribers = new HashSet<UserSubscriber>();
            BlackList = new HashSet<BlockedUser>();
        }

        public User(Guid accountId, Profile profile)
        {
            Id = accountId;
            Profile = profile;
            Settings = new HashSet<UserSetting>();
            Friends = new HashSet<UserFriend>();
            Subscribers = new HashSet<UserSubscriber>();
            BlackList = new HashSet<BlockedUser>();
        }

        public Profile Profile { get; set; }
        public ICollection<UserSetting> Settings { get; set; }
        public ICollection<UserFriend> Friends { get; set; }
        public ICollection<UserSubscriber> Subscribers { get; set; }
        public ICollection<BlockedUser> BlackList { get; set; }


        public void Update(Profile profile)
        {
            Profile.Update(profile);
        }
    }
}
