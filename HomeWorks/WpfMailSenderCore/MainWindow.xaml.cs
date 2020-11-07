using System;
using System.Windows;
using MailSender.Interfaces;
using MailSender.Services;

namespace WpfMailSenderCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary> Тест шифрования </summary>
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            IEncryptService encryptor = new Rfc2898Encryptor();
            var str = "Привет всем и привет мир!";
            const string password = "password";
            var encryptString = encryptor.Encrypt(str, password);
            var decryptedString = encryptor.Decrypt(encryptString, password);
            MessageBox.Show($"Зашифрованное слово: {encryptString} Расшифрованное: {decryptedString}");
        }

        private void ButtonSendPlan_OnClick(object sender, RoutedEventArgs e)
        {
            var scheduler = new SchedulerMailService();
            var debug = new DebugMailService();
            var two = debug.GetSender("smtp.mail.ru", 12, true, "gg", "tt");
            var one = scheduler.GetScheduler(two);
            one.AddTaskSend(DateTime.Now.AddSeconds(10), "from@mail.ru", "to@mail.ru", "123", "text");
        }
    }
}
