using Common.Networking.Implementaions;
using Common.Networking.Implementations;
using Common.Networking.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Networking.Extensions
{
    public static class CustomHttpClientServiceExtensions
    {
        public static IServiceCollection AddCustomHttpClient(this IServiceCollection services)
        {
            services.AddScoped<IHttpBaseClient, HttpBaseClient>();
            services.AddScoped<IHttpCallerUtils, HttpCallerUtils>();

            return services;
        }
    }
}
