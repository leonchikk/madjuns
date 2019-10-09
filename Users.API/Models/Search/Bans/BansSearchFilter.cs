
using Users.Core.Domain;

namespace Users.API.Models.Search.Bans
{
    public class BansSearchFilter
    {
        public static string[] SearchableFields => new string[]
        {
            $"{nameof(BlockedUser.BannedUser)}.{nameof(BlockedUser.BannedUser.Profile)}.{nameof(BlockedUser.BannedUser.Profile.UserName)}"
        };
    }
}
