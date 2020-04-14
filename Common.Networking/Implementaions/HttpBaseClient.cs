
using Common.Networking.Extensions;
using Common.Networking.Interfaces;
using IdentityModel.Client;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Networking.Implementations
{
    public class HttpBaseClient : IHttpBaseClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpCallerUtils _httpUtils;

        public HttpBaseClient(IHttpClientFactory httpClientFactory, IHttpCallerUtils httpUtils)
        {
            _httpClientFactory = httpClientFactory;
            _httpUtils = httpUtils;
        }

        public async Task<TResult> GetAsync<TResult>(string clientName, string requestUri, Dictionary<string, string> headers = default)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                requestMessage.CopyHeaders(headers);

                var authToken = _httpUtils.GetAuthToken();

                var httpClient = _httpClientFactory.CreateClient(clientName);
                 httpClient.SetBearerToken(authToken);

                var httpResponseMessage = await httpClient.SendAsync(requestMessage);

                return await httpResponseMessage.HandleResponseAsync<TResult>();
            }
        }

        public async Task<TResult> DeleteAsync<TResult>(string clientName, string requestUri, Dictionary<string, string> headers = default)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri))
            {
                requestMessage.CopyHeaders(headers);

                var authToken = _httpUtils.GetAuthToken();

                var httpClient = _httpClientFactory.CreateClient(clientName);
                httpClient.SetBearerToken(authToken);

                var httpResponseMessage = await httpClient.SendAsync(requestMessage);

                return await httpResponseMessage.HandleResponseAsync<TResult>();
            }
        }

        public async Task DeleteAsync(string clientName, string requestUri, Dictionary<string, string> headers = default)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri))
            {
                requestMessage.CopyHeaders(headers);

                var authToken = _httpUtils.GetAuthToken();

                var httpClient = _httpClientFactory.CreateClient(clientName);
                httpClient.SetBearerToken(authToken);

                var httpResponseMessage = await httpClient.SendAsync(requestMessage);

                await httpResponseMessage.HandleResponseAsync();
            }
        }

        public async Task<TResult> PostAsync<TResult, TRequest>(string clientName, string requestUri, TRequest requestModel, Dictionary<string, string> headers = default, string mediaType = "application/json") where TRequest : class
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri))
            {
                requestMessage.Content = requestModel.ToContent(mediaType);
                requestMessage.CopyHeaders(headers);

                var authToken = _httpUtils.GetAuthToken();
                var httpClient = _httpClientFactory.CreateClient(clientName);
                httpClient.SetBearerToken(authToken);

                var httpResponseMessage = await httpClient.SendAsync(requestMessage);

                return await httpResponseMessage.HandleResponseAsync<TResult>();
            }
        }

        public async Task PostAsync<TRequest>(string clientName, string requestUri, TRequest requestModel, Dictionary<string, string> headers = default, string mediaType = "application/json") where TRequest : class
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri))
            {
                requestMessage.Content = requestModel.ToContent(mediaType);
                requestMessage.CopyHeaders(headers);

                var authToken = _httpUtils.GetAuthToken();
                var httpClient = _httpClientFactory.CreateClient(clientName);
                httpClient.SetBearerToken(authToken);

                var httpResponseMessage = await httpClient.SendAsync(requestMessage);

                await httpResponseMessage.HandleResponseAsync();
            }
        }

        public async Task PutAsync<TRequest>(string clientName, string requestUri, TRequest requestModel, Dictionary<string, string> headers = default, string mediaType = "application/json") where TRequest : class
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri))
            {
                requestMessage.Content = requestModel.ToContent(mediaType);
                requestMessage.CopyHeaders(headers);

                var authToken = _httpUtils.GetAuthToken();
                var httpClient = _httpClientFactory.CreateClient(clientName);
                httpClient.SetBearerToken(authToken);

                var httpResponseMessage = await httpClient.SendAsync(requestMessage);

                await httpResponseMessage.HandleResponseAsync();
            }
        }

        public async Task<TResult> PutAsync<TResult, TRequest>(string clientName, string requestUri, TRequest requestModel, Dictionary<string, string> headers = default, string mediaType = "application/json") where TRequest : class
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri))
            {
                requestMessage.Content = requestModel.ToContent(mediaType);
                requestMessage.CopyHeaders(headers);

                var authToken = _httpUtils.GetAuthToken();
                var httpClient = _httpClientFactory.CreateClient(clientName);
                httpClient.SetBearerToken(authToken);

                var httpResponseMessage = await httpClient.SendAsync(requestMessage);

                return await httpResponseMessage.HandleResponseAsync<TResult>();
            }
        }
    }
}
