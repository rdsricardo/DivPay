using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DivPay.API.Models;

namespace DivPay.API.Filters
{
    public class ErrorResponseExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<RequestResponseLoggingMiddleware> logger;

        public ErrorResponseExceptionFilter(ILogger<RequestResponseLoggingMiddleware> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var request = context.HttpContext.Request;

            logger.LogError(context.Exception, $"{request.Scheme} {request.Host}{request.Path} Erro: {context.Exception.Message}");

            context.Result = new ObjectResult(ErrorResponse.From(context.Exception)) { StatusCode = 500 };
        }
    }
}
