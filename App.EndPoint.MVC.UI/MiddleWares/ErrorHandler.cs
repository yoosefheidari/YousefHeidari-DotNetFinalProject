using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.EndPoint.MVC.UI.MiddleWares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandler> _logger;

        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
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
            catch (Exception error)
            {

                var response = httpContext.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case BadHttpRequestException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        _logger.LogError(error, "درخواست اشتباه داده شده است");
                        break;
                    case NullReferenceException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        _logger.LogError(error, "داده درخواستی وجود ندارد");
                        break;
                    case KeyNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        _logger.LogError(error, "داده درخواستی وجود ندارد");
                        break;
                    case DirectoryNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        _logger.LogError(error, "مسیر درخواستی وجود ندارد");
                        break;
                    case FileNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        _logger.LogError(error, "فایل درخواستی وجود ندارد");
                        break;
                    case AggregateException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        _logger.LogCritical(error, "خطا در برقراری ارتباط با دیتابیس");
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        _logger.LogError(error, "خطایی رخ داده است");
                        break;
                }
                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);       
                httpContext.Response.Redirect("/Home/Error");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandler>();
        }
    }
}
