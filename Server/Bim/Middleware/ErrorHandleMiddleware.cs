using Newtonsoft.Json;
using System.Net;

namespace Bim.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlerMiddleware> log;
        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            log = loggerFactory.CreateLogger<ErrorHandlerMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new
            {
                Success = false,
                Message = GetAllMessages(exception),
                StackTrace = GetAllStackTrace(exception),
            };
            switch (exception)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            log.LogError(exception, exception.Message);

            var result = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(result);
        }

        public static string GetAllMessages(Exception ex)
        {
            if (ex.InnerException == null)
                return ex.Message;

            return $"\r\n{GetAllMessages(ex.InnerException)}";
        }

        public static string GetAllStackTrace(Exception ex)
        {
            if (ex.InnerException == null)
                return ex.StackTrace ?? string.Empty;

            return $"\r\n{GetAllStackTrace(ex.InnerException)}";
        }
    }
}
