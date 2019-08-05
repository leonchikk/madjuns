using Auth.Core.Entities;
using Auth.ExternalProviders.Models;

namespace Auth.ExternalProviders.Interfaces.Internal
{
    internal interface ITokenService
    {
        AuthorizationToken CreateToken(Account account);
    }
}
