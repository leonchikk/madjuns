using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Auth.ExternalProviders.Providers
{
    internal abstract class BaseProvider
    {
        private readonly HttpClient _httpClient;

        protected BaseProvider(string host)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(host)
            };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<T> GetAsync<T>(string accessToken, string endpoint = null, string args = null)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{endpoint}?access_token={accessToken}&{args}");
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
