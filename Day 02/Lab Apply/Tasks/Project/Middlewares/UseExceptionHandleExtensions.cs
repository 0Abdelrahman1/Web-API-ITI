namespace Project.Middlewares
{
    public static class UseExceptionHandleExtensions
    {
        public static IApplicationBuilder UseExceptionHandle(this IApplicationBuilder app) => app.UseMiddleware<ExceptionHandleMiddleware>();
    }
}
