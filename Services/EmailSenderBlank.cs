using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Mtd.Cpq.Manager.DataConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager.Services
{
    public interface IEmailSenderBlank
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task<bool> SendEmailBlankAsync(BlankEmail blankEmail);
    }

    public class EmailSenderBlank : IEmailSenderBlank
    {
        private readonly EmailSettings emailSettings;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public EmailSenderBlank(IOptions<EmailSettings> options, IWebHostEnvironment hostingEnvironment)
        {
            emailSettings = options.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {

            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }
        
        public async Task<bool> SendEmailBlankAsync(BlankEmail blankEmail)
        {
            try
            {
                string message = string.Empty;
                foreach (string p in blankEmail.Content)
                {
                    message += $"<p>{p}</p>";
                }

                string webRootPath = _hostingEnvironment.WebRootPath;
                string contentRootPath = _hostingEnvironment.ContentRootPath;
                var file = Path.Combine(contentRootPath, "wwwroot", "lib", "mtd", "emailform", "blank.html");
                var htmlArray = File.ReadAllText(file);
                string htmlText = htmlArray.ToString();

                htmlText = htmlText.Replace("{title}", emailSettings.Title);
                htmlText = htmlText.Replace("{header}", blankEmail.Header);
                htmlText = htmlText.Replace("{content}", message);
                htmlText = htmlText.Replace("{copyright}", emailSettings.CopyRight);
                htmlText = htmlText.Replace("{footer}", emailSettings.Footer);

                await SendEmailAsync(blankEmail.Email, blankEmail.Subject, htmlText);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private async Task Execute(string email, string subject, string message)
        {
            try
            {
                MailAddress toAddress = new MailAddress(email);
                MailAddress fromAddress = new MailAddress(emailSettings.FromAddress, emailSettings.FromName);
                // создаем письмо: message.Destination - адрес получателя
                MailMessage mail = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };

                using SmtpClient smtp = new SmtpClient(emailSettings.SmtpServer, emailSettings.Port)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailSettings.FromAddress, emailSettings.Password),
                    EnableSsl = true
                };
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error EMail sender service \n {ex.Message}");
            }
        }
    }

    public class BlankEmail
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Header { get; set; }
        public List<string> Content { get; set; }
    }

}
