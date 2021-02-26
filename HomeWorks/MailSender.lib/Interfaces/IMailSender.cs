using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Interfaces
{
    public interface IMailSender
    {
        void Send(string from, string to, string title, string message);
        void Send(string from, IEnumerable<string> tos, string title, string message);
    }
}
