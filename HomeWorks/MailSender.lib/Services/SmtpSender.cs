using MailSender.Interfaces;
using System.Net;
using System.Net.Mail;

namespace MailSender.Services
{
    public class SmtpSender : IMailSender
    {
        private readonly string _address;
        private readonly int _port;
        private readonly bool _useSsl;
        private readonly string _login;
        private readonly string _password;
        public SmtpSender(string address, int port, bool useSSL, string login, string password)
        {
            _address = address;
            _port = port;
            _useSsl = useSSL;
            _login = login;
            _password = password;
        }

        public void Send(string from, string to, string title, string message)
        {
            var loc_message = new MailMessage(from, to)
            {
                Subject = title,
                Body = message
            };
            var client = new SmtpClient(_address, _port)
            {
                EnableSsl = _useSsl,
                Credentials = new NetworkCredential(_login, _password)
            };
            client.Send(loc_message);
        }
    }
}
