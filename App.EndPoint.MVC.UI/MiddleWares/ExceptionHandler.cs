using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace App.EndPoint.MVC.UI.MiddleWares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandler2
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler2> _logger;

        public ExceptionHandler2(RequestDelegate next, ILogger<ExceptionHandler2> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطایی رخ داده است");
                httpContext.Response.Redirect("Home/Error");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseExceptionHandler2(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandler2>();
        }
    }
}
