using Common.Networking.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;

namespace Common.Networking.Implementaions
{
    class HttpCallerUtils : IHttpCallerUtils
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpCallerUtils(
            IHttpContextAccessor httpContextAccessor
            )
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GetAuthToken()
        {
            if (!_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                return null;

            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"][0];

            if (string.IsNullOrWhiteSpace(token))
                return null;

           return _httpContextAccessor.HttpContext.Request.Headers["Authorization"][0].Remove(0, 7);
        }
    }
}
