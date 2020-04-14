using System;
using System.Collections.Generic;
using ApiGateway.Web.HttpClients.Implementations;
using ApiGateway.Web.HttpClients.Interfaces;
using ApiGateway.Web.Infrastructure.Extensions;
using ApiGateway.Web.Settings;
using Common.Networking.Extensions;
using Common.Networking.Implementations;
using Common.Networking.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace ApiGateway.Web
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
            services.AddHttpClient("auth", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetSection("GatewaySettings:AuthApiUrl").Value);
            });

            services.AddHttpClient("users", c =>
                c.BaseAddress = new Uri(Configuration.GetSection("GatewaySettings:UsersApiUrl").Value)
            );

            services.Configure<GatewaySettings>(Configuration.GetSection("GatewaySettings"));
            services.AddCustomHttpClient();

            services.AddScoped<IHttpAuthClient, HttpAuthClient>();
            services.AddScoped<IHttpUsersClient, HttpUsersClient>();

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.ConfigureAuthentication(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core API", Description = "Swagger Core API" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
                });
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
