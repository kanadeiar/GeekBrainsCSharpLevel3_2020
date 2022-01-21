using System.ComponentModel;
using System.Text.RegularExpressions;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Sender : BasePerson, IDataErrorInfo
    {
        string IDataErrorInfo.Error => null;

        public string this[string propertyName]
        {
            get
            {
                Regex regex = null;
                switch (propertyName)
                {
                    case nameof(Name):
                        var name = Name;
                        if (name is null) return "Имя отправителя не может быть пустым";
                        if (name.Length < 2) return "Имя отправителя должно быть длиннее двух символов";
                        if (name.Length > 30) return "Имя отправителя должно быть короче тридцати символов";
                        return null;
                    case nameof(Address):
                        var address = Address;
                        if (address is null) return "Почтовый адрес отправителя не может быть пустой строкой";
                        regex = new Regex(@"(\w+\.)*\w+[A-Za-z]+");
                        if (!regex.IsMatch(address)) return "Строка почтового адреса отправителя имеет неверный формат";
                        return null;
                    case nameof(Description):
                        var description = Description;
                        if (description == "Туфта") return "Запрещено вводить такое слово как \"Туфта\"!";
                        return null;
                    default:
                        return null;
                }
            }
        }
    }
}
