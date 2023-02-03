using Microsoft.AspNetCore.Builder;


public static class ApplicationBuildExtensionsSwagger
{
    public static void UseSwaggerConfig(this IApplicationBuilder app)
    {
        
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Galdino-Consumer-V1");
            c.RoutePrefix = string.Empty;
        });
    }
}