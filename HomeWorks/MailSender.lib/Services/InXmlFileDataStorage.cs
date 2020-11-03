using MailSender.Interfaces;
using MailSender.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MailSender.Services
{
    /// <summary>
    /// Сохранение данных в XML файле
    /// </summary>
    public class InXmlFileDataStorage : IServerStorage, ISenderStorage, IRecipientStorage, IMessageStorage
    {
        private readonly string _fileName;
        private DataStructure Data { get; set; } = new DataStructure();

        ICollection<Server> IStorage<Server>.Items => Data.Servers;
        ICollection<Sender> IStorage<Sender>.Items => Data.Senders;
        ICollection<Recipient> IStorage<Recipient>.Items => Data.Recipients;
        ICollection<Message> IStorage<Message>.Items => Data.Messages;

        public InXmlFileDataStorage(string fileName)
        {
            _fileName = fileName;
        }
        public void Load()
        {
            if (!File.Exists(_fileName))
            {
                Data = new DataStructure();
                return;
            }
            using var file = File.OpenText(_fileName);
            if (file.BaseStream.Length == 0)
            {
                Data = new DataStructure();
                return;
            }
            var serializer = new XmlSerializer(typeof(DataStructure));
            Data = (DataStructure)serializer.Deserialize(file);
        }
        public void SaveChanges()
        {
            using var file = File.CreateText(_fileName);
            var serializer = new XmlSerializer(typeof(DataStructure));
            serializer.Serialize(file, Data);
        }
        public class DataStructure
        {
            public List<Server> Servers { get; set; } = new List<Server>();
            public List<Sender> Senders { get; set; } = new List<Sender>();
            public List<Recipient> Recipients { get; set; } = new List<Recipient>();
            public List<Message> Messages { get; set; } = new List<Message>();
        }


    }
}
