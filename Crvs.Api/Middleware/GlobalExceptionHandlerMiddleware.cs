using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Crvs.Api.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var details = new ProblemDetails
                {
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = "An application error occurred",
                };
                _logger.LogError(ex, "An error occurred");
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(details);
            }
        }
    }
    //public class GlobalExceptionHandlerMiddleware : IMiddleware
    //{
    //    private readonly ILogger _logger;

    //    public GlobalExceptionHandlerMiddleware(ILogger logger)
    //    {
    //        _logger = logger;
    //    }
    //    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    //    {
    //        try
    //        {
    //            await next(context);
    //        }
    //        catch (Exception ex)
    //        {
    //            var details = new ProblemDetails
    //            {
    //                Detail = ex.Message,
    //                Status = (int)HttpStatusCode.BadRequest,
    //                Title = "An unhandled exception occurred",
    //            };
    //            _logger.LogError(ex, "Unhandled exception");
    //            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    //            await context.Response.WriteAsJsonAsync(details);
    //        }
    //    }
    //}

}
