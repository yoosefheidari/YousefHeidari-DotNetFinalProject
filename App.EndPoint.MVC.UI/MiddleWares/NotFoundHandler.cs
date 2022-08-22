using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace App.EndPoint.MVC.UI.MiddleWares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class NotFoundHandler
    {
        private readonly RequestDelegate _next;

        public NotFoundHandler(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                httpContext.Response.Redirect("/Home/Error");

            return _next(httpContext);

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class NotFoundHandlerExtensions
    {
        public static IApplicationBuilder UseNotFoundHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NotFoundHandler>();
        }
    }
}
