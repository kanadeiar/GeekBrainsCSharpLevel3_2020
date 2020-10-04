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
        }
        private string _title = "Рассыльщик электронной почты";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
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
