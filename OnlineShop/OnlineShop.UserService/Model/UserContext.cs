﻿using Microsoft.EntityFrameworkCore;

namespace OnlineShop.UserService.Model
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}