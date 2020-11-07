using System;

namespace MailSender.Interfaces
{
    public interface ISchedulerMailSender
    {
        void AddTaskSend(DateTime dateTimeSend, string from, string to, string title, string message);
        void Stop();
        event Action EventSend;
    }
}
