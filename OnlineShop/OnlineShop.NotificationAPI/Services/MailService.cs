using Microsoft.Extensions.Options;
using OnlineShop.Common.SettingOptions;
using OnlineShop.NotificationAPI.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OnlineShop.NotificationAPI.Services
{
    public class MailService : IMailService
    {
        public MailService(IOptions<SmtpMailOptions> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public SmtpMailOptions _emailSettings { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {

            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string email, string subject, string message)
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
                //mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = "Personal Management System - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                //You can set the priority of an e-mail
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("daty.danghuynh20497@gmail.com", "rtarrbpciebtvkul");

                    await smtp.SendMailAsync(mail);
                }


            }
            catch (Exception ex)
            {
                //do something here
                Console.WriteLine("loi nguyen con");
                Console.ReadLine(); 
            }
        }
    }
}
