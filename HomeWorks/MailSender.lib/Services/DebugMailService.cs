using MailSender.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MailSender.Services
{
    public class DebugMailService : IMailService
    {
        public IMailSender GetSender(string address, int port, bool useSSL, string login, string password)
            => new DebugMailSender(address, port, useSSL, login, password);
        private class DebugMailSender : IMailSender
        {
            private readonly string _address;
            private readonly int _port;
            private readonly bool _useSsl;
            private readonly string _login;
            private readonly string _password;
            public DebugMailSender(string address, int port, bool useSSL, string login, string password)
            {
                _address = address;
                _port = port;
                _useSsl = useSSL;
                _login = login;
                _password = password;
            }
            public void Send(string from, string to, string title, string message)
            {
                Debug.WriteLine($"Почтовый сервер {_address}:{_port}(ssl:{(_useSsl?"да":"нет")}[логин: {_login} - пароль: {_password}])");
                Debug.WriteLine($"Отправка письма от {from} к {to} тема: {title} текст: {message}");
            }
        }
    }
}
