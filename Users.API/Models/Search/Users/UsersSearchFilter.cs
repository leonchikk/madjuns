

using Users.Core.Domain;

namespace Users.API.Models.Search.Users
{
    public class UsersSearchFilter
    {
        public static string[] SearchableFields => new string[]
        {
            $"{nameof(User.Profile)}.{nameof(Profile.UserName)}",
            $"{nameof(User.Profile)}.{nameof(Profile.Email)}",
            $"{nameof(User.Profile)}.{nameof(Profile.Address)}.{nameof(User.Profile.Address.City)}",
            $"{nameof(User.Profile)}.{nameof(Profile.Address)}.{nameof(User.Profile.Address.Country)}",
            $"{nameof(User.Profile)}.{nameof(Profile.Address)}.{nameof(User.Profile.Address.District)}"
        };
    }
}
