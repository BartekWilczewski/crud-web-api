using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Microsoft.Extensions.Options;
using WebApiSample.Config;
using WebApiSample.Models;

namespace WebApiSample.Helpers
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;
        private readonly MailSettings _mailSettings;

        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger, IOptions<MailSettings> options)
        {
            _logger =  logger;
            _next = next;
            _mailSettings = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong: {e}");
                await HandleExceptionAsync(context, e);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "internal server error from custom middleware"
            }.ToString());
        }
    }
}
