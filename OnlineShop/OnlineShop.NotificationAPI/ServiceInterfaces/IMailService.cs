﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.NotificationAPI.ServiceInterfaces
{
    public interface IMailService
    {
      Task SendEmailAsync(string email, string subject, string message);
    }
}
