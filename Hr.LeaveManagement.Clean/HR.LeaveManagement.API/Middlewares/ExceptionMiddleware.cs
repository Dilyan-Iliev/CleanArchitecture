using HR.LeaveManagement.API.Models;
using HR.LeaveManagement.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace HR.LeaveManagement.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
                throw;
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            //default status code
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            object problem = default!;

            switch (ex)
            {
                case BadRequestException badRequestEx:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomProblemDetails
                    {
                        Title = badRequestEx.Message,
                        Status = (int)statusCode,
                        Detail = badRequestEx.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = badRequestEx.ValidationErrors
                    };
                    break;
                case NotFoundException notFoundEx:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomProblemDetails
                    {
                        Title = notFoundEx.Message,
                        Status = (int)statusCode,
                        Detail = notFoundEx.InnerException?.Message,
                        Type = nameof(NotFoundException),
                    };
                    break;
                default:
                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Detail = ex.StackTrace,
                        Type = nameof(HttpStatusCode.InternalServerError),
                    };
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            var logMsg = JsonConvert.SerializeObject(problem);
            _logger.LogError(logMsg);
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
