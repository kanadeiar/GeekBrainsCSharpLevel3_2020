using System;
using System.Linq;
using System.Net;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        public static readonly DependencyProperty SecurePasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(SecureString), typeof(ServerEditWindow));
        public ServerEditWindow()
        {
            InitializeComponent();
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }

        public static bool ShowDialog(string title, ref string server, ref string address, ref int port,
            ref bool useSSL,
            ref string description, ref string login, ref SecureString password)
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
                    Description = description,
                    Login = login,
                    Password = password,
                },
                Owner = Application.Current.Windows.Cast<Window>()
                    .FirstOrDefault(win => win.IsActive),
            };
            window.DockPanelServerEdit.DataContext = window.Server;
            Binding passwordBinding = new Binding(SecurePasswordProperty.Name);
            passwordBinding.Source = window.Server;
            passwordBinding.ValidatesOnDataErrors = true;
            window.PasswordBoxPassword.SetBinding(SecurePasswordProperty, passwordBinding);
            window.PasswordBoxPassword.PasswordChanged += (sender, args) =>
            {
                window.Server.Password = window.PasswordBoxPassword.SecurePassword;
            };
            if (window.ShowDialog() != true)
                return false;
            server = window.Server.Name;
            address = window.Server.Address;
            port = window.Server.Port;
            useSSL = window.Server.UseSSL;
            description = window.Server.Description;
            login = window.Server.Login;
            password = window.Server.Password;
            return true;
        }

        public static bool Create(out string server, out string address, out int port, out bool useSSL, out string description,
            out string login, out SecureString password)
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
