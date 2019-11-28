using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using OnlineShop.Common.Exceptions;
using System.Text;

namespace OnlineShop.Common.Middlewares
{
    public static class CutomExceptionHandlerMiddleware
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    // Allow valid when accessed from different servers
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                    // Get Error Code and Error Message by IExceptionHandlerFeature
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var ex = error.Error;

                        // Check whether the CustomException type
                        if (ex is CustomException)
                        {
                            context.Response.ContentType = "application/json";
                            var specEx = (CustomException)ex;
                            if (specEx.IsUnauthorized)
                            {
                                context.Response.StatusCode = 401;
                            }
                            else
                            {
                                context.Response.StatusCode = 400;
                            }
                            await context.Response.WriteAsync(specEx.ToString(), Encoding.UTF8);
                        }
                    }
                });
            });
        }
    }
}