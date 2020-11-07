using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Timers;
using MailSender.Interfaces;
using MailSender.Models.Base;

namespace MailSender.Services
{
    /// <summary>
    /// Сервис отправки писем запланированно
    /// </summary>
    public class SchedulerMailService : ISchedulerMailService
    {
        public ISchedulerMailSender GetScheduler(IMailSender mailSender) => new SchedulerMailSender(mailSender);
        public class SchedulerMailSender : Model, ISchedulerMailSender
        {
            private Timer _timer;
            private DateTime _dateTimeSend;
            public DateTime DateTimeSend
            {
                get => _dateTimeSend;
                set => Set(ref _dateTimeSend, value);
            }
            private IMailSender _mailSender;
            private MailMessage _mailMessage;
            public string MailMessageFromAddress => _mailMessage.From.Address;
            public string MailMessageToAddress => _mailMessage.To.First().Address;
            public string MailMessageTitle => _mailMessage.Subject;
            public string MailMessageMessage => _mailMessage.Body;
            public SchedulerMailSender(IMailSender mailSender)
            {
                _mailSender = mailSender;
            }
            public void AddTaskSend(DateTime dateTimeSend, string from, string to, string title, string message)
            {
                _timer = new Timer(1000);
                _timer.Elapsed += Timer_Tick;
                _timer.Start();
                DateTimeSend = dateTimeSend;
                _mailMessage = new MailMessage(from, to, title, message);
            }
            private void Timer_Tick(object sender, EventArgs e)
            {
                if (DateTime.Now > _dateTimeSend)
                {
                    _mailSender.Send(_mailMessage.From.Address, _mailMessage.To.First().Address, _mailMessage.Subject, _mailMessage.Body);
                    _timer.Stop();
                    _eventSend?.Invoke();
                }
            }
            public void Stop()
            {
                _timer.Stop();
            }
            private event Action _eventSend;
            public event Action EventSend
            {
                add
                {
                    _eventSend += value;
                }
                remove
                {
                    if (_eventSend != null) _eventSend -= value;
                }
            }
        }
    }
}
