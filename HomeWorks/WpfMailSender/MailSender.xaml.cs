using MailSender.Models;
using MailSender.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using WpfMailSender.Data;
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



        private void ButtonSendNow_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ComboBoxSenders.SelectedItem is Sender sender_)) return;
            if (!(DataGridRecirients.SelectedItem is Recipient recipient)) return;
            if (!(ComboBoxServers.SelectedItem is Server server)) return;
            if (!(ListBoxMessages.SelectedItem is Message message)) return;

            if (string.IsNullOrEmpty(TextBoxMailMessage.Text))
            {
                MessageBox.Show("Письмо без текста нельзя отправить, пожалуйста заполните тело письма.", "Отмена отправки письма", MessageBoxButton.OK, MessageBoxImage.Stop);
                TabItemLetter.IsSelected = true;
                return;
            }
            
            var mailSender = new SmtpSender(server.Address, server.Port, server.UseSSL, server.Login, new NetworkCredential("",server.Password).Password);

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
            
            int newid = 1;
            if (TestData.Servers.Count != 0)
                newid = TestData.Servers.Max(s => s.Id) + 1;
            
            var server = new Server
            {
                Id = newid,
                Name = name,
                Address = address,
                Port = port,
                UseSSL = ssl,
                Description = description,
                Login = login,
                Password = new NetworkCredential("",password).SecurePassword,
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
            var description = server.Description;
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
            server.Description = description;
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

        private void ButtonToPlan_OnClick(object sender, RoutedEventArgs e)
        {
            TabItemPlan.IsSelected = true;
        }
    }
}
