using System;
using System.Collections.Generic;
using System.Text;
using MailSender.Models.Base;

namespace MailSender.Interfaces
{
    public interface IStore<T> where T : Model
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        T Add(T Item);
        void Update(T Item);
        void Delete(int Id);
    }
}
