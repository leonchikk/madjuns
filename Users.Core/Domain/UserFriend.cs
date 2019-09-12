using Common.Core.Models;
using System;

namespace Users.Core.Domain
{
    public class UserFriend : BaseEntity
    {
        protected UserFriend() { }
        public UserFriend(User currentUser, User subscriber)
        {
            Id = Guid.NewGuid();
            User = currentUser;
            Friend = subscriber;
        }

        public virtual User User { get; set; }
        public virtual User Friend { get; set; }
    }
}
