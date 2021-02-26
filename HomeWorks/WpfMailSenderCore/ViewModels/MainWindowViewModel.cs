using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using MailSender.Interfaces;
using MailSender.Models;
using WpfMailSenderCore.ViewModels.Base;
using static MailSender.Services.SchedulerMailService;

namespace WpfMailSenderCore.ViewModels
{
    partial class MainWindowViewModel : ViewModel
    {
        private readonly IMailService _mailService;
        private readonly IServerStorage _serverStorage;
        private readonly ISenderStorage _senderStorage;
        private readonly IRecipientStorage _recipientStorage;
        private readonly IMessageStorage _messageStorage;
        private readonly ISchedulerMailService _schedulerMail;
        /// <summary> Статистика приложения </summary>
        public StatisticViewModel StatisticViewModel { get; } = new StatisticViewModel();
        public MainWindowViewModel(IMailService mailService,
            IServerStorage serverStorage, ISenderStorage senderStorage, IRecipientStorage recipientStorage, IMessageStorage messageStorage, ISchedulerMailService schedulerMail)
        {
            _mailService = mailService;
            _serverStorage = serverStorage;
            _senderStorage = senderStorage;
            _recipientStorage = recipientStorage;
            _messageStorage = messageStorage;
            _schedulerMail = schedulerMail;
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
        ///////////////////////////////////////////////////////////////////////////////////////////////
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
        private ObservableCollection<SchedulerMailSender> _schedulerMailSenders;
        public ObservableCollection<SchedulerMailSender> SchedulerMailSenders
        {
            get => _schedulerMailSenders;
            set => Set(ref _schedulerMailSenders, value);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
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
        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => Set(ref _selectedDate, value);
        }


        /////////////////////////////////////////////////////////////////////////////////////////
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
    }
}
