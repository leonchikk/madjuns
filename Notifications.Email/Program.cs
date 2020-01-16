using Common.Messaging.Abstractions;
using Common.Messaging.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notifications.Email.EventHandlers;
using Notifications.Email.Events;
using Notifications.Email.Interfaces;
using Notifications.Email.Services;
using System;

namespace Notifications.Email
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IEmailService, EmailService>()
                .AddSingleton<SendMailEventHandler>()
                .AddRabbitMQEventBus(configuration)
                .AddSingleton<IConfiguration>(sp => configuration)
                .AddLogging(configure => configure.AddConsole())
                .BuildServiceProvider();

            var messageBus = serviceProvider.GetRequiredService<IEventBus>();
            messageBus.Subscribe<SendMailEvent, SendMailEventHandler>();
        }
    }
}
