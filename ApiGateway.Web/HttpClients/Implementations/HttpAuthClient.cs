using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.HttpClients.Models.Auth;
using Newtonsoft.Json;

namespace ApiGateway.Web.HttpClients.Implementations
{
    public class HttpAuthClient : IHttpAuthClient
    {
        private readonly IHttpBaseClient _httpBaseClient;

        public HttpAuthClient(IHttpBaseClient httpBaseClient)
        {
            _httpBaseClient = httpBaseClient;
        }

        public async Task<AuthResponseModel> AuthAsync(AuthRequestModel requestModel)
        {
            return await _httpBaseClient.SendPostAsync<AuthResponseModel, AuthRequestModel>("auth", "api/auth/sign-in", requestModel);
        }
    }
}
