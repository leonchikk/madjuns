using Common.Core.Models;

namespace Users.Core.Domain
{
    public class UserSetting : BaseEntity
    {
        public User User { get; set; }
        public Setting Setting { get; set; }
    }
}
