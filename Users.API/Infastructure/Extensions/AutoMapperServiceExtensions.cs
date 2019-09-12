using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Users.API.Profiles;

namespace Users.API.Extensions
{
    public static class AutoMapperServiceExtensions
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
