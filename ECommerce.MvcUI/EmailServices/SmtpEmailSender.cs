
using System.Net;
using System.Net.Mail;

namespace ECommerce.MvcUI.EmailServices
{
    public class SmtpEmailSender : IEmailSender
    {

        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly bool _enableSSL;
        public SmtpEmailSender(string host, int port, string username, string password, bool enableSSL)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _enableSSL = enableSSL;
        }


        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = _enableSSL
            };
            using var mailMessage = new MailMessage(_username, email, subject, htmlMessage)
            {
                IsBodyHtml = true
            };
            await client.SendMailAsync(mailMessage);
        }
    }
}
