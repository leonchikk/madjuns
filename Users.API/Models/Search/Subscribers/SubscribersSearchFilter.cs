using Users.Core.Domain;

namespace Users.API.Models.Search.Subscribers
{
    public class SubscribersSearchFilter
    {
        //Consider to use expression in this way
        public static string[] SearchableFields => new string[]
        {
            $"{nameof(UserSubscriber.Subscriber)}.{nameof(UserSubscriber.Subscriber.Profile)}.{nameof(UserSubscriber.Subscriber.Profile.UserName)}"
        };
    }
}
