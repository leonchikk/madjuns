using Common.Core.Models;

namespace Users.Core.Domain
{
    public class UserSetting : BaseEntity
    {
        public virtual User User { get; set; }
        public virtual Setting Setting { get; set; }
    }
}
