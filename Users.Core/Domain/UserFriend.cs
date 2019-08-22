using Common.Core.Models;

namespace Users.Core.Domain
{
    public class UserFriend : BaseEntity
    {
        public User FirstUser { get; set; }
        public User SecondUser { get; set; }
    }
}
