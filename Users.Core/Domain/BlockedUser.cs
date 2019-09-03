using Common.Core.Models;
using System;

namespace Users.Core.Domain
{
    public class BlockedUser : BaseEntity
    {
        private BlockedUser() { }
        public BlockedUser(User user, User userToBeBanned)
        {
            Id = Guid.NewGuid();
            User = user;
            BannedUser = userToBeBanned;
        }

        public User User { get; set; }
        public User BannedUser { get; set; }
    }
}
