using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Common.Extensions;
using OnlineShop.Common.SettingOptions;
using OnlineShop.UserService.MappingProfiles;
using OnlineShop.UserService.Models;
using OnlineShop.UserService.ServiceInterfaces;
using OnlineShop.UserService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.UserService
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
            services.Configure<JwtTokenOptions>(options =>
            {
                var jwtConfigs = Configuration.GetSection("Authentication").GetSection("JWT");
                var jwtSecretToken = jwtConfigs["SecretKey"];
                var jwtAudience = jwtConfigs["Audience"];
                var jwtIssuer = jwtConfigs["Issuer"];
                var jwtExpiresInDays = int.Parse(jwtConfigs["ExpiresInDays"]);

                // secretKey contains a secret passphrase only your server knows
                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecretToken));
                options.Audience = jwtAudience;
                options.Issuer = jwtIssuer;
                options.Expiration = TimeSpan.FromDays(jwtExpiresInDays);
                options.ExpiresInDays = jwtExpiresInDays;
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddCustomMvc(Configuration)
                    .AddSwagger(Configuration)
                    .AddCustomOptions(Configuration)
                    .AddCustomAutoMapper(new List<Profile> { new AccountProfile() })
                    .AddCustomDbContext<UserContext>(Configuration);

            services.AddScoped<IAuthService, AuthService>();
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
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}