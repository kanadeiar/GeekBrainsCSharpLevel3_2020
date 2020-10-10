namespace MailSender.Models.Base
{
    public abstract class BaseMailer : Model
    {
        public int Id { get; set; }
        private string _name;
        public string Name 
        { 
            get => _name; 
            set => Set(ref _name, value); 
        }
        private string _address;
        public string Address 
        { 
            get => _address; 
            set => Set(ref _address, value); 
        }
        private string _description;
        public string Description 
        { 
            get => _description; 
            set => Set(ref _description, value); 
        }
    }
}
