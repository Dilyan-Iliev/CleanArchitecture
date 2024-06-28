using HR.LeaveManagement.API.Models;
using HR.LeaveManagement.Application.Exceptions;
using System.Net;

namespace HR.LeaveManagement.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
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
            HttpStatusCode defaultStatusCode = HttpStatusCode.InternalServerError;

            var (statusCode, problem) = ex switch
            {
                BadRequestException badRequestEx => (
                    HttpStatusCode.BadRequest,
                    (object)new CustomProblemDetails
                    {
                        Title = badRequestEx.Message,
                        Status = (int)HttpStatusCode.BadRequest,
                        Detail = badRequestEx.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = badRequestEx.ValidationErrors
                    }
                ),
                NotFoundException notFoundEx => (
                    HttpStatusCode.NotFound,
                    (object)new CustomProblemDetails
                    {
                        Title = notFoundEx.Message,
                        Status = (int)HttpStatusCode.NotFound,
                        Detail = notFoundEx.InnerException?.Message,
                        Type = nameof(NotFoundException),
                    }
                ),
                _ => (
                    defaultStatusCode,
                    (object)new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)defaultStatusCode,
                        Detail = ex.StackTrace,
                        Type = nameof(HttpStatusCode.InternalServerError),
                    }
                )
            };

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
