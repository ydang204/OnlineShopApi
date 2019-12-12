using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using OnlineShop.Common.Constants;
using OnlineShop.Common.SettingOptions;
using OnlineShop.Common.Utitlities;
using System;
using System.IO;
using System.Reflection;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    // always serialize enum value as string
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddCors(options =>
            {
                options.AddPolicy(SharedContants.CORS_POLICY,
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
                options.UseSqlServer(configuration.GetConnectionString(SharedContants.DEFAULT_CONNECTION_STRING));
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
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = configuration.GetValue<string>(SharedContants.SWAGGER_TITLE),
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Y Dang",
                        Email = "daty.danghuynh20497@gmail.com",
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },
                        new string[] {}
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
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
            services.AddHttpContextAccessor();

            // Add configuration model
            services.Configure<SiteMapUrlOptions>(configuration.GetSection("SiteMapUrl"));
            services.AddScoped<ApiBuilderHelper>();
            services.AddScoped<ApiRequestHelper>();

            return services;
        }

        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static IServiceCollection AddCustomJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfigs = configuration.GetSection("Authentication").GetSection("JWT");
            var jwtSecretKey = jwtConfigs["SecretKey"];
            var jwtAudience = jwtConfigs["Audience"];
            var jwtIssuer = jwtConfigs["Issuer"];

            var jwtExpiresInDays = int.Parse(jwtConfigs["ExpiresInDays"]);
            services.Configure<JwtTokenOptions>(options =>
            {
                // secretKey contains a secret passphrase only your server knows
                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecretKey));
                options.Audience = jwtAudience;
                options.Issuer = jwtIssuer;
                options.Expiration = TimeSpan.FromDays(jwtExpiresInDays);
                options.ExpiresInDays = jwtExpiresInDays;
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.Events = new JwtBearerEvents
                {
                    //OnTokenValidated = async (context) =>
                    //{
                    //    var accessToken = context.SecurityToken as JwtSecurityToken;

                    //    var getUserName = accessToken.Claims.FirstOrDefault(c => c.Type == AuthConstants.ACCOUNT_USERNAME_CLAIM_TYPE);

                    //    var username = getUserName?.Value.ToString();

                    //    var validateService = context.HttpContext.RequestServices.GetRequiredService<IValidateTokenService>();

                    //    var account = await validateService.GetAccountByUserNameAsync(username);

                    //    if (account == null)
                    //    {
                    //        throw new CustomException("Unauthorized");
                    //    }
                    //}
                };

                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecretKey)),

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = jwtIssuer,

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = jwtAudience,

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // If you want to allow a certain amount of clock drift, set that here:
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

            return services;
        }
    }
}