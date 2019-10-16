using ApiGateway.Web.HttpClients.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiGateway.Web.HttpClients.Implementations
{
    public class HttpBaseClient : IHttpBaseClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpBaseClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<TResult> SendPostAsync<TResult, TRequest>(string clientName, string requestUri, TRequest requestModel, string mediaType = "application/json")
        {
            var client = _httpClientFactory.CreateClient(clientName);

            var jsonFromRequestModel = JsonConvert.SerializeObject(requestModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonFromRequestModel);
            var byteContent = new ByteArrayContent(buffer) ;

            byteContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

            var httpResponseMessage = await client.PostAsync(requestUri, byteContent);
            var resultContent = await httpResponseMessage.Content.ReadAsStringAsync();

            if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception(resultContent);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception(resultContent);
            }

            return JsonConvert.DeserializeObject<TResult>(resultContent);
        }
    }
}
