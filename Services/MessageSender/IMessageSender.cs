using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Services.MessageSender
{
    public interface IMessageSender
    {
        public Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false);

    }
}
