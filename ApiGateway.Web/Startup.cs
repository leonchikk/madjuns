﻿using System;
using System.Collections.Generic;
using ApiGateway.Web.BackgroundWorkers;
using ApiGateway.Web.HttpClients.Implementations;
using ApiGateway.Web.HttpClients.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                c.BaseAddress = new Uri(Configuration.GetSection("ApiUrls:AuthApi").Value)
            );
            services.AddScoped<IHttpBaseClient, HttpBaseClient>();
            services.AddScoped<IHttpAuthClient, HttpAuthClient>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Core API", Description = "Swagger Core API" });

                Dictionary<string, IEnumerable<string>> security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });

            var section = Configuration.GetSection("Test");

            services.AddHostedService<TestBackgroundWorker>();
            services.AddOptions();
            services.Configure<TestSettings>(section);
            services.AddMvc();
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
            app.UseMvc();
        }
    }
}
