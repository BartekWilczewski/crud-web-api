using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using WebApiSample.Models;

namespace WebApiSample.Helpers
{
    public static class Extensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appErrror =>
            {
                appErrror.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "internal server error"
                        }.ToString());
                });
            });
        }

        public static void ConfigureExceptionMiddleware(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseMiddleware<ExceptionMiddleware>();
        }

        public static void ConfigureNlogLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
