using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMailSender
{
    public class EmailSendServiceClass
    {
        private readonly string _login;
        private readonly SecureString _password;
        public EmailSendServiceClass(string login, SecureString password)
        {
            _login = login;
            _password = password;
        }
        public void SendMail(string from, string to, string subject, string body)
        {
            using (MailMessage mm = new MailMessage(from, to))
            {
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = false;
                using (SmtpClient sc = new SmtpClient(WpfTestMailSender.Server, WpfTestMailSender.Port))
                {
                    sc.EnableSsl = true;
                    sc.Credentials = new NetworkCredential(_login, _password);
                    try
                    {
                        sc.Send(mm);
                        MessageBox.Show("Успешная отправка письма!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Облом!" + ex.Message);
                    }
                }
            }
        }
    }
}
