using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MailSender.Models;

namespace WpfMailSenderCore.Data
{
    public class TestData
    {
        public static TestData LoadFromXML(string filename)
        {
            var serializer = new XmlSerializer(typeof(TestData));
            using var file = File.OpenText(filename);
            return (TestData) serializer.Deserialize(file);
        }
        public void SaveToXML(string filename)
        {
            var serializer = new XmlSerializer(typeof(TestData));
            using var file = File.Create(filename);
            serializer.Serialize(file, this);
        }
        public IList<Server> Servers { get; set; } = new List<Server>
        {
            new Server
            {
                Id = 1,
                Name = "Яндекс",
                Address = "smtp.yandex.ru",
                Port = 465,
                UseSSL = true,
                Login = "user@yandex.ru",
                Password = "Password",
            },
            new Server
            {
                Id = 2,
                Name = "GMail",
                Address = "smtp.gmail.com",
                Port = 465,
                UseSSL = true,
                Login = "user@gmail.com",
                Password = "Password",
            }
        };
        public IList<Sender> Senders { get; set; } = new List<Sender>
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
        public IList<Recipient> Recipients { get; set; } = new List<Recipient>
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
                Description = "Почта для Петрова"
            },
            new Recipient
            {
                Id = 3,
                Name = "Сидоров",
                Address = "sidorov@server.ru",
                Description = "Почта для Сидорова"
            }
        };
        public IList<Message> Messages { get; set; } = Enumerable
            .Range(1, 10)
            .Select(i => new Message
            {
                Id = i,
                Title = $"Сообщение {i}",
                Body = $"Текст сообщения {i}"
            })
            .ToList();
    }

}
