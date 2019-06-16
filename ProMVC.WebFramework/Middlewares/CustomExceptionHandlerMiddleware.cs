using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProMVC.WebFramework.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;
        //private HttpStatusCode httpStatusCode;


        public CustomExceptionHandlerMiddleware(RequestDelegate next, IHostingEnvironment env,)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string message = null;
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace,
                    };
                    if (exception.InnerException != null)
                    {
                        dic.Add("InnerException.Exception", exception.InnerException.Message);
                        dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                    }

                    message = JsonConvert.SerializeObject(dic);
                }
                else
                {
                    message = exception.Message;
                }

                //httpContext.Response.StatusCode = (int)httpStatusCode; exeptionStatusCode
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(message);
            }
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
