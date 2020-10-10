using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MailSender.Interfaces;
using MailSender.Models;
using WpfMailSenderCore.Infrastructure.Commands;
using WpfMailSenderCore.ViewModels.Base;
using WpfMailSenderCore.Windows;

namespace WpfMailSenderCore.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IMailService _MailService;
        private readonly IServerStorage _serverStorage;
        private readonly ISenderStorage _senderStorage;
        private readonly IRecipientStorage _recipientStorage;
        private readonly IMessageStorage _messageStorage;
        public MainWindowViewModel(IMailService mailService,
            IServerStorage serverStorage, ISenderStorage senderStorage, IRecipientStorage recipientStorage, IMessageStorage messageStorage)
        {
            _MailService = mailService;
            _serverStorage = serverStorage;
            _senderStorage = senderStorage;
            _recipientStorage = recipientStorage;
            _messageStorage = messageStorage;
            _timer = new Timer(100);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
        #region Свойства
        private string _title = "Рассыльщик электронной почты Core";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        private readonly Timer _timer;
        public DateTime CurrentTime => DateTime.Now;
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }
        private ObservableCollection<Server> _servers;
        public ObservableCollection<Server> Servers
        {
            get => _servers;
            set => Set(ref _servers, value);
        }
        private ObservableCollection<Sender> _senders;
        public ObservableCollection<Sender> Senders
        {
            get => _senders;
            set => Set(ref _senders, value);
        }
        private ObservableCollection<Recipient> _recipients;
        public ObservableCollection<Recipient> Recipients
        {
            get => _recipients;
            set => Set(ref _recipients, value);
        }
        private ObservableCollection<Message> _messages;
        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set => Set(ref _messages, value);
        }
        private Server _selectedServer;
        public Server SelectedServer
        {
            get => _selectedServer;
            set => Set(ref _selectedServer, value);
        }
        private Sender _selectedSender;
        public Sender SelectedSender
        {
            get => _selectedSender;
            set => Set(ref _selectedSender, value);
        }
        private Recipient _selectedRecipient;
        public Recipient SelectedRecipient
        {
            get => _selectedRecipient;
            set => Set(ref _selectedRecipient, value);
        }
        private Message _selectedMessage;
        public Message SelectedMessage
        {
            get => _selectedMessage;
            set => Set(ref _selectedMessage, value);
        }
        private string _recipientsFilter;
        public string RecipientsFilter
        {
            get => _recipientsFilter;
            set
            {
                Set(ref _recipientsFilter, value);
                OnPropertyChanged(nameof(FilteredRecipients));
            }
        }
        public ICollection<Recipient> FilteredRecipients
        {
            get
            {
                if (string.IsNullOrEmpty(RecipientsFilter))
                    return Recipients;
                return Recipients.Where(r => r.Name.ToLower().Contains(RecipientsFilter.ToLower())).ToList();
            }
        }
        #endregion
        #region Команды
        /// <summary> Команда показа сообщения </summary>
        public ICommand ShowDialogCommand => _showDialogCommand ??= new LambdaCommand(OnShowDialogCommandExecuted);
        private ICommand _showDialogCommand;
        private void OnShowDialogCommandExecuted(object p)
        {
            var message = p as string ?? "Привет, Мир!";
            MessageBox.Show(message, "Сообщение приложения", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary> Команда показа вкладки </summary>
        public ICommand ShowTabItemCommand => _showTabItemCommand ??= new LambdaCommand(OnShowTabItemCommand, CanShowTabItemCommand);
        private ICommand _showTabItemCommand;
        private bool CanShowTabItemCommand(object p) => p is TabItem;
        private void OnShowTabItemCommand(object p)
        {
            ((TabItem)p).IsSelected = true;
        }
        /// <summary> Команда загрузки данных </summary>
        public ICommand LoadDataCommand => _loadDataCommand ??= new LambdaCommand(OnLoadDataCommandExecuted);
        private ICommand _loadDataCommand;
        private void OnLoadDataCommandExecuted(object p)
        {
            _serverStorage.Load();
            _senderStorage.Load();
            _recipientStorage.Load();
            _messageStorage.Load();
            Servers = new ObservableCollection<Server>(_serverStorage.Items);
            Senders = new ObservableCollection<Sender>(_senderStorage.Items);
            Recipients = new ObservableCollection<Recipient>(_recipientStorage.Items);
            Messages = new ObservableCollection<Message>(_messageStorage.Items);
            OnPropertyChanged(nameof(FilteredRecipients));
        }
        /// <summary> Команда сохранения данных </summary>
        public ICommand SaveDataCommand => _saveDataCommand ??= new LambdaCommand(OnSaveDataCommandExecuted);
        private ICommand _saveDataCommand;
        private void OnSaveDataCommandExecuted(object p)
        {
            _serverStorage.SaveChanges();
            _senderStorage.SaveChanges();
            _recipientStorage.SaveChanges();
            _messageStorage.SaveChanges();
        }
        /// <summary> Команда создания сервера </summary>
        public ICommand CreateServerCommand => _createServerCommand ??= new LambdaCommand(OnCreateServerCommandExecuted);
        private ICommand _createServerCommand;
        private void OnCreateServerCommandExecuted(object p)
        {
            if (!ServerEditWindow.Create(
                out var server,
                out var address,
                out var port,
                out var useSSL,
                out var description,
                out var login,
                out var password))
                return;
            int newid = 1;
            if (Servers.Count != 0)
                newid = Servers.Max(s => s.Id) + 1;
            var newServer = new Server
            {
                Id = newid,
                Name = server,
                Address = address,
                Port = port,
                UseSSL = useSSL,
                Desctiption = description,
                Login = login,
                Password = password,
            };
            _serverStorage.Items.Add(newServer);
            Servers.Add(newServer);
        }
        /// <summary> Команда редактирования сервера </summary>
        public ICommand EditServerCommand => _editServerCommand 
            ??= new LambdaCommand(OnEditServerCommandExecuted, CanEditServerCommandExecute);
        private ICommand _editServerCommand;
        private bool CanEditServerCommandExecute(object p) => p is Server;
        private void OnEditServerCommandExecuted(object p)
        {
            if (!(p is Server editServer)) 
                return;
            var server = editServer.Name;
            var address = editServer.Address;
            var port = editServer.Port;
            var useSSL = editServer.UseSSL;
            var description = editServer.Desctiption;
            var login = editServer.Login;
            var password = editServer.Password;
            if (!ServerEditWindow.ShowDialog("Редактирование сервера",
                ref server,
                ref address, 
                ref port,
                ref useSSL, 
                ref description,
                ref login, 
                ref password
                )) 
                return;
            editServer.Name = server;
            editServer.Address = address;
            editServer.Port = port;
            editServer.UseSSL = useSSL;
            editServer.Desctiption = description;
            editServer.Login = login;
            editServer.Password = password;
        }
        /// <summary> Команда удаления сервера </summary>
        public ICommand DeleteServerCommand => _deleteServerCommand
            ??= new LambdaCommand(OnDeleteServerCommandExecuted, CanDeleteServerCommandExecute);
        private ICommand _deleteServerCommand;
        private bool CanDeleteServerCommandExecute(object p) => p is Server;
        private void OnDeleteServerCommandExecuted(object p)
        {
            if (!(p is Server server)) 
                return;
            _serverStorage.Items.Remove(server);
            Servers.Remove(server);
        }
        /// <summary> Команда создания отправителя </summary>
        public ICommand CreateSenderCommand => _createSenderCommand ??= new LambdaCommand(OnCreateSenderCommandExecuted);
        private ICommand _createSenderCommand;
        private void OnCreateSenderCommandExecuted(object p)
        {
            if (!SenderEditWindow.Create(
                out var name,
                out var address,
                out var description))
                return;
            int newid = 1;
            if (Senders.Count != 0)
                newid = Senders.Max(s => s.Id) + 1;
            var newSender = new Sender
            {
                Id = newid,
                Name = name,
                Address = address,
                Description = description,
            };
            _senderStorage.Items.Add(newSender);
            Senders.Add(newSender);
        }
        /// <summary> Команда редактирования отправителя </summary>
        public ICommand EditSenderCommand => _editSenderCommand
            ??= new LambdaCommand(OnEditSenderCommandExecuted, CanEditSenderCommandExecute);
        private ICommand _editSenderCommand;
        private bool CanEditSenderCommandExecute(object p) => p is Sender;
        private void OnEditSenderCommandExecuted(object p)
        {
            if (!(p is Sender editSender))
                return;
            var name = editSender.Name;
            var address = editSender.Address;
            var description = editSender.Description;
            if (!SenderEditWindow.ShowDialog("Редактирование отправителя",
                ref name,
                ref address,
                ref description
                ))
                return;
            editSender.Name = name;
            editSender.Address = address;
            editSender.Description = description;
        }
        /// <summary> Команда удаления отправителя </summary>
        public ICommand DeleteSenderComand => _deleteSenderCommand
            ??= new LambdaCommand(OnDeleteSenderCommandExecuted, CanDeleteSenderCommandExecute);
        private ICommand _deleteSenderCommand;
        private bool CanDeleteSenderCommandExecute(object p) => p is Sender;
        private void OnDeleteSenderCommandExecuted(object p)
        {
            if (!(p is Sender sender))
                return;
            _senderStorage.Items.Remove(sender);
            Senders.Remove(sender);
        }
        /// <summary> Команда отправки собщения </summary>
        public ICommand SendMessageCommand => _sendMailMessageCommand
            ??= new LambdaCommand(OnSendMessageCommandExecuted, CanSendMailMessageCommand);
        private ICommand _sendMailMessageCommand;
        private bool CanSendMailMessageCommand(object p)
        {
            return SelectedServer != null && SelectedSender != null && SelectedRecipient != null && SelectedMessage != null;
        }
        private void OnSendMessageCommandExecuted(object p)
        {
            var server = SelectedServer;
            var client = _MailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);
            var sender = SelectedSender;
            var recipient = SelectedRecipient;
            var message = SelectedMessage;
            client.Send(sender.Address, recipient.Address, message.Title, message.Body);
        }

        #endregion
    }
}
