namespace MailSender.Interfaces
{
    public interface ISchedulerMailService
    {
        ISchedulerMailSender GetScheduler(IMailSender mailSender);
    }
}
