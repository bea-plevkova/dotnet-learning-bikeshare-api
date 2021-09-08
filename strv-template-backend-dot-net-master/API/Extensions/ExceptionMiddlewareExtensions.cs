using System.Net;
using API;
using GlobalErrorHandling.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GlobalErrorHandling.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            _ = app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    var loggerFactory =
                        new LoggerFactory()
                            .AddConsole(); //NOTE: Obsolete? https://docs.microsoft.com/en-us/ef/core/miscellaneous/logging#filtering-what-is-logged
#pragma warning restore CS0618 // Type or member is obsolete
                    ILogger logger = loggerFactory.CreateLogger<Program>();
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"{context.Request.Method} {context.Request.Path} {context.Request}");
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}