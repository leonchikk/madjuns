using Auth.Core.Entities;
using Authentication.Models.Responses;

namespace Authentication.Interfaces
{
    public interface ITokenService
    {
        AuthenticationToken CreateToken(Account account);
    }
}
