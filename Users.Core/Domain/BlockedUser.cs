using Common.Core.Models;

namespace Users.Core.Domain
{
    public class BlockedUser : BaseEntity
    {
        public User User { get; set; }
        public User BannedUser { get; set; }
    }
}
