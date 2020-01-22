
using Users.Core.Domain;

namespace Users.API.Models.Search.Bans
{
    public class BansSearchFilter
    {
        public static string[] SearchableFields => new string[]
        {
            $"{nameof(BlockedUser.Initiator)}.{nameof(BlockedUser.Initiator.Profile)}.{nameof(BlockedUser.Initiator.Profile.UserName)}",
            $"{nameof(BlockedUser.WhoisBlocked)}.{nameof(BlockedUser.WhoisBlocked.Profile)}.{nameof(BlockedUser.WhoisBlocked.Profile.UserName)}"
        };
    }
}
