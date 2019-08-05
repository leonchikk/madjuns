using Auth.ExternalProviders.Interfaces.Internal;
using Auth.ExternalProviders.Models;
using System;
using System.Threading.Tasks;

namespace Auth.ExternalProviders.Providers
{
    internal class VkAuthProvider : BaseProvider, IVkAuthProvider
    {
        public VkAuthProvider() : base("https://api.vk.com/") { }

        public async Task<ProviderUser> GetAccountInfoAsync(string providerToken)
        {
            dynamic result = await GetAsync<dynamic>(providerToken, "method/users.get", "v=5.95&fields=photo_100");

            if (result == null)
            {
                throw new Exception("User from this token not exist");
            }

            ProviderUser account = new ProviderUser()
            {
                ProviderId = result["response"].First["id"],
                FirstName = result["response"].First["first_name"],
                LastName = result["response"].First["last_name"],
                ImageUrl = result["response"].First["photo_100"]
            };

            return account;
        }
    }
}
