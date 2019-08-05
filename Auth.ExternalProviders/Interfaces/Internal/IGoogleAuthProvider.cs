using Auth.ExternalProviders.Models;
using System.Threading.Tasks;

namespace Auth.ExternalProviders.Interfaces.Internal
{
    internal interface IGoogleAuthProvider
    {
        Task<ProviderUser> GetAccountInfoAsync(string providerToken);
    }
}
