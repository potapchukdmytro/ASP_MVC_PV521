using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace _01_intro.Services
{
    public class EmailService : IEmailSender
    {
        private string _from;
        private SmtpClient _smtpClient;

        public EmailService()
        {
            _from = "dmytro.potapchuk22@gmail.com";
            string password = "wrxg pefb xhgf qatu";
            string host = "smtp.gmail.com";
            int port = 587;

            var credentials = new NetworkCredential(_from, password);
            _smtpClient = new SmtpClient(host, port);
            _smtpClient.Credentials = credentials;
            _smtpClient.EnableSsl = true;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_from);
            message.To.Add(email);
            message.Subject = subject;
            message.Body = htmlMessage;
            message.IsBodyHtml = true;

            await _smtpClient.SendMailAsync(message);
        }
    }
}
