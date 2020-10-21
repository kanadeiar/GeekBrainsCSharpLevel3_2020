using System;
using System.Collections.Generic;
using System.Text;
using MailSender.Models.Base;

namespace MailSender.Models
{
    public class SchedulerTask : Model
    {
        public DateTime Time { get; set; }
        public Server Server { get; set; }
        public Sender Sender { get; set; }
        public ICollection<Recipient> Recipients { get; set; }
        public Message Message { get; set; }
    }
}
