namespace DesafioBtg.Consumers.Extensions;

public static class CorsConsumersExtensions
{
    public static IApplicationBuilder AddCorsConsumersExtensions(this IApplicationBuilder app)
    {
        return app.UseCors(c =>
        {
            c.AllowAnyHeader();
            c.AllowAnyMethod();
            c.AllowAnyOrigin();
        });
    }
}
