using Auth.API.EventsHandlers;
using Auth.API.Extensions;
using Auth.API.Infrastructure.Extensions;
using Auth.API.Interfaces;
using Auth.API.Middlewares;
using Auth.API.Services;
using Auth.Core.Events;
using Auth.Data;
using Auth.Data.Repositories;
using Authentication.API.Interfaces;
using Authentication.API.Services;
using Authentication.Services;
using Common.Core.Interfaces;
using Common.Messaging.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddDbContext<AuthContext>(options => options.UseSqlServer(Configuration.GetConnectionString("local")));
            services.AddRabbitMQEventBus(Configuration);
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddHttpContextAccessor();
            services.AddSwagger();

            services.AddTransient<UserDeletedEventHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
            });

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.SubscribeToEvent<UserDeletedEvent, UserDeletedEventHandler>();
        }
    }
}
