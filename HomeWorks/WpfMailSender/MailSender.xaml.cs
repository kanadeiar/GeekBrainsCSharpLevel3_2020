using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using WpfMailSender.Data;
using WpfMailSender.Models;
using WpfMailSender.Services;
using WpfMailSender.Windows;

namespace WpfMailSender
{
    /// <summary>
    /// Логика взаимодействия для MailSender.xaml
    /// </summary>
    public partial class MailSender : Window
    {
        public MailSender()
        {
            InitializeComponent();
        }


        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonSendNow_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ComboBoxSenders.SelectedItem is Sender sender_)) return;
            if (!(DataGridRecirients.SelectedItem is Recipient recipient)) return;
            if (!(ComboBoxServers.SelectedItem is Server server)) return;
            if (!(ListBoxMessages.SelectedItem is Message message)) return;
            
            var mailSender = new SmtpSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);

            try
            {
                var timer = Stopwatch.StartNew();
                mailSender.Send(sender_.Address, recipient.Address, message.Title, message.Body);
                timer.Stop();
                MessageBox.Show($"Почтовое сообщение успешно отправлено за {timer.Elapsed.TotalSeconds:0.##} секунд", "Отправка почты", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка отправки почты!", "Отправка почты", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAddServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (!ServerEditWindow.Create(
                out var name,
                out var address,
                out var port,
                out var ssl,
                out var description,
                out var login,
                out var password))
                return;

            var server = new Server
            {
                Id = TestData.Servers
                    .DefaultIfEmpty()
                    .Max(s => s.Id) + 1,
                Name = name,
                Address = address,
                Port = port,
                UseSSL = ssl,
                Desctiption = description,
                Login = login,
                Password = password
            };

            TestData.Servers.Add(server);

            ComboBoxServers.ItemsSource = null;
            ComboBoxServers.ItemsSource = TestData.Servers;
            ComboBoxServers.SelectedItem = server;
        }

        private void ButtonEditServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ComboBoxServers.SelectedItem is Server server))
                return;

            var name = server.Name;
            var address = server.Address;
            var port = server.Port;
            var ssl = server.UseSSL;
            var description = server.Desctiption;
            var login = server.Login;
            var password = server.Password;

            if (!ServerEditWindow.ShowDialog("Редактирование сервера",
                ref name,
                ref address,
                ref port,
                ref ssl,
                ref description,
                ref login,
                ref password))
                return;

            server.Name = name;
            server.Address = address;
            server.Port = port;
            server.UseSSL = ssl;
            server.Desctiption = description;
            server.Login = login;
            server.Password = password;

            ComboBoxServers.ItemsSource = null;
            ComboBoxServers.ItemsSource = TestData.Servers;
            ComboBoxServers.SelectedItem = server;
        }

        private void ButtonDeleteServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ComboBoxServers.SelectedItem is Server server)) 
                return;

            TestData.Servers.Remove(server);

            ComboBoxServers.ItemsSource = null;
            ComboBoxServers.ItemsSource = TestData.Servers;
            ComboBoxServers.SelectedItem = TestData.Servers.FirstOrDefault();
        }
    }
}
