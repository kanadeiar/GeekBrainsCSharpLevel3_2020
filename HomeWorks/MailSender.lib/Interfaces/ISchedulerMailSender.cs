using System;

namespace MailSender.Interfaces
{
    public interface ISchedulerMailSender
    {
        void Send(DateTime dateTimeSend, string from, string to, string title, string message);
    }
}
