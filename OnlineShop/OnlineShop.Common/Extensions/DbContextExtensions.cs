using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace OnlineShop.Common.Extensions
{
    public class DbContextExtensions
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ChangeTracker _changeTracker;

        public DbContextExtensions(IHttpContextAccessor httpContext, ChangeTracker changeTracker)
        {
            _httpContext = httpContext;
            _changeTracker = changeTracker;
        }

        public void UpdateTimeTracker()
        {
            var currentTime = DateTime.Now;
            int? accountId = null;
            if (_httpContext != null && _httpContext.HttpContext != null && _httpContext.HttpContext.User != null)
            {
                accountId = _httpContext.HttpContext.User.GetAccountId();
            }

            //Find all Entities that are Added/Modified that inherit from my EntityBase
            var entries = _changeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Metadata.FindProperty("CreatedAt") != null)
                    {
                        if (entry.Property("CreatedAt").CurrentValue == null ||
                            (DateTime)entry.Property("CreatedAt").CurrentValue == default
                            )
                            entry.Property("CreatedAt").CurrentValue = currentTime;
                    }

                    if (entry.Metadata.FindProperty("CreatedBy") != null)
                    {
                        if (entry.Property("CreatedBy").CurrentValue == null ||
                            (int)entry.Property("CreatedBy").CurrentValue == default)
                            entry.Property("CreatedBy").CurrentValue = accountId;
                    }
                }

                if (entry.Metadata.FindProperty("ModifiedAt") != null)
                {
                    if (entry.Property("ModifiedAt").CurrentValue == null ||
                        (DateTime)entry.Property("ModifiedAt").CurrentValue == default
                        )
                        entry.Property("ModifiedAt").CurrentValue = currentTime;
                }

                if (entry.Metadata.FindProperty("ModifiedBy") != null)
                {
                    if (entry.Property("ModifiedBy").CurrentValue == null ||
                    (int)entry.Property("ModifiedBy").CurrentValue == default)
                        entry.Property("ModifiedBy").CurrentValue = accountId;
                }
            }
        }
    }
}