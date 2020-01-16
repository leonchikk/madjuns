using Common.Core.Interfaces;
using Common.Messaging.Abstractions;
using Common.Messaging.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.API.EventHandlers;
using Users.API.Extensions;
using Users.API.Infastructure.Extensions;
using Users.API.Middlewares;
using Users.Core.Events;
using Users.Data;
using Users.Data.Repositories;
using Users.Services.Services;
using Users.Services.Services.Bans;
using Users.Services.Services.Friends;
using Users.Services.Services.Subscriptions;
using Users.Services.Users.Interfaces;

namespace Users.API
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
            services.AddDbContext<UsersContext>(options => options.UseLazyLoadingProxies()
                                                                  .UseSqlServer(Configuration.GetConnectionString("local")));
            services.AddRabbitMQEventBus(Configuration);
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IBansService, BansService>();
            services.AddTransient<IFriendsService, FriendsService>();
            services.AddTransient<ISubscriptionsService, SubscriptionsService>();

            services.ConfigureAuthentication(Configuration);
            services.ConfigureAutoMapper();
            services.AddSwaggerDocumentation();
            services.AddTransient<UserCreatedEventHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerDocumentation();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();
            app.UseMvc();

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<UserCreatedEvent, UserCreatedEventHandler>();
        }
    }
}
