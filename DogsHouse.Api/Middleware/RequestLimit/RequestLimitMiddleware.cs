using System.Net;

namespace DogsHouse.Api.Middleware
{
    public class RequestLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _maxRequests;
        private readonly TimeSpan _interval;
        private DateTime _intervalStart;
        private int _requestCount;

        public RequestLimitMiddleware(
            RequestDelegate next,
            int maxRequests,
            TimeSpan interval)
        {
            _next = next;
            _maxRequests = maxRequests;
            _interval = interval;
            _intervalStart = DateTime.UtcNow;
            _requestCount = 0;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentTime = DateTime.UtcNow;

            if (currentTime - _intervalStart > _interval)
            {
                _requestCount = 0;
                _intervalStart = currentTime;
            }

            if (_requestCount > _maxRequests)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await context.Response.WriteAsync("Too Many Requests");
                return;
            }

            _requestCount++;

            await _next(context);
        }
    }
}