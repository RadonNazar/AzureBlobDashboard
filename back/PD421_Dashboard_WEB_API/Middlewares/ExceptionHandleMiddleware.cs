using PD421_Dashboard_WEB_API.BLL.Services;

namespace PD421_Dashboard_WEB_API.Middlewares
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandleMiddleware> _logger;

        public ExceptionHandleMiddleware(RequestDelegate next, ILogger<ExceptionHandleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // request
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log
                string logMessage = ex.Message;
                if(ex.InnerException != null)
                {
                    logMessage += "\n" + ex.InnerException.Message;
                }
                _logger.LogError(logMessage);

                string error = ex.InnerException != null 
                    ? ex.InnerException.Message 
                    : ex.Message;

                var response = new ServiceResponse
                {
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Message = error
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(response);
            }
            // response
        }
    }
}
