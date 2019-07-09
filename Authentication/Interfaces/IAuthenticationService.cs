using Authentication.Models.Requests;
using Authentication.Models.Responses;
using System.Threading.Tasks;

namespace Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationToken> LoginAsync(AuthenticationRequest request);
    }
}
