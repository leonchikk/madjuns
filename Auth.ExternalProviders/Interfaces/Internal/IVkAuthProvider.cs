using Auth.ExternalProviders.Models;
using System.Threading.Tasks;

namespace Auth.ExternalProviders.Interfaces.Internal
{
    internal interface IVkAuthProvider
    {
        Task<ProviderUser> GetAccountInfoAsync(string providerToken);
    }
}
