using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Interfaces;
using MailSender.Models;

namespace MailSender.Services
{
    public class TaskMailSchedulerService : IMailSchedulerService
    {
        private readonly IStore<SchedulerTask> _tasksStore;
        private readonly IMailService _mailService;
        private Task _shedulerTask;
        private readonly CancellationTokenSource _tastCancellation = new CancellationTokenSource();

        public TaskMailSchedulerService(IStore<SchedulerTask> tasksStore, IMailService mailService)
        {
            _tasksStore = tasksStore;
            _mailService = mailService;
        }
        public void Start()
        {
            _shedulerTask = Task.Factory.StartNew(SchedulerTaskMethodAsync, TaskCreationOptions.LongRunning);
        }
        public void Stop() => _tastCancellation.Cancel();

        private async Task SchedulerTaskMethodAsync()
        {
            var cancel = _tastCancellation.Token;
            while (true)
            {
                cancel.ThrowIfCancellationRequested();

                var nextTask = _tasksStore.GetAll()
                    .OrderBy(t => t.Time)
                    .FirstOrDefault(t => t.Time > DateTime.Now);
                if (nextTask is null) break;

                TimeSpan sleepTime = nextTask.Time - DateTime.Now;

                if (sleepTime.TotalMilliseconds > 0)
                    await Task.Delay(sleepTime, cancel);

                cancel.ThrowIfCancellationRequested();
                await ExecuteAsync(nextTask);
            }
        }
        private async Task ExecuteAsync(SchedulerTask task)
        {
            var server = task.Server;
            var sender = _mailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login,
                new NetworkCredential("",server.Password).Password);
            sender.Send(task.Sender.Address, task.Recipients.Select(r => r.Address), task.Message.Title,
                task.Message.Body);
        }

        public void AddTask(DateTime time, Sender sender, IEnumerable<Recipient> recipients, Server server, Message message)
        {
            Stop();
            _tasksStore.Add(new SchedulerTask
            {
                Time = time,
                Sender = sender,
                Recipients = recipients.ToArray(),
                Message = message,
                Server = server,
            });
            Start();
        }
    }
}
