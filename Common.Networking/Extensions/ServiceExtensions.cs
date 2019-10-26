using Common.Networking.Implementations;
using Common.Networking.Interfaces;
using Common.Networking.Options;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Networking.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddHttpBaseClient(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IHttpBaseClient, HttpBaseClient>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<HttpBaseClientOptions>();

            return services;
        }

        public static IServiceCollection AddHttpBaseClient(this IServiceCollection services, HttpBaseClientOptions options)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IHttpBaseClient, HttpBaseClient>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton(options);

            return services;
        }
    }
}
