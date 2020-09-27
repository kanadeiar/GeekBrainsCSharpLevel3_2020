using System.Windows;

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
    }
}
