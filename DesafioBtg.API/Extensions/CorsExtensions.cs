namespace DesafioBtg.API.Extensions;

public static class CorsExtensions
{
    public static IApplicationBuilder AddCorsExtensions(this IApplicationBuilder app)
    {
        return app.UseCors(c =>
        {
            c.AllowAnyHeader();
            c.AllowAnyMethod();
            c.WithOrigins();
            c.AllowCredentials();
            c.WithExposedHeaders("Authorization");
        });
    }
}
