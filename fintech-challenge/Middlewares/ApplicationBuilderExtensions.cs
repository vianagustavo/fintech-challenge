namespace FintechChallenge.Middlewares;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder AddErrorHandler(this IApplicationBuilder applicationBuilder)
    => applicationBuilder.UseMiddleware<ErrorHandlerMiddleware>();
}