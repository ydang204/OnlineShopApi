using Microsoft.Extensions.Options;
﻿using Microsoft.Extensions.Logging;
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
        private readonly ILogger<MailService> _logger;

        public MailService(IOptions<SmtpMailOptions> emailSettings, ILogger<MailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
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

                _logger.LogError(ex, $"Can not send email to {email}");
            }
        }
    }
}