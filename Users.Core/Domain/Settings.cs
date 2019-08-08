using Common.Core.Models;

namespace Users.Core.Domain
{
    public class Settings : BaseEntity
    {
        public string Name { get; set; }
        public int Setting { get; set; }
    }
}
