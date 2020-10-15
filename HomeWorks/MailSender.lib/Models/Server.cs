using MailSender.Models.Base;

namespace MailSender.Models
{
    public class Server : NamedModel
    {
        private string _name;
        public new string Name 
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
        private int _port;
        public int Port 
        { 
            get => _port;
            set => Set(ref _port, value); 
        }
        public bool UseSSL { get; set; }
        private string _description;
        public string Desctiption 
        { 
            get => _description; 
            set => Set(ref _description, value); 
        }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
