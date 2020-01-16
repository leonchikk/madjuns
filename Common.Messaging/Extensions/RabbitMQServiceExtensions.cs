using Common.Messaging.Abstractions;
using Common.Messaging.EventBus;
using Common.Messaging.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

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
                    HostName = configuration["RabbitMQ:EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["RabbitMQ:EventBusUserName"]))
                {
                    factory.UserName = configuration["RabbitMQ:EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["RabbitMQ:EventBusPassword"]))
                {
                    factory.Password = configuration["RabbitMQ:EventBusPassword"];
                }

                return new PersistentConnection(factory);
            });

            var subscriptionClientName = configuration["RabbitMQ:SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(serviceProvider =>
            {
                var rabbitMQPersistentConnection = serviceProvider.GetRequiredService<IPersistentConnection>();
                var eventBusSubcriptionsManager = serviceProvider.GetRequiredService<ISubscriptionsManager>();
                var logger = serviceProvider.GetRequiredService<ILogger<EventBusRabbitMQ>>();

                return new EventBusRabbitMQ(serviceProvider, logger, rabbitMQPersistentConnection, eventBusSubcriptionsManager, subscriptionClientName);
            });

            services.AddSingleton<ISubscriptionsManager, SubscriptionsManager>();

            return services;
        }
    }
}
