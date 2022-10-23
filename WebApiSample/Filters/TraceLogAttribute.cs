using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiSample.Filters
{
    public class TraceLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Trace.WriteLine($"Action method {context.ActionDescriptor.DisplayName} started executing at {DateTime.Now.ToLongTimeString()}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Trace.WriteLine($"Action method {context.ActionDescriptor.DisplayName} executed at {DateTime.Now.ToLongTimeString()}");
        }
    }
}
