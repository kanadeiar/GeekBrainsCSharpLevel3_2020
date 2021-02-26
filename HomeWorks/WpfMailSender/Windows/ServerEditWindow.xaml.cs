using System.Linq;
using System.Net;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfMailSender.Windows
{
    /// <summary>
    /// Логика взаимодействия для ServerEditWindow.xaml
    /// </summary>
    public partial class ServerEditWindow : Window
    {
        public ServerEditWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button) e.OriginalSource).IsCancel;
            Close();
        }

        private void TextBoxServerPort_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox) || textBox.Text == "") return;
            e.Handled = !int.TryParse(textBox.Text, out _);
        }

        public static bool ShowDialog(string Title, ref string Name, ref string Address, ref int Port, ref bool UseSSL,
            ref string Description, ref string Login, ref SecureString Password)
        {
            var window = new ServerEditWindow
            {
                Title = Title,
                TextBoxServerName= {Text = Name},
                TextBoxServerAddress = {Text = Address},
                TextBoxServerPort = {Text = Port.ToString()},
                CheckBoxServerSSL = {IsChecked = UseSSL},
                TextBoxLogin = {Text = Login},
                PasswordBoxPassword = {Password = new NetworkCredential("",Password).Password},
                TextBoxDescription = {Text = Description},
                Owner = Application
                    .Current
                    .Windows
                    .Cast<Window>()
                    .FirstOrDefault(win => win.IsActive)
            };
            if (window.ShowDialog() != true) return false;
            Name = window.TextBoxServerName.Text;
            Address = window.TextBoxServerAddress.Text;
            Port = int.Parse(window.TextBoxServerPort.Text);
            UseSSL = window.CheckBoxServerSSL.IsChecked == true;
            Login = window.TextBoxLogin.Text;
            Password = window.PasswordBoxPassword.SecurePassword;
            Description = window.TextBoxDescription.Text;
            return true;
        }

        public static bool Create(out string Name, out string Address, out int Port, out bool UseSSL,
            out string Description, out string Login, out SecureString Password)
        {
            Name = null;
            Address = null;
            Port = 25;
            UseSSL = false;
            Description = null;
            Login = null;
            Password = null;

            return ShowDialog("Создать сервер", ref Name, ref Address, ref Port, ref UseSSL, ref Description, ref Login,
                ref Password);
        }
    }
}
