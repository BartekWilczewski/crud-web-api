using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace WebApiSample.Filters
{
    public class ETagFilter : Attribute, IActionFilter
    {
        private readonly int[] _statusCodes;
        public ETagFilter(params int[] statusCodes)
        {
            _statusCodes = statusCodes;
            if(statusCodes.Length == 0) _statusCodes = new [] {(int)HttpStatusCode.OK};
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method == "GET")
            {
                if (_statusCodes.Contains(context.HttpContext.Response.StatusCode))
                {
                    var content = JsonConvert.SerializeObject(context.Result);
                    var etag = ETagGenerator.GetEtag(context.HttpContext.Request.Path.ToString(),
                        Encoding.UTF8.GetBytes(content));

                    if (context.HttpContext.Request.Headers.Keys.Contains("If-None-Match"))
                    {
                        context.Result = new StatusCodeResult(304);
                    }
                    context.HttpContext.Response.Headers.Add("ETag", new[]{etag});

                }
            }
        }
    }
}
