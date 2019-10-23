using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineShop.Common.Constants;
using OnlineShop.Common.SettingOptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Common.Extensions
{
    public static class CustomServiceExtensions
    {
        /// <summary>
        /// Configuration for custom MVC
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options =>
            {
                options.AddPolicy(SharedContant.CORS_POLICY,
                    builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });

            return services;
        }

        /// <summary>
        /// Configuration for custom Db context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomDbContext<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
        {
            services.AddDbContext<T>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(SharedContant.DEFAULT_CONNECTION_STRING));
            });
            return services;
        }

        /// <summary>
        /// Configuration for custom swagger ui
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)

        {
            services.AddSwaggerGen(config =>
            {
                string title = configuration.GetSection($"{SharedContant.SWAGGER}:Title").Value;
                config.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = "v1" });
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options => options.LowercaseUrls = true);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }

        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static IServiceCollection AddCustomJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtTokenOptions>(options =>
            {
                var jwtConfigs = configuration.GetSection("Authentication").GetSection("JWT");
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

            return services;
        }
    }
}