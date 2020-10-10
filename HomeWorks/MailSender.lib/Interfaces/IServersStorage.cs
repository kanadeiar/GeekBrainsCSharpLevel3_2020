using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Interfaces
{
    public interface IServersStorage
    {
        ICollection<Server> Servers { get; }
        void Load();
        void SaveChanges();
    }
}
