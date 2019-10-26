using Common.Networking.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;

namespace Common.Networking.Implementaions
{
    public class BaseClient
    {
        protected readonly IHttpBaseClient HttpClient;
        protected readonly HttpContext HttpContext;
        protected Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        public BaseClient(IHttpBaseClient httpClient, IHttpContextAccessor httpAccessor)
        {
            HttpClient = httpClient;
            HttpContext = httpAccessor.HttpContext;

            InitializeDefaultHeader();
        }

        private void InitializeDefaultHeader()
        {
            var request = HttpContext.Request;
            Headers.Add("X-ApiGateway-Address", $"{request.Scheme}://{request.Host}");
        }
    }
}
