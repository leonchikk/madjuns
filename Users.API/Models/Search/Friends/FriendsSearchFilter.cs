using Users.Core.Domain;

namespace Users.API.Models.Search.Friends
{
    public class FriendsSearchFilter
    {
        public static string[] SearchableFields => new string[]
        {
            $"{nameof(UserFriend.Friend)}.{nameof(UserFriend.Friend.Profile)}.{nameof(UserFriend.Friend.Profile.UserName)}"
        };
    }
}
