using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MailSender.Models;

namespace WpfMailSenderCore.Windows
{
    /// <summary>
    /// Логика взаимодействия для ServerEditWindow.xaml
    /// </summary>
    public partial class ServerEditWindow : Window
    {
        public Server Server { get; set; }
        public ServerEditWindow()
        {
            InitializeComponent();
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }
        public static bool ShowDialog(string title, ref string server, ref string address, ref int port, ref bool useSSL,
            ref string description, ref string login, ref string password)
        {
            var window = new ServerEditWindow
            {
                Title = title,
                Server = new Server
                {
                    Name = server,
                    Address = address,
                    Port = port,
                    UseSSL = useSSL,
                    Desctiption = description,
                    Login = login,
                    Password = password,
                },
                //TextBoxServerName = { Text = server },
                //TextBoxServerAddress = { Text = address },
                //TextBoxServerPort = { Text = port.ToString() },
                //CheckBoxServerSSL = { IsChecked = useSSL },
                //TextBoxDescription = { Text = description },
                //TextBoxLogin = { Text = login },
                //PasswordBoxPassword = { Password = password },
                Owner = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(win => win.IsActive),
            };
            window.DockPanelServerEdit.DataContext = window.Server;
            if (window.ShowDialog() != true)
                return false;
            server = window.Server.Name;
            address = window.Server.Address;
            port = window.Server.Port;
            useSSL = window.Server.UseSSL;
            description = window.Server.Desctiption;
            login = window.Server.Login;
            password = window.Server.Password;
            //server = window.TextBoxServerName.Text;
            //address = window.TextBoxServerAddress.Text;
            //port = int.Parse(window.TextBoxServerPort.Text);
            //useSSL = window.CheckBoxServerSSL.IsChecked == true;
            //description = window.TextBoxDescription.Text;
            //login = window.TextBoxLogin.Text;
            //password = window.PasswordBoxPassword.Password;
            return true;
        }
        public static bool Create(out string server, out string address, out int port, out bool useSSL, out string description,
            out string login, out string password)
        {
            server = null;
            address = null;
            port = 25;
            useSSL = false;
            description = null;
            login = null;
            password = null;
            return ShowDialog("Создать сервер", ref server, ref address, ref port, ref useSSL, ref description, 
                ref login, ref password);
        }
    }
}
