using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfMailSenderCore.Windows
{
    /// <summary>
    /// Логика взаимодействия для SenderEditWindow.xaml
    /// </summary>
    public partial class SenderEditWindow : Window
    {
        public SenderEditWindow()
        {
            InitializeComponent();
        }
        private void ButtonBase_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }
        public static bool ShowDialog(string title, ref string name, ref string address, ref string description)
        {
            var window = new SenderEditWindow
            {
                Title = title,
                TextBoxName = { Text = name },
                TextBoxAddress = { Text = address },
                TextBoxDescription = { Text = description },
                Owner = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(win => win.IsActive),
            };
            if (window.ShowDialog() != true)
                return false;
            name = window.TextBoxName.Text;
            address = window.TextBoxAddress.Text;
            description = window.TextBoxDescription.Text;
            return true;
        }
        public static bool Create(out string name, out string address, out string description)
        {
            name = null;
            address = null;
            description = null;
            return ShowDialog("Создать отправителя", ref name, ref address, ref description);
        }
    }
}
