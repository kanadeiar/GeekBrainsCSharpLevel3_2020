using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Message : Model
    {
        public int Id { get; set; }
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
    }
}
