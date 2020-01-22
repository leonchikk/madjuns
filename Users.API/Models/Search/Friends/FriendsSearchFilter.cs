using Users.Core.Domain;

namespace Users.API.Models.Search.Friends
{
    public class FriendsSearchFilter
    {
        public static string[] SearchableFields => new string[]
        {
            $"{nameof(FriendsShip.MyFriend)}.{nameof(FriendsShip.MyFriend.Profile)}.{nameof(FriendsShip.MyFriend.Profile.UserName)}"
        };
    }
}
