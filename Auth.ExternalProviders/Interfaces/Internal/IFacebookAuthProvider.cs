using Auth.ExternalProviders.Models;
using System.Threading.Tasks;

namespace Auth.ExternalProviders.Interfaces.Internal
{
    internal interface IFacebookAuthProvider
    {
        Task<ProviderUser> GetAccountInfoAsync(string providerToken);
    }
}
