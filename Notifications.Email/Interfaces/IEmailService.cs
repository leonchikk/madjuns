using System.Net.Mail;

namespace Notifications.Email.Interfaces
{
    public interface IEmailService
    {
        void SendMail(MailAddress to, string body);
    }
}
