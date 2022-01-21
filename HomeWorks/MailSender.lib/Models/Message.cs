using System.ComponentModel;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Message : Model, IDataErrorInfo
    {
        private string _title;
        public string Title 
        { 
            get => _title; 
            set => Set(ref _title, value); 
        }
        private string _body;
        public string Body 
        { 
            get => _body; 
            set => Set(ref _body, value); 
        }
        string IDataErrorInfo.Error => null;
        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case nameof(Title):
                        var title = Title;
                        if (title is null) return "Нельзя отправлять письма без заголовков";
                        if (title.Length < 2) return "Слишком короткий заголовок";
                        if (title.Length > 30) return "Слишком длинный заголовок";
                        return null;
                    case nameof(Body):
                        var body = Body;
                        if (body is null) return "Нельзя отправлять письма без текста";
                        if (body.Length < 2) return "Слишком короткое письмо";
                        return null;
                    default:
                        return null;
                }
            }
        }
    }
}
