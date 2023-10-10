using DivPay.AuthProvider.Models;
using System.Net;
using System.Text.Json;

namespace DivPay.AuthProvider.Filters
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var request = httpContext.Request;
                logger.LogError(ex, $"{request.Scheme} {request.Host}{request.Path} Erro: {ex.Message}");

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = ErrorResponse.From(ex);

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }
    }
}