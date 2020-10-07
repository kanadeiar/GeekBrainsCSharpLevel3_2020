using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using MailSender.Models;
using WpfMailSenderCore.Data;
using WpfMailSenderCore.Infrastructure.Commands;
using WpfMailSenderCore.ViewModels.Base;

namespace WpfMailSenderCore.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private static string __DataFileName = "test.dat";
        public MainWindowViewModel()
        {
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
        /// <summary> Команда загрузки данных из файла </summary>
        public ICommand LoadDataCommand => _loadDataCommand ??= new LambdaCommand(OnLoadDataCommandExecuted);
        private ICommand _loadDataCommand;
        private void OnLoadDataCommandExecuted(object p)
        {
            var data = File.Exists(__DataFileName)
                ? TestData.LoadFromXML(__DataFileName)
                : new TestData();
            Servers = new ObservableCollection<Server>(data.Servers);
            Senders = new ObservableCollection<Sender>(data.Senders);
            Recipients = new ObservableCollection<Recipient>(data.Recipients);
            Messages = new ObservableCollection<Message>(data.Messages);
        }
        /// <summary> Команда сохранения данных в файле </summary>
        public ICommand SaveDataCommand => _saveDataCommand ??= new LambdaCommand(OnSaveDataCommandExecuted);
        private ICommand _saveDataCommand;
        private void OnSaveDataCommandExecuted(object p)
        {
            var data = new TestData
            {
                Servers = Servers,
                Senders = Senders,
                Recipients = Recipients,
                Messages = Messages,
            };
            data.SaveToXML(__DataFileName);
        }
        #endregion
    }
}
