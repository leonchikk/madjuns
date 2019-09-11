using Common.Core.Models;
using System;

namespace Users.Core.Domain
{
    public class UserFriend : BaseEntity
    {
        private UserFriend() { }
        public UserFriend(User firstUser, User secondUser)
        {
            Id = Guid.NewGuid();
            FirstUser = firstUser;
            SecondUser = secondUser;
        }

        public virtual User FirstUser { get; set; }
        public virtual User SecondUser { get; set; }
    }
}
