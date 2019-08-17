using Common.Core.Interfaces;
using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.API.Extensions;
using Users.API.Interfaces;
using Users.API.Services;
using Users.Core.Interfaces;
using Users.Data;
using Users.Data.Repositories;

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
            services.AddMvc();
            services.AddDbContext<UsersContext>(options => options.UseSqlServer(Configuration.GetConnectionString("local")));
            services.AddSingleton(RabbitHutch.CreateBus($"host={Configuration.GetSection("RabbitMqHost").Value}"));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUsersService, UsersService>();
            services.ConfigureAutoMapper();
            services.AddSwaggerDocumentation();
            services.AddSingleton<ServiceBusListener>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerDocumentation();
            app.UseServiceBusListener();
        }
    }
}
