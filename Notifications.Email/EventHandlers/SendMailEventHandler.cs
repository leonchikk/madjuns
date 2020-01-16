using Common.Messaging.Abstractions;
using Notifications.Email.Events;
using Notifications.Email.Interfaces;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Notifications.Email.EventHandlers
{
    public class SendMailEventHandler : IEventHandler<SendMailEvent>
    {
        private readonly IEmailService _emailService;

        public SendMailEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task HandleAsync(SendMailEvent @event)
        {
            _emailService.SendMail(new MailAddress(@event.To), @event.Subject, @event.Body);
        }
    }
}
