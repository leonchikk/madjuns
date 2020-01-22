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
            IAmFriendsWith = new HashSet<FriendsShip>();
            AreFriendsWithMe = new HashSet<FriendsShip>();
            Subscribers = new HashSet<UserSubscriber>();
            UsersBlockedByMe = new HashSet<BlockedUser>();
            IAmBlockedByUsers = new HashSet<BlockedUser>();
        }

        public User(Guid accountId, Profile profile)
        {
            Id = accountId;
            Profile = profile;
            Settings = new HashSet<UserSetting>();
            IAmFriendsWith = new HashSet<FriendsShip>();
            AreFriendsWithMe = new HashSet<FriendsShip>();
            Subscribers = new HashSet<UserSubscriber>();
            UsersBlockedByMe = new HashSet<BlockedUser>();
            IAmBlockedByUsers = new HashSet<BlockedUser>();
        }

        public virtual Profile Profile { get; set; }
        public virtual ICollection<UserSetting> Settings { get; set; }
        public virtual ICollection<FriendsShip> IAmFriendsWith { get; set; }
        public virtual ICollection<FriendsShip> AreFriendsWithMe { get; set; }
        public virtual ICollection<UserSubscriber> Subscribers { get; set; }
        public virtual ICollection<UserSubscriber> SubscribesTo { get; set; }
        public virtual ICollection<BlockedUser> UsersBlockedByMe { get; set; }
        public virtual ICollection<BlockedUser> IAmBlockedByUsers { get; set; }

        public void AddToFriends(User subscriber)
        {
            if (Id == subscriber.Id)
                throw new Exception("You can not add to friend yourself"); 

            if (!Subscribers.Any(sub => sub.Subscriber.Id == subscriber.Id))
                throw new Exception("This user is not your subscriber");

            var currentFriends = GetFriends();
   
            if (currentFriends.Any(friend => friend.Id == subscriber.Id))
                throw new Exception("This user already your friend");

            IAmFriendsWith.Add(new FriendsShip(Id, subscriber.Id));
            AreFriendsWithMe.Add(new FriendsShip(subscriber.Id, Id));

            RemoveFromSubscribers(subscriber.Id);
        }

        public void RemoveFromFriends(User friend)
        {
            var myFriendshipWithFriendRelation = IAmFriendsWith.Where(x => x.MyFriendId == friend.Id).FirstOrDefault();
            var FriendshipWithMe = AreFriendsWithMe.Where(x => x.MyFriendId == Id).FirstOrDefault();

            if (myFriendshipWithFriendRelation != null)
            {
                IAmFriendsWith.Remove(myFriendshipWithFriendRelation);
            }

            if (FriendshipWithMe != null)
            {
                AreFriendsWithMe.Remove(FriendshipWithMe);
            }
        }

        public void AddToBlackList(User userToBeBanned)
        {
            if (Id == userToBeBanned.Id)
                throw new Exception("You can not block yourself");

            if (UsersBlockedByMe.Any(blockedUser => blockedUser.WhoisBlocked.Id == userToBeBanned.Id))
                throw new Exception("This user already in black list");

            RemoveFromFriends(userToBeBanned);
            UsersBlockedByMe.Add(new BlockedUser(Id, userToBeBanned.Id));
        }

        public void RemoveFromBlackList(User bannedUser)
        {
            var userBlockedByMe = UsersBlockedByMe.Where(blockedUser => blockedUser.WhoisBlocked.Id == bannedUser.Id).FirstOrDefault();

            if (userBlockedByMe == null)
                throw new Exception("This user has not been banned");

            UsersBlockedByMe.Remove(userBlockedByMe);
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
            return IAmFriendsWith.Select(x => x.MyFriend);
        }

        public void Update(Profile profile)
        {
            Profile.Update(profile);
        }
    }
}
