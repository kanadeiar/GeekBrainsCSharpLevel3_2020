using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Interfaces
{
    public interface IMailService
    {
        IMailSender GetSender(string address, int port, bool useSSL, string login, string password);
    }
}
