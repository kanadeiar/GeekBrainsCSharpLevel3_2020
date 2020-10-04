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




    }
}
