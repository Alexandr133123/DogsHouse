using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using DogsHouse.Application.Common.Exceptions;
using FluentValidation;
namespace DogsHouse.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionsAsync(context, ex);
            }
        }

        private Task HandleExceptionsAsync(HttpContext context, Exception ex) 
        {
            var code = HttpStatusCode.InternalServerError;
            var result = String.Empty;
            switch (ex)
            {
                case FluentValidation.ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case SortArgumentNotFoundException:
                    code = HttpStatusCode.BadRequest;
                    result = ex.Message;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == String.Empty)
            {
                result = JsonSerializer.Serialize(new { errpr = ex.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
