using Autofac;
using Autofac.Core.Lifetime;
using Common.Messaging.Abstractions;
using Common.Messaging.EventBus;
using Common.Messaging.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Extensions
{
    public static class RabbitMQServiceExtensions
    {
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPersistentConnection>(serviceProvider =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                {
                    factory.UserName = configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                {
                    factory.Password = configuration["EventBusPassword"];
                }

                return new PersistentConnection(factory);
            });

            var subscriptionClientName = configuration["SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<ISubscriptionsManager>();

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, eventBusSubcriptionsManager, iLifetimeScope, subscriptionClientName);
            });

            services.AddSingleton<ISubscriptionsManager, SubscriptionsManager>();

            return services;
        }

        public static EventBusRabbitMQ CreateRabbitMQEventBus(IConfiguration configuration)
        {
            var lifeTimeScope = new ContainerBuilder().Build().BeginLifetimeScope();

            var factory = new ConnectionFactory()
            {
                HostName = configuration["EventBusConnection"],
                DispatchConsumersAsync = true
            };

            if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
            {
                factory.UserName = configuration["EventBusUserName"];
            }

            if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
            {
                factory.Password = configuration["EventBusPassword"];
            }

            var persistentConnection = new PersistentConnection(factory);
            var subscriptionClientName = configuration["SubscriptionClientName"];
            var eventBusSubcriptionsManager = new SubscriptionsManager();

            return new EventBusRabbitMQ(persistentConnection, eventBusSubcriptionsManager, lifeTimeScope, subscriptionClientName);
        }
    }
}
