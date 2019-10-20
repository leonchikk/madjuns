using Microsoft.Extensions.Configuration;
using Notifications.Email.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Notifications.Email.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_configuration.GetSection("SmtpClientSettings:UserName").Value, _configuration.GetSection("SmtpClientSettings:Password").Value),
                EnableSsl = true
            };
        }

        public void SendMail(MailAddress to, string subject, string body)
        {
            Console.WriteLine($"Starting sending email to {to.Address}");

            MailMessage msg = new MailMessage
            {
                From = new MailAddress(_configuration.GetSection("SmtpClientSettings:UserName").Value, "MadJuns"),
                Subject = subject,
                Body = body,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };
            msg.To.Add(to);

            _smtpClient.Send(msg);
        }
    }
}
