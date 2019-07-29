using Authentication.Models.Requests;
using Authentication.Models.Responses;

namespace Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationToken Login(AuthenticationRequest request);
    }
}
