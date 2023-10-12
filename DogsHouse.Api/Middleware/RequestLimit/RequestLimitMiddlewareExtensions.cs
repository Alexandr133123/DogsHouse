namespace DogsHouse.Api.Middleware.RequestLimit
{
    public static class RequestLimitMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLimitHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLimitMiddleware>(8, TimeSpan.FromSeconds(5));
        }
    }
}
