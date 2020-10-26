using System.Net.Mail;
using System.Threading.Tasks;

namespace MailSender.Services
{
    internal interface ISmtpSenderSender
    {
        Task SendAsync(MailMessage message);
    }
}