using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Common.Constants;
using OnlineShop.Common.SettingOptions;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
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
            services.AddSwaggerGen(c =>
            {
                string title = configuration.GetSection(SharedContant.SWAGGER_TITLE).Value;
                c.SwaggerDoc("v1", new Info { Title = title, Version = "v1" });
                c.DescribeAllEnumsAsStrings(); // display enum on swagger as string
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "Header",
                    Description = "Enter JWT preceeded by the word Bearer and a space, like 'Bearer XYZ...'",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", Enumerable.Empty<string>() }
                });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
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