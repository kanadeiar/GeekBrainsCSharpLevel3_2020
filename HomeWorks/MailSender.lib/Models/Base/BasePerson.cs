namespace MailSender.Models.Base
{
    public abstract class BasePerson : PersonModel
    {
        private string _name;
        public override string Name 
        { 
            get => _name; 
            set => Set(ref _name, value); 
        }
        private string _address;
        public override string Address 
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
