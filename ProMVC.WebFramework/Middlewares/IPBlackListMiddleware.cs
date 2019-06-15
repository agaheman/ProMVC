using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ProMVC.WebFramework.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProMVC.WebFramework.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class IPBlackListMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IPListConfiguration iPListConfiguration;
        private readonly IEnumerable<IPAddress> blockedIPs;

        public IPBlackListMiddleware(RequestDelegate next,IPListConfiguration iPListConfiguration)
        {
            _next = next;

            this.blockedIPs = iPListConfiguration.BlockedIPs.Select(item => IPAddress.Parse(item));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var remoteIp = httpContext.Connection.RemoteIpAddress;

            //var remoteIpBytes = remoteIp.GetAddressBytes();

            if (blockedIPs.Contains(remoteIp))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            await _next.Invoke(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class IPBlackListMiddlewareExtensions
    {
        public static IApplicationBuilder UseIPBlackListMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IPBlackListMiddleware>();
        }
    }
}
