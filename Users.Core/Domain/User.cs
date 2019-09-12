using Common.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Users.Core.Domain
{
    public class User : BaseEntity
    {
        protected User()
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

        public virtual Profile Profile { get; set; }
        public virtual ICollection<UserSetting> Settings { get; set; }
        public virtual ICollection<UserFriend> Friends { get; set; }
        public virtual ICollection<UserSubscriber> Subscribers { get; set; }
        public virtual ICollection<UserSubscriber> SubscribesTo { get; set; }
        public virtual ICollection<BlockedUser> BlackList { get; set; }

        public void AddToFriends(User subscriber)
        {
            if (Id == subscriber.Id)
                throw new Exception("You can not add to friend yourself");

            if (!Subscribers.Any(sub => sub.Subscriber.Id == subscriber.Id))
                throw new Exception("This user is not your subscriber");

            if (Friends.Any(friend => friend.Friend.Id == subscriber.Id))
                throw new Exception("This user already your friend");

            Friends.Add(new UserFriend(subscriber, this));
            Friends.Add(new UserFriend(this, subscriber));
        }

        public void RemoveFromFriends(User friend)
        {
            if (Id == friend.Id)
                throw new Exception("You can not remove yourself");

            if (Friends.Any(f => friend.Id != f.Friend.Id))
                throw new Exception("This user is not your friend");

            Friends.Where(f => f.User.Id == friend.Id || f.Friend.Id == friend.Id)
                .ToList()
                .ForEach(userFriend =>
                {
                    Friends.Remove(userFriend);
                });
        }

        public void AddToBlackList(User userToBeBanned)
        {
            if (Id == userToBeBanned.Id)
                throw new Exception("You can not block yourself");

            if (BlackList.Any(b => b.BannedUser.Id == userToBeBanned.Id))
                throw new Exception("This user already in black list");

            Friends.Where(f => f.User.Id == userToBeBanned.Id || f.Friend.Id == userToBeBanned.Id)
                .ToList()
                .ForEach(userFriend =>
                {
                    Friends.Remove(userFriend);
                });

            BlackList.Add(new BlockedUser(this, userToBeBanned));
        }

        public void SubscribeTo(User targetUser)
        {
            if (Id == targetUser.Id)
                throw new Exception("You can not add to subscribers yourself");

            if (SubscribesTo.Any(s => s.Subscriber.Id == Id))
                throw new Exception("You already have been subscribed");

            SubscribesTo.Add(new UserSubscriber(targetUser, this));
        }

        public void Update(Profile profile)
        {
            Profile.Update(profile);
        }
    }
}
