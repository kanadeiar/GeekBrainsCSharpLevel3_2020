using System;
using System.Collections.Generic;
using System.Threading;
using MailSender.Interfaces;
using MailSender.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.lib.Tests1.Services
{
    [TestClass]
    public class SchedulerMailServiceTests
    {
        private MailSenderMock _mock;
        private SchedulerMailService.SchedulerMailSender _scheduler;
        [TestInitialize]
        public void Init()
        {
            _mock = new MailSenderMock();
            _scheduler = (SchedulerMailService.SchedulerMailSender)new SchedulerMailService().GetScheduler(_mock);
        }
        [TestMethod]
        public void Scheduler_Give_NotNull()
        { 
            var actual = _scheduler;

            Assert.IsNotNull(actual);            
        }
        [TestMethod]
        public void Scheduler_Give_SchedulerMailSender()
        {
            var actual = _scheduler;

            if (!(actual is SchedulerMailService.SchedulerMailSender))
                Assert.Fail();
        }
        [TestMethod]
        public void AddTaskSendSend_Verify_Values()
        {
            DateTime testDateTime = new DateTime(2020, 1, 1);
            string testFrom = "from@mail.ru";
            string testTo = "to@mail.ru";
            string testTitle = "Title";
            string testMessage = "Message";

            _scheduler.AddTaskSend(testDateTime, testFrom, testTo, testTitle, testMessage);

            Assert.AreEqual(testDateTime, _scheduler.DateTimeSend);
            Assert.AreEqual(testFrom, _scheduler.MailMessageFromAddress);
            Assert.AreEqual(testTo, _scheduler.MailMessageToAddress);
            Assert.AreEqual(testTitle, _scheduler.MailMessageTitle);
            Assert.AreEqual(testMessage, _scheduler.MailMessageMessage);
        }
        [TestMethod]
        public void AddTaskSend_Is_Called_Send()
        {
            DateTime testDateTime = DateTime.Now;
            string testFrom = "from@mail.ru";
            string testTo = "to@mail.ru";
            string testTitle = "Title";
            string testMessage = "Message";

            _scheduler.AddTaskSend(testDateTime, testFrom, testTo, testTitle, testMessage);

            Thread.Sleep(2000);
            Assert.IsTrue(_mock.SendCalled);
        }
        [TestMethod]
        public void AddTaskSend_Is_Called_Send_In_5_Seconds()
        {
            DateTime testDateTime = DateTime.Now.AddSeconds(5);
            string testFrom = "from@mail.ru";
            string testTo = "to@mail.ru";
            string testTitle = "Title";
            string testMessage = "Message";

            _scheduler.AddTaskSend(testDateTime, testFrom, testTo, testTitle, testMessage);

            Thread.Sleep(7000);
            Assert.IsTrue(_mock.SendTimeCalled <= testDateTime.AddSeconds(1) && _mock.SendTimeCalled >= testDateTime.AddSeconds(-1));
        }
        [TestMethod]
        public void Stop_Is_Not_Called_Send()
        {
            DateTime testDateTime = DateTime.Now;
            string testFrom = "from@mail.ru";
            string testTo = "to@mail.ru";
            string testTitle = "Title";
            string testMessage = "Message";

            _scheduler.AddTaskSend(testDateTime, testFrom, testTo, testTitle, testMessage);
            _scheduler.Stop();

            Thread.Sleep(2000);
            Assert.IsFalse(_mock.SendCalled);
        }
        [TestMethod]
        public void EventSend_Is_Called()
        {
            DateTime testDateTime = DateTime.Now;
            string testFrom = "from@mail.ru";
            string testTo = "to@mail.ru";
            string testTitle = "Title";
            string testMessage = "Message";
            bool isCalled = false;

            _scheduler.EventSend += () => isCalled = true;
            _scheduler.AddTaskSend(testDateTime, testFrom, testTo, testTitle, testMessage);

            Thread.Sleep(2000);
            Assert.IsTrue(isCalled);
        }
        

        private class MailSenderMock : IMailSender
        {
            public bool SendCalled { get; set; }
            public DateTime SendTimeCalled { get; set; }
            public bool SendEnumerableCalled { get; set; }
            public void Send(string from, string to, string title, string message) 
            {
                SendCalled = true;
                SendTimeCalled = DateTime.Now;
            }
            public void Send(string from, IEnumerable<string> tos, string title, string message) 
            {
                SendEnumerableCalled = true;
            }
        }
    }
}
