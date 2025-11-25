namespace ECommerce.MvcUI.EmailServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email,string subject,string htmlMessage);
    }
}
