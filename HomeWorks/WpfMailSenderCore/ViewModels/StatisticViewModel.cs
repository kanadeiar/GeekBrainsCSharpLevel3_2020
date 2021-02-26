using WpfMailSenderCore.ViewModels.Base;

namespace WpfMailSenderCore.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        /// <summary> Число отправленных сообщений </summary>
        public int SendMessagesCount
        {
            get => _sendMessagesCount;
            private set => Set(ref _sendMessagesCount, value);
        }
        private int _sendMessagesCount;

        /// <summary> Письмо отправлено </summary>
        public void MessageSended() => SendMessagesCount++;
    }
}
