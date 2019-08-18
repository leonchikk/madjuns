using Auth.Core.Entities;
using Authentication.Models.Responses;

namespace Authentication.API.Interfaces
{
    public interface ITokenService
    {
        AuthenticationToken CreateToken(Account account);
    }
}
