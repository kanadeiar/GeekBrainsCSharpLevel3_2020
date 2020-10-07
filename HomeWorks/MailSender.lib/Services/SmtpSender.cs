using System.Net;
using System.Net.Mail;

namespace MailSender.Services
{
    public class SmtpSender
    {
        private readonly string _address;
        private readonly int _port;
        private readonly bool _useSsl;
        private readonly string _login;
        private readonly string _password;
        public SmtpSender(string Address, int Port, bool UseSSL, string Login, string Password)
        {
            _address = Address;
            _port = Port;
            _useSsl = UseSSL;
            _login = Login;
            _password = Password;
        }

        public void Send(string From, string To, string Title, string Message)
        {
            var message = new MailMessage(From, To)
            {
                Subject = Title,
                Body = Message
            };
            var client = new SmtpClient(_address, _port)
            {
                EnableSsl = _useSsl,
                Credentials = new NetworkCredential(_login, _password)
            };
            client.Send(message);
        }
    }
}
