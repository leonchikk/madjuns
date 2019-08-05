using Auth.ExternalProviders.Interfaces.Internal;
using Auth.ExternalProviders.Models;
using System;
using System.Threading.Tasks;

namespace Auth.ExternalProviders.Providers
{
    internal class FacebookAuthProvider : BaseProvider, IFacebookAuthProvider
    {
        public FacebookAuthProvider() : base("https://graph.facebook.com/v2.8/") { }

        public async Task<ProviderUser> GetAccountInfoAsync(string providerToken)
        {
            dynamic result = await GetAsync<dynamic>(providerToken, endpoint: "me", args: "fields=first_name,last_name,email,picture.width(100).height(100)");

            if (result == null)
            {
                throw new Exception("User from this token not exist");
            }

            ProviderUser account = new ProviderUser()
            {
                Email = result.email,
                FirstName = result.first_name,
                LastName = result.last_name,
                ImageUrl = result.picture.data.url
            };

            return account;
        }
    }
}
