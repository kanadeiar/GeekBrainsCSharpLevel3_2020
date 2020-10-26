using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailSender.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.lib.Tests1.Service
{
    [TestClass]
    public class SmtpSenderTests
    {
        private MockSmtpClient _mockClient;
        private SmtpSender _sender;
        [TestInitialize]
        public void Init()
        {
            _mockClient = new MockSmtpClient();
            _sender = new SmtpSender("mail.test.ru",25,true,"test","password", _mockClient);

        }
        [TestMethod]
        public void Send_NewClientCalled()
        {
            _sender.Send("test@test.ru", "test@test2.ru", "title", "body");

            Assert.IsTrue(_mockClient.NewSmtpClientCalled);
        }
        [TestMethod]
        public void Send_SendCalled()
        {
            _sender.Send("test@test.ru", "test@test2.ru", "title", "body");

            Assert.IsTrue(_mockClient.SendCalled);
        }
        [TestMethod]
        public void Send_SendAsyncCalled()
        {
            _sender.SendAsync("test@test.ru", "test@test2.ru", "title", "body");

            Assert.IsTrue(_mockClient.SendMailAsyncCalled);
        }
        [TestMethod]
        public void Send_MailMessageCreated()
        {
            MailMessage message = new MailMessage("test@test.ru", "test@test2.ru");
            string messageTitle = "title";
            string messageBody = "body";
            _sender.Send(message.From.ToString(), message.To.First().ToString(), messageTitle, messageBody);

            Assert.IsNotNull(_mockClient.MailMessage);
            Assert.AreEqual(message.From, _mockClient.MailMessage.From);
            Assert.AreEqual(message.To.First().ToString(), _mockClient.MailMessage.To.First().ToString());
            Assert.AreEqual(messageTitle, _mockClient.MailMessage.Subject);
            Assert.AreEqual(messageBody, _mockClient.MailMessage.Body);
        }


        public class MockSmtpClient : ISmtpSenderSmtpClient
        {
            public bool NewSmtpClientCalled { get; set; }
            public bool SendCalled { get; set; }
            public bool SendMailAsyncCalled { get; set; }
            public MailMessage MailMessage { get; set; }
            public void NewSmtpClient(string address, int port, bool useSsl, NetworkCredential credential)
            {
                NewSmtpClientCalled = true;
            }
            public void Send(MailMessage message)
            {
                SendCalled = true;
                MailMessage = message;
            }
            public async Task SendMailAsync(MailMessage message)
            {
                SendMailAsyncCalled = true;
                MailMessage = message;
            }
        }
    }
}
