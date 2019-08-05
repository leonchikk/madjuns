using Auth.ExternalProviders.Models;
using System.Threading.Tasks;

namespace Auth.ExternalProviders.Interfaces.Public
{
    public interface IExternalAuthProvider
    {
        Task<AuthorizationToken> FacebookLoginAsync(ProviderToken providerResource);
        Task<AuthorizationToken> GoogleLoginAsync(ProviderToken providerResource);
        Task<AuthorizationToken> VkLoginAsync(ProviderToken providerResource);

    }
}
