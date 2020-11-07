using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Timers;
using MailSender.Interfaces;

namespace MailSender.Services
{
    /// <summary>
    /// Сервис отправки писем запланированно
    /// </summary>
    public class SchedulerMailService : ISchedulerMailService
    {
        public ISchedulerMailSender GetScheduler(IMailSender mailSender) => new SchedulerMailSender(mailSender);
        private class SchedulerMailSender : ISchedulerMailSender
        {
            private Timer _timer;
            private DateTime _dateTimeSend;
            private IMailSender _mailSender;
            private MailMessage _mailMessage;
            public SchedulerMailSender(IMailSender mailSender)
            {
                _mailSender = mailSender;
            }
            public void AddTaskSend(DateTime dateTimeSend, string from, string to, string title, string message)
            {
                _timer = new Timer(1000);
                _timer.Elapsed += Timer_Tick;
                _timer.Start();
                _dateTimeSend = dateTimeSend;
                _mailMessage = new MailMessage(from, to, title, message);
            }
            private void Timer_Tick(object sender, EventArgs e)
            {
                if (DateTime.Now > _dateTimeSend)
                {
                    _mailSender.Send(_mailMessage.From.Address, _mailMessage.To.First().Address, _mailMessage.Subject, _mailMessage.Body);
                    _timer.Stop();
#if DEBUG
                    Debug.WriteLine($"Запланированное на {_dateTimeSend.ToString("U")} письмо отправлено.");
#endif
                }
            }
        }
    }
}
