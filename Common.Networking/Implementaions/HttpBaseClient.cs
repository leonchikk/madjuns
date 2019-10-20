
using Common.Networking.Extensions;
using Common.Networking.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Networking.Implementations
{
    public class HttpBaseClient : IHttpBaseClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpBaseClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TResult> GetAsync<TResult>(string clientName, string requestUri, Dictionary<string, string> headers = null)
        {
            var client = _httpClientFactory.CreateClient(clientName);

            var httpResponseMessage = await client.GetAsync(requestUri);

            return await httpResponseMessage.HandleResponseAsync<TResult>();
        }

        public async Task<TResult> DeleteAsync<TResult>(string clientName, string requestUri, Dictionary<string, string> headers = null)
        {
            var client = _httpClientFactory.CreateClient(clientName);

            var httpResponseMessage = await client.DeleteAsync(requestUri);

            return await httpResponseMessage.HandleResponseAsync<TResult>();
        }

        public async Task DeleteAsync(string clientName, string requestUri, Dictionary<string, string> headers = null)
        {
            var client = _httpClientFactory.CreateClient(clientName);

            var httpResponseMessage = await client.DeleteAsync(requestUri);

            await httpResponseMessage.HandleResponseAsync();
        }

        public async Task<TResult> PostAsync<TResult, TRequest>(string clientName, string requestUri, TRequest requestModel, Dictionary<string, string> headers = null, string mediaType = "application/json") where TRequest : class
        {
            var client = _httpClientFactory.CreateClient(clientName);

            var httpResponseMessage = await client.PostAsync(requestUri, requestModel.GenerateHttpContent(headers, mediaType));

            return await httpResponseMessage.HandleResponseAsync<TResult>();
        }

        public async Task PostAsync<TRequest>(string clientName, string requestUri, TRequest requestModel, Dictionary<string, string> headers = null, string mediaType = "application/json") where TRequest : class
        {
            var client = _httpClientFactory.CreateClient(clientName);

            var httpResponseMessage = await client.PostAsync(requestUri, requestModel.GenerateHttpContent(headers, mediaType));

            await httpResponseMessage.HandleResponseAsync();
        }

        public async Task PutAsync<TRequest>(string clientName, string requestUri, TRequest requestModel, Dictionary<string, string> headers = null, string mediaType = "application/json") where TRequest : class
        {
            var client = _httpClientFactory.CreateClient(clientName);

            var httpResponseMessage = await client.PutAsync(requestUri, requestModel.GenerateHttpContent(headers, mediaType));

            await httpResponseMessage.HandleResponseAsync();
        }

        public async Task<TResult> PutAsync<TResult, TRequest>(string clientName, string requestUri, TRequest requestModel, Dictionary<string, string> headers = null, string mediaType = "application/json") where TRequest : class
        {
            var client = _httpClientFactory.CreateClient(clientName);

            var httpResponseMessage = await client.PutAsync(requestUri, requestModel.GenerateHttpContent(headers, mediaType));

            return await httpResponseMessage.HandleResponseAsync<TResult>();
        }
    }
}
