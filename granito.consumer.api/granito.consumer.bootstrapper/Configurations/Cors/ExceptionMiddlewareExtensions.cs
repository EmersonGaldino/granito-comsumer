

using Microsoft.AspNetCore.Builder;

namespace granito.bootstrapper.Configurations.Cors;

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ExceptionMiddleware>();
}