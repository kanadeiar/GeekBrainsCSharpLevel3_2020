using System;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Recipient : BasePerson
    {
        public override string Name
        {
            get => base.Name;
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(value));
                if (value == "")
                    throw new ArgumentException("Имя не может быть пустой строкой", nameof(value));
                if (value == "QWE")
                    throw new ArgumentException("Запрещено вводить это!", nameof(value));
                base.Name = value;
            }
        }
    }
}
