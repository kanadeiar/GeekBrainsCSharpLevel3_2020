using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using WpfMailSender.Infrastructure.Commands;
using WpfMailSender.ViewModels.Base;

namespace WpfMailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            _timer = new Timer(100);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
        private string _title = "Рассыльщик электронной почты";
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

        #region Команды
        private ICommand _showDialogCommand;
        public ICommand ShowDialogCommand => _showDialogCommand ?? (_showDialogCommand = new LambdaCommand(OnShowDialogCommandExecuted));
        private void OnShowDialogCommandExecuted(object p)
        {
            var message = p as string ?? "Привет, Мир!";
            MessageBox.Show(message, "Сообщение приложения", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
    }
}
