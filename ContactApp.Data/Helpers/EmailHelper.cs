using System.Net;
using System.Net.Mail;

namespace ContactApp.Data.Helpers
{
    public static class EmailHelper
    {
        public static bool SendEmail(string userEmail, string confirmationLink)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("contactapptest00@gmail.com"),
                Subject = "Confirm your email",
                IsBodyHtml = true,
                Body = confirmationLink
            };
            mailMessage.To.Add(new MailAddress(userEmail));

            var client = new SmtpClient
            {
                Credentials = new NetworkCredential("contactapptest00@gmail.com", "slpbomwytykvxzbj"),
                //UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true
            };

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }

        public static bool SendEmailPasswordReset(string userEmail, string link)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("noreply@contactapp.com"),
                Subject = "Password Reset",
                IsBodyHtml = true,
                Body = link
            };

            mailMessage.To.Add(new MailAddress(userEmail));

            var client = new SmtpClient
            {
                Credentials = new NetworkCredential("noreply@contactapp.com", "yourpassword"),
                Host = "smtpout.secureserver.net",
                Port = 80
            };

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }

            return false;
        }
    }
}
