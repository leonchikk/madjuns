using Auth.ExternalProviders.Interfaces.Internal;
using Auth.ExternalProviders.Interfaces.Public;
using Auth.ExternalProviders.Providers;
using Auth.ExternalProviders.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.ExternalProviders.Extensions
{
    public static class AuthorizationServiceExtensions
    {
        public static IServiceCollection AddExternalAuthorization(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IExternalAuthProvider, AccountService>();

            return services;
        }

        public static IServiceCollection AddFacebookLogin(this IServiceCollection services)
        {
            services.AddScoped<IFacebookAuthProvider, FacebookAuthProvider>();
            return services;
        }

        public static IServiceCollection AddGoogleLogin(this IServiceCollection services)
        {
            services.AddScoped<IGoogleAuthProvider, GoogleAuthProvider>();
            return services;
        }

        public static IServiceCollection AddVkLogin(this IServiceCollection services)
        {
            services.AddScoped<IVkAuthProvider, VkAuthProvider>();
            return services;
        }
    }
}
