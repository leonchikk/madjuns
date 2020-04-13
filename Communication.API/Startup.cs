using Common.Core.Interfaces;
using Common.Messaging.Abstractions;
using Common.Messaging.Extensions;
using Communication.API.Application.EventHandlers;
using Communication.Core.Events;
using Communication.Data;
using Communication.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Communication.API
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
            services.AddDbContext<CommunicationsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("local")));
            services.AddMediatR(typeof(Startup).Assembly);
            
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            services.AddRabbitMQEventBus(Configuration)
                .AddTransient<UserCreatedEventHandler>()
                .AddTransient<UserDeletedEventHandler>();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureEventHandlers(app.ApplicationServices);

            app.UseMvc();
        }

        private void ConfigureEventHandlers(IServiceProvider applicationServices)
        {
            var eventBus = applicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<UserCreatedEvent, UserCreatedEventHandler>();
            eventBus.Subscribe<UserDeletedEvent, UserDeletedEventHandler>();
        }
    }
}
