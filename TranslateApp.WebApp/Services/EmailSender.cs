
using System.Net;
using System.Net.Mail;


namespace TranslateApp.WebApp.Services
{
    public class EmailSender : IEmailSender
    {

        public void SendEmail(string email, string subject, string bodyMessage)
        {
            var fromAddress = new MailAddress("***", "***");
            var toAddress = new MailAddress(email);
            const string fromPassword = "***";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                IsBodyHtml = true,
                Subject = subject,
                Body = bodyMessage
            })
            {
                smtp.Send(message);
            }
        }
    }
}
