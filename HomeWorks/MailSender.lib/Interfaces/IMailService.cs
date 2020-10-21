using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.Interfaces
{
    public interface IMailService
    {
        IMailSender GetSender(string address, int port, bool useSSL, string login, string password);
    }
    public interface IMailSender
    {
        void Send(string from, string to, string title, string message);
        void Send(string from, IEnumerable<string> tos, string title, string message);
        Task SendAsync(string from, string to, string title, string message, CancellationToken cancel = default);

        Task SendAsync(string from, IEnumerable<string> tos, string title, string body,
            IProgress<(string to, double percent)> progress = null, CancellationToken cancel = default);
    }
}
