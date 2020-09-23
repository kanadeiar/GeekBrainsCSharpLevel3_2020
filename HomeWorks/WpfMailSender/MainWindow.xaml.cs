using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
