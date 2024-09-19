
using IdealTimeTracker.API.Exceptions;
using IdealTimeTracker.API.Models;

namespace IdealTimeTracker.API.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _logger.LogError(ex, ex.StackTrace);

                if(ex is ApplicationConfigurationException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    Error error = new Error(404, ex.Message);
                    await context.Response.WriteAsJsonAsync(error);
                }
                if (ex is UserException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    Error error = new Error(404, ex.Message);
                    await context.Response.WriteAsJsonAsync(error);
;               }

                else if (ex is UserActivityException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    Error error = new Error(404, ex.Message);
                    await context.Response.WriteAsJsonAsync(error);
                }

                else if (ex is UserLogException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    Error error = new Error(404, ex.Message);
                    await context.Response.WriteAsJsonAsync(error);
                }

                else if (ex is ContextException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    Error error = new Error(404, ex.Message);
                    await context.Response.WriteAsJsonAsync(error);
                }

                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    Error error = new Error(500, "Something went wrong");
                    await context.Response.WriteAsJsonAsync(error);
                }

            }
        }
    }
}
