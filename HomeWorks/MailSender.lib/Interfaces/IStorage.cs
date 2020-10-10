using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Interfaces
{
    public interface IStorage<T>
    {
        ICollection<T> Items { get; }
        /// <summary> Считывание данных из файла/БД </summary>
        void Load();
        /// <summary> Записать данные в файл/БД </summary>
        void SaveChanges();
    }

    public interface IServerStorage : IStorage<Server> { }
    public interface ISenderStorage : IStorage<Sender> { }
    public interface IRecipientStorage : IStorage<Recipient> { }
    public interface IMessageStorage : IStorage<Message> { }
}
