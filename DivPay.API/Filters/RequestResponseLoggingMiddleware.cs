namespace DivPay.API.Filters
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestResponseLoggingMiddleware> logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            this.next = (next ?? throw new ArgumentNullException(nameof(next)));
            this.logger = (logger ?? throw new ArgumentNullException(nameof(logger)));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Capture the request
            var request = await FormatRequest(context.Request);

            logger.LogInformation($"Request: {request}");

            // Capture the response
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await next(context);

                // Capture the response body
                var response = await FormatResponse(context.Response);

                logger.LogInformation($"Response: {response}");

                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {body}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"{response.StatusCode}: {text}";
        }
    }
}
