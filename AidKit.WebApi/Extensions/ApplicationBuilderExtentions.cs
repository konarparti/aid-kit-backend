using AidKit.WebApi.Middleware;

namespace AidKit.WebApi.Extensions
{
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder AddExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionMiddleware>();

            return applicationBuilder;
        }
    }
}
