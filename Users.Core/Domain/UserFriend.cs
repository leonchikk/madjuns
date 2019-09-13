using Common.Core.Models;
using System;

namespace Users.Core.Domain
{
    public class UserFriend : BaseEntity
    {
        protected UserFriend() { }
        public UserFriend(Guid currentUserId, Guid subscriberId)
        {
            Id = Guid.NewGuid();
            UserId = currentUserId;
            FriendId = subscriberId;
        }

        public virtual Guid UserId { get; set; }
        public virtual Guid FriendId { get; set; }

        public virtual User User { get; set; }
        public virtual User Friend { get; set; }
    }
}
