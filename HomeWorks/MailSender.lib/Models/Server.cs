using System.ComponentModel;
using System.Security;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Server : NamedModel, IDataErrorInfo
    {
        private string _name;
        public override string Name 
        {
            get => _name;
            set => Set(ref _name, value); 
        }
        private string _address;
        public string Address 
        { 
            get => _address; 
            set => Set(ref _address, value); 
        }
        private int _port;
        public int Port 
        { 
            get => _port;
            set => Set(ref _port, value); 
        }
        public bool UseSSL { get; set; }
        private string _description;
        public string Description 
        { 
            get => _description; 
            set => Set(ref _description, value); 
        }
        private string _login;
        public string Login
        {
            get => _login; 
            set => Set(ref _login, value);
        }
        private SecureString _password;
        public SecureString Password
        {
            get => _password; 
            set => Set(ref _password, value);
        }
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
                        if (name is null) return "Имя сервера не может быть пустой строкой";
                        if (name.Length < 3) return "Имя сервера не может быть короче трех симоволов";
                        if (name.Length > 30) return "Имя сервера не может быть длиннее тридцати символов";
                        return null;
                    case nameof(Address):
                        var address = Address;
                        if (address is null) return "Адрес сервера не может быть пустой строкой";
                        regex = new Regex(@"(\w+\.)*\w+[A-Za-z]+");
                        if (!regex.IsMatch(address)) return "Строка адреса сервера имеет неверный формат";
                        return null;
                    case nameof(Port):
                        var port = Port;
                        if (port < 1) return "Значение порта не может быть меньше одного";
                        if (port > 999) return "Значение порта не может быть больше 999";
                        return null;
                    case nameof(Description):
                        var description = Description;
                        if (description == "Туфта") return "Запрещено вводить такое слово как \"Туфта\"!";
                        return null;
                    case nameof(Login):
                        var login = Login;
                        if (login is null) return "Значение логина пользователя не может быть пустой строкой";
                        if (login.Length < 3) return "Слишком короткое имя пользователя";
                        if (login.Length > 30) return "Слишком длинное имя пользователя";
                        regex = new Regex(@"(?i)\b(\w+\p{P}*\p{S}*\p{Z}*\p{C}*\s?)+\b");
                        if (!regex.IsMatch(login)) return "Логин пользователя слишком простой";
                        return null;
                    case nameof(Password):
                        var password = Password;
                        if (password is null) return "Значение пароля не может быть пустым";
                        if (password.Length < 6) return "Слишком короткий пароль";
                        regex = new Regex(@"(\w+\.)*\w+[A-Za-z]+");
                        if (!regex.IsMatch(password.ToString())) return "Пароль слишком простой, должны быть: цифры, спецсимволы, буквы в верхнем регистре, нижнем регистре и длинна более 6 символов";
                        return null;
                    default:
                        return null;
                }
            }
        }
    }
}
