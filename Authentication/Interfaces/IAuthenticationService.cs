using Authentication.Models.Requests;
using Authentication.Models.Responses;
using System.Threading.Tasks;

namespace Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationToken Login(AuthenticationRequest request);
    }
}
