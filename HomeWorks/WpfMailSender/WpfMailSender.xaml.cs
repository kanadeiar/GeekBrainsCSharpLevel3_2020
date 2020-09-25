using System;
using System.Windows;

namespace WpfMailSender
{
    /// <summary>
    /// Логика взаимодействия для WpfMailSender.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    {
        public WpfMailSender()
        {
            InitializeComponent();
        }
        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            WpfTestMailSender.Server = TextBoxServer.Text;
            WpfTestMailSender.Port = Int32.Parse(TextBoxPort.Text);
            string from = TextBoxFrom.Text;
            string to = TextBoxTo.Text;
            string subject = TextBoxSubject.Text;
            string body = TextBoxBody.Text;
            var emailService = new EmailSendServiceClass(TextBoxLogin.Text, PasswordBoxPassword.SecurePassword);
            emailService.SendMail(from, to, subject, body);
        }
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxBody.Text = String.Empty;
        }
        private void MenuItemExit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void MenuItemHelp_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Приложение \"Рассыльщик\" Домашнее задание лекции № 1.");
        }

    }
}
