using DogsHouse.Api.Middleware;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace DogsHouse.Presentation.Tests.Middleware
{
    public class RequestLimitMiddlewareTests
    {
        [Fact]
        public async Task Middleware_Should_Return_TooManyRequests_When_Exceeding_MaxRequests()
        {
            var middleware = new RequestLimitMiddleware(
                context => Task.CompletedTask,
                maxRequests: 2,
                interval: TimeSpan.FromSeconds(10));

            var httpContext = new DefaultHttpContext();

            for (var i = 0; i < 10; i++)
            {
                httpContext.Response.Body = new MemoryStream();
                await middleware.Invoke(httpContext);
            }

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = new StreamReader(httpContext.Response.Body).ReadToEnd();

            Assert.Equal((int)HttpStatusCode.TooManyRequests, httpContext.Response.StatusCode);
            Assert.Equal("Too Many Requests", responseText);
        }
    }
}