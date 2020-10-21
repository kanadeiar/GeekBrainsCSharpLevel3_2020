using System;
using System.Collections.Generic;
using System.Diagnostics;
using MailSender.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

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
            var locMessage = new MailMessage(from, to)
            {
                Subject = title,
                Body = message
            };
            var client = new SmtpClient(_address, _port)
            {
                EnableSsl = _useSsl,
                Credentials = new NetworkCredential(_login, _password)
            };
            try
            {
                client.Send(locMessage);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }
        }
        public void Send(string from, IEnumerable<string> tos, string title, string message)
        {
            foreach (var to in tos)
            {
                Send(from, to, title, message);
            }
        }
        public async Task SendAsync(string @from, string to, string title, string message, CancellationToken cancel = default)
        {
            var locMessage = new MailMessage(from, to)
            {
                Subject = title,
                Body = message
            };
            var client = new SmtpClient(_address, _port)
            {
                EnableSsl = _useSsl,
                Credentials = new NetworkCredential(_login, _password)
            };
            try
            {
                cancel.ThrowIfCancellationRequested();
                await client.SendMailAsync(new MailMessage(from, to)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                throw;
            }
        }
        public async Task SendAsync(string @from, IEnumerable<string> tos, string title, string body, IProgress<(string to, double percent)> progress = null,
            CancellationToken cancel = default)
        {
            
        }
    }
}
