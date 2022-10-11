using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace LeaveManagement.Web.Services
{
    public class EmailSenderMe : IEmailSender
    {
        private string smtpServer;
        private int smtPort;
        private string fromEmailAddress;

        public EmailSenderMe(string smtpServer, int smtPort, string fromEmailAddress)
        {
            this.smtpServer = smtpServer;
            this.smtPort = smtPort;
            this.fromEmailAddress = fromEmailAddress;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //building an email messgage:

            var message = new MailMessage
            {
                From = new MailAddress(fromEmailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));//adding the recepuent address

            //connecting to the server (because we define in program.cs "add.transient" this will happen each time and be closed after
            var client = new SmtpClient(smtpServer, smtPort);            
            client.Send(message);

            return Task.CompletedTask;
            
        }
    }
}
