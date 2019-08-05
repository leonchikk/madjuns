using Auth.ExternalProviders.Interfaces.Internal;
using Auth.ExternalProviders.Models;
using System;
using System.Threading.Tasks;

namespace Auth.ExternalProviders.Providers
{
    internal class GoogleAuthProvider : BaseProvider, IGoogleAuthProvider
    {
        public GoogleAuthProvider() : base("https://www.googleapis.com/oauth2/v1/") { }

        public async Task<ProviderUser> GetAccountInfoAsync(string providerToken)
        {
            dynamic result = await GetAsync<dynamic>(providerToken);

            if (result == null)
            {
                throw new Exception("User from this token not exist");
            }

            ProviderUser account = new ProviderUser()
            {
                Email = result.email,
                FirstName = result.given_name,
                LastName = result.family_name,
                ImageUrl = result.picture
            };

            return account;
        }
    }
}
