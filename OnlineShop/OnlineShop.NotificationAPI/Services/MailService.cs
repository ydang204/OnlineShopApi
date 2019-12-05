using Microsoft.Extensions.Options;
using OnlineShop.Common.SettingOptions;
using OnlineShop.NotificationAPI.ServiceInterfaces;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OnlineShop.NotificationAPI.Services
{
    public class MailService : IMailService
    {
        private readonly SmtpMailOptions _emailSettings;

        public MailService(IOptions<SmtpMailOptions> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email)
                                 ? _emailSettings.ToEmail
                                 : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Muhammad Hassan Tariq")
                };
                mail.To.Add(new MailAddress(toEmail));
                mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = "Personal Management System - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                //You can set the priority of an e-mail
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.SecondayDomain, _emailSettings.SecondaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                //do something here
            }
        }
    }
}