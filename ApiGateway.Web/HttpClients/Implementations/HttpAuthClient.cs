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
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpAuthClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AuthResponseModel> AuthAsync(AuthRequestModel requestModel)
        {
            var client = _httpClientFactory.CreateClient("auth");

            var credentials = JsonConvert.SerializeObject(requestModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(credentials);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var httpResponseMessage = await client.PostAsync("api/auth/sign-in", byteContent);
            var stringResult = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AuthResponseModel>(stringResult);
        }
    }
}
