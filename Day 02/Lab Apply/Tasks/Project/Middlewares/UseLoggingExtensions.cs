namespace Project.Middlewares
{
    public static class UseLoggingExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
            => app.UseMiddleware<LoggingMiddleware>();
    }
}
