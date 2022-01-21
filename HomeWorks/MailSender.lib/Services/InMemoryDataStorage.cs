using MailSender.Interfaces;
using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;

namespace MailSender.Services
{
    /// <summary>
    /// Хранилище данных в оперативной памяти
    /// </summary>
    public class InMemoryDataStorage : IServerStorage, ISenderStorage, IRecipientStorage, IMessageStorage
    {
        public ICollection<Server> Servers { get; set; }
        public ICollection<Sender> Senders { get; set; }
        public ICollection<Recipient> Recipients { get; set; }
        public ICollection<Message> Messages { get; set; }
        ICollection<Server> IStorage<Server>.Items => Servers;
        ICollection<Sender> IStorage<Sender>.Items => Senders;
        ICollection<Recipient> IStorage<Recipient>.Items => Recipients;
        ICollection<Message> IStorage<Message>.Items => Messages;
        public void Load()
        {
            Debug.WriteLine("Вызвана процедура загрузки данных");
            if (Servers is null || Servers.Count == 0)
            {
                Servers = new List<Server>
                {
                    new Server
                    {
                        Id = 1,
                        Name = "Яндекс",
                        Address = "smtp.yandex.ru",
                        Port = 465,
                        UseSSL = true,
                        Login = "user@yandex.ru",
                        Password = new NetworkCredential("","Password").SecurePassword,
                    },
                    new Server
                    {
                        Id = 2,
                        Name = "GMail",
                        Address = "smtp.gmail.com",
                        Port = 465,
                        UseSSL = true,
                        Login = "user@gmail.com",
                        Password = new NetworkCredential("","Password").SecurePassword,
                    }
                };
            };
            if (Senders is null || Senders.Count == 0)
            {
                Senders = new List<Sender>
                {
                    new Sender
                    {
                        Id = 1,
                        Name = "Иванов",
                        Address = "ivanov@server.ru",
                        Description = "Почта от Иванова"
                    },
                    new Sender
                    {
                        Id = 2,
                        Name = "Петров",
                        Address = "petrov@server.ru",
                        Description = "Почта от Петровича"
                    },
                    new Sender
                    {
                        Id = 3,
                        Name = "Сидоров",
                        Address = "sidorov@server.ru",
                        Description = "Почта от Сидорова"
                    }
                };
            };
            if (Recipients is null || Recipients.Count == 0)
            {
                Recipients = new List<Recipient>
                {
                    new Recipient
                    {
                        Id = 1,
                        Name = "Иванов",
                        Address = "ivanov@server.ru",
                        Description = "Почта для Иванова"
                    },
                    new Recipient
                    {
                        Id = 2,
                        Name = "Петров",
                        Address = "petrov@server.ru",
                        Description = "Почта для Петровича"
                    },
                    new Recipient
                    {
                        Id = 3,
                        Name = "Сидоров",
                        Address = "sidorov@server.ru",
                        Description = "Почта для Сидорова"
                    }
                };
            };
            if (Messages == null || Messages.Count == 0)
            {
                Messages = Enumerable.Range(1, 10)
                    .Select(i => new Message
                    {
                        Id = i,
                        Title = $"Сообщение {i}",
                        Body = $"Текст сообщения {i}"
                    })
                    .ToList();
            };
        }
        public void SaveChanges()
        {
            Debug.WriteLine("Вызвана процедура сохранения данных");
        }
    }
}
