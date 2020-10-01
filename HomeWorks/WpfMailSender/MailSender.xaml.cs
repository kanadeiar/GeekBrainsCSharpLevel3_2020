using System;
using System.Diagnostics;
using System.Windows;
using WpfMailSender.Models;
using WpfMailSender.Services;

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
    }
}
