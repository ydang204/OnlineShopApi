﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Common.Extensions;
using OnlineShop.Common.Middlewares;
using OnlineShop.Common.SettingOptions;
using OnlineShop.Common.Utitlities;
using OnlineShop.OrderAPI.Models;
using OnlineShop.OrderAPI.ServiceInterfaces;
using OnlineShop.OrderAPI.Services;

namespace OnlineShop.OrderAPI
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
                   .AddCustomDbContext<OrderContext>(Configuration);

            services.Configure<MoMoPaymentOptions>(Configuration.GetSection("MoMoPayment"));
            services.AddScoped<MoMoPaymentHelper>();
            services.AddScoped<IOrderService, OrderService>();
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

            app.UseCustomExceptionHandler();
            app.UseAuthentication();
            app.UseMvc();

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<OrderContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}