using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfMailSender
{
    /// <summary>
    /// Логика взаимодействия для SendErrorWindow.xaml
    /// </summary>
    public partial class SendErrorWindow : Window
    {
        private string _error;
        public SendErrorWindow(string error)
        {
            InitializeComponent();
            _error = error;
            TextBlockError.Text = "Причина ошибки: " + error;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
