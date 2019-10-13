

using ApiGateway.Web.HttpClients.Models.Auth;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Interfaces
{
    public interface IHttpAuthClient
    {
        Task<AuthResponseModel> AuthAsync(AuthRequestModel requestModel);
    }
}
