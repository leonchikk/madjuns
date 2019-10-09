

using Users.Core.Domain;

namespace Users.API.Models.Search.Users
{
    public class UsersSearchFilter
    {
        public static string[] SearchableFields => new string[]
        {
            $"{nameof(User.Profile.UserName)}",
            $"{nameof(User.Profile.Email)}",
            $"{nameof(User.Profile.Address.City)}",
            $"{nameof(User.Profile.Address.Country)}",
            $"{nameof(User.Profile.Address.District)}"
        };
    }
}
