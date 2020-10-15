namespace MailSender.Models.Base
{
    public abstract class BasePerson : PersonModel
    {
        private string _name;
        public new string Name 
        { 
            get => _name; 
            set => Set(ref _name, value); 
        }
        private string _address;
        public new string Address 
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
