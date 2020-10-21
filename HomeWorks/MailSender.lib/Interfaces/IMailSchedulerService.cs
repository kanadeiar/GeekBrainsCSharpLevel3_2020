using System;
using System.Collections.Generic;
using System.Text;
using MailSender.Models;

namespace MailSender.Interfaces
{
    public interface IMailSchedulerService
    {
        void Start();
        void Stop();
        void AddTask(DateTime time, Sender sender, IEnumerable<Recipient> recipients, Server server, Message message);
    }
}
