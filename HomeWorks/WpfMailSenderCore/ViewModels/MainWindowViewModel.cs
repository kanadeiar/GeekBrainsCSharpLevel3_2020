using WpfMailSenderCore.ViewModels.Base;

namespace WpfMailSenderCore.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
        }
        private string _title = "Рассыльщик электронной почты Core";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
