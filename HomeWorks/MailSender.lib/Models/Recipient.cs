using System;
using System.ComponentModel;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Recipient : BasePerson, IDataErrorInfo
    {
        public override string Name
        {
            get => base.Name;
            set
            {
                if (value == "QWE")
                    throw new ArgumentException("Запрещено вводить это!", nameof(value));
                base.Name = value;
            }
        }
        string IDataErrorInfo.Error => null;
        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case nameof(Name):
                        var name = Name;
                        if (name is null) return "Имя не может быть пустой строкой";
                        if (name.Length < 2) return "Имя не должно быть короче двух символов";
                        if (name.Length > 20) return "Имя не должно быть длиннее 20 символов";
                        return null;
                    case nameof(Address):
                        return null;
                    default:
                        return null;
                }
            }
        }



    }
}
