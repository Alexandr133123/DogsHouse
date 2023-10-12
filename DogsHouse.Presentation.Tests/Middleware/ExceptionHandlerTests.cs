using DogsHouse.Api.Middleware.ExceptionHandler;
using DogsHouse.Application.Common.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace DogsHouse.Presentation.Tests.Middleware
{
    public class ExceptionHandlerTests
    {
        [Fact]
        public async Task Middleware_Should_Return_NotFound_When_Handles_Exception()
        {
            var middleware = new ExceptionHandlerMiddleware(
                context => throw new NotFoundException("testName", 1)
                );

            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await middleware.Invoke(httpContext);

            Assert.Equal((int)HttpStatusCode.NotFound, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Middleware_Should_Return_BadRequest_When_Handles_Exception()
        {
            var middleware = new ExceptionHandlerMiddleware(
                context => throw new ValidationException("validation error")
                );

            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await middleware.Invoke(httpContext);

            Assert.Equal((int)HttpStatusCode.BadRequest, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Middleware_Should_Return_BadRequest_With_Message_When_Handles_Exception()
        {
            string attribute = "test-attribute";
            var middleware = new ExceptionHandlerMiddleware(
                context => throw new SortArgumentNotFoundException(attribute)
                );

            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await middleware.Invoke(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = new StreamReader(httpContext.Response.Body).ReadToEnd();

            Assert.Equal((int)HttpStatusCode.BadRequest, httpContext.Response.StatusCode);
            Assert.Equal($"\"Sorting attribute ({attribute}) not found.\"", responseText);
        }
    }
}
