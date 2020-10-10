using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Interfaces
{
    public interface IMailSender
    {
        void Send(string from, string to, string title, string message);
    }
}
