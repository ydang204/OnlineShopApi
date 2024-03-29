﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Common.Extensions;
using OnlineShop.Common.Middlewares;
using OnlineShop.Common.SettingOptions;
using OnlineShop.NotificationAPI.Models;
using OnlineShop.NotificationAPI.ServiceInterfaces;
using OnlineShop.NotificationAPI.Services;

namespace OnlineShop.NotificationAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(Configuration)
                    .AddSwagger(Configuration)
                    .AddCustomOptions(Configuration)
                    .AddCustomJwtToken(Configuration)
                    .AddCustomAutoMapper()
                    .AddCustomDbContext<NotificationContext>(Configuration);

            services.AddHttpContextAccessor();

            // Add services to DI
            // TODO: Use Autofac
            services.AddTransient<IMailService, MailService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.Configure<SmtpMailOptions>(Configuration.GetSection("SmtpMail"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
                c.DefaultModelsExpandDepth(-1);
            });
            app.UseAuthentication();
            app.UseCustomExceptionHandler();

            app.UseMvc();

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<NotificationContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}