﻿using Common.Core.Models;
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
            UserFriends = new HashSet<UserFriend>();
            Subscribers = new HashSet<UserSubscriber>();
            BlackList = new HashSet<BlockedUser>();
        }

        public User(Guid accountId, Profile profile)
        {
            Id = accountId;
            Profile = profile;
            Settings = new HashSet<UserSetting>();
            UserFriends = new HashSet<UserFriend>();
            Subscribers = new HashSet<UserSubscriber>();
            BlackList = new HashSet<BlockedUser>();
        }

        public virtual Profile Profile { get; set; }
        public virtual ICollection<UserSetting> Settings { get; set; }
        public virtual ICollection<UserFriend> UserFriends { get; set; }
        public virtual ICollection<UserFriend> UserIsAFriendOf { get; set; }
        public virtual ICollection<UserSubscriber> Subscribers { get; set; }
        public virtual ICollection<UserSubscriber> SubscribesTo { get; set; }
        public virtual ICollection<BlockedUser> BlackList { get; set; }

        public void AddToFriends(User subscriber)
        {
            if (Id == subscriber.Id)
                throw new Exception("You can not add to friend yourself");

            if (!Subscribers.Any(sub => sub.Subscriber.Id == subscriber.Id))
                throw new Exception("This user is not your subscriber");

            if (UserFriends.Any(friend => friend.Friend.Id == subscriber.Id))
                throw new Exception("This user already your friend");

            UserFriends.Add(new UserFriend(Id, subscriber.Id));
            RemoveFromSubscribers(subscriber.Id);
        }

        public void RemoveFromFriends(User friend)
        {
            if (Id == friend.Id)
                throw new Exception("You can not remove yourself");

            UserFriends.Where(f => f.User.Id == friend.Id || f.Friend.Id == friend.Id)
                .ToList()
                .ForEach(userFriend =>
                {
                    UserFriends.Remove(userFriend);
                });
        }

        public void AddToBlackList(User userToBeBanned)
        {
            if (Id == userToBeBanned.Id)
                throw new Exception("You can not block yourself");

            if (BlackList.Any(b => b.BannedUser.Id == userToBeBanned.Id))
                throw new Exception("This user already in black list");

            RemoveFromFriends(userToBeBanned);
            BlackList.Add(new BlockedUser(this, userToBeBanned));
        }
        
        public void RemoveFromBlackList(User bannedUser)
        {
            BlackList.Where(f => f.BannedUser.Id == bannedUser.Id)
                .ToList()
                .ForEach(banned =>
                {
                    BlackList.Remove(banned);
                });
        }

        public void SubscribeTo(User targetUser)
        {
            if (Id == targetUser.Id)
                throw new Exception("You can not add to subscribers yourself");

            if (SubscribesTo.Any(s => s.Subscriber.Id == Id))
                throw new Exception("You already have been subscribed");

            SubscribesTo.Add(new UserSubscriber(targetUser, this));
        }

        public void RemoveFromSubscribers(Guid subscribeId)
        {
            var subscription = Subscribers.FirstOrDefault(s => s.SubscriberId == subscribeId);

            if (subscription == null)
                throw new Exception("This user is not subscribe to you");

            Subscribers.Remove(subscription);
        }

        public void RejectSubscription(Guid userId)
        {
            var subscription = SubscribesTo.FirstOrDefault(s => s.UserId == userId);

            if (subscription == null)
                throw new Exception("You are not subscribe to this user");

            Subscribers.Remove(subscription);
        }

        public IEnumerable<User> GetFriends()
        {
            var userFriends = UserFriends.Select(u => u.User);
            var userIsAFriendOf = UserIsAFriendOf.Select(u => u.Friend);

            return userFriends.Concat(userIsAFriendOf);
        }

        public void Update(Profile profile)
        {
            Profile.Update(profile);
        }
    }
}
