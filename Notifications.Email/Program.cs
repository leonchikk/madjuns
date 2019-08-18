using Common.Core.Events;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Notifications.Email.Interfaces;
using Notifications.Email.Services;
using System;
using System.Net.Mail;

namespace Notifications.Email
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            IEmailService emailService = new EmailService(configuration);

            System.Console.WriteLine("---------------------------------------------------------");
            System.Console.WriteLine($"host={configuration.GetSection("RabbitMqHost").Value}");
            System.Console.WriteLine("---------------------------------------------------------");

            IBus messageBus = RabbitHutch.CreateBus($"host={configuration.GetSection("RabbitMqHost").Value}");


            messageBus.Subscribe<SendMailEvent>(Guid.NewGuid().ToString(), msg => emailService.SendMail(new MailAddress(msg.To), msg.Subject, msg.Body));
        }
    }
}
