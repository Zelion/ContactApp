using ContactApp.Data.Services.Interfaces;
using ContactApp.Domain.Settings;
using System.Net;
using System.Net.Mail;

namespace ContactApp.Data.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task<bool> SendEmailAsync(string userEmail, string confirmationLink, string subject)
        {
            var mailMessage = BuildMail(userEmail, confirmationLink, subject);
            var client = BuildClient();

            try
            {
                await Task.Run(() => client.SendAsync(mailMessage, null));
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }

        #region Private Methods

        private MailMessage BuildMail(string userEmail, string confirmationLink, string subject)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailConfig.From),
                Subject = subject,
                IsBodyHtml = true,
                Body = confirmationLink
            };
            mailMessage.To.Add(new MailAddress(userEmail));

            return mailMessage;
        }

        private SmtpClient BuildClient()
        {
            return new SmtpClient
            {
                Credentials = new NetworkCredential(_emailConfig.From, _emailConfig.Password),
                Host = _emailConfig.SmtpServer,
                Port = _emailConfig.Port,
                EnableSsl = true
            };
        }

        #endregion
    }
}
