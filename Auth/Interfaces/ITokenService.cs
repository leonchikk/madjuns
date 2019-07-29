using Auth.Data.Entities;
using Authentication.Models.Responses;

namespace Authentication.Interfaces
{
    public interface ITokenService
    {
        AuthenticationToken CreateToken(Account account);
    }
}
