using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.MessageSender
{
    public class MessageSender : IMessageSender
    {
        public Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false)
        {
            using (var client = new System.Net.Mail.SmtpClient())
            {
                var credentials = new System.Net.NetworkCredential()
                {
                    UserName = "**", // without @gmail.com
                    Password = "***"
                };
                client.Credentials = credentials;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                using var emailMessage = new System.Net.Mail.MailMessage()
                {
                    To = { new System.Net.Mail.MailAddress(toEmail) },
                    From = new System.Net.Mail.MailAddress("***@gmail.com"), // with @gmail.com
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = isMessageHtml
                };
                client.SendMailAsync(emailMessage);
            }
            return Task.CompletedTask;
        }
    }
}

