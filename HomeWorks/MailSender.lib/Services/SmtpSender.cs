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

        private readonly ISmtpSenderSmtpClient _smtpClient;
        public SmtpSender(string address, int port, bool useSSL, string login, string password, ISmtpSenderSmtpClient smtpClient) :
            this(address, port, useSSL, login, password)
        {
            _smtpClient = smtpClient;
        }
        public SmtpSender(string address, int port, bool useSSL, string login, string password)
        {
            _address = address;
            _port = port;
            _useSsl = useSSL;
            _login = login;
            _password = password;
            _smtpClient = new SmtpSenderSmtpClient();
        }
        public void Send(string from, string to, string title, string message)
        {
            var locMessage = new MailMessage(from, to)
            {
                Subject = title,
                Body = message
            };
            _smtpClient.NewSmtpClient(_address, _port, _useSsl, new NetworkCredential(_login, _password));
            try
            {
                _smtpClient.Send(locMessage);
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
            _smtpClient.NewSmtpClient(_address,_port,_useSsl, new NetworkCredential(_login, _password));
            try
            {
                cancel.ThrowIfCancellationRequested();

                await _smtpClient.SendMailAsync(locMessage).ConfigureAwait(false);
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
        private class SmtpSenderSmtpClient : ISmtpSenderSmtpClient
        {
            private SmtpClient client;
            public void NewSmtpClient(string address, int port, bool useSsl, NetworkCredential credential)
            {
                client = new SmtpClient(address, port);
                client.EnableSsl = useSsl;
                client.Credentials = credential;
            }
            public void Send(MailMessage message)
            {
                client.Send(message);
            }
            public async Task SendMailAsync(MailMessage message)
            {
                await client.SendMailAsync(message).ConfigureAwait(false);
            }
        }
    }
    public interface ISmtpSenderSmtpClient
    {
        void NewSmtpClient(string address, int port, bool useSsl, NetworkCredential credential);
        void Send(MailMessage message);
        Task SendMailAsync(MailMessage message);
    }
}
