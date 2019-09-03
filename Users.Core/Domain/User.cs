using Common.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void AddToFriends(User subscriber)
        {
            if (Id == subscriber.Id)
                throw new Exception("You can not add to friend yourself");

            if (Friends.Any(friend => friend.SecondUser.Id == subscriber.Id || friend.FirstUser.Id == subscriber.Id))
                throw new Exception("This user already your friend");

            Friends.Add(new UserFriend(this, subscriber));
        }

        public void RemoveFromFriends(User friend)
        {
            if (Id == friend.Id)
                throw new Exception("You can not remove yourself");

            if (Friends.Any(f => friend.Id != f.SecondUser.Id && friend.Id != f.FirstUser.Id))
                throw new Exception("This user is not your friend");

            var userFriend = Friends.FirstOrDefault(f => f.FirstUser.Id == friend.Id || f.SecondUser.Id == friend.Id);
            Friends.Remove(userFriend);
        }

        public void AddToBlackList(User userToBeBanned)
        {
            if (Id == userToBeBanned.Id)
                throw new Exception("You can not block yourself");

            if (BlackList.Any(b => b.BannedUser.Id == userToBeBanned.Id))
                throw new Exception("This user already in black list");

            BlackList.Add(new BlockedUser(this, userToBeBanned));
        }

        public void Update(Profile profile)
        {
            Profile.Update(profile);
        }
    }
}
