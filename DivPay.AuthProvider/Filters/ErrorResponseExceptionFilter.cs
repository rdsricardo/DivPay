using DivPay.AuthProvider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DivPay.AuthProvider.Filters
{
    public class ErrorResponseExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ErrorResponseExceptionFilter> logger;

        public ErrorResponseExceptionFilter(ILogger<ErrorResponseExceptionFilter> logger)
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
