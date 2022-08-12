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
                if (ex.GetType() == typeof(NullReferenceException))
                    _logger.LogError(ex, "داده درخواستی وجود ندارد");

                else if (ex.GetType() == typeof(DirectoryNotFoundException))
                    _logger.LogError(ex, "مسیر درخواستی وجود ندارد");

                else if (ex.GetType() == typeof(ArgumentNullException))
                    _logger.LogError(ex, "مقدار وارد شده صحیح نیست");

                else if (ex.GetType() == typeof(FileNotFoundException))
                    _logger.LogError(ex, "فایل درخواستی شما وجود ندارد");

                else if (ex.GetType() == typeof(AggregateException))
                    _logger.LogCritical(ex, "ارتباط با دیتابیس خطا دارد");

                else
                {
                    _logger.LogError(ex, "خطایی رخ داده است");                    
                }

                httpContext.Response.Redirect("/Home/Error");
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
